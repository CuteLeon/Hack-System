<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LoginAndLockUI
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
        Me.LoginAreaControl = New System.Windows.Forms.Panel()
        Me.LoginButtonControl = New System.Windows.Forms.PictureBox()
        Me.PasswordControl = New System.Windows.Forms.TextBox()
        Me.LoginAreaControl.SuspendLayout()
        CType(Me.LoginButtonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LoginAreaControl
        '
        Me.LoginAreaControl.BackColor = System.Drawing.Color.Transparent
        Me.LoginAreaControl.BackgroundImage = Global.HackSystem.My.Resources.SystemAssets.LoginArea
        Me.LoginAreaControl.Controls.Add(Me.LoginButtonControl)
        Me.LoginAreaControl.Controls.Add(Me.PasswordControl)
        Me.LoginAreaControl.Location = New System.Drawing.Point(74, 118)
        Me.LoginAreaControl.Name = "LoginAreaControl"
        Me.LoginAreaControl.Size = New System.Drawing.Size(643, 185)
        Me.LoginAreaControl.TabIndex = 4
        '
        'LoginButtonControl
        '
        Me.LoginButtonControl.BackColor = System.Drawing.Color.Transparent
        Me.LoginButtonControl.Image = Global.HackSystem.My.Resources.SystemAssets.LoginButton_1
        Me.LoginButtonControl.Location = New System.Drawing.Point(507, 49)
        Me.LoginButtonControl.Name = "LoginButtonControl"
        Me.LoginButtonControl.Size = New System.Drawing.Size(94, 95)
        Me.LoginButtonControl.TabIndex = 5
        Me.LoginButtonControl.TabStop = False
        '
        'PasswordControl
        '
        Me.PasswordControl.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.PasswordControl.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.PasswordControl.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.PasswordControl.Font = New System.Drawing.Font("微软雅黑", 13.5!, System.Drawing.FontStyle.Bold)
        Me.PasswordControl.ForeColor = System.Drawing.Color.Black
        Me.PasswordControl.Location = New System.Drawing.Point(268, 114)
        Me.PasswordControl.MaxLength = 11
        Me.PasswordControl.Name = "PasswordControl"
        Me.PasswordControl.PasswordChar = Global.Microsoft.VisualBasic.ChrW(1422)
        Me.PasswordControl.Size = New System.Drawing.Size(207, 24)
        Me.PasswordControl.TabIndex = 4
        Me.PasswordControl.Text = "00000000"
        '
        'LoginAndLockUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BackgroundImage = Global.HackSystem.My.Resources.SystemAssets.SystemWallpaper_09
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(812, 440)
        Me.Controls.Add(Me.LoginAreaControl)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "LoginAndLockUI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Hack System"
        Me.TopMost = True
        Me.LoginAreaControl.ResumeLayout(False)
        Me.LoginAreaControl.PerformLayout()
        CType(Me.LoginButtonControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LoginAreaControl As Panel
    Friend WithEvents LoginButtonControl As PictureBox
    Friend WithEvents PasswordControl As TextBox
End Class
