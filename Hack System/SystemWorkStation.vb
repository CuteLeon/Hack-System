Imports System.ComponentModel
Imports Microsoft.VisualBasic.Devices

Public Class SystemWorkStation
    Public Declare Sub SetWindowPos Lib "User32" (ByVal hWnd As IntPtr, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer)

    'Mouse Drag Area.
    Private Declare Function SetParent Lib "user32" Alias "SetParent" (ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As Integer
    Private Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As IntPtr
    Private Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As IntPtr, ByVal nIndex As Integer) As Integer
    Private Declare Function SetLayeredWindowAttributes Lib "user32" (ByVal hwnd As IntPtr, ByVal crKey As Integer, ByVal bAlpha As Integer, ByVal dwFlags As Integer) As Integer

    Public Const AeroPeekOpacity As Double = 0.15 'Opacity in AeroPeek model.
    Public Const ScriptUpperBound As Int16 = 22
    Public Const IconWidth As Integer = 65
    Public Const IconHeight As Integer = 90
    Public Const MainHomeURL As String = "http://www.baidu.com/"

    Public ScriptInfomation() As String ='Labels of icons.
        {"Digital Rain", "Network Attack", "AirDefence System", "Iron Man", "Attack Data",
        "3D Map", "Ballistic Missile", "Missile Deployment", "Action Indication", "Zone Isolation", "Waiting Connection",
        "Life Sustaining", "Agent Info.", "Round Oscilloscope", "Face 3DModeling", "Driving System", "Thinking Export",
        "Augmented Reality", "Combat Deployment", "UAV Camera", "NOVA 6", "Near-Earth Satellite", "Stream Decryption"}
    Public AeroPeekModel As Boolean 'AeroPeek model statu.
    Public ScriptForm(ScriptUpperBound) As WindowsTemplates 'Scripts
    Public BrowserForms As New ArrayList  'Browsers.
    Public ScriptFormVisible(ScriptUpperBound) As Boolean 'Scripts' visible.（Diffrent with Form.Visible）
    Public ScriptIcon(ScriptUpperBound) As Label
    Public NowIconIndex As Integer = -1 'Icon under mouse.
    Public RightestLoction As Integer 'The rightest icon's right location.
    Public ApplicationClosing As Boolean 'Application is going to exit.
    Public ShowMeBehind As Boolean

    Dim MouseDownLocation As Point
    Dim XDistance, YDistance As Integer
    Dim ColumnIconCount As Integer = Int(My.Computer.Screen.Bounds.Height / IconHeight) - 1
    Dim IntervalDistance As Integer = (My.Computer.Screen.Bounds.Height - ColumnIconCount * IconHeight) / (ColumnIconCount + 1) 'Distance between icons.
    Dim WallpaperIndex As Integer = 9 'Default wallpaper's ID.
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
        InfoData.Parent = WorkStationWallpaperControl
        InfoTitle.Parent = WorkStationWallpaperControl
        ConsoleButtonControl.Parent = WorkStationWallpaperControl
        XYMailButtonControl.Parent = WorkStationWallpaperControl
        XYBrowserButtonControl.Parent = WorkStationWallpaperControl
        ShutdownButtonControl.Parent = WorkStationWallpaperControl
        SettingButtonControl.Parent = WorkStationWallpaperControl

        Call SetParent(MouseDragForm.Handle, Me.Handle)
        Call SetWindowLong(MouseDragForm.Handle, -20, GetWindowLong(MouseDragForm.Handle, -20) Or &H80000)
        Call SetLayeredWindowAttributes(MouseDragForm.Handle, 0, 100, 2)

        InfoData.Location = New Point(Me.Width - InfoData.Width, 12)
        InfoTitle.Location = New Point(InfoData.Left - InfoTitle.Width, 12)
        ShutdownButtonControl.Location = New Point(Me.Width - ShutdownButtonControl.Width - IntervalDistance, Me.Height - IntervalDistance - ShutdownButtonControl.Height)
        SettingButtonControl.Location = New Point(ShutdownButtonControl.Left - IntervalDistance - SettingButtonControl.Width, ShutdownButtonControl.Top)
        XYBrowserButtonControl.Location = New Point(SettingButtonControl.Left - IntervalDistance - XYBrowserButtonControl.Width, SettingButtonControl.Top)
        XYMailButtonControl.Location = New Point(XYBrowserButtonControl.Left - IntervalDistance - XYMailButtonControl.Width, XYBrowserButtonControl.Top)
        ConsoleButtonControl.Location = New Point(XYMailButtonControl.Left - IntervalDistance - ConsoleButtonControl.Width, XYMailButtonControl.Top)

        Me.Cursor = StartingUpUI.SystemCursor
        InfoTitle.Cursor = StartingUpUI.SystemCursor
        InfoData.Cursor = StartingUpUI.SystemCursor

        CommandConsole.Show(Me)
        CommandConsole.Height = My.Computer.Screen.Bounds.Height
        CommandConsole.Location = New Point(My.Computer.Screen.Bounds.Width, 0)
        CommandConsole.CommandPast.Height = My.Computer.Screen.Bounds.Height - 120
        CommandConsole.CommandTip.Top = My.Computer.Screen.Bounds.Height - 100
        CommandConsole.CommandInputBox.Top = My.Computer.Screen.Bounds.Height - 40
    End Sub

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
            SenderControl = ScriptIcon(ScriptIndex)
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
            '激活脚本窗体
            ScriptForm(ScriptIndex).Focus()
            '如果系统弹出框显示，则应置前显示系统弹出框
            If ShutdownWindow.Visible Then ShutdownWindow.TopMost = True
            If AboutMeForm.Visible Then AboutMeForm.TopMost = True
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
        ShutdownWindow.Focus()
    End Sub

    '关机按钮响应鼠标动态效果
    Private Sub SystemWorkStation_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If Not ApplicationClosing Then
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
        AboutMeForm.Focus()
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
        Next
        '显示数据
        InfoData.Text = Int(CPUCounter.NextValue) & "%" & vbCrLf & Int(MemoryUsageRate) & "%" & vbCrLf & FormatSpeedString(DiskReadCounter.NextValue) & vbCrLf & FormatSpeedString(DiskWriteCounter.NextValue) & vbCrLf & FormatSpeedString(UploadSpeedCount) & vbCrLf & FormatSpeedString(DownloadSpeedCount)
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
            ScriptIcon(Index) = New Label
            '计算图标位置
            PointX = Int(Index / ColumnIconCount)
            PointY = Index - PointX * ColumnIconCount
            '设置图标属性
            With ScriptIcon(Index)
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
                '设置图标指向的脚本标识和位置后显示图标
                .Tag = Index.ToString("00")
                .Left = IntervalDistance + PointX * (IconWidth + IntervalDistance)
                .Top = IntervalDistance + PointY * (IconHeight + IntervalDistance)
                .Show()
            End With
            '为图标注册点击和鼠标进入、悬停、离开、按下、抬起事件
            AddHandler ScriptIcon(Index).Click, AddressOf IconTemplates_MouseClick
            AddHandler ScriptIcon(Index).MouseEnter, AddressOf IconTemplates_MouseEnter
            AddHandler ScriptIcon(Index).MouseHover, AddressOf IconTemplates_MouseHover
            AddHandler ScriptIcon(Index).MouseLeave, AddressOf IconTemplates_MouseLeave
            AddHandler ScriptIcon(Index).MouseDown, AddressOf IconTemplates_MouseDown
            AddHandler ScriptIcon(Index).MouseUp, AddressOf IconTemplates_MouseUp
        Next
        '遍历完成后记录图标显示区域最右边界
        RightestLoction = ScriptIcon(ScriptUpperBound).Left + IconWidth
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
        '播放开机提示音效，切换并激活界面
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("LoginDesktop"), AudioPlayMode.Background)
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
        Me.TopMost = MenuTopMost.Checked
        ShowMeBehind = Not MenuTopMost.Checked
        If ShowMeBehind Then SetWindowPos(Me.Handle, 1, 0, 0, 0, 0, &H10 Or &H40 Or &H2 Or &H1)
    End Sub

    Private Sub SystemWorkStation_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        '激活窗体时，判断是否需要置后
        If ShowMeBehind Then SetWindowPos(Me.Handle, 1, 0, 0, 0, 0, &H10 Or &H40 Or &H2 Or &H1)
    End Sub

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
        NewXYBrowser.Focus()
    End Sub

    Private Sub XYMailButtonControl_Click(sender As Object, e As EventArgs) Handles XYMailButtonControl.Click
        '显示/隐藏发送邮件窗体
        If XYMail.Visible Then XYMail.Hide() Else XYMail.Show(Me)
    End Sub

