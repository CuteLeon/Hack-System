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
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuTopMost = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuShutdown = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShutdownButtonControl = New System.Windows.Forms.Label()
        Me.SettingButtonControl = New System.Windows.Forms.Label()
        Me.IconMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MenuSetToWallpaper = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuCloseScript = New System.Windows.Forms.ToolStripMenuItem()
        Me.PerformanceCounterTimer = New System.Windows.Forms.Timer(Me.components)
        Me.XYBrowserButtonControl = New System.Windows.Forms.Label()
        Me.XYMailButtonControl = New System.Windows.Forms.Label()
        Me.ConsoleButtonControl = New System.Windows.Forms.Label()
        Me.InfoData = New System.Windows.Forms.Label()
        Me.InfoTitle = New System.Windows.Forms.Label()
        Me.MenuBreath = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.WorkStationWallpaperControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DesktopMenuStrip.SuspendLayout()
        Me.IconMenuStrip.SuspendLayout()
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
        Me.DesktopMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuLastWallpaper, Me.MenuNextWallpaper, Me.ToolStripSeparator1, Me.MenuTopMost, Me.ToolStripSeparator2, Me.MenuShutdown})
        Me.DesktopMenuStrip.Name = "SystemMenuStrip"
        Me.DesktopMenuStrip.Size = New System.Drawing.Size(168, 104)
        Me.DesktopMenuStrip.Text = "系统菜单"
        '
        'MenuLastWallpaper
        '
        Me.MenuLastWallpaper.Name = "MenuLastWallpaper"
        Me.MenuLastWallpaper.Size = New System.Drawing.Size(167, 22)
        Me.MenuLastWallpaper.Text = "Last Wallpaper"
        '
        'MenuNextWallpaper
        '
        Me.MenuNextWallpaper.Name = "MenuNextWallpaper"
        Me.MenuNextWallpaper.Size = New System.Drawing.Size(167, 22)
        Me.MenuNextWallpaper.Text = "Next Wallpaper"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(164, 6)
        '
        'MenuTopMost
        '
        Me.MenuTopMost.Checked = True
        Me.MenuTopMost.CheckOnClick = True
        Me.MenuTopMost.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MenuTopMost.Name = "MenuTopMost"
        Me.MenuTopMost.Size = New System.Drawing.Size(167, 22)
        Me.MenuTopMost.Text = "Topmost"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(164, 6)
        '
        'MenuShutdown
        '
        Me.MenuShutdown.Name = "MenuShutdown"
        Me.MenuShutdown.Size = New System.Drawing.Size(167, 22)
        Me.MenuShutdown.Text = "Shutdown"
        '
        'ShutdownButtonControl
        '
        Me.ShutdownButtonControl.BackColor = System.Drawing.Color.Transparent
        Me.ShutdownButtonControl.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ShutdownButtonControl.Image = Global.HackSystem.My.Resources.SystemAssets.ShutdownWindow_0
        Me.ShutdownButtonControl.Location = New System.Drawing.Point(1035, 478)
        Me.ShutdownButtonControl.Name = "ShutdownButtonControl"
        Me.ShutdownButtonControl.Size = New System.Drawing.Size(64, 64)
        Me.ShutdownButtonControl.TabIndex = 2
        Me.ShutdownButtonControl.Tag = "ShutdownWindow_"
        Me.ShutdownButtonControl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SettingButtonControl
        '
        Me.SettingButtonControl.BackColor = System.Drawing.Color.Transparent
        Me.SettingButtonControl.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.SettingButtonControl.Image = CType(resources.GetObject("SettingButtonControl.Image"), System.Drawing.Image)
        Me.SettingButtonControl.Location = New System.Drawing.Point(1105, 478)
        Me.SettingButtonControl.Name = "SettingButtonControl"
        Me.SettingButtonControl.Size = New System.Drawing.Size(64, 64)
        Me.SettingButtonControl.TabIndex = 3
        Me.SettingButtonControl.Tag = "SettingButton_"
        Me.SettingButtonControl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'IconMenuStrip
        '
        Me.IconMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuSetToWallpaper, Me.MenuBreath, Me.ToolStripSeparator4, Me.MenuCloseScript})
        Me.IconMenuStrip.Name = "IconMenuStrip"
        Me.IconMenuStrip.Size = New System.Drawing.Size(180, 98)
        '
        'MenuSetToWallpaper
        '
        Me.MenuSetToWallpaper.Name = "MenuSetToWallpaper"
        Me.MenuSetToWallpaper.Size = New System.Drawing.Size(179, 22)
        Me.MenuSetToWallpaper.Text = "Set To Wallpaper"
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
        Me.XYBrowserButtonControl.Image = Global.HackSystem.My.Resources.SystemAssets.XYBrowserIcon0
        Me.XYBrowserButtonControl.Location = New System.Drawing.Point(965, 478)
        Me.XYBrowserButtonControl.Name = "XYBrowserButtonControl"
        Me.XYBrowserButtonControl.Size = New System.Drawing.Size(64, 64)
        Me.XYBrowserButtonControl.TabIndex = 6
        Me.XYBrowserButtonControl.Tag = "XYBrowserIcon"
        Me.XYBrowserButtonControl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'XYMailButtonControl
        '
        Me.XYMailButtonControl.BackColor = System.Drawing.Color.Transparent
        Me.XYMailButtonControl.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.XYMailButtonControl.Image = Global.HackSystem.My.Resources.SystemAssets.Mailcon_0
        Me.XYMailButtonControl.Location = New System.Drawing.Point(895, 478)
        Me.XYMailButtonControl.Name = "XYMailButtonControl"
        Me.XYMailButtonControl.Size = New System.Drawing.Size(64, 64)
        Me.XYMailButtonControl.TabIndex = 7
        Me.XYMailButtonControl.Tag = "Mailcon_"
        Me.XYMailButtonControl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ConsoleButtonControl
        '
        Me.ConsoleButtonControl.BackColor = System.Drawing.Color.Transparent
        Me.ConsoleButtonControl.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ConsoleButtonControl.Image = Global.HackSystem.My.Resources.SystemAssets.Console_0
        Me.ConsoleButtonControl.Location = New System.Drawing.Point(825, 478)
        Me.ConsoleButtonControl.Name = "ConsoleButtonControl"
        Me.ConsoleButtonControl.Size = New System.Drawing.Size(64, 64)
        Me.ConsoleButtonControl.TabIndex = 8
        Me.ConsoleButtonControl.Tag = "Console_"
        Me.ConsoleButtonControl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'InfoData
        '
        Me.InfoData.BackColor = System.Drawing.Color.Transparent
        Me.InfoData.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.InfoData.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.InfoData.ForeColor = System.Drawing.Color.White
        Me.InfoData.Location = New System.Drawing.Point(1080, 9)
        Me.InfoData.Name = "InfoData"
        Me.InfoData.Size = New System.Drawing.Size(92, 120)
        Me.InfoData.TabIndex = 10
        Me.InfoData.Text = "0%" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "0%" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "0.00 MB/S" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "0.00 MB/S" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "0.00 KB/S" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "0.00 KB/S"
        Me.InfoData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'InfoTitle
        '
        Me.InfoTitle.BackColor = System.Drawing.Color.Transparent
        Me.InfoTitle.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.InfoTitle.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.InfoTitle.ForeColor = System.Drawing.Color.White
        Me.InfoTitle.Location = New System.Drawing.Point(987, 9)
        Me.InfoTitle.Name = "InfoTitle"
        Me.InfoTitle.Size = New System.Drawing.Size(93, 120)
        Me.InfoTitle.TabIndex = 9
        Me.InfoTitle.Text = "CPU：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "RAM：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "DiskRead：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "DiskWrite：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Upload：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Download："
        Me.InfoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'MenuBreath
        '
        Me.MenuBreath.Name = "MenuBreath"
        Me.MenuBreath.Size = New System.Drawing.Size(179, 22)
        Me.MenuBreath.Text = "Start/Stop Breath"
        '
        'SystemWorkStation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1181, 551)
        Me.Controls.Add(Me.InfoData)
        Me.Controls.Add(Me.InfoTitle)
        Me.Controls.Add(Me.ConsoleButtonControl)
        Me.Controls.Add(Me.XYMailButtonControl)
        Me.Controls.Add(Me.XYBrowserButtonControl)
        Me.Controls.Add(Me.SettingButtonControl)
        Me.Controls.Add(Me.ShutdownButtonControl)
        Me.Controls.Add(Me.WorkStationWallpaperControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "SystemWorkStation"
        Me.Text = "SystemWorkStation"
        Me.TopMost = True
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.WorkStationWallpaperControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DesktopMenuStrip.ResumeLayout(False)
        Me.IconMenuStrip.ResumeLayout(False)
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
    Friend WithEvents InfoData As Label
    Friend WithEvents MenuBreath As ToolStripMenuItem
End Class
