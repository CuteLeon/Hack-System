<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DownloaderForm
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
        Me.CloseButton = New System.Windows.Forms.Label()
        Me.DLProgressBar = New System.Windows.Forms.PictureBox()
        Me.DownloadLabel = New System.Windows.Forms.Label()
        Me.DLOKButton = New System.Windows.Forms.Label()
        Me.DLCancelButton = New System.Windows.Forms.Label()
        CType(Me.DLProgressBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CloseButton
        '
        Me.CloseButton.BackColor = System.Drawing.Color.Transparent
        Me.CloseButton.Image = Global.HackSystem.My.Resources.SystemAssets.DownloaderClose_0
        Me.CloseButton.Location = New System.Drawing.Point(257, 9)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(12, 13)
        Me.CloseButton.TabIndex = 0
        Me.CloseButton.Tag = "DownloaderClose"
        '
        'DLProgressBar
        '
        Me.DLProgressBar.BackColor = System.Drawing.Color.Transparent
        Me.DLProgressBar.BackgroundImage = Global.HackSystem.My.Resources.SystemAssets.DownloadProgressPanel
        Me.DLProgressBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.DLProgressBar.Location = New System.Drawing.Point(16, 90)
        Me.DLProgressBar.Name = "DLProgressBar"
        Me.DLProgressBar.Size = New System.Drawing.Size(250, 19)
        Me.DLProgressBar.TabIndex = 1
        Me.DLProgressBar.TabStop = False
        Me.DLProgressBar.Visible = False
        '
        'DownloadLabel
        '
        Me.DownloadLabel.BackColor = System.Drawing.Color.Transparent
        Me.DownloadLabel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.DownloadLabel.ForeColor = System.Drawing.Color.White
        Me.DownloadLabel.Location = New System.Drawing.Point(12, 38)
        Me.DownloadLabel.Name = "DownloadLabel"
        Me.DownloadLabel.Size = New System.Drawing.Size(258, 47)
        Me.DownloadLabel.TabIndex = 2
        Me.DownloadLabel.Text = "发现新版本，是否更新？"
        Me.DownloadLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DLOKButton
        '
        Me.DLOKButton.BackColor = System.Drawing.Color.Transparent
        Me.DLOKButton.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.DLOKButton.Image = Global.HackSystem.My.Resources.SystemAssets.DownloadButton_0
        Me.DLOKButton.Location = New System.Drawing.Point(46, 92)
        Me.DLOKButton.Name = "DLOKButton"
        Me.DLOKButton.Size = New System.Drawing.Size(80, 26)
        Me.DLOKButton.TabIndex = 3
        Me.DLOKButton.Tag = "DownloadButton"
        Me.DLOKButton.Text = "确定"
        Me.DLOKButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DLCancelButton
        '
        Me.DLCancelButton.BackColor = System.Drawing.Color.Transparent
        Me.DLCancelButton.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.DLCancelButton.ForeColor = System.Drawing.Color.Black
        Me.DLCancelButton.Image = Global.HackSystem.My.Resources.SystemAssets.DownloadButton_0
        Me.DLCancelButton.Location = New System.Drawing.Point(156, 92)
        Me.DLCancelButton.Name = "DLCancelButton"
        Me.DLCancelButton.Size = New System.Drawing.Size(80, 26)
        Me.DLCancelButton.TabIndex = 4
        Me.DLCancelButton.Tag = "DownloadButton"
        Me.DLCancelButton.Text = "取消"
        Me.DLCancelButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DownloaderForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BackgroundImage = Global.HackSystem.My.Resources.SystemAssets.DownloaderBGI
        Me.ClientSize = New System.Drawing.Size(282, 134)
        Me.Controls.Add(Me.DLCancelButton)
        Me.Controls.Add(Me.DLOKButton)
        Me.Controls.Add(Me.DownloadLabel)
        Me.Controls.Add(Me.DLProgressBar)
        Me.Controls.Add(Me.CloseButton)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "DownloaderForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "下载更新"
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        CType(Me.DLProgressBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CloseButton As Label
    Friend WithEvents DLProgressBar As PictureBox
    Friend WithEvents DownloadLabel As Label
    Friend WithEvents DLOKButton As Label
    Friend WithEvents DLCancelButton As Label
End Class
