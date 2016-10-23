Imports System.ComponentModel
Imports System.Threading

Public Class LoginAndLockUI

#Region "声明区"

    Private Declare Function GetCursorPos Lib "user32" (ByRef lpPoint As POINTAPI) As Integer

    Private Structure POINTAPI
        Dim X As Int16
        Dim Y As Int16
    End Structure

    Dim WallpaperIndex As Integer = 14 '初始壁纸标识
    Dim FirstPoint As POINTAPI '鼠标按下时坐标
    Dim MoveDistance As Integer = My.Computer.Screen.Bounds.Width \ 50 '自移动线程每次移动的距离
    Dim SplitDistance As Integer = My.Computer.Screen.Bounds.Height \ 50 '分离特效每次移动的距离
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
        PasswordLabel.Parent = LoginAreaControl
        LoginButtonControl.Parent = LoginAreaControl
        PasswordTextBox.Parent = LoginAreaControl
        HeadPictureBox.Parent = LoginAreaControl
        UserNameControl.Parent = LoginAreaControl
        PasswordLabel.Location = New Point(272, 105)
        PasswordTextBox.Location = PasswordLabel.Location
        LoginButtonControl.Location = New Point(507, 48)
        UserNameControl.Location = New Point(HeadPictureBox.Right, 20)
        LoginAreaControl.Left = (My.Computer.Screen.Bounds.Width - LoginAreaControl.Width) / 2
        LoginAreaControl.Top = (My.Computer.Screen.Bounds.Height - LoginAreaControl.Height) / 2
        LastWallpaperButton.Location = New Point(My.Computer.Screen.Bounds.Width - LastWallpaperButton.Width * 2 - 90, My.Computer.Screen.Bounds.Height - LastWallpaperButton.Height - 40)
        NextWallpaperButton.Location = New Point(LastWallpaperButton.Right - 1, LastWallpaperButton.Top)
        '不选中密码
        PasswordTextBox.SelectionStart = PasswordTextBox.TextLength
        PasswordTextBox.SelectionLength = 0

        Me.Cursor = UnityModule.SystemCursor
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
        If TipsForm.Visible Then TipsForm.CancelTip()
        If Me.Left > Me.Width \ 3 Then
            HideLockScreen(True)
        ElseIf Me.Left < -Me.Width \ 3 Then
            HideLockScreen(False)
        Else
            Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf ReSetMyLocation))
            Exit Sub
        End If
        LockScreenMode = False
        UnityModule.SetForegroundWindow(SystemWorkStation.Handle)
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

    Private Sub PasswordControl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PasswordTextBox.KeyPress
        '敲回车键登录系统
        If Asc(e.KeyChar) = Keys.Enter Then ExchangeUI()
    End Sub

    Private Sub WallpaperButton_Click(sender As Object, e As EventArgs) Handles LastWallpaperButton.Click, NextWallpaperButton.Click
        '点击切换壁纸
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        If CType(sender, Label).Tag = "Last" Then
            WallpaperIndex = IIf(WallpaperIndex = 0, UnityModule.WallpaperUBound, WallpaperIndex - 1) '上一张
        Else
            WallpaperIndex = (WallpaperIndex + 1) Mod (UnityModule.WallpaperUBound + 1) '下一张
        End If
        Me.BackgroundImage = My.Resources.SystemAssets.ResourceManager.GetObject("SystemWallpaper_" & WallpaperIndex.ToString("00"))
        My.Settings.LoginWallpaperIndex = WallpaperIndex
        My.Settings.Save()
    End Sub

    Private Sub WallpaperButton_MouseDown(sender As Object, e As MouseEventArgs) Handles LastWallpaperButton.MouseDown, NextWallpaperButton.MouseDown
        CType(sender, Label).Image = My.Resources.SystemAssets.ResourceManager.GetObject(CType(sender, Label).Tag & "_Down")
    End Sub

    Private Sub WallpaperButton_MouseEnter(sender As Object, e As EventArgs) Handles LastWallpaperButton.MouseEnter, NextWallpaperButton.MouseEnter
        CType(sender, Label).Image = My.Resources.SystemAssets.ResourceManager.GetObject(CType(sender, Label).Tag & "_Enter")
    End Sub

    Private Sub WallpaperButton_MouseLeave(sender As Object, e As EventArgs) Handles LastWallpaperButton.MouseLeave, NextWallpaperButton.MouseLeave
        CType(sender, Label).Image = My.Resources.SystemAssets.ResourceManager.GetObject(CType(sender, Label).Tag & "_Normal")
    End Sub

    Private Sub WallpaperButton_MouseUp(sender As Object, e As MouseEventArgs) Handles LastWallpaperButton.MouseUp, NextWallpaperButton.MouseUp
        CType(sender, Label).Image = My.Resources.SystemAssets.ResourceManager.GetObject(CType(sender, Label).Tag & "_Enter")
    End Sub

