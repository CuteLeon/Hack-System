Imports System.ComponentModel
Imports System.Threading

Public Class ShutdowningUI
    Private Declare Function GetCursorPos Lib "user32" (ByRef lpPoint As POINTAPI) As Integer
    Private Structure POINTAPI
        Dim X As Int16
        Dim Y As Int16
    End Structure
    Dim FirstPoint As POINTAPI

    Dim MoveDistance As Integer = My.Computer.Screen.Bounds.Width \ 50
    Dim ShutdownBitmap As Bitmap = New Bitmap(My.Resources.SystemAssets.ShutdownBGI, My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height)
    Dim ShutdownGraphics As Graphics = Graphics.FromImage(ShutdownBitmap)
    Dim ThreadShowMe As Thread
    Dim ThreadHideMe As Thread
    Dim MouseDowned As Boolean

    Public Sub ShowShutdownForm()
        'Hide TipsForm if it is shown
        If TipsForm.Visible Then TipsForm.CancelTip()

        If ThreadShowMe IsNot Nothing AndAlso ThreadShowMe.ThreadState = ThreadState.Running Then Exit Sub
        ThreadShowMe = New Thread(AddressOf ShowMe)
        ThreadShowMe.Start()
        'Application is going to exit.
        If SystemWorkStation.ApplicationClosing Then
            ThreadShowMe.Join()
            My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("Shutdown"), AudioPlayMode.WaitToComplete)
            '释放语音识别引擎(容错处理)
            Try
                SystemWorkStation.MySpeechRecognitionEngine.RecognizeAsyncStop()
                SystemWorkStation.MySpeechRecognitionEngine.Dispose()
            Catch ex As Exception
            End Try
            If TipsForm.Visible Then TipsForm.CancelTip()
            If XYMail.Visible Then XYMail.Hide()
            If AboutMeForm.Visible Then AboutMeForm.Hide()
            If MineSweeperForm.Visible Then MineSweeperForm.Hide()
            'Close scripts.
            For Each ChildForm As Form In SystemWorkStation.ScriptForm
                If Not (ChildForm Is Nothing) Then
                    ChildForm.Close()
                End If
            Next
            'Close browsers.
            For Each BrowserForm In SystemWorkStation.BrowserForms
                CType(BrowserForm, XYBrowser).GoingToExit = True
                CType(BrowserForm, XYBrowser).Close()
            Next
            'Hide desktop.
            SystemWorkStation.Hide()

            ThreadHideMe = New Thread(AddressOf HideMe)
            ThreadHideMe.Start(True)
            ThreadHideMe.Join()

            End
        End If
    End Sub

    Private Sub ShowMe()
        For Index As Integer = 1 To 10
            Me.Opacity = Index / 10
            Thread.Sleep(50)
        Next
    End Sub

    Public Sub HideShutdownForm(ByVal ToRight As Boolean)
        If ThreadHideMe IsNot Nothing AndAlso ThreadHideMe.ThreadState = ThreadState.Running Then Exit Sub
        ThreadHideMe = New Thread(AddressOf HideMe)
        ThreadHideMe.Start(ToRight)
        ThreadHideMe.Join()
        SystemWorkStation.SetForegroundWindow(SystemWorkStation.Handle)
    End Sub

    Private Sub HideMe(ByVal ToRight As Boolean)
        If ToRight Then
            'Hide to right
            Do Until Me.Left > My.Computer.Screen.Bounds.Width
                Me.Left += MoveDistance
                Me.Opacity = 0.5 * (1 - Me.Left / My.Computer.Screen.Bounds.Width) + 0.5
                Thread.Sleep(10)
            Loop
        Else
            'Hide to left.
            Do Until Me.Left < -My.Computer.Screen.Bounds.Width
                Me.Left -= MoveDistance
                Me.Opacity = 0.5 * ((My.Computer.Screen.Bounds.Width + Me.Left) / My.Computer.Screen.Bounds.Width) + 0.5
                Thread.Sleep(10)
            Loop
        End If
        'Reset form.
        Me.Opacity = 0
        Me.Location = New Point(0, 0)
        Me.Hide()
    End Sub

    Private Sub Shutdowning_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Allow thread to visit UI.
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False

        Me.Location = New Point(0, 0)
        Me.Size = My.Computer.Screen.Bounds.Size
        Me.Cursor = StartingUpUI.SystemCursor
        ShutdownGraphics.DrawImage(My.Resources.SystemAssets.HackSystemLogo, (My.Computer.Screen.Bounds.Width - My.Resources.SystemAssets.HackSystemLogo.Width) \ 2, My.Computer.Screen.Bounds.Height \ 5, My.Resources.SystemAssets.HackSystemLogo.Width, My.Resources.SystemAssets.HackSystemLogo.Height)
        ShutdownGraphics.DrawImage(My.Resources.SystemAssets.SplitLine, 0, (6 * My.Computer.Screen.Bounds.Height) \ 7, My.Computer.Screen.Bounds.Width, 2)
        Me.BackgroundImage = ShutdownBitmap
        ShutdownGraphics.Dispose()
    End Sub

    Private Sub Shutdowning_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        'Allow mouse to move window.
        If Not MouseDowned Then Exit Sub
        Dim NowPoint As POINTAPI
        GetCursorPos(NowPoint)
        Me.Left += NowPoint.X - FirstPoint.X

        Me.Opacity = 0.5 * IIf(Me.Left > 0, 1 - Me.Left / My.Computer.Screen.Bounds.Width, (My.Computer.Screen.Bounds.Width + Me.Left) / My.Computer.Screen.Bounds.Width) + 0.5
        FirstPoint = NowPoint
    End Sub

    Private Sub Shutdowning_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        MouseDowned = True
        GetCursorPos(FirstPoint)
    End Sub

    Private Sub Shutdowning_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        'Mouse Up.
        MouseDowned = False
        If Me.Left > Me.Width \ 3 Then
            HideShutdownForm(True)
        ElseIf Me.Left < -Me.Width \ 3 Then
            HideShutdownForm(False)
        Else
            Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf ReSetMyLocation))
        End If
    End Sub

    Private Sub ReSetMyLocation()
        If Me.Left > 0 Then
            Do While Me.Left > MoveDistance
                Me.Left -= MoveDistance
                Me.Opacity = 0.5 * (1 - Me.Left / My.Computer.Screen.Bounds.Width) + 0.5
                Thread.Sleep(10)
            Loop
            Me.Left = 0
        Else
            Do While Me.Left < -MoveDistance
                Me.Left += MoveDistance
                Me.Opacity = 0.5 * ((My.Computer.Screen.Bounds.Width + Me.Left) / My.Computer.Screen.Bounds.Width) + 0.5
                Thread.Sleep(10)
            Loop
            Me.Left = 0
        End If
    End Sub

    Private Sub Shutdowning_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
    End Sub
End Class