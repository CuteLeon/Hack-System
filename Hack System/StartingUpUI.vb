Imports System.ComponentModel

Public Class StartingUpUI
#Region "窗体"

    Private Sub StartingUpUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '全屏显示
        Me.Location = New Point(0, 0)
        Me.Size = My.Computer.Screen.Bounds.Size
        '设置窗体图标
        Me.Icon = My.Resources.SystemAssets.HackSystem
        Me.Cursor = SystemCursor
        '初始化界面
        StartingUpControl.Location = New Point((My.Computer.Screen.Bounds.Width - StartingUpControl.Width) / 2, (My.Computer.Screen.Bounds.Height - StartingUpControl.Height) * 0.85)
        StartUpLogo.Location = New Point((My.Computer.Screen.Bounds.Width - My.Resources.SystemAssets.HackSystemLogo.Width) \ 2, My.Computer.Screen.Bounds.Height \ 4)

        StartingUpLable.Left = (My.Computer.Screen.Bounds.Width - StartingUpLable.Width) / 2
        StartingUpLable.Top = StartingUpControl.Bottom + 10
    End Sub

    Private Sub StartingUpUI_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        LoginAndLockUI.Icon = My.Resources.SystemAssets.HackSystem
        SystemWorkStation.Icon = My.Resources.SystemAssets.HackSystem
        WindowsTemplates.Icon = My.Resources.SystemAssets.HackSystem
        ShutdowningUI.Icon = My.Resources.SystemAssets.HackSystem
        DownloaderForm.Icon = My.Resources.SystemAssets.HackSystem

        DownloaderForm.Cursor = SystemCursor
        AboutMeForm.Cursor = SystemCursor
        CommandConsole.Cursor = SystemCursor
        CommandConsole.CommandPast.Cursor = SystemCursor
        CommandConsole.CommandButton.Cursor = SystemCursor
        LockSplitForm.Cursor = SystemCursor
        XYMail.Cursor = SystemCursor
        LoginAndLockUI.Cursor = SystemCursor
        MineSweeperForm.Cursor = SystemCursor
        MineSweeperForm.MinefieldPanel.Cursor = SystemCursor
        Game1010Form.Cursor = SystemCursor
        Game2048Form.Cursor = SystemCursor
        ShutdownTips.Cursor = SystemCursor
        ShutdowningUI.Cursor = SystemCursor
        SystemWorkStation.Cursor = SystemCursor
        SystemWorkStation.InfoTitle.Cursor = SystemCursor
        SystemWorkStation.CPUCounterBar.Cursor = SystemCursor
        SystemWorkStation.MemoryUsageRateBar.Cursor = SystemCursor
        SystemWorkStation.DiskReadCounterLabel.Cursor = SystemCursor
        SystemWorkStation.DiskWriteCounterLabel.Cursor = SystemCursor
        SystemWorkStation.UploadSpeedCountLabel.Cursor = SystemCursor
        SystemWorkStation.DownloadSpeedCountLabel.Cursor = SystemCursor
        SystemWorkStation.DateTimeLabel.Cursor = SystemCursor
        TipsForm.Cursor = SystemCursor

        '播放开机音效
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("StartedUp"), AudioPlayMode.Background)
    End Sub

    Private Sub StartingUpUI_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        '关闭启动界面时，系统关闭
        Application.Exit()
    End Sub
#End Region

#Region "控件"

    Private Sub StartingUpTimer_Tick(sender As Object, e As EventArgs) Handles StartingUpTimer.Tick
        '前50帧用于播放加载进度动画
        Static FrameIndex As Integer = 0
        StartingUpControl.Image = My.Resources.SystemAssets.ResourceManager.GetObject("StartingUp_" & FrameIndex)
        FrameIndex += 1

        StartingUpLable.Text = "Hack System Loading... (" & 2 * FrameIndex & "%)"
        Me.Text = StartingUpLable.Text
        '50帧后切换界面
        If FrameIndex = 50 Then
            StartingUpTimer.Enabled = False
            ExchangeUITimer.Enabled = True
            LoginAndLockUI.Show()
            Me.Owner = LoginAndLockUI
            Me.Focus()
        End If
    End Sub

    Private Sub ExchangeUITimer_Tick(sender As Object, e As EventArgs) Handles ExchangeUITimer.Tick
        '后10帧用于淡出淡入切换界面
        Static FrameIndex As Integer = 50
        StartingUpControl.Image = My.Resources.SystemAssets.ResourceManager.GetObject("StartingUp_" & FrameIndex)
        FrameIndex += 1
        Me.Opacity -= 0.1

        If FrameIndex = 60 Then
            '完成切换
            ExchangeUITimer.Enabled = False
            Me.Hide()
            UnityModule.SetForegroundWindow(LoginAndLockUI.Handle)
        End If
    End Sub

#End Region

End Class