Public Class AboutMeForm
    Dim LabelForeColor As Color = Color.Aqua

    Private Sub AboutMeForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Set control's parent.
        AboutMeControl.Parent = AboutMeWallpaperControl
        OKButtonControl.Parent = AboutMeControl
        SetLabelColorLabel.Parent = AboutMeControl
        WebLink.Parent = AboutMeControl
        OKButtonControl.Image = My.Resources.SystemAssets.OKButton.Clone(New Rectangle(0, 0, 140, 49), Imaging.PixelFormat.Format32bppArgb)
        'Set mouse curor.
        Me.Cursor = StartingUpUI.SystemCursor
    End Sub

    Private Sub AboutMeControl_MouseDown(sender As Object, e As MouseEventArgs) Handles AboutMeControl.MouseDown
        'Allow mouse to move window.
        WindowsTemplates.ReleaseCapture()
        WindowsTemplates.SendMessageA(Me.Handle, &HA1, 2, 0&)
    End Sub

    Private Sub OKButtonControl_Click(sender As Object, e As EventArgs) Handles OKButtonControl.Click
        'Click OKButton to and hide me and focus main desktop.
        My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
        Me.Hide()
        SystemWorkStation.Focus()
    End Sub

#Region "The dynamic effect on the OKButton"
    Private Sub OKButtonControl_MouseDown(sender As Object, e As MouseEventArgs) Handles OKButtonControl.MouseDown
        OKButtonControl.Image = My.Resources.SystemAssets.OKButton.Clone(New Rectangle(280, 0, 140, 49), Imaging.PixelFormat.Format32bppArgb)
    End Sub

    Private Sub OKButtonControl_MouseEnter(sender As Object, e As EventArgs) Handles OKButtonControl.MouseEnter
        OKButtonControl.Image = My.Resources.SystemAssets.OKButton.Clone(New Rectangle(140, 0, 140, 49), Imaging.PixelFormat.Format32bppArgb)
    End Sub

    Private Sub OKButtonControl_MouseLeave(sender As Object, e As EventArgs) Handles OKButtonControl.MouseLeave
        OKButtonControl.Image = My.Resources.SystemAssets.OKButton.Clone(New Rectangle(0, 0, 140, 49), Imaging.PixelFormat.Format32bppArgb)
    End Sub

    Private Sub OKButtonControl_MouseUp(sender As Object, e As MouseEventArgs) Handles OKButtonControl.MouseUp
        OKButtonControl.Image = My.Resources.SystemAssets.OKButton.Clone(New Rectangle(140, 0, 140, 49), Imaging.PixelFormat.Format32bppArgb)
    End Sub
#End Region

    Private Sub AboutMe_KeyPress(sender As Object, e As KeyPressEventArgs) Handles AboutMeControl.KeyPress, Me.KeyPress, WebLink.KeyPress
        'Press [Enter] or [Esc] to hide me and focus main desktop.
        Dim KeyAscii As Integer = Asc(e.KeyChar)
        If KeyAscii = 27 Or KeyAscii = 13 Then
            My.Computer.Audio.Play(My.Resources.SystemAssets.ResourceManager.GetStream("MouseClick"), AudioPlayMode.Background)
            Me.Hide()
            SystemWorkStation.Focus()
        End If
    End Sub

    Private Sub WebLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles WebLink.LinkClicked
        '访问官网
        SystemWorkStation.LoadNewBrowser("http://www.HackSystem.icoc.in/")
    End Sub

    Private Sub SetLabelColorLabel_Click(sender As Object, e As EventArgs) Handles SetLabelColorLabel.Click
        '修改系统文本标签的字体颜色
        SetLabelForecolor()
    End Sub

    Public Sub SetLabelForecolor()
        LabelColorDialog.Color = LabelForeColor
        If LabelColorDialog.ShowDialog = DialogResult.OK Then
            LabelForeColor = LabelColorDialog.Color
            SetLabelColorLabel.ForeColor = LabelForeColor

            For Each ScriptIcon As Label In SystemWorkStation.ScriptIcons
                ScriptIcon.ForeColor = LabelForeColor
            Next

            SystemWorkStation.InfoTitle.ForeColor = LabelForeColor
            SystemWorkStation.CPUCounterLable.ForeColor = LabelForeColor
            SystemWorkStation.MemoryUsageRateLabel.ForeColor = LabelForeColor
            SystemWorkStation.DiskReadCounterLabel.ForeColor = LabelForeColor
            SystemWorkStation.DiskWriteCounterLabel.ForeColor = LabelForeColor
            SystemWorkStation.UploadSpeedCountLabel.ForeColor = LabelForeColor
            SystemWorkStation.DownloadSpeedCountLabel.ForeColor = LabelForeColor
            SystemWorkStation.IPLabel.ForeColor = LabelForeColor
            SystemWorkStation.AddressLabel.ForeColor = LabelForeColor
            SystemWorkStation.DateTimeLabel.ForeColor = LabelForeColor

            SystemWorkStation.ConsoleButtonControl.ForeColor = LabelForeColor
            SystemWorkStation.XYMailButtonControl.ForeColor = LabelForeColor
            SystemWorkStation.XYBrowserButtonControl.ForeColor = LabelForeColor
            SystemWorkStation.ShutdownButtonControl.ForeColor = LabelForeColor
            SystemWorkStation.SettingButtonControl.ForeColor = LabelForeColor
        End If
    End Sub

    Private Sub SetLabelColor_MouseEnter(sender As Object, e As EventArgs) Handles SetLabelColorLabel.MouseEnter
        SetLabelColorLabel.Font = New Font(SetLabelColorLabel.Font.FontFamily, SetLabelColorLabel.Font.Size, FontStyle.Underline Or FontStyle.Bold)
    End Sub

    Private Sub SetLabelColor_MouseLeave(sender As Object, e As EventArgs) Handles SetLabelColorLabel.MouseLeave
        SetLabelColorLabel.Font = New Font(SetLabelColorLabel.Font.FontFamily, SetLabelColorLabel.Font.Size, FontStyle.Bold)
    End Sub
End Class