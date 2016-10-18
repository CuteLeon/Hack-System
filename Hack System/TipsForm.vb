Imports System.ComponentModel

Public Class TipsForm

#Region "声明区"

    '置前显示，之所以不用TopMost是因为TopMost会让窗体激活，在Timer里值守置前会影响脚本窗口获取焦点
    Private Declare Sub SetWindowPos Lib "User32" (ByVal hWnd As Integer, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer)
    Public CloseMe As Boolean = False
    '显示、隐藏、等待线程
    Dim ShowThread As Threading.Thread = New Threading.Thread(AddressOf ShowTips)
    Dim HideThread As Threading.Thread = New Threading.Thread(AddressOf HideTips)
    Dim WaitThread As Threading.Thread = New Threading.Thread(AddressOf WaitForHiding)
    '绘制标题相关
    Dim TitleFont As Font = New Font("微软雅黑", 18.0!, System.Drawing.FontStyle.Bold)
    Dim TitlePoint As PointF = New PointF(0, 18)
    Dim TitleColor As Color = Color.FromArgb(235, 100, 60)
    Dim TitleBrush As Brush = New SolidBrush(TitleColor)
    '绘制消息相关
    Dim BodyFont As Font = New Font("微软雅黑", 16.0!, System.Drawing.FontStyle.Regular)
    Dim BodyPoint As PointF = New PointF(0, 48)
    Dim BodyColor As Color = Color.FromArgb(60, 100, 100)
    Dim BodyBrush As Brush = New SolidBrush(BodyColor)
    '提示窗图像相关
    Dim TipsBitmap As Bitmap
    Dim TipsInfoAera As Bitmap
    Dim TipsGraphics As Graphics
    Dim TipsInfoGraphics As Graphics
    Dim TipsIcon As Bitmap
    '浮窗区域划分
    Dim InfoAeraRectangle As Rectangle = New Rectangle(95, 5, 203, 88)
    Dim IconBackgroundRectangle As Rectangle = New Rectangle(18, 14, 70, 70)
    Dim IconRectangle As Rectangle = New Rectangle(26, 22, 54, 54)
    '关闭按钮
    Dim CloseRecangle As Rectangle = New Rectangle(311, 41, 25, 25)
    Dim CloseBitmap As Bitmap = My.Resources.TipsRes.TipsCloseButton_0
    Dim TipsTimeOut As Integer '浮窗自动关闭超时
    Dim TipsShown As Boolean = False '浮窗状态

    Dim HiddenLocation As Point '隐藏坐标
    Dim ShownLocation As Point '显示坐标

    Public Enum TipsIconType '消息类型枚举
        Infomation = 0     '消息
        Question = 1        '询问
        Exclamation = 2    '警告
        Critical = 3            '错误
    End Enum
#End Region

#Region "窗体"

    Private Sub TipsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '允许多线程访问UI
        CheckForIllegalCrossThreadCalls = False
        Me.Location = New Point(-Me.Width, 0)
        '绑定桌面键盘事件
        AddHandler Me.KeyPress, AddressOf SystemWorkStation.SystemWorkStation_KeyPress
    End Sub

    Private Sub TipsForm_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        '鼠标移动切换关闭按钮图像
        Static LastState As Boolean = False '防止相同状态重复切换消耗性能
        If Not LastState = CloseRecangle.Contains(e.X, e.Y) Then
            LastState = Not LastState
            If LastState Then
                CloseBitmap = My.Resources.TipsRes.TipsCloseButton_1
            Else
                CloseBitmap = My.Resources.TipsRes.TipsCloseButton_0
            End If
        End If
    End Sub

    Private Sub TipsForm_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        '鼠标抬起判断是否关闭
        If CloseRecangle.Contains(e.X, e.Y) Then CloseBitmap = My.Resources.TipsRes.TipsCloseButton_0 : CancelTip()
    End Sub

    Private Sub TipsForm_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        '鼠标按下判断是否切换关闭按钮图像
        If CloseRecangle.Contains(e.X, e.Y) Then CloseBitmap = My.Resources.TipsRes.TipsCloseButton_2
    End Sub

    Private Sub TipsForm_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        '鼠标进入浮窗，关闭等待线程
        If WaitThread IsNot Nothing AndAlso WaitThread.ThreadState = Threading.ThreadState.Running Then WaitThread.Abort()
    End Sub

    Private Sub TipsForm_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        '鼠标离开，开启等待线程
        If HideThread.ThreadState = Threading.ThreadState.Running Then Exit Sub
        WaitThread = New Threading.Thread(AddressOf WaitForHiding)
        WaitThread.Start()
    End Sub

    Private Sub TipsForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        '使用自定义过程替换直接关闭
        If Not CloseMe Then
            e.Cancel = True
            CancelTip()
        End If
    End Sub

    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get 'Alpha通道绘图相关
            If Not DesignMode Then
                Dim cp As CreateParams = MyBase.CreateParams
                cp.ExStyle = cp.ExStyle Or WS_EX_LAYERED
                Return cp
            Else
                Return MyBase.CreateParams
            End If
        End Get
    End Property
