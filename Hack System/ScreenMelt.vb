Module ScreenMelt

#Region "声明区"

    '屏幕融化
    Private Declare Function GetWindowDC Lib "user32" (ByVal hwnd As Integer) As Integer
    Private Declare Function GetDesktopWindow Lib "user32" () As Integer
    Private Declare Function CreateCompatibleBitmap Lib "gdi32" (ByVal hdc As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer) As Integer
    Private Declare Function CreateCompatibleDC Lib "gdi32" (ByVal hdc As Integer) As Integer
    Private Declare Function SelectObject Lib "gdi32" (ByVal hdc As Integer, ByVal hObject As Integer) As Integer
    Private Declare Function BitBlt Lib "gdi32" (ByVal hDestDC As Integer, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hSrcDC As Integer, ByVal xSrc As Integer, ByVal ySrc As Integer, ByVal dwRop As Integer) As Integer
    Private Declare Sub InvalidateRect Lib "user32" (ByVal hwnd As Integer, lpRect As IntPtr, ByVal bErase As Integer)
    Private Const FragmentWidth = 20 '碎片宽度
    Private Const FragmentHeight = 24 '碎片高度
    Private Const Excursion = 10 '偏移距离
    Private Const SRCCOPY = &HCC0020
    Public Melting As Boolean '融化状态
    Dim X As Integer, Y As Integer
    Dim FragmentHDC， FragmentBitmap As Integer
    Dim DesktopHDC As Integer
    Dim ScreenWidth As Integer = My.Computer.Screen.Bounds.Width
    Dim ScreenHeight As Integer = My.Computer.Screen.Bounds.Height
    Dim ThreadMelt As Threading.Thread '融化线程
#End Region

#Region "接口函数"

    Public Sub StartMelt()
        '开启融化线程
        If Melting Then Exit Sub
        Melting = True
        ThreadMelt = New Threading.Thread(AddressOf Melt)
        ThreadMelt.Start()
    End Sub

    Public Sub StopMelt()
        '关闭融化线程
        If Not Melting Then Exit Sub
        ThreadMelt.Abort()
        Melting = False
    End Sub
#End Region

#Region "功能函数"

    Private Sub Melt()
        '融化函数
        DesktopHDC = GetWindowDC(GetDesktopWindow())
        FragmentHDC = CreateCompatibleDC(DesktopHDC)
        FragmentBitmap = CreateCompatibleBitmap(DesktopHDC, FragmentWidth, FragmentHeight)

        SelectObject(FragmentHDC, FragmentBitmap)
        Dim Index As Integer
        '无限循环，直到关闭线程
        Do While True
            For Index = 0 To 100
                '随机拷贝屏幕碎片并随机绘制到周围区域
                X = (My.Computer.Screen.Bounds.Width) * Rnd()
                Y = (My.Computer.Screen.Bounds.Height) * Rnd()
                BitBlt(FragmentHDC, 0, 0, FragmentWidth, FragmentHeight, DesktopHDC, X, Y, SRCCOPY)
                X = X + (Excursion - (Excursion * 2) * Rnd())
                Y = Y + (Excursion - (Excursion * 2) * Rnd())
                BitBlt(DesktopHDC, X, Y, FragmentWidth, FragmentHeight, FragmentHDC, 0, 0, SRCCOPY)
            Next
            Threading.Thread.Sleep(1)
        Loop
    End Sub
#End Region

End Module
