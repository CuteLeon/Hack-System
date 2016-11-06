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
        Me.CommandButton = New System.Windows.Forms.Label()
        Me.CleanButton = New System.Windows.Forms.Label()
        Me.CommandInputBoxBGI = New System.Windows.Forms.Label()
        Me.ConsoleTitleLabel = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'CommandInputBox
        '
        Me.CommandInputBox.BackColor = System.Drawing.Color.White
        Me.CommandInputBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.CommandInputBox.Font = New System.Drawing.Font("微软雅黑", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.CommandInputBox.ForeColor = System.Drawing.Color.Gray
        Me.CommandInputBox.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.CommandInputBox.Location = New System.Drawing.Point(15, 331)
        Me.CommandInputBox.Name = "CommandInputBox"
        Me.CommandInputBox.Size = New System.Drawing.Size(300, 20)
        Me.CommandInputBox.TabIndex = 2
        Me.CommandInputBox.Text = "Please input command..."
        '
        'CommandTip
        '
        Me.CommandTip.Font = New System.Drawing.Font("微软雅黑", 10.5!, CType((System.Drawing.FontStyle.Italic Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.CommandTip.ForeColor = System.Drawing.Color.Cyan
        Me.CommandTip.Image = Global.HackSystem.My.Resources.SystemAssets.CommandTipBGI
        Me.CommandTip.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CommandTip.Location = New System.Drawing.Point(12, 268)
        Me.CommandTip.Name = "CommandTip"
        Me.CommandTip.Size = New System.Drawing.Size(336, 40)
        Me.CommandTip.TabIndex = 3
        Me.CommandTip.Text = "Hello,Welcome to Hack System !"
        Me.CommandTip.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'CommandPast
        '
        Me.CommandPast.BackColor = System.Drawing.Color.DimGray
        Me.CommandPast.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.CommandPast.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.CommandPast.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.CommandPast.ForeColor = System.Drawing.Color.DarkOrange
        Me.CommandPast.Location = New System.Drawing.Point(12, 35)
        Me.CommandPast.Name = "CommandPast"
        Me.CommandPast.ReadOnly = True
        Me.CommandPast.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.CommandPast.ShortcutsEnabled = False
        Me.CommandPast.Size = New System.Drawing.Size(336, 230)
        Me.CommandPast.TabIndex = 5
        Me.CommandPast.Text = ""
        Me.CommandPast.WordWrap = False
        '
        'CommandButton
        '
        Me.CommandButton.BackColor = System.Drawing.Color.White
        Me.CommandButton.Image = Global.HackSystem.My.Resources.SystemAssets.CommandButton_0
        Me.CommandButton.Location = New System.Drawing.Point(315, 330)
        Me.CommandButton.Name = "CommandButton"
        Me.CommandButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CommandButton.Size = New System.Drawing.Size(23, 20)
        Me.CommandButton.TabIndex = 6
        Me.CommandButton.Tag = "CommandButton_"
        '
        'CleanButton
        '
        Me.CleanButton.BackColor = System.Drawing.Color.Transparent
        Me.CleanButton.Image = Global.HackSystem.My.Resources.SystemAssets.CleanCommandPast_0
        Me.CleanButton.Location = New System.Drawing.Point(323, 7)
        Me.CleanButton.Name = "CleanButton"
        Me.CleanButton.Size = New System.Drawing.Size(25, 25)
        Me.CleanButton.TabIndex = 7
        Me.CleanButton.Tag = "CleanCommandPast_"
        '
        'CommandInputBoxBGI
        '
        Me.CommandInputBoxBGI.BackColor = System.Drawing.Color.Transparent
        Me.CommandInputBoxBGI.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.CommandInputBoxBGI.Image = Global.HackSystem.My.Resources.SystemAssets.CommandInputBox
        Me.CommandInputBoxBGI.Location = New System.Drawing.Point(0, 320)
        Me.CommandInputBoxBGI.Name = "CommandInputBoxBGI"
        Me.CommandInputBoxBGI.Size = New System.Drawing.Size(360, 40)
        Me.CommandInputBoxBGI.TabIndex = 8
        '
        'ConsoleTitleLabel
        '
        Me.ConsoleTitleLabel.AutoSize = True
        Me.ConsoleTitleLabel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ConsoleTitleLabel.ForeColor = System.Drawing.Color.Cyan
        Me.ConsoleTitleLabel.Location = New System.Drawing.Point(12, 13)
        Me.ConsoleTitleLabel.Name = "ConsoleTitleLabel"
        Me.ConsoleTitleLabel.Size = New System.Drawing.Size(78, 19)
        Me.ConsoleTitleLabel.TabIndex = 9
        Me.ConsoleTitleLabel.Text = "Console："
        '
        'CommandConsole
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(360, 360)
        Me.Controls.Add(Me.ConsoleTitleLabel)
        Me.Controls.Add(Me.CleanButton)
        Me.Controls.Add(Me.CommandButton)
        Me.Controls.Add(Me.CommandPast)
        Me.Controls.Add(Me.CommandTip)
        Me.Controls.Add(Me.CommandInputBox)
        Me.Controls.Add(Me.CommandInputBoxBGI)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Location = New System.Drawing.Point(-360, 0)
        Me.Name = "CommandConsole"
        Me.Opacity = 0.9R
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
    Friend WithEvents CommandButton As Label
    Friend WithEvents CleanButton As Label
    Friend WithEvents CommandInputBoxBGI As Label
    Friend WithEvents ConsoleTitleLabel As Label
End Class
