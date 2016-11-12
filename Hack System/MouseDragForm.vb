Public Class MouseDragForm

    'Private Structure MouseDragColor
    '    Shared Red As Integer = 20
    '    Shared Green As Integer = 115
    '    Shared Blue As Integer = 210
    'End Structure

    Private Sub MouseDragForm_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Me.CreateGraphics.DrawRectangle(New Pen(Color.FromArgb(255, 50, 255, 255), 2), New Rectangle(1, 1, Me.Width - 2, Me.Height - 2))
    End Sub

    Private Sub MouseDragForm_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Me.Invalidate()
    End Sub
End Class