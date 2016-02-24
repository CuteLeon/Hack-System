Imports System.ComponentModel

Public Class LockUI
    'The UpperBound of wallpapers.
    Public Const WallpaperUpperBound As Int16 = 11
    Dim LoginAreaRect As RectangleF

    Private Sub LockUI_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Full screen
        Me.Location = New Point(0, 0)
        Me.Size = My.Computer.Screen.Bounds.Size
        With LoginAreaRect
            .Width = My.Resources.SystemAssets.LoginArea.Width
            .Height = My.Resources.SystemAssets.LoginArea.Height
            .X = (My.Computer.Screen.Bounds.Width - .Width) / 2
            .Y = (My.Computer.Screen.Bounds.Height - .Height) / 2
            LoginAreaControl.Location = New Point(.X, .Y)
        End With
        'Set Parent
        LoginAreaControl.Parent = LockWallpaperControl
        LoginButtonControl.Parent = LoginAreaControl
        PasswordControl.Parent = LoginAreaControl
        'Dont select password.
        PasswordControl.SelectionStart = PasswordControl.TextLength
        PasswordControl.SelectionLength = 0

        Me.Cursor = StartingUpUI.SystemCursor
    End Sub

    Private Sub LockWallpaperControl_Click(sender As Object, e As EventArgs) Handles LockWallpaperControl.Click
        'Click to change wallpaper.
        Static Index As Integer = 10
        LockWallpaperControl.Image = My.Resources.SystemAssets.ResourceManager.GetObject("SystemWallpaper_" & Index.ToString("00"))
        If Index = WallpaperUpperBound Then Index = 0 Else Index += 1
    End Sub

#Region "Button"

    Private Sub LoginButtonControl_MouseEnter(sender As Object, e As EventArgs) Handles LoginButtonControl.MouseEnter
        LoginButtonControl.Image = My.Resources.SystemAssets.LoginButton_2
    End Sub
    Private Sub LoginButtonControl_MouseDown(sender As Object, e As MouseEventArgs) Handles LoginButtonControl.MouseDown
        LoginButtonControl.Image = My.Resources.SystemAssets.LoginButton_3
    End Sub
    Private Sub LoginButtonControl_MouseUp(sender As Object, e As MouseEventArgs) Handles LoginButtonControl.MouseUp
        LoginButtonControl.Image = My.Resources.SystemAssets.LoginButton_2
    End Sub
    Private Sub LoginButtonControl_MouseLeave(sender As Object, e As EventArgs) Handles LoginButtonControl.MouseLeave
        LoginButtonControl.Image = My.Resources.SystemAssets.LoginButton_1
    End Sub
#End Region

    'Exchange windows.
    Private Sub LoginButtonControl_Click(sender As Object, e As EventArgs) Handles LoginButtonControl.Click
        ExchangeUI()
    End Sub

    Private Sub PasswordControl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PasswordControl.KeyPress
        If Asc(e.KeyChar) = Keys.Enter Then ExchangeUI()
    End Sub

    Private Sub ExchangeUI()
        SystemWorkStation.Show()
        SystemWorkStation.Focus()
        Me.Hide()
    End Sub

    Private Sub LockUI_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        End
    End Sub

    Private Sub LockUI_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Play audio,loop.
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("LockUIBGM"), AudioPlayMode.BackgroundLoop)
    End Sub
End Class