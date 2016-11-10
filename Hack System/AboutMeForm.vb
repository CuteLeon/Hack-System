Public Class AboutMeForm

#Region "窗体和控件"

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

    Private Sub OKButtonControl_Click(sender As Object, e As EventArgs) Handles OKButtonControl.Click
        '点击按钮隐藏
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        Me.Hide()
        UnityModule.SetForegroundWindow(SystemWorkStation.Handle)
    End Sub

    Private Sub AboutMe_KeyPress(sender As Object, e As KeyPressEventArgs) Handles AboutMeControl.KeyPress, Me.KeyPress, WebLink.KeyPress
        '按 [Enter] 或 [Esc] 隐藏
        Dim KeyAscii As Integer = Asc(e.KeyChar)
        If KeyAscii = 27 Or KeyAscii = 13 Then
            My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
            Me.Hide()
            UnityModule.SetForegroundWindow(SystemWorkStation.Handle)
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
            If Not (My.Computer.Network.Ping("raw.githubusercontent.com")) Then Exit Sub
            Dim VersionFileAdress As String = "https://raw.githubusercontent.com/CuteLeon/FileRepository/master/HackSystem-Execute/Version.txt"
            Dim WebStream As IO.Stream = Net.WebRequest.Create(VersionFileAdress).GetResponse().GetResponseStream()
            Dim WebStreamReader As IO.StreamReader = New IO.StreamReader(WebStream, System.Text.Encoding.UTF8)
            Dim NewestVersion As String = WebStreamReader.ReadToEnd
            WebStreamReader.Dispose()
            If Application.ProductVersion = NewestVersion Then
                TipsForm.PopupTips(SystemWorkStation, "无需更新：", UnityModule.TipsIconType.Infomation, "当前已经是最新版本！")
            Else
                DownloaderForm.DownloadLabel.Text = "发现新版本（" & NewestVersion & "），是否更新？"
                DownloaderForm.Show(SystemWorkStation)
            End If
        Catch ex As Exception
            TipsForm.PopupTips(SystemWorkStation, "更新错误：", UnityModule.TipsIconType.Critical, ex.Message)
        End Try
    End Sub
#End Region

End Class
