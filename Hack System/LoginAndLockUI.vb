Imports System.ComponentModel
Imports System.Threading

Public Class LoginAndLockUI

#Region "声明区"

    Private Declare Function GetCursorPos Lib "user32" (ByRef lpPoint As POINTAPI) As Integer

    Private Structure POINTAPI
        Dim X As Int16
        Dim Y As Int16
    End Structure

    Private Const WallpaperCount As Int16 = 19
    Public HeadSize As Size = New Size(159, 159)
    Public UserHead As Bitmap
    Public UserName As String
    Public UserNameBitmap As Bitmap
    Public UserHeadString As String
    Public UserNameString As String
    Public LockScreenMode As Boolean = False
    Dim WallpaperIndex As Integer = 14
    Dim FirstPoint As POINTAPI
    Dim MoveDistance As Integer = My.Computer.Screen.Bounds.Width \ 50
    Dim ThreadShowMe As Thread
    Dim ThreadHideMe As Thread
#End Region

#Region "窗体"

    Private Sub LoginAndLockUI_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Allow thread to visit UI.
        CheckForIllegalCrossThreadCalls = False
        'Full screen
        Me.Location = New Point(0, 0)
        Me.Size = My.Computer.Screen.Bounds.Size

        Dim WallpaperIndexSetting As String = My.Settings.LoginWallpaperIndex
        If Not WallpaperIndexSetting = vbNullString Then
            WallpaperIndex = Int(WallpaperIndexSetting)
            Me.BackgroundImage = My.Resources.SystemAssets.ResourceManager.GetObject("SystemWallpaper_" & WallpaperIndex.ToString("00"))
        End If

        UserHeadString = My.Settings.UserHead
        If UserHeadString = vbNullString Then UserHead = My.Resources.SystemAssets.DefaultUserHead Else UserHead = StringToBitmap(UserHeadString)
        HeadPictureBox.BackgroundImage = UserHead

        UserNameString = My.Settings.UserNameBitmap
        If UserNameString = vbNullString Then UserNameBitmap = My.Resources.SystemAssets.DefaultUserName Else UserNameBitmap = StringToBitmap(UserNameString)
        UserNameControl.Image = UserNameBitmap

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
        'Dont select password.
        PasswordControl.SelectionStart = PasswordControl.TextLength
        PasswordControl.SelectionLength = 0

        Me.Cursor = StartingUpUI.SystemCursor
    End Sub

    Private Sub LoginAndLockUI_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
    End Sub

    Private Sub LoginAndLockUI_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Play audio,loop.
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("LockUIBGM"), AudioPlayMode.BackgroundLoop)
    End Sub

    Private Sub LoginAndLockUI_MouseMove(sender As Object, e As MouseEventArgs)
        'Allow mouse to move window.
        Dim NowPoint As POINTAPI
        GetCursorPos(NowPoint)
        Me.Left += NowPoint.X - FirstPoint.X
        Me.Opacity = 0.5 * IIf(Me.Left > 0, 1 - Me.Left / My.Computer.Screen.Bounds.Width, (My.Computer.Screen.Bounds.Width + Me.Left) / My.Computer.Screen.Bounds.Width) + 0.5
        FirstPoint = NowPoint
    End Sub

    Private Sub LoginAndLockUI_MouseUp(sender As Object, e As MouseEventArgs)
        RemoveHandler Me.MouseMove, AddressOf LoginAndLockUI_MouseMove
        If Me.Left > Me.Width \ 3 Then
            HideLockScreen(True)
        ElseIf Me.Left < -Me.Width \ 3 Then
            HideLockScreen(False)
        Else
            Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf ReSetMyLocation))
        End If
    End Sub

    Private Sub LoginAndLockUI_MouseDown(sender As Object, e As MouseEventArgs)
        GetCursorPos(FirstPoint)
        AddHandler Me.MouseMove, AddressOf LoginAndLockUI_MouseMove
    End Sub

    Private Sub LoginAndLockUI_Click(sender As Object, e As EventArgs) Handles Me.Click
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        'Click to change wallpaper.
        If Me.Left ^ 2 < 25 Then
            If WallpaperIndex = WallpaperCount Then WallpaperIndex = 0 Else WallpaperIndex += 1
            Me.BackgroundImage = My.Resources.SystemAssets.ResourceManager.GetObject("SystemWallpaper_" & WallpaperIndex.ToString("00"))
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
        ExchangeUI()
    End Sub

    Private Sub PasswordControl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PasswordControl.KeyPress
        If Asc(e.KeyChar) = Keys.Enter Then ExchangeUI()
    End Sub
#End Region

#Region "动态显示和隐藏"

    Public Sub ShowLockScreen()
        If TipsForm.Visible Then TipsForm.CancelTip()
        If ThreadShowMe IsNot Nothing AndAlso ThreadShowMe.ThreadState = ThreadState.Running Then Exit Sub
        ThreadShowMe = New Thread(AddressOf ShowMe)
        ThreadShowMe.Start()
    End Sub

    Private Sub ShowMe()
        For Index As Integer = 1 To 10
            Me.Opacity = Index / 10
            Thread.Sleep(50)
        Next
    End Sub

    Public Sub HideLockScreen(ByVal ToRight As Boolean)
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
#End Region

#Region "功能函数"

    Public Function StringToBitmap(ByVal Base64 As String) As Bitmap
        Try
            Dim EncryptByte() As Byte = Convert.FromBase64String(Base64)
            Dim BitmapStream As IO.MemoryStream = New IO.MemoryStream(EncryptByte)
            Return Bitmap.FromStream(BitmapStream)
        Catch ex As Exception
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

            If Not TipsForm.Visible Then TipsForm.Show(Me)
            TipsForm.PopupTips("Successfully !", TipsForm.TipsIconType.Infomation, "Reset head successfully")

            Me.Activate()
        Else
            If LockScreenMode Then
                My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("Tips"), AudioPlayMode.Background)
                HideLockScreen(True)
                LockScreenMode = False
            Else
                SystemWorkStation.Show()
                Me.Hide()
            End If
            SystemWorkStation.Focus()
            AddHandler Me.MouseUp, AddressOf LoginAndLockUI_MouseUp
            AddHandler Me.MouseDown, AddressOf LoginAndLockUI_MouseDown
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

#End Region

End Class
