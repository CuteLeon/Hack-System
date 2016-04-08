Imports System.ComponentModel

Public Class ShutdownTips
    '圆角窗体
    Private Declare Function CreateRoundRectRgn Lib "gdi32" Alias "CreateRoundRectRgn" (ByVal X1 As Int32, ByVal Y1 As Int32, ByVal X2 As Int32, ByVal Y2 As Int32, ByVal X3 As Int32, ByVal Y3 As Int32) As Int32
    Private Declare Function SetWindowRgn Lib "user32" Alias "SetWindowRgn" (ByVal hWnd As Int32, ByVal hRgn As Int32, ByVal bRedraw As Boolean) As Int32

    Private Sub ShutdownWindows_Load(sender As Object, e As EventArgs) Handles Me.Load
        ShutdownAreaControl.Parent = ShutdownWallpaperControl
        CancelButtonControl.Parent = ShutdownAreaControl
        ShutdownButtonControl.Parent = ShutdownAreaControl
        Me.Cursor = StartingUpUI.SystemCursor
        '圆角窗体
        Dim RoundRectangle As Integer = CreateRoundRectRgn(2, 2, Me.Width + 1, Me.Height + 1, Me.Height, Me.Height)
        SetWindowRgn(Me.Handle, RoundRectangle, True)
    End Sub

    Private Sub ShutdownButtonControl_Click(sender As Object, e As EventArgs) Handles ShutdownButtonControl.Click
        Shutdown()
    End Sub

    Private Sub Shutdown()
        SystemWorkStation.SystemClosing = True
        Me.Hide()
        ShutdowningUI.Show()
        ShutdowningUI.ShowShutdownForm()
        SystemWorkStation.SetForegroundWindow(ShutdowningUI.Handle)
    End Sub

#Region "Shutdown Button"

    Private Sub ShutdownButtonControl_MouseDown(sender As Object, e As MouseEventArgs) Handles ShutdownButtonControl.MouseDown
        ShutdownButtonControl.Image = My.Resources.SystemAssets.ShutdownButton_3
    End Sub

    Private Sub ShutdownButtonControl_MouseEnter(sender As Object, e As EventArgs) Handles ShutdownButtonControl.MouseEnter
        ShutdownButtonControl.Image = My.Resources.SystemAssets.ShutdownButton_2
    End Sub

    Private Sub ShutdownButtonControl_MouseLeave(sender As Object, e As EventArgs) Handles ShutdownButtonControl.MouseLeave
        ShutdownButtonControl.Image = My.Resources.SystemAssets.ShutdownButton_1
    End Sub

    Private Sub ShutdownButtonControl_MouseUp(sender As Object, e As MouseEventArgs) Handles ShutdownButtonControl.MouseUp
        ShutdownButtonControl.Image = My.Resources.SystemAssets.ShutdownButton_2
    End Sub
#End Region

    Private Sub CancelButtonControl_Click(sender As Object, e As EventArgs) Handles CancelButtonControl.Click
        CancelShutdown()
    End Sub

    Private Sub CancelShutdown()
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        Me.Hide()
        SystemWorkStation.SetForegroundWindow(SystemWorkStation.Handle)
    End Sub

    Private Sub ShutdownWindows_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If Not SystemWorkStation.SystemClosing Then
            e.Cancel = True
            CancelShutdown()
        End If
    End Sub

#Region "Cancel Button"

    Private Sub CancelButtonControl_MouseDown(sender As Object, e As MouseEventArgs) Handles CancelButtonControl.MouseDown
        CancelButtonControl.Image = My.Resources.SystemAssets.CancelButton_3
    End Sub

    Private Sub CancelButtonControl_MouseEnter(sender As Object, e As EventArgs) Handles CancelButtonControl.MouseEnter
        CancelButtonControl.Image = My.Resources.SystemAssets.CancelButton_2
    End Sub

    Private Sub CancelButtonControl_MouseUp(sender As Object, e As MouseEventArgs) Handles CancelButtonControl.MouseUp
        CancelButtonControl.Image = My.Resources.SystemAssets.CancelButton_2
    End Sub

    Private Sub CancelButtonControl_MouseLeave(sender As Object, e As EventArgs) Handles CancelButtonControl.MouseLeave
        CancelButtonControl.Image = My.Resources.SystemAssets.CancelButton_1
    End Sub
#End Region

    Private Sub ShutdownWindows_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Dim KeyAsc As Integer = Asc(e.KeyChar)
        If KeyAsc = 27 Then
            '[Esc] to cancel.
            CancelShutdown()
        ElseIf KeyAsc = 13 Then
            '[Enter] to shutdown.
            Shutdown()
        End If
    End Sub

End Class