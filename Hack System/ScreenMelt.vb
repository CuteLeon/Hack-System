Module ScreenMelt

#Region "声明区"

    'Melt screen.
    Private Declare Function GetWindowDC Lib "user32" (ByVal hwnd As Integer) As Integer
    Private Declare Function GetDesktopWindow Lib "user32" () As Integer
    Private Declare Function CreateCompatibleBitmap Lib "gdi32" (ByVal hdc As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer) As Integer
    Private Declare Function CreateCompatibleDC Lib "gdi32" (ByVal hdc As Integer) As Integer
    Private Declare Function SelectObject Lib "gdi32" (ByVal hdc As Integer, ByVal hObject As Integer) As Integer
    Private Declare Function BitBlt Lib "gdi32" (ByVal hDestDC As Integer, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hSrcDC As Integer, ByVal xSrc As Integer, ByVal ySrc As Integer, ByVal dwRop As Integer) As Integer
    Private Declare Sub InvalidateRect Lib "user32" (ByVal hwnd As Integer, lpRect As IntPtr, ByVal bErase As Integer)
    Private Const FragmentWidth = 20
    Private Const FragmentHeight = 24
    Private Const Excursion = 10
    Private Const SRCCOPY = &HCC0020
    Public Melting As Boolean 'state
    Dim X As Integer, Y As Integer
    Dim FragmentHDC， FragmentBitmap As Integer
    Dim DesktopHDC As Integer
    Dim ScreenWidth As Integer = My.Computer.Screen.Bounds.Width
    Dim ScreenHeight As Integer = My.Computer.Screen.Bounds.Height
    Dim ThreadMelt As Threading.Thread
#End Region

#Region "接口函数"

    Public Sub StartMelt()
        Melting = True
        ThreadMelt = New Threading.Thread(AddressOf Melt)
        ThreadMelt.Start()
    End Sub

    Public Sub StopMelt()
        ThreadMelt.Abort()
        Melting = False
    End Sub
#End Region

#Region "功能函数"

    Private Sub Melt()
        DesktopHDC = GetWindowDC(GetDesktopWindow())
        FragmentHDC = CreateCompatibleDC(DesktopHDC)
        FragmentBitmap = CreateCompatibleBitmap(DesktopHDC, FragmentWidth, FragmentHeight)

        SelectObject(FragmentHDC, FragmentBitmap)
        Dim Index As Integer
        Do While True
            For Index = 0 To 100
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
