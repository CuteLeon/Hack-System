Imports System.ComponentModel
Imports System.Threading

Public Class ShutdowningUI

#Region "声明区"
    ''' <summary>
    ''' 线程每次移动的距离
    ''' </summary>
    Dim MoveDistance As Integer = My.Computer.Screen.Bounds.Height \ 300
    ''' <summary>
    ''' 双倍的移动距离
    ''' </summary>
    Dim DoubleMoveDistance As Integer = MoveDistance * 2
    ''' <summary>
    ''' 拉伸为窗口尺寸的背景图
    ''' </summary>
    Dim ShutdownBGI As Bitmap = New Bitmap(My.Resources.SystemAssets.ShutdownBGI, My.Computer.Screen.Bounds.Size)
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
        '动态隐藏关机界面（不要把 BackgroundImageLayout 设置为 Stretch，否则会卡顿）
        For Index As Integer = 0 To 50
            Me.Left += MoveDistance
            Me.Top -= MoveDistance
            Me.Width -= DoubleMoveDistance
            Me.Height -= DoubleMoveDistance
            Me.Opacity = (50 - Index) / 50
            Thread.Sleep(15)
        Next
    End Sub
#End Region

#Region "窗体"

    Private Sub ShutdowningUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '允许多线程访问UI
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        '初始化界面
        Me.Location = New Point(0, 0)
        Me.Size = My.Computer.Screen.Bounds.Size
        Dim HackSystemLogo As Bitmap = My.Resources.SystemAssets.HackSystemLogo
        Using ShutdownGraphics As Graphics = Graphics.FromImage(ShutdownBGI)
            ShutdownGraphics.DrawImage(HackSystemLogo, New Rectangle((My.Computer.Screen.Bounds.Width - HackSystemLogo.Width) \ 2, My.Computer.Screen.Bounds.Height \ 4, HackSystemLogo.Width, HackSystemLogo.Height))
        End Using
        Me.BackgroundImage = ShutdownBGI
        '结束屏幕融化线程，否则程序无法退出
        If ScreenMelt.Melting Then ScreenMelt.StopMelt()
        '释放语音识别引擎(容错处理)
        If UnityModule.SpeechRecognitionMode Then
            Try
                UnityModule.MySpeechRecognitionEngine.RecognizeAsyncStop()
                UnityModule.MySpeechRecognitionEngine.Dispose()
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
        TipsForm.CancelTip()
        If DownloaderForm.IsDownloading Then DownloaderForm.CancelDownload()
        If XYMail.Visible Then XYMail.Hide()
        If AboutMeForm.Visible Then AboutMeForm.Hide()
        If VMMainForm.Visible Then VMMainForm.Hide()
        If VPMainForm.Visible Then VPMainForm.Hide()
        If MineSweeperForm.Visible Then MineSweeperForm.Hide()
        If Game1010Form.Visible Then Game1010Form.Hide()
        If Game2048Form.Visible Then Game2048Form.Hide()
        '遍历关闭脚本窗口
        For Each ChildForm As Form In UnityModule.ScriptForm
            If Not (ChildForm Is Nothing) Then
                ChildForm.Close()
            End If
        Next
        '遍历关闭浏览器
        For Each BrowserForm In UnityModule.BrowserForms
            CType(BrowserForm, XYBrowser).CloseFormShutdown()
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