#End Region

#Region "接口函数"

    ''' <summary>
    ''' 弹出信息浮窗
    ''' </summary>
    ''' <param name="TipTitle">浮窗标题</param>
    ''' <param name="TipIcon">浮窗图标种类</param>
    ''' <param name="TipBody">浮窗消息文本</param>
    ''' <param name="TimeOut">浮窗自动隐藏超时</param>
    Public Sub PopupTips(ByVal TipTitle As String, ByVal TipIcon As TipsIconType, ByVal TipBody As String, Optional ByVal TimeOut As Integer = 5000)
        '弹出浮窗函数
        Me.TopMost = True
        My.Computer.Audio.Play(My.Resources.TipsRes.TipsAlarm, AudioPlayMode.Background)
        '如果已经显示，则立即隐藏后重新弹出浮窗
        If WaitThread IsNot Nothing AndAlso WaitThread.ThreadState = Threading.ThreadState.Running Then WaitThread.Abort() : WaitThread = Nothing
        If ShowThread.ThreadState = Threading.ThreadState.Running Then
            ShowThread.Abort()
            ShowThread = Nothing
            HideThread = New Threading.Thread(AddressOf HideTips)
            HideThread.Start()
            HideThread.Join()
        ElseIf ShowThread.ThreadState = Threading.ThreadState.Running Then
            HideThread.Join()
        ElseIf TipsShown Then
            HideThread = Nothing
            HideThread = New Threading.Thread(AddressOf HideTips)
            HideThread.Start()
            HideThread.Join()
        End If
        IconTimer.Start()
        '重新绘制浮窗图像
        TipsBitmap = My.Resources.TipsRes.TipsBackground
        TipsGraphics = Graphics.FromImage(TipsBitmap)
        TipsInfoAera = My.Resources.TipsRes.TipsBackground.Clone(InfoAeraRectangle, Imaging.PixelFormat.Format32bppArgb)
        TipsInfoGraphics = Graphics.FromImage(TipsInfoAera)

        TipsTimeOut = TimeOut
        TipsIcon = My.Resources.TipsRes.TipsIcons.Clone(New Rectangle(TipIcon * IconRectangle.Width, 0, IconRectangle.Width, IconRectangle.Height), Imaging.PixelFormat.Format32bppArgb)
        TipsInfoGraphics.DrawString(TipTitle, TitleFont, TitleBrush, TitlePoint)
        TipsInfoGraphics.DrawString(TipBody, BodyFont, BodyBrush, BodyPoint)
        TipsGraphics.DrawImage(TipsInfoAera, InfoAeraRectangle)

        TipsGraphics.DrawImage(My.Resources.TipsRes.TipsIconBackground.Clone(New Rectangle(0, 0, IconBackgroundRectangle.Width, IconBackgroundRectangle.Height), Imaging.PixelFormat.Format32bppArgb), IconBackgroundRectangle)
        TipsGraphics.DrawImage(TipsIcon, IconRectangle)
        TipsGraphics.DrawImage(CloseBitmap, CloseRecangle)


        DrawImage(Me, TipsBitmap)

        TipsInfoGraphics.Dispose()
        TipsGraphics.Dispose()
        TipsBitmap.Dispose()
        '重新弹出
        ShowThread = Nothing
        ShowThread = New Threading.Thread(AddressOf ShowTips)
        ShowThread.Start()
        ShowThread.Join()
        '开启等待线程
        TipsShown = True
        WaitThread = New Threading.Thread(AddressOf WaitForHiding)
        WaitThread.Start()
    End Sub

    ''' <summary>
    ''' 收回浮窗
    ''' </summary>
    Public Sub CancelTip()
        If Not Me.Visible Then Exit Sub

        CloseMe = True
        If ShowThread.ThreadState = Threading.ThreadState.Running Then
            ShowThread.Abort()
            ShowThread = Nothing
        ElseIf WaitThread.ThreadState = Threading.ThreadState.Running Then
            WaitThread.Abort()
            WaitThread = Nothing
        End If

        If Not HideThread.ThreadState = Threading.ThreadState.Running Then
            HideThread = New Threading.Thread(AddressOf HideTips)
            HideThread.Start()
        End If
        HideThread.Join()
        Me.Close()
    End Sub
