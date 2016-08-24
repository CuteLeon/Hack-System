Imports System.ComponentModel
Imports System.Speech.Recognition
Imports Microsoft.VisualBasic.Devices

Public Class SystemWorkStation

#Region "声明区"

    Public Declare Function SetForegroundWindow Lib "user32" Alias "SetForegroundWindow" (ByVal hwnd As Integer) As Integer
    Private Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    Private Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer
    Private Declare Function GetDesktopWindow Lib "user32" Alias "GetDesktopWindow" () As IntPtr
    Private Declare Function SetParent Lib "user32" Alias "SetParent" (ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As Integer
    Private Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As IntPtr
    Private Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As IntPtr, ByVal nIndex As Integer) As Integer
    Private Declare Function SetLayeredWindowAttributes Lib "user32" (ByVal hwnd As IntPtr, ByVal crKey As Integer, ByVal bAlpha As Integer, ByVal dwFlags As Integer) As Integer

    Public Const AeroPeekOpacity As Double = 0.15 'AeroPeek视图时，未激活的脚本窗口的透明度
    Public Const ScriptUpperBound As Int16 = 22 '脚本窗体数组的下标
    Public Const IconWidth As Integer = 65 '桌面图标的宽度(不适用Size类型：结构体类型无法定义为常量)
    Public Const IconHeight As Integer = 90 '桌面图标的高度
    Public Const MainHomeURL As String = "https://www.zoomeye.org/" ''浏览器默认主页 //("http://wwwwwwwww.jodi.org" 是一个很奇怪的网站)
    Private Const WallpaperUpperBound As Int16 = 18 '桌面壁纸数组的下标

    '以默认语言创建语音识别引擎
    Public MySpeechRecognitionEngine As SpeechRecognitionEngine
    '桌面图标对应的文本
    Public ScriptInfomation() As String = {"Digital Rain", "Network Attack", "Air Defence", "Iron Man", "Attack Data", "3D Map", "Ballistic Missile", "Missile", "Action Indication", "Zone Isolation", "Waiting...", "Life Support", "Agent Info.", "Graphic SO", "Face 3DModel", "Driving System", "Thinking Export", "ARToolkit", "Combat", "UAV Camera", "NOVA 6", "Satellite", "Decrypt"}
    '桌面图标(脚本)对应的语音命令
    Public ScriptSpeechGrammar() As String = {"打开数字雨", "打开网络攻击", "打开防空系统", "打开钢铁侠", "打开攻击数据", "打开三维地图", "打开弹道导弹", "打开导弹部署", "打开行动指示", "打开区域隔离", "打开等待连接", "打开生命维护系统", "打开特工信息", "打开示波器", "打开面部模型", "打开驱动系统", "打开思维导出系统", "打开增强现实", "打开作战部署", "打开无人机", "打开新星", "打开近地卫星", "打开解密"}
    Public AeroPeekModel As Boolean 'AeroPeek视图的开关
    Public ScriptForm(ScriptUpperBound) As WindowsTemplates '脚本窗口
    Public BrowserForms As New ArrayList  '开启的浏览器窗口
    Public ScriptFormVisible(ScriptUpperBound) As Boolean '脚本开启状态(不同于Form.Visible，两者配合判断脚本的状态)
    Public ScriptIcons(ScriptUpperBound) As Label
    Public NowIconIndex As Integer = -1 '鼠标进入的桌面图标标识(鼠标离开为-1)
    Public RightestLoction As Integer '最右图标的横坐标(作为脚本随机位置的左界)
    Public SystemClosing As Boolean '系统正在关闭
    Public SpeechRecognitionMode As Boolean = True '语音识别引擎的状态

    Dim UserNameFont As Font = New Font("微软雅黑", 36.0) '用户名显示字体
    Dim MouseDownLocation As Point '鼠标按下的坐标(用于显示桌面拖动蓝色矩形)
    Dim XDistance, YDistance As Integer '鼠标按下后移动的距离
    Dim ColumnIconCount As Integer = Int(My.Computer.Screen.Bounds.Height / IconHeight) - 1 '计算桌面每列显示图标的数量
    Dim IntervalDistance As Integer = (My.Computer.Screen.Bounds.Height - ColumnIconCount * IconHeight) / (ColumnIconCount + 1) '两个桌面图标间的距离
    Dim WallpaperIndex As Integer = 9 '初始桌面壁纸的标识
    Dim CustomWallpaperBitmap As Bitmap '用户自定义壁纸
    Dim CustomWallpaperString As String '用户自定义壁纸-Base64字符串
    Dim HighLightIcon(ScriptUpperBound) As Bitmap '高亮的桌面图标
    Dim MouseDownIcon(ScriptUpperBound) As Bitmap '鼠标按下的桌面图标
    Dim LabelForecolor As Color = Color.Aqua '初始文本标签颜色
    Dim IconGraphics As Graphics '桌面图标画笔
    Dim SenderControl As Label '用于记录触发事件的控件。
    Dim CPUCounter As New PerformanceCounter("Processor", "% Processor Time", "_Total") 'CPU性能计数器
    Dim DiskReadCounter As New PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", "_Total") '内存性能计数器
    Dim DiskWriteCounter As New PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total") '硬盘性能计数器
    Dim PCCategory As New PerformanceCounterCategory("Network Interface") '网络性能计数器
    Dim CmptInfo As New ComputerInfo() '计算机信息
    Dim MemoryUsageRate As Integer '内存使用率
    '为每块网卡创建单独的性能计数器，防止某块网卡突然仅用时，网速出现负数情况
    Dim UBoundOfPCCategory As UInteger = PCCategory.GetInstanceNames.Count - 1 '网卡数组的下标
    Dim DownloadCounter(UBoundOfPCCategory) As PerformanceCounter
    Dim UploadCounter(UBoundOfPCCategory) As PerformanceCounter
    Dim DownloadSpeed(UBoundOfPCCategory) As ULong, UploadSpeed(UBoundOfPCCategory) As ULong
    Dim DownloadValue(UBoundOfPCCategory) As ULong, UploadValue(UBoundOfPCCategory) As ULong
    Dim DownloadValueOld(UBoundOfPCCategory) As ULong, UploadValueOld(UBoundOfPCCategory) As ULong
    Dim DownloadSpeedCount As ULong, UploadSpeedCount As ULong
#End Region

