Imports System.ComponentModel

Public Class TipsForm
    Dim ShowThread As Threading.Thread = New Threading.Thread(AddressOf ShowTips)
    Dim HideThread As Threading.Thread = New Threading.Thread(AddressOf HideTips)
    Dim WaitThread As Threading.Thread = New Threading.Thread(AddressOf WaitForHiding)

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
    Dim TipsGraphics As Graphics
    Dim TipsInfoGraphics As Graphics
    Dim InfoAeraRectangle As Rectangle = New Rectangle(95, 5, 203, 88)
    Dim IconBackgroundRectangle As Rectangle = New Rectangle(18, 14, 70, 70)
    Dim IconRectangle As Rectangle = New Rectangle(26, 22, 54, 54)
    Dim TipsIcon As Bitmap
    Dim CloseRecangle As Rectangle = New Rectangle(311, 41, 25, 25)
    Dim CloseBitmap As Bitmap = My.Resources.TipsRes.TipsCloseButton_0
    Dim TipsTimeOut As Integer
    Dim TipsShown As Boolean = False

    Dim HiddenLocation As Point
    Dim ShownLocation As Point
    Dim CloseMe As Boolean = False

    Public Enum TipsIconType
        Infomation = 0     '消息
        Question = 1        '询问
        Exclamation = 2    '警告
        Critical = 3            '错误
    End Enum

    Public Sub PopupTips(ByVal TipTitle As String, ByVal TipIcon As TipsIconType, ByVal TipBody As String, Optional ByVal TimeOut As Integer = 5000)
        Me.TopMost = True
        My.Computer.Audio.Play(My.Resources.TipsRes.TipsAlarm, AudioPlayMode.Background)

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

        ShowThread = Nothing
        ShowThread = New Threading.Thread(AddressOf ShowTips)
        ShowThread.Start()
        ShowThread.Join()

        TipsShown = True
        WaitThread = New Threading.Thread(AddressOf WaitForHiding)
        WaitThread.Start()
    End Sub

    Public Sub CancelTip()
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
        IconTimer.Stop()

        Me.Close()
    End Sub

    Private Sub ShowTips()
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

    Private Sub TipsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        Me.Location = New Point(-Me.Width, 0)
    End Sub

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

    Private Sub IconTimer_Tick(sender As Object, e As EventArgs) Handles IconTimer.Tick
        Static IconBackgroundIndex As Integer = 0
        Dim TempIconBackground As Bitmap = My.Resources.TipsRes.TipsIconBackground.Clone(New Rectangle(IconBackgroundIndex * IconBackgroundRectangle.Width, 0, IconBackgroundRectangle.Width, IconBackgroundRectangle.Height), Imaging.PixelFormat.Format32bppArgb)

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
    End Sub

    Private Sub TipsForm_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        If HideThread.ThreadState = Threading.ThreadState.Running Then Exit Sub
        WaitThread = New Threading.Thread(AddressOf WaitForHiding)
        WaitThread.Start()
    End Sub

    Private Sub TipsForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If CloseMe Then Exit Sub
        e.Cancel = True
        CancelTip()
    End Sub
End Class
