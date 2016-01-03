Public Class LeonTextBox

    Private Sub LeonTextBox_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        TextBox.Location = Point.Empty
        ScrollBar.Location = New Point(Me.Width - ScrollBar.Width, 0)
        ScrollBar.Height = Me.Height
    End Sub

    Private Sub LabelScrollBar_MouseDown(sender As Object, e As MouseEventArgs) Handles ScrollBar.MouseDown
        TextBox.Tag = MousePosition.Y : ScrollBar.Tag = ScrollBar.Top
        AddHandler ScrollBar.MouseMove, AddressOf LabelScrollBar_MouseMove
    End Sub

    Private Sub LabelScrollBar_MouseMove(sender As Object, e As MouseEventArgs)
        If TextBox.Height <= Me.Height Then Exit Sub
        Dim TopPosition As Integer = MousePosition.Y - TextBox.Tag + ScrollBar.Tag
        If TopPosition < 0 Or TopPosition > Me.Height - ScrollBar.Height Then Exit Sub
        ScrollBar.Top = TopPosition
        TextBox.Top = -(TextBox.Height - Me.Height) * (ScrollBar.Top / (Me.Height - ScrollBar.Height))
    End Sub

    Private Sub LabelScrollBar_MouseUp(sender As Object, e As MouseEventArgs) Handles ScrollBar.MouseUp
        RemoveHandler ScrollBar.MouseMove, AddressOf LabelScrollBar_MouseMove
    End Sub

    Private Sub LabelTextBox_Resize(sender As Object, e As EventArgs) Handles TextBox.Resize
        If TextBox.Height > Me.Height Then
            If Not ScrollBar.Visible Then ScrollBar.Show()
            ScrollBar.Height = Me.Height * (Me.Height / TextBox.Height)
            TextBox.Top = Me.Height - TextBox.Height
            ScrollBar.Top = Me.Height - ScrollBar.Height
        Else
            If ScrollBar.Visible Then
                TextBox.Location = Point.Empty
                ScrollBar.Hide()
            End If
        End If
    End Sub

    Private Sub LabelTextBox_MouseDown(sender As Object, e As MouseEventArgs) Handles TextBox.MouseDown, Me.MouseDown
        ScrollBar.Tag = MousePosition.Y : TextBox.Tag = TextBox.Top
        AddHandler TextBox.MouseMove, AddressOf LabelTextBox_MouseMove
        AddHandler Me.MouseMove, AddressOf LabelTextBox_MouseMove
    End Sub

    Private Sub LabelTextBox_MouseMove(sender As Object, e As MouseEventArgs)
        If TextBox.Height <= Me.Height Then Exit Sub
        Dim TopPosition As Integer = MousePosition.Y - ScrollBar.Tag
        If TopPosition > -TextBox.Tag Or TextBox.Tag + TopPosition < Me.Height - TextBox.Height Then Exit Sub
        TextBox.Top = TextBox.Tag + TopPosition
        ScrollBar.Top = -TextBox.Top * (Me.Height - ScrollBar.Height) / (TextBox.Height - Me.Height)
    End Sub

    Private Sub LabelTextBox_MouseUp(sender As Object, e As MouseEventArgs) Handles TextBox.MouseUp, Me.MouseUp
        RemoveHandler TextBox.MouseMove, AddressOf LabelTextBox_MouseMove
        RemoveHandler Me.MouseMove, AddressOf LabelTextBox_MouseMove
    End Sub

    Private Sub LeonTextBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ScrollBar.BackColor = Color.FromArgb(50, Color.White)
        Me.BackColor = Color.FromArgb(50, Color.White)
    End Sub

    Private Sub ScrollBar_Click(sender As Object, e As EventArgs) Handles ScrollBar.Click
        RemoveHandler ScrollBar.MouseMove, AddressOf LabelScrollBar_MouseMove
    End Sub

    Private Sub TextBox_Click(sender As Object, e As EventArgs) Handles TextBox.Click, Me.Click
        RemoveHandler TextBox.MouseMove, AddressOf LabelTextBox_MouseMove
        RemoveHandler Me.MouseMove, AddressOf LabelTextBox_MouseMove
    End Sub
End Class