#Region "窗体"

    Private Sub SystemWorkStation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '全屏显示
        Me.Location = New Point(0, 0)
        Me.Size = My.Computer.Screen.Bounds.Size
        '设置桌面蓝色拖拽矩形的样式
        Call SetParent(MouseDragForm.Handle, Me.Handle)
        Call SetWindowLong(MouseDragForm.Handle, -20, GetWindowLong(MouseDragForm.Handle, -20) Or &H80000)
        Call SetLayeredWindowAttributes(MouseDragForm.Handle, 0, 100, 2)
        '设置右上角数据的位置
        InfoTitle.Location = New Point(Me.Width - InfoTitle.Width - DiskReadCounterLabel.Width - 12, 38)
        CPUCounterBar.Location = New Point(InfoTitle.Right + 4, InfoTitle.Top + 4)
        MemoryUsageRateBar.Location = New Point(InfoTitle.Right + 4, CPUCounterBar.Bottom + 8)
        DiskReadCounterLabel.Location = New Point(InfoTitle.Right, 78)
        DiskWriteCounterLabel.Location = New Point(InfoTitle.Right, 98)
        UploadSpeedCountLabel.Location = New Point(InfoTitle.Right, 118)
        DownloadSpeedCountLabel.Location = New Point(InfoTitle.Right, 138)
        PowerLineLabel.Location = New Point(InfoTitle.Right, 158)
        BatteryStatusLabel.Location = New Point(InfoTitle.Right, 178)
        BatteryPercentLabel.Location = New Point(InfoTitle.Right, 198)
        IPLabel.Location = New Point(InfoTitle.Right, 218)
        AddressLabel.Location = New Point(InfoTitle.Right, 238)
        '设置右下角功能图标的位置
        VoiceLevelBar.Parent = SpeechButtonControl
        ShutdownButtonControl.Location = New Point(Me.Width - ShutdownButtonControl.Width - IntervalDistance, Me.Height - IntervalDistance - ShutdownButtonControl.Height)
        SettingButtonControl.Location = New Point(ShutdownButtonControl.Left - IntervalDistance - SettingButtonControl.Width, ShutdownButtonControl.Top)
        XYBrowserButtonControl.Location = New Point(SettingButtonControl.Left - IntervalDistance - XYBrowserButtonControl.Width, SettingButtonControl.Top)
        XYMailButtonControl.Location = New Point(XYBrowserButtonControl.Left - IntervalDistance - XYMailButtonControl.Width, XYBrowserButtonControl.Top)
        ConsoleButtonControl.Location = New Point(XYMailButtonControl.Left - IntervalDistance - ConsoleButtonControl.Width, XYMailButtonControl.Top)
        SpeechButtonControl.Location = New Point(ConsoleButtonControl.Left - IntervalDistance - SpeechButtonControl.Width, ConsoleButtonControl.Top)
        VoiceLevelBar.Location = New Point(5, SpeechButtonControl.Height - 12)
        '设置桌面控件鼠标样式
        Me.Cursor = StartingUpUI.SystemCursor
        InfoTitle.Cursor = StartingUpUI.SystemCursor
        CPUCounterBar.Cursor = StartingUpUI.SystemCursor
        MemoryUsageRateBar.Cursor = StartingUpUI.SystemCursor
        DiskReadCounterLabel.Cursor = StartingUpUI.SystemCursor
        DiskWriteCounterLabel.Cursor = StartingUpUI.SystemCursor
        UploadSpeedCountLabel.Cursor = StartingUpUI.SystemCursor
        DownloadSpeedCountLabel.Cursor = StartingUpUI.SystemCursor
        DateTimeLabel.Cursor = StartingUpUI.SystemCursor
        '把IP和地址的鼠标设置为"手"的形状，提示用户可以点击
        IPLabel.Cursor = Cursors.Hand
        AddressLabel.Cursor = Cursors.Hand

        '尝试从存档中读取保存的自定义壁纸数据
        CustomWallpaperString = My.Settings.CustomWallpaper
        If Not CustomWallpaperString = vbNullString Then
            CustomWallpaperBitmap = LoginAndLockUI.StringToBitmap(CustomWallpaperString)
        End If
        '尝试从存档中读取保存的桌面壁纸标识
        Dim WallpaperIndexSetting As String = My.Settings.DesktopWallpaperIndex
        If Not WallpaperIndexSetting = vbNullString Then
            WallpaperIndex = Int(WallpaperIndexSetting)
            If WallpaperIndex = -1 Then
                '当壁纸标识为-1(即自定义壁纸)且自定义壁纸不为空时将自定义壁纸设置为桌面壁纸
                If CustomWallpaperBitmap IsNot Nothing Then Me.BackgroundImage = CustomWallpaperBitmap
            Else
                '否则显示相应标识的桌面壁纸
                Me.BackgroundImage = My.Resources.SystemAssets.ResourceManager.GetObject("SystemWallpaper_" & WallpaperIndex.ToString("00"))
            End If
        End If

        '启动、初始化控制台窗体并将其隐藏在屏幕右侧
        CommandConsole.Show(Me)
        CommandConsole.Height = My.Computer.Screen.Bounds.Height
        CommandConsole.Location = New Point(My.Computer.Screen.Bounds.Width, 0)
        CommandConsole.CommandPast.Height = My.Computer.Screen.Bounds.Height - 120
        CommandConsole.CommandTip.Top = My.Computer.Screen.Bounds.Height - 100
        CommandConsole.CommandInputBox.Top = My.Computer.Screen.Bounds.Height - 40
        '设置自定义壁纸选取控件的初始路径为系统图库路径
        CustomImageDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)

        '考虑到用户时间和日期的格式问题，需要自适应标签大小，防止数据显示不全
        DateTimeLabel.AutoSize = True
        DateTimeLabel.Text = My.Computer.Clock.LocalTime.ToLocalTime
        DateTimeLabel.Location = New Point(Me.Width - DateTimeLabel.Width - 12, 12)
    End Sub

    Public Sub SystemWorkStation_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        '其他几个控件也会绑定此事件
        If LoginAndLockUI.Visible Then Exit Sub '锁屏时无效

        Dim KeyAscii As Integer = Asc(e.KeyChar) '获取键盘Asc码
        If KeyAscii = 27 Then
            '按下Esc弹出关机提示框
            ShowShutdownWindow()
        ElseIf KeyAscii = 96 Or KeyAscii = -24156 Then
            '按下~键，显示控制台
            If Not CommandConsole.Visible Then CommandConsole.Show(Me)
            CommandConsole.ShowConsole()
        Else
            '其它按键Asc码对脚本总数求余当做脚本标识，加载脚本
            KeyAscii = KeyAscii Mod (ScriptUpperBound + 1)
            LoadScript(KeyAscii)
        End If
    End Sub

    Private Sub SystemWorkStation_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If Not SystemClosing Then
            '系统不是用户正规操作关机时拦截关闭消息并弹出关机提示框
            e.Cancel = True
            ShowShutdownWindow()
        End If
    End Sub

    Private Sub SystemWorkStation_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        '桌面按下左键开始拖动效果，先记录鼠标按下的坐标
        MouseDragForm.Location = e.Location
        MouseDownLocation = e.Location
        MouseDragForm.Size = New Size(0, 0)
        MouseDragForm.Visible = True
        '注册鼠标移动事件，开时监听鼠标移动
        AddHandler Me.MouseMove, AddressOf SystemWorkStation_MouseMove
    End Sub

    Private Sub SystemWorkStation_MouseMove(sender As Object, e As MouseEventArgs)
        '记录鼠标移动距离
        XDistance = e.X - MouseDownLocation.X
        YDistance = e.Y - MouseDownLocation.Y
        '设置鼠标拖动区域，需要考虑鼠标移动到按下坐标的左侧和右侧两种情况
        Dim DragArea As Rectangle
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

    Private Sub SystemWorkStation_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        '桌面左键抬起，停止鼠标拖动
        MouseDragForm.Visible = False
        '卸载鼠标移动事件
        RemoveHandler Me.MouseMove, AddressOf SystemWorkStation_MouseMove
    End Sub

    Private Sub SystemWorkStation_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Load()之后执行
        '加载桌面图标
        Dim PointX, PointY As Integer
        For Index = 0 To ScriptUpperBound
            ScriptIcons(Index) = New Label '创建新图标控件
            PointX = Int(Index / ColumnIconCount) '计算坐标
            PointY = Index - PointX * ColumnIconCount
            With ScriptIcons(Index) '设置众多属性
                .Size = New Size(IconWidth, IconHeight)
                .Parent = Me
                .BackColor = Color.Transparent
                .ContextMenuStrip = IconMenuStrip
                .ImageAlign = ContentAlignment.TopCenter
                .Image = My.Resources.SystemAssets.ResourceManager.GetObject("ScriptIcon_" & Index.ToString("00"))
                .TextAlign = ContentAlignment.BottomCenter
                .Text = ScriptInfomation(Index)
                .Font = ShutdownButtonControl.Font
                .Tag = Index.ToString("00") '储存图标对应的标识！
                .Left = IntervalDistance + PointX * (IconWidth + IntervalDistance)
                .Top = IntervalDistance + PointY * (IconHeight + IntervalDistance)
                .Show() '设置完毕，显示图标
            End With

            '为桌面图标绑定事件
            AddHandler ScriptIcons(Index).Click, AddressOf IconTemplates_Click
            AddHandler ScriptIcons(Index).MouseEnter, AddressOf IconTemplates_MouseEnter
            AddHandler ScriptIcons(Index).MouseHover, AddressOf IconTemplates_MouseHover
            AddHandler ScriptIcons(Index).MouseLeave, AddressOf IconTemplates_MouseLeave
            AddHandler ScriptIcons(Index).MouseDown, AddressOf IconTemplates_MouseDown
            AddHandler ScriptIcons(Index).MouseUp, AddressOf IconTemplates_MouseUp
        Next

        '尝试从存档中读取保存的自定义标签文本颜色(RGB值间用","隔开)
        Dim ForeColorCell() As String = My.Settings.ForeColor.Split(",")
        If ForeColorCell.Count = 3 Then
            LabelForecolor = Color.FromArgb(Int(ForeColorCell(0)), Int(ForeColorCell(1)), Int(ForeColorCell(2)))
        End If
        '调用方法设置桌面标签的文本颜色
        SetLabelForecolor(LabelForecolor)

        '播放开机音效
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("LoginDesktop"), AudioPlayMode.Background)

        '记录最右桌面图标的横坐标
        RightestLoction = ScriptIcons(ScriptUpperBound).Left + IconWidth

        '初始化网卡计数器
        For Index As Integer = 0 To UBoundOfPCCategory
            DownloadCounter(Index) = New PerformanceCounter("Network Interface", "Bytes Received/sec", PCCategory.GetInstanceNames(Index))
            UploadCounter(Index) = New PerformanceCounter("Network Interface", "Bytes Sent/sec", PCCategory.GetInstanceNames(Index))
            DownloadValueOld(Index) = DownloadCounter(Index).NextSample().RawValue
            UploadValueOld(Index) = UploadCounter(Index).NextSample().RawValue
        Next
        '开启性能计数器"引擎"
        PerformanceCounterTimer.Enabled = True

        '尝试开启语音识别引擎(不存在录音设备时会出错，需要容错处理)
        Try
            MySpeechRecognitionEngine = New SpeechRecognitionEngine(SpeechRecognitionEngine.InstalledRecognizers().First)
            MySpeechRecognitionEngine.SetInputToDefaultAudioDevice()
            LoadGrammar() '为语音引擎导入语法
            '为语音引擎绑定事件
            AddHandler MySpeechRecognitionEngine.AudioLevelUpdated, AddressOf SpeechRecognitionEngine_AudioLevelUpdated
            AddHandler MySpeechRecognitionEngine.SpeechRecognized, AddressOf SpeechRecognitionEngine_SpeechRecognized
            'AddHandler MySpeechRecognitionEngine.AudioStateChanged, AddressOf SpeechRecognitionEngine_AudioStateChanged
            '开启语音识别引擎
            MySpeechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple)
        Catch ex As Exception
            '开启语音室备引擎失败，善后处理
            VoiceLevelBar.Value = 0
            SpeechRecognitionMode = False
            SpeechButtonControl.Image = My.Resources.SystemAssets.MicroPhone_Off
            MySpeechRecognitionEngine.Dispose()
        End Try

        '首次获取IP和地址
        GetIPAndAddress(True)
    End Sub

    Private Sub SystemWorkStation_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        '不要使用Timer值守置前，因为会影响输入法的候选词窗体
        Me.TopMost = MenuTopMost.Checked
    End Sub

