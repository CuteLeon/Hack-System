Imports System.ComponentModel
Imports System.Threading

Public Class LoginAndLockUI

#Region "声明区"

    Private Declare Function GetCursorPos Lib "user32" (ByRef lpPoint As POINTAPI) As Integer

    Private Structure POINTAPI
        Dim X As Int16
        Dim Y As Int16
    End Structure

    Private Const WallpaperCount As Int16 = 19 '壁纸总数
    Public HeadSize As Size = New Size(159, 159) '头像图像尺寸
    Public UserHead As Bitmap '用户头像图像
    Public UserName As String '用户名
    Public UserNameBitmap As Bitmap '用户名图像
    Public UserHeadString As String '用户头像图像Base64编码
    Public UserNameString As String '用户名图像Base64编码
    Public LockScreenMode As Boolean = False '锁屏模式或者登录模式
    Dim WallpaperIndex As Integer = 14 '初始壁纸标识
    Dim FirstPoint As POINTAPI '鼠标按下时坐标
    Dim MoveDistance As Integer = My.Computer.Screen.Bounds.Width \ 50 '自移动线程每次移动的距离
    Dim ThreadShowMe As Thread '显示线程
    Dim ThreadHideMe As Thread '隐藏线程
#End Region

#Region "窗体"

    Private Sub LoginAndLockUI_Load(sender As Object, e As EventArgs) Handles Me.Load
        '允许多线程访问UI
        CheckForIllegalCrossThreadCalls = False
        '全屏显示(切记不可设置为FormWindowState.Maximized，否则无法使用鼠标拖动)
        Me.Location = New Point(0, 0)
        Me.Size = My.Computer.Screen.Bounds.Size
        '尝试从存档读取壁纸标识
        Dim WallpaperIndexSetting As String = My.Settings.LoginWallpaperIndex
        If Not WallpaperIndexSetting = vbNullString Then
            WallpaperIndex = Int(WallpaperIndexSetting)
            Me.BackgroundImage = My.Resources.SystemAssets.ResourceManager.GetObject("SystemWallpaper_" & WallpaperIndex.ToString("00"))
        End If
        '尝试读取用户头像(存档为空时使用默认头像)
        UserHeadString = My.Settings.UserHead
        If UserHeadString = vbNullString Then UserHead = My.Resources.SystemAssets.DefaultUserHead Else UserHead = StringToBitmap(UserHeadString)
        HeadPictureBox.BackgroundImage = UserHead
        '尝试读取用户名图像(存档为空时使用默认用户名图像)
        UserNameString = My.Settings.UserNameBitmap
        If UserNameString = vbNullString Then UserNameBitmap = My.Resources.SystemAssets.DefaultUserName Else UserNameBitmap = StringToBitmap(UserNameString)
        UserNameControl.Image = UserNameBitmap
        '尝试读取用户名(存档为空时使用默认用户名)
        UserName = My.Settings.UserName
        If UserName = vbNullString Then UserName = "Leon"
        SystemWorkStation.MenuUserName.Text = UserName

        '使用Panel控件可以简化设计，但是Panel会闪烁，所以继续使用PictureBox
        HeadPictureBox.Location = New Point(-3, -3)
        PasswordControl.Parent = LoginAreaControl
        LoginButtonControl.Parent = LoginAreaControl
        HeadPictureBox.Parent = LoginAreaControl
        UserNameControl.Parent = LoginAreaControl
        PasswordControl.Location = New Point(281, 113)
        LoginButtonControl.Location = New Point(507, 48)
        UserNameControl.Location = New Point(HeadPictureBox.Right, 20)
        LoginAreaControl.Left = (My.Computer.Screen.Bounds.Width - LoginAreaControl.Width) / 2
        LoginAreaControl.Top = (My.Computer.Screen.Bounds.Height - LoginAreaControl.Height) / 2
        '不选中密码
        PasswordControl.SelectionStart = PasswordControl.TextLength
        PasswordControl.SelectionLength = 0

        Me.Cursor = StartingUpUI.SystemCursor
    End Sub

    Private Sub LoginAndLockUI_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        '禁止关闭
        e.Cancel = True
    End Sub

    Private Sub LoginAndLockUI_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        '首次加载(系统启动)时循环播放背景音乐
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("LockUIBGM"), AudioPlayMode.BackgroundLoop)
    End Sub

    Private Sub LoginAndLockUI_MouseDown(sender As Object, e As MouseEventArgs)
        '鼠标按下，记录按下坐标，开始拖动
        GetCursorPos(FirstPoint)
        AddHandler Me.MouseMove, AddressOf LoginAndLockUI_MouseMove
    End Sub

    Private Sub LoginAndLockUI_MouseMove(sender As Object, e As MouseEventArgs)
        '允许鼠标水平拖动窗口
        Dim NowPoint As POINTAPI
        GetCursorPos(NowPoint)
        Me.Left += NowPoint.X - FirstPoint.X
        Me.Opacity = 0.5 * IIf(Me.Left > 0, 1 - Me.Left / My.Computer.Screen.Bounds.Width, (My.Computer.Screen.Bounds.Width + Me.Left) / My.Computer.Screen.Bounds.Width) + 0.5
        FirstPoint = NowPoint
    End Sub

    Private Sub LoginAndLockUI_MouseUp(sender As Object, e As MouseEventArgs)
        '鼠标抬起，判断时候解锁或回复锁屏状态
        RemoveHandler Me.MouseMove, AddressOf LoginAndLockUI_MouseMove
        If Me.Left > Me.Width \ 3 Then
            HideLockScreen(True)
        ElseIf Me.Left < -Me.Width \ 3 Then
            HideLockScreen(False)
        Else
            Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf ReSetMyLocation))
        End If
    End Sub

    Private Sub LoginAndLockUI_Click(sender As Object, e As EventArgs) Handles Me.Click
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        '点击切换壁纸
        If Math.Abs(Me.Left) < 15 Then '当拖动距离超过15像素时不切换壁纸
            If WallpaperIndex = WallpaperCount - 1 Then WallpaperIndex = 0 Else WallpaperIndex += 1
            Me.BackgroundImage = My.Resources.SystemAssets.ResourceManager.GetObject("SystemWallpaper_" & WallpaperIndex.ToString("00"))
            UserNameControl.Text = WallpaperIndex & "," & CBool(Me.BackgroundImage Is Nothing)
            My.Settings.LoginWallpaperIndex = WallpaperIndex
            My.Settings.Save()
        End If
    End Sub
