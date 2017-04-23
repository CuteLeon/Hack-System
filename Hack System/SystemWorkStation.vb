Imports System.ComponentModel
Imports System.Net
Imports System.Speech.Recognition
Imports Microsoft.VisualBasic.Devices

Public Class SystemWorkStation

#Region "声明区"
    ''' <summary>
    ''' 检测句柄是否有效（置后显示时检测物理桌面是否崩溃）
    ''' </summary>
    Private Declare Function IsWindow Lib "user32" Alias "IsWindow" (ByVal hWnd As IntPtr) As Integer '判断一个窗口句柄是否有效，置后显示时检测桌面容器是否意外关闭
    ''' <summary>
    ''' 用于查找物理桌面句柄
    ''' </summary>
    Private Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer
    ''' <summary>
    ''' 获取桌面句柄
    ''' </summary>
    Private Declare Function GetDesktopWindow Lib "user32" Alias "GetDesktopWindow" () As IntPtr
    ''' <summary>
    ''' 设置窗口的父窗口句柄
    ''' </summary>
    Private Declare Function SetParent Lib "user32" Alias "SetParent" (ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As Integer
    ''' <summary>
    ''' 设置窗体样式
    ''' </summary>
    Private Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As IntPtr
    ''' <summary>
    ''' 获取窗体样式
    ''' </summary>
    Private Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As IntPtr, ByVal nIndex As Integer) As Integer
    ''' <summary>
    ''' 设置窗体半透明度
    ''' </summary>
    Private Declare Function SetLayeredWindowAttributes Lib "user32" (ByVal hwnd As IntPtr, ByVal crKey As Integer, ByVal bAlpha As Integer, ByVal dwFlags As Integer) As Integer
    ''' <summary>
    ''' 记录物理桌面容器的句柄
    ''' </summary>
    Dim DesktopIconHandle As IntPtr
    ''' <summary>
    ''' 用户名显示字体
    ''' </summary>
    Dim UserNameFont As Font = New Font("微软雅黑", 36.0)
    ''' <summary>
    ''' 鼠标按下的坐标(用于显示桌面拖动蓝色矩形)
    ''' </summary>
    Dim MouseDownLocation As Point
    ''' <summary>
    ''' 鼠标按下后移动的距离
    ''' </summary>
    Dim XDistance, YDistance As Integer
    ''' <summary>
    ''' 计算桌面每列显示图标的数量
    ''' </summary>
    Dim ColumnIconCount As Integer = Int(My.Computer.Screen.Bounds.Height / IconHeight) - 1
    ''' <summary>
    ''' 两个桌面图标间的距离
    ''' </summary>
    Dim IntervalDistance As Integer = (My.Computer.Screen.Bounds.Height - ColumnIconCount * IconHeight) / (ColumnIconCount + 1)
    ''' <summary>
    ''' 初始桌面壁纸的标识
    ''' </summary>
    Dim WallpaperIndex As Integer = 9
    ''' <summary>
    ''' 用户自定义壁纸
    ''' </summary>
    Dim CustomWallpaperBitmap As Bitmap
    ''' <summary>
    ''' 用户自定义壁纸-Base64字符串
    ''' </summary>
    Dim CustomWallpaperString As String
    ''' <summary>
    ''' 高亮的桌面图标
    ''' </summary>
    Dim HighLightIcon(ScriptUpperBound) As Bitmap
    ''' <summary>
    ''' 鼠标按下的桌面图标
    ''' </summary>
    Dim MouseDownIcon(ScriptUpperBound) As Bitmap
    ''' <summary>
    ''' 初始文本标签颜色
    ''' </summary>
    Dim LabelForecolor As Color = Color.Aqua
    ''' <summary>
    ''' 桌面图标画笔
    ''' </summary>
    Dim IconGraphics As Graphics
    ''' <summary>
    ''' 用于记录触发事件的控件
    ''' </summary>
    Dim SenderControl As Label
    ''' <summary>
    ''' CPU性能计数器
    ''' </summary>
    Dim CPUCounter As New PerformanceCounter("Processor", "% Processor Time", "_Total")
    ''' <summary>
    ''' 内存性能计数器
    ''' </summary>
    Dim DiskReadCounter As New PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", "_Total")
    ''' <summary>
    ''' 硬盘性能计数器
    ''' </summary>
    Dim DiskWriteCounter As New PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total")
    ''' <summary>
    ''' 网络性能计数器
    ''' </summary>
    Dim PCCategory As New PerformanceCounterCategory("Network Interface")
    ''' <summary>
    ''' 计算机信息
    ''' </summary>
    Dim CmptInfo As New ComputerInfo()
    ''' <summary>
    ''' 内存使用率
    ''' </summary>
    Dim MemoryUsageRate As Integer
    '为每块网卡创建单独的性能计数器，防止某块网卡突然仅用时，网速出现负数情况
    ''' <summary>
    ''' 网卡数组的下标
    ''' </summary>
    Dim UBoundOfPCCategory As UInteger = PCCategory.GetInstanceNames.Count - 1
    ''' <summary>
    ''' 下载计数器
    ''' </summary>
    Dim DownloadCounter(UBoundOfPCCategory) As PerformanceCounter
    ''' <summary>
    ''' 上传计数器
    ''' </summary>
    Dim UploadCounter(UBoundOfPCCategory) As PerformanceCounter
    ''' <summary>
    ''' 下载速度；上传速度
    ''' </summary>
    Dim DownloadSpeed(UBoundOfPCCategory) As ULong, UploadSpeed(UBoundOfPCCategory) As ULong
    ''' <summary>
    ''' 下载字节数；上传字节数
    ''' </summary>
    Dim DownloadValue(UBoundOfPCCategory) As ULong, UploadValue(UBoundOfPCCategory) As ULong
    ''' <summary>
    ''' 上一次记录的下载字节数；上一次记录的上传字节数
    ''' </summary>
    Dim DownloadValueOld(UBoundOfPCCategory) As ULong, UploadValueOld(UBoundOfPCCategory) As ULong
    ''' <summary>
    ''' 总下载速度；总上传速度
    ''' </summary>
    Dim DownloadSpeedCount As ULong, UploadSpeedCount As ULong
    ''' <summary>
    ''' 用于获取IP和城市定位
    ''' </summary>
    Dim IPAndAddressClient As WebClient
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
        VPButton.Location = New Point(ConsoleButtonControl.Left - IntervalDistance - SpeechButtonControl.Width, ConsoleButtonControl.Top)
        VMButton.Location = New Point(VPButton.Left - IntervalDistance - SpeechButtonControl.Width, ConsoleButtonControl.Top)
        SpeechButtonControl.Location = New Point(VMButton.Left - IntervalDistance - SpeechButtonControl.Width, ConsoleButtonControl.Top)
        VoiceLevelBar.Location = New Point(5, SpeechButtonControl.Height - 12)
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
        Try
            For Index As Integer = 0 To UBoundOfPCCategory
                DownloadCounter(Index) = New PerformanceCounter("Network Interface", "Bytes Received/sec", PCCategory.GetInstanceNames(Index))
                UploadCounter(Index) = New PerformanceCounter("Network Interface", "Bytes Sent/sec", PCCategory.GetInstanceNames(Index))
                DownloadValueOld(Index) = DownloadCounter(Index).NextSample().RawValue
                UploadValueOld(Index) = UploadCounter(Index).NextSample().RawValue
            Next
        Catch ex As Exception
        End Try
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
        Catch ex As Exception
            '开启语音引擎失败，善后处理
            VoiceLevelBar.Value = 0
            SpeechButtonControl.Image = My.Resources.SystemAssets.MicroPhone_Off
            MySpeechRecognitionEngine.Dispose()
        End Try

        '首次获取IP和地址
        GetIPAndAddress()
    End Sub

    Public Sub SystemWorkStation_Activated(sender As Object, e As EventArgs)
        '不要使用Timer值守置前，因为会影响输入法的候选词窗体
        'SystemWorkStation 初次显示时会自动 Activated 并置前显示，导致登录界面的 FirstLoginIn() 特效无法置前显示，所以需要特效结束后再注册事件
        Me.TopMost = MenuTopMost.Checked
    End Sub

#End Region

#Region "控件"

#Region "性能计数器区域"

    Private Sub PerformanceCounterTimer_Tick(sender As Object, e As EventArgs) Handles PerformanceCounterTimer.Tick
        '置后显示时检测桌面容器是否意外关闭，意外关闭时需要恢复窗体置顶显示
        If MenuTopMost.Checked = False AndAlso IsWindow(DesktopIconHandle) = False Then
            MenuTopMost.PerformClick() '模拟点击一次 MenuTopMost
            '恢复后所有Label会丢失Text属性，需要重新赋值
            For ScriptIndex As Integer = 0 To ScriptUpperBound
                ScriptIcons(ScriptIndex).Text = ScriptInfomation(ScriptIndex)
            Next
            InfoTitle.Text = "CPU：" & vbCrLf & "RAM：" & vbCrLf & "DiskRead：" & vbCrLf & "DiskWrite：" & vbCrLf & "Upload：" & vbCrLf & "Download：" & vbCrLf & "PowerLine：" & vbCrLf & "BatteryStatus：" & vbCrLf & "BatteryPercent：" & vbCrLf & "IP：" & vbCrLf & "Address："
            XYBrowserButtonControl.Text = "Browser"
            ConsoleButtonControl.Text = "Console"
            ShutdownButtonControl.Text = "Shutdown"
            XYMailButtonControl.Text = "Mail"
            SettingButtonControl.Text = "About"
            GetIPAndAddress() '刷新IP和地址信息
        End If

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
            If e.Result.Text = "解锁" Then LoginAndLockUI.SplitLoginIn()
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
                UnityModule.SetForegroundWindow(XYMail.Handle)
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
            Case "二零四八"
                If Not Game2048Form.Visible Then Game2048Form.Show(Me)
                SetForegroundWindow(Game2048Form.Handle)
            Case "一零一零"
                If Not Game1010Form.Visible Then Game1010Form.Show(Me)
                SetForegroundWindow(Game1010Form.Handle)
            Case "妖零妖零"
                If Not Game1010Form.Visible Then Game1010Form.Show(Me)
                SetForegroundWindow(Game1010Form.Handle)
            Case "浏览器"
                LoadNewBrowser()
            Case "锁屏"
                LoginAndLockUI.ShowLockScreen()
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
        MenuCloseScript.Enabled = ScriptFormShown(NowIconIndex)
        MenuBreath.Enabled = ScriptFormShown(NowIconIndex)
    End Sub

    Private Sub IconTemplates_MouseLeave(sender As Object, e As EventArgs)
        '鼠标离开图标时，如果脚本在关闭状态就取消高亮显示图标
        Dim ScriptIndex As Integer = Int(CType(sender, Label).Tag)
        'NowIconIndex = -1
        If Not (ScriptFormShown(ScriptIndex)) Then SenderControl.Image = My.Resources.SystemAssets.ResourceManager.GetObject("ScriptIcon_" & SenderControl.Tag)
        If AeroPeekMode Then
            '遍历脚本窗体，还原在AeroPeek模式下被透明的窗体的透明度
            For ScriptIndex = 0 To ScriptUpperBound
                If ScriptFormShown(ScriptIndex) Then ScriptForm(ScriptIndex).Opacity = UnityModule.NegativeOpacity
            Next
            '关闭AeroPeek模式
            AeroPeekMode = False
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
        If Not (ScriptFormShown(NowIconIndex)) Or ShutdownTips.Visible Or AboutMeForm.Visible Then Exit Sub

        AeroPeekMode = True
        Dim StartIndex, EndIndex, ScriptIndex As Integer
        EndIndex = NowIconIndex - 1
        StartIndex = NowIconIndex + 1
        ScriptForm(NowIconIndex).Opacity = 1
        '向前遍历隐藏脚本窗体
        For ScriptIndex = 0 To EndIndex
            If ScriptFormShown(ScriptIndex) Then ScriptForm(ScriptIndex).Opacity = AeroPeekOpacity
        Next
        '向后遍历隐藏脚本窗体
        For ScriptIndex = StartIndex To ScriptUpperBound
            If ScriptFormShown(ScriptIndex) Then ScriptForm(ScriptIndex).Opacity = AeroPeekOpacity
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
            WallpaperIndex = IIf(WallpaperIndex <= 0, UnityModule.WallpaperUBound, WallpaperIndex - 1)
            Me.BackgroundImage = My.Resources.SystemAssets.ResourceManager.GetObject("SystemWallpaper_" & WallpaperIndex.ToString("00"))
        End If
        '保存壁纸标识
        My.Settings.DesktopWallpaperIndex = WallpaperIndex
        My.Settings.Save()
    End Sub

    Private Sub MenuNextWallpaper_Click(sender As Object, e As EventArgs) Handles MenuNextWallpaper.Click
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        '桌面右键菜单之下一张壁纸'
        If WallpaperIndex = UnityModule.WallpaperUBound AndAlso (CustomWallpaperBitmap IsNot Nothing) Then
            '允许显示用户自定义壁纸
            WallpaperIndex = -1
            Me.BackgroundImage = CustomWallpaperBitmap
        Else
            WallpaperIndex = IIf(WallpaperIndex = UnityModule.WallpaperUBound Or WallpaperIndex = -1, 0, WallpaperIndex + 1)
            Me.BackgroundImage = My.Resources.SystemAssets.ResourceManager.GetObject("SystemWallpaper_" & WallpaperIndex.ToString("00"))
        End If
        '保存壁纸标识
        My.Settings.DesktopWallpaperIndex = WallpaperIndex
        My.Settings.Save()
    End Sub

    Private Sub MenuLockScreen_Click(sender As Object, e As EventArgs) Handles MenuLockScreen.Click
        '锁屏
        LoginAndLockUI.ShowLockScreen()
    End Sub

    Private Sub MenuShutdown_Click(sender As Object, e As EventArgs) Handles MenuShutdown.Click
        '桌面右键菜单之关机
        ShowShutdownWindow()
    End Sub

    Private Sub MenuTopMost_Click(sender As Object, e As EventArgs) Handles MenuTopMost.Click
        '改变系统的置前和置后状态
        If MenuTopMost.Checked Then
            '置前显示，需要重新拥有子窗体，使它们永远在SystemWorkStation前面显示
            If Me.TopMost = False Then Me.TopMost = True
            For Each OwnedForm As Form In Application.OpenForms
                If OwnedForm IsNot Me Then Me.AddOwnedForm(OwnedForm)
            Next
            SetParent(Me.Handle, GetDesktopWindow())
        Else
            '把SystemWorkStation嵌入到物理桌面，首先查找物理桌面句柄，查找到之后置后显示并移除拥有的子窗体，否则子窗体会集中在一层显示
            DesktopIconHandle = GetDesktopIconHandle()
            If DesktopIconHandle = IntPtr.Zero Then
                '如果查找DesktopIconHandle句柄失败时，无法置后显示，需要弹窗提示并取消任务
                TipsForm.PopupTips(Me, "置后失败！", UnityModule.TipsIconType.Infomation, "无法定位物理桌面句柄！")
                MenuTopMost.Checked = True '不改变 MenuTopMost 的勾选状态，防止重复响应 Click 事件
            Else
                Me.TopMost = False
                For Each OwnedForm As Form In Me.OwnedForms
                    If OwnedForm IsNot TipsForm Then Me.RemoveOwnedForm(OwnedForm)
                Next
                SetParent(Me.Handle, DesktopIconHandle)
            End If
        End If
    End Sub

    Private Sub MenuCustomWallpaper_Click(sender As Object, e As EventArgs) Handles MenuCustomWallpaper.Click
        '设置自定义壁纸
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        '启动自定义壁纸选取对话框
        If CustomImageDialog.ShowDialog() = DialogResult.OK Then
            Try '为选取文件方便，给过滤器加了"*.*"，为防止故意选取非图像格式文件，需要容错处理！
                CustomWallpaperBitmap = Bitmap.FromFile(CustomImageDialog.FileName)
                Me.BackgroundImage = CustomWallpaperBitmap
                '弹出提示浮窗
                TipsForm.PopupTips(Me, "设置壁纸：", UnityModule.TipsIconType.Infomation, "设置自定义壁纸成功！")
                WallpaperIndex = -1
                '保存壁纸标识和自定义壁纸的Base64编码到存档
                My.Settings.DesktopWallpaperIndex = WallpaperIndex
                My.Settings.CustomWallpaper = BitmapToString(CustomWallpaperBitmap)
                My.Settings.Save()
            Catch ex As Exception
                '出错时弹出提示浮窗
                TipsForm.PopupTips(Me, "设置壁纸：", UnityModule.TipsIconType.Critical, "无法读取文件为图像格式！")
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
                UnityModule.UserHead = MakeCircularBitmap(Bitmap.FromFile(CustomImageDialog.FileName), New Size(159, 159))
                LoginAndLockUI.HeadPictureBox.BackgroundImage = UnityModule.UserHead
                '头像转换为Base64编码后存进存档
                UnityModule.UserHeadString = BitmapToString(UnityModule.UserHead)
                My.Settings.UserHead = UnityModule.UserHeadString
                My.Settings.Save()
                '弹出提示浮窗
                TipsForm.PopupTips(Me, "设置头像：", UnityModule.TipsIconType.Infomation, "设置用户头像成功！")
            Catch ex As Exception
                '出错时弹出提示浮窗
                TipsForm.PopupTips(Me, "设置头像：", UnityModule.TipsIconType.Critical, "无法读取文件为图像格式！")
            End Try
        Else
            My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        End If
    End Sub

    Private Sub MenuUserName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MenuUserName.KeyPress
        '在菜单里敲击回车键时设置用户名
        If e.KeyChar = Chr(Keys.Enter) Then
            '用户名未改变不处理
            If UnityModule.UserName = MenuUserName.Text Then Exit Sub
            '用户名背后阴影的模糊半径
            Dim ShadowRadius As Integer = 10
            Dim DoubleRadius As Integer = ShadowRadius * 2
            UnityModule.UserName = MenuUserName.Text
            LoginAndLockUI.UserNameControl.AutoSize = True
            LoginAndLockUI.UserNameControl.Font = UserNameFont
            LoginAndLockUI.UserNameControl.Text = MenuUserName.Text
            '绘制用户名的描边加阴影图像
            UnityModule.UserNameBitmap = TextShadowStroke(MenuUserName.Text, UserNameFont,
                ShadowRadius, Color.White, Color.Red, Color.Black,
                New Size(LoginAndLockUI.UserNameControl.Width + DoubleRadius, LoginAndLockUI.UserNameControl.Height + DoubleRadius))
            LoginAndLockUI.UserNameControl.Image = UnityModule.UserNameBitmap
            LoginAndLockUI.UserNameControl.AutoSize = False
            LoginAndLockUI.UserNameControl.Text = vbNullString
            LoginAndLockUI.UserNameControl.Size = New Size(300, LoginAndLockUI.UserNameControl.Image.Height)
            '处理完毕弹出提示浮窗
            TipsForm.PopupTips(Me, "设置用户名：", UnityModule.TipsIconType.Infomation, "设置用户名成功！")
            '将用户名图像转换为Base64编码存进存档
            UnityModule.UserNameString = BitmapToString(UnityModule.UserNameBitmap)
            My.Settings.UserName = UnityModule.UserName
            My.Settings.UserNameBitmap = UnityModule.UserNameString
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
        UnityModule.SetForegroundWindow(CommandConsole.Handle)
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
                TipsForm.PopupTips(Me, "语音识别：", UnityModule.TipsIconType.Exclamation, "语音识别引擎已经关闭！")
            Else
                '开启语音识别
                MySpeechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple)
                SpeechRecognitionMode = True
                SpeechButtonControl.Image = My.Resources.SystemAssets.MicroPhone_On
                '开启成功弹出提示浮窗
                TipsForm.PopupTips(Me, "语音识别：", UnityModule.TipsIconType.Exclamation, "语音识别引擎已经开启！")
            End If
        Catch ex As Exception
            '操作失败，弹出提示浮窗
            TipsForm.PopupTips(Me, "语音识别：", UnityModule.TipsIconType.Exclamation, "无法调用语音识别引擎！")
        End Try
    End Sub

    Private Sub XYMailButtonControl_Click(sender As Object, e As EventArgs) Handles XYMailButtonControl.Click
        '显示/隐藏发送邮件窗体
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        If XYMail.Visible Then XYMail.Hide() Else XYMail.Show(Me)
    End Sub

    Private Sub VMButton_Click(sender As Object, e As EventArgs) Handles VMButton.Click
        VMMainForm.Show(Me)
    End Sub

    Private Sub VPButton_Click(sender As Object, e As EventArgs) Handles VPButton.Click
        VPMainForm.Show(Me)
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

    ''' <summary>
    ''' 为语音引擎导入要识别的指令语法
    ''' </summary>
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
        Grammars.Add("二零四八")
        Grammars.Add("一零一零")
        Grammars.Add("妖零妖零")
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

    ''' <summary>
    ''' 加载制定下标的脚本
    ''' </summary>
    ''' <param name="ScriptIndex"></param>
    Public Sub LoadScript(ByVal ScriptIndex As Integer)
        '播放脚本启动音效
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("ScriptStarting"), AudioPlayMode.Background)
        '如果脚本未被加载过，先创建脚本窗体对象并设定脚本标识
        If ScriptForm(ScriptIndex) Is Nothing Then
            ScriptForm(ScriptIndex) = New WindowsTemplates
            ScriptForm(ScriptIndex).Tag = ScriptIndex.ToString("00")
            ScriptForm(ScriptIndex).Text = ScriptInfomation(ScriptIndex)
        End If
        '脚本在关闭状态
        If (ScriptForm(ScriptIndex).Visible) = False AndAlso (ScriptFormShown(ScriptIndex) = False) Then
            '高亮显示脚本对应桌面图标
            SenderControl = ScriptIcons(ScriptIndex)
            If HighLightIcon(ScriptIndex) Is Nothing Then
                HighLightIcon(ScriptIndex) = My.Resources.SystemAssets.ResourceManager.GetObject("ScriptIcon_" & SenderControl.Tag)
                Using IconGraphics = Graphics.FromImage(HighLightIcon(ScriptIndex))
                    IconGraphics.DrawImage(My.Resources.SystemAssets.MouseEnter, 0, 0)
                End Using
            End If
            SenderControl.Image = HighLightIcon(ScriptIndex)
            '显示脚本窗体并记录
            ScriptFormShown(ScriptIndex) = True
            '置后显示时不允许为.Show()赋予拥有者参数，否则子窗口会集中在一层显示
            If Me.MenuTopMost.Checked Then ScriptForm(ScriptIndex).Show(Me) Else ScriptForm(ScriptIndex).Show()
            '脚本在打开状态
        ElseIf ScriptForm(ScriptIndex).Visible And ScriptFormShown(ScriptIndex) Then
            '图标右键菜单项设为可用
            MenuCloseScript.Enabled = True
            MenuBreath.Enabled = True
            '震动脚本
            Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf UnityModule.QQ_Vibration), ScriptForm(ScriptIndex))
        End If
    End Sub

    ''' <summary>
    ''' 试图关闭系统，弹出提示窗
    ''' </summary>
    Public Sub ShowShutdownWindow()
        If LoginAndLockUI.Visible Then Exit Sub
        '播放提示音
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("Tips"), AudioPlayMode.Background)
        '显示或激活关机提示框
        If Not (ShutdownTips.Visible) Then ShutdownTips.Show(Me)
        SetForegroundWindow(ShutdownTips.Handle)
    End Sub

    ''' <summary>
    ''' 获取IP并异步从第三方网站获取物理地址
    ''' </summary>
    Public Sub GetIPAndAddress()
        If IPAndAddressClient IsNot Nothing AndAlso IPAndAddressClient.IsBusy Then Exit Sub
        Try
            IPLabel.Text = "正在获取..." : AddressLabel.Text = "正在获取..."
            Me.Refresh()
            '需要事先ping一下目标网站用于连接测试，避免无法连接时浪费等待时间
            If My.Computer.Network.Ping("ip.chinaz.com") Then
                IPAndAddressClient = New WebClient With {.Encoding = System.Text.Encoding.UTF8}
                AddHandler IPAndAddressClient.DownloadStringCompleted, AddressOf DownloadStringCompleted
                IPAndAddressClient.DownloadStringAsync(New Uri("http://ip.chinaz.com/getip.aspx"))
            Else
                IPLabel.Text = "LocalHost" : AddressLabel.Text = "Click to get."
            End If
        Catch ex As Exception
            IPLabel.Text = "LocalHost" : AddressLabel.Text = "Click to get."
        End Try
    End Sub

    ''' <summary>
    ''' 获取IP和地址完毕
    ''' </summary>
    Private Sub DownloadStringCompleted(ender As Object, e As System.Net.DownloadStringCompletedEventArgs)
        If e.Cancelled = False AndAlso e.Error Is Nothing Then
            Dim RegIP As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex("\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}")
            IPLabel.Text = RegIP.Match(e.Result).ToString
            AddressLabel.Text = Replace(Strings.Mid(e.Result, IPLabel.Text.Length + 17, e.Result.Length - IPLabel.Text.Length - 18), Chr(32), vbCrLf)
        Else
            IPLabel.Text = "LocalHost" : AddressLabel.Text = "Click to get."
            IPAndAddressClient.Dispose()
        End If
    End Sub


    ''' <summary>
    ''' 格式化速度文本并添加单位
    ''' </summary>
    ''' <param name="LoadSpeed">传入的无符号整型速度</param>
    ''' <returns></returns>
    Private Function FormatSpeedString(ByVal LoadSpeed As ULong) As String
        '格式化速度
        If LoadSpeed < 1048576 Then
            Return [String].Format("{0:n} KB/S", LoadSpeed / 1024.0)
        Else
            Return [String].Format("{0:n} MB/S", LoadSpeed / 1048576)
        End If
    End Function

    ''' <summary>
    ''' 获取物理系统桌面图标的句柄，用于嵌入实现置后显示
    ''' </summary>
    ''' <returns>物理桌面容器句柄</returns>
    Private Function GetDesktopIconHandle() As IntPtr
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
            If LastHandleTop = 0 Then Exit Do : Return IntPtr.Zero
        Loop
        Return HandleSysListView32
    End Function

    ''' <summary>
    ''' 加载新浏览器窗口
    ''' </summary>
    ''' <param name="HomeURL">自动打开的网址(包括系统主页和在新窗口打开的链接)</param>
    Public Sub LoadNewBrowser(Optional ByVal HomeURL As String = MainHomeURL)
        Dim NewXYBrowser As Form = New XYBrowser
        NewXYBrowser.Tag = HomeURL
        BrowserForms.Add(NewXYBrowser)
        If Me.MenuTopMost.Checked Then NewXYBrowser.Show(Me) Else NewXYBrowser.Show()
        SetForegroundWindow(NewXYBrowser.Handle)
    End Sub

    ''' <summary>
    ''' 设置桌面图标文本颜色
    ''' </summary>
    ''' <param name="ForeColor">字体颜色</param>
    Private Sub SetLabelForecolor(ByVal ForeColor As Color)
        For Each ScriptIcon As Label In ScriptIcons
            ScriptIcon.ForeColor = LabelForecolor
        Next
        '设置控制台Label颜色
        CommandConsole.CommandTip.ForeColor = LabelForecolor
        CommandConsole.ConsoleTitleLabel.ForeColor = LabelForecolor
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

    ''' <summary>
    ''' 把图像转换为Base64文本
    ''' </summary>
    ''' <param name="Image">要转换的图像</param>
    ''' <returns>转换出的文本</returns>
    Private Function BitmapToString(ByVal Image As Bitmap) As String
        '把图像转换为Base64编码
        Dim BitmapStream As IO.MemoryStream = New IO.MemoryStream()
        Image.Save(BitmapStream, System.Drawing.Imaging.ImageFormat.Png)
        Dim EncryptByte() As Byte = BitmapStream.GetBuffer()
        Return Convert.ToBase64String(EncryptByte)
    End Function

    ''' <summary>
    ''' 把原始头像图像裁剪为原型
    ''' </summary>
    ''' <param name="InitialBitmap">原始的头像图像</param>
    ''' <param name="BitmapSize">图像尺寸</param>
    ''' <returns>裁剪为圆形的头像</returns>
    Private Function MakeCircularBitmap(ByVal InitialBitmap As Bitmap, ByVal BitmapSize As Size) As Bitmap
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

    ''' <summary>
    ''' 为文本绘制阴影（对文本图像模糊处理）
    ''' </summary>
    ''' <param name="DrawText">要绘制的文本</param>
    ''' <param name="TextFont">文本字体</param>
    ''' <param name="ShadowRadius">阴影半径</param>
    ''' <param name="ForeColor">文本颜色</param>
    ''' <param name="ShadowColor">阴影颜色</param>
    ''' <param name="StrokeColor">描边颜色</param>
    ''' <param name="BitmapSize">图像尺寸</param>
    ''' <returns></returns>
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
