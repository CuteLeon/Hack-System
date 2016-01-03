<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LeonTextBox
    Inherits System.Windows.Forms.UserControl

    'UserControl 重写释放以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ScrollBar = New System.Windows.Forms.Label()
        Me.TextBox = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ScrollBar
        '
        Me.ScrollBar.BackColor = System.Drawing.Color.Transparent
        Me.ScrollBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ScrollBar.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ScrollBar.ForeColor = System.Drawing.Color.White
        Me.ScrollBar.Location = New System.Drawing.Point(123, 65)
        Me.ScrollBar.Name = "ScrollBar"
        Me.ScrollBar.Size = New System.Drawing.Size(12, 10)
        Me.ScrollBar.TabIndex = 4
        Me.ScrollBar.Visible = False
        '
        'TextBox
        '
        Me.TextBox.AutoSize = True
        Me.TextBox.BackColor = System.Drawing.Color.Transparent
        Me.TextBox.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBox.ForeColor = System.Drawing.Color.White
        Me.TextBox.Location = New System.Drawing.Point(0, 0)
        Me.TextBox.Name = "TextBox"
        Me.TextBox.Size = New System.Drawing.Size(0, 20)
        Me.TextBox.TabIndex = 5
        '
        'LeonTextBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.TextBox)
        Me.Controls.Add(Me.ScrollBar)
        Me.DoubleBuffered = True
        Me.Name = "LeonTextBox"
        Me.Size = New System.Drawing.Size(198, 232)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents ScrollBar As Label
    Public WithEvents TextBox As Label
End Class