#End Region

#Region "控件"

#Region "性能计数器区域"

    Private Sub PerformanceCounterTimer_Tick(sender As Object, e As EventArgs) Handles PerformanceCounterTimer.Tick
        '计算内存使用率
        MemoryUsageRate = (CmptInfo.TotalPhysicalMemory - CmptInfo.AvailablePhysicalMemory) / CmptInfo.TotalPhysicalMemory * 100
        '初始化总下载和上传速度
        DownloadSpeedCount = 0 : UploadSpeedCount = 0
        '遍历网卡性能计数器
        For Index As Integer = 0 To UBoundOfPCCategory
            Try '网卡被突然禁用后会出错，需要容错处理
                DownloadValue(Index) = DownloadCounter(Index).NextSample().RawValue
                UploadValue(Index) = UploadCounter(Index).NextSample().RawValue
                If DownloadValue(Index) > 0 Then DownloadSpeed(Index) = DownloadValue(Index) - DownloadValueOld(Index) Else DownloadSpeed(Index) = 0
                If UploadValue(Index) > 0 Then UploadSpeed(Index) = UploadValue(Index) - UploadValueOld(Index) Else UploadSpeed(Index) = 0
                DownloadSpeedCount += DownloadSpeed(Index)
                UploadSpeedCount += UploadSpeed(Index)
                DownloadValueOld(Index) = DownloadValue(Index)
                UploadValueOld(Index) = UploadValue(Index)
            Catch ex As Exception
                '出错时善后处理
                DownloadValueOld(Index) = 0
                UploadValueOld(Index) = 0
            End Try
        Next
        '使用进度条显示CPU和内存性能
        CPUCounterBar.Value = Int(CPUCounter.NextValue)
        MemoryUsageRateBar.Value = Int(MemoryUsageRate)
        '显示硬盘读写速度和网络上传下载速度
        DiskReadCounterLabel.Text = FormatSpeedString(DiskReadCounter.NextValue)
        DiskWriteCounterLabel.Text = FormatSpeedString(DiskWriteCounter.NextValue)
        UploadSpeedCountLabel.Text = FormatSpeedString(UploadSpeedCount)
        DownloadSpeedCountLabel.Text = FormatSpeedString(DownloadSpeedCount)
        '显示时间
        DateTimeLabel.Text = My.Computer.Clock.LocalTime.ToLocalTime
        '显示电源线、充电、电源状态
        PowerLineLabel.Text = SystemInformation.PowerStatus.PowerLineStatus.ToString
        BatteryStatusLabel.Text = SystemInformation.PowerStatus.BatteryChargeStatus.ToString
        BatteryPercentLabel.Text = SystemInformation.PowerStatus.BatteryLifePercent * 100 & "%"

        '强制每秒回收内存
        GC.Collect()
    End Sub

    Private Sub IPAndAddressLabel_Click(sender As Object, e As EventArgs) Handles IPLabel.Click, AddressLabel.Click
        '点击IP和地址标签可以重新获取IP和地址
        GetIPAndAddress()
    End Sub
