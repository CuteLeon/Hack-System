<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CommandConsole
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.CommandInputBox = New System.Windows.Forms.TextBox()
        Me.CommandTip = New System.Windows.Forms.Label()
        Me.CommandPast = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'CommandInputBox
        '
        Me.CommandInputBox.BackColor = System.Drawing.Color.White
        Me.CommandInputBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CommandInputBox.Font = New System.Drawing.Font("微软雅黑", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.CommandInputBox.ForeColor = System.Drawing.Color.Gray
        Me.CommandInputBox.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.CommandInputBox.Location = New System.Drawing.Point(12, 321)
        Me.CommandInputBox.Name = "CommandInputBox"
        Me.CommandInputBox.Size = New System.Drawing.Size(336, 27)
        Me.CommandInputBox.TabIndex = 2
        Me.CommandInputBox.Text = "请输入指令..."
        '
        'CommandTip
        '
        Me.CommandTip.Font = New System.Drawing.Font("微软雅黑", 10.5!, CType((System.Drawing.FontStyle.Italic Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.CommandTip.ForeColor = System.Drawing.Color.Cyan
        Me.CommandTip.Image = Global.HackSystem.My.Resources.SystemAssets.CommandTipBGI
        Me.CommandTip.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CommandTip.Location = New System.Drawing.Point(12, 265)
        Me.CommandTip.Name = "CommandTip"
        Me.CommandTip.Size = New System.Drawing.Size(336, 45)
        Me.CommandTip.TabIndex = 3
        Me.CommandTip.Text = "Hello,Welcome to Hack System !"
        Me.CommandTip.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'CommandPast
        '
        Me.CommandPast.BackColor = System.Drawing.Color.DimGray
        Me.CommandPast.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.CommandPast.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.CommandPast.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.CommandPast.ForeColor = System.Drawing.Color.DarkOrange
        Me.CommandPast.Location = New System.Drawing.Point(20, 12)
        Me.CommandPast.Name = "CommandPast"
        Me.CommandPast.ReadOnly = True
        Me.CommandPast.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.CommandPast.ShortcutsEnabled = False
        Me.CommandPast.Size = New System.Drawing.Size(325, 242)
        Me.CommandPast.TabIndex = 5
        Me.CommandPast.Text = "Console：（Double Click to Clear）"
        Me.CommandPast.WordWrap = False
        '
        'CommandConsole
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(360, 360)
        Me.Controls.Add(Me.CommandPast)
        Me.Controls.Add(Me.CommandTip)
        Me.Controls.Add(Me.CommandInputBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "CommandConsole"
        Me.Opacity = 0.8R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Tag = ""
        Me.Text = "CommandConsoleForm"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CommandInputBox As TextBox
    Friend WithEvents CommandTip As Label
    Friend WithEvents CommandPast As RichTextBox
End Class
