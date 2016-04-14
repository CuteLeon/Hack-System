Imports System.ComponentModel

Public Class StartingUpUI
    Public SystemCursor As Cursor = New Cursor(My.Resources.SystemAssets.MouseCursor.GetHicon)

#Region "窗体"

    Private Sub StartingUpUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Location = New Point(0, 0)
        Me.Size = My.Computer.Screen.Bounds.Size

        Me.Icon = My.Resources.SystemAssets.HackSystem
        LoginAndLockUI.Icon = My.Resources.SystemAssets.HackSystem
        SystemWorkStation.Icon = My.Resources.SystemAssets.HackSystem
        WindowsTemplates.Icon = My.Resources.SystemAssets.HackSystem

        Dim LocationX As Integer = (My.Computer.Screen.Bounds.Width - StartingUpControl.Width) / 2
        Dim LocationY As Integer = (My.Computer.Screen.Bounds.Height - StartingUpControl.Height) * 0.85
        StartingUpControl.Location = New Point(LocationX, LocationY)
        StartingUpControl.Image = My.Resources.SystemAssets.StartingUp.Clone(New Rectangle(0, 0, 200, 200), Imaging.PixelFormat.Format32bppArgb)
        StartUpLogo.Location = New Point((My.Computer.Screen.Bounds.Width - My.Resources.SystemAssets.HackSystemLogo.Width) \ 2, My.Computer.Screen.Bounds.Height \ 4)

        Me.Cursor = SystemCursor

        StartingUpLable.Left = (My.Computer.Screen.Bounds.Width - StartingUpLable.Width) / 2
        StartingUpLable.Top = LocationY + 210

        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("StartedUp"), AudioPlayMode.Background)
    End Sub

    Private Sub StartingUpUI_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Application.Exit()
    End Sub
#End Region

#Region "控件"

    'Timer
    Private Sub StartingUpTimer_Tick(sender As Object, e As EventArgs) Handles StartingUpTimer.Tick
        Static FrameIndex As Integer = 1
        StartingUpControl.Image = My.Resources.SystemAssets.StartingUp.Clone(New Rectangle(0, FrameIndex * 200, 200, 200), Imaging.PixelFormat.Format32bppArgb)
        FrameIndex += 1

        StartingUpLable.Text = "Hack System Loading... (" & 2 * FrameIndex & "%)"
        Me.Text = StartingUpLable.Text
        'Exchange UI after the 50th frame.
        If FrameIndex = 50 Then
            StartingUpTimer.Enabled = False
            ExchangeUITimer.Enabled = True
            LoginAndLockUI.Show()
            Me.Focus()
        End If
    End Sub

    Private Sub ExchangeUITimer_Tick(sender As Object, e As EventArgs) Handles ExchangeUITimer.Tick
        'Contniue to exchange UI.
        Static FrameIndex As Integer = 50
        StartingUpControl.Image = My.Resources.SystemAssets.StartingUp.Clone(New Rectangle(0, FrameIndex * 200, 200, 200), Imaging.PixelFormat.Format32bppArgb)
        FrameIndex += 1
        Me.Opacity -= 0.1
        'Finish.
        If FrameIndex = 60 Then
            ExchangeUITimer.Enabled = False
            Me.Hide()
            SystemWorkStation.SetForegroundWindow(LoginAndLockUI.Handle)
        End If
    End Sub
#End Region

End Class