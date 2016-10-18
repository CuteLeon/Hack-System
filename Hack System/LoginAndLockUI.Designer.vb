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
        Me.LoginAreaControl = New System.Windows.Forms.PictureBox()
        Me.PasswordTextBox = New System.Windows.Forms.TextBox()
        Me.LoginButtonControl = New System.Windows.Forms.PictureBox()
        Me.HeadPictureBox = New System.Windows.Forms.PictureBox()
        Me.UserNameControl = New System.Windows.Forms.Label()
        Me.PasswordLabel = New System.Windows.Forms.Label()
        CType(Me.LoginAreaControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LoginButtonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HeadPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        'PasswordTextBox
        '
        Me.PasswordTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.PasswordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.PasswordTextBox.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.PasswordTextBox.Font = New System.Drawing.Font("微软雅黑", 13.5!, System.Drawing.FontStyle.Bold)
        Me.PasswordTextBox.ForeColor = System.Drawing.Color.Black
        Me.PasswordTextBox.Location = New System.Drawing.Point(376, 260)
        Me.PasswordTextBox.MaxLength = 10
        Me.PasswordTextBox.Name = "PasswordTextBox"
        Me.PasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(1422)
        Me.PasswordTextBox.Size = New System.Drawing.Size(0, 24)
        Me.PasswordTextBox.TabIndex = 4
        Me.PasswordTextBox.Text = "Leon.ID"
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
        'HeadPictureBox
        '
        Me.HeadPictureBox.BackColor = System.Drawing.Color.Transparent
        Me.HeadPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.HeadPictureBox.Image = Global.HackSystem.My.Resources.SystemAssets.HeadMask
        Me.HeadPictureBox.Location = New System.Drawing.Point(92, 144)
        Me.HeadPictureBox.Name = "HeadPictureBox"
        Me.HeadPictureBox.Size = New System.Drawing.Size(191, 191)
        Me.HeadPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.HeadPictureBox.TabIndex = 7
        Me.HeadPictureBox.TabStop = False
        '
        'UserNameControl
        '
        Me.UserNameControl.BackColor = System.Drawing.Color.Transparent
        Me.UserNameControl.Font = New System.Drawing.Font("微软雅黑", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.UserNameControl.Location = New System.Drawing.Point(285, 177)
        Me.UserNameControl.Name = "UserNameControl"
        Me.UserNameControl.Size = New System.Drawing.Size(300, 64)
        Me.UserNameControl.TabIndex = 8
        Me.UserNameControl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PasswordLabel
        '
        Me.PasswordLabel.BackColor = System.Drawing.Color.Transparent
        Me.PasswordLabel.Font = New System.Drawing.Font("微软雅黑", 13.5!, System.Drawing.FontStyle.Bold)
        Me.PasswordLabel.ForeColor = System.Drawing.Color.White
        Me.PasswordLabel.Image = Global.HackSystem.My.Resources.SystemAssets.PasswordInputBox_Normal
        Me.PasswordLabel.Location = New System.Drawing.Point(367, 253)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(220, 40)
        Me.PasswordLabel.TabIndex = 9
        Me.PasswordLabel.Text = "֎֎֎֎֎֎֎"
        Me.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LoginAndLockUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BackgroundImage = Global.HackSystem.My.Resources.SystemAssets.SystemWallpaper_14
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(812, 440)
        Me.Controls.Add(Me.PasswordLabel)
        Me.Controls.Add(Me.UserNameControl)
        Me.Controls.Add(Me.HeadPictureBox)
        Me.Controls.Add(Me.LoginButtonControl)
        Me.Controls.Add(Me.PasswordTextBox)
        Me.Controls.Add(Me.LoginAreaControl)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "LoginAndLockUI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HackSystem"
        Me.TopMost = True
        CType(Me.LoginAreaControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LoginButtonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HeadPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LoginAreaControl As PictureBox
    Friend WithEvents LoginButtonControl As PictureBox
    Friend WithEvents PasswordTextBox As TextBox
    Friend WithEvents HeadPictureBox As PictureBox
    Friend WithEvents UserNameControl As Label
    Friend WithEvents PasswordLabel As Label
End Class
