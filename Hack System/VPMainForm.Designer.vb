<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class VPMainForm
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.CloseButton = New System.Windows.Forms.Label()
        Me.CreateJobListButton = New System.Windows.Forms.Label()
        Me.LogoLabel = New System.Windows.Forms.Label()
        Me.TimeLinePanel = New System.Windows.Forms.Label()
        Me.DispatchPanel = New System.Windows.Forms.Label()
        Me.PlayPauseButton = New System.Windows.Forms.Label()
        Me.SystemClockTitle = New System.Windows.Forms.Label()
        Me.SystemClockLabel = New System.Windows.Forms.Label()
        Me.SystemClockTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SpeedDownButton = New System.Windows.Forms.Label()
        Me.SpeedUpButton = New System.Windows.Forms.Label()
        Me.TimeLineLabel = New System.Windows.Forms.Label()
        Me.WaitLabel = New System.Windows.Forms.Label()
        Me.ExecuteLabel = New System.Windows.Forms.Label()
        Me.NextJobTipLabel = New System.Windows.Forms.Label()
        Me.DispatchComboBox = New System.Windows.Forms.ComboBox()
        Me.SettingButton = New System.Windows.Forms.Label()
        Me.RecordPanel = New System.Windows.Forms.Label()
        Me.ReplayCheckBox = New System.Windows.Forms.CheckBox()
        Me.TimeSliceLabel = New System.Windows.Forms.Label()
        Me.TimeSliceNumeric = New System.Windows.Forms.NumericUpDown()
        Me.LogLabel = New HackSystem.LeonTextBox()
        CType(Me.TimeSliceNumeric, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CloseButton
        '
        Me.CloseButton.BackColor = System.Drawing.Color.Transparent
        Me.CloseButton.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.CloseButton.Image = Global.HackSystem.My.Resources.UnityResource.Close_0
        Me.CloseButton.Location = New System.Drawing.Point(758, 0)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(39, 21)
        Me.CloseButton.TabIndex = 1
        Me.CloseButton.Tag = "Close"
        '
        'CreateJobListButton
        '
        Me.CreateJobListButton.BackColor = System.Drawing.Color.Transparent
        Me.CreateJobListButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CreateJobListButton.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.CreateJobListButton.ForeColor = System.Drawing.Color.White
        Me.CreateJobListButton.Image = Global.HackSystem.My.Resources.UnityResource.CreateJobList
        Me.CreateJobListButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CreateJobListButton.Location = New System.Drawing.Point(30, 41)
        Me.CreateJobListButton.Name = "CreateJobListButton"
        Me.CreateJobListButton.Size = New System.Drawing.Size(100, 37)
        Me.CreateJobListButton.TabIndex = 2
        Me.CreateJobListButton.Tag = ""
        Me.CreateJobListButton.Text = "重置    "
        Me.CreateJobListButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LogoLabel
        '
        Me.LogoLabel.AutoSize = True
        Me.LogoLabel.BackColor = System.Drawing.Color.Transparent
        Me.LogoLabel.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LogoLabel.ForeColor = System.Drawing.Color.Yellow
        Me.LogoLabel.Location = New System.Drawing.Point(5, 5)
        Me.LogoLabel.Name = "LogoLabel"
        Me.LogoLabel.Size = New System.Drawing.Size(186, 21)
        Me.LogoLabel.TabIndex = 5
        Me.LogoLabel.Text = "操作系统调度算法可视化"
        '
        'TimeLinePanel
        '
        Me.TimeLinePanel.BackColor = System.Drawing.Color.Transparent
        Me.TimeLinePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TimeLinePanel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TimeLinePanel.ForeColor = System.Drawing.Color.Transparent
        Me.TimeLinePanel.Location = New System.Drawing.Point(29, 83)
        Me.TimeLinePanel.Name = "TimeLinePanel"
        Me.TimeLinePanel.Size = New System.Drawing.Size(474, 129)
        Me.TimeLinePanel.TabIndex = 6
        Me.TimeLinePanel.Text = "时间线："
        Me.TimeLinePanel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'DispatchPanel
        '
        Me.DispatchPanel.BackColor = System.Drawing.Color.Transparent
        Me.DispatchPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DispatchPanel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.DispatchPanel.ForeColor = System.Drawing.Color.Transparent
        Me.DispatchPanel.Location = New System.Drawing.Point(30, 222)
        Me.DispatchPanel.Name = "DispatchPanel"
        Me.DispatchPanel.Size = New System.Drawing.Size(474, 100)
        Me.DispatchPanel.TabIndex = 7
        Me.DispatchPanel.Text = "调度区："
        Me.DispatchPanel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'PlayPauseButton
        '
        Me.PlayPauseButton.BackColor = System.Drawing.Color.Transparent
        Me.PlayPauseButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PlayPauseButton.Enabled = False
        Me.PlayPauseButton.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.PlayPauseButton.ForeColor = System.Drawing.Color.White
        Me.PlayPauseButton.Image = Global.HackSystem.My.Resources.UnityResource.Play
        Me.PlayPauseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.PlayPauseButton.Location = New System.Drawing.Point(145, 41)
        Me.PlayPauseButton.Name = "PlayPauseButton"
        Me.PlayPauseButton.Size = New System.Drawing.Size(100, 37)
        Me.PlayPauseButton.TabIndex = 8
        Me.PlayPauseButton.Tag = ""
        Me.PlayPauseButton.Text = "播放   "
        Me.PlayPauseButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'SystemClockTitle
        '
        Me.SystemClockTitle.AutoSize = True
        Me.SystemClockTitle.BackColor = System.Drawing.Color.Transparent
        Me.SystemClockTitle.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.SystemClockTitle.ForeColor = System.Drawing.Color.Aqua
        Me.SystemClockTitle.Location = New System.Drawing.Point(653, 57)
        Me.SystemClockTitle.Name = "SystemClockTitle"
        Me.SystemClockTitle.Size = New System.Drawing.Size(90, 21)
        Me.SystemClockTitle.TabIndex = 9
        Me.SystemClockTitle.Text = "系统时间："
        '
        'SystemClockLabel
        '
        Me.SystemClockLabel.BackColor = System.Drawing.Color.Transparent
        Me.SystemClockLabel.Font = New System.Drawing.Font("微软雅黑", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.SystemClockLabel.ForeColor = System.Drawing.Color.Aqua
        Me.SystemClockLabel.Location = New System.Drawing.Point(714, 40)
        Me.SystemClockLabel.Name = "SystemClockLabel"
        Me.SystemClockLabel.Size = New System.Drawing.Size(75, 38)
        Me.SystemClockLabel.TabIndex = 10
        Me.SystemClockLabel.Text = "0"
        Me.SystemClockLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'SystemClockTimer
        '
        Me.SystemClockTimer.Interval = 500
        '
        'SpeedDownButton
        '
        Me.SpeedDownButton.BackColor = System.Drawing.Color.Transparent
        Me.SpeedDownButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SpeedDownButton.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.SpeedDownButton.ForeColor = System.Drawing.Color.White
        Me.SpeedDownButton.Image = Global.HackSystem.My.Resources.UnityResource.SpeedDown
        Me.SpeedDownButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SpeedDownButton.Location = New System.Drawing.Point(260, 41)
        Me.SpeedDownButton.Name = "SpeedDownButton"
        Me.SpeedDownButton.Size = New System.Drawing.Size(100, 37)
        Me.SpeedDownButton.TabIndex = 11
        Me.SpeedDownButton.Tag = ""
        Me.SpeedDownButton.Text = "减速   "
        Me.SpeedDownButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'SpeedUpButton
        '
        Me.SpeedUpButton.BackColor = System.Drawing.Color.Transparent
        Me.SpeedUpButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SpeedUpButton.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.SpeedUpButton.ForeColor = System.Drawing.Color.White
        Me.SpeedUpButton.Image = Global.HackSystem.My.Resources.UnityResource.SpeedUp
        Me.SpeedUpButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SpeedUpButton.Location = New System.Drawing.Point(375, 41)
        Me.SpeedUpButton.Name = "SpeedUpButton"
        Me.SpeedUpButton.Size = New System.Drawing.Size(100, 37)
        Me.SpeedUpButton.TabIndex = 12
        Me.SpeedUpButton.Tag = ""
        Me.SpeedUpButton.Text = "加速   "
        Me.SpeedUpButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TimeLineLabel
        '
        Me.TimeLineLabel.BackColor = System.Drawing.Color.Transparent
        Me.TimeLineLabel.Image = Global.HackSystem.My.Resources.UnityResource.TimeLine
        Me.TimeLineLabel.Location = New System.Drawing.Point(194, 84)
        Me.TimeLineLabel.Name = "TimeLineLabel"
        Me.TimeLineLabel.Size = New System.Drawing.Size(29, 127)
        Me.TimeLineLabel.TabIndex = 14
        Me.TimeLineLabel.Visible = False
        '
        'WaitLabel
        '
        Me.WaitLabel.AutoSize = True
        Me.WaitLabel.BackColor = System.Drawing.Color.Transparent
        Me.WaitLabel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.WaitLabel.ForeColor = System.Drawing.Color.White
        Me.WaitLabel.Location = New System.Drawing.Point(359, 249)
        Me.WaitLabel.Name = "WaitLabel"
        Me.WaitLabel.Size = New System.Drawing.Size(107, 20)
        Me.WaitLabel.TabIndex = 15
        Me.WaitLabel.Text = "正在等待队列："
        Me.WaitLabel.Visible = False
        '
        'ExecuteLabel
        '
        Me.ExecuteLabel.AutoSize = True
        Me.ExecuteLabel.BackColor = System.Drawing.Color.Transparent
        Me.ExecuteLabel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ExecuteLabel.ForeColor = System.Drawing.Color.White
        Me.ExecuteLabel.Location = New System.Drawing.Point(387, 223)
        Me.ExecuteLabel.Name = "ExecuteLabel"
        Me.ExecuteLabel.Size = New System.Drawing.Size(79, 20)
        Me.ExecuteLabel.TabIndex = 16
        Me.ExecuteLabel.Text = "正在执行："
        Me.ExecuteLabel.Visible = False
        '
        'NextJobTipLabel
        '
        Me.NextJobTipLabel.AutoSize = True
        Me.NextJobTipLabel.BackColor = System.Drawing.Color.Transparent
        Me.NextJobTipLabel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.NextJobTipLabel.ForeColor = System.Drawing.Color.Red
        Me.NextJobTipLabel.Location = New System.Drawing.Point(347, 275)
        Me.NextJobTipLabel.Name = "NextJobTipLabel"
        Me.NextJobTipLabel.Size = New System.Drawing.Size(123, 20)
        Me.NextJobTipLabel.TabIndex = 18
        Me.NextJobTipLabel.Text = "下次应执行作业 ▶"
        Me.NextJobTipLabel.Visible = False
        '
        'DispatchComboBox
        '
        Me.DispatchComboBox.BackColor = System.Drawing.Color.Black
        Me.DispatchComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DispatchComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DispatchComboBox.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.DispatchComboBox.ForeColor = System.Drawing.Color.White
        Me.DispatchComboBox.FormattingEnabled = True
        Me.DispatchComboBox.Items.AddRange(New Object() {"先来先服务-FCFS", "短作业优先-SJF", "最高响应比优先-HRN", "优先数调度-HPF", "时间片轮转-RR", "多级反馈队列-MFQ"})
        Me.DispatchComboBox.Location = New System.Drawing.Point(481, 46)
        Me.DispatchComboBox.Name = "DispatchComboBox"
        Me.DispatchComboBox.Size = New System.Drawing.Size(160, 28)
        Me.DispatchComboBox.TabIndex = 19
        '
        'SettingButton
        '
        Me.SettingButton.BackColor = System.Drawing.Color.Transparent
        Me.SettingButton.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.SettingButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.SettingButton.Location = New System.Drawing.Point(695, 0)
        Me.SettingButton.Name = "SettingButton"
        Me.SettingButton.Size = New System.Drawing.Size(32, 21)
        Me.SettingButton.TabIndex = 20
        Me.SettingButton.Tag = "Setting"
        '
        'RecordPanel
        '
        Me.RecordPanel.BackColor = System.Drawing.Color.Transparent
        Me.RecordPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RecordPanel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.RecordPanel.ForeColor = System.Drawing.Color.Transparent
        Me.RecordPanel.Location = New System.Drawing.Point(30, 331)
        Me.RecordPanel.Name = "RecordPanel"
        Me.RecordPanel.Size = New System.Drawing.Size(474, 55)
        Me.RecordPanel.TabIndex = 21
        Me.RecordPanel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'ReplayCheckBox
        '
        Me.ReplayCheckBox.AutoSize = True
        Me.ReplayCheckBox.BackColor = System.Drawing.Color.Transparent
        Me.ReplayCheckBox.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ReplayCheckBox.ForeColor = System.Drawing.Color.White
        Me.ReplayCheckBox.Location = New System.Drawing.Point(375, 0)
        Me.ReplayCheckBox.Name = "ReplayCheckBox"
        Me.ReplayCheckBox.Size = New System.Drawing.Size(189, 25)
        Me.ReplayCheckBox.TabIndex = 22
        Me.ReplayCheckBox.Text = "完成后自动重置并播放"
        Me.ReplayCheckBox.UseVisualStyleBackColor = False
        '
        'TimeSliceLabel
        '
        Me.TimeSliceLabel.AutoSize = True
        Me.TimeSliceLabel.BackColor = System.Drawing.Color.Transparent
        Me.TimeSliceLabel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TimeSliceLabel.ForeColor = System.Drawing.Color.White
        Me.TimeSliceLabel.Location = New System.Drawing.Point(481, 50)
        Me.TimeSliceLabel.Name = "TimeSliceLabel"
        Me.TimeSliceLabel.Size = New System.Drawing.Size(65, 20)
        Me.TimeSliceLabel.TabIndex = 24
        Me.TimeSliceLabel.Text = "时间片："
        Me.TimeSliceLabel.Visible = False
        '
        'TimeSliceNumeric
        '
        Me.TimeSliceNumeric.BackColor = System.Drawing.Color.Black
        Me.TimeSliceNumeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TimeSliceNumeric.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TimeSliceNumeric.ForeColor = System.Drawing.Color.White
        Me.TimeSliceNumeric.Location = New System.Drawing.Point(546, 47)
        Me.TimeSliceNumeric.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.TimeSliceNumeric.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.TimeSliceNumeric.Name = "TimeSliceNumeric"
        Me.TimeSliceNumeric.Size = New System.Drawing.Size(95, 26)
        Me.TimeSliceNumeric.TabIndex = 26
        Me.TimeSliceNumeric.Value = New Decimal(New Integer() {5, 0, 0, 0})
        Me.TimeSliceNumeric.Visible = False
        '
        'LogLabel
        '
        Me.LogLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LogLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LogLabel.Location = New System.Drawing.Point(510, 84)
        Me.LogLabel.Name = "LogLabel"
        Me.LogLabel.Size = New System.Drawing.Size(277, 303)
        Me.LogLabel.TabIndex = 23
        '
        'VPMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.HackSystem.My.Resources.UnityResource.BGI
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(799, 399)
        Me.Controls.Add(Me.TimeSliceNumeric)
        Me.Controls.Add(Me.TimeSliceLabel)
        Me.Controls.Add(Me.LogLabel)
        Me.Controls.Add(Me.ReplayCheckBox)
        Me.Controls.Add(Me.RecordPanel)
        Me.Controls.Add(Me.SettingButton)
        Me.Controls.Add(Me.DispatchComboBox)
        Me.Controls.Add(Me.NextJobTipLabel)
        Me.Controls.Add(Me.ExecuteLabel)
        Me.Controls.Add(Me.WaitLabel)
        Me.Controls.Add(Me.TimeLineLabel)
        Me.Controls.Add(Me.SpeedUpButton)
        Me.Controls.Add(Me.SpeedDownButton)
        Me.Controls.Add(Me.SystemClockTitle)
        Me.Controls.Add(Me.PlayPauseButton)
        Me.Controls.Add(Me.DispatchPanel)
        Me.Controls.Add(Me.TimeLinePanel)
        Me.Controls.Add(Me.LogoLabel)
        Me.Controls.Add(Me.CreateJobListButton)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.SystemClockLabel)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "VPMainForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "操作系统调度算法可视化"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.TimeSliceNumeric, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CloseButton As Label
    Friend WithEvents CreateJobListButton As Label
    Friend WithEvents LogoLabel As Label
    Friend WithEvents TimeLinePanel As Label
    Friend WithEvents DispatchPanel As Label
    Friend WithEvents PlayPauseButton As Label
    Friend WithEvents SystemClockTitle As Label
    Friend WithEvents SystemClockLabel As Label
    Friend WithEvents SystemClockTimer As Timer
    Friend WithEvents SpeedDownButton As Label
    Friend WithEvents SpeedUpButton As Label
    Friend WithEvents TimeLineLabel As Label
    Friend WithEvents WaitLabel As Label
    Friend WithEvents ExecuteLabel As Label
    Friend WithEvents NextJobTipLabel As Label
    Friend WithEvents DispatchComboBox As ComboBox
    Friend WithEvents SettingButton As Label
    Friend WithEvents RecordPanel As Label
    Friend WithEvents ReplayCheckBox As CheckBox
    Friend WithEvents LogLabel As LeonTextBox
    Friend WithEvents TimeSliceLabel As Label
    Friend WithEvents TimeSliceNumeric As NumericUpDown
End Class
