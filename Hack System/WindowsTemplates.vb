Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Threading

Public Class WindowsTemplates

#Region "声明区"

    '允许鼠标通过控件拖动窗体的API
    Public Declare Function ReleaseCapture Lib "user32" () As Integer
    Public Declare Function SendMessageA Lib "user32" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, lParam As VariantType) As Integer
    '常量
    Private Const TitleHeight As Integer = 25 '上边界多出的标题栏高度
    Private Const BorderWidth As Integer = 2 'GIF控件与窗体的基本边界距离
    Public Const NegativeOpacity As Double = 0.8 '窗体失活时的透明度（需要考虑{停止呼吸}里的Me.Opacity保留小数点的位数）
    '变量
    Dim TitleTextBrush As Brush = Brushes.White '标题文本颜色
    Dim BoundaryPen As Pen = Pens.Red '窗体边界线条的颜色
    Dim BorderColor_Active As Color = Color.DarkSlateGray '窗体激活时边框颜色
    Dim BorderColor_Negative As Color = Color.Black '窗体失活时边框颜色
    Dim MyScriptIndex As Integer '自己对应的脚本标识
    Dim IconLocation As Point '与自己对应的图标在桌面上的位置
    Dim IconImage As Image '图标图像
    Dim IconControl As Label '在桌面环境里对应的图标控件
#End Region

#Region "窗体"

    Private Sub WindowsTemplates_Load(sender As Object, e As EventArgs) Handles Me.Load
        '允许多线程访问UI
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        MyScriptIndex = Int(Me.Tag)
        '设置GIF控件左距离和图像
        Me.Icon = Icon.FromHandle(CType(My.Resources.SystemAssets.ResourceManager.GetObject("ScriptIcon_" & MyScriptIndex.ToString("00")), Bitmap).GetHicon)
        With GIFControl
            .Left = BorderWidth
            .Image = My.Resources.SystemAssets.ResourceManager.GetObject("HackScript_" & Me.Tag)
        End With
        '调用初始化过程
        SetMeToDefaultSetting()
        '设置窗体背景色、标题、鼠标样式
        Me.BackColor = BorderColor_Active
        Me.Name = SystemWorkStation.ScriptInfomation(Me.Tag)
        Me.Cursor = StartingUpUI.SystemCursor
        '初始化关闭按钮
        CloseButtonControl.Image = My.Resources.SystemAssets.CloseButton.Clone(New Rectangle(0, 0, 27, 27), Imaging.PixelFormat.Format32bppArgb)
        '注册桌面环境的按键事件，和桌面响应同样的按键事件
        AddHandler Me.KeyPress, AddressOf SystemWorkStation.SystemWorkStation_KeyPress
        '为窗体绘制左上角图标、标题文字和边界线条
        Dim WindowsBitmap As New Bitmap(Me.Width, Me.Height)
        Dim WindowsGraphics As Graphics = Graphics.FromImage(WindowsBitmap)
        WindowsGraphics.SmoothingMode = SmoothingMode.HighQuality
        WindowsGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality
        WindowsGraphics.DrawImage(My.Resources.SystemAssets.ResourceManager.GetObject("ScriptIcon_" & Me.Tag), BorderWidth, BorderWidth, 24, 24)
        WindowsGraphics.DrawString(SystemWorkStation.ScriptInfomation(Me.Tag), CloseButtonControl.Font, TitleTextBrush, 30, (TitleHeight + BorderWidth - FontHeight) / 2)
        WindowsGraphics.DrawRectangle(BoundaryPen, 0, 0, Me.Width, Me.Height)
        Me.BackgroundImage = WindowsBitmap
        WindowsGraphics.Dispose()
    End Sub

    Private Sub WindowsTemplates_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If Not SystemWorkStation.SystemClosing Then
            '不是退出程序时，关闭窗体调用关闭过程
            e.Cancel = True
            CloseScript()
        End If
    End Sub

    Private Sub WindowsTemplates_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        '窗体失去焦点时，如果不是AeroPeek模式也不是正在退出程序，就降低窗口透明度
        If Not (SystemWorkStation.AeroPeekModel) AndAlso Not SystemWorkStation.SystemClosing _
            AndAlso SystemWorkStation.ScriptFormVisible(MyScriptIndex) = True Then Me.Opacity = NegativeOpacity
        '改变窗体背景色
        Me.BackColor = BorderColor_Negative
        '隐藏标题栏
        GIFControl.Top = BorderWidth
        CloseButtonControl.Hide()
        Me.Height = GIFControl.Height + 2 * BorderWidth
        Me.Top += TitleHeight
    End Sub

    Private Sub WindowsTemplates_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
        '防止在显示动态关闭特效时被激活
        If Not SystemWorkStation.ScriptFormVisible(MyScriptIndex) Then Exit Sub
        '判断AeroPeek模式状态和当前脚本标识，设置窗体透明度
        Me.Opacity = IIf(SystemWorkStation.AeroPeekModel And SystemWorkStation.NowIconIndex <> MyScriptIndex, SystemWorkStation.AeroPeekOpacity, 1)
        '改变背景色
        Me.BackColor = BorderColor_Active
        '优先显示系统弹出框
        If ShutdownTips.Visible Then ShutdownTips.Hide()
        '重新显示标题栏
        GIFControl.Top = TitleHeight + BorderWidth
        CloseButtonControl.Show()
        Me.Height = GIFControl.Height + TitleHeight + 2 * BorderWidth
        Me.Top -= TitleHeight
    End Sub

    Private Sub WindowsTemplates_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible Then
            '恢复窗体到初始设置
            SetMeToDefaultSetting()
        End If
    End Sub
