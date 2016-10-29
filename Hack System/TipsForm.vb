Imports System.ComponentModel

Public Class TipsForm

#Region "声明区"
    ''' <summary>
    ''' 置前显示（使用 TopMost 会让窗体激活，在Timer里值守置前会影响脚本窗口获取焦点）
    ''' </summary>
    Private Declare Sub SetWindowPos Lib "User32" (ByVal hWnd As Integer, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer)
    ''' <summary>
    ''' 当前窗体状态（用于调度动态显示和隐藏）
    ''' </summary>
    Dim MyState As ActiveFormState = ActiveFormState.Hidden
    ''' <summary>
    ''' 显示线程
    ''' </summary>
    Dim ShowThread As Threading.Thread ' = New Threading.Thread(AddressOf ShowTips)
    ''' <summary>
    ''' 隐藏线程
    ''' </summary>
    Dim HideThread As Threading.Thread ' = New Threading.Thread(AddressOf HideTips)
    ''' <summary>
    ''' 等待隐藏线程
    ''' </summary>
    Dim WaitThread As Threading.Thread ' = New Threading.Thread(AddressOf WaitForHiding)
    ''' <summary>
    ''' 消息标题字体
    ''' </summary>
    Dim TitleFont As Font = New Font("微软雅黑", 18.0!, System.Drawing.FontStyle.Bold)
    ''' <summary>
    ''' 消息标题位置
    ''' </summary>
    Dim TitlePoint As PointF = New PointF(0, 18)
    ''' <summary>
    ''' 消息标题颜色
    ''' </summary>
    Dim TitleColor As Color = Color.FromArgb(235, 100, 60)
    ''' <summary>
    ''' 消息标题笔刷
    ''' </summary>
    Dim TitleBrush As Brush = New SolidBrush(TitleColor)
    ''' <summary>
    ''' 消息内容字体
    ''' </summary>
    Dim BodyFont As Font = New Font("微软雅黑", 16.0!, System.Drawing.FontStyle.Regular)
    ''' <summary>
    ''' 消息内容位置
    ''' </summary>
    Dim BodyPoint As PointF = New PointF(0, 48)
    ''' <summary>
    ''' 消息内容颜色
    ''' </summary>
    Dim BodyColor As Color = Color.FromArgb(120, 140, 150)
    ''' <summary>
    ''' 消息内容笔刷
    ''' </summary>
    Dim BodyBrush As Brush = New SolidBrush(BodyColor)
    ''' <summary>
    ''' 浮窗的图像
    ''' </summary>
    Dim TipsBitmap As Bitmap
    ''' <summary>
    ''' 浮窗的消息区域图像
    ''' </summary>
    Dim TipsInfoAera As Bitmap
    ''' <summary>
    ''' 消息区域的坐标和尺寸
    ''' </summary>
    Dim InfoAeraRectangle As Rectangle = New Rectangle(95, 5, 203, 88)
    ''' <summary>
    ''' 图标背景的坐标和尺寸
    ''' </summary>
    Dim IconBackgroundRectangle As Rectangle = New Rectangle(18, 14, 70, 70)
    ''' <summary>
    ''' 图标的坐标和尺寸
    ''' </summary>
    Dim IconRectangle As Rectangle = New Rectangle(26, 22, 54, 54)
    ''' <summary>
    ''' 图标图像
    ''' </summary>
    Dim TipsIcon As Bitmap
    ''' <summary>
    ''' 关闭按钮的区域
    ''' </summary>
    Dim CloseRecangle As Rectangle = New Rectangle(311, 41, 25, 25)
    ''' <summary>
    ''' 关闭按钮的图像
    ''' </summary>
    Dim CloseBitmap As Bitmap = My.Resources.TipsRes.TipsCloseButton_0
    ''' <summary>
    ''' 等待自动隐藏的超时（单位：毫秒）
    ''' </summary>
    Dim TipsTimeOut As Integer
    ''' <summary>
    ''' 隐藏到的位置
    ''' </summary>
    Dim HiddenLocation As Point
    ''' <summary>
    ''' 显示到的位置
    ''' </summary>
    Dim ShownLocation As Point
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

