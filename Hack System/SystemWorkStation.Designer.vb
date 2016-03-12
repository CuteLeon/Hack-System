<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SystemWorkStation
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SystemWorkStation))
        Me.WorkStationWallpaperControl = New System.Windows.Forms.PictureBox()
        Me.DesktopMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MenuLastWallpaper = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuNextWallpaper = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuCustomWallpaper = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuSetForecolor = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuTopMost = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuShutdown = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShutdownButtonControl = New System.Windows.Forms.Label()
        Me.SettingButtonControl = New System.Windows.Forms.Label()
        Me.IconMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MenuSetToWallpaper = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuBreath = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuCloseScript = New System.Windows.Forms.ToolStripMenuItem()
        Me.PerformanceCounterTimer = New System.Windows.Forms.Timer(Me.components)
        Me.XYBrowserButtonControl = New System.Windows.Forms.Label()
        Me.XYMailButtonControl = New System.Windows.Forms.Label()
        Me.ConsoleButtonControl = New System.Windows.Forms.Label()
        Me.InfoTitle = New System.Windows.Forms.Label()
        Me.CustomWallpaperDialog = New System.Windows.Forms.OpenFileDialog()
        Me.VoiceLevelBar = New System.Windows.Forms.ProgressBar()
        Me.SpeechButtonControl = New System.Windows.Forms.PictureBox()
        Me.CPUCounterLable = New System.Windows.Forms.Label()
        Me.MemoryUsageRateLabel = New System.Windows.Forms.Label()
        Me.DiskReadCounterLabel = New System.Windows.Forms.Label()
        Me.DiskWriteCounterLabel = New System.Windows.Forms.Label()
        Me.UploadSpeedCountLabel = New System.Windows.Forms.Label()
        Me.DownloadSpeedCountLabel = New System.Windows.Forms.Label()
        Me.IPLabel = New System.Windows.Forms.Label()
        Me.AddressLabel = New System.Windows.Forms.Label()
        Me.DateTimeLabel = New System.Windows.Forms.Label()
        CType(Me.WorkStationWallpaperControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DesktopMenuStrip.SuspendLayout()
        Me.IconMenuStrip.SuspendLayout()
        CType(Me.SpeechButtonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'WorkStationWallpaperControl
        '
        Me.WorkStationWallpaperControl.BackColor = System.Drawing.SystemColors.Control
        Me.WorkStationWallpaperControl.ContextMenuStrip = Me.DesktopMenuStrip
        Me.WorkStationWallpaperControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WorkStationWallpaperControl.Image = Global.HackSystem.My.Resources.SystemAssets.SystemWallpaper_09
        Me.WorkStationWallpaperControl.Location = New System.Drawing.Point(0, 0)
        Me.WorkStationWallpaperControl.Name = "WorkStationWallpaperControl"
        Me.WorkStationWallpaperControl.Size = New System.Drawing.Size(1181, 551)
        Me.WorkStationWallpaperControl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.WorkStationWallpaperControl.TabIndex = 1
        Me.WorkStationWallpaperControl.TabStop = False
        Me.WorkStationWallpaperControl.Tag = ""
        '
        'DesktopMenuStrip
        '
        Me.DesktopMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuLastWallpaper, Me.MenuNextWallpaper, Me.ToolStripSeparator3, Me.MenuCustomWallpaper, Me.MenuSetForecolor, Me.ToolStripSeparator1, Me.MenuTopMost, Me.ToolStripSeparator2, Me.MenuShutdown})
        Me.DesktopMenuStrip.Name = "SystemMenuStrip"
        Me.DesktopMenuStrip.Size = New System.Drawing.Size(185, 154)
        Me.DesktopMenuStrip.Text = "系统菜单"
        '
        'MenuLastWallpaper
        '
        Me.MenuLastWallpaper.Name = "MenuLastWallpaper"
        Me.MenuLastWallpaper.Size = New System.Drawing.Size(184, 22)
        Me.MenuLastWallpaper.Text = "Last Wallpaper"
        '
        'MenuNextWallpaper
        '
        Me.MenuNextWallpaper.Name = "MenuNextWallpaper"
        Me.MenuNextWallpaper.Size = New System.Drawing.Size(184, 22)
        Me.MenuNextWallpaper.Text = "Next Wallpaper"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(181, 6)
        '
        'MenuCustomWallpaper
        '
        Me.MenuCustomWallpaper.Name = "MenuCustomWallpaper"
        Me.MenuCustomWallpaper.Size = New System.Drawing.Size(184, 22)
        Me.MenuCustomWallpaper.Text = "Custom Wallpaper"
        '
        'MenuSetForecolor
        '
        Me.MenuSetForecolor.Name = "MenuSetForecolor"
        Me.MenuSetForecolor.Size = New System.Drawing.Size(184, 22)
        Me.MenuSetForecolor.Text = "Set Forecolor"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(181, 6)
        '
        'MenuTopMost
        '
        Me.MenuTopMost.Checked = True
        Me.MenuTopMost.CheckOnClick = True
        Me.MenuTopMost.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MenuTopMost.Name = "MenuTopMost"
        Me.MenuTopMost.Size = New System.Drawing.Size(184, 22)
        Me.MenuTopMost.Text = "Topmost"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(181, 6)
        '
        'MenuShutdown
        '
        Me.MenuShutdown.Name = "MenuShutdown"
        Me.MenuShutdown.Size = New System.Drawing.Size(184, 22)
        Me.MenuShutdown.Text = "Shutdown"
        '
        'ShutdownButtonControl
        '
        Me.ShutdownButtonControl.BackColor = System.Drawing.Color.Transparent
        Me.ShutdownButtonControl.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ShutdownButtonControl.ForeColor = System.Drawing.Color.Aqua
        Me.ShutdownButtonControl.Image = Global.HackSystem.My.Resources.SystemAssets.ShutdownWindow_0
        Me.ShutdownButtonControl.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ShutdownButtonControl.Location = New System.Drawing.Point(1105, 460)
        Me.ShutdownButtonControl.Name = "ShutdownButtonControl"
        Me.ShutdownButtonControl.Size = New System.Drawing.Size(66, 80)
        Me.ShutdownButtonControl.TabIndex = 2
        Me.ShutdownButtonControl.Tag = "ShutdownWindow_"
        Me.ShutdownButtonControl.Text = "Shutdown"
        Me.ShutdownButtonControl.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'SettingButtonControl
        '
        Me.SettingButtonControl.BackColor = System.Drawing.Color.Transparent
        Me.SettingButtonControl.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.SettingButtonControl.ForeColor = System.Drawing.Color.Aqua
        Me.SettingButtonControl.Image = CType(resources.GetObject("SettingButtonControl.Image"), System.Drawing.Image)
        Me.SettingButtonControl.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.SettingButtonControl.Location = New System.Drawing.Point(1035, 460)
        Me.SettingButtonControl.Name = "SettingButtonControl"
        Me.SettingButtonControl.Size = New System.Drawing.Size(66, 80)
        Me.SettingButtonControl.TabIndex = 3
        Me.SettingButtonControl.Tag = "SettingButton_"
        Me.SettingButtonControl.Text = "About"
        Me.SettingButtonControl.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'IconMenuStrip
        '
        Me.IconMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuSetToWallpaper, Me.MenuBreath, Me.ToolStripSeparator4, Me.MenuCloseScript})
        Me.IconMenuStrip.Name = "IconMenuStrip"
        Me.IconMenuStrip.Size = New System.Drawing.Size(180, 76)
        '
        'MenuSetToWallpaper
        '
        Me.MenuSetToWallpaper.Name = "MenuSetToWallpaper"
        Me.MenuSetToWallpaper.Size = New System.Drawing.Size(179, 22)
        Me.MenuSetToWallpaper.Text = "Set To Wallpaper"
        '
        'MenuBreath
        '
        Me.MenuBreath.Name = "MenuBreath"
        Me.MenuBreath.Size = New System.Drawing.Size(179, 22)
        Me.MenuBreath.Text = "Start/Stop Breath"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(176, 6)
        '
        'MenuCloseScript
        '
        Me.MenuCloseScript.Name = "MenuCloseScript"
        Me.MenuCloseScript.Size = New System.Drawing.Size(179, 22)
        Me.MenuCloseScript.Text = "Close       ALT+F4"
        '
        'PerformanceCounterTimer
        '
        Me.PerformanceCounterTimer.Interval = 1000
        '
        'XYBrowserButtonControl
        '
        Me.XYBrowserButtonControl.BackColor = System.Drawing.Color.Transparent
        Me.XYBrowserButtonControl.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.XYBrowserButtonControl.ForeColor = System.Drawing.Color.Aqua
        Me.XYBrowserButtonControl.Image = Global.HackSystem.My.Resources.SystemAssets.XYBrowserIcon0
        Me.XYBrowserButtonControl.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.XYBrowserButtonControl.Location = New System.Drawing.Point(965, 460)
        Me.XYBrowserButtonControl.Name = "XYBrowserButtonControl"
        Me.XYBrowserButtonControl.Size = New System.Drawing.Size(66, 80)
        Me.XYBrowserButtonControl.TabIndex = 6
        Me.XYBrowserButtonControl.Tag = "XYBrowserIcon"
        Me.XYBrowserButtonControl.Text = "Browser"
        Me.XYBrowserButtonControl.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'XYMailButtonControl
        '
        Me.XYMailButtonControl.BackColor = System.Drawing.Color.Transparent
        Me.XYMailButtonControl.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.XYMailButtonControl.ForeColor = System.Drawing.Color.Aqua
        Me.XYMailButtonControl.Image = Global.HackSystem.My.Resources.SystemAssets.Mailcon_0
        Me.XYMailButtonControl.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.XYMailButtonControl.Location = New System.Drawing.Point(895, 460)
        Me.XYMailButtonControl.Name = "XYMailButtonControl"
        Me.XYMailButtonControl.Size = New System.Drawing.Size(66, 80)
        Me.XYMailButtonControl.TabIndex = 7
        Me.XYMailButtonControl.Tag = "Mailcon_"
        Me.XYMailButtonControl.Text = "Mail"
        Me.XYMailButtonControl.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'ConsoleButtonControl
        '
        Me.ConsoleButtonControl.BackColor = System.Drawing.Color.Transparent
        Me.ConsoleButtonControl.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ConsoleButtonControl.ForeColor = System.Drawing.Color.Aqua
        Me.ConsoleButtonControl.Image = Global.HackSystem.My.Resources.SystemAssets.Console_0
        Me.ConsoleButtonControl.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ConsoleButtonControl.Location = New System.Drawing.Point(825, 460)
        Me.ConsoleButtonControl.Name = "ConsoleButtonControl"
        Me.ConsoleButtonControl.Size = New System.Drawing.Size(66, 80)
        Me.ConsoleButtonControl.TabIndex = 8
        Me.ConsoleButtonControl.Tag = "Console_"
        Me.ConsoleButtonControl.Text = "Console"
        Me.ConsoleButtonControl.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'InfoTitle
        '
        Me.InfoTitle.BackColor = System.Drawing.Color.Transparent
        Me.InfoTitle.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.InfoTitle.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.InfoTitle.ForeColor = System.Drawing.Color.White
        Me.InfoTitle.Location = New System.Drawing.Point(968, 37)
        Me.InfoTitle.Name = "InfoTitle"
        Me.InfoTitle.Size = New System.Drawing.Size(93, 160)
        Me.InfoTitle.TabIndex = 9
        Me.InfoTitle.Text = "CPU：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "RAM：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "DiskRead：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "DiskWrite：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Upload：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Download：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "IP：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Address："
        Me.InfoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CustomWallpaperDialog
        '
        Me.CustomWallpaperDialog.Filter = "Image Files|*.jpg;*.jpeg;*.gif;*.png;*.bmp;*.wmf|All Files|*.*"
        Me.CustomWallpaperDialog.Title = "Please select the path of custom wallpaper."
        '
        'VoiceLevelBar
        '
        Me.VoiceLevelBar.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.VoiceLevelBar.Location = New System.Drawing.Point(760, 526)
        Me.VoiceLevelBar.Name = "VoiceLevelBar"
        Me.VoiceLevelBar.Size = New System.Drawing.Size(56, 10)
        Me.VoiceLevelBar.TabIndex = 11
        '
        'SpeechButtonControl
        '
        Me.SpeechButtonControl.BackColor = System.Drawing.Color.Transparent
        Me.SpeechButtonControl.BackgroundImage = Global.HackSystem.My.Resources.SystemAssets.SpeechButton_0
        Me.SpeechButtonControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.SpeechButtonControl.Image = Global.HackSystem.My.Resources.SystemAssets.MicroPhone_On
        Me.SpeechButtonControl.Location = New System.Drawing.Point(755, 460)
        Me.SpeechButtonControl.Name = "SpeechButtonControl"
        Me.SpeechButtonControl.Size = New System.Drawing.Size(66, 78)
        Me.SpeechButtonControl.TabIndex = 12
        Me.SpeechButtonControl.TabStop = False
        '
        'CPUCounterLable
        '
        Me.CPUCounterLable.BackColor = System.Drawing.Color.Transparent
        Me.CPUCounterLable.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.CPUCounterLable.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.CPUCounterLable.ForeColor = System.Drawing.Color.White
        Me.CPUCounterLable.Location = New System.Drawing.Point(1061, 37)
        Me.CPUCounterLable.Name = "CPUCounterLable"
        Me.CPUCounterLable.Size = New System.Drawing.Size(115, 20)
        Me.CPUCounterLable.TabIndex = 13
        Me.CPUCounterLable.Text = "0 %"
        Me.CPUCounterLable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MemoryUsageRateLabel
        '
        Me.MemoryUsageRateLabel.BackColor = System.Drawing.Color.Transparent
        Me.MemoryUsageRateLabel.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.MemoryUsageRateLabel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.MemoryUsageRateLabel.ForeColor = System.Drawing.Color.White
        Me.MemoryUsageRateLabel.Location = New System.Drawing.Point(1061, 57)
        Me.MemoryUsageRateLabel.Name = "MemoryUsageRateLabel"
        Me.MemoryUsageRateLabel.Size = New System.Drawing.Size(115, 20)
        Me.MemoryUsageRateLabel.TabIndex = 14
        Me.MemoryUsageRateLabel.Text = "0 %"
        Me.MemoryUsageRateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DiskReadCounterLabel
        '
        Me.DiskReadCounterLabel.BackColor = System.Drawing.Color.Transparent
        Me.DiskReadCounterLabel.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.DiskReadCounterLabel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.DiskReadCounterLabel.ForeColor = System.Drawing.Color.White
        Me.DiskReadCounterLabel.Location = New System.Drawing.Point(1061, 77)
        Me.DiskReadCounterLabel.Name = "DiskReadCounterLabel"
        Me.DiskReadCounterLabel.Size = New System.Drawing.Size(115, 20)
        Me.DiskReadCounterLabel.TabIndex = 15
        Me.DiskReadCounterLabel.Text = "0.00 MB/S"
        Me.DiskReadCounterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DiskWriteCounterLabel
        '
        Me.DiskWriteCounterLabel.BackColor = System.Drawing.Color.Transparent
        Me.DiskWriteCounterLabel.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.DiskWriteCounterLabel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.DiskWriteCounterLabel.ForeColor = System.Drawing.Color.White
        Me.DiskWriteCounterLabel.Location = New System.Drawing.Point(1061, 97)
        Me.DiskWriteCounterLabel.Name = "DiskWriteCounterLabel"
        Me.DiskWriteCounterLabel.Size = New System.Drawing.Size(115, 20)
        Me.DiskWriteCounterLabel.TabIndex = 16
        Me.DiskWriteCounterLabel.Text = "0.00 MB/S"
        Me.DiskWriteCounterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UploadSpeedCountLabel
        '
        Me.UploadSpeedCountLabel.BackColor = System.Drawing.Color.Transparent
        Me.UploadSpeedCountLabel.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.UploadSpeedCountLabel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.UploadSpeedCountLabel.ForeColor = System.Drawing.Color.White
        Me.UploadSpeedCountLabel.Location = New System.Drawing.Point(1061, 117)
        Me.UploadSpeedCountLabel.Name = "UploadSpeedCountLabel"
        Me.UploadSpeedCountLabel.Size = New System.Drawing.Size(115, 20)
        Me.UploadSpeedCountLabel.TabIndex = 17
        Me.UploadSpeedCountLabel.Text = "0.00 MB/S"
        Me.UploadSpeedCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DownloadSpeedCountLabel
        '
        Me.DownloadSpeedCountLabel.BackColor = System.Drawing.Color.Transparent
        Me.DownloadSpeedCountLabel.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.DownloadSpeedCountLabel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.DownloadSpeedCountLabel.ForeColor = System.Drawing.Color.White
        Me.DownloadSpeedCountLabel.Location = New System.Drawing.Point(1061, 137)
        Me.DownloadSpeedCountLabel.Name = "DownloadSpeedCountLabel"
        Me.DownloadSpeedCountLabel.Size = New System.Drawing.Size(115, 20)
        Me.DownloadSpeedCountLabel.TabIndex = 18
        Me.DownloadSpeedCountLabel.Text = "0.00 MB/S"
        Me.DownloadSpeedCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'IPLabel
        '
        Me.IPLabel.BackColor = System.Drawing.Color.Transparent
        Me.IPLabel.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.IPLabel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.IPLabel.ForeColor = System.Drawing.Color.White
        Me.IPLabel.Location = New System.Drawing.Point(1061, 157)
        Me.IPLabel.Name = "IPLabel"
        Me.IPLabel.Size = New System.Drawing.Size(115, 20)
        Me.IPLabel.TabIndex = 19
        Me.IPLabel.Text = "000.000.000.000"
        Me.IPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'AddressLabel
        '
        Me.AddressLabel.BackColor = System.Drawing.Color.Transparent
        Me.AddressLabel.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.AddressLabel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.AddressLabel.ForeColor = System.Drawing.Color.White
        Me.AddressLabel.Location = New System.Drawing.Point(1061, 177)
        Me.AddressLabel.Name = "AddressLabel"
        Me.AddressLabel.Size = New System.Drawing.Size(115, 20)
        Me.AddressLabel.TabIndex = 20
        Me.AddressLabel.Text = "xx省xx市"
        Me.AddressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DateTimeLabel
        '
        Me.DateTimeLabel.BackColor = System.Drawing.Color.Transparent
        Me.DateTimeLabel.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.DateTimeLabel.ForeColor = System.Drawing.Color.White
        Me.DateTimeLabel.Location = New System.Drawing.Point(968, 8)
        Me.DateTimeLabel.Name = "DateTimeLabel"
        Me.DateTimeLabel.Size = New System.Drawing.Size(208, 25)
        Me.DateTimeLabel.TabIndex = 23
        Me.DateTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SystemWorkStation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1181, 551)
        Me.Controls.Add(Me.DateTimeLabel)
        Me.Controls.Add(Me.AddressLabel)
        Me.Controls.Add(Me.IPLabel)
        Me.Controls.Add(Me.DownloadSpeedCountLabel)
        Me.Controls.Add(Me.UploadSpeedCountLabel)
        Me.Controls.Add(Me.DiskWriteCounterLabel)
        Me.Controls.Add(Me.DiskReadCounterLabel)
        Me.Controls.Add(Me.MemoryUsageRateLabel)
        Me.Controls.Add(Me.CPUCounterLable)
        Me.Controls.Add(Me.VoiceLevelBar)
        Me.Controls.Add(Me.InfoTitle)
        Me.Controls.Add(Me.ConsoleButtonControl)
        Me.Controls.Add(Me.XYMailButtonControl)
        Me.Controls.Add(Me.XYBrowserButtonControl)
        Me.Controls.Add(Me.SettingButtonControl)
        Me.Controls.Add(Me.ShutdownButtonControl)
        Me.Controls.Add(Me.SpeechButtonControl)
        Me.Controls.Add(Me.WorkStationWallpaperControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "SystemWorkStation"
        Me.Text = "SystemWorkStation"
        Me.TopMost = True
        CType(Me.WorkStationWallpaperControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DesktopMenuStrip.ResumeLayout(False)
        Me.IconMenuStrip.ResumeLayout(False)
        CType(Me.SpeechButtonControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents WorkStationWallpaperControl As PictureBox
    Friend WithEvents ShutdownButtonControl As Label
    Friend WithEvents SettingButtonControl As Label
    Friend WithEvents DesktopMenuStrip As ContextMenuStrip
    Friend WithEvents MenuShutdown As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents MenuNextWallpaper As ToolStripMenuItem
    Friend WithEvents MenuLastWallpaper As ToolStripMenuItem
    Friend WithEvents IconMenuStrip As ContextMenuStrip
    Friend WithEvents MenuCloseScript As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents MenuSetToWallpaper As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents MenuTopMost As ToolStripMenuItem
    Friend WithEvents PerformanceCounterTimer As Timer
    Friend WithEvents XYBrowserButtonControl As Label
    Friend WithEvents XYMailButtonControl As Label
    Friend WithEvents ConsoleButtonControl As Label
    Friend WithEvents InfoTitle As Label
    Friend WithEvents MenuBreath As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents MenuCustomWallpaper As ToolStripMenuItem
    Friend WithEvents CustomWallpaperDialog As OpenFileDialog
    Friend WithEvents VoiceLevelBar As ProgressBar
    Friend WithEvents SpeechButtonControl As PictureBox
    Friend WithEvents CPUCounterLable As Label
    Friend WithEvents MemoryUsageRateLabel As Label
    Friend WithEvents DiskReadCounterLabel As Label
    Friend WithEvents DiskWriteCounterLabel As Label
    Friend WithEvents UploadSpeedCountLabel As Label
    Friend WithEvents DownloadSpeedCountLabel As Label
    Friend WithEvents MenuSetForecolor As ToolStripMenuItem
    Friend WithEvents IPLabel As Label
    Friend WithEvents AddressLabel As Label
    Friend WithEvents DateTimeLabel As Label
End Class