#Region "桌面右下角图标响应鼠标动态效果"

    Private Sub ButtonControl_MouseEnter(sender As Object, e As EventArgs) Handles XYBrowserButtonControl.MouseEnter, ConsoleButtonControl.MouseEnter, SettingButtonControl.MouseEnter, ShutdownButtonControl.MouseEnter, XYMailButtonControl.MouseEnter
        NowButton = CType(sender, Label)
        NowButton.Image = My.Resources.SystemAssets.ResourceManager.GetObject(NowButton.Tag & "1")
    End Sub

    Private Sub ButtonControl_MouseLeave(sender As Object, e As EventArgs) Handles XYBrowserButtonControl.MouseLeave, ConsoleButtonControl.MouseLeave, SettingButtonControl.MouseLeave, ShutdownButtonControl.MouseLeave, XYMailButtonControl.MouseLeave
        NowButton = CType(sender, Label)
        NowButton.Image = My.Resources.SystemAssets.ResourceManager.GetObject(NowButton.Tag & "0")
    End Sub

    Private Sub ButtonControl_MouseUp(sender As Object, e As MouseEventArgs) Handles XYBrowserButtonControl.MouseUp, ConsoleButtonControl.MouseUp, SettingButtonControl.MouseUp, ShutdownButtonControl.MouseUp, XYMailButtonControl.MouseUp
        NowButton = CType(sender, Label)
        NowButton.Image = My.Resources.SystemAssets.ResourceManager.GetObject(NowButton.Tag & "1")
    End Sub

    Private Sub ButtonControl_MouseDown(sender As Object, e As MouseEventArgs) Handles XYBrowserButtonControl.MouseDown, ConsoleButtonControl.MouseDown, SettingButtonControl.MouseDown, ShutdownButtonControl.MouseDown, XYMailButtonControl.MouseDown
        NowButton = CType(sender, Label)
        NowButton.Image = My.Resources.SystemAssets.ResourceManager.GetObject(NowButton.Tag & "2")
    End Sub
#End Region
End Class