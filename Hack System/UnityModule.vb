Imports System.Speech.Recognition

Module UnityModule
    Public Declare Function ReleaseCapture Lib "user32" () As Integer
    Public Declare Function SendMessageA Lib "user32" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, lParam As VariantType) As Integer
    Public Declare Function SetForegroundWindow Lib "user32" Alias "SetForegroundWindow" (ByVal hwnd As Integer) As Integer
    Public Enum TipsIconType
        Infomation = 0     '消息
        Question = 1        '询问
        Exclamation = 2    '警告
        Critical = 3            '错误
    End Enum
    Public Enum ActiveFormState
        Showing '正在显示
        Shown '已经完成显示
        MouseOut '鼠标在窗体外
        MouseIn '鼠标在窗体内
        Hiding '正在隐藏
        Hidden '已经隐藏
    End Enum
    Public Const AeroPeekOpacity As Double = 0.15 'AeroPeek视图时，未激活的脚本窗口的透明度
    Public Const ScriptUpperBound As Int16 = 22 '脚本窗体数组的下标
    Public Const IconWidth As Integer = 65 '桌面图标的宽度(不适用Size类型：结构体类型无法定义为常量)
    Public Const IconHeight As Integer = 90 '桌面图标的高度
    Public Const WallpaperUBound As Integer = 18 '桌面壁纸数组的下标
    Public Const MainHomeURL As String = "https://www.zoomeye.org/" ''浏览器默认主页 //("http://wwwwwwwww.jodi.org" 是一个很奇怪的网站)
    Public Const NegativeOpacity As Double = 0.8 '窗体失活时的透明度（需要考虑{停止呼吸}里的Me.Opacity保留小数点的位数）
    Public UserHead As Bitmap '用户头像图像
    Public UserName As String '用户名
    Public UserNameBitmap As Bitmap '用户名图像
    Public UserHeadString As String '用户头像图像Base64编码
    Public UserNameString As String '用户名图像Base64编码
    Public LockScreenMode As Boolean = False '锁屏模式或者登录模式
    Public SystemCursor As Cursor = New Cursor(My.Resources.SystemAssets.MouseCursor.GetHicon)

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
End Module