#End Region

#Region "语音引擎"

    Private Sub SpeechRecognitionEngine_SpeechRecognized(sender As Object, e As SpeechRecognizedEventArgs)
        If LoginAndLockUI.Visible Then
            If e.Result.Text = "解锁" Then LoginAndLockUI.HideLockScreen(True)
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
                LockScreen()
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

    Private Sub SpeechRecognitionEngine_AudioLevelUpdated(sender As Object, e As AudioLevelUpdatedEventArgs)
        Static LastVoiceLevel As Int16 = 0
        VoiceLevelBar.Value = e.AudioLevel
        If e.AudioLevel > 1 Then
            If Not MicroPhoneTimer.Enabled Then MicroPhoneTimer.Start()
        Else
            If LastVoiceLevel = 0 AndAlso MicroPhoneTimer.Enabled Then
                MicroPhoneTimer.Stop()
                SpeechButtonControl.Image = My.Resources.SystemAssets.MicroPhone_On
            End If
        End If
        LastVoiceLevel = e.AudioLevel
    End Sub

    'Private Sub SpeechRecognitionEngine_AudioStateChanged(sender As Object, e As AudioStateChangedEventArgs)
    '    If e.AudioState = AudioState.Silence Then
    '        Debug.Print("识别引擎开启")
    '    ElseIf e.AudioState = AudioState.Speech Then
    '        Debug.Print("引擎识别到结果")
    '    ElseIf e.AudioState = AudioState.Stopped Then
    '        Debug.Print("识别引擎关闭")
    '    End If
    'End Sub
#End Region

#Region "桌面图标"

    Private Sub IconTemplates_Click(sender As Object, e As MouseEventArgs)
        '点击图标加载对应的脚本窗口
        Dim ScriptIndex As Integer = Int(CType(sender, Label).Tag)
        LoadScript(ScriptIndex)
    End Sub

    Private Sub IconTemplates_MouseEnter(sender As Object, e As EventArgs)
        '鼠标进入桌面图标，高亮显示图标
        SenderControl = CType(sender, Label)
        Dim ScriptIndex As Integer = Int(CType(sender, Label).Tag)
        '不重复绘制已经存在的高亮图标，减少计算
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
        MenuBreath.Enabled = ScriptFormVisible(NowIconIndex)
    End Sub

    Private Sub IconTemplates_MouseLeave(sender As Object, e As EventArgs)
        '鼠标离开图标时，如果脚本在关闭状态就取消高亮显示图标
        Dim ScriptIndex As Integer = Int(CType(sender, Label).Tag)
        'NowIconIndex = -1
        If Not (ScriptFormVisible(ScriptIndex)) Then SenderControl.Image = My.Resources.SystemAssets.ResourceManager.GetObject("ScriptIcon_" & SenderControl.Tag)
        If AeroPeekModel Then
            '遍历脚本窗体，还原在AeroPeek模式下被透明的窗体的透明度
            For ScriptIndex = 0 To ScriptUpperBound
                If ScriptFormVisible(ScriptIndex) Then ScriptForm(ScriptIndex).Opacity = WindowsTemplates.NegativeOpacity
            Next
            '关闭AeroPeek模式
            AeroPeekModel = False
            '被激活的窗体不被透明
            If Not (ActiveForm Is Nothing) Then If Not (ActiveForm Is CommandConsole) Then ActiveForm.Opacity = 1
        End If
    End Sub

    Private Sub IconTemplates_MouseUp(sender As Object, e As MouseEventArgs)
        '鼠标抬起，显示高亮图标，同MouseEnter()
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
        '鼠标悬停时如果脚本是打开状态，则显示AeroPeek视图
        If Not (ScriptFormVisible(NowIconIndex)) Or ShutdownTips.Visible Or AboutMeForm.Visible Then Exit Sub

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
        '不重复绘制按下的桌面图标，减少计算
        If MouseDownIcon(ScriptIndex) Is Nothing Then
            MouseDownIcon(ScriptIndex) = My.Resources.SystemAssets.ResourceManager.GetObject("ScriptIcon_" & SenderControl.Tag)
            IconGraphics = Graphics.FromImage(MouseDownIcon(ScriptIndex))
            IconGraphics.DrawImage(My.Resources.SystemAssets.MouseDown, 0, 0)
            IconGraphics.Dispose()
        End If
        SenderControl.Image = MouseDownIcon(ScriptIndex)
    End Sub
#End Region

