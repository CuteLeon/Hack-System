Imports System.ComponentModel
Imports System.Speech.Recognition
Imports Microsoft.VisualBasic.Devices

Public Class SystemWorkStation
    Public Declare Function SetForegroundWindow Lib "user32" Alias "SetForegroundWindow" (ByVal hwnd As Integer) As Integer
    Private Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    Private Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer
    Private Declare Function GetDesktopWindow Lib "user32" Alias "GetDesktopWindow" () As IntPtr
    Private Declare Function SetParent Lib "user32" Alias "SetParent" (ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As Integer
    Private Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As IntPtr
    Private Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As IntPtr, ByVal nIndex As Integer) As Integer
    Private Declare Function SetLayeredWindowAttributes Lib "user32" (ByVal hwnd As IntPtr, ByVal crKey As Integer, ByVal bAlpha As Integer, ByVal dwFlags As Integer) As Integer

    Public Const AeroPeekOpacity As Double = 0.15 'Opacity in AeroPeek model.
    Public Const ScriptUpperBound As Int16 = 22
    Public Const IconWidth As Integer = 65
    Public Const IconHeight As Integer = 90
    Public Const MainHomeURL As String = "http://www.baidu.com/"

    '以默认语言创建语音识别引擎
    Public MySpeechRecognitionEngine As SpeechRecognitionEngine
    Public ScriptInfomation() As String = {"Digital Rain", "Network Attack", "Air Defence", "Iron Man", "Attack Data", "3D Map", "Ballistic Missile", "Missile", "Action Indication", "Zone Isolation", "Waiting...", "Life Support", "Agent Info.", "Graphic SO", "Face 3DModel", "Driving System", "Thinking Export", "ARToolkit", "Combat", "UAV Camera", "NOVA 6", "Satellite", "Decrypt"}
    Public ScriptSpeechGrammar() As String = {"打开数字雨", "打开网络攻击", "打开防空系统", "打开钢铁侠", "打开攻击数据", "打开三维地图", "打开弹道导弹", "打开导弹部署", "打开行动指示", "打开区域隔离", "打开等待连接", "打开生命维护系统", "打开特工信息", "打开示波器", "打开面部模型", "打开驱动系统", "打开思维导出系统", "打开增强现实", "打开作战部署", "打开无人机", "打开新星", "打开近地卫星", "打开解密"}
    Public AeroPeekModel As Boolean 'AeroPeek model state.
    Public ScriptForm(ScriptUpperBound) As WindowsTemplates 'Scripts
    Public BrowserForms As New ArrayList  'Browsers.
    Public ScriptFormVisible(ScriptUpperBound) As Boolean 'Scripts' visible.（Diffrent with Form.Visible）
    Public ScriptIcons(ScriptUpperBound) As Label
    Public NowIconIndex As Integer = -1 'Icon under mouse.
    Public RightestLoction As Integer 'The rightest icon's right location.
    Public NotFirstGetIPAndAddress As Boolean
    Public SystemClosing As Boolean 'Application is going to exit.

    Dim SpeechRecognitionMode As Boolean = True
    Dim MouseDownLocation As Point
    Dim XDistance, YDistance As Integer
    Dim ColumnIconCount As Integer = Int(My.Computer.Screen.Bounds.Height / IconHeight) - 1
    Dim IntervalDistance As Integer = (My.Computer.Screen.Bounds.Height - ColumnIconCount * IconHeight) / (ColumnIconCount + 1) 'Distance between icons.
    Dim WallpaperIndex As Integer = 9 'Default wallpaper's ID.
    Dim CustomWallpaperBitmap As Bitmap 'User Custom Wallpaper
    Dim HighLightIcon(ScriptUpperBound) As Bitmap
    Dim MouseDownIcon(ScriptUpperBound) As Bitmap
    Dim IconGraphics As Graphics
    Dim SenderControl As Label
    Dim NowButton As Label
    Dim CPUCounter As New PerformanceCounter("Processor", "% Processor Time", "_Total") 'CPU
    Dim DiskReadCounter As New PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", "_Total") 'RAM
    Dim DiskWriteCounter As New PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total") 'DISK
    Dim CmptInfo As New ComputerInfo()
    Dim MemoryUsageRate As Integer
    Dim PCCategory As New PerformanceCounterCategory("Network Interface") 'NetWork
    Dim LBoundOfArray As UInteger = 0, UBoundOfArray As UInteger = PCCategory.GetInstanceNames.Count - 1
    Dim DownloadCounter(UBoundOfArray) As PerformanceCounter
    Dim UploadCounter(UBoundOfArray) As PerformanceCounter
    Dim DownloadSpeed(UBoundOfArray) As ULong, UploadSpeed(UBoundOfArray) As ULong
    Dim DownloadValue(UBoundOfArray) As ULong, UploadValue(UBoundOfArray) As ULong
    Dim DownloadValueOld(UBoundOfArray) As ULong, UploadValueOld(UBoundOfArray) As ULong
    Dim DownloadSpeedCount As ULong, UploadSpeedCount As ULong

    Private Sub SystemWorkStation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Location = New Point(0, 0)
        Me.Size = My.Computer.Screen.Bounds.Size

        '用控件显示图片当做壁纸：窗体BackgroundImage不支持动态图
        InfoTitle.Parent = WorkStationWallpaperControl
        CPUCounterBar.Parent = WorkStationWallpaperControl
        MemoryUsageRateBar.Parent = WorkStationWallpaperControl
        DiskReadCounterLabel.Parent = WorkStationWallpaperControl
        DiskWriteCounterLabel.Parent = WorkStationWallpaperControl
        UploadSpeedCountLabel.Parent = WorkStationWallpaperControl
        DownloadSpeedCountLabel.Parent = WorkStationWallpaperControl
        IPLabel.Parent = WorkStationWallpaperControl
        AddressLabel.Parent = WorkStationWallpaperControl
        DateTimeLabel.Parent = WorkStationWallpaperControl

        ConsoleButtonControl.Parent = WorkStationWallpaperControl
        XYMailButtonControl.Parent = WorkStationWallpaperControl
        XYBrowserButtonControl.Parent = WorkStationWallpaperControl
        ShutdownButtonControl.Parent = WorkStationWallpaperControl
        SettingButtonControl.Parent = WorkStationWallpaperControl
        SpeechButtonControl.Parent = WorkStationWallpaperControl
        VoiceLevelBar.Parent = SpeechButtonControl

        Call SetParent(MouseDragForm.Handle, Me.Handle)
        Call SetWindowLong(MouseDragForm.Handle, -20, GetWindowLong(MouseDragForm.Handle, -20) Or &H80000)
        Call SetLayeredWindowAttributes(MouseDragForm.Handle, 0, 100, 2)

        DateTimeLabel.Location = New Point(Me.Width - DateTimeLabel.Width - 12, 12)
        InfoTitle.Location = New Point(DateTimeLabel.Left, 38)
        CPUCounterBar.Location = New Point(InfoTitle.Right + 4, InfoTitle.Top + 4)
        MemoryUsageRateBar.Location = New Point(InfoTitle.Right + 4, CPUCounterBar.Bottom + 8)
        DiskReadCounterLabel.Location = New Point(InfoTitle.Right, 78)
        DiskWriteCounterLabel.Location = New Point(InfoTitle.Right, 98)
        UploadSpeedCountLabel.Location = New Point(InfoTitle.Right, 118)
        DownloadSpeedCountLabel.Location = New Point(InfoTitle.Right, 138)
        IPLabel.Location = New Point(InfoTitle.Right, 158)
        AddressLabel.Location = New Point(InfoTitle.Right, 178)

        ShutdownButtonControl.Location = New Point(Me.Width - ShutdownButtonControl.Width - IntervalDistance, Me.Height - IntervalDistance - ShutdownButtonControl.Height)
        SettingButtonControl.Location = New Point(ShutdownButtonControl.Left - IntervalDistance - SettingButtonControl.Width, ShutdownButtonControl.Top)
        XYBrowserButtonControl.Location = New Point(SettingButtonControl.Left - IntervalDistance - XYBrowserButtonControl.Width, SettingButtonControl.Top)
        XYMailButtonControl.Location = New Point(XYBrowserButtonControl.Left - IntervalDistance - XYMailButtonControl.Width, XYBrowserButtonControl.Top)
        ConsoleButtonControl.Location = New Point(XYMailButtonControl.Left - IntervalDistance - ConsoleButtonControl.Width, XYMailButtonControl.Top)
        SpeechButtonControl.Location = New Point(ConsoleButtonControl.Left - IntervalDistance - SpeechButtonControl.Width, ConsoleButtonControl.Top)
        VoiceLevelBar.Location = New Point(5, SpeechButtonControl.Height - 12)
        Me.Cursor = StartingUpUI.SystemCursor
        InfoTitle.Cursor = StartingUpUI.SystemCursor

        CPUCounterBar.Cursor = StartingUpUI.SystemCursor
        MemoryUsageRateBar.Cursor = StartingUpUI.SystemCursor
        DiskReadCounterLabel.Cursor = StartingUpUI.SystemCursor
        DiskWriteCounterLabel.Cursor = StartingUpUI.SystemCursor
        UploadSpeedCountLabel.Cursor = StartingUpUI.SystemCursor
        DownloadSpeedCountLabel.Cursor = StartingUpUI.SystemCursor
        DateTimeLabel.Cursor = StartingUpUI.SystemCursor
        'Set as hand,to tips that users can click them.
        IPLabel.Cursor = Cursors.Hand
        AddressLabel.Cursor = Cursors.Hand

        CommandConsole.Show(Me)
        CommandConsole.Height = My.Computer.Screen.Bounds.Height
        CommandConsole.Location = New Point(My.Computer.Screen.Bounds.Width, 0)
        CommandConsole.CommandPast.Height = My.Computer.Screen.Bounds.Height - 120
        CommandConsole.CommandTip.Top = My.Computer.Screen.Bounds.Height - 100
        CommandConsole.CommandInputBox.Top = My.Computer.Screen.Bounds.Height - 40

        CustomWallpaperDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
        '考虑到用户时间和日期的格式问题，需要自适应标签大小，防止数据显示不全
        DateTimeLabel.AutoSize = True
        DateTimeLabel.Text = My.Computer.Clock.LocalTime.ToLocalTime
        DateTimeLabel.Location = New Point(Me.Width - DateTimeLabel.Width - 12, 12)
    End Sub

    Private Sub LoadGrammar()
        Dim Grammars As Choices = New Choices()
        Grammars.Add("控制台")
        Grammars.Add("确定")
        Grammars.Add("取消")
        Grammars.Add("关闭")
        Grammars.Add("切换")
        Grammars.Add("发送邮件")
        Grammars.Add("屏幕融化开启")
        Grammars.Add("屏幕融化关闭")
        Grammars.Add("关机")
        Grammars.Add("扫雷")
        Grammars.Add("浏览器")
        Grammars.Add("锁屏")
        Grammars.Add("解锁")
        Grammars.Add("about")
        For Index As Integer = 0 To UBound(ScriptSpeechGrammar)
            Grammars.Add(ScriptSpeechGrammar(Index))
        Next
        Dim GrammarList As Grammar = New Grammar(New GrammarBuilder(Grammars))
        MySpeechRecognitionEngine.LoadGrammar(GrammarList)
    End Sub

    '采用与其加载启用的 Grammar 对象匹配的输入
    Private Sub SpeechRecognitionEngine_SpeechRecognized(sender As Object, e As SpeechRecognizedEventArgs)
        If ShutdowningUI.Visible Then
            If e.Result.Text = "解锁" Then ShutdowningUI.HideShutdownForm(True)
            Exit Sub
        End If

        Select Case e.Result.Text
            Case "控制台"
                CommandConsole.ShowConsole()
            Case "确定"
                SendKeys.Send(Chr(Keys.Enter))
            Case "取消"
                SendKeys.Send(Chr(Keys.Escape))
            Case "关闭"
                SendKeys.Send("%{F4}")
            Case "切换"
                SendKeys.Send("%{TAB}")
            Case "发送邮件"
                If Not XYMail.Visible Then XYMail.Show(Me)
                SetForegroundWindow(XYMail.Handle)
                XYMail.TopMost = False
            Case "屏幕融化开启"
                ScreenMelt.StartMelt()
            Case "屏幕融化关闭"
                ScreenMelt.StopMelt()
                Me.Refresh()
            Case "关机"
                ShowShutdownWindow()
            Case "扫雷"
                If Not MineSweeperForm.Visible Then MineSweeperForm.Show(Me)
                SetForegroundWindow(MineSweeperForm.Handle)
                MineSweeperForm.TopMost = False
            Case "浏览器"
                LoadNewBrowser()
            Case "锁屏"
                If ShutdowningUI.Visible Then Exit Sub
                ShutdowningUI.Opacity = 0
                ShutdowningUI.Show(Me)
                ShutdowningUI.ShowShutdownForm()
                SetForegroundWindow(ShutdowningUI.Handle)
            Case "about"
                If Not (AboutMeForm.Visible) Then AboutMeForm.Show(Me)
                SetForegroundWindow(AboutMeForm.Handle)
            Case "解锁"
                '占空处理， 防止“解锁”进入[Case Else]
            Case Else
                Dim ScriptIndex As Integer = Array.IndexOf(ScriptSpeechGrammar, e.Result.Text)
                If ScriptIndex > -1 Then LoadScript(ScriptIndex)
        End Select
    End Sub

    '报告音频输入的级别
    Private Sub SpeechRecognitionEngine_AudioLevelUpdated(sender As Object, e As AudioLevelUpdatedEventArgs)
        VoiceLevelBar.Value = e.AudioLevel
    End Sub

    '引擎状态改变
    'Private Sub SpeechRecognitionEngine_AudioStateChanged(sender As Object, e As AudioStateChangedEventArgs)
    '   If e.AudioState = AudioState.Speech Then
    '       ResultLabel.ForeColor = Color.DarkGray
    '       Me.Text = "语音引擎： Speech..."
    '   ElseIf e.AudioState = AudioState.Silence Then
    '       Me.Text = "语音引擎：Silence..."
    '   Else
    '       Me.Text = "语音引擎：Stoped..."
    '   End If
    'End Sub

    Private Sub IconTemplates_MouseClick(sender As Object, e As MouseEventArgs)
        '点击图标调用过程加载对应的脚本窗口
        Dim ScriptIndex As Integer = Int(CType(sender, Label).Tag)
        LoadScript(ScriptIndex)
    End Sub

    Public Sub SystemWorkStation_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Dim KeyAscii As Integer = Asc(e.KeyChar)
        If KeyAscii = 27 Then
            '按下Esc弹出关机提示框
            ShowShutdownWindow()
        ElseIf KeyAscii = 96 Or KeyAscii = -24156 Then
            '按下~键，显示控制台
            CommandConsole.ShowConsole()
        Else
            '其它按键Asc码对脚本总数求余当做脚本标识，加载脚本
            KeyAscii = KeyAscii Mod (ScriptUpperBound + 1)
            LoadScript(KeyAscii)
        End If
    End Sub

    Public Sub LoadScript(ByVal ScriptIndex As Integer) '加载脚本
        '播放脚本启动音效
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("ScriptStarting"), AudioPlayMode.Background)
        '如果脚本未被加载过，先创建脚本窗体对象并设定脚本标识
        If ScriptForm(ScriptIndex) Is Nothing Then
            ScriptForm(ScriptIndex) = New WindowsTemplates
            ScriptForm(ScriptIndex).Tag = ScriptIndex.ToString("00")
        End If
        '脚本在关闭状态
        If Not (ScriptForm(ScriptIndex).Visible) And Not (ScriptFormVisible(ScriptIndex)) Then
            '高亮显示脚本对应桌面图标
            SenderControl = ScriptIcons(ScriptIndex)
            If HighLightIcon(ScriptIndex) Is Nothing Then
                HighLightIcon(ScriptIndex) = My.Resources.SystemAssets.ResourceManager.GetObject("ScriptIcon_" & SenderControl.Tag)
                IconGraphics = Graphics.FromImage(HighLightIcon(ScriptIndex))
                IconGraphics.DrawImage(My.Resources.SystemAssets.MouseEnter, 0, 0)
                IconGraphics.Dispose()
            End If
            SenderControl.Image = HighLightIcon(ScriptIndex)
            '显示脚本窗体并记录
            ScriptFormVisible(ScriptIndex) = True
            ScriptForm(ScriptIndex).Show(Me)
        End If
        '脚本在打开状态
        If ScriptForm(ScriptIndex).Visible And ScriptFormVisible(ScriptIndex) Then
            '图标右键菜单项设为可用
            MenuCloseScript.Enabled = True
            MenuSetToWallpaper.Enabled = True
            MenuBreath.Enabled = True
            '激活脚本窗体
            SetForegroundWindow(ScriptForm(ScriptIndex).Handle)
            ScriptForm(ScriptIndex).TopMost = False
        End If
    End Sub

    Private Sub IconTemplates_MouseEnter(sender As Object, e As EventArgs)
        '鼠标进入桌面图标，高亮显示图标
        SenderControl = CType(sender, Label)
        Dim ScriptIndex As Integer = Int(CType(sender, Label).Tag)
        If HighLightIcon(ScriptIndex) Is Nothing Then
            HighLightIcon(ScriptIndex) = My.Resources.SystemAssets.ResourceManager.GetObject("ScriptIcon_" & SenderControl.Tag)
            IconGraphics = Graphics.FromImage(HighLightIcon(ScriptIndex))
            IconGraphics.DrawImage(My.Resources.SystemAssets.MouseEnter, 0, 0)
            IconGraphics.Dispose()
        End If
        SenderControl.Image = HighLightIcon(ScriptIndex)
        '记录鼠标下图标的标识
        NowIconIndex = Int(SenderControl.Tag)
        '设置图标右键菜单项可用
        MenuCloseScript.Enabled = ScriptFormVisible(NowIconIndex)
        MenuSetToWallpaper.Enabled = ScriptFormVisible(NowIconIndex)
        MenuBreath.Enabled = ScriptFormVisible(NowIconIndex)
    End Sub

    Private Sub IconTemplates_MouseLeave(sender As Object, e As EventArgs)
        '鼠标离开图标时，如果脚本在关闭状态就取消高亮显示图标
        Dim ScriptIndex As Integer = Int(CType(sender, Label).Tag)
        'NowIconIndex = -1
        If Not (ScriptFormVisible(ScriptIndex)) Then SenderControl.Image = My.Resources.SystemAssets.ResourceManager.GetObject("ScriptIcon_" & SenderControl.Tag)
        If AeroPeekModel Then
            '遍历脚本窗体
            For ScriptIndex = 0 To ScriptUpperBound
                '还原在AeroPeek模式下被隐藏的窗体的透明度
                If ScriptFormVisible(ScriptIndex) Then ScriptForm(ScriptIndex).Opacity = WindowsTemplates.NegativeOpacity
            Next
            '关闭AeroPeek模式
            AeroPeekModel = False
            '被激活的窗体不被透明
            If Not (ActiveForm Is Nothing) Then If Not (ActiveForm Is CommandConsole) Then ActiveForm.Opacity = 1
        End If
    End Sub

    Private Sub IconTemplates_MouseUp(sender As Object, e As MouseEventArgs)
        '鼠标抬起
        SenderControl = CType(sender, Label)
        Dim ScriptIndex As Integer = Int(CType(sender, Label).Tag)
        If HighLightIcon(ScriptIndex) Is Nothing Then
            HighLightIcon(ScriptIndex) = My.Resources.SystemAssets.ResourceManager.GetObject("ScriptIcon_" & SenderControl.Tag)
            IconGraphics = Graphics.FromImage(HighLightIcon(ScriptIndex))
            IconGraphics.DrawImage(My.Resources.SystemAssets.MouseEnter, 0, 0)
            IconGraphics.Dispose()
        End If
        SenderControl.Image = HighLightIcon(ScriptIndex)
    End Sub
    Private Sub IconTemplates_MouseHover(sender As Object, e As EventArgs)
        '鼠标悬停时显示AeroPeek视图
        If Not (ScriptFormVisible(NowIconIndex)) Or ShutdownWindow.Visible Or AboutMeForm.Visible Then Exit Sub
        '如果脚本是打开状态，则开启AeroPeek模式
        AeroPeekModel = True
        Dim StartIndex, EndIndex, ScriptIndex As Integer
        EndIndex = NowIconIndex - 1
        StartIndex = NowIconIndex + 1
        ScriptForm(NowIconIndex).Opacity = 1
        '向前遍历隐藏脚本窗体
        For ScriptIndex = 0 To EndIndex
            If ScriptFormVisible(ScriptIndex) Then ScriptForm(ScriptIndex).Opacity = AeroPeekOpacity
        Next
        '向后遍历隐藏脚本窗体
        For ScriptIndex = StartIndex To ScriptUpperBound
            If ScriptFormVisible(ScriptIndex) Then ScriptForm(ScriptIndex).Opacity = AeroPeekOpacity
        Next
    End Sub

    Private Sub IconTemplates_MouseDown(sender As Object, e As MouseEventArgs)
        '鼠标按下，再次改变图标样式
        SenderControl = CType(sender, Label)
        Dim ScriptIndex As Integer = Int(CType(sender, Label).Tag)
        If MouseDownIcon(ScriptIndex) Is Nothing Then
            MouseDownIcon(ScriptIndex) = My.Resources.SystemAssets.ResourceManager.GetObject("ScriptIcon_" & SenderControl.Tag)
            IconGraphics = Graphics.FromImage(MouseDownIcon(ScriptIndex))
            IconGraphics.DrawImage(My.Resources.SystemAssets.MouseDown, 0, 0)
            IconGraphics.Dispose()
        End If
        SenderControl.Image = MouseDownIcon(ScriptIndex)
    End Sub

    Private Sub ShutdownButtonControl_Click(sender As Object, e As EventArgs) Handles ShutdownButtonControl.Click
        '点击关机按钮显示关机提示框
        ShowShutdownWindow()
    End Sub

    Public Sub ShowShutdownWindow()
        If ShutdowningUI.Visible Then Exit Sub
        '播放提示音
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("Tips"), AudioPlayMode.Background)
        '显示或激活关机提示框
        If Not (ShutdownWindow.Visible) Then ShutdownWindow.Show(Me)
        SetForegroundWindow(ShutdownWindow.Handle)
    End Sub

    '关机按钮响应鼠标动态效果
    Private Sub SystemWorkStation_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If Not SystemClosing Then
            '取消关闭消息
            e.Cancel = True
            '弹出关机提示框
            ShowShutdownWindow()
        End If
    End Sub

    Private Sub SettingButtonControl_Click(sender As Object, e As EventArgs) Handles SettingButtonControl.Click
        '播放提示音
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("Tips"), AudioPlayMode.Background)
        '显示或激活关于窗体
        If Not (AboutMeForm.Visible) Then AboutMeForm.Show(Me)
        SetForegroundWindow(AboutMeForm.Handle)
    End Sub

    Private Sub WorkStationWallpaperControl_MouseDown(sender As Object, e As MouseEventArgs) Handles WorkStationWallpaperControl.MouseDown
        '桌面按下左键开始拖动效果
        MouseDragForm.Location = e.Location
        MouseDownLocation = e.Location
        MouseDragForm.Size = New Size(0, 0)
        MouseDragForm.Visible = True
        '注册鼠标移动事件，开时监听鼠标移动
        AddHandler WorkStationWallpaperControl.MouseMove, AddressOf WorkStationWallpaperControl_MouseMove
    End Sub

    Private Sub WorkStationWallpaperControl_MouseMove(sender As Object, e As MouseEventArgs)
        '记录鼠标移动距离
        XDistance = e.X - MouseDownLocation.X
        YDistance = e.Y - MouseDownLocation.Y
        Dim DragArea As Rectangle
        '设置鼠标拖动区域
        If XDistance > 0 Then
            DragArea.X = MouseDownLocation.X
            DragArea.Width = XDistance
        Else
            DragArea.X = e.Location.X
            DragArea.Width = -XDistance
        End If
        If YDistance > 0 Then
            DragArea.Y = MouseDownLocation.Y
            DragArea.Height = YDistance
        Else
            DragArea.Y = e.Location.Y
            DragArea.Height = -YDistance
        End If
        '将上述区域应用到鼠标拖动窗体
        MouseDragForm.SetBounds(DragArea.X, DragArea.Y, DragArea.Width, DragArea.Height)
    End Sub

    Private Sub MenuSetToWallpaper_Click(sender As Object, e As EventArgs) Handles MenuSetToWallpaper.Click
        '桌面图标菜单之将脚本设置为桌面壁纸
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        WorkStationWallpaperControl.Image = My.Resources.SystemAssets.ResourceManager.GetObject("HackScript_" & NowIconIndex.ToString("00"))
    End Sub

    Private Sub MenuBreath_Click(sender As Object, e As EventArgs) Handles MenuBreath.Click
        '呼吸
        ScriptForm(NowIconIndex).Breath()
    End Sub

    Private Sub MenuCloseScript_Click(sender As Object, e As EventArgs) Handles MenuCloseScript.Click
        '桌面图标菜单之关闭脚本
        ScriptForm(NowIconIndex).Close()
    End Sub

    Private Sub MenuLastWallpaper_Click(sender As Object, e As EventArgs) Handles MenuLastWallpaper.Click
        '桌面右键菜单之上一张壁纸
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        WallpaperIndex += IIf(WallpaperIndex = 0, LockUI.WallpaperUpperBound, -1)
        WorkStationWallpaperControl.Image = My.Resources.SystemAssets.ResourceManager.GetObject("SystemWallpaper_" & WallpaperIndex.ToString("00"))
    End Sub

    Private Sub MenuNextWallpaper_Click(sender As Object, e As EventArgs) Handles MenuNextWallpaper.Click
        '桌面右键菜单之下一张壁纸
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        WallpaperIndex += IIf(WallpaperIndex = LockUI.WallpaperUpperBound, -LockUI.WallpaperUpperBound, 1)
        WorkStationWallpaperControl.Image = My.Resources.SystemAssets.ResourceManager.GetObject("SystemWallpaper_" & WallpaperIndex.ToString("00"))
    End Sub

    Private Sub MenuShutdown_Click(sender As Object, e As EventArgs) Handles MenuShutdown.Click
        '桌面右键菜单之关机
        ShowShutdownWindow()
    End Sub

    Private Sub PerformanceCounterTimer_Tick(sender As Object, e As EventArgs) Handles PerformanceCounterTimer.Tick
        MemoryUsageRate = (CmptInfo.TotalPhysicalMemory - CmptInfo.AvailablePhysicalMemory) / CmptInfo.TotalPhysicalMemory * 100
        '初始化目前已经上传和下载的字节
        DownloadSpeedCount = 0 : UploadSpeedCount = 0
        '遍历所有网卡
        For Index As Integer = LBoundOfArray To UBoundOfArray
            Try
                '累计所有网卡已经下载和上传的字节
                DownloadValue(Index) = DownloadCounter(Index).NextSample().RawValue
                UploadValue(Index) = UploadCounter(Index).NextSample().RawValue
                '每块网卡独立计算，防止禁用某网卡时出现速度为负值的情况，无符号整形变量冲突
                If DownloadValue(Index) > 0 Then DownloadSpeed(Index) = DownloadValue(Index) - DownloadValueOld(Index) Else DownloadSpeed(Index) = 0
                If UploadValue(Index) > 0 Then UploadSpeed(Index) = UploadValue(Index) - UploadValueOld(Index) Else UploadSpeed(Index) = 0
                '计算总的下载和上传速度
                DownloadSpeedCount += DownloadSpeed(Index)
                UploadSpeedCount += UploadSpeed(Index)
                '更新上次记录
                DownloadValueOld(Index) = DownloadValue(Index)
                UploadValueOld(Index) = UploadValue(Index)
            Catch ex As Exception
                DownloadValueOld(Index) = 0
                UploadValueOld(Index) = 0
            End Try
        Next

        '显示数据
        CPUCounterBar.Value = Int(CPUCounter.NextValue)
        MemoryUsageRateBar.Value = Int(MemoryUsageRate)
        DiskReadCounterLabel.Text = FormatSpeedString(DiskReadCounter.NextValue)
        DiskWriteCounterLabel.Text = FormatSpeedString(DiskWriteCounter.NextValue)
        UploadSpeedCountLabel.Text = FormatSpeedString(UploadSpeedCount)
        DownloadSpeedCountLabel.Text = FormatSpeedString(DownloadSpeedCount)
        DateTimeLabel.Text = My.Computer.Clock.LocalTime.ToLocalTime
    End Sub

    Private Sub WorkStationWallpaperControl_MouseUp(sender As Object, e As MouseEventArgs) Handles WorkStationWallpaperControl.MouseUp
        '桌面左键抬起，停止鼠标拖动
        MouseDragForm.Visible = False
        '卸载鼠标移动事件
        RemoveHandler WorkStationWallpaperControl.MouseMove, AddressOf WorkStationWallpaperControl_MouseMove
    End Sub

    Private Sub SystemWorkStation_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        '排列桌面图标需要的坐标变量
        Dim PointX, PointY As Integer
        '遍历桌面图标
        For Index = 0 To ScriptUpperBound
            '创建新的图标对象
            ScriptIcons(Index) = New Label
            '计算图标位置
            PointX = Int(Index / ColumnIconCount)
            PointY = Index - PointX * ColumnIconCount
            '设置图标属性
            With ScriptIcons(Index)
                '设置尺寸、透明背景色、关联图标右键菜单、设置父句柄、图像对齐位置、初始图像、文本对齐位置、图标文本、文本颜色
                .Size = New Size(IconWidth, IconHeight)
                .BackColor = Color.Transparent
                .ContextMenuStrip = IconMenuStrip
                .Parent = WorkStationWallpaperControl
                .ImageAlign = ContentAlignment.TopCenter
                .Image = My.Resources.SystemAssets.ResourceManager.GetObject("ScriptIcon_" & Index.ToString("00"))
                .TextAlign = ContentAlignment.BottomCenter
                .Text = ScriptInfomation(Index)
                .ForeColor = Color.Aqua
                .Font = ShutdownButtonControl.Font
                '设置图标指向的脚本标识和位置后显示图标
                .Tag = Index.ToString("00")
                .Left = IntervalDistance + PointX * (IconWidth + IntervalDistance)
                .Top = IntervalDistance + PointY * (IconHeight + IntervalDistance)
                .Show()
            End With
            '为图标注册点击和鼠标进入、悬停、离开、按下、抬起事件
            AddHandler ScriptIcons(Index).Click, AddressOf IconTemplates_MouseClick
            AddHandler ScriptIcons(Index).MouseEnter, AddressOf IconTemplates_MouseEnter
            AddHandler ScriptIcons(Index).MouseHover, AddressOf IconTemplates_MouseHover
            AddHandler ScriptIcons(Index).MouseLeave, AddressOf IconTemplates_MouseLeave
            AddHandler ScriptIcons(Index).MouseDown, AddressOf IconTemplates_MouseDown
            AddHandler ScriptIcons(Index).MouseUp, AddressOf IconTemplates_MouseUp
        Next

        '播放开机提示音效
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("LoginDesktop"), AudioPlayMode.Background)

        '遍历完成后记录图标显示区域最右边界
        RightestLoction = ScriptIcons(ScriptUpperBound).Left + IconWidth
        '遍历网卡
        For Index As Integer = LBoundOfArray To UBoundOfArray
            '初始化性能计数器
            DownloadCounter(Index) = New PerformanceCounter("Network Interface", "Bytes Received/sec", PCCategory.GetInstanceNames(Index))
            UploadCounter(Index) = New PerformanceCounter("Network Interface", "Bytes Sent/sec", PCCategory.GetInstanceNames(Index))
            '默认记录上次下载和上传的字节
            DownloadValueOld(Index) = DownloadCounter(Index).NextSample().RawValue
            UploadValueOld(Index) = UploadCounter(Index).NextSample().RawValue
        Next
        PerformanceCounterTimer.Enabled = True

        '开启语音识别
        Try
            MySpeechRecognitionEngine = New SpeechRecognitionEngine(SpeechRecognitionEngine.InstalledRecognizers().First)
            MySpeechRecognitionEngine.SetInputToDefaultAudioDevice()
            LoadGrammar()
            AddHandler MySpeechRecognitionEngine.AudioLevelUpdated, AddressOf SpeechRecognitionEngine_AudioLevelUpdated
            AddHandler MySpeechRecognitionEngine.SpeechRecognized, AddressOf SpeechRecognitionEngine_SpeechRecognized
            'AddHandler MySpeechRecognitionEngine.AudioStateChanged, AddressOf SpeechRecognitionEngine_AudioStateChanged
            MySpeechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple)
        Catch ex As Exception
            '开启语音识别失败
            VoiceLevelBar.Value = 0
            SpeechRecognitionMode = False
            SpeechButtonControl.Image = My.Resources.SystemAssets.MicroPhone_Off
            MySpeechRecognitionEngine.Dispose()

            If Not TipsForm.Visible Then TipsForm.Show(Me)
            TipsForm.PopupTips("Failed to start the", TipsForm.TipsIconType.Critical, "SpeechRecognitionEngine")
        End Try

        GetIPAndAddress()
    End Sub

    '点击IP和地址标签重新获取IP和地址
    Private Sub IPAndAddressLabel_Click(sender As Object, e As EventArgs) Handles IPLabel.Click, AddressLabel.Click
        GetIPAndAddress()
    End Sub

    '获取IP和真实地址
    Public Sub GetIPAndAddress()
        Try
            If (My.Computer.Network.Ping("ip.chinaz.com")) Then
                Dim IPWebClient As Net.WebClient = New Net.WebClient
                Dim WebString As String = vbNullString
                Dim RegIP As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex("\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}")
                IPWebClient.Encoding = System.Text.Encoding.UTF8
                WebString = IPWebClient.DownloadString(New Uri("http://ip.chinaz.com/getip.aspx"))
                IPLabel.Text = RegIP.Match(WebString).ToString
                AddressLabel.Text = Strings.Mid(WebString, IPLabel.Text.Length + 17, WebString.Length - IPLabel.Text.Length - 21)
                If Not IPWebClient Is Nothing Then IPWebClient.Dispose()
                If NotFirstGetIPAndAddress Then
                    If Not TipsForm.Visible Then TipsForm.Show(Me)
                    TipsForm.PopupTips("Successfully :", TipsForm.TipsIconType.Exclamation, "Get IP and address !")
                    Exit Sub
                End If
                NotFirstGetIPAndAddress = True
                Exit Sub
            End If
        Catch ex As Exception
        End Try

        IPLabel.Text = "127.0.0.1" : AddressLabel.Text = "Click to get."
        If NotFirstGetIPAndAddress Then
            If Not TipsForm.Visible Then TipsForm.Show(Me)
            TipsForm.PopupTips("Error :", TipsForm.TipsIconType.Exclamation, "Can't get IP and Address.")
            Exit Sub
        End If
        NotFirstGetIPAndAddress = True
    End Sub

    '格式化速度
    Private Function FormatSpeedString(ByVal LoadSpeed As ULong) As String
        If LoadSpeed < 1048576 Then
            Return [String].Format("{0:n} KB/S", LoadSpeed / 1024.0)
        Else
            Return [String].Format("{0:n} MB/S", LoadSpeed / 1048576)
        End If
    End Function

    Private Sub MenuTopMost_Click(sender As Object, e As EventArgs) Handles MenuTopMost.Click
        '改变窗体的置前和置后状态
        SetParent(Me.Handle, IIf(MenuTopMost.Checked, GetDesktopWindow(), GetDesktopIconHandle()))
        Me.TopMost = True
    End Sub

    ''' <summary>
    ''' 遍历顶级句柄下的所有WorkerW句柄和Progman句柄，查找桌面图标所在句柄
    ''' 两种桌面句柄结构：
    ''' ""#32769
    ''' ┝""WorkerW
    ''' ┃ ┕""SHELLDLL_DefView
    ''' ┃   ┕"FolderView"SysListView32 //即为所求
    ''' ┝"..."...
    ''' ┕"Program Manager"Progman
    '''   ┕""SHELLDLL_DefView
    '''     ┕"FolderView"SysListView32 //即为所求
    ''' </summary>
    ''' <returns>SysListView32类的句柄</returns>
    Private Function GetDesktopIconHandle() As IntPtr
        Dim HandleDesktop As Integer = GetDesktopWindow
        Dim HandleTop As Integer = 0
        Dim LastHandleTop As Integer = 0
        Dim HandleSHELLDLL_DefView As Integer = 0
        Dim HandleSysListView32 As Integer = 0
        '在WorkerW里搜索
        Do Until HandleSysListView32 > 0
            HandleTop = FindWindowEx(HandleDesktop, LastHandleTop, "WorkerW", vbNullString)
            HandleSHELLDLL_DefView = FindWindowEx(HandleTop, 0, "SHELLDLL_DefView", vbNullString)
            If HandleSHELLDLL_DefView > 0 Then HandleSysListView32 = FindWindowEx(HandleSHELLDLL_DefView, 0, "SysListView32", "FolderView")
            LastHandleTop = HandleTop
            If LastHandleTop = 0 Then Exit Do
        Loop
        '在Progman里搜索
        Do Until HandleSysListView32 > 0
            HandleTop = FindWindowEx(HandleDesktop, LastHandleTop, "Progman", "Program Manager")
            HandleSHELLDLL_DefView = FindWindowEx(HandleTop, 0, "SHELLDLL_DefView", vbNullString)
            If HandleSHELLDLL_DefView > 0 Then HandleSysListView32 = FindWindowEx(HandleSHELLDLL_DefView, 0, "SysListView32", "FolderView")
            LastHandleTop = HandleTop
            If LastHandleTop = 0 Then Exit Do : Return 0
        Loop
        Return HandleSysListView32
    End Function

    Private Sub XYBrowserButtonControl_Click(sender As Object, e As EventArgs) Handles XYBrowserButtonControl.Click
        '新建浏览器窗口
        LoadNewBrowser()
    End Sub

    Private Sub ConsoleButtonControl_Click(sender As Object, e As EventArgs) Handles ConsoleButtonControl.Click
        '显示控制台
        CommandConsole.ShowConsole()
    End Sub

    Public Sub LoadNewBrowser(Optional ByVal HomeURL As String = MainHomeURL)
        '加载新浏览器窗口
        Dim NewXYBrowser As Form = New XYBrowser
        '将需要打开的网址赋给新窗口
        NewXYBrowser.Tag = HomeURL
        '将新窗口添加进BrowserForms数组
        BrowserForms.Add(NewXYBrowser)
        '显示新的浏览器窗口
        NewXYBrowser.Show(Me)
        SetForegroundWindow(NewXYBrowser.Handle)
        NewXYBrowser.TopMost = False
    End Sub

    Private Sub MenuCustomWallpaper_Click(sender As Object, e As EventArgs) Handles MenuCustomWallpaper.Click
        '设置自定义壁纸
        If CustomWallpaperDialog.ShowDialog() = DialogResult.OK Then
            Try
                CustomWallpaperBitmap = Bitmap.FromFile(CustomWallpaperDialog.FileName)
                WorkStationWallpaperControl.Image = CustomWallpaperBitmap
            Catch ex As Exception
                If Not TipsForm.Visible Then TipsForm.Show(Me)
                TipsForm.PopupTips("Error reading image", TipsForm.TipsIconType.Critical, "Please select again.")
            End Try
        End If
    End Sub

    Private Sub MenuSetForecolor_Click(sender As Object, e As EventArgs) Handles MenuSetForecolor.Click
        '右键菜单设置系统字体颜色
        AboutMeForm.SetLabelForecolor()
    End Sub

    Private Sub SpeechButtonControl_Click(sender As Object, e As EventArgs) Handles SpeechButtonControl.Click
        '无法开启语音识别服务时，跳出过程
        Try
            If SpeechRecognitionMode Then
                '关闭语音识别
                MySpeechRecognitionEngine.RecognizeAsyncStop()
                VoiceLevelBar.Value = 0
                SpeechRecognitionMode = False
                SpeechButtonControl.Image = My.Resources.SystemAssets.MicroPhone_Off

                If Not TipsForm.Visible Then TipsForm.Show(Me)
                TipsForm.PopupTips("Shuted the", TipsForm.TipsIconType.Exclamation, "RecognitionEngine off.")
            Else
                '开启语音识别
                MySpeechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple)
                SpeechRecognitionMode = True
                SpeechButtonControl.Image = My.Resources.SystemAssets.MicroPhone_On

                If Not TipsForm.Visible Then TipsForm.Show(Me)
                TipsForm.PopupTips("Started the", TipsForm.TipsIconType.Infomation, "SpeechRecognitionEngine.")
            End If
        Catch ex As Exception
            If Not TipsForm.Visible Then TipsForm.Show(Me)
            TipsForm.PopupTips("Failed to start the", TipsForm.TipsIconType.Critical, "SpeechRecognitionEngine")
        End Try
    End Sub

    Private Sub XYMailButtonControl_Click(sender As Object, e As EventArgs) Handles XYMailButtonControl.Click
        '显示/隐藏发送邮件窗体
        If XYMail.Visible Then XYMail.Hide() Else XYMail.Show(Me)
    End Sub

    Private Sub SystemWorkStation_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        '不要使用Timer值守置前，因为会影响输入法的候选词窗体
        Me.TopMost = True
    End Sub

#Region "桌面右下角图标响应鼠标动态效果"
    Private Sub ButtonControl_MouseEnter(sender As Object, e As EventArgs) Handles XYBrowserButtonControl.MouseEnter, ConsoleButtonControl.MouseEnter, ShutdownButtonControl.MouseEnter, XYMailButtonControl.MouseEnter, SettingButtonControl.MouseEnter
        NowButton = CType(sender, Label)
        NowButton.Image = My.Resources.SystemAssets.ResourceManager.GetObject(NowButton.Tag & "1")
    End Sub

    Private Sub ButtonControl_MouseLeave(sender As Object, e As EventArgs) Handles XYBrowserButtonControl.MouseLeave, ConsoleButtonControl.MouseLeave, ShutdownButtonControl.MouseLeave, XYMailButtonControl.MouseLeave, SettingButtonControl.MouseLeave
        NowButton = CType(sender, Label)
        NowButton.Image = My.Resources.SystemAssets.ResourceManager.GetObject(NowButton.Tag & "0")
    End Sub

    Private Sub ButtonControl_MouseUp(sender As Object, e As MouseEventArgs) Handles XYBrowserButtonControl.MouseUp, ConsoleButtonControl.MouseUp, ShutdownButtonControl.MouseUp, XYMailButtonControl.MouseUp, SettingButtonControl.MouseUp
        NowButton = CType(sender, Label)
        NowButton.Image = My.Resources.SystemAssets.ResourceManager.GetObject(NowButton.Tag & "1")
    End Sub

    Private Sub ButtonControl_MouseDown(sender As Object, e As MouseEventArgs) Handles XYBrowserButtonControl.MouseDown, ConsoleButtonControl.MouseDown, ShutdownButtonControl.MouseDown, XYMailButtonControl.MouseDown, SettingButtonControl.MouseDown
        NowButton = CType(sender, Label)
        NowButton.Image = My.Resources.SystemAssets.ResourceManager.GetObject(NowButton.Tag & "2")
    End Sub

    '语音识别按钮是PictureBox类型，所以单独处理
    Private Sub SpeechButtonControl_MouseEnter(sender As Object, e As EventArgs) Handles SpeechButtonControl.MouseEnter
        SpeechButtonControl.BackgroundImage = My.Resources.SystemAssets.SpeechButton_1
    End Sub

    Private Sub SpeechButtonControl_MouseUp(sender As Object, e As MouseEventArgs) Handles SpeechButtonControl.MouseUp
        SpeechButtonControl.BackgroundImage = My.Resources.SystemAssets.SpeechButton_1
    End Sub

    Private Sub SpeechButtonControl_MouseLeave(sender As Object, e As EventArgs) Handles SpeechButtonControl.MouseLeave
        SpeechButtonControl.BackgroundImage = My.Resources.SystemAssets.SpeechButton_0
    End Sub

    Private Sub SpeechButtonControl_MouseDown(sender As Object, e As MouseEventArgs) Handles SpeechButtonControl.MouseDown
        SpeechButtonControl.BackgroundImage = My.Resources.SystemAssets.SpeechButton_2
    End Sub

#End Region

End Class