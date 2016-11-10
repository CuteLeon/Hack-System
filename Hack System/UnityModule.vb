Imports System.Speech.Recognition

Public Module UnityModule
#Region "API"
    ''' <summary>
    ''' 为当前的应用程序释放鼠标捕获
    ''' </summary>
    Public Declare Function ReleaseCapture Lib "user32" () As Integer
    ''' <summary>
    ''' 为句柄消息队列发送新消息
    ''' </summary>
    Public Declare Function SendMessageA Lib "user32" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, lParam As VariantType) As Integer
    ''' <summary>
    ''' 激活指定窗体作为活动窗体（不置前显示）
    ''' </summary>
    ''' <param name="hwnd">要激活的窗体句柄</param>
    Public Declare Function SetForegroundWindow Lib "user32" Alias "SetForegroundWindow" (ByVal hwnd As Integer) As Integer
#End Region

#Region "枚举"
    ''' <summary>
    ''' 动态显示的窗体当前所处的状态
    ''' </summary>
    Public Enum ActiveFormState
        ''' <summary>
        ''' 正在动态显示
        ''' </summary>
        Showing
        ''' <summary>
        ''' 已经完成动态显示
        ''' </summary>
        Shown
        ''' <summary>
        ''' 鼠标在窗体外
        ''' </summary>
        MouseOut
        ''' <summary>
        ''' 鼠标在窗体内
        ''' </summary>
        MouseIn
        ''' <summary>
        ''' 正在动态隐藏
        ''' </summary>
        Hiding
        ''' <summary>
        ''' 已经完成动态隐藏
        ''' </summary>
        Hidden
    End Enum

    ''' <summary>
    ''' 浮窗图标类型枚举
    ''' </summary>
    Public Enum TipsIconType
        Infomation = 0     '消息
        Question = 1        '询问
        Exclamation = 2    '警告
        Critical = 3            '错误
    End Enum
#End Region

#Region "全局常量"
    ''' <summary>
    ''' AeroPeek视图时，未激活的脚本窗口的透明度
    ''' </summary>
    Public Const AeroPeekOpacity As Double = 0.15
    ''' <summary>
    ''' 脚本窗体数组的下标
    ''' </summary>
    Public Const ScriptUpperBound As Int16 = 22
    ''' <summary>
    ''' 桌面图标的宽度
    ''' </summary>
    Public Const IconWidth As Integer = 65
    ''' <summary>
    ''' 桌面图标的高度
    ''' </summary>
    Public Const IconHeight As Integer = 90
    ''' <summary>
    ''' 桌面壁纸数组的下标
    ''' </summary>
    Public Const WallpaperUBound As Integer = 18
    ''' <summary>
    ''' 浏览器默认主页 //("http://wwwwwwwww.jodi.org" 是一个很奇怪的网站)
    ''' </summary>
    Public Const MainHomeURL As String = "https://www.zoomeye.org/"
    ''' <summary>
    ''' '窗体失活时的透明度（需要考虑{停止呼吸}里的Me.Opacity保留小数点的位数）
    ''' </summary>
    Public Const NegativeOpacity As Double = 0.8
    ''' <summary>
    ''' 默认的像素格式
    ''' </summary>
    Public Const DefaultPixelFormat As Imaging.PixelFormat = Imaging.PixelFormat.Format32bppArgb
#End Region