#Region "菜单项"

    Private Sub MenuBreath_Click(sender As Object, e As EventArgs) Handles MenuBreath.Click
        '脚本窗体呼吸功能
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        ScriptForm(NowIconIndex).Breath()
    End Sub

    Private Sub MenuCloseScript_Click(sender As Object, e As EventArgs) Handles MenuCloseScript.Click
        '桌面图标菜单之关闭脚本
        ScriptForm(NowIconIndex).Close()
    End Sub

    Private Sub MenuLastWallpaper_Click(sender As Object, e As EventArgs) Handles MenuLastWallpaper.Click
        '桌面右键菜单之上一张壁纸
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        If WallpaperIndex = 0 AndAlso (CustomWallpaperBitmap IsNot Nothing) Then
            '允许显示用户自定义壁纸
            WallpaperIndex = -1
            Me.BackgroundImage = CustomWallpaperBitmap
        Else
            WallpaperIndex = IIf(WallpaperIndex <= 0, WallpaperUpperBound, WallpaperIndex - 1)
            Me.BackgroundImage = My.Resources.SystemAssets.ResourceManager.GetObject("SystemWallpaper_" & WallpaperIndex.ToString("00"))
        End If
        '保存壁纸标识
        My.Settings.DesktopWallpaperIndex = WallpaperIndex
        My.Settings.Save()
    End Sub

    Private Sub MenuNextWallpaper_Click(sender As Object, e As EventArgs) Handles MenuNextWallpaper.Click
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        '桌面右键菜单之下一张壁纸'
        If WallpaperIndex = WallpaperUpperBound AndAlso (CustomWallpaperBitmap IsNot Nothing) Then
            '允许显示用户自定义壁纸
            WallpaperIndex = -1
            Me.BackgroundImage = CustomWallpaperBitmap
        Else
            WallpaperIndex = IIf(WallpaperIndex = WallpaperUpperBound Or WallpaperIndex = -1, 0, WallpaperIndex + 1)
            Me.BackgroundImage = My.Resources.SystemAssets.ResourceManager.GetObject("SystemWallpaper_" & WallpaperIndex.ToString("00"))
        End If
        '保存壁纸标识
        My.Settings.DesktopWallpaperIndex = WallpaperIndex
        My.Settings.Save()
    End Sub

    Private Sub MenuLockScreen_Click(sender As Object, e As EventArgs) Handles MenuLockScreen.Click
        '锁屏
        LockScreen()
    End Sub

    Private Sub MenuShutdown_Click(sender As Object, e As EventArgs) Handles MenuShutdown.Click
        '桌面右键菜单之关机
        ShowShutdownWindow()
    End Sub

    Private Sub MenuTopMost_Click(sender As Object, e As EventArgs) Handles MenuTopMost.Click
        '改变系统的置前和置后状态
        SetParent(Me.Handle, IIf(MenuTopMost.Checked, GetDesktopWindow(), GetDesktopIconHandle()))
        Me.TopMost = MenuTopMost.Checked
    End Sub

    Private Sub MenuCustomWallpaper_Click(sender As Object, e As EventArgs) Handles MenuCustomWallpaper.Click
        '设置自定义壁纸
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        '启动自定义壁纸选取对话框
        If CustomImageDialog.ShowDialog() = DialogResult.OK Then
            Try '我为了方便，给过滤器加了"*.*"，但是啊，有些用户，Ta贱！非得作死，比如故意选取一个txt文件。需要容错处理！
                CustomWallpaperBitmap = Bitmap.FromFile(CustomImageDialog.FileName)
                Me.BackgroundImage = CustomWallpaperBitmap
                If Not TipsForm.Visible Then TipsForm.Show(Me)
                '弹出提示浮窗
                TipsForm.PopupTips("Successfully !", TipsForm.TipsIconType.Infomation, "Set wallpaper successfully")
                WallpaperIndex = -1
                '保存壁纸标识和自定义壁纸的Base64编码到存档
                My.Settings.DesktopWallpaperIndex = WallpaperIndex
                My.Settings.CustomWallpaper = BitmapToString(CustomWallpaperBitmap)
                My.Settings.Save()
            Catch ex As Exception
                '出错时弹出提示浮窗
                If Not TipsForm.Visible Then TipsForm.Show(Me)
                TipsForm.PopupTips("Error reading image", TipsForm.TipsIconType.Critical, "Please select again.")
            End Try
        Else
            My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        End If
    End Sub

    Private Sub MenuSetForecolor_Click(sender As Object, e As EventArgs) Handles MenuSetForecolor.Click
        '右键菜单设置系统字体颜色
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        '弹出颜色设置对话框
        If LabelColorDialog.ShowDialog = DialogResult.OK Then
            LabelForecolor = LabelColorDialog.Color
            SetLabelForecolor(LabelForecolor)
            '保存自定义颜色到存档
            My.Settings.ForeColor = LabelForecolor.R & "," & LabelForecolor.G & "," & LabelForecolor.B
            My.Settings.Save()
        Else
            My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        End If
    End Sub

    Private Sub MenuSetUserHead_Click(sender As Object, e As EventArgs) Handles MenuSetUserHead.Click
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        '弹出用户头像文件选取对话框
        If (CustomImageDialog.ShowDialog = DialogResult.OK) Then
            Try '同上，防止用户找茬设置容错处理
                '将图像裁剪成圆形，装载到登录框里
                LoginAndLockUI.UserHead = MakeCircularBitmap(Bitmap.FromFile(CustomImageDialog.FileName), LoginAndLockUI.HeadSize)
                LoginAndLockUI.HeadPictureBox.BackgroundImage = LoginAndLockUI.UserHead
                '头像转换为Base64编码后存进存档
                LoginAndLockUI.UserHeadString = BitmapToString(LoginAndLockUI.UserHead)
                My.Settings.UserHead = LoginAndLockUI.UserHeadString
                My.Settings.Save()
                '弹出提示浮窗
                If Not TipsForm.Visible Then TipsForm.Show(Me)
                TipsForm.PopupTips("Successfully !", TipsForm.TipsIconType.Infomation, "Set user head successfully")
            Catch ex As Exception
                '出错时弹出提示浮窗
                If Not TipsForm.Visible Then TipsForm.Show(Me)
                TipsForm.PopupTips("Error reading image", TipsForm.TipsIconType.Critical, "Please select again.")
            End Try
        Else
            My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        End If
    End Sub

    Private Sub MenuUserName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MenuUserName.KeyPress
        '在菜单里敲击回车键时设置用户名
        If e.KeyChar = Chr(Keys.Enter) Then
            '用户名未改变不处理
            If LoginAndLockUI.UserName = MenuUserName.Text Then Exit Sub
            '用户名背后阴影的模糊半径
            Dim ShadowRadius As Integer = 10
            Dim DoubleRadius As Integer = ShadowRadius * 2
            LoginAndLockUI.UserName = MenuUserName.Text
            LoginAndLockUI.UserNameControl.AutoSize = True
            LoginAndLockUI.UserNameControl.Font = UserNameFont
            LoginAndLockUI.UserNameControl.Text = MenuUserName.Text
            '绘制用户名的描边加阴影图像
            LoginAndLockUI.UserNameBitmap = TextShadowStroke(MenuUserName.Text, UserNameFont,
                ShadowRadius, Color.White, Color.Red, Color.Black,
                New Size(LoginAndLockUI.UserNameControl.Width + DoubleRadius, LoginAndLockUI.UserNameControl.Height + DoubleRadius))
            LoginAndLockUI.UserNameControl.Image = LoginAndLockUI.UserNameBitmap
            LoginAndLockUI.UserNameControl.AutoSize = False
            LoginAndLockUI.UserNameControl.Text = vbNullString
            LoginAndLockUI.UserNameControl.Size = New Size(300, LoginAndLockUI.UserNameControl.Image.Height)
            '处理完毕弹出提示浮窗
            If Not TipsForm.Visible Then TipsForm.Show(Me)
            TipsForm.PopupTips("Successfully !", TipsForm.TipsIconType.Infomation, "Set user name successfully")
            '将用户名图像转换为Base64编码存进存档
            LoginAndLockUI.UserNameString = BitmapToString(LoginAndLockUI.UserNameBitmap)
            My.Settings.UserName = LoginAndLockUI.UserName
            My.Settings.UserNameBitmap = LoginAndLockUI.UserNameString
            My.Settings.Save()
        End If
    End Sub