#End Region

#Region "控件"

    Private Sub LoginButtonControl_MouseEnter(sender As Object, e As EventArgs) Handles LoginButtonControl.MouseEnter
        LoginButtonControl.Image = My.Resources.SystemAssets.LoginButton_2
    End Sub

    Private Sub LoginButtonControl_MouseDown(sender As Object, e As MouseEventArgs) Handles LoginButtonControl.MouseDown
        LoginButtonControl.Image = My.Resources.SystemAssets.LoginButton_3
    End Sub

    Private Sub LoginButtonControl_MouseUp(sender As Object, e As MouseEventArgs) Handles LoginButtonControl.MouseUp
        LoginButtonControl.Image = My.Resources.SystemAssets.LoginButton_2
    End Sub

    Private Sub LoginButtonControl_MouseLeave(sender As Object, e As EventArgs) Handles LoginButtonControl.MouseLeave
        LoginButtonControl.Image = My.Resources.SystemAssets.LoginButton_1
    End Sub

    Private Sub LoginButtonControl_Click(sender As Object, e As EventArgs) Handles LoginButtonControl.Click
        '点击登录按钮登录系统
        ExchangeUI()
    End Sub

    Private Sub PasswordControl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PasswordControl.KeyPress
        '敲回车键登录系统
        If Asc(e.KeyChar) = Keys.Enter Then ExchangeUI()
    End Sub
#End Region

