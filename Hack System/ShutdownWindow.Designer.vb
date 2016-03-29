<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ShutdownWindow
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
        Me.ShutdownAreaControl = New System.Windows.Forms.PictureBox()
        Me.ShutdownButtonControl = New System.Windows.Forms.PictureBox()
        Me.CancelButtonControl = New System.Windows.Forms.PictureBox()
        Me.ShutdownWallpaperControl = New System.Windows.Forms.PictureBox()
        CType(Me.ShutdownAreaControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ShutdownButtonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CancelButtonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ShutdownWallpaperControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ShutdownAreaControl
        '
        Me.ShutdownAreaControl.BackColor = System.Drawing.Color.Transparent
        Me.ShutdownAreaControl.Image = Global.HackSystem.My.Resources.SystemAssets.ShutdownArea
        Me.ShutdownAreaControl.Location = New System.Drawing.Point(0, 0)
        Me.ShutdownAreaControl.Name = "ShutdownAreaControl"
        Me.ShutdownAreaControl.Size = New System.Drawing.Size(643, 185)
        Me.ShutdownAreaControl.TabIndex = 6
        Me.ShutdownAreaControl.TabStop = False
        '
        'ShutdownButtonControl
        '
        Me.ShutdownButtonControl.BackColor = System.Drawing.Color.Transparent
        Me.ShutdownButtonControl.Image = Global.HackSystem.My.Resources.SystemAssets.ShutdownButton_1
        Me.ShutdownButtonControl.Location = New System.Drawing.Point(507, 47)
        Me.ShutdownButtonControl.Name = "ShutdownButtonControl"
        Me.ShutdownButtonControl.Size = New System.Drawing.Size(94, 94)
        Me.ShutdownButtonControl.TabIndex = 7
        Me.ShutdownButtonControl.TabStop = False
        '
        'CancelButtonControl
        '
        Me.CancelButtonControl.BackColor = System.Drawing.Color.Transparent
        Me.CancelButtonControl.Image = Global.HackSystem.My.Resources.SystemAssets.CancelButton_1
        Me.CancelButtonControl.Location = New System.Drawing.Point(42, 47)
        Me.CancelButtonControl.Name = "CancelButtonControl"
        Me.CancelButtonControl.Size = New System.Drawing.Size(94, 94)
        Me.CancelButtonControl.TabIndex = 8
        Me.CancelButtonControl.TabStop = False
        '
        'ShutdownWallpaperControl
        '
        Me.ShutdownWallpaperControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ShutdownWallpaperControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ShutdownWallpaperControl.Image = Global.HackSystem.My.Resources.SystemAssets.SystemWallpaper_10
        Me.ShutdownWallpaperControl.Location = New System.Drawing.Point(0, 0)
        Me.ShutdownWallpaperControl.Name = "ShutdownWallpaperControl"
        Me.ShutdownWallpaperControl.Size = New System.Drawing.Size(643, 185)
        Me.ShutdownWallpaperControl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.ShutdownWallpaperControl.TabIndex = 9
        Me.ShutdownWallpaperControl.TabStop = False
        '
        'ShutdownWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(643, 185)
        Me.Controls.Add(Me.CancelButtonControl)
        Me.Controls.Add(Me.ShutdownButtonControl)
        Me.Controls.Add(Me.ShutdownAreaControl)
        Me.Controls.Add(Me.ShutdownWallpaperControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ShutdownWindow"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ShutdownWindows"
        Me.TopMost = True
        CType(Me.ShutdownAreaControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ShutdownButtonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CancelButtonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ShutdownWallpaperControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ShutdownAreaControl As PictureBox
    Friend WithEvents ShutdownButtonControl As PictureBox
    Friend WithEvents CancelButtonControl As PictureBox
    Friend WithEvents ShutdownWallpaperControl As PictureBox
End Class
