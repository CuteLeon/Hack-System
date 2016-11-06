Imports System.ComponentModel
Imports System.Threading

Public Class DownloaderForm
    '网络下载文件，提供从GitHub更新
    Private Declare Function URLDownloadToFile Lib "urlmon" Alias "URLDownloadToFileA" (ByVal pCaller As Integer, ByVal szURL As String, ByVal szFileName As String, ByVal dwReserved As Integer, ByVal lpfnCB As Integer) As Integer
    ''' <summary>
    ''' 是否允许窗体关闭
    ''' </summary>
    Public CanClose As Boolean
    Private IsDownloading As Boolean
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
    Dim DownloadUpdateThread As Threading.Thread
    Private BytesCount As Integer = 1024 * 1024 * 100

#Region "窗体事件"
    Private Sub DownloaderForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
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

                'TODO: 在这里使用 e_abort 终止下载任务
                CheckFileSizeTimer.Stop()
                DownloadUpdateThread.Abort()
                DownloadUpdateThread = Nothing
                'DeleteUrlCacheEntry
                'TODO:这里需要清除缓存
                'IO.File.Delete(DLFilePath)
                CloseForm()
            Case ClickToDo.RetryToUpdate
                '重新下载
                DownloadLabel.Text = "正在重新下载..."
                DownloadUpdateFunction()
            Case ClickToDo.Updated
                DownloadLabel.Text = "正在关闭程序..."
                'TODO:关闭程序，打开新版程序
                CloseForm()
        End Select
    End Sub

    Private Sub DLCacncelButton_Click(sender As Object, e As EventArgs) Handles DLCancelButton.Click
        Select Case ButtonState
            Case ClickToDo.DownloadUpdate
                CloseForm()
            Case ClickToDo.CancelUpdate
                DownloadLabel.Text = "正在下载更新..."
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

    Private Sub DownloadUpdateFunction()
        Dim SaveDialog As SaveFileDialog = New SaveFileDialog With {
            .AddExtension = True,
            .CheckPathExists = True,
            .InitialDirectory = Application.StartupPath,
            .FileName = "HackSystem_New",
            .Filter = "EXE 可执行程序|*.exe",
            .Title = "请选择新版本 Hack-System 保存位置：",
            .ValidateNames = True
        }
        If SaveDialog.ShowDialog = DialogResult.OK Then
            DownloadLabel.Text = "正在下载更新..."
            IsDownloading = True
            DLOKButton.Hide()
            DLCancelButton.Hide()
            DLProgressBar.Show()
            DLFilePath = SaveDialog.FileName
            If IO.File.Exists(DLFilePath) Then IO.File.Delete(DLFilePath)

            '这里使用多线程下载
            DownloadUpdateThread = New Thread(AddressOf DownloadUpdate)
            DownloadUpdateThread.Start()
            '开始检测文件下载进度
            CheckFileSizeTimer.Start()
        End If
    End Sub

    ''' <summary>
    ''' 由多线程调用的下载函数
    ''' </summary>
    Private Sub DownloadUpdate()
        Dim DownloadResult As Integer 'URLDownloadToFile 返回的下载结果
        DownloadResult = URLDownloadToFile(0, "https://raw.githubusercontent.com/CuteLeon/FileRepository/master/HackSystem-Execute/Hack%20System.exe", DLFilePath, 0, 0)
        CheckFileSizeTimer.Stop()
        IsDownloading = False
        If DownloadResult = 0 Then
            DownloadLabel.Text = "更新成功，请运行新版程序。" & vbCrLf & "确定要立即退出当前版本吗？"
            ButtonState = ClickToDo.Updated
        Else
            DownloadLabel.Text = "更新失败，请检查网络或更换路径。" & vbCrLf & "确定要重试吗？"
            ButtonState = ClickToDo.RetryToUpdate
        End If
        DLProgressBar.Hide()
        DLOKButton.Show()
        DLCancelButton.Show()
    End Sub

    Private Sub CheckFileSizeTimer_Tick(sender As Object, e As EventArgs) Handles CheckFileSizeTimer.Tick
        If Not IO.File.Exists(DLFilePath) Then Exit Sub
        Dim FileInfo As IO.FileInfo = New IO.FileInfo(DLFilePath)
        Dim ProgressBarWidth As Integer = 8 + (FileInfo.Length / BytesCount) * 234
        Static LastWidth As Integer = 8
        If ProgressBarWidth < 9 OrElse ProgressBarWidth > 234 Then Exit Sub
        If ProgressBarWidth = LastWidth Then Exit Sub
        LastWidth = ProgressBarWidth
        'Debug.Print("文件下载进度： " & FileInfo.Length)
        'Debug.Print("进度条长度：" & ProgressBarWidth)
        DLProgressBar.Image = My.Resources.SystemAssets.DownloadProgress.Clone(New Rectangle(0, 0, ProgressBarWidth, 19), Imaging.PixelFormat.Format32bppArgb)
    End Sub

#End Region

End Class