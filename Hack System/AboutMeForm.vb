Public Class AboutMeForm

#Region "窗体和控件"

    Private Sub AboutMeForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        '设置控件的父容器
        AboutMeControl.Parent = AboutMeWallpaperControl
        OKButtonControl.Parent = AboutMeControl
        WebLink.Parent = AboutMeControl
        OKButtonControl.Image = My.Resources.SystemAssets.OKButton.Clone(New Rectangle(0, 0, 140, 49), UnityModule.DefaultPixelFormat)

        Me.Cursor = UnityModule.SystemCursor
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

#End Region

#Region "按钮动态效果"

    Private Sub OKButtonControl_MouseDown(sender As Object, e As MouseEventArgs) Handles OKButtonControl.MouseDown
        OKButtonControl.Image = My.Resources.SystemAssets.OKButton.Clone(New Rectangle(280, 0, 140, 49), UnityModule.DefaultPixelFormat)
    End Sub

    Private Sub OKButtonControl_MouseEnter(sender As Object, e As EventArgs) Handles OKButtonControl.MouseEnter
        OKButtonControl.Image = My.Resources.SystemAssets.OKButton.Clone(New Rectangle(140, 0, 140, 49), UnityModule.DefaultPixelFormat)
    End Sub

    Private Sub OKButtonControl_MouseLeave(sender As Object, e As EventArgs) Handles OKButtonControl.MouseLeave
        OKButtonControl.Image = My.Resources.SystemAssets.OKButton.Clone(New Rectangle(0, 0, 140, 49), UnityModule.DefaultPixelFormat)
    End Sub

    Private Sub OKButtonControl_MouseUp(sender As Object, e As MouseEventArgs) Handles OKButtonControl.MouseUp
        OKButtonControl.Image = My.Resources.SystemAssets.OKButton.Clone(New Rectangle(140, 0, 140, 49), UnityModule.DefaultPixelFormat)
    End Sub
#End Region

End Class