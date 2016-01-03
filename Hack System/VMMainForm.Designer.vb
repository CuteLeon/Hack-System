<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class VMMainForm
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
        Me.CloseButton = New System.Windows.Forms.Label()
        Me.ResetSystemButton = New System.Windows.Forms.Label()
        Me.LogoLabel = New System.Windows.Forms.Label()
        Me.ControlPanel = New System.Windows.Forms.Label()
        Me.MemoryManagerPanel = New System.Windows.Forms.Label()
        Me.MemoryBackUpPanel = New System.Windows.Forms.Label()
        Me.FreeMemorySortByAddressPanel = New System.Windows.Forms.Label()
        Me.FreeMemorySortBySizePanel = New System.Windows.Forms.Label()
        Me.AddProcessButton = New System.Windows.Forms.Label()
        Me.DisposeProcessButton = New System.Windows.Forms.Label()
        Me.ProcessMemorySizeNumeric = New System.Windows.Forms.NumericUpDown()
        Me.ProcessListComboBox = New System.Windows.Forms.ComboBox()
        Me.RelocateButton = New System.Windows.Forms.Label()
        Me.AddProcessLabel = New System.Windows.Forms.Label()
        Me.DisposeProcessLabel = New System.Windows.Forms.Label()
        Me.SegmentPageButton = New System.Windows.Forms.Label()
        Me.SegmentPageLabel = New System.Windows.Forms.Label()
        Me.DispathComboBox = New System.Windows.Forms.ComboBox()
        Me.LogLabel = New HackSystem.LeonTextBox()
        CType(Me.ProcessMemorySizeNumeric, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'ResetSystemButton
        '
        Me.ResetSystemButton.BackColor = System.Drawing.Color.Transparent
        Me.ResetSystemButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ResetSystemButton.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ResetSystemButton.ForeColor = System.Drawing.Color.White
        Me.ResetSystemButton.Image = Global.HackSystem.My.Resources.UnityResource.CreateJobList
        Me.ResetSystemButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ResetSystemButton.Location = New System.Drawing.Point(15, 37)
        Me.ResetSystemButton.Name = "ResetSystemButton"
        Me.ResetSystemButton.Size = New System.Drawing.Size(157, 37)
        Me.ResetSystemButton.TabIndex = 2
        Me.ResetSystemButton.Tag = ""
        Me.ResetSystemButton.Text = "     重置系统"
        Me.ResetSystemButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LogoLabel
        '
        Me.LogoLabel.AutoSize = True
        Me.LogoLabel.BackColor = System.Drawing.Color.Transparent
        Me.LogoLabel.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LogoLabel.ForeColor = System.Drawing.Color.Yellow
        Me.LogoLabel.Location = New System.Drawing.Point(15, 5)
        Me.LogoLabel.Name = "LogoLabel"
        Me.LogoLabel.Size = New System.Drawing.Size(154, 21)
        Me.LogoLabel.TabIndex = 5
        Me.LogoLabel.Text = "存储管理系统可视化"
        '
        'ControlPanel
        '
        Me.ControlPanel.BackColor = System.Drawing.Color.Transparent
        Me.ControlPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ControlPanel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ControlPanel.ForeColor = System.Drawing.Color.Transparent
        Me.ControlPanel.Location = New System.Drawing.Point(12, 35)
        Me.ControlPanel.Name = "ControlPanel"
        Me.ControlPanel.Size = New System.Drawing.Size(194, 279)
        Me.ControlPanel.TabIndex = 6
        Me.ControlPanel.Text = "控制区："
        Me.ControlPanel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'MemoryManagerPanel
        '
        Me.MemoryManagerPanel.BackColor = System.Drawing.Color.Transparent
        Me.MemoryManagerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MemoryManagerPanel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.MemoryManagerPanel.ForeColor = System.Drawing.Color.Transparent
        Me.MemoryManagerPanel.Location = New System.Drawing.Point(406, 35)
        Me.MemoryManagerPanel.Name = "MemoryManagerPanel"
        Me.MemoryManagerPanel.Size = New System.Drawing.Size(188, 279)
        Me.MemoryManagerPanel.TabIndex = 7
        Me.MemoryManagerPanel.Text = "图形化存储管理区："
        Me.MemoryManagerPanel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'MemoryBackUpPanel
        '
        Me.MemoryBackUpPanel.BackColor = System.Drawing.Color.Transparent
        Me.MemoryBackUpPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MemoryBackUpPanel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.MemoryBackUpPanel.ForeColor = System.Drawing.Color.Transparent
        Me.MemoryBackUpPanel.Location = New System.Drawing.Point(212, 35)
        Me.MemoryBackUpPanel.Name = "MemoryBackUpPanel"
        Me.MemoryBackUpPanel.Size = New System.Drawing.Size(188, 279)
        Me.MemoryBackUpPanel.TabIndex = 21
        Me.MemoryBackUpPanel.Text = "备份对比区："
        Me.MemoryBackUpPanel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'FreeMemorySortByAddressPanel
        '
        Me.FreeMemorySortByAddressPanel.BackColor = System.Drawing.Color.Transparent
        Me.FreeMemorySortByAddressPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FreeMemorySortByAddressPanel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.FreeMemorySortByAddressPanel.ForeColor = System.Drawing.Color.Transparent
        Me.FreeMemorySortByAddressPanel.Location = New System.Drawing.Point(12, 324)
        Me.FreeMemorySortByAddressPanel.Name = "FreeMemorySortByAddressPanel"
        Me.FreeMemorySortByAddressPanel.Size = New System.Drawing.Size(388, 90)
        Me.FreeMemorySortByAddressPanel.TabIndex = 23
        Me.FreeMemorySortByAddressPanel.Text = "空闲区域链表（按首地址排序）"
        Me.FreeMemorySortByAddressPanel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'FreeMemorySortBySizePanel
        '
        Me.FreeMemorySortBySizePanel.BackColor = System.Drawing.Color.Transparent
        Me.FreeMemorySortBySizePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FreeMemorySortBySizePanel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.FreeMemorySortBySizePanel.ForeColor = System.Drawing.Color.Transparent
        Me.FreeMemorySortBySizePanel.Location = New System.Drawing.Point(406, 324)
        Me.FreeMemorySortBySizePanel.Name = "FreeMemorySortBySizePanel"
        Me.FreeMemorySortBySizePanel.Size = New System.Drawing.Size(381, 90)
        Me.FreeMemorySortBySizePanel.TabIndex = 24
        Me.FreeMemorySortBySizePanel.Text = "空闲区域链表（按控件大小排序）"
        Me.FreeMemorySortBySizePanel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'AddProcessButton
        '
        Me.AddProcessButton.BackColor = System.Drawing.Color.Transparent
        Me.AddProcessButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AddProcessButton.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.AddProcessButton.ForeColor = System.Drawing.Color.White
        Me.AddProcessButton.Image = Global.HackSystem.My.Resources.UnityResource.AddProcess
        Me.AddProcessButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.AddProcessButton.Location = New System.Drawing.Point(15, 146)
        Me.AddProcessButton.Name = "AddProcessButton"
        Me.AddProcessButton.Size = New System.Drawing.Size(157, 37)
        Me.AddProcessButton.TabIndex = 26
        Me.AddProcessButton.Tag = ""
        Me.AddProcessButton.Text = "     载入进程"
        Me.AddProcessButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DisposeProcessButton
        '
        Me.DisposeProcessButton.BackColor = System.Drawing.Color.Transparent
        Me.DisposeProcessButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DisposeProcessButton.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.DisposeProcessButton.ForeColor = System.Drawing.Color.White
        Me.DisposeProcessButton.Image = Global.HackSystem.My.Resources.UnityResource.DisposeProcess
        Me.DisposeProcessButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DisposeProcessButton.Location = New System.Drawing.Point(15, 256)
        Me.DisposeProcessButton.Name = "DisposeProcessButton"
        Me.DisposeProcessButton.Size = New System.Drawing.Size(157, 37)
        Me.DisposeProcessButton.TabIndex = 27
        Me.DisposeProcessButton.Tag = ""
        Me.DisposeProcessButton.Text = "     释放进程"
        Me.DisposeProcessButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ProcessMemorySizeNumeric
        '
        Me.ProcessMemorySizeNumeric.BackColor = System.Drawing.Color.White
        Me.ProcessMemorySizeNumeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ProcessMemorySizeNumeric.Font = New System.Drawing.Font("微软雅黑", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ProcessMemorySizeNumeric.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.ProcessMemorySizeNumeric.Location = New System.Drawing.Point(15, 115)
        Me.ProcessMemorySizeNumeric.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        Me.ProcessMemorySizeNumeric.Minimum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.ProcessMemorySizeNumeric.Name = "ProcessMemorySizeNumeric"
        Me.ProcessMemorySizeNumeric.ReadOnly = True
        Me.ProcessMemorySizeNumeric.Size = New System.Drawing.Size(157, 27)
        Me.ProcessMemorySizeNumeric.TabIndex = 28
        Me.ProcessMemorySizeNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ProcessMemorySizeNumeric.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'ProcessListComboBox
        '
        Me.ProcessListComboBox.BackColor = System.Drawing.Color.White
        Me.ProcessListComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ProcessListComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ProcessListComboBox.Font = New System.Drawing.Font("微软雅黑", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ProcessListComboBox.FormattingEnabled = True
        Me.ProcessListComboBox.Items.AddRange(New Object() {"PID-0: 10KB", "PID-1: 20KB"})
        Me.ProcessListComboBox.Location = New System.Drawing.Point(15, 224)
        Me.ProcessListComboBox.Name = "ProcessListComboBox"
        Me.ProcessListComboBox.Size = New System.Drawing.Size(157, 28)
        Me.ProcessListComboBox.TabIndex = 29
        '
        'RelocateButton
        '
        Me.RelocateButton.BackColor = System.Drawing.Color.Transparent
        Me.RelocateButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RelocateButton.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.RelocateButton.ForeColor = System.Drawing.Color.White
        Me.RelocateButton.Image = Global.HackSystem.My.Resources.UnityResource.Relocate
        Me.RelocateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RelocateButton.Location = New System.Drawing.Point(15, 414)
        Me.RelocateButton.Name = "RelocateButton"
        Me.RelocateButton.Size = New System.Drawing.Size(157, 37)
        Me.RelocateButton.TabIndex = 30
        Me.RelocateButton.Tag = ""
        Me.RelocateButton.Text = "      重定位"
        Me.RelocateButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'AddProcessLabel
        '
        Me.AddProcessLabel.AutoSize = True
        Me.AddProcessLabel.BackColor = System.Drawing.Color.Transparent
        Me.AddProcessLabel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.AddProcessLabel.ForeColor = System.Drawing.Color.Goldenrod
        Me.AddProcessLabel.Location = New System.Drawing.Point(5, 89)
        Me.AddProcessLabel.Name = "AddProcessLabel"
        Me.AddProcessLabel.Size = New System.Drawing.Size(149, 20)
        Me.AddProcessLabel.TabIndex = 31
        Me.AddProcessLabel.Text = "设置新进程内存大小："
        '
        'DisposeProcessLabel
        '
        Me.DisposeProcessLabel.AutoSize = True
        Me.DisposeProcessLabel.BackColor = System.Drawing.Color.Transparent
        Me.DisposeProcessLabel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.DisposeProcessLabel.ForeColor = System.Drawing.Color.Goldenrod
        Me.DisposeProcessLabel.Location = New System.Drawing.Point(5, 198)
        Me.DisposeProcessLabel.Name = "DisposeProcessLabel"
        Me.DisposeProcessLabel.Size = New System.Drawing.Size(135, 20)
        Me.DisposeProcessLabel.TabIndex = 32
        Me.DisposeProcessLabel.Text = "选择要释放的进程："
        '
        'SegmentPageButton
        '
        Me.SegmentPageButton.BackColor = System.Drawing.Color.Transparent
        Me.SegmentPageButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SegmentPageButton.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.SegmentPageButton.ForeColor = System.Drawing.Color.White
        Me.SegmentPageButton.Image = Global.HackSystem.My.Resources.UnityResource.SegmentPage
        Me.SegmentPageButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SegmentPageButton.Location = New System.Drawing.Point(15, 333)
        Me.SegmentPageButton.Name = "SegmentPageButton"
        Me.SegmentPageButton.Size = New System.Drawing.Size(157, 37)
        Me.SegmentPageButton.TabIndex = 33
        Me.SegmentPageButton.Tag = ""
        Me.SegmentPageButton.Text = "      禁止分页"
        Me.SegmentPageButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SegmentPageLabel
        '
        Me.SegmentPageLabel.AutoSize = True
        Me.SegmentPageLabel.BackColor = System.Drawing.Color.Transparent
        Me.SegmentPageLabel.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.SegmentPageLabel.ForeColor = System.Drawing.Color.Goldenrod
        Me.SegmentPageLabel.Location = New System.Drawing.Point(5, 308)
        Me.SegmentPageLabel.Name = "SegmentPageLabel"
        Me.SegmentPageLabel.Size = New System.Drawing.Size(107, 20)
        Me.SegmentPageLabel.TabIndex = 36
        Me.SegmentPageLabel.Text = "设置分页大小："
        '
        'DispathComboBox
        '
        Me.DispathComboBox.BackColor = System.Drawing.Color.White
        Me.DispathComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DispathComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DispathComboBox.Font = New System.Drawing.Font("微软雅黑", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.DispathComboBox.FormattingEnabled = True
        Me.DispathComboBox.Items.AddRange(New Object() {"首次适应算法", "循环首次适应算法", "最佳适应算法", "最坏适应算法"})
        Me.DispathComboBox.Location = New System.Drawing.Point(15, 378)
        Me.DispathComboBox.Name = "DispathComboBox"
        Me.DispathComboBox.Size = New System.Drawing.Size(157, 28)
        Me.DispathComboBox.TabIndex = 37
        '
        'LogLabel
        '
        Me.LogLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LogLabel.Location = New System.Drawing.Point(600, 35)
        Me.LogLabel.Name = "LogLabel"
        Me.LogLabel.Size = New System.Drawing.Size(187, 279)
        Me.LogLabel.TabIndex = 38
        '
        'VMMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.HackSystem.My.Resources.UnityResource.BGI
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(799, 423)
        Me.Controls.Add(Me.LogLabel)
        Me.Controls.Add(Me.SegmentPageLabel)
        Me.Controls.Add(Me.AddProcessLabel)
        Me.Controls.Add(Me.DisposeProcessLabel)
        Me.Controls.Add(Me.RelocateButton)
        Me.Controls.Add(Me.ProcessListComboBox)
        Me.Controls.Add(Me.ProcessMemorySizeNumeric)
        Me.Controls.Add(Me.DisposeProcessButton)
        Me.Controls.Add(Me.AddProcessButton)
        Me.Controls.Add(Me.ResetSystemButton)
        Me.Controls.Add(Me.FreeMemorySortBySizePanel)
        Me.Controls.Add(Me.MemoryBackUpPanel)
        Me.Controls.Add(Me.MemoryManagerPanel)
        Me.Controls.Add(Me.LogoLabel)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.ControlPanel)
        Me.Controls.Add(Me.DispathComboBox)
        Me.Controls.Add(Me.SegmentPageButton)
        Me.Controls.Add(Me.FreeMemorySortByAddressPanel)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "VMMainForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "操作系统调度算法可视化"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.ProcessMemorySizeNumeric, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CloseButton As Label
    Friend WithEvents ResetSystemButton As Label
    Friend WithEvents LogoLabel As Label
    Friend WithEvents ControlPanel As Label
    Friend WithEvents MemoryManagerPanel As Label
    Friend WithEvents MemoryBackUpPanel As Label
    Friend WithEvents LogLabel As LeonTextBox
    Friend WithEvents FreeMemorySortByAddressPanel As Label
    Friend WithEvents FreeMemorySortBySizePanel As Label
    Friend WithEvents AddProcessButton As Label
    Friend WithEvents DisposeProcessButton As Label
    Friend WithEvents ProcessMemorySizeNumeric As NumericUpDown
    Friend WithEvents ProcessListComboBox As ComboBox
    Friend WithEvents RelocateButton As Label
    Friend WithEvents AddProcessLabel As Label
    Friend WithEvents DisposeProcessLabel As Label
    Friend WithEvents SegmentPageButton As Label
    Friend WithEvents SegmentPageLabel As Label
    Friend WithEvents DispathComboBox As ComboBox
End Class
