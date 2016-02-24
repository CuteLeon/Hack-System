Public Class AboutMeForm

    Private Sub AboutMeForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Set control's parent.
        AboutMeControl.Parent = AboutMeWallpaperControl
        OKButtonControl.Parent = AboutMeControl
        OKButtonControl.Image = My.Resources.SystemAssets.OKButton.Clone(New Rectangle(0, 0, 140, 49), Imaging.PixelFormat.Format32bppArgb)
        'Set mouse curor.
        Me.Cursor = StartingUpUI.SystemCursor
    End Sub

    Private Sub AboutMeControl_MouseDown(sender As Object, e As MouseEventArgs) Handles AboutMeControl.MouseDown
        'Allow mouse to move window.
        WindowsTemplates.ReleaseCapture()
        WindowsTemplates.SendMessageA(Me.Handle, &HA1, 2, 0&)
    End Sub

    Private Sub OKButtonControl_Click(sender As Object, e As EventArgs) Handles OKButtonControl.Click
        'Click OKButton to and hide me and focus main desktop.
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        Me.Hide()
        SystemWorkStation.Focus()
    End Sub

#Region "The dynamic effect on the OKButton"
    Private Sub OKButtonControl_MouseDown(sender As Object, e As MouseEventArgs) Handles OKButtonControl.MouseDown
        OKButtonControl.Image = My.Resources.SystemAssets.OKButton.Clone(New Rectangle(280, 0, 140, 49), Imaging.PixelFormat.Format32bppArgb)
    End Sub

    Private Sub OKButtonControl_MouseEnter(sender As Object, e As EventArgs) Handles OKButtonControl.MouseEnter
        OKButtonControl.Image = My.Resources.SystemAssets.OKButton.Clone(New Rectangle(140, 0, 140, 49), Imaging.PixelFormat.Format32bppArgb)
    End Sub

    Private Sub OKButtonControl_MouseLeave(sender As Object, e As EventArgs) Handles OKButtonControl.MouseLeave
        OKButtonControl.Image = My.Resources.SystemAssets.OKButton.Clone(New Rectangle(0, 0, 140, 49), Imaging.PixelFormat.Format32bppArgb)
    End Sub

    Private Sub OKButtonControl_MouseUp(sender As Object, e As MouseEventArgs) Handles OKButtonControl.MouseUp
        OKButtonControl.Image = My.Resources.SystemAssets.OKButton.Clone(New Rectangle(140, 0, 140, 49), Imaging.PixelFormat.Format32bppArgb)
    End Sub
#End Region

    Private Sub AboutMe_KeyPress(sender As Object, e As KeyPressEventArgs) Handles AboutMeControl.KeyPress, Me.KeyPress
        'Press [Enter] or [Esc] to hide me and focus main desktop.
        Dim KeyAscii As Integer = Asc(e.KeyChar)
        If KeyAscii = 27 Or KeyAscii = 13 Then
            My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
            Me.Hide()
            SystemWorkStation.Focus()
        End If
    End Sub
End Class