#Region "动态显示和隐藏"

    Public Sub ShowLockScreen()
        '进入锁屏状态
        If TipsForm.Visible Then TipsForm.CancelTip()
        If ThreadShowMe IsNot Nothing AndAlso ThreadShowMe.ThreadState = ThreadState.Running Then Exit Sub
        ThreadShowMe = New Thread(AddressOf ShowMe)
        ThreadShowMe.Start()
    End Sub

    Private Sub ShowMe()
        '动态显示
        For Index As Integer = 1 To 10
            Me.Opacity = Index / 10
            Thread.Sleep(50)
        Next
    End Sub

    Public Sub HideLockScreen(ByVal ToRight As Boolean)
        '动态隐藏
        If ThreadHideMe IsNot Nothing AndAlso ThreadHideMe.ThreadState = ThreadState.Running Then Exit Sub
        ThreadHideMe = New Thread(AddressOf HideMe)
        ThreadHideMe.Start(ToRight)
        ThreadHideMe.Join()
        SystemWorkStation.SetForegroundWindow(SystemWorkStation.Handle)
    End Sub

    Private Sub HideMe(ByVal ToRight As Boolean)
        If ToRight Then
            '向右隐藏
            Do Until Me.Left > My.Computer.Screen.Bounds.Width
                Me.Left += MoveDistance
                Me.Opacity = 0.5 * (1 - Me.Left / My.Computer.Screen.Bounds.Width) + 0.5
                Thread.Sleep(10)
            Loop
        Else
            '向左隐藏
            Do Until Me.Left < -My.Computer.Screen.Bounds.Width
                Me.Left -= MoveDistance
                Me.Opacity = 0.5 * ((My.Computer.Screen.Bounds.Width + Me.Left) / My.Computer.Screen.Bounds.Width) + 0.5
                Thread.Sleep(10)
            Loop
        End If
        '重置窗体
        Me.Opacity = 0
        Me.Location = New Point(0, 0)
        Me.Hide()
    End Sub
#End Region

#Region "功能函数"

    Public Function StringToBitmap(ByVal Base64 As String) As Bitmap
        Try '把Base64编码转换为图像
            Dim EncryptByte() As Byte = Convert.FromBase64String(Base64)
            Dim BitmapStream As IO.MemoryStream = New IO.MemoryStream(EncryptByte)
            Return Bitmap.FromStream(BitmapStream)
        Catch ex As Exception
            '出错时返回空
            Return Nothing
        End Try
    End Function

    Private Sub ExchangeUI()
        If PasswordControl.Text.ToLower = "resetuser" Then
            '密码输入框输入"resetuser"可以恢复初始头像和用户名
            UserHead = Nothing
            UserName = "Leon"
            SystemWorkStation.MenuUserName.Text = UserName
            UserNameString = vbNullString
            UserNameBitmap = Nothing
            UserHeadString = vbNullString

            UserNameControl.Image = My.Resources.SystemAssets.DefaultUserName
            UserNameControl.Size = New Size(300, UserNameControl.Image.Height)
            HeadPictureBox.BackgroundImage = My.Resources.SystemAssets.DefaultUserHead

            My.Settings.UserName = UserName
            My.Settings.UserNameBitmap = UserNameString
            My.Settings.UserHead = vbNullString
            My.Settings.Save()
            '弹出提示浮窗
            If Not TipsForm.Visible Then TipsForm.Show(Me)
            TipsForm.PopupTips("Successfully !", TipsForm.TipsIconType.Infomation, "Reset head successfully")

            Me.Activate()
        Else
            '可以在这里设置判断登录密码
            If LockScreenMode Then
                '解锁
                My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("Tips"), AudioPlayMode.Background)
                HideLockScreen(True)
                LockScreenMode = False
            Else
                '登录
                SystemWorkStation.Show()
                Me.Hide()
            End If
            SystemWorkStation.Focus()

            '非锁屏状态时，不允许通过鼠标拖动的方式登录系统，所以首先登录一次绑定事件
            AddHandler Me.MouseUp, AddressOf LoginAndLockUI_MouseUp
            AddHandler Me.MouseDown, AddressOf LoginAndLockUI_MouseDown
        End If
    End Sub

    Private Sub ReSetMyLocation()
        '鼠标拖动距离太少不足以解锁时用于恢复锁屏状态
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

#End Region

End Class
