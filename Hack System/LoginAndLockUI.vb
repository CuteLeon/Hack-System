Imports System.ComponentModel
Imports System.Threading

Public Class LoginAndLockUI
    Private Declare Function GetCursorPos Lib "user32" (ByRef lpPoint As POINTAPI) As Integer
    Private Structure POINTAPI
        Dim X As Int16
        Dim Y As Int16
    End Structure
    Dim FirstPoint As POINTAPI
    Dim MoveDistance As Integer = My.Computer.Screen.Bounds.Width \ 50
    Dim LockScreenMode As Boolean = False
    Dim MovedMe As Boolean = False
    Dim ThreadShowMe As Thread
    Dim ThreadHideMe As Thread

    'The UpperBound of wallpapers.
    Private Const WallpaperUpperBound As Int16 = 18

    Private Sub LoginAndLockUI_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Allow thread to visit UI.
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        'Full screen
        Me.Location = New Point(0, 0)
        Me.Size = My.Computer.Screen.Bounds.Size
        StartUpLogo.Location = New Point((My.Computer.Screen.Bounds.Width - My.Resources.SystemAssets.HackSystemLogo.Width) \ 2, My.Computer.Screen.Bounds.Height \ 4)
        LoginAreaControl.Left = (My.Computer.Screen.Bounds.Width - LoginAreaControl.Width) / 2
        LoginAreaControl.Top = (My.Computer.Screen.Bounds.Height - LoginAreaControl.Height) / 2
        'Dont select password.
        PasswordControl.SelectionStart = PasswordControl.TextLength
        PasswordControl.SelectionLength = 0

        Me.Cursor = StartingUpUI.SystemCursor
    End Sub

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

    Private Sub LockWallpaperControl_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        'Click to change wallpaper.
        If Not MovedMe Then
            Static Index As Integer = 10
            Me.BackgroundImage = My.Resources.SystemAssets.ResourceManager.GetObject("SystemWallpaper_" & Index.ToString("00"))
            If Index = WallpaperUpperBound Then Index = 0 Else Index += 1
        End If
    End Sub

#Region "Button"
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
#End Region

    'Exchange windows.
    Private Sub LoginButtonControl_Click(sender As Object, e As EventArgs) Handles LoginButtonControl.Click
        ExchangeUI()
    End Sub

    Private Sub PasswordControl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PasswordControl.KeyPress
        If Asc(e.KeyChar) = Keys.Enter Then ExchangeUI()
    End Sub

    Private Sub ExchangeUI()
        If LockScreenMode Then
            HideLockScreen(True)
        Else
            SystemWorkStation.Show()
            Me.Hide()
        End If
        SystemWorkStation.Focus()
        AddHandler Me.MouseUp, AddressOf LoginAndLockUI_MouseUp
        AddHandler Me.MouseDown, AddressOf LoginAndLockUI_MouseDown
        LockScreenMode = True
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
        MovedMe = True
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
        MovedMe = False
        GetCursorPos(FirstPoint)
        AddHandler Me.MouseMove, AddressOf LoginAndLockUI_MouseMove
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
End Class
