<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class XYBrowser
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MainWebBrowser = New System.Windows.Forms.WebBrowser()
        Me.BrowserState = New System.Windows.Forms.Label()
        Me.TopPanel = New System.Windows.Forms.Panel()
        Me.Btn_FullScreen = New System.Windows.Forms.Label()
        Me.SearchTextBox = New System.Windows.Forms.TextBox()
        Me.Btn_Max = New System.Windows.Forms.Label()
        Me.Btn_GoForward = New System.Windows.Forms.Label()
        Me.Btn_Search = New System.Windows.Forms.Label()
        Me.Btn_GoNavigate = New System.Windows.Forms.Label()
        Me.BrowserAddress = New System.Windows.Forms.ComboBox()
        Me.BrowserTitle = New System.Windows.Forms.Label()
        Me.Btn_Close = New System.Windows.Forms.Label()
        Me.Btn_Refresh = New System.Windows.Forms.Label()
        Me.Btn_GoBack = New System.Windows.Forms.Label()
        Me.Btn_Home = New System.Windows.Forms.Label()
        Me.Btn_NowStop = New System.Windows.Forms.Label()
        Me.Btn_Restore = New System.Windows.Forms.Label()
        Me.Btn_DragBlock = New System.Windows.Forms.Label()
        Me.TopPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainWebBrowser
        '
        Me.MainWebBrowser.Location = New System.Drawing.Point(1, 57)
        Me.MainWebBrowser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.MainWebBrowser.Name = "MainWebBrowser"
        Me.MainWebBrowser.ScriptErrorsSuppressed = True
        Me.MainWebBrowser.Size = New System.Drawing.Size(798, 524)
        Me.MainWebBrowser.TabIndex = 0
        '
        'BrowserState
        '
        Me.BrowserState.BackColor = System.Drawing.Color.White
        Me.BrowserState.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.BrowserState.Location = New System.Drawing.Point(1, 581)
        Me.BrowserState.Name = "BrowserState"
        Me.BrowserState.Size = New System.Drawing.Size(780, 18)
        Me.BrowserState.TabIndex = 9
        Me.BrowserState.Text = "XY Browser"
        '
        'TopPanel
        '
        Me.TopPanel.BackColor = System.Drawing.Color.White
        Me.TopPanel.Controls.Add(Me.Btn_FullScreen)
        Me.TopPanel.Controls.Add(Me.SearchTextBox)
        Me.TopPanel.Controls.Add(Me.Btn_Max)
        Me.TopPanel.Controls.Add(Me.Btn_GoForward)
        Me.TopPanel.Controls.Add(Me.Btn_Search)
        Me.TopPanel.Controls.Add(Me.Btn_GoNavigate)
        Me.TopPanel.Controls.Add(Me.BrowserAddress)
        Me.TopPanel.Controls.Add(Me.BrowserTitle)
        Me.TopPanel.Controls.Add(Me.Btn_Close)
        Me.TopPanel.Controls.Add(Me.Btn_Refresh)
        Me.TopPanel.Controls.Add(Me.Btn_GoBack)
        Me.TopPanel.Controls.Add(Me.Btn_Home)
        Me.TopPanel.Controls.Add(Me.Btn_NowStop)
        Me.TopPanel.Controls.Add(Me.Btn_Restore)
        Me.TopPanel.Location = New System.Drawing.Point(1, 1)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Size = New System.Drawing.Size(798, 57)
        Me.TopPanel.TabIndex = 16
        '
        'Btn_FullScreen
        '
        Me.Btn_FullScreen.BackColor = System.Drawing.Color.White
        Me.Btn_FullScreen.Image = Global.HackSystem.My.Resources.XYBrowserRes.FullScreen_N
        Me.Btn_FullScreen.Location = New System.Drawing.Point(714, 0)
        Me.Btn_FullScreen.Name = "Btn_FullScreen"
        Me.Btn_FullScreen.Size = New System.Drawing.Size(28, 28)
        Me.Btn_FullScreen.TabIndex = 30
        Me.Btn_FullScreen.Tag = "FullScreen"
        '
        'SearchTextBox
        '
        Me.SearchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SearchTextBox.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.SearchTextBox.ForeColor = System.Drawing.Color.DimGray
        Me.SearchTextBox.Location = New System.Drawing.Point(620, 30)
        Me.SearchTextBox.Name = "SearchTextBox"
        Me.SearchTextBox.Size = New System.Drawing.Size(150, 23)
        Me.SearchTextBox.TabIndex = 26
        Me.SearchTextBox.Text = "Search..."
        '
        'Btn_Max
        '
        Me.Btn_Max.BackColor = System.Drawing.Color.White
        Me.Btn_Max.Image = Global.HackSystem.My.Resources.XYBrowserRes.Max_N
        Me.Btn_Max.Location = New System.Drawing.Point(742, 0)
        Me.Btn_Max.Name = "Btn_Max"
        Me.Btn_Max.Size = New System.Drawing.Size(28, 28)
        Me.Btn_Max.TabIndex = 22
        Me.Btn_Max.Tag = "Max"
        '
        'Btn_GoForward
        '
        Me.Btn_GoForward.BackColor = System.Drawing.Color.White
        Me.Btn_GoForward.Location = New System.Drawing.Point(56, 28)
        Me.Btn_GoForward.Name = "Btn_GoForward"
        Me.Btn_GoForward.Size = New System.Drawing.Size(28, 28)
        Me.Btn_GoForward.TabIndex = 28
        Me.Btn_GoForward.Tag = "GoForward"
        '
        'Btn_Search
        '
        Me.Btn_Search.BackColor = System.Drawing.Color.White
        Me.Btn_Search.Location = New System.Drawing.Point(770, 28)
        Me.Btn_Search.Name = "Btn_Search"
        Me.Btn_Search.Size = New System.Drawing.Size(28, 28)
        Me.Btn_Search.TabIndex = 27
        Me.Btn_Search.Tag = "Search"
        '
        'Btn_GoNavigate
        '
        Me.Btn_GoNavigate.BackColor = System.Drawing.Color.White
        Me.Btn_GoNavigate.Location = New System.Drawing.Point(592, 28)
        Me.Btn_GoNavigate.Name = "Btn_GoNavigate"
        Me.Btn_GoNavigate.Size = New System.Drawing.Size(28, 28)
        Me.Btn_GoNavigate.TabIndex = 25
        Me.Btn_GoNavigate.Tag = "GoNavigate"
        '
        'BrowserAddress
        '
        Me.BrowserAddress.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.BrowserAddress.ForeColor = System.Drawing.Color.DimGray
        Me.BrowserAddress.FormattingEnabled = True
        Me.BrowserAddress.Location = New System.Drawing.Point(113, 29)
        Me.BrowserAddress.Name = "BrowserAddress"
        Me.BrowserAddress.Size = New System.Drawing.Size(478, 25)
        Me.BrowserAddress.TabIndex = 24
        '
        'BrowserTitle
        '
        Me.BrowserTitle.AutoEllipsis = True
        Me.BrowserTitle.BackColor = System.Drawing.Color.White
        Me.BrowserTitle.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.BrowserTitle.Image = Global.HackSystem.My.Resources.XYBrowserRes.XYBrowser1
        Me.BrowserTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BrowserTitle.Location = New System.Drawing.Point(0, 0)
        Me.BrowserTitle.Name = "BrowserTitle"
        Me.BrowserTitle.Size = New System.Drawing.Size(714, 28)
        Me.BrowserTitle.TabIndex = 23
        Me.BrowserTitle.Text = "XY Browser"
        Me.BrowserTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Btn_Close
        '
        Me.Btn_Close.BackColor = System.Drawing.Color.White
        Me.Btn_Close.Image = Global.HackSystem.My.Resources.XYBrowserRes.Close_N
        Me.Btn_Close.Location = New System.Drawing.Point(770, 0)
        Me.Btn_Close.Name = "Btn_Close"
        Me.Btn_Close.Size = New System.Drawing.Size(28, 28)
        Me.Btn_Close.TabIndex = 21
        Me.Btn_Close.Tag = "Close"
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(84, 28)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(28, 28)
        Me.Btn_Refresh.TabIndex = 17
        Me.Btn_Refresh.Tag = "Refresh"
        '
        'Btn_GoBack
        '
        Me.Btn_GoBack.BackColor = System.Drawing.Color.White
        Me.Btn_GoBack.Location = New System.Drawing.Point(0, 28)
        Me.Btn_GoBack.Name = "Btn_GoBack"
        Me.Btn_GoBack.Size = New System.Drawing.Size(28, 28)
        Me.Btn_GoBack.TabIndex = 16
        Me.Btn_GoBack.Tag = "GoBack"
        '
        'Btn_Home
        '
        Me.Btn_Home.BackColor = System.Drawing.Color.White
        Me.Btn_Home.Location = New System.Drawing.Point(28, 28)
        Me.Btn_Home.Name = "Btn_Home"
        Me.Btn_Home.Size = New System.Drawing.Size(28, 28)
        Me.Btn_Home.TabIndex = 18
        Me.Btn_Home.Tag = "Home"
        '
        'Btn_NowStop
        '
        Me.Btn_NowStop.BackColor = System.Drawing.Color.White
        Me.Btn_NowStop.Location = New System.Drawing.Point(56, 28)
        Me.Btn_NowStop.Name = "Btn_NowStop"
        Me.Btn_NowStop.Size = New System.Drawing.Size(28, 28)
        Me.Btn_NowStop.TabIndex = 19
        Me.Btn_NowStop.Tag = "NowStop"
        Me.Btn_NowStop.Visible = False
        '
        'Btn_Restore
        '
        Me.Btn_Restore.BackColor = System.Drawing.Color.White
        Me.Btn_Restore.Image = Global.HackSystem.My.Resources.XYBrowserRes.Restore_N
        Me.Btn_Restore.Location = New System.Drawing.Point(742, 0)
        Me.Btn_Restore.Name = "Btn_Restore"
        Me.Btn_Restore.Size = New System.Drawing.Size(28, 28)
        Me.Btn_Restore.TabIndex = 29
        Me.Btn_Restore.Tag = "Restore"
        Me.Btn_Restore.Visible = False
        '
        'Btn_DragBlock
        '
        Me.Btn_DragBlock.BackColor = System.Drawing.Color.White
        Me.Btn_DragBlock.Cursor = System.Windows.Forms.Cursors.SizeNWSE
        Me.Btn_DragBlock.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Btn_DragBlock.Image = Global.HackSystem.My.Resources.XYBrowserRes.DragBlock
        Me.Btn_DragBlock.Location = New System.Drawing.Point(781, 581)
        Me.Btn_DragBlock.Name = "Btn_DragBlock"
        Me.Btn_DragBlock.Size = New System.Drawing.Size(18, 18)
        Me.Btn_DragBlock.TabIndex = 17
        '
        'XYBrowser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.MainWebBrowser)
        Me.Controls.Add(Me.Btn_DragBlock)
        Me.Controls.Add(Me.BrowserState)
        Me.Controls.Add(Me.TopPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "XYBrowser"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "XY浏览器"
        Me.TopPanel.ResumeLayout(False)
        Me.TopPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents MainWebBrowser As WebBrowser
    Friend WithEvents BrowserState As Label
    Friend WithEvents TopPanel As Panel
    Friend WithEvents Btn_NowStop As Label
    Friend WithEvents Btn_Max As Label
    Friend WithEvents Btn_Home As Label
    Friend WithEvents Btn_GoBack As Label
    Friend WithEvents Btn_Refresh As Label
    Friend WithEvents Btn_Close As Label
    Friend WithEvents BrowserTitle As Label
    Friend WithEvents BrowserAddress As ComboBox
    Friend WithEvents Btn_GoNavigate As Label
    Friend WithEvents Btn_Search As Label
    Friend WithEvents SearchTextBox As TextBox
    Friend WithEvents Btn_Restore As Label
    Friend WithEvents Btn_GoForward As Label
    Friend WithEvents Btn_DragBlock As Label
    Friend WithEvents Btn_FullScreen As Label
End Class