#End Region

#Region "动态显示和隐藏"

    Private Sub ShowTips()
        '动态显示浮窗
        HiddenLocation = Nothing : ShownLocation = Nothing
        HiddenLocation = New Point(My.Computer.Screen.Bounds.Width, 0.8 * (My.Computer.Screen.Bounds.Height - Me.Height))
        ShownLocation = New Point(HiddenLocation.X - Me.Width, HiddenLocation.Y)

        Me.Location = HiddenLocation
        Do While Me.Left > ShownLocation.X
            Me.Left -= 20
            Threading.Thread.Sleep(10)
        Loop
        Me.Location = ShownLocation
    End Sub

    Private Sub HideTips()
        '动态隐藏浮窗
        If WaitThread IsNot Nothing AndAlso WaitThread.ThreadState = Threading.ThreadState.Running Then
            WaitThread.Abort()
            WaitThread = Nothing
        End If
        Me.Location = ShownLocation
        Do While Me.Left < HiddenLocation.X
            Me.Left += 20
            Threading.Thread.Sleep(10)
        Loop
        Me.Location = HiddenLocation

        TipsShown = False
    End Sub
#End Region

#Region "功能函数"

    ''' <summary>
    ''' 等待自动收回浮窗
    ''' </summary>
    Private Sub WaitForHiding()
        Threading.Thread.Sleep(TipsTimeOut)
        If WaitThread IsNot Nothing AndAlso WaitThread.ThreadState = Threading.ThreadState.Running Then
            HideThread = New Threading.Thread(AddressOf HideTips)
            HideThread.Start()
            IconTimer.Stop()
            HideThread.Join()
            CloseMe = True
            Me.Close()
        End If
    End Sub
#End Region

#Region "控件"

    Private Sub IconTimer_Tick(sender As Object, e As EventArgs) Handles IconTimer.Tick
        On Error Resume Next
        '浮窗刷新"引擎"
        Static IconBackgroundIndex As Integer = 0
        Dim TempIconBackground As Bitmap = My.Resources.TipsRes.TipsIconBackground.Clone(New Rectangle(IconBackgroundIndex * IconBackgroundRectangle.Width, 0, IconBackgroundRectangle.Width, IconBackgroundRectangle.Height), Imaging.PixelFormat.Format32bppArgb)
        SetWindowPos(Me.Handle, -1, 0, 0, 0, 0, &H10 Or &H40 Or &H2 Or &H1)

        TipsBitmap = My.Resources.TipsRes.TipsBackground
        TipsGraphics = Graphics.FromImage(TipsBitmap)
        TipsGraphics.DrawImage(TempIconBackground, IconBackgroundRectangle)
        TipsGraphics.DrawImage(TipsInfoAera, InfoAeraRectangle)
        TipsGraphics.DrawImage(TipsIcon, IconRectangle)
        TipsGraphics.DrawImage(CloseBitmap, CloseRecangle)

        DrawImage(Me, TipsBitmap)
        IconBackgroundIndex += IIf(IconBackgroundIndex = 11, -11, 1)

        TipsGraphics.Dispose()
        TipsBitmap.Dispose()
        TempIconBackground.Dispose()
    End Sub

#End Region

End Class