#End Region

#Region "功能函数"

    Private Sub MoveWindow(sender As Object, e As MouseEventArgs) Handles Me.MouseDown, GIFControl.MouseDown
        '鼠标通过控件拖动窗体
        ReleaseCapture()
        SendMessageA(Me.Handle, &HA1, 2, 0&)
    End Sub

    Private Sub HideMe() '动态关闭特效的线程
        '计算窗体位置和尺寸没每次应该变化的数值
        Dim EachXDis, EachYDis, EachWidthDis, EachHeightDis As Integer
        EachXDis = (Me.Left - IconLocation.X) \ 7
        EachYDis = (Me.Top - IconLocation.Y) \ 7
        EachWidthDis = (Me.Width - SystemWorkStation.IconWidth) \ 7
        EachHeightDis = (Me.Height - SystemWorkStation.IconHeight) \ 7
        '开始特效，窗口透明度为0.3时正式关闭
        Do While Me.Opacity > 0
            '降低透明度
            Me.Opacity -= 0.1
            '缩小GIF控件
            GIFControl.Width -= EachWidthDis
            GIFControl.Height -= EachHeightDis
            '缩小自身窗体
            Me.Width -= EachWidthDis
            Me.Height -= EachHeightDis
            '窗体向桌面图标移动
            Me.Left -= EachXDis
            Me.Top -= EachYDis
            '关闭按钮向左移动
            CloseButtonControl.Left = Me.Width - CloseButtonControl.Width
            '线程暂停30毫秒
            Thread.Sleep(30)
        Loop
        '取消桌面图标高亮效果
        IconControl.Image = IconImage
        Me.Hide()
    End Sub

    Private Sub SetMeToDefaultSetting()
        '根据脚本GIF设置控件和窗体尺寸
        GIFControl.Size = GIFControl.Image.Size
        Me.Width = GIFControl.Width + 2 * BorderWidth
        Me.Height = GIFControl.Height + TitleHeight + 2 * BorderWidth
        '恢复GIF控件和关闭按钮位置
        GIFControl.Top = TitleHeight + BorderWidth
        CloseButtonControl.Location = New Point(Me.Width - CloseButtonControl.Width, 0)
        '脚本窗体随机定位到桌面右侧
        VBMath.Randomize()
        Me.Left = (SystemWorkStation.Width - Me.Width - SystemWorkStation.RightestLoction) * VBMath.Rnd + SystemWorkStation.RightestLoction
        Me.Top = (SystemWorkStation.Height - Me.Height) * VBMath.Rnd
    End Sub

#End Region

#Region "接口函数"

    Public Sub Breath()
        If BreathTimer.Enabled Then
            BreathTimer.Stop()
            UnBreathTimer.Start()
        Else '开始呼吸
            UnBreathTimer.Stop()
            BreathTimer.Start()
        End If
    End Sub
#End Region

#Region "控件"

    Private Sub CloseButtonControl_Click(sender As Object, e As EventArgs) Handles CloseButtonControl.Click
        '点击关闭按钮调用关闭过程
        CloseScript()
    End Sub

    Private Sub BreathTimer_Tick(sender As Object, e As EventArgs) Handles BreathTimer.Tick
        '呼吸
        Static Difference As Single = 0.05
        If Me.Opacity = 0 Or Me.Opacity = 1 Then Difference = -Difference
        Me.Opacity += Difference
    End Sub

    Private Sub UnBreathTimer_Tick(sender As Object, e As EventArgs) Handles UnBreathTimer.Tick
        '呼吸到对应的透明度后停止呼吸
        Static EndOpacity As Double = IIf(ActiveForm Is Me, 1, NegativeOpacity)
        Me.Opacity += IIf(Me.Opacity > EndOpacity, -0.05, 0.05)
        If Math.Round(Me.Opacity, 1) = EndOpacity Then UnBreathTimer.Stop()
    End Sub

