Imports System.ComponentModel
Imports System.Threading

Public Class ShutdowningUI

#Region "声明区"
    '线程每次移动的距离
    Dim MoveDistance As Integer = My.Computer.Screen.Bounds.Width \ 50
#End Region

#Region "动态显示和隐藏"

    Private Sub ShowMe()
        '动态显示关机界面
        For Index As Integer = 1 To 10
            Me.Opacity = Index / 10
            Thread.Sleep(50)
        Next
    End Sub

    Private Sub HideMe()
        '动态隐藏关机界面
        Do Until Me.Left > My.Computer.Screen.Bounds.Width
            Me.Left += MoveDistance
            Me.Opacity = 0.5 * (1 - Me.Left / My.Computer.Screen.Bounds.Width) + 0.5
            Thread.Sleep(10)
        Loop
    End Sub
#End Region

#Region "窗体"

    Private Sub ShutdowningUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '允许多线程访问UI
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        '初始化界面
        Me.Location = New Point(0, 0)
        Me.Size = My.Computer.Screen.Bounds.Size
        Me.Cursor = StartingUpUI.SystemCursor
        StartUpLogo.Location = New Point((My.Computer.Screen.Bounds.Width - My.Resources.SystemAssets.HackSystemLogo.Width) \ 2, My.Computer.Screen.Bounds.Height \ 4)
        '结束屏幕融化线程，否则程序无法退出
        If ScreenMelt.Melting Then ScreenMelt.StopMelt()
        '释放语音识别引擎(容错处理)
        If SystemWorkStation.SpeechRecognitionMode Then
            Try
                SystemWorkStation.MySpeechRecognitionEngine.RecognizeAsyncStop()
                SystemWorkStation.MySpeechRecognitionEngine.Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub ShutdowningUI_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        '需要隐藏提示浮窗
        Dim ThreadShowMe As Thread = New Thread(AddressOf ShowMe)
        ThreadShowMe.Start()
        ThreadShowMe.Join()
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("Shutdown"), AudioPlayMode.WaitToComplete)
        If CommandConsole.Visible Then CommandConsole.Hide()
        '直接关闭提示浮窗，因为CancelTips函数要等待线程结束，退出程序时会出错
        If TipsForm.Visible Then TipsForm.CloseMe = True : TipsForm.Close()
        If XYMail.Visible Then XYMail.Hide()
        If AboutMeForm.Visible Then AboutMeForm.Hide()
        If MineSweeperForm.Visible Then MineSweeperForm.Hide()
        If Game1010Form.Visible Then Game1010Form.Hide()
        If Game2048Form.Visible Then Game2048Form.Hide()
        '遍历关闭脚本窗口
        For Each ChildForm As Form In SystemWorkStation.ScriptForm
            If Not (ChildForm Is Nothing) Then
                ChildForm.Close()
            End If
        Next
        '遍历关闭浏览器
        For Each BrowserForm In SystemWorkStation.BrowserForms
            CType(BrowserForm, XYBrowser).GoingToExit = True
            CType(BrowserForm, XYBrowser).Close()
        Next
        '隐藏主桌面
        SystemWorkStation.Hide()

        '开始隐藏
        Dim ThreadHideMe As Thread = New Thread(AddressOf HideMe)
        ThreadHideMe.Start()
        ThreadHideMe.Join()

        '程序退出！！！
        GC.Collect()
        Application.Exit()
    End Sub
#End Region

End Class