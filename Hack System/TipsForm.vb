Imports System.ComponentModel

Public Class TipsForm

#Region "声明区"
    Private Enum TipsState
        Showing '正在显示
        Waitting '正在等待
        MouseEnter '鼠标进入停止等待
        Hiding '正在隐藏
        Hidden '已经隐藏
    End Enum

    '置前显示，之所以不用TopMost是因为TopMost会让窗体激活，在Timer里值守置前会影响脚本窗口获取焦点
    Private Declare Sub SetWindowPos Lib "User32" (ByVal hWnd As Integer, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer)
    Dim MyState As TipsState = TipsState.Hidden
    Dim ShowThread As Threading.Thread ' = New Threading.Thread(AddressOf ShowTips)
    Dim HideThread As Threading.Thread ' = New Threading.Thread(AddressOf HideTips)
    Dim WaitThread As Threading.Thread ' = New Threading.Thread(AddressOf WaitForHiding)

    Dim TitleFont As Font = New Font("微软雅黑", 18.0!, System.Drawing.FontStyle.Bold)
    Dim TitlePoint As PointF = New PointF(0, 18)
    Dim TitleColor As Color = Color.FromArgb(235, 100, 60)
    Dim TitleBrush As Brush = New SolidBrush(TitleColor)

    Dim BodyFont As Font = New Font("微软雅黑", 16.0!, System.Drawing.FontStyle.Regular)
    Dim BodyPoint As PointF = New PointF(0, 48)
    Dim BodyColor As Color = Color.FromArgb(120, 140, 150)
    Dim BodyBrush As Brush = New SolidBrush(BodyColor)

    Dim TipsBitmap As Bitmap
    Dim TipsInfoAera As Bitmap
    Dim InfoAeraRectangle As Rectangle = New Rectangle(95, 5, 203, 88)
    Dim IconBackgroundRectangle As Rectangle = New Rectangle(18, 14, 70, 70)
    Dim IconRectangle As Rectangle = New Rectangle(26, 22, 54, 54)
    Dim TipsIcon As Bitmap
    Dim CloseRecangle As Rectangle = New Rectangle(311, 41, 25, 25)
    Dim CloseBitmap As Bitmap = My.Resources.TipsRes.TipsCloseButton_0
    Dim TipsTimeOut As Integer

    Dim HiddenLocation As Point
    Dim ShownLocation As Point

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
        SystemWorkStation.SetForegroundWindow(Me.Handle)

        Select Case MyState
            Case TipsState.Showing
                ShowThread.Abort()
                ShowThread.DisableComObjectEagerCleanup()
            Case TipsState.Waitting
                WaitThread.Abort()
                WaitThread.DisableComObjectEagerCleanup()
            Case TipsState.MouseEnter
            Case TipsState.Hiding
                GoTo HiddingLabel
            Case TipsState.Hidden
                GoTo HiddenLabel
        End Select
        '如果已经或正在显示，需要先收回隐藏
        MyState = TipsState.Hiding

        HideThread = New Threading.Thread(AddressOf HideTips)
        HideThread.Start()
HiddingLabel:
        HideThread.Join()
        HideThread.DisableComObjectEagerCleanup()
HiddenLabel:
        MyState = TipsState.Showing

        TipsBitmap = My.Resources.TipsRes.TipsBackground
        TipsInfoAera = My.Resources.TipsRes.TipsBackground.Clone(InfoAeraRectangle, Imaging.PixelFormat.Format32bppArgb)
        TipsTimeOut = TimeOut
        TipsIcon = My.Resources.TipsRes.TipsIcons.Clone(New Rectangle(TipIcon * IconRectangle.Width, 0, IconRectangle.Width, IconRectangle.Height), Imaging.PixelFormat.Format32bppArgb)
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

        MyState = TipsState.Waitting
    End Sub

    ''' <summary>
    ''' 收回并隐藏消息浮窗
    ''' </summary>
    Public Sub CancelTip()
        Select Case MyState
            Case TipsState.Showing
                ShowThread.Abort()
                ShowThread.DisableComObjectEagerCleanup()
            Case TipsState.Waitting
                WaitThread.Abort()
                WaitThread.DisableComObjectEagerCleanup()
            Case TipsState.MouseEnter
            Case TipsState.Hiding
                Exit Sub
            Case TipsState.Hidden
                Exit Sub
        End Select

        MyState = TipsState.Hiding
        '这里需要解绑鼠标移出事件，否则隐藏时会自动触发鼠标移出事件，导致窗体进入TipsState.Watting状态
        RemoveHandler Me.MouseLeave, AddressOf TipsForm_MouseLeave
        HideThread = New Threading.Thread(AddressOf HideTips)
        HideThread.Start()
        HideThread.Join()
        HideThread.DisableComObjectEagerCleanup()
        IconTimer.Stop()
        MyState = TipsState.Hidden
        GC.Collect()
        Me.Close()
    End Sub
#End Region

#Region "动态显示和隐藏"

    Private Sub ShowTips()
        HiddenLocation = Nothing : ShownLocation = Nothing
        HiddenLocation = New Point(My.Computer.Screen.Bounds.Width, 0.9 * (My.Computer.Screen.Bounds.Height - Me.Height))
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
        If MyState = TipsState.Waitting Then
            HideThread = New Threading.Thread(AddressOf HideTips)
            HideThread.Start()
            HideThread.Join()
            IconTimer.Stop()
            GC.Collect()
            MyState = TipsState.Hidden
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
        MyState = TipsState.MouseEnter
        '绑定鼠标移出事件
        AddHandler Me.MouseLeave, AddressOf TipsForm_MouseLeave
    End Sub

    Private Sub TipsForm_MouseLeave(sender As Object, e As EventArgs)
        WaitThread = New Threading.Thread(AddressOf WaitForHiding)
        WaitThread.Start()
        MyState = TipsState.Waitting
    End Sub

    Private Sub TipsForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If MyState = TipsState.Hidden Then Exit Sub
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