#End Region

#Region "右下角按钮组"

    Private Sub XYBrowserButtonControl_Click(sender As Object, e As EventArgs) Handles XYBrowserButtonControl.Click
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        '新建浏览器窗口
        LoadNewBrowser()
    End Sub

    Private Sub ConsoleButtonControl_Click(sender As Object, e As EventArgs) Handles ConsoleButtonControl.Click
        '显示控制台
        CommandConsole.ShowConsole()
    End Sub

    Private Sub ShutdownButtonControl_Click(sender As Object, e As EventArgs) Handles ShutdownButtonControl.Click
        '点击关机按钮显示关机提示框
        ShowShutdownWindow()
    End Sub

    Private Sub SettingButtonControl_Click(sender As Object, e As EventArgs) Handles SettingButtonControl.Click
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("Tips"), AudioPlayMode.Background)
        '显示或激活关于窗体
        If Not (AboutMeForm.Visible) Then AboutMeForm.Show(Me)
        SetForegroundWindow(AboutMeForm.Handle)
    End Sub

    Private Sub SpeechButtonControl_Click(sender As Object, e As EventArgs) Handles SpeechButtonControl.Click
        Try '防止语音引擎出错，需要容错处理
            If SpeechRecognitionMode Then
                '关闭语音识别
                MicroPhoneTimer.Stop()
                MySpeechRecognitionEngine.RecognizeAsyncStop()
                VoiceLevelBar.Value = 0
                SpeechRecognitionMode = False
                SpeechButtonControl.Image = My.Resources.SystemAssets.MicroPhone_Off
                '关闭成功弹出提示浮窗
                If Not TipsForm.Visible Then TipsForm.Show(Me)
                TipsForm.PopupTips("Shuted the", TipsForm.TipsIconType.Exclamation, "RecognitionEngine off.")
            Else
                '开启语音识别
                MySpeechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple)
                SpeechRecognitionMode = True
                SpeechButtonControl.Image = My.Resources.SystemAssets.MicroPhone_On
                '开启成功弹出提示浮窗
                If Not TipsForm.Visible Then TipsForm.Show(Me)
                TipsForm.PopupTips("Started the", TipsForm.TipsIconType.Infomation, "SpeechRecognitionEngine.")
            End If
        Catch ex As Exception
            '操作失败，弹出提示浮窗
            If Not TipsForm.Visible Then TipsForm.Show(Me)
            TipsForm.PopupTips("Failed to control the", TipsForm.TipsIconType.Critical, "SpeechRecognitionEngine")
        End Try
    End Sub

    Private Sub XYMailButtonControl_Click(sender As Object, e As EventArgs) Handles XYMailButtonControl.Click
        '显示/隐藏发送邮件窗体
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        If XYMail.Visible Then XYMail.Hide() Else XYMail.Show(Me)
    End Sub

#End Region

#Region "右下角图标动态效果"
    Private Sub ButtonControl_MouseEnter(sender As Object, e As EventArgs) Handles XYBrowserButtonControl.MouseEnter, ConsoleButtonControl.MouseEnter, ShutdownButtonControl.MouseEnter, XYMailButtonControl.MouseEnter, SettingButtonControl.MouseEnter
        Dim NowButton As Label = CType(sender, Label)
        NowButton.Image = My.Resources.SystemAssets.ResourceManager.GetObject(NowButton.Tag & "1")
    End Sub

    Private Sub ButtonControl_MouseLeave(sender As Object, e As EventArgs) Handles XYBrowserButtonControl.MouseLeave, ConsoleButtonControl.MouseLeave, ShutdownButtonControl.MouseLeave, XYMailButtonControl.MouseLeave, SettingButtonControl.MouseLeave
        Dim NowButton As Label = CType(sender, Label)
        NowButton.Image = My.Resources.SystemAssets.ResourceManager.GetObject(NowButton.Tag & "0")
    End Sub

    Private Sub ButtonControl_MouseUp(sender As Object, e As MouseEventArgs) Handles XYBrowserButtonControl.MouseUp, ConsoleButtonControl.MouseUp, ShutdownButtonControl.MouseUp, XYMailButtonControl.MouseUp, SettingButtonControl.MouseUp
        Dim NowButton As Label = CType(sender, Label)
        NowButton.Image = My.Resources.SystemAssets.ResourceManager.GetObject(NowButton.Tag & "1")
    End Sub

    Private Sub ButtonControl_MouseDown(sender As Object, e As MouseEventArgs) Handles XYBrowserButtonControl.MouseDown, ConsoleButtonControl.MouseDown, ShutdownButtonControl.MouseDown, XYMailButtonControl.MouseDown, SettingButtonControl.MouseDown
        Dim NowButton As Label = CType(sender, Label)
        NowButton.Image = My.Resources.SystemAssets.ResourceManager.GetObject(NowButton.Tag & "2")
    End Sub

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

    Private Sub MicroPhoneTimer_Tick(sender As Object, e As EventArgs) Handles MicroPhoneTimer.Tick
        '麦克风动态效果
        Static BitmapIndex As Int16 = 0
        SpeechButtonControl.Image = My.Resources.SystemAssets.ResourceManager.GetObject("MicroPhone_" & BitmapIndex.ToString("0"))
        BitmapIndex = IIf(BitmapIndex = 4, 0, BitmapIndex + 1)
    End Sub
#End Region

#End Region