#Region "接口函数"
    ''' <summary>
    ''' 弹出消息浮窗
    ''' </summary>
    ''' <param name="OwnerForm">浮窗拥有者个窗体</param>
    ''' <param name="TipTitle">浮窗消息标题</param>
    ''' <param name="TipIcon">浮窗消息图标类型</param>
    ''' <param name="TipBody">浮窗消息内容</param>
    ''' <param name="TimeOut">等待自动隐藏超时（默认为6000毫秒）</param>
    Public Sub PopupTips(ByVal OwnerForm As Form, ByVal TipTitle As String, ByVal TipIcon As TipsIconType, ByVal TipBody As String, Optional ByVal TimeOut As Integer = 6000)
        My.Computer.Audio.Play(My.Resources.TipsRes.TipsAlarm, AudioPlayMode.Background)
        If Not Me.Visible Then Me.Show(OwnerForm)
        UnityModule.SetForegroundWindow(Me.Handle)

        Select Case MyState
            Case ActiveFormState.Showing
                ShowThread.Abort()
                ShowThread.DisableComObjectEagerCleanup()
            Case ActiveFormState.MouseOut
                WaitThread.Abort()
                WaitThread.DisableComObjectEagerCleanup()
            Case ActiveFormState.MouseIn
            Case ActiveFormState.Hiding
                GoTo HiddingLabel
            Case ActiveFormState.Hidden
                GoTo HiddenLabel
        End Select
        '如果已经或正在显示，需要先收回隐藏
        MyState = ActiveFormState.Hiding

        HideThread = New Threading.Thread(AddressOf HideTips)
        HideThread.Start()
HiddingLabel:
        HideThread.Join()
        HideThread.DisableComObjectEagerCleanup()
