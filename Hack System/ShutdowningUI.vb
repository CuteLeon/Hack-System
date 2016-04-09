Imports System.ComponentModel
Imports System.Threading

Public Class ShutdowningUI
    Dim ShutdownBitmap As Bitmap = New Bitmap(My.Resources.SystemAssets.ShutdownBGI, My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height)
    Dim ShutdownGraphics As Graphics = Graphics.FromImage(ShutdownBitmap)
    Dim MoveDistance As Integer = My.Computer.Screen.Bounds.Width \ 50
    Dim ThreadShowMe As Thread
    Dim ThreadHideMe As Thread

    Private Sub ShowMe()
        For Index As Integer = 1 To 10
            Me.Opacity = Index / 10
            Thread.Sleep(50)
        Next
    End Sub

    Private Sub HideMe(ByVal ToRight As Boolean)
        Do Until Me.Left > My.Computer.Screen.Bounds.Width
            Me.Left += MoveDistance
            Me.Opacity = 0.5 * (1 - Me.Left / My.Computer.Screen.Bounds.Width) + 0.5
            Thread.Sleep(10)
        Loop
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

    Private Sub Shutdowning_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If SystemWorkStation.SystemClosing Then Exit sub 
        e.Cancel = True
    End Sub

    Private Sub ShutdowningUI_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Hide TipsForm if it is shown
        If TipsForm.Visible Then TipsForm.CancelTip()
        ThreadShowMe = New Thread(AddressOf ShowMe)
        ThreadShowMe.Start()
        ThreadShowMe.Join()
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("Shutdown"), AudioPlayMode.WaitToComplete)
        '结束屏幕融化线程，否则程序无法退出
        If ScreenMelt.Melting Then ScreenMelt.StopMelt()
        '释放语音识别引擎(容错处理)
        Try
            SystemWorkStation.MySpeechRecognitionEngine.RecognizeAsyncStop()
            SystemWorkStation.MySpeechRecognitionEngine.Dispose()
        Catch ex As Exception
        End Try
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

        Application.Exit()
    End Sub
End Class