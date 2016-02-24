<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LockUI
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
        Me.PasswordControl = New System.Windows.Forms.TextBox()
        Me.LoginButtonControl = New System.Windows.Forms.PictureBox()
        Me.LoginAreaControl = New System.Windows.Forms.PictureBox()
        Me.LockWallpaperControl = New System.Windows.Forms.PictureBox()
        CType(Me.LoginButtonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LoginAreaControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LockWallpaperControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PasswordControl
        '
        Me.PasswordControl.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.PasswordControl.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.PasswordControl.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.PasswordControl.Font = New System.Drawing.Font("微软雅黑", 13.5!, System.Drawing.FontStyle.Bold)
        Me.PasswordControl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.PasswordControl.Location = New System.Drawing.Point(268, 113)
        Me.PasswordControl.MaxLength = 16
        Me.PasswordControl.Name = "PasswordControl"
        Me.PasswordControl.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.PasswordControl.Size = New System.Drawing.Size(207, 24)
        Me.PasswordControl.TabIndex = 0
        Me.PasswordControl.Text = "00000000"
        '
        'LoginButtonControl
        '
        Me.LoginButtonControl.BackColor = System.Drawing.Color.Transparent
        Me.LoginButtonControl.Image = Global.HackSystem.My.Resources.SystemAssets.LoginButton_1
        Me.LoginButtonControl.Location = New System.Drawing.Point(507, 48)
        Me.LoginButtonControl.Name = "LoginButtonControl"
        Me.LoginButtonControl.Size = New System.Drawing.Size(94, 95)
        Me.LoginButtonControl.TabIndex = 3
        Me.LoginButtonControl.TabStop = False
        '
        'LoginAreaControl
        '
        Me.LoginAreaControl.BackColor = System.Drawing.Color.Transparent
        Me.LoginAreaControl.Image = Global.HackSystem.My.Resources.SystemAssets.LoginArea
        Me.LoginAreaControl.Location = New System.Drawing.Point(0, 0)
        Me.LoginAreaControl.Name = "LoginAreaControl"
        Me.LoginAreaControl.Size = New System.Drawing.Size(643, 185)
        Me.LoginAreaControl.TabIndex = 1
        Me.LoginAreaControl.TabStop = False
        '
        'LockWallpaperControl
        '
        Me.LockWallpaperControl.BackColor = System.Drawing.SystemColors.Control
        Me.LockWallpaperControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LockWallpaperControl.Image = Global.HackSystem.My.Resources.SystemAssets.SystemWallpaper_10
        Me.LockWallpaperControl.Location = New System.Drawing.Point(0, 0)
        Me.LockWallpaperControl.Name = "LockWallpaperControl"
        Me.LockWallpaperControl.Size = New System.Drawing.Size(1366, 768)
        Me.LockWallpaperControl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.LockWallpaperControl.TabIndex = 0
        Me.LockWallpaperControl.TabStop = False
        '
        'LockUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1366, 768)
        Me.Controls.Add(Me.LoginButtonControl)
        Me.Controls.Add(Me.PasswordControl)
        Me.Controls.Add(Me.LoginAreaControl)
        Me.Controls.Add(Me.LockWallpaperControl)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "LockUI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Hack System"
        Me.TopMost = True
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.LoginButtonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LoginAreaControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LockWallpaperControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LockWallpaperControl As PictureBox
    Friend WithEvents LoginAreaControl As PictureBox
    Friend WithEvents PasswordControl As TextBox
    Friend WithEvents LoginButtonControl As PictureBox
End Class