#Region "功能函数"

    Private Sub LoadGrammar()
        '加载语音识别引擎语法表
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

    Public Sub LockScreen()
        '锁屏函数
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("ShowConsole"), AudioPlayMode.Background)
        If LoginAndLockUI.Visible Then Exit Sub
        LoginAndLockUI.LockScreenMode = True
        LoginAndLockUI.Show(Me)
        LoginAndLockUI.ShowLockScreen()
        SetForegroundWindow(LoginAndLockUI.Handle)
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
            MenuBreath.Enabled = True
            '激活脚本窗体
            SetForegroundWindow(ScriptForm(ScriptIndex).Handle)
        End If
    End Sub

    Public Sub ShowShutdownWindow()
        If LoginAndLockUI.Visible Then Exit Sub
        '播放提示音
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("Tips"), AudioPlayMode.Background)
        '显示或激活关机提示框
        If Not (ShutdownTips.Visible) Then ShutdownTips.Show(Me)
        SetForegroundWindow(ShutdownTips.Handle)
    End Sub

    Public Sub GetIPAndAddress(Optional ByVal IsFirstTime As Boolean = False)
        Try
            '网络未连接时程序会陷入等待假死，需要事先ping一下目标网站用于连接测试
            If (My.Computer.Network.Ping("ip.chinaz.com")) Then
                Dim IPWebClient As Net.WebClient = New Net.WebClient
                Dim WebString As String = vbNullString
                Dim RegIP As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex("\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}")
                IPWebClient.Encoding = System.Text.Encoding.UTF8
                WebString = IPWebClient.DownloadString(New Uri("http://ip.chinaz.com/getip.aspx"))
                IPLabel.Text = RegIP.Match(WebString).ToString
                AddressLabel.Text = Replace(Strings.Mid(WebString, IPLabel.Text.Length + 17, WebString.Length - IPLabel.Text.Length - 18), Chr(32), vbCrLf)
                IPWebClient.Dispose()
                '首次获取时(即程序启动时)不弹出提示浮窗
                If Not IsFirstTime Then
                    If Not TipsForm.Visible Then TipsForm.Show(Me)
                    TipsForm.PopupTips("Successfully :", TipsForm.TipsIconType.Exclamation, "Get IP and address !")
                End If
                '成功读取IP和地址，跳出过程
                Exit Sub
            End If
        Catch ex As Exception
        End Try

        '出错或网络未连接时显示错误信息
        IPLabel.Text = "127.0.0.1" : AddressLabel.Text = "Click to get."
        '首次获取时(即程序启动时)不弹出提示浮窗
        If Not IsFirstTime Then
            If Not TipsForm.Visible Then TipsForm.Show(Me)
            TipsForm.PopupTips("Error :", TipsForm.TipsIconType.Exclamation, "Can't get IP and Address.")
        End If
    End Sub

    Private Function FormatSpeedString(ByVal LoadSpeed As ULong) As String
        '格式化速度
        If LoadSpeed < 1048576 Then
            Return [String].Format("{0:n} KB/S", LoadSpeed / 1024.0)
        Else
            Return [String].Format("{0:n} MB/S", LoadSpeed / 1048576)
        End If
    End Function

    Private Function GetDesktopIconHandle() As IntPtr
        '获取物理系统桌面图标的句柄，用于嵌入实现置后显示
        Dim HandleDesktop As Integer = GetDesktopWindow
        Dim HandleTop As Integer = 0
        Dim LastHandleTop As Integer = 0
        Dim HandleSHELLDLL_DefView As Integer = 0
        Dim HandleSysListView32 As Integer = 0
        '在WorkerW结构里搜索
        Do Until HandleSysListView32 > 0
            HandleTop = FindWindowEx(HandleDesktop, LastHandleTop, "WorkerW", vbNullString)
            HandleSHELLDLL_DefView = FindWindowEx(HandleTop, 0, "SHELLDLL_DefView", vbNullString)
            If HandleSHELLDLL_DefView > 0 Then HandleSysListView32 = FindWindowEx(HandleSHELLDLL_DefView, 0, "SysListView32", "FolderView")
            LastHandleTop = HandleTop
            If LastHandleTop = 0 Then Exit Do
        Loop
        '如果找到了，立即返回
        If HandleSysListView32 > 0 Then Return HandleSysListView32
        '未找到，则在Progman里搜索(用于兼容WinXP系统)
        Do Until HandleSysListView32 > 0
            HandleTop = FindWindowEx(HandleDesktop, LastHandleTop, "Progman", "Program Manager")
            HandleSHELLDLL_DefView = FindWindowEx(HandleTop, 0, "SHELLDLL_DefView", vbNullString)
            If HandleSHELLDLL_DefView > 0 Then HandleSysListView32 = FindWindowEx(HandleSHELLDLL_DefView, 0, "SysListView32", "FolderView")
            LastHandleTop = HandleTop
            If LastHandleTop = 0 Then Exit Do : Return 0
        Loop
        Return HandleSysListView32
    End Function

    Public Sub LoadNewBrowser(Optional ByVal HomeURL As String = MainHomeURL)
        '加载新浏览器窗口
        Dim NewXYBrowser As Form = New XYBrowser
        NewXYBrowser.Tag = HomeURL
        BrowserForms.Add(NewXYBrowser)
        NewXYBrowser.Show(Me)
        SetForegroundWindow(NewXYBrowser.Handle)
        NewXYBrowser.TopMost = False
    End Sub

    Private Sub SetLabelForecolor(ByVal ForeColor As Color)
        '设置桌面图标文本颜色
        For Each ScriptIcon As Label In ScriptIcons
            ScriptIcon.ForeColor = LabelForecolor
        Next
        '设置右上角性能计数器文本颜色
        InfoTitle.ForeColor = LabelForecolor
        DiskReadCounterLabel.ForeColor = LabelForecolor
        DiskWriteCounterLabel.ForeColor = LabelForecolor
        UploadSpeedCountLabel.ForeColor = LabelForecolor
        DownloadSpeedCountLabel.ForeColor = LabelForecolor
        PowerLineLabel.ForeColor = LabelForecolor
        BatteryStatusLabel.ForeColor = LabelForecolor
        BatteryPercentLabel.ForeColor = LabelForecolor
        IPLabel.ForeColor = LabelForecolor
        AddressLabel.ForeColor = LabelForecolor
        DateTimeLabel.ForeColor = LabelForecolor
        '设置右下角功能图标文本颜色
        ConsoleButtonControl.ForeColor = LabelForecolor
        XYMailButtonControl.ForeColor = LabelForecolor
        XYBrowserButtonControl.ForeColor = LabelForecolor
        ShutdownButtonControl.ForeColor = LabelForecolor
        SettingButtonControl.ForeColor = LabelForecolor
    End Sub

    Private Function BitmapToString(ByVal Image As Bitmap) As String
        '把图像转换为Base64编码
        Dim BitmapStream As IO.MemoryStream = New IO.MemoryStream()
        Image.Save(BitmapStream, System.Drawing.Imaging.ImageFormat.Png)
        Dim EncryptByte() As Byte = BitmapStream.GetBuffer()
        Return Convert.ToBase64String(EncryptByte)
    End Function

    Private Function MakeCircularBitmap(ByVal InitialBitmap As Bitmap, ByVal BitmapSize As Size) As Bitmap
        '把原始头像图像裁剪为原型
        With InitialBitmap
            If .Width > .Height Then
                InitialBitmap = .Clone(New Rectangle((.Width - .Height) / 2, 0, .Height, .Height), InitialBitmap.PixelFormat)
            ElseIf .Width < .Height Then
                InitialBitmap = .Clone(New Rectangle(0, (.Height - .Width) / 2, .Width, .Width), InitialBitmap.PixelFormat)
            End If
        End With
        InitialBitmap = New Bitmap(InitialBitmap, BitmapSize)
        Dim CircularBitmap As Bitmap = New Bitmap(BitmapSize.Width, BitmapSize.Height)
        Dim CircularGraphics As Graphics = Graphics.FromImage(InitialBitmap)
        Dim CircularGraphicsPath As Drawing2D.GraphicsPath = New Drawing2D.GraphicsPath
        '创建圆形外围路径，填充颜色后将该颜色置为透明
        CircularGraphicsPath.AddRectangle(New RectangleF(0, 0, BitmapSize.Width, BitmapSize.Height))
        CircularGraphicsPath.AddEllipse(0, 0, BitmapSize.Width, BitmapSize.Height)
        CircularGraphics.FillPath(Brushes.Black, CircularGraphicsPath)
        CircularGraphicsPath.Dispose()
        InitialBitmap.MakeTransparent(Color.Black)
        '再用被透明的颜色绘制一个圆形当做背景，并覆盖处理后的圆形图像，防止中心圆形图像被透明
        CircularGraphics = Graphics.FromImage(CircularBitmap)
        CircularGraphics.FillEllipse(Brushes.Black, New Rectangle(0, 0, CircularBitmap.Width, CircularBitmap.Height))
        CircularGraphics.DrawImage(InitialBitmap, New Rectangle(0, 0, InitialBitmap.Width, InitialBitmap.Height))
        CircularGraphics.Dispose()

        Return CircularBitmap
    End Function

    Private Function TextShadowStroke(ByVal DrawText As String, ByVal TextFont As Font, ByVal ShadowRadius As Integer, ByVal ForeColor As Color, ByVal ShadowColor As Color, ByVal StrokeColor As Color, ByVal BitmapSize As Size) As Bitmap
        '返回文本描边加阴影后的图像
        Dim DoubleRadius As Integer = ShadowRadius * 2
        Dim ShadowColorCell() As Byte = {ShadowColor.R, ShadowColor.G, ShadowColor.B}
        Dim ResualtBitmap As Bitmap = New Bitmap(BitmapSize.Width, BitmapSize.Height)
        Dim ResualtGraphics As Graphics = Graphics.FromImage(ResualtBitmap)
        ResualtGraphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        ResualtGraphics.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit

        '绘制阴影(直接操作图像内存，速度较快)
        ResualtGraphics.DrawString(DrawText, TextFont, New SolidBrush(ShadowColor), New PointF(ShadowRadius, ShadowRadius))
        Dim ResualtBitmapData As Imaging.BitmapData = New Imaging.BitmapData
        ResualtBitmapData = ResualtBitmap.LockBits(New Rectangle(0, 0, BitmapSize.Width, BitmapSize.Height), Imaging.ImageLockMode.WriteOnly, ResualtBitmap.PixelFormat)
        Dim DataStride As Integer = ResualtBitmapData.Stride
        Dim DataWidth As Integer = ResualtBitmapData.Width
        Dim DataHeight As Integer = ResualtBitmapData.Height
        Dim InitalDataArray(DataStride * DataHeight - 1) As Byte
        Dim DataArray(DataStride * DataHeight - 1) As Byte
        Dim Position(DoubleRadius, DoubleRadius) As Integer
        Runtime.InteropServices.Marshal.Copy(ResualtBitmapData.Scan0, InitalDataArray, 0, InitalDataArray.Length)
        Dim Index = 0, IndexX, IndexY As Integer, Round, RoundX, RoundY As Integer
        Dim ByteSum, AValue As Integer
        Dim Boundary(DataHeight) As Integer, LineIndex As Integer
        For RoundY = 0 To DoubleRadius
            Index = (RoundY - ShadowRadius) * DataWidth - ShadowRadius
            For RoundX = 0 To DoubleRadius
                Position(RoundY, RoundX) = IIf((RoundX - ShadowRadius) ^ 2 + (RoundY - ShadowRadius) ^ 2 <= ShadowRadius ^ 2, 4 * (Index + RoundX), 0)
            Next
        Next
        For IndexY = 0 To DataHeight - 1
            Boundary(IndexY + 1) = Boundary(IndexY) + DataStride
        Next
        For IndexY = 0 To DataHeight - 1
            For IndexX = 0 To DataWidth - 1
                ByteSum = 0 : AValue = 0
                Index = IndexY * DataStride + IndexX * 4
                For RoundY = 0 To DoubleRadius
                    LineIndex = IndexY + RoundY - ShadowRadius
                    If 0 <= LineIndex AndAlso LineIndex < Boundary.Count - 1 Then
                        For RoundX = 0 To DoubleRadius
                            Round = Index + Position(RoundY, RoundX)
                            If Boundary(LineIndex) <= Round AndAlso Round < Boundary(LineIndex + 1) Then
                                AValue += InitalDataArray(Round + 3)
                                ByteSum += 1
                            End If
                        Next
                    End If
                Next
                AValue /= ByteSum
                DataArray(Index + 0) = ShadowColorCell(2)
                DataArray(Index + 1) = ShadowColorCell(1)
                DataArray(Index + 2) = ShadowColorCell(0)
                DataArray(Index + 3) = AValue
            Next
        Next
        Runtime.InteropServices.Marshal.Copy(DataArray, 0, ResualtBitmapData.Scan0, DataArray.Length)
        ResualtBitmap.UnlockBits(ResualtBitmapData)

        '文字描边
        Dim DrawBrush As Brush = New System.Drawing.Drawing2D.LinearGradientBrush(New Point(0, 0), New Point(0, 1), StrokeColor, StrokeColor)
        ResualtGraphics.DrawString(DrawText, TextFont, DrawBrush, New Point(ShadowRadius - 1, ShadowRadius + 1))
        ResualtGraphics.DrawString(DrawText, TextFont, DrawBrush, New Point(ShadowRadius - 1, ShadowRadius - 1))
        ResualtGraphics.DrawString(DrawText, TextFont, DrawBrush, New Point(ShadowRadius + 1, ShadowRadius + 1))
        ResualtGraphics.DrawString(DrawText, TextFont, DrawBrush, New Point(ShadowRadius + 1, ShadowRadius - 1))
        ResualtGraphics.DrawString(DrawText, TextFont, DrawBrush, New Point(ShadowRadius + 1, ShadowRadius))
        ResualtGraphics.DrawString(DrawText, TextFont, DrawBrush, New Point(ShadowRadius - 1, ShadowRadius))
        ResualtGraphics.DrawString(DrawText, TextFont, DrawBrush, New Point(ShadowRadius, ShadowRadius + 1))
        ResualtGraphics.DrawString(DrawText, TextFont, DrawBrush, New Point(ShadowRadius, ShadowRadius - 1))

        '最后绘制原文字
        ResualtGraphics.DrawString(DrawText, TextFont, New SolidBrush(ForeColor), New PointF(ShadowRadius, ShadowRadius))
        ResualtGraphics.Dispose()
        Return ResualtBitmap
    End Function

#End Region

End Class
