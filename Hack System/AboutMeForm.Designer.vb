<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AboutMeForm
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
        Me.AboutMeWallpaperControl = New System.Windows.Forms.PictureBox()
        Me.AboutMeControl = New System.Windows.Forms.PictureBox()
        Me.OKButtonControl = New System.Windows.Forms.Label()
        Me.WebLink = New System.Windows.Forms.LinkLabel()
        Me.CheckUpdateLabel = New System.Windows.Forms.Label()
        CType(Me.AboutMeWallpaperControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AboutMeControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AboutMeWallpaperControl
        '
        Me.AboutMeWallpaperControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AboutMeWallpaperControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AboutMeWallpaperControl.Image = Global.HackSystem.My.Resources.SystemAssets.SystemMsgBGI
        Me.AboutMeWallpaperControl.Location = New System.Drawing.Point(0, 0)
        Me.AboutMeWallpaperControl.Name = "AboutMeWallpaperControl"
        Me.AboutMeWallpaperControl.Size = New System.Drawing.Size(500, 300)
        Me.AboutMeWallpaperControl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.AboutMeWallpaperControl.TabIndex = 10
        Me.AboutMeWallpaperControl.TabStop = False
        '
        'AboutMeControl
        '
        Me.AboutMeControl.BackColor = System.Drawing.Color.Transparent
        Me.AboutMeControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AboutMeControl.Image = Global.HackSystem.My.Resources.SystemAssets.AboutMe
        Me.AboutMeControl.Location = New System.Drawing.Point(0, 0)
        Me.AboutMeControl.Name = "AboutMeControl"
        Me.AboutMeControl.Size = New System.Drawing.Size(500, 300)
        Me.AboutMeControl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.AboutMeControl.TabIndex = 11
        Me.AboutMeControl.TabStop = False
        '
        'OKButtonControl
        '
        Me.OKButtonControl.BackColor = System.Drawing.Color.Transparent
        Me.OKButtonControl.Font = New System.Drawing.Font("新宋体", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.OKButtonControl.Image = Global.HackSystem.My.Resources.SystemAssets.OKButton_0
        Me.OKButtonControl.Location = New System.Drawing.Point(180, 235)
        Me.OKButtonControl.Name = "OKButtonControl"
        Me.OKButtonControl.Size = New System.Drawing.Size(140, 49)
        Me.OKButtonControl.TabIndex = 12
        Me.OKButtonControl.Tag = "OKButton"
        Me.OKButtonControl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'WebLink
        '
        Me.WebLink.AutoSize = True
        Me.WebLink.BackColor = System.Drawing.Color.Transparent
        Me.WebLink.Cursor = System.Windows.Forms.Cursors.Hand
        Me.WebLink.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.WebLink.LinkArea = New System.Windows.Forms.LinkArea(3, 33)
        Me.WebLink.LinkColor = System.Drawing.Color.Red
        Me.WebLink.Location = New System.Drawing.Point(113, 150)
        Me.WebLink.Name = "WebLink"
        Me.WebLink.Size = New System.Drawing.Size(268, 24)
        Me.WebLink.TabIndex = 13
        Me.WebLink.TabStop = True
        Me.WebLink.Text = "访问：http://www.HackSystem.icoc.in/"
        Me.WebLink.UseCompatibleTextRendering = True
        '
        'CheckUpdateLabel
        '
        Me.CheckUpdateLabel.BackColor = System.Drawing.Color.Transparent
        Me.CheckUpdateLabel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.CheckUpdateLabel.ForeColor = System.Drawing.Color.White
        Me.CheckUpdateLabel.Image = Global.HackSystem.My.Resources.SystemAssets.CheckUpdate_0
        Me.CheckUpdateLabel.Location = New System.Drawing.Point(197, 191)
        Me.CheckUpdateLabel.Name = "CheckUpdateLabel"
        Me.CheckUpdateLabel.Size = New System.Drawing.Size(106, 40)
        Me.CheckUpdateLabel.TabIndex = 14
        Me.CheckUpdateLabel.Tag = "CheckUpdate"
        Me.CheckUpdateLabel.Text = "检查更新"
        Me.CheckUpdateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'AboutMeForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(500, 300)
        Me.Controls.Add(Me.CheckUpdateLabel)
        Me.Controls.Add(Me.WebLink)
        Me.Controls.Add(Me.OKButtonControl)
        Me.Controls.Add(Me.AboutMeControl)
        Me.Controls.Add(Me.AboutMeWallpaperControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "AboutMeForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        CType(Me.AboutMeWallpaperControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AboutMeControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents AboutMeWallpaperControl As PictureBox
    Friend WithEvents AboutMeControl As PictureBox
    Friend WithEvents OKButtonControl As Label
    Friend WithEvents WebLink As LinkLabel
    Friend WithEvents CheckUpdateLabel As Label
End Class
