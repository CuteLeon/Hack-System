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
        Me.StartUpLogo = New System.Windows.Forms.Label()
        Me.LoginAreaControl = New System.Windows.Forms.PictureBox()
        Me.PasswordControl = New System.Windows.Forms.TextBox()
        Me.LoginButtonControl = New System.Windows.Forms.PictureBox()
        CType(Me.LoginAreaControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LoginButtonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StartUpLogo
        '
        Me.StartUpLogo.BackColor = System.Drawing.Color.Transparent
        Me.StartUpLogo.Image = Global.HackSystem.My.Resources.SystemAssets.HackSystemLogo
        Me.StartUpLogo.Location = New System.Drawing.Point(169, 9)
        Me.StartUpLogo.Name = "StartUpLogo"
        Me.StartUpLogo.Size = New System.Drawing.Size(487, 100)
        Me.StartUpLogo.TabIndex = 6
        '
        'LoginAreaControl
        '
        Me.LoginAreaControl.BackColor = System.Drawing.Color.Transparent
        Me.LoginAreaControl.BackgroundImage = Global.HackSystem.My.Resources.SystemAssets.LoginArea
        Me.LoginAreaControl.Location = New System.Drawing.Point(95, 147)
        Me.LoginAreaControl.Name = "LoginAreaControl"
        Me.LoginAreaControl.Size = New System.Drawing.Size(643, 185)
        Me.LoginAreaControl.TabIndex = 6
        Me.LoginAreaControl.TabStop = False
        '
        'PasswordControl
        '
        Me.PasswordControl.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.PasswordControl.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.PasswordControl.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.PasswordControl.Font = New System.Drawing.Font("微软雅黑", 13.5!, System.Drawing.FontStyle.Bold)
        Me.PasswordControl.ForeColor = System.Drawing.Color.Black
        Me.PasswordControl.Location = New System.Drawing.Point(363, 260)
        Me.PasswordControl.MaxLength = 11
        Me.PasswordControl.Name = "PasswordControl"
        Me.PasswordControl.PasswordChar = Global.Microsoft.VisualBasic.ChrW(1422)
        Me.PasswordControl.Size = New System.Drawing.Size(207, 24)
        Me.PasswordControl.TabIndex = 4
        Me.PasswordControl.Text = "HackSystem"
        '
        'LoginButtonControl
        '
        Me.LoginButtonControl.BackColor = System.Drawing.Color.Transparent
        Me.LoginButtonControl.Image = Global.HackSystem.My.Resources.SystemAssets.LoginButton_1
        Me.LoginButtonControl.Location = New System.Drawing.Point(602, 195)
        Me.LoginButtonControl.Name = "LoginButtonControl"
        Me.LoginButtonControl.Size = New System.Drawing.Size(94, 95)
        Me.LoginButtonControl.TabIndex = 5
        Me.LoginButtonControl.TabStop = False
        '
        'LoginAndLockUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BackgroundImage = Global.HackSystem.My.Resources.SystemAssets.SystemWallpaper_09
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(812, 440)
        Me.Controls.Add(Me.LoginButtonControl)
        Me.Controls.Add(Me.PasswordControl)
        Me.Controls.Add(Me.LoginAreaControl)
        Me.Controls.Add(Me.StartUpLogo)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "LoginAndLockUI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HackSystem"
        Me.TopMost = True
        CType(Me.LoginAreaControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LoginButtonControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StartUpLogo As Label
    Friend WithEvents LoginAreaControl As PictureBox
    Friend WithEvents LoginButtonControl As PictureBox
    Friend WithEvents PasswordControl As TextBox
End Class