#End Region

#Region "动态显示和隐藏"

    ''' <summary>
    ''' 动态显示锁屏界面
    ''' </summary>
    Public Sub ShowLockScreen()
        UnityModule.LockScreenMode = True
        If Me.Visible Then Exit Sub Else Me.Show(SystemWorkStation)
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("ShowConsole"), AudioPlayMode.Background)
        If TipsForm.Visible Then TipsForm.CancelTip()
        UnityModule.SetForegroundWindow(Me.Handle)
        If ThreadShowMe IsNot Nothing AndAlso ThreadShowMe.ThreadState = ThreadState.Running Then Exit Sub
        ThreadShowMe = New Thread(AddressOf ShowMe)
        ThreadShowMe.Start()
        ThreadShowMe.Join()
    End Sub

    Private Sub ShowMe()
        '动态显示
        For Index As Integer = 1 To 10
            Me.Opacity = Index / 10
            Thread.Sleep(50)
        Next
    End Sub

    ''' <summary>
    ''' 鼠标向左或向右动态隐藏锁屏界面
    ''' </summary>
    ''' <param name="ToRight">指定向左隐藏还是向右隐藏</param>
    Public Sub HideLockScreen(ByVal ToRight As Boolean)
        '动态隐藏
        If ThreadHideMe IsNot Nothing AndAlso ThreadHideMe.ThreadState = ThreadState.Running Then Exit Sub
        ThreadHideMe = New Thread(AddressOf HideMe)
        ThreadHideMe.Start(ToRight)
        ThreadHideMe.Join()
        UnityModule.SetForegroundWindow(SystemWorkStation.Handle)
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

    ''' <summary>
    ''' 上下分割窗体的登录特效
    ''' </summary>
    Public Sub SplitLoginIn()
        ShowSplitForm()
        Do While Me.Bottom > 0
            Me.Top -= SplitDistance
            LockSplitForm.Top += SplitDistance
            Me.Opacity = 0.5 * (Me.Bottom / Me.Height) + 0.5
            LockSplitForm.Opacity = Me.Opacity
            Thread.Sleep(15)
        Loop
        Me.Opacity = 0
        Me.Location = New Point(0, -My.Computer.Screen.Bounds.Height)
        LoginAreaControl.Show()
        Me.Height = My.Computer.Screen.Bounds.Height
        Me.BackgroundImage = My.Resources.SystemAssets.ResourceManager.GetObject("SystemWallpaper_" & WallpaperIndex.ToString("00"))
        Me.Refresh()
        Me.Hide()
        Me.Location = New Point(0, 0)
        LockSplitForm.Close()
    End Sub
#End Region

