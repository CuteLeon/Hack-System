<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StartingUpUI
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If SystemCursor IsNot Nothing Then SystemCursor.Dispose()
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
        Me.components = New System.ComponentModel.Container()
        Me.StartingUpTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ExchangeUITimer = New System.Windows.Forms.Timer(Me.components)
        Me.StartingUpLable = New System.Windows.Forms.Label()
        Me.StartingUpControl = New System.Windows.Forms.PictureBox()
        Me.StartUpLogo = New System.Windows.Forms.Label()
        CType(Me.StartingUpControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StartingUpTimer
        '
        Me.StartingUpTimer.Enabled = True
        Me.StartingUpTimer.Interval = 50
        '
        'ExchangeUITimer
        '
        Me.ExchangeUITimer.Interval = 50
        '
        'StartingUpLable
        '
        Me.StartingUpLable.BackColor = System.Drawing.Color.Transparent
        Me.StartingUpLable.Font = New System.Drawing.Font("微软雅黑", 18.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.StartingUpLable.ForeColor = System.Drawing.Color.Silver
        Me.StartingUpLable.Location = New System.Drawing.Point(222, 348)
        Me.StartingUpLable.Name = "StartingUpLable"
        Me.StartingUpLable.Size = New System.Drawing.Size(400, 32)
        Me.StartingUpLable.TabIndex = 2
        Me.StartingUpLable.Text = "Hack System Loading... (0%)"
        Me.StartingUpLable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'StartingUpControl
        '
        Me.StartingUpControl.BackColor = System.Drawing.Color.Transparent
        Me.StartingUpControl.BackgroundImage = Global.HackSystem.My.Resources.SystemAssets.StartingUp
        Me.StartingUpControl.Location = New System.Drawing.Point(322, 148)
        Me.StartingUpControl.Name = "StartingUpControl"
        Me.StartingUpControl.Size = New System.Drawing.Size(200, 200)
        Me.StartingUpControl.TabIndex = 0
        Me.StartingUpControl.TabStop = False
        '
        'StartUpLogo
        '
        Me.StartUpLogo.BackColor = System.Drawing.Color.Transparent
        Me.StartUpLogo.Image = Global.HackSystem.My.Resources.SystemAssets.HackSystemLogo
        Me.StartUpLogo.Location = New System.Drawing.Point(179, 45)
        Me.StartUpLogo.Name = "StartUpLogo"
        Me.StartUpLogo.Size = New System.Drawing.Size(487, 100)
        Me.StartUpLogo.TabIndex = 3
        '
        'StartingUpUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BackgroundImage = Global.HackSystem.My.Resources.SystemAssets.StartingUIWallpaper
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(768, 390)
        Me.Controls.Add(Me.StartUpLogo)
        Me.Controls.Add(Me.StartingUpLable)
        Me.Controls.Add(Me.StartingUpControl)
        Me.DoubleBuffered = True
        Me.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "StartingUpUI"
        Me.Text = "Hack System Loading... (0%)"
        Me.TopMost = True
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.StartingUpControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents StartingUpControl As PictureBox
    Friend WithEvents StartingUpTimer As Timer
    Friend WithEvents ExchangeUITimer As Timer
    Friend WithEvents StartingUpLable As Label
    Friend WithEvents StartUpLogo As Label
End Class