#Region "全局变量"
    ''' <summary>
    ''' 用户头像图像
    ''' </summary>
    Public UserHead As Bitmap
    ''' <summary>
    ''' 用户名
    ''' </summary>
    Public UserName As String
    ''' <summary>
    ''' 用户名图像
    ''' </summary>
    Public UserNameBitmap As Bitmap
    ''' <summary>
    ''' 用户头像图像Base64编码
    ''' </summary>
    Public UserHeadString As String
    ''' <summary>
    ''' 用户名图像Base64编码
    ''' </summary>
    Public UserNameString As String
    ''' <summary>
    ''' 系统全局鼠标样式
    ''' </summary>
    Public SystemCursor As Cursor = New Cursor(My.Resources.SystemAssets.MouseCursor.GetHicon)
    ''' <summary>
    ''' 语音识别引擎
    ''' </summary>
    Public MySpeechRecognitionEngine As SpeechRecognitionEngine
    ''' <summary>
    ''' 桌面图标对应的文本
    ''' </summary>
    Public ScriptInfomation() As String = {"Digital Rain", "Network Attack", "Air Defence", "Iron Man", "Attack Data", "3D Map", "Ballistic Missile", "Missile", "Action Indication", "Zone Isolation", "Waiting...", "Life Support", "Agent Info.", "Graphic SO", "Face 3DModel", "Driving System", "Thinking Export", "ARToolkit", "Combat", "UAV Camera", "NOVA 6", "Satellite", "Decrypt"}
    ''' <summary>
    ''' 桌面图标(脚本)对应的语音命令
    ''' </summary>
    Public ScriptSpeechGrammar() As String = {"打开数字雨", "打开网络攻击", "打开防空系统", "打开钢铁侠", "打开攻击数据", "打开三维地图", "打开弹道导弹", "打开导弹部署", "打开行动指示", "打开区域隔离", "打开等待连接", "打开生命维护系统", "打开特工信息", "打开示波器", "打开面部模型", "打开驱动系统", "打开思维导出系统", "打开增强现实", "打开作战部署", "打开无人机", "打开新星", "打开近地卫星", "打开解密"}
    ''' <summary>
    ''' AeroPeek视图的开关
    ''' </summary>
    Public AeroPeekMode As Boolean
    ''' <summary>
    ''' 脚本窗口
    ''' </summary>
    Public ScriptForm(ScriptUpperBound) As WindowsTemplates
    ''' <summary>
    ''' 开启的浏览器窗口
    ''' </summary>
    Public BrowserForms As New ArrayList
    ''' <summary>
    ''' 脚本开启状态(不同于Form.Visible，两者配合判断脚本的状态)
    ''' ScriptFormShown为真，Visible为假：窗体正在动态显示或隐藏
    ''' ScriptFormShown为假，Visible为假：窗体已经关闭
    ''' ScriptFormShown为真，Visible为真：窗体已经稳定显示
    ''' </summary>
    Public ScriptFormShown(ScriptUpperBound) As Boolean
    ''' <summary>
    ''' 桌面脚本图标
    ''' </summary>
    Public ScriptIcons(ScriptUpperBound) As Label
    ''' <summary>
    ''' 鼠标进入的桌面图标标识(鼠标离开为-1)
    ''' </summary>
    Public NowIconIndex As Integer = -1
    ''' <summary>
    ''' 最右图标的横坐标(作为脚本随机位置的左界)
    ''' </summary>
    Public RightestLoction As Integer
    ''' <summary>
    ''' 系统正在关闭
    ''' </summary>
    Public SystemClosing As Boolean
    ''' <summary>
    ''' 语音识别引擎的状态
    ''' </summary>
    Public SpeechRecognitionMode As Boolean = True
#End Region

#Region "全局函数"
    ''' <summary>
    ''' 模拟 QQ 抖动窗体
    ''' </summary>
    ''' <param name="Radius">抖动半径</param>
    ''' <param name="Count">抖动圈数</param>
    Public Sub QQ_Vibration(Form As Form, Optional ByVal Radius As Integer = 2, Optional ByVal Count As Integer = 3)
        Dim InitPoint As Point = Form.Location '记录原始窗体坐标
        Form.Refresh()
        UnityModule.SetForegroundWindow(Form.Handle)
        For VibrationIndex As Integer = 0 To Count '抖动次数
            For Index As Integer = -Radius To Radius
                Dim X As Integer = Math.Sqrt(Radius * Radius - Index * Index)
                Dim Y As Integer = Index
                Form.Location = New Point(InitPoint.X + X, InitPoint.Y + Y)
                Threading.Thread.Sleep(10)
            Next
            For Index As Integer = Radius To -Radius Step -1
                Dim X As Integer = Math.Sqrt(Radius * Radius - Index * Index)
                Dim Y As Integer = -Index
                Form.Location = New Point(InitPoint.X - X, InitPoint.Y - Y)
                Threading.Thread.Sleep(5)
            Next
        Next
        Form.Location = InitPoint
    End Sub
#End Region

End Module
