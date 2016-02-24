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
        CType(Me.AboutMeWallpaperControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AboutMeControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AboutMeWallpaperControl
        '
        Me.AboutMeWallpaperControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AboutMeWallpaperControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AboutMeWallpaperControl.Image = Global.HackSystem.My.Resources.SystemAssets.SystemWallpaper_10
        Me.AboutMeWallpaperControl.Location = New System.Drawing.Point(0, 0)
        Me.AboutMeWallpaperControl.Name = "AboutMeWallpaperControl"
        Me.AboutMeWallpaperControl.Size = New System.Drawing.Size(520, 250)
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
        Me.AboutMeControl.Size = New System.Drawing.Size(520, 250)
        Me.AboutMeControl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.AboutMeControl.TabIndex = 11
        Me.AboutMeControl.TabStop = False
        '
        'OKButtonControl
        '
        Me.OKButtonControl.BackColor = System.Drawing.Color.Transparent
        Me.OKButtonControl.Font = New System.Drawing.Font("新宋体", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.OKButtonControl.Image = Global.HackSystem.My.Resources.SystemAssets.OKButton
        Me.OKButtonControl.Location = New System.Drawing.Point(177, 177)
        Me.OKButtonControl.Name = "OKButtonControl"
        Me.OKButtonControl.Size = New System.Drawing.Size(140, 49)
        Me.OKButtonControl.TabIndex = 12
        Me.OKButtonControl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'AboutMeForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(520, 250)
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

    End Sub

    Friend WithEvents AboutMeWallpaperControl As PictureBox
    Friend WithEvents AboutMeControl As PictureBox
    Friend WithEvents OKButtonControl As Label
End Class
