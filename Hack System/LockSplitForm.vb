Public Class LockSplitForm
    Private Sub LockSplitForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Cursor = StartingUpUI.SystemCursor
        Me.Icon = Me.Icon
        Me.Size = New Size(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height / 2)
        Me.Location = New Point(0, My.Computer.Screen.Bounds.Height / 2)
    End Sub
End Class