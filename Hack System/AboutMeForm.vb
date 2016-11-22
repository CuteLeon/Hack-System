Imports System.ComponentModel
Imports System.Net

Public Class AboutMeForm

#Region "窗体和控件"
    Dim CheckClient As WebClient

    Private Sub AboutMeForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        '设置控件的父容器
        AboutMeControl.Parent = AboutMeWallpaperControl
        OKButtonControl.Parent = AboutMeControl
        WebLink.Parent = AboutMeControl
        CheckUpdateLabel.Parent = AboutMeControl
    End Sub

    Private Sub AboutMeControl_MouseDown(sender As Object, e As MouseEventArgs) Handles AboutMeControl.MouseDown
        '允许鼠标拖动窗体
        UnityModule.ReleaseCapture()
        UnityModule.SendMessageA(Me.Handle, &HA1, 2, 0&)
    End Sub

    Private Sub AboutMeForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        CloseMe(True)
    End Sub

    Private Sub OKButtonControl_Click(sender As Object, e As EventArgs) Handles OKButtonControl.Click
        '点击按钮隐藏
        CloseMe()
    End Sub

    Private Sub AboutMe_KeyPress(sender As Object, e As KeyPressEventArgs) Handles AboutMeControl.KeyPress, Me.KeyPress, WebLink.KeyPress
        '按 [Enter] 或 [Esc] 隐藏
        Dim KeyAscii As Integer = Asc(e.KeyChar)
        If KeyAscii = 27 Or KeyAscii = 13 Then
            CloseMe()
        End If
    End Sub

    Private Sub WebLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles WebLink.LinkClicked
        '访问官网
        SystemWorkStation.LoadNewBrowser("http://www.HackSystem.icoc.in/")
    End Sub

    Private Sub CheckUpdateLabel_Click(sender As Object, e As EventArgs) Handles CheckUpdateLabel.Click
        CheckUpdate()
    End Sub

#End Region

#Region "按钮动态效果"

    Private Sub ButtonControl_MouseDown(sender As Object, e As MouseEventArgs) Handles OKButtonControl.MouseDown, CheckUpdateLabel.MouseDown
        With CType(sender, Label)
            .Image = My.Resources.SystemAssets.ResourceManager.GetObject(.Tag & "_2")
        End With
    End Sub

    Private Sub ButtonControl_MouseEnter(sender As Object, e As EventArgs) Handles OKButtonControl.MouseEnter, CheckUpdateLabel.MouseEnter
        With CType(sender, Label)
            .Image = My.Resources.SystemAssets.ResourceManager.GetObject(.Tag & "_1")
        End With
    End Sub

    Private Sub ButtonControl_MouseLeave(sender As Object, e As EventArgs) Handles OKButtonControl.MouseLeave, CheckUpdateLabel.MouseLeave
        With CType(sender, Label)
            .Image = My.Resources.SystemAssets.ResourceManager.GetObject(.Tag & "_0")
        End With
    End Sub

    Private Sub ButtonControl_MouseUp(sender As Object, e As MouseEventArgs) Handles OKButtonControl.MouseUp, CheckUpdateLabel.MouseUp
        With CType(sender, Label)
            .Image = My.Resources.SystemAssets.ResourceManager.GetObject(.Tag & "_1")
        End With
    End Sub

#End Region

#Region "功能函数"
    ''' <summary>
    ''' 以各种方式关闭关于窗口
    ''' </summary>
    Private Sub CloseMe(Optional CancelClose As Boolean = False)
        CheckUpdateLabel.Text = "检查更新"
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        UnityModule.SetForegroundWindow(SystemWorkStation.Handle)
        If Not CancelClose Then Me.Close()
    End Sub

    ''' <summary>
    ''' 检查更新（需要在 FileRepository/HackSystem-Execute/Version.txt 里记录最新的版本号）
    ''' </summary>
    Public Sub CheckUpdate()
        '防止重复检查更新
        If DownloaderForm.Visible Then
            My.Computer.Audio.Play(My.Resources.SystemAssets.ShowConsole, AudioPlayMode.Background)
            Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf UnityModule.QQ_Vibration), DownloaderForm)
            Exit Sub
        End If

        Try
            CheckUpdateLabel.Text = "正在检查..."
            Me.Refresh()
            If CheckClient IsNot Nothing AndAlso CheckClient.IsBusy Then Exit Sub
            If Not (My.Computer.Network.Ping("raw.githubusercontent.com")) Then
                CheckUpdateLabel.Text = "检查更新"
                TipsForm.PopupTips(SystemWorkStation, "更新错误：", UnityModule.TipsIconType.Critical, "无法连接到更新服务器！")
                Exit Sub
            End If
            CheckClient = New WebClient
            AddHandler CheckClient.DownloadStringCompleted, AddressOf DownloadStringCompleted
            CheckClient.DownloadStringAsync(New Uri("https://raw.githubusercontent.com/CuteLeon/FileRepository/master/HackSystem-Execute/Version.txt"))
        Catch ex As Exception
            CheckUpdateLabel.Text = "检查更新"
            TipsForm.PopupTips(SystemWorkStation, "更新错误：", UnityModule.TipsIconType.Critical, ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 版本号文本下载结束
    ''' </summary>
    Private Sub DownloadStringCompleted(ender As Object, e As System.Net.DownloadStringCompletedEventArgs)
        'e.Cancelled 成立时表示是取消了异步下载任务，不做完成处理
        If e.Cancelled = False Then
            If e.Error Is Nothing Then
                Dim NewestVersion As String = e.Result
                If Application.ProductVersion = NewestVersion Then
                    TipsForm.PopupTips(SystemWorkStation, "无需更新：", UnityModule.TipsIconType.Infomation, "当前已经是最新版本！")
                Else
                    DownloaderForm.DownloadLabel.Text = "发现新版本（" & NewestVersion & "），是否更新？"
                    DownloaderForm.Show(SystemWorkStation)
                End If
            Else
                TipsForm.PopupTips(SystemWorkStation, "更新错误：", UnityModule.TipsIconType.Critical, e.Error.Message)
            End If
        End If
        CheckUpdateLabel.Text = "检查更新"
        CheckClient.Dispose()
    End Sub

#End Region

End Class
