Imports System.ComponentModel
Imports System.Net
Imports System.Threading

Public Class DownloaderForm
    ''' <summary>
    ''' 是否允许窗体关闭
    ''' </summary>
    Public CanClose As Boolean
    Public IsDownloading As Boolean
    Dim LastProgressPercentage As Integer = 0
    Dim UpdateClient As WebClient

    ''' <summary>
    ''' 下次点击按钮的任务状态
    ''' </summary>
    Private Enum ClickToDo
        DownloadUpdate '下载更新
        CancelUpdate '取消下载
        RetryToUpdate '重试更新
        Updated '更新完毕
    End Enum
    Dim ButtonState = ClickToDo.DownloadUpdate
    Dim DLFilePath As String

#Region "窗体事件"
    Private Sub DownloaderForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        CheckForIllegalCrossThreadCalls = False
    End Sub

    Private Sub MoveWindow(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        '鼠标通过控件拖动窗体
        UnityModule.ReleaseCapture()
        UnityModule.SendMessageA(Me.Handle, &HA1, 2, 0&)
    End Sub

    Private Sub DownloaderForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If CanClose Then Exit Sub
        e.Cancel = True
        CloseButton_Click(sender, e)
    End Sub
#End Region

#Region "按钮动态效果"
    Private Sub CloseButton_MouseEnter(sender As Object, e As EventArgs) Handles CloseButton.MouseEnter, DLOKButton.MouseEnter, DLCancelButton.MouseEnter
        With CType(sender, Label)
            .Image = My.Resources.SystemAssets.ResourceManager.GetObject(.Tag & "_1")
        End With
    End Sub

    Private Sub CloseButton_MouseLeave(sender As Object, e As EventArgs) Handles CloseButton.MouseLeave, DLOKButton.MouseLeave, DLCancelButton.MouseLeave
        With CType(sender, Label)
            .Image = My.Resources.SystemAssets.ResourceManager.GetObject(.Tag & "_0")
        End With
    End Sub

    Private Sub CloseButton_MouseDown(sender As Object, e As MouseEventArgs) Handles CloseButton.MouseDown, DLOKButton.MouseDown, DLCancelButton.MouseDown
        With CType(sender, Label)
            .Image = My.Resources.SystemAssets.ResourceManager.GetObject(.Tag & "_2")
        End With
    End Sub

    Private Sub CloseButton_MouseUp(sender As Object, e As MouseEventArgs) Handles CloseButton.MouseUp, DLOKButton.MouseUp, DLCancelButton.MouseUp
        With CType(sender, Label)
            .Image = My.Resources.SystemAssets.ResourceManager.GetObject(.Tag & "_1")
        End With
    End Sub
#End Region

#Region "控件"

    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        If IsDownloading Then
            DownloadLabel.Text = "正在下载更新，确定要取消下载吗？"
            ButtonState = ClickToDo.CancelUpdate
            DLOKButton.Show()
            DLCancelButton.Show()
            DLProgressBar.Hide()
        Else
            CloseForm()
        End If
    End Sub

    Private Sub DLOKButton_Click(sender As Object, e As EventArgs) Handles DLOKButton.Click
        Select Case ButtonState
            Case ClickToDo.DownloadUpdate
                '下载更新
                DownloadUpdateFunction()
            Case ClickToDo.CancelUpdate
                DownloadLabel.Text = "正在停止下载..."
                '终止下载任务
                CancelDownload()
            Case ClickToDo.RetryToUpdate
                '重新下载
                DownloadUpdateFunction()
            Case ClickToDo.Updated
                DownloadLabel.Text = "正在关闭程序..."
                CloseForm()
                ShutdownTips.Shutdown()
        End Select
    End Sub

    Private Sub DLCacncelButton_Click(sender As Object, e As EventArgs) Handles DLCancelButton.Click
        Select Case ButtonState
            Case ClickToDo.DownloadUpdate
                CloseForm()
            Case ClickToDo.CancelUpdate
                DownloadLabel.Text = "正在下载更新...(" & LastProgressPercentage & "%)"
                Dim ProgressBarWidth As Integer = 8 + (LastProgressPercentage * 234) \ 100
                DLProgressBar.Image = My.Resources.SystemAssets.DownloadProgress.Clone(New Rectangle(0, 0, ProgressBarWidth, 19), Imaging.PixelFormat.Format32bppArgb)
                DLOKButton.Hide()
                DLCancelButton.Hide()
                DLProgressBar.Show()
            Case ClickToDo.RetryToUpdate
                CloseForm()
            Case ClickToDo.Updated
                CloseForm()
        End Select
    End Sub
#End Region

#Region "功能函数"
    ''' <summary>
    ''' 关闭窗体
    ''' </summary>
    Private Sub CloseForm()
        CanClose = True
        Me.Close()
    End Sub

    ''' <summary>
    ''' 取消下载更新
    ''' </summary>
    Public Sub CancelDownload()
        UpdateClient.CancelAsync()
        UpdateClient.Dispose()
        '清除缓存
        Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf WaitToDeleteCacheFile))
        CloseForm()
    End Sub

    ''' <summary>
    ''' 开始下载更新
    ''' </summary>
    Private Sub DownloadUpdateFunction()
        Dim SaveDialog As SaveFileDialog = New SaveFileDialog With {
            .AddExtension = True,
            .CheckPathExists = True,
            .InitialDirectory = Application.StartupPath,
            .FileName = "Hack System_New",
            .Filter = "EXE 可执行程序|*.exe",
            .Title = "请选择新版本 Hack-System 保存位置：",
            .ValidateNames = True
        }
        If SaveDialog.ShowDialog = DialogResult.OK Then
            LastProgressPercentage = 0
            DownloadLabel.Text = "正在下载更新...(0%)"
            DLProgressBar.Image = Nothing
            IsDownloading = True
            DLOKButton.Hide()
            DLCancelButton.Hide()
            DLProgressBar.Show()
            DLFilePath = SaveDialog.FileName
            If IO.File.Exists(DLFilePath) Then IO.File.Delete(DLFilePath)

            '开始下载
            UpdateClient = New WebClient
            AddHandler UpdateClient.DownloadFileCompleted, AddressOf DownloadFileCompleted
            AddHandler UpdateClient.DownloadProgressChanged, AddressOf DownloadProgressChanged
            UpdateClient.DownloadFileAsync(New Uri("https://raw.githubusercontent.com/CuteLeon/FileRepository/master/HackSystem-Execute/Hack%20System.exe"), DLFilePath)
        End If
    End Sub

    ''' <summary>
    ''' 文件下载结束
    ''' </summary>
    Private Sub DownloadFileCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        'e.Cancelled 成立时表示是取消了异步下载任务，不做完成处理
        If e.Cancelled = True Then Exit Sub
        IsDownloading = False
        If e.Error Is Nothing Then
            My.Computer.Audio.Play(My.Resources.TipsRes.TipsAlarm, AudioPlayMode.Background)
            DownloadLabel.Text = "更新成功，请运行新版程序。" & vbCrLf & "确定要立即退出当前版本吗？"
            ButtonState = ClickToDo.Updated
        Else
            My.Computer.Audio.Play(My.Resources.SystemAssets.ShowConsole, AudioPlayMode.Background)
            IO.File.Delete(DLFilePath)
            DownloadLabel.Text = "更新失败，请检查网络或更换路径。" & vbCrLf & "确定要重试吗？"
            ButtonState = ClickToDo.RetryToUpdate
        End If

        UpdateClient.Dispose()
        DLProgressBar.Hide()
        DLOKButton.Show()
        DLCancelButton.Show()
        Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf UnityModule.QQ_Vibration), Me)
    End Sub

    ''' <summary>
    ''' 文件下载进度更新
    ''' </summary>
    Private Sub DownloadProgressChanged(ByVal sender As Object, ByVal e As System.Net.DownloadProgressChangedEventArgs)
        If e.ProgressPercentage = LastProgressPercentage Then Exit Sub
        LastProgressPercentage = e.ProgressPercentage
        If DLProgressBar.Visible = False Then Exit Sub
        Dim ProgressBarWidth As Integer = 8 + (LastProgressPercentage * 234) \ 100
        DownloadLabel.Text = "正在下载更新...(" & LastProgressPercentage & "%)"
        DLProgressBar.Image = My.Resources.SystemAssets.DownloadProgress.Clone(New Rectangle(0, 0, ProgressBarWidth, 19), Imaging.PixelFormat.Format32bppArgb)
    End Sub

    ''' <summary>
    ''' 等待50毫秒尝试删除取消下载的缓存文件
    ''' </summary>
    Private Sub WaitToDeleteCacheFile()
        On Error Resume Next
        Do While (IO.File.Exists(DLFilePath))
            IO.File.Delete(DLFilePath)
            Thread.Sleep(50)
        Loop
    End Sub
#End Region

End Class