Imports System.ComponentModel

Public Class ShutdownWindow
    Private Sub ShutdownWindows_Load(sender As Object, e As EventArgs) Handles Me.Load
        ShutdownAreaControl.Parent = ShutdownWallpaperControl
        CancelButtonControl.Parent = ShutdownAreaControl
        ShutdownButtonControl.Parent = ShutdownAreaControl
        Me.Cursor = StartingUpUI.SystemCursor
    End Sub

    Private Sub ShutdownButtonControl_Click(sender As Object, e As EventArgs) Handles ShutdownButtonControl.Click
        Shutdown()
    End Sub

    Private Sub Shutdown()
        SystemWorkStation.ApplicationClosing = True
        Me.Hide()
        ShutdowningUI.Show()
        ShutdowningUI.ShowShutdownForm()
        ShutdowningUI.Focus()
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
        SystemWorkStation.Focus()
    End Sub

    Private Sub ShutdownWindows_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If Not SystemWorkStation.ApplicationClosing Then
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