HiddenLabel:
        MyState = ActiveFormState.Showing

        TipsBitmap = My.Resources.TipsRes.TipsBackground
        TipsInfoAera = My.Resources.TipsRes.TipsBackground.Clone(InfoAeraRectangle, UnityModule.DefaultPixelFormat)
        TipsTimeOut = TimeOut
        TipsIcon = My.Resources.TipsRes.TipsIcons.Clone(New Rectangle(TipIcon * IconRectangle.Width, 0, IconRectangle.Width, IconRectangle.Height), UnityModule.DefaultPixelFormat)
        Using TipsInfoGraphics As Graphics = Graphics.FromImage(TipsInfoAera)
            TipsInfoGraphics.DrawString(TipTitle, TitleFont, TitleBrush, TitlePoint)
            TipsInfoGraphics.DrawString(TipBody, BodyFont, BodyBrush, BodyPoint)
        End Using
        Using TipsGraphics As Graphics = Graphics.FromImage(TipsBitmap)
            TipsGraphics.DrawImage(TipsInfoAera, InfoAeraRectangle)
            TipsGraphics.DrawImage(My.Resources.TipsRes.TipsIconBackground_0, IconBackgroundRectangle)
            TipsGraphics.DrawImage(TipsIcon, IconRectangle)
            TipsGraphics.DrawImage(CloseBitmap, CloseRecangle)
        End Using

        DrawImage(Me, TipsBitmap)
        GC.Collect()

        ShowThread = New Threading.Thread(AddressOf ShowTips)
        ShowThread.Start()
        ShowThread.Join()

        IconTimer.Start()
        WaitThread = New Threading.Thread(AddressOf WaitForHiding)
        WaitThread.Start()

        MyState = ActiveFormState.MouseOut
    End Sub

    ''' <summary>
    ''' 收回并隐藏消息浮窗
    ''' </summary>
    Public Sub CancelTip()
        Select Case MyState
            Case ActiveFormState.Showing
                ShowThread.Abort()
                ShowThread.DisableComObjectEagerCleanup()
            Case ActiveFormState.MouseOut
                WaitThread.Abort()
                WaitThread.DisableComObjectEagerCleanup()
            Case ActiveFormState.MouseIn
            Case ActiveFormState.Hiding
                Exit Sub
            Case ActiveFormState.Hidden
                Exit Sub
        End Select

        MyState = ActiveFormState.Hiding
        '这里需要解绑鼠标移出事件，否则隐藏时会自动触发鼠标移出事件，导致窗体进入ActiveFormState.Watting状态
        RemoveHandler Me.MouseLeave, AddressOf TipsForm_MouseLeave
        HideThread = New Threading.Thread(AddressOf HideTips)
        HideThread.Start()
        HideThread.Join()
        HideThread.DisableComObjectEagerCleanup()
        IconTimer.Stop()
        MyState = ActiveFormState.Hidden
        GC.Collect()
        Me.Close()
    End Sub
#End Region

#Region "动态显示和隐藏"

    Private Sub ShowTips()
        HiddenLocation = Nothing : ShownLocation = Nothing
        HiddenLocation = New Point(My.Computer.Screen.Bounds.Width, 0.85 * (My.Computer.Screen.Bounds.Height - Me.Height))
        ShownLocation = New Point(HiddenLocation.X - Me.Width, HiddenLocation.Y)

        Me.Location = HiddenLocation
        Do While Me.Left > ShownLocation.X
            Me.Left -= 20
            Threading.Thread.Sleep(10)
        Loop
        Me.Location = ShownLocation
    End Sub

    Private Sub HideTips()
        Me.Location = ShownLocation
        Do While Me.Left < HiddenLocation.X
            Me.Left += 20
            Threading.Thread.Sleep(10)
        Loop
        Me.Location = HiddenLocation
    End Sub

    Private Sub WaitForHiding()
        Threading.Thread.Sleep(TipsTimeOut)
        If MyState = ActiveFormState.MouseOut Then
            MyState = ActiveFormState.Hiding
            HideThread = New Threading.Thread(AddressOf HideTips)
            HideThread.Start()
            HideThread.Join()
            IconTimer.Stop()
            GC.Collect()
            MyState = ActiveFormState.Hidden
            Me.Close()
        End If
    End Sub
#End Region

#Region "窗体和控件"

    Private Sub TipsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        Me.Location = New Point(-Me.Width, 0)
        AddHandler Me.KeyPress, AddressOf SystemWorkStation.SystemWorkStation_KeyPress
    End Sub

    Private Sub IconTimer_Tick(sender As Object, e As EventArgs) Handles IconTimer.Tick
        SetWindowPos(Me.Handle, -1, 0, 0, 0, 0, &H10 Or &H40 Or &H2 Or &H1)
        Static IconBackgroundIndex As Integer = 0
        TipsBitmap = My.Resources.TipsRes.TipsBackground
        Using TipsGraphics As Graphics = Graphics.FromImage(TipsBitmap)
            TipsGraphics.DrawImage(My.Resources.TipsRes.ResourceManager.GetObject("TipsIconBackground_" & IconBackgroundIndex), IconBackgroundRectangle)
            TipsGraphics.DrawImage(TipsInfoAera, InfoAeraRectangle)
            TipsGraphics.DrawImage(TipsIcon, IconRectangle)
            TipsGraphics.DrawImage(CloseBitmap, CloseRecangle)
        End Using
        DrawImage(Me, TipsBitmap)
        IconBackgroundIndex = (IconBackgroundIndex + 1) Mod 12
        TipsBitmap.Dispose()
        GC.Collect()
    End Sub

    Private Sub TipsForm_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        Static LastState As Boolean = False
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
        If CloseRecangle.Contains(e.X, e.Y) Then CloseBitmap = My.Resources.TipsRes.TipsCloseButton_0 : CancelTip()
    End Sub

    Private Sub TipsForm_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If CloseRecangle.Contains(e.X, e.Y) Then CloseBitmap = My.Resources.TipsRes.TipsCloseButton_2
    End Sub

    Private Sub TipsForm_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        WaitThread.Abort()
        WaitThread.DisableComObjectEagerCleanup()
        MyState = ActiveFormState.MouseIn
        '绑定鼠标移出事件
        AddHandler Me.MouseLeave, AddressOf TipsForm_MouseLeave
    End Sub

    Private Sub TipsForm_MouseLeave(sender As Object, e As EventArgs)
        WaitThread = New Threading.Thread(AddressOf WaitForHiding)
        WaitThread.Start()
        MyState = ActiveFormState.MouseOut
    End Sub

    Private Sub TipsForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If MyState = ActiveFormState.Hidden Then Exit Sub
        e.Cancel = True
        CancelTip()
    End Sub

    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
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

End Class