#Region "功能函数"

    ''' <summary>
    ''' 为分离登陆特效显示下半部分窗体
    ''' </summary>
    Private Sub ShowSplitForm()
        Dim MyScreenShot As Bitmap = CopyFromMe()
        LoginAreaControl.Hide()
        LockSplitForm.Show(SystemWorkStation)
        LockSplitForm.BackgroundImage = MyScreenShot.Clone(New Rectangle(0, LockSplitForm.Top - 1, LockSplitForm.Width, LockSplitForm.Height), MyScreenShot.PixelFormat)
        LockSplitForm.Refresh()
        Me.Height = Me.Height / 2
        Me.BackgroundImage = MyScreenShot.Clone(New Rectangle(0, 0, Me.Width, Me.Height), MyScreenShot.PixelFormat)
    End Sub

    ''' <summary>
    ''' 制作当前窗口的截图
    ''' </summary>
    ''' <returns>当前窗口的截图</returns>
    Private Function CopyFromMe() As Bitmap
        Dim MyScreenShot As Bitmap = New Bitmap(Me.BackgroundImage, Me.Size)
        LoginAreaControl.DrawToBitmap(MyScreenShot, New Rectangle(LoginAreaControl.Location, LoginAreaControl.Size))
        LastWallpaperButton.DrawToBitmap(MyScreenShot, New Rectangle(LastWallpaperButton.Location, LastWallpaperButton.Size))
        NextWallpaperButton.DrawToBitmap(MyScreenShot, New Rectangle(NextWallpaperButton.Location, NextWallpaperButton.Size))
        Return MyScreenShot
    End Function


    ''' <summary>
    ''' 把 Base64 加密的文本转换为图像
    ''' </summary>
    ''' <param name="Base64"></param>
    ''' <returns>转换出的图像</returns>
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

    ''' <summary>
    ''' 登录时切换工作窗口
    ''' </summary>
    Private Sub ExchangeUI()
        If PasswordTextBox.Text.ToLower = "resetuser" Then
            '密码输入框输入"resetuser"可以恢复初始头像和用户名
            ResetUserConfig()
            PasswordTextBox.Text = "LoginMeIn"
            PasswordTextBox.SelectionStart = 9
        Else
            '可以在这里设置判断登录密码或设计彩！蛋！
            '切换界面时需要收回隐藏 TipsForm 浮窗
            If TipsForm.Visible Then TipsForm.CancelTip()
            SystemWorkStation.Show()

            ThreadHideMe = New Thread(AddressOf SplitLoginIn)
            ThreadHideMe.Start()
            ThreadHideMe.Join()

            If LockScreenMode Then
                '解锁
                My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("Tips"), AudioPlayMode.Background)
                LockScreenMode = False
            End If
            UnityModule.SetForegroundWindow(SystemWorkStation.Handle)
            'SystemWorkStation 初次显示时会自动 Activated 并置前显示，导致 FirstLoginIn() 特效无法置前显示，所以需要特效结束后注册事件
            AddHandler SystemWorkStation.Activated, AddressOf SystemWorkStation.SystemWorkStation_Activated
            '非锁屏状态时，不允许通过鼠标拖动的方式登录系统，所以首先登录一次绑定事件
            AddHandler Me.MouseUp, AddressOf LoginAndLockUI_MouseUp
            AddHandler Me.MouseDown, AddressOf LoginAndLockUI_MouseDown
        End If
    End Sub

    ''' <summary>
    ''' 初始化用户配置
    ''' </summary>
    Private Sub ResetUserConfig()
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
        TipsForm.PopupTips(Me, "Successfully !", TipsForm.TipsIconType.Infomation, "Reset head successfully")

        Me.Activate()
    End Sub

    ''' <summary>
    ''' 鼠标拖动距离太少不足以解锁时用于恢复锁屏状态
    ''' </summary>
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

#End Region

#Region "使用 TextBox 在后台为前台的 Label 响应按键操作"

    Private Sub PasswordTextBox_TextChanged(sender As Object, e As EventArgs) Handles PasswordTextBox.TextChanged
        '使 PasswordLabel 显示 PasswordTextBox 显示的文本
        PasswordLabel.Text = Strings.StrDup(PasswordTextBox.Text.Length, PasswordTextBox.PasswordChar)
    End Sub

    Private Sub PasswordTextBox_LostFocus(sender As Object, e As EventArgs) Handles PasswordTextBox.LostFocus
        '防止 PasswordTextBox 失去焦点导致无法接收按键消息
        PasswordTextBox.Focus()
    End Sub

    Private Sub PasswordLabel_MouseEnter(sender As Object, e As EventArgs) Handles PasswordLabel.MouseEnter
        PasswordLabel.Image = My.Resources.SystemAssets.PasswordInputBox_Enter
    End Sub

    Private Sub PasswordLabel_MouseLeave(sender As Object, e As EventArgs) Handles PasswordLabel.MouseLeave
        PasswordLabel.Image = My.Resources.SystemAssets.PasswordInputBox_Normal
    End Sub

    Private Sub PasswordLabel_MouseUp(sender As Object, e As MouseEventArgs) Handles PasswordLabel.MouseUp
        PasswordLabel.Image = My.Resources.SystemAssets.PasswordInputBox_Enter
    End Sub

    Private Sub PasswordLabel_MouseDown(sender As Object, e As MouseEventArgs) Handles PasswordLabel.MouseDown
        PasswordLabel.Image = My.Resources.SystemAssets.PasswordInputBox_Down
    End Sub

#End Region

End Class