#Region "关闭按钮动态效果"

    Private Sub CloseButtonControl_MouseEnter(sender As Object, e As EventArgs) Handles CloseButtonControl.MouseEnter
        If Not (ShutdownTips.Visible) And Not (AboutMeForm.Visible) Then CloseButtonControl.Image = My.Resources.SystemAssets.CloseButton.Clone(New Rectangle(27, 0, 27, 27), Imaging.PixelFormat.Format32bppArgb)
    End Sub

    Private Sub CloseButtonControl_MouseLeave(sender As Object, e As EventArgs) Handles CloseButtonControl.MouseLeave
        CloseButtonControl.Image = My.Resources.SystemAssets.CloseButton.Clone(New Rectangle(0, 0, 27, 27), Imaging.PixelFormat.Format32bppArgb)
    End Sub

    Private Sub CloseButtonControl_MouseUp(sender As Object, e As MouseEventArgs) Handles CloseButtonControl.MouseUp
        If Not (ShutdownTips.Visible) And Not (AboutMeForm.Visible) Then CloseButtonControl.Image = My.Resources.SystemAssets.CloseButton.Clone(New Rectangle(27, 0, 27, 27), Imaging.PixelFormat.Format32bppArgb)
    End Sub

    Private Sub CloseButtonControl_MouseDown(sender As Object, e As MouseEventArgs) Handles CloseButtonControl.MouseDown
        If Not (ShutdownTips.Visible) And Not (AboutMeForm.Visible) Then CloseButtonControl.Image = My.Resources.SystemAssets.CloseButton.Clone(New Rectangle(54, 0, 27, 27), Imaging.PixelFormat.Format32bppArgb)
    End Sub
#End Region

#End Region

#Region "动态隐藏"

    Private Sub CloseScript() '关闭脚本过程
        '记录脚本状态
        SystemWorkStation.ScriptFormVisible(MyScriptIndex) = False
        '读取自身的脚本标识和需要的在桌面环境里的数据
        IconLocation.X = SystemWorkStation.ScriptIcons(MyScriptIndex).Left
        IconLocation.Y = SystemWorkStation.ScriptIcons(MyScriptIndex).Top
        IconImage = My.Resources.SystemAssets.ResourceManager.GetObject("ScriptIcon_" & Me.Tag)
        IconControl = SystemWorkStation.ScriptIcons(MyScriptIndex)
        '关闭呼吸
        BreathTimer.Stop()
        UnBreathTimer.Stop()
        '播放提示音
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("ScriptStoped"), AudioPlayMode.Background)
        '遍历脚本窗体，将焦点交给下一个标识的脚本窗体
        For ScriptIndex As Integer = SystemWorkStation.ScriptUpperBound To 0 Step -1
            If SystemWorkStation.ScriptFormVisible(ScriptIndex) Then
                '如果当前窗口正在被AeroPeek模式高亮显示，关闭后应退出AeroPeek模式
                If SystemWorkStation.AeroPeekModel And SystemWorkStation.NowIconIndex = MyScriptIndex Then
                    '遍历脚本窗体
                    For TempScriptIndex As Integer = 0 To SystemWorkStation.ScriptUpperBound
                        '还原在AeroPeek模式下被隐藏的窗体的透明度
                        If SystemWorkStation.ScriptFormVisible(TempScriptIndex) Then SystemWorkStation.ScriptForm(TempScriptIndex).Opacity = WindowsTemplates.NegativeOpacity
                    Next
                    '关闭AeroPeek模式
                    SystemWorkStation.AeroPeekModel = False
                    '被激活的窗体不被透明
                    If Not (ActiveForm Is Nothing) Then ActiveForm.Opacity = 1
                End If
                '启动动态关闭特效的线程
                ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf HideMe))
                '找到了，激活下一个标识的脚本窗体
                SystemWorkStation.SetForegroundWindow(SystemWorkStation.ScriptForm(ScriptIndex).Handle)
                Exit Sub
            End If
        Next
        '未找到，将焦点交给桌面
        SystemWorkStation.SetForegroundWindow(SystemWorkStation.Handle)
        '启动动态关闭特效的线程
        ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf HideMe))
    End Sub
#End Region

End Class