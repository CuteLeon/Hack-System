Imports System.ComponentModel

Public Class VPMainForm

#Region "变量和常量"

    ''' <summary>
    ''' 最大作业数量（必须与 JobColors 个数匹配）
    ''' </summary>
    Private Const Max_JobCount As Integer = 12
    ''' <summary>
    ''' 默认按钮透明度
    ''' </summary>
    Private Const ButtonAlphaDefault As Integer = 50
    ''' <summary>
    ''' 作业允许达到和预计完成的最大时间范围
    ''' </summary>
    Private Const Max_SystemTime As Integer = 60
    ''' <summary>
    ''' 鼠标进入按钮时透明度
    ''' </summary>
    Private Const ButtonAlphaActive As Integer = 100
    ''' <summary>
    ''' 鼠标按压按钮时透明度
    ''' </summary>
    Private Const ButtonAlphaExecute As Integer = 150
    ''' <summary>
    ''' 图形化显示作业阴影距离
    ''' </summary>
    Dim ShadowDistance As Integer = 5
    ''' <summary>
    ''' 按钮默认颜色
    ''' </summary>
    Dim ButtonColorDefault As Color = Color.Orange
    ''' <summary>
    ''' 鼠标进入按钮时颜色
    ''' </summary>
    Dim ButtonColorActive As Color = Color.Aqua
    ''' <summary>
    ''' 鼠标按压按钮时颜色
    ''' </summary>
    Dim ButtonColorExecute As Color = Color.Yellow
    ''' <summary>
    ''' 纯色按钮组
    ''' </summary>
    Dim Buttons() As Label
    ''' <summary>
    ''' 伊登十二色环配色
    ''' </summary>
    Dim JobColors() As Color = {
        Color.FromArgb(227, 35, 34),
        Color.FromArgb(244, 229, 0),
        Color.FromArgb(38, 113, 178),
        Color.FromArgb(0, 142, 91),
        Color.FromArgb(241, 145, 1),
        Color.FromArgb(109, 56, 137),
        Color.FromArgb(253, 198, 11),
        Color.FromArgb(234, 98, 31),
        Color.FromArgb(196, 3, 125),
        Color.FromArgb(68, 78, 153),
        Color.FromArgb(6, 15, 187),
        Color.FromArgb(140, 187, 38)}
    ''' <summary>
    ''' 初始化时储存所有的作业（作业到达后将被从该列表移除）
    ''' </summary>
    Dim AllJobList As New List(Of JobClass)
    ''' <summary>
    ''' 正在等待队列的作业列表（MFQ算法不使用）
    ''' </summary>
    Dim WaitJobList As New List(Of JobClass)
    ''' <summary>
    ''' 正在多级队列的作业列表（用于MFQ算法）
    ''' </summary>
    Dim PriorityList() As List(Of JobClass)
    ''' <summary>
    ''' 已经执行的时间片（与 TimeSliceNumeric.Value 比较）
    ''' </summary>
    Dim ExecuteTimeSlice As Integer = 0
    ''' <summary>
    ''' 多级队列调度时队列数组的下标
    ''' </summary>
    Dim PriorityListSubscript As Integer
    ''' <summary>
    ''' 正在执行的作业（RR、MFQ 调度不使用）
    ''' </summary>
    Dim ExecuteJob As JobClass
    ''' <summary>
    ''' 系统全局时间
    ''' </summary>
    Dim SystemClock As Integer = 0
    ''' <summary>
    ''' 下个执行的作业下标
    ''' </summary>
    Dim NextJobSubscript As Integer = 0
    ''' <summary>
    ''' 当前执行的作业实际开始执行的时间
    ''' </summary>
    Dim ExecuteTime As Integer
    ''' <summary>
    ''' 随机数发生器
    ''' </summary>
    Dim SystemRandom As Random = New Random
    ''' <summary>
    ''' 时间区-坐标系区域
    ''' </summary>
    Dim CoordinateRectangle As Rectangle
    ''' <summary>
    ''' 调度区-调度区域
    ''' </summary>
    Dim DispatchRectangle As Rectangle
    ''' <summary>
    ''' 调度区-多级队列调度子队列区域
    ''' </summary>
    Dim DispatchPriorityListRectangle As Rectangle
    ''' <summary>
    ''' 调度区域-执行区域
    ''' </summary>
    Dim ExecuteRectangle As Rectangle
    ''' <summary>
    ''' 调度区域-等待区域
    ''' </summary>
    Dim WaitRectangle As Rectangle
    ''' <summary>
    ''' 时间线区域单位时间对应的图像宽度
    ''' </summary>
    Dim TimeCellWidth As Double
    ''' <summary>
    ''' 时间线区域单位时间对应的图像高度
    ''' </summary>
    Dim TimeCellHeight As Double
    ''' <summary>
    ''' 调度区域单位时间对应的图像宽度
    ''' </summary>
    Dim DispatchCellWidth As Double
    ''' <summary>
    ''' 调度区域单位时间对应的图像高度
    ''' </summary>
    Dim DispatchCellHeight As Double
    ''' <summary>
    ''' 多级队列调度每个队列图像区域单位时间对应的图像宽度
    ''' </summary>
    Dim PriorityListCellWidth As Double
    ''' <summary>
    ''' 日志区域单位时间对应的图像宽度
    ''' </summary>
    Dim RecordCellWidth As Double
    ''' <summary>
    ''' 日志区域单位时间对应的图像高度
    ''' </summary>
    Dim RecordCellHeight As Double
    ''' <summary>
    ''' 响应比数组
    ''' </summary>
    Dim ResponseRatios As New List(Of Double)
    ''' <summary>
    ''' 执行记录结构体
    ''' </summary>
    Private Structure ExecuteLog
        ''' <summary>
        ''' 执行的作业ID
        ''' </summary>
        Dim ID As Integer
        ''' <summary>
        ''' 执行的作业名称
        ''' </summary>
        Dim Name As String
        ''' <summary>
        ''' 作业的执行时间
        ''' </summary>
        Dim ExecuteTime As Integer
        ''' <summary>
        ''' 作业的完成时间
        ''' </summary>
        Dim CompleteTime As Integer
        ''' <summary>
        ''' 作业的标记颜色
        ''' </summary>
        Dim Color As Color

        ''' <summary>
        ''' 构建函数
        ''' </summary>
        ''' <param name="jID">作业ID</param>
        ''' <param name="jName">作业名称</param>
        ''' <param name="eTime">执行时间</param>
        ''' <param name="tLength">执行时间长度</param>
        ''' <param name="jColor">作业标记颜色</param>
        Public Sub New(jID As Integer, jName As String, eTime As Integer, tLength As Integer, jColor As Color)
            ID = jID
            Name = jName
            ExecuteTime = eTime
            CompleteTime = eTime + tLength
            Color = jColor
        End Sub
    End Structure
    ''' <summary>
    ''' 执行记录日志列表
    ''' </summary>
    Dim ExecuteLogs As New List(Of ExecuteLog)
#End Region

#Region "窗体事件"

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '初始化纯色按钮数组
        Buttons = New Label() {CreateJobListButton, PlayPauseButton, SpeedDownButton, SpeedUpButton}

        '设置标志，减少闪烁
        SetStyle(ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)

        '初始化窗口图标和标题栏按钮控件
        Me.Icon = My.Resources.UnityResource.VPSystemICON
        CloseButton.Left = Me.Width - CloseButton.Width
        ReplayCheckBox.Left = SettingButton.Left - ReplayCheckBox.Width - 10

        '初始化纯色按钮
        Dim LastRightPoint As Integer = 15
        For Each InsButton As Label In Buttons
            InsButton.BackColor = Color.FromArgb(ButtonAlphaDefault, ButtonColorDefault)
            InsButton.Left = LastRightPoint
            LastRightPoint += InsButton.Width + 15
            AddHandler InsButton.MouseEnter, AddressOf ColorButton_MouseEnter
            AddHandler InsButton.MouseDown, AddressOf ColorButton_MouseDown
            AddHandler InsButton.MouseUp, AddressOf ColorButton_MouseUp
            AddHandler InsButton.MouseLeave, AddressOf ColorButton_MouseLeave
        Next
        '初始化调度控件
        DispatchComboBox.Left = LastRightPoint
        TimeSliceLabel.Left = DispatchComboBox.Right + 10
        TimeSliceNumeric.Left = TimeSliceLabel.Right
        TimeSliceNumeric.Maximum = Max_SystemTime

        '设置容器控件的背景颜色
        TimeLinePanel.BackColor = Color.FromArgb(50, Color.White)
        DispatchPanel.BackColor = Color.FromArgb(50, Color.Gray)
        RecordPanel.BackColor = Color.FromArgb(50, Color.Gray)
        LogLabel.BackColor = Color.FromArgb(50, Color.White)

        '调整容器控件的位置和尺寸
        TimeLinePanel.Location = New Point(15, CreateJobListButton.Bottom + 15)
        TimeLinePanel.Size = New Size((Me.Width - 40) * 0.7, (Me.Height - TimeLinePanel.Top - 15) * 0.2)
        DispatchPanel.Location = New Point(15, TimeLinePanel.Bottom + 10)
        DispatchPanel.Size = New Size(TimeLinePanel.Width, (Me.Height - TimeLinePanel.Top - 10) * 0.6)
        RecordPanel.Location = New Point(TimeLinePanel.Left, DispatchPanel.Bottom + 10)
        RecordPanel.Size = New Size(TimeLinePanel.Width, Me.Height - DispatchPanel.Bottom - 25)
        LogLabel.Location = New Point(TimeLinePanel.Right + 10, TimeLinePanel.Top)
        LogLabel.Size = New Size((Me.Width - 40) * 0.3, RecordPanel.Bottom - TimeLinePanel.Top)

        '计算各区域单位时间的宽度和任务高度
        CoordinateRectangle = New Rectangle(15, 25, TimeLinePanel.Width - 30, TimeLinePanel.Height - 45)
        TimeCellWidth = CoordinateRectangle.Width / Max_SystemTime
        TimeCellHeight = CoordinateRectangle.Height / Max_JobCount
        DispatchRectangle = New Rectangle(15, 25, DispatchPanel.Width - 30, DispatchPanel.Height - 45)
        DispatchCellHeight = (DispatchRectangle.Height + ExecuteRectangle.Top - DispatchRectangle.Top - WaitLabel.Height - 10 - Max_JobCount * 2) / (Max_JobCount + 1)
        ExecuteRectangle = New Rectangle(15, 50, DispatchRectangle.Width, DispatchCellHeight)
        WaitRectangle.Location = New Point(ExecuteRectangle.Left + ExecuteRectangle.Width * 0.3, ExecuteRectangle.Bottom + WaitLabel.Height + 10)
        WaitRectangle.Size = New Size(ExecuteRectangle.Right - WaitRectangle.Left, (DispatchCellHeight + 2) * Max_JobCount)
        DispatchCellWidth = WaitRectangle.Width / Max_SystemTime
        RecordCellWidth = RecordPanel.Width
        RecordCellHeight = RecordPanel.Height / Max_JobCount

        '初始化时间线控件
        TimeLineLabel.Parent = TimeLinePanel
        TimeLineLabel.Image = New Bitmap(My.Resources.UnityResource.TimeLine, 29, CoordinateRectangle.Height)
        TimeLineLabel.SetBounds(CoordinateRectangle.Left - 14, CoordinateRectangle.Top, TimeLineLabel.Image.Width, TimeLineLabel.Image.Height)

        '初始化调度区域指示控件
        ExecuteLabel.Parent = DispatchPanel
        ExecuteLabel.Location = New Point(WaitRectangle.Left, ExecuteRectangle.Top - ExecuteLabel.Height - 5)
        WaitLabel.Parent = DispatchPanel
        NextJobTipLabel.Parent = DispatchPanel
        WaitLabel.Location = New Point(WaitRectangle.Left, WaitRectangle.Top - WaitLabel.Height - 5)

        '初始化系统时间标签
        SystemClockLabel.Left = LogLabel.Right - SystemClockLabel.Width
        SystemClockTitle.Left = SystemClockLabel.Left - SystemClockTitle.Width

        '初始化调度算法选择控件
        DispatchComboBox.SelectedIndex = 5
    End Sub

#End Region

#Region "标题栏按钮事件"

    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        If SystemClockTimer.Enabled Then PlayPauseButton_Click(sender, e)
        Me.Hide()
    End Sub

#End Region

#Region "标题栏按钮动态效果"

    Private Sub ControlButton_MouseDown(sender As Label, e As MouseEventArgs) Handles CloseButton.MouseDown
        sender.Image = My.Resources.UnityResource.ResourceManager.GetObject(sender.Tag & "_2")
    End Sub

    Private Sub ControlButton_MouseEnter(sender As Label, e As EventArgs) Handles CloseButton.MouseEnter
        sender.Image = My.Resources.UnityResource.ResourceManager.GetObject(sender.Tag & "_1")
    End Sub

    Private Sub ControlButton_MouseLeave(sender As Label, e As EventArgs) Handles CloseButton.MouseLeave
        sender.Image = My.Resources.UnityResource.ResourceManager.GetObject(sender.Tag & "_0")
    End Sub

    Private Sub ControlButton_MouseUp(sender As Label, e As MouseEventArgs) Handles CloseButton.MouseUp
        sender.Image = My.Resources.UnityResource.ResourceManager.GetObject(sender.Tag & "_1")
    End Sub

#End Region

#Region "纯色按钮动态效果"

    Private Sub ColorButton_MouseDown(sender As Label, e As MouseEventArgs)
        sender.BackColor = Color.FromArgb(ButtonAlphaExecute, ButtonColorExecute)
    End Sub

    Private Sub ColorButton_MouseEnter(sender As Label, e As EventArgs)
        sender.BackColor = Color.FromArgb(ButtonAlphaActive, ButtonColorActive)
    End Sub

    Private Sub ColorButton_MouseLeave(sender As Label, e As EventArgs)
        sender.BackColor = Color.FromArgb(ButtonAlphaDefault, ButtonColorDefault)
    End Sub

    Private Sub ColorButton_MouseUp(sender As Label, e As MouseEventArgs)
        sender.BackColor = Color.FromArgb(ButtonAlphaActive, ButtonColorActive)
    End Sub

#End Region

#Region "功能函数"

    ''' <summary>
    ''' 重置系统配置
    ''' </summary>
    Private Sub ResetSystem()
        SystemClock = 0
        NextJobSubscript = 0
        ExecuteTime = 0
        ExecuteJob = Nothing
        SystemClockLabel.Text = "0"
        SystemClockTimer.Stop()
        PlayPauseButton.Enabled = True
        PlayPauseButton.Text = "播放   "
        PlayPauseButton.Image = My.Resources.UnityResource.Play
        DispatchPanel.Image = Nothing
        RecordPanel.Image = Nothing
        TimeLineLabel.Hide()
        ExecuteLabel.Hide()
        WaitLabel.Hide()
        NextJobTipLabel.Hide()
        PriorityListSubscript = 0
        PriorityList = New List(Of JobClass)() {}
        Select Case DispatchComboBox.SelectedIndex
            Case < 4
                If DispatchComboBox.Items.Count = 4 Then
                    DispatchComboBox.Items.Add("时间片轮转-RR")
                    DispatchComboBox.Items.Add("多级反馈队列-MFQ")
                End If
            Case 4
                DispatchComboBox.Enabled = True
            Case 5
                DispatchComboBox.Enabled = True
                TimeSliceNumeric.Enabled = True
        End Select

        TimeLineLabel.Location = New Point(CoordinateRectangle.Left - 14, CoordinateRectangle.Top - 1)
        LogLabel.TextBox.Text = "重置系统配置！" & vbCrLf
    End Sub

    ''' <summary>
    ''' 重新生成作业列表
    ''' </summary>
    Private Sub CreateJobsList()
        '清空作业列表
        AllJobList.Clear()
        WaitJobList.Clear()
        ExecuteLogs.Clear()
        LogLabel.TextBox.Text &= "开始重新生成作业队列..." & vbCrLf
        For Index As Integer = 0 To Max_JobCount - 1
            Dim JobStartTime As Integer = SystemRandom.Next(Max_SystemTime)
            Dim JobEndTime As Integer = JobStartTime + 1 + SystemRandom.Next(Max_SystemTime - JobStartTime)
            Dim InsJob As JobClass = New JobClass(Index, "作业-" & Index, JobStartTime, JobEndTime, JobColors(Index), SystemRandom.Next(Max_JobCount))
            AllJobList.Add(InsJob)
            LogLabel.TextBox.Text &= String.Format("    生成作业：{0} / 时间：{1}-{2}", InsJob.Name, InsJob.StartTime, InsJob.EndTime) & vbCrLf
        Next
        LogLabel.TextBox.Text &= "生成作业队列完毕！" & vbCrLf
        LogLabel.TextBox.Text &= "================" & vbCrLf
    End Sub

    ''' <summary>
    ''' 绘制时间线区域图像
    ''' </summary>
    Private Function CreateTimeLineImage() As Image
        Dim Index As Integer
        Dim TimeLineImage As Bitmap = New Bitmap(TimeLinePanel.Width, TimeLinePanel.Height)
        Dim TimeLineGraphics As Graphics = Graphics.FromImage(TimeLineImage)
        Dim TempPen As Pen = New Pen(Color.FromArgb(150, Color.MediumSpringGreen), 3)
        '绘制坐标轴
        TimeLineGraphics.DrawLine(TempPen, CoordinateRectangle.Left, CoordinateRectangle.Top + 1, CoordinateRectangle.Left, CoordinateRectangle.Bottom + 1)
        TimeLineGraphics.DrawLine(TempPen, CoordinateRectangle.Left - 1, CoordinateRectangle.Bottom + 1, CoordinateRectangle.Right + 1, CoordinateRectangle.Bottom + 1)
        TempPen = New Pen(Color.FromArgb(100, Color.White), 1)
        '绘制时间线和数值
        For Index = 0 To Max_SystemTime Step 5
            TimeLineGraphics.DrawLine(TempPen, CInt(CoordinateRectangle.Left + TimeCellWidth * Index), CoordinateRectangle.Top, CInt(CoordinateRectangle.Left + TimeCellWidth * Index), CoordinateRectangle.Bottom)
            TimeLineGraphics.DrawString(Index, Me.Font, Brushes.Yellow, CoordinateRectangle.Left + TimeCellWidth * Index - 4, CoordinateRectangle.Bottom + 2)
        Next
        '遍历作业列表绘制作业
        For Index = 0 To Max_JobCount - 1
            TempPen = New Pen(AllJobList(Index).Color, 2)
            TimeLineGraphics.DrawLine(TempPen, CInt(CoordinateRectangle.Left + TimeCellWidth * (AllJobList(Index).StartTime)), CInt(CoordinateRectangle.Top + Index * TimeCellHeight + 5), CInt(CoordinateRectangle.Left + TimeCellWidth * (AllJobList(Index).EndTime)), CInt(CoordinateRectangle.Top + Index * TimeCellHeight + 5))
            TimeLineGraphics.FillEllipse(New SolidBrush(AllJobList(Index).Color), CInt(CoordinateRectangle.Left + TimeCellWidth * (AllJobList(Index).StartTime)) - 3, CInt(CoordinateRectangle.Top + Index * TimeCellHeight + 2), 5, 5)
            TimeLineGraphics.DrawString("JID-" & Index, Me.Font, New SolidBrush(AllJobList(Index).Color), CInt(CoordinateRectangle.Left + TimeCellWidth * AllJobList(Index).StartTime - 10), CoordinateRectangle.Top + Index * TimeCellHeight - 7)
        Next
        Return TimeLineImage
    End Function

    ''' <summary>
    ''' 检查作业到达（针对 FCFS、SJF、HRN、HPF 调度算法）
    ''' </summary>
    ''' <return>是否加入了新作业</return> 
    Private Function CheckJobArriveFCFS_SJF_HRN_HPF() As Boolean
        Dim JIDs As String = vbNullString
        Dim Index As Integer = 0, ListCount As Integer = AllJobList.Count
        Do While Index < ListCount
            If AllJobList(Index).StartTime = SystemClock Then
                '作业到达时从左右作业列表转移至等待列表，所有作业总数减一，遍历下标不变
                JIDs &= AllJobList(Index).ID & " 和 "
                WaitJobList.Add(AllJobList(Index))
                AllJobList.RemoveAt(Index)
                ListCount -= 1
            Else
                '作业未到达，遍历下一作业
                Index += 1
            End If
        Loop
        If JIDs <> vbNullString Then
            '有作业达到
            LogLabel.TextBox.Text &= String.Format("系统时间：{0}  ||  作业-{1} 加入等待执行队列！", SystemClock, JIDs.Remove(JIDs.Length - 3)) & vbCrLf
            Return True
        Else
            '无作业到达
            Return False
        End If
    End Function

    ''' <summary>
    ''' 检查作业执行结束并启动下一个作业（针对 FCFS、SJF、HRN、HPF 调度算法）
    ''' </summary>
    ''' <returns>是否有作业删除</returns> 
    Private Function CheckJobCompeletFCFS_SJF_HRN_HPF() As Boolean
        If (IsNothing(ExecuteJob)) Then
            '当前未执行作业
            If WaitJobList.Count > 0 Then
                '等待列表不为空时从等待列表装入作业执行
                ExecuteJob = WaitJobList(NextJobSubscript)
                '记录日志
                Dim ExecuteLog As ExecuteLog = New ExecuteLog(ExecuteJob.ID, ExecuteJob.Name, SystemClock, ExecuteJob.TimeLength, ExecuteJob.Color)
                ExecuteLogs.Add(ExecuteLog)
                LogLabel.TextBox.Text &= String.Format("系统时间：{0}  ||  开始执行 {1}！", SystemClock, ExecuteJob.Name) & vbCrLf
                ExecuteTime = SystemClock
                ExecuteRectangle.Size = New Size(DispatchCellWidth * ExecuteJob.TimeLength, ExecuteRectangle.Height)
                '从等待列表移除作业
                WaitJobList.RemoveAt(NextJobSubscript)
            End If
            '返回等待列表是否为空
            Return (WaitJobList.Count > 0)
        Else
            If SystemClock >= ExecuteTime + ExecuteJob.TimeLength Then
                '有作业正在执行且完成
                If WaitJobList.Count > 0 Then
                    '从等待列表装入一个作业
                    LogLabel.TextBox.Text &= String.Format("系统时间：{0}  ||  {1} 执行完毕！开始执行 {2}！", SystemClock, ExecuteJob.Name, WaitJobList(NextJobSubscript).Name) & vbCrLf
                    ExecuteJob = WaitJobList(NextJobSubscript)
                    '记录日志
                    Dim ExecuteLog As ExecuteLog = New ExecuteLog(ExecuteJob.ID, ExecuteJob.Name, SystemClock, ExecuteJob.TimeLength, ExecuteJob.Color)
                    ExecuteLogs.Add(ExecuteLog)
                    ExecuteTime = SystemClock
                    ExecuteRectangle.Size = New Size(DispatchCellWidth * (ExecuteJob.EndTime - ExecuteJob.StartTime), ExecuteRectangle.Height)
                    '从等待列表移除
                    WaitJobList.RemoveAt(NextJobSubscript)
                    '返回等待列表是否为空
                    Return (WaitJobList.Count > 0)
                Else
                    '当前作业执行完毕且等待列表为空时，仅销毁当前作业
                    ExecuteJob = Nothing
                End If
            End If
            '有作业但未执行完毕
            Return False
        End If
    End Function

    ''' <summary>
    ''' 检查是否所有作业都已经完成（针对 FCFS、SJF、HRN、HPF、RR 调度算法）
    ''' </summary>
    Private Function CheckAllJobCompeletFCFS_SJF_HRN_HPF_RR() As Boolean
        Return (SystemClock > 0 AndAlso AllJobList.Count = 0 AndAlso WaitJobList.Count = 0 AndAlso IsNothing(ExecuteJob))
    End Function

    ''' <summary>
    ''' 检查是否所有作业都已经完成（针对 MFQ 调度算法）
    ''' </summary>
    Private Function CheckAllJobCompeletMFQ() As Boolean
        If SystemClock = 0 Then Return False
        If AllJobList.Count > 0 Then Return False
        For Index As Integer = 0 To PriorityList.Length - 1
            If PriorityList(Index).Count > 0 Then Return False
        Next
        Return True
    End Function

    ''' <summary>
    ''' 所有作业完成
    ''' </summary>
    Private Sub AllJobCompelet()
        If ReplayCheckBox.Checked Then
            '需要自动重置并播放，初始化系统参数，重置，播放
            SystemClock = 0
            NextJobSubscript = 0
            ExecuteTime = 0
            SystemClockLabel.Text = "0"
            DispatchPanel.Image = Nothing
            TimeLineLabel.Location = New Point(CoordinateRectangle.Left - 14, CoordinateRectangle.Top - 1)
            ExecuteLabel.Hide()
            WaitLabel.Hide()
            NextJobTipLabel.Hide()
            LogLabel.TextBox.Text = "重置系统配置！" & vbCrLf
            CreateJobsList()
            ReDim PriorityList(PriorityList.Length - 1)
            For Index As Integer = 0 To PriorityList.Length - 1
                PriorityList(Index) = New List(Of JobClass)
            Next
            TimeLinePanel.Image = CreateTimeLineImage()
            ExecuteFunctionComman()
        Else
            '所有作业完成，弹出提示信息
            LogLabel.TextBox.Text &= String.Format("系统时间：{0}  ||  所有作业已完成！", SystemClock) & vbCrLf
            SystemClockTimer.Stop()
            PlayPauseButton.Text = "播放   "
            PlayPauseButton.Image = My.Resources.UnityResource.Play
            MsgBox("年轻的樵夫呦！-貌似队列里所有的作业都已经执行完毕了呢！-快来重置生成新的作业队列吧！".Replace("-", vbCrLf), MsgBoxStyle.Information, "Leon：)")
        End If
    End Sub

    ''' <summary>
    ''' 返回等待队列里下次要执行的作业下标（针对 FCFS、SJF、HRN、HPF 调度算法）
    ''' </summary>
    Private Function GetNextJobSubscript() As Integer
        Dim JobSubscript As Integer = 0
        Select Case DispatchComboBox.SelectedIndex
            Case 0
                '先来先服务-FCFS
                NextJobTipLabel.Location = New Point(WaitRectangle.Left - NextJobTipLabel.Width, WaitRectangle.Top + JobSubscript * DispatchCellHeight)
                Return JobSubscript
            Case 1
                '短作业优先-SJF
                For Index As Integer = 1 To WaitJobList.Count - 1
                    If (WaitJobList(Index).TimeLength < WaitJobList(JobSubscript).TimeLength) Then JobSubscript = Index
                Next
                LogLabel.TextBox.Text &= String.Format("系统时间：{0}  ||  找到最短作业：{1}", SystemClock, WaitJobList(JobSubscript).Name) & vbCrLf
                NextJobTipLabel.Location = New Point(WaitRectangle.Left - NextJobTipLabel.Width, WaitRectangle.Top + JobSubscript * DispatchCellHeight)
                Return JobSubscript
            Case 2
                '最高响应比优先-HRN（响应比 = 1+ 等待时间/执行时间）
                ResponseRatios.Clear()
                For JobSubscript = 0 To WaitJobList.Count - 1
                    '计算每个作业的响应比
                    ResponseRatios.Add(Math.Round((SystemClock - WaitJobList(JobSubscript).StartTime + WaitJobList(JobSubscript).TimeLength) / WaitJobList(JobSubscript).TimeLength, 2))
                Next
                '返回最大值下标
                JobSubscript = ResponseRatios.IndexOf(ResponseRatios.Max)
                LogLabel.TextBox.Text &= String.Format("系统时间：{0}  ||  找到最高响应比作业：{1}", SystemClock, WaitJobList(JobSubscript).Name) & vbCrLf
                NextJobTipLabel.Location = New Point(WaitRectangle.Left - NextJobTipLabel.Width, WaitRectangle.Top + JobSubscript * DispatchCellHeight)
                Return (JobSubscript)
            Case 3
                '优先数调度-HPF
                For Index As Integer = 1 To WaitJobList.Count - 1
                    If (WaitJobList(Index).Priority > WaitJobList(JobSubscript).Priority) Then JobSubscript = Index
                Next
                LogLabel.TextBox.Text &= String.Format("系统时间：{0}  ||  找到最高优先数作业：{1}", SystemClock, WaitJobList(JobSubscript).Name) & vbCrLf
                NextJobTipLabel.Location = New Point(WaitRectangle.Left - NextJobTipLabel.Width, WaitRectangle.Top + JobSubscript * DispatchCellHeight)
                Return JobSubscript
        End Select
        Return 0
    End Function

    ''' <summary>
    ''' 调度函数枢纽，用于分配适当的调度算法
    ''' </summary>
    Private Sub ExecuteFunctionComman()
        Select Case DispatchComboBox.SelectedIndex
            Case < 4
                ExecuteFunctionFCFS_SJF_HRN_HPF()
            Case 4
                ExecuteFunctionRR()
            Case 5
                ExecuteFunctionMFQ()
        End Select
    End Sub

    ''' <summary>
    ''' 系统时钟值守执行（针对 FCFS、SJF、HRN、HPF 调度算法）
    ''' </summary>
    Private Sub ExecuteFunctionFCFS_SJF_HRN_HPF()
        On Error Resume Next
        '检查作业到达
        If CheckJobArriveFCFS_SJF_HRN_HPF() Then NextJobSubscript = GetNextJobSubscript()
        TimeLineLabel.Left = Math.Min(CoordinateRectangle.Right - 14, CInt(CoordinateRectangle.Left + TimeCellWidth * SystemClock - 14))
        '刷新调度区域图像
        DispatchPanel.Image = CreateDispatchImageFCFS_SJF_HRN_HPF()
        '刷新图像日志记录
        RecordPanel.Image = CreateRecordImageFCFS_SJF_HRN_HPF()
        '检查作业结束
        If CheckJobCompeletFCFS_SJF_HRN_HPF() Then NextJobSubscript = GetNextJobSubscript()
        '检查所有作业完成
        If CheckAllJobCompeletFCFS_SJF_HRN_HPF_RR() Then AllJobCompelet()
    End Sub

    ''' <summary>
    ''' 绘制调度区图像（针对 FCFS、SJF、HRN、HPF 调度算法）
    ''' </summary>
    Private Function CreateDispatchImageFCFS_SJF_HRN_HPF() As Image
        On Error Resume Next
        Dim DispatchImage As Bitmap = New Bitmap(DispatchPanel.Width, DispatchPanel.Height)
        Dim DispatchGraphics As Graphics = Graphics.FromImage(DispatchImage)
        Dim WaitJobPoint As Point
        Dim WaitJobSize As Size
        Dim TempPen As Pen
        '绘制等待分界线
        DispatchGraphics.DrawLine(Pens.Red, WaitRectangle.Left + ShadowDistance, ExecuteRectangle.Top + ShadowDistance, WaitRectangle.Left + ShadowDistance, WaitRectangle.Bottom + ShadowDistance)
        DispatchGraphics.DrawLine(Pens.Red, WaitRectangle.Left, ExecuteRectangle.Top, WaitRectangle.Left + ShadowDistance, ExecuteRectangle.Top + ShadowDistance)

        If WaitJobList.Count > 0 Then
            If Not WaitLabel.Visible Then WaitLabel.Show()
            If Not NextJobTipLabel.Visible Then NextJobTipLabel.Show()
            Dim InsWaitJob As JobClass
            '遍历等待作业列表
            For Index As Integer = 0 To WaitJobList.Count - 1
                InsWaitJob = WaitJobList(Index)
                '计算绘制作业的坐标和大小
                WaitJobPoint = New Point(WaitRectangle.Left, WaitRectangle.Top + Index * (DispatchCellHeight + 2))
                WaitJobSize = New Size(DispatchCellWidth * InsWaitJob.TimeLength, DispatchCellHeight)
                '当前执行作业需要突出显示
                If (NextJobSubscript = Index) Then
                    ShadowDistance = 8
                    WaitJobPoint.Offset(-3, -3)
                Else
                    ShadowDistance = 5
                End If

                '绘制立体阴影
                TempPen = New Pen(InsWaitJob.Color, 1)
                DispatchGraphics.DrawLine(TempPen, WaitJobPoint.X, WaitJobPoint.Y + WaitJobSize.Height, WaitJobPoint.X + ShadowDistance, WaitJobPoint.Y + WaitJobSize.Height + ShadowDistance)
                DispatchGraphics.DrawLine(TempPen, WaitJobPoint.X + WaitJobSize.Width, WaitJobPoint.Y, WaitJobPoint.X + WaitJobSize.Width + ShadowDistance, WaitJobPoint.Y + ShadowDistance)
                DispatchGraphics.DrawLine(TempPen, WaitJobPoint.X + WaitJobSize.Width, WaitJobPoint.Y + WaitJobSize.Height, WaitJobPoint.X + WaitJobSize.Width + ShadowDistance, WaitJobPoint.Y + WaitJobSize.Height + ShadowDistance)
                DispatchGraphics.DrawLine(TempPen, WaitJobPoint.X + ShadowDistance, WaitJobPoint.Y + ShadowDistance, WaitJobPoint.X + ShadowDistance, WaitJobPoint.Y + WaitJobSize.Height + ShadowDistance)
                DispatchGraphics.DrawLine(TempPen, WaitJobPoint.X + ShadowDistance, WaitJobPoint.Y + ShadowDistance, WaitJobPoint.X + WaitJobSize.Width + ShadowDistance, WaitJobPoint.Y + ShadowDistance)
                DispatchGraphics.DrawLine(TempPen, WaitJobPoint.X + WaitJobSize.Width + ShadowDistance, WaitJobPoint.Y + ShadowDistance, WaitJobPoint.X + WaitJobSize.Width + ShadowDistance, WaitJobPoint.Y + WaitJobSize.Height + ShadowDistance)
                DispatchGraphics.DrawLine(TempPen, WaitJobPoint.X + ShadowDistance, WaitJobPoint.Y + WaitJobSize.Height + ShadowDistance, WaitJobPoint.X + WaitJobSize.Width + ShadowDistance, WaitJobPoint.Y + WaitJobSize.Height + ShadowDistance)
                '填充作业区域
                DispatchGraphics.FillRectangle(New SolidBrush(InsWaitJob.Color), New Rectangle(WaitJobPoint, WaitJobSize))
                '显示作业信息
                WaitJobPoint.Offset(3, 3)
                Select Case DispatchComboBox.SelectedIndex
                    Case 0
                        DispatchGraphics.DrawString(InsWaitJob.Name & " / 到达顺序：" & Index, Me.Font, Brushes.Black, WaitJobPoint)
                        WaitJobPoint.Offset(-1, -1)
                        DispatchGraphics.DrawString(InsWaitJob.Name & " / 到达顺序：" & Index, Me.Font, Brushes.White, WaitJobPoint)
                    Case 1
                        DispatchGraphics.DrawString(InsWaitJob.Name & " / 作业长度：" & InsWaitJob.TimeLength, Me.Font, Brushes.Black, WaitJobPoint)
                        WaitJobPoint.Offset(-1, -1)
                        DispatchGraphics.DrawString(InsWaitJob.Name & " / 作业长度：" & InsWaitJob.TimeLength, Me.Font, Brushes.White, WaitJobPoint)
                    Case 2
                        If ResponseRatios.Count > Index Then
                            DispatchGraphics.DrawString(InsWaitJob.Name & " / 响应比：" & ResponseRatios(Index), Me.Font, Brushes.Black, WaitJobPoint)
                            WaitJobPoint.Offset(-1, -1)
                            DispatchGraphics.DrawString(InsWaitJob.Name & " / 响应比：" & ResponseRatios(Index), Me.Font, Brushes.White, WaitJobPoint)
                        End If
                    Case 3
                        DispatchGraphics.DrawString(InsWaitJob.Name & " / 优先数：" & InsWaitJob.Priority, Me.Font, Brushes.Black, WaitJobPoint)
                        WaitJobPoint.Offset(-1, -1)
                        DispatchGraphics.DrawString(InsWaitJob.Name & " / 优先数：" & InsWaitJob.Priority, Me.Font, Brushes.White, WaitJobPoint)
                End Select
            Next
        Else
            If WaitLabel.Visible Then WaitLabel.Hide()
            If NextJobTipLabel.Visible Then NextJobTipLabel.Hide()
        End If
        '绘制当前执行作业信息
        If Not (IsNothing(ExecuteJob)) Then
            If Not ExecuteLabel.Visible Then
                ExecuteLabel.Show()
            End If
            ExecuteLabel.Text = "正在执行：" & ExecuteJob.Name
            TempPen = New Pen(ExecuteJob.Color, 1)
            ExecuteRectangle.Location = New Point(WaitRectangle.Left - DispatchCellWidth * (SystemClock - ExecuteTime), ExecuteRectangle.Top)
            '绘制立体阴影
            DispatchGraphics.DrawLine(TempPen, ExecuteRectangle.Left, ExecuteRectangle.Top + ExecuteRectangle.Height, ExecuteRectangle.Left + ShadowDistance, ExecuteRectangle.Top + ExecuteRectangle.Height + ShadowDistance)
            DispatchGraphics.DrawLine(TempPen, ExecuteRectangle.Left + ExecuteRectangle.Width, ExecuteRectangle.Top, ExecuteRectangle.Left + ExecuteRectangle.Width + ShadowDistance, ExecuteRectangle.Top + ShadowDistance)
            DispatchGraphics.DrawLine(TempPen, ExecuteRectangle.Left + ExecuteRectangle.Width, ExecuteRectangle.Top + ExecuteRectangle.Height, ExecuteRectangle.Left + ExecuteRectangle.Width + ShadowDistance, ExecuteRectangle.Top + ExecuteRectangle.Height + ShadowDistance)
            DispatchGraphics.DrawLine(TempPen, ExecuteRectangle.Left + ShadowDistance, ExecuteRectangle.Top + ShadowDistance, ExecuteRectangle.Left + ShadowDistance, ExecuteRectangle.Top + ExecuteRectangle.Height + ShadowDistance)
            DispatchGraphics.DrawLine(TempPen, ExecuteRectangle.Left + ShadowDistance, ExecuteRectangle.Top + ShadowDistance, ExecuteRectangle.Left + ExecuteRectangle.Width + ShadowDistance, ExecuteRectangle.Top + ShadowDistance)
            DispatchGraphics.DrawLine(TempPen, ExecuteRectangle.Left + ExecuteRectangle.Width + ShadowDistance, ExecuteRectangle.Top + ShadowDistance, ExecuteRectangle.Left + ExecuteRectangle.Width + ShadowDistance, ExecuteRectangle.Top + ExecuteRectangle.Height + ShadowDistance)
            DispatchGraphics.DrawLine(TempPen, ExecuteRectangle.Left + ShadowDistance, ExecuteRectangle.Top + ExecuteRectangle.Height + ShadowDistance, ExecuteRectangle.Left + ExecuteRectangle.Width + ShadowDistance, ExecuteRectangle.Top + ExecuteRectangle.Height + ShadowDistance)
            '填充区域并描边
            DispatchGraphics.FillRectangle(New SolidBrush(ExecuteJob.Color), ExecuteRectangle)
            DispatchGraphics.DrawRectangle(Pens.Red, ExecuteRectangle)
        Else
            ExecuteLabel.Hide()
        End If
        '绘制等待分界面上层线条
        ShadowDistance = 5
        DispatchGraphics.DrawLine(Pens.Red, WaitRectangle.Left, ExecuteRectangle.Top, WaitRectangle.Left, WaitRectangle.Bottom)
        DispatchGraphics.DrawLine(Pens.Red, WaitRectangle.Left, WaitRectangle.Bottom, WaitRectangle.Left + ShadowDistance, WaitRectangle.Bottom + ShadowDistance)

        Return DispatchImage
    End Function

    ''' <summary>
    ''' 绘制图像日志记录（针对 FCFS、SJF、HRN、HPF 调度算法）
    ''' </summary>
    Private Function CreateRecordImageFCFS_SJF_HRN_HPF() As Bitmap
        On Error Resume Next
        Dim RecordImage As Bitmap = New Bitmap(RecordPanel.Width, RecordPanel.Height)
        Dim RecordGraphics As Graphics = Graphics.FromImage(RecordImage)
        Dim InsExecuteLog As ExecuteLog
        '实时更新单位时间对应的图像宽度
        RecordCellWidth = RecordPanel.Width / SystemClock
        '遍历日志记录列表
        For Index As Integer = 0 To ExecuteLogs.Count - 1
            InsExecuteLog = ExecuteLogs(Index)
            '绘制执行时间线
            RecordGraphics.DrawLine(New Pen(Color.FromArgb(50, Color.White), 1), CInt(InsExecuteLog.ExecuteTime * RecordCellWidth), 0, CInt(InsExecuteLog.ExecuteTime * RecordCellWidth), RecordPanel.Height)
            '绘制作业
            RecordGraphics.DrawLine(New Pen(InsExecuteLog.Color, 2),
                CInt(InsExecuteLog.ExecuteTime * RecordCellWidth), CInt(InsExecuteLog.ID * RecordCellHeight + 4),
                CInt(InsExecuteLog.CompleteTime * RecordCellWidth), CInt(InsExecuteLog.ID * RecordCellHeight + 4))
            '显示作业名称和执行时间
            RecordGraphics.DrawString(InsExecuteLog.Name, Me.Font, New SolidBrush(InsExecuteLog.Color), CInt(InsExecuteLog.ExecuteTime * RecordCellWidth), CInt(InsExecuteLog.ID * RecordCellHeight - 10))
            RecordGraphics.DrawString(InsExecuteLog.ExecuteTime, Me.Font, Brushes.White, CInt(InsExecuteLog.ExecuteTime * RecordCellWidth), RecordPanel.Height - 13)
        Next
        Return RecordImage
    End Function

    ''' <summary>
    ''' 系统时钟值守执行（针对 RR 调度算法）
    ''' </summary>
    Private Sub ExecuteFunctionRR()
        On Error Resume Next
        '检查作业到达
        CheckJobArriveRR()
        Dim NextSubscriptTipsLocation As Integer
        Dim InsWaitJob As JobClass
        TimeLineLabel.Left = Math.Min(CoordinateRectangle.Right - 14, CInt(CoordinateRectangle.Left + TimeCellWidth * SystemClock - 14))
        '遍历等待列表
        If WaitJobList.Count > 0 Then
            InsWaitJob = WaitJobList(NextJobSubscript)
            '记录日志
            Dim ExecuteLog As ExecuteLog = New ExecuteLog(InsWaitJob.ID, InsWaitJob.Name, SystemClock, 1, InsWaitJob.Color)
            ExecuteLogs.Add(ExecuteLog)
            '作业时间片加一
            InsWaitJob.TimeSlice += 1
            '适应执行作业指示的位置
            NextSubscriptTipsLocation = WaitRectangle.Left - DispatchCellWidth * InsWaitJob.TimeSlice - NextJobTipLabel.Width
            If NextSubscriptTipsLocation < 0 Then
                NextSubscriptTipsLocation = WaitRectangle.Left + (InsWaitJob.TimeLength - InsWaitJob.TimeSlice) * DispatchCellWidth + 5
                NextJobTipLabel.Text = "◀ 正在执行作业"
            Else
                NextJobTipLabel.Text = "正在执行作业 ▶"
            End If
            NextJobTipLabel.Location = New Point(NextSubscriptTipsLocation, WaitRectangle.Top + NextJobSubscript * (DispatchCellHeight + 2))
            If Not NextJobTipLabel.Visible Then NextJobTipLabel.Show()
            '刷新调度区域图像
            DispatchPanel.Image = CreateDispatchImageRR()
            '检查作业状态
            If InsWaitJob.TimeSlice >= InsWaitJob.TimeLength Then
                '作业执行完毕，从等待区列表移除
                LogLabel.TextBox.Text &= String.Format("系统时间：{0}  ||  {1} 执行完毕！从队列移除！", SystemClock, InsWaitJob.Name) & vbCrLf
                WaitJobList.RemoveAt(NextJobSubscript)
                If NextJobSubscript = WaitJobList.Count Then
                    '遍历完成一次列表
                    LogLabel.TextBox.Text &= String.Format("系统时间：{0}  ||  分时执行队列扫描完成一次，返回队首！", SystemClock) & vbCrLf
                    NextJobSubscript = 0
                End If
                '初始化时间片
                ExecuteTimeSlice = 0
            Else
                '时间片加一
                ExecuteTimeSlice += 1
                If ExecuteTimeSlice >= TimeSliceNumeric.Value Then
                    '完成预设最大时间片，但作业未完成，更新作业下表执行下一作业
                    NextJobSubscript = (NextJobSubscript + 1) Mod WaitJobList.Count
                    '初始化时间片
                    ExecuteTimeSlice = 0
                End If
            End If
        Else
            If NextJobTipLabel.Visible Then NextJobTipLabel.Hide()
        End If

        '刷新图像日志记录
        RecordPanel.Image = CreateRecordImageRR_MFQ()
        '检查所有作业结束
        If CheckAllJobCompeletFCFS_SJF_HRN_HPF_RR() Then AllJobCompelet()
    End Sub

    ''' <summary>
    ''' 绘制调度区图像（针对 RR 调度算法）
    ''' </summary>
    Private Function CreateDispatchImageRR() As Image
        On Error Resume Next
        Dim DispatchImage As Bitmap = New Bitmap(DispatchPanel.Width, DispatchPanel.Height)
        Dim DispatchGraphics As Graphics = Graphics.FromImage(DispatchImage)
        Dim WaitJobPoint As Point
        Dim WaitJobSize As Size
        Dim TempPen As Pen
        '绘制等待分界线
        DispatchGraphics.DrawLine(Pens.Red, WaitRectangle.Left + ShadowDistance, ExecuteRectangle.Top + ShadowDistance, WaitRectangle.Left + ShadowDistance, WaitRectangle.Bottom + ShadowDistance)
        DispatchGraphics.DrawLine(Pens.Red, WaitRectangle.Left, ExecuteRectangle.Top, WaitRectangle.Left + ShadowDistance, ExecuteRectangle.Top + ShadowDistance)

        Dim InsWaitJob As JobClass
        '遍历等待作业列表
        For Index As Integer = 0 To WaitJobList.Count - 1
            InsWaitJob = WaitJobList(Index)
            '计算作业位置和大小
            WaitJobPoint = New Point(WaitRectangle.Left - DispatchCellWidth * InsWaitJob.TimeSlice, WaitRectangle.Top + Index * (DispatchCellHeight + 2))
            WaitJobSize = New Size(DispatchCellWidth * InsWaitJob.TimeLength, DispatchCellHeight)
            '突出显示正在执行作业
            If NextJobSubscript = Index Then
                ShadowDistance = 8
                WaitJobPoint.Offset(-3, -3)
            Else
                ShadowDistance = 5
            End If
            '绘制作业立体阴影
            TempPen = New Pen(InsWaitJob.Color, 1)
            DispatchGraphics.DrawLine(TempPen, WaitJobPoint.X, WaitJobPoint.Y + WaitJobSize.Height, WaitJobPoint.X + ShadowDistance, WaitJobPoint.Y + WaitJobSize.Height + ShadowDistance)
            DispatchGraphics.DrawLine(TempPen, WaitJobPoint.X + WaitJobSize.Width, WaitJobPoint.Y, WaitJobPoint.X + WaitJobSize.Width + ShadowDistance, WaitJobPoint.Y + ShadowDistance)
            DispatchGraphics.DrawLine(TempPen, WaitJobPoint.X + WaitJobSize.Width, WaitJobPoint.Y + WaitJobSize.Height, WaitJobPoint.X + WaitJobSize.Width + ShadowDistance, WaitJobPoint.Y + WaitJobSize.Height + ShadowDistance)

            DispatchGraphics.DrawLine(TempPen, WaitJobPoint.X + ShadowDistance, WaitJobPoint.Y + ShadowDistance, WaitJobPoint.X + ShadowDistance, WaitJobPoint.Y + WaitJobSize.Height + ShadowDistance)
            DispatchGraphics.DrawLine(TempPen, WaitJobPoint.X + ShadowDistance, WaitJobPoint.Y + ShadowDistance, WaitJobPoint.X + WaitJobSize.Width + ShadowDistance, WaitJobPoint.Y + ShadowDistance)
            DispatchGraphics.DrawLine(TempPen, WaitJobPoint.X + WaitJobSize.Width + ShadowDistance, WaitJobPoint.Y + ShadowDistance, WaitJobPoint.X + WaitJobSize.Width + ShadowDistance, WaitJobPoint.Y + WaitJobSize.Height + ShadowDistance)
            DispatchGraphics.DrawLine(TempPen, WaitJobPoint.X + ShadowDistance, WaitJobPoint.Y + WaitJobSize.Height + ShadowDistance, WaitJobPoint.X + WaitJobSize.Width + ShadowDistance, WaitJobPoint.Y + WaitJobSize.Height + ShadowDistance)

            DispatchGraphics.FillRectangle(New SolidBrush(InsWaitJob.Color), New Rectangle(WaitJobPoint, WaitJobSize))
            If NextJobSubscript = Index Then
                '正在执行作业加描边
                DispatchGraphics.DrawRectangle(Pens.Red, New Rectangle(WaitJobPoint, WaitJobSize))
            End If
            WaitJobPoint.Offset(3, 3)
            '显示作业信息
            DispatchGraphics.DrawString(InsWaitJob.Name & " / 时间片：" & InsWaitJob.TimeSlice & " of " & InsWaitJob.TimeLength, Me.Font, Brushes.Black, Math.Max(1, WaitJobPoint.X + 1), WaitJobPoint.Y + 1)
            DispatchGraphics.DrawString(InsWaitJob.Name & " / 时间片：" & InsWaitJob.TimeSlice & " of " & InsWaitJob.TimeLength, Me.Font, Brushes.White, Math.Max(0, WaitJobPoint.X), WaitJobPoint.Y)
        Next
        '绘制等待分界线上层线条
        DispatchGraphics.DrawLine(Pens.Red, WaitRectangle.Left, ExecuteRectangle.Top, WaitRectangle.Left, WaitRectangle.Bottom)
        DispatchGraphics.DrawLine(Pens.Red, WaitRectangle.Left, WaitRectangle.Bottom, WaitRectangle.Left + ShadowDistance, WaitRectangle.Bottom + ShadowDistance)

        Return DispatchImage
    End Function

    ''' <summary>
    ''' 检查作业到达（针对 RR 调度算法）
    ''' </summary>
    ''' <return>是否加入了新作业</return> 
    Private Sub CheckJobArriveRR()
        Dim JIDs As String = vbNullString
        Dim Index As Integer = 0, ListCount As Integer = AllJobList.Count
        Do While Index < ListCount
            If AllJobList(Index).StartTime = SystemClock Then
                '作业到达时从左右作业列表转移至等待列表，所有作业总数减一，遍历下标不变
                JIDs &= AllJobList(Index).ID & " 和 "
                WaitJobList.Add(AllJobList(Index))
                AllJobList.RemoveAt(Index)
                ListCount -= 1
            Else
                '作业未到达，遍历下一作业
                Index += 1
            End If
        Loop
        If JIDs <> vbNullString Then
            '有作业达到
            LogLabel.TextBox.Text &= String.Format("系统时间：{0}  ||  作业-{1} 加入等待执行队列！", SystemClock, JIDs.Remove(JIDs.Length - 3)) & vbCrLf
        End If
    End Sub

    ''' <summary>
    ''' 绘制图像日志记录（针对 RR、MFQ 调度算法）
    ''' </summary>
    Private Function CreateRecordImageRR_MFQ() As Bitmap
        On Error Resume Next
        Dim RecordImage As Bitmap = New Bitmap(RecordPanel.Width, RecordPanel.Height)
        Dim RecordGraphics As Graphics = Graphics.FromImage(RecordImage)
        Dim InsExecuteLog As ExecuteLog
        '上一次记录ID，用于判断两次作业是否一致，是否需要重新绘制时间线
        Dim LastJID As Integer = Integer.MinValue
        '实时更新单位时间对应图像宽度
        RecordCellWidth = RecordPanel.Width / SystemClock
        '遍历日志记录列表
        For Index As Integer = 0 To ExecuteLogs.Count - 1
            InsExecuteLog = ExecuteLogs(Index)
            '绘制作业
            RecordGraphics.DrawLine(New Pen(InsExecuteLog.Color, 2),
            CInt(InsExecuteLog.ExecuteTime * RecordCellWidth), CInt(InsExecuteLog.ID * RecordCellHeight + 4),
            CInt(InsExecuteLog.CompleteTime * RecordCellWidth), CInt(InsExecuteLog.ID * RecordCellHeight + 4))

            If LastJID <> InsExecuteLog.ID Then
                '新的作业，绘制时间线
                RecordGraphics.DrawLine(New Pen(Color.FromArgb(50, Color.White), 1), CInt(InsExecuteLog.ExecuteTime * RecordCellWidth), 0, CInt(InsExecuteLog.ExecuteTime * RecordCellWidth), RecordPanel.Height)
                RecordGraphics.DrawString(InsExecuteLog.ExecuteTime, Me.Font, Brushes.White, CInt(InsExecuteLog.ExecuteTime * RecordCellWidth), RecordPanel.Height - 13)
                'RecordGraphics.DrawString(InsExecuteLog.Name, Me.Font, New SolidBrush(InsExecuteLog.Color), CInt(InsExecuteLog.ExecuteTime * RecordCellWidth), CInt(InsExecuteLog.ID * RecordCellHeight - 10))
                '更新上一作业ID
                LastJID = InsExecuteLog.ID
            End If
        Next
        Return RecordImage
    End Function

    ''' <summary>
    ''' 系统时钟值守执行（针对 MFQ 调度算法）
    ''' </summary>
    Private Sub ExecuteFunctionMFQ()
        On Error Resume Next
        '检查作业到达
        CheckJobArriveMFQ()
        Dim InsWaitJob As JobClass
        TimeLineLabel.Left = Math.Min(CoordinateRectangle.Right - 14, CInt(CoordinateRectangle.Left + TimeCellWidth * SystemClock - 14))
        '检查当前队列不为空
        If PriorityList(PriorityListSubscript).Count > 0 Then
            InsWaitJob = PriorityList(PriorityListSubscript)(NextJobSubscript)
            '记录执行日志
            Dim ExecuteLog As ExecuteLog = New ExecuteLog(InsWaitJob.ID, InsWaitJob.Name, SystemClock, 1, InsWaitJob.Color)
            ExecuteLogs.Add(ExecuteLog)
            '作业时间片加一
            InsWaitJob.TimeSlice += 1
            '检查作业状态
            If InsWaitJob.TimeSlice >= InsWaitJob.TimeLength Then
                '作业执行完毕
                LogLabel.TextBox.Text &= String.Format("系统时间：{0}  ||  {1} 执行完毕！从队列移除！", SystemClock, InsWaitJob.Name) & vbCrLf
                '从作业队列移除
                PriorityList(PriorityListSubscript).RemoveAt(NextJobSubscript)
                '检查当前队列执行完毕
                If NextJobSubscript >= PriorityList(PriorityListSubscript).Count Then
                    '初始化队列元素下标
                    NextJobSubscript = 0
                    '所有队列数组状态
                    If PriorityListSubscript >= PriorityList.Length - 1 Then
                        '返回首队列
                        PriorityListSubscript = 0
                    Else
                        '跳至下一队列
                        PriorityListSubscript += 1
                    End If
                End If
                '初始化时间片
                ExecuteTimeSlice = 0
            Else
                '计算时间片是否达到队列预设时间片（队列递增）
                If ExecuteTimeSlice >= TimeSliceNumeric.Value * (1 + PriorityListSubscript) Then
                    '时间片用尽，但作业未完成，高优先级队列作业转移到低优先级队列
                    LogLabel.TextBox.Text &= String.Format("系统时间：{0}  ||  {1} 转移到低优先级队列！", SystemClock, InsWaitJob.Name) & vbCrLf
                    PriorityList(PriorityListSubscript + 1).Add(InsWaitJob)
                    PriorityList(PriorityListSubscript).RemoveAt(NextJobSubscript)
                    '检查当前队列执行完毕
                    If NextJobSubscript >= PriorityList(PriorityListSubscript).Count Then
                        '初始化队列元素下标
                        NextJobSubscript = 0
                        '所有队列数组状态
                        If PriorityListSubscript >= PriorityList.Length - 1 Then
                            '返回首队列
                            PriorityListSubscript = 0
                        Else
                            '跳至下一队列
                            PriorityListSubscript += 1
                        End If
                    End If
                    '初始化时间片
                    ExecuteTimeSlice = 0
                Else
                    '时间片加一
                    ExecuteTimeSlice += 1
                End If
            End If
        Else
            '所有队列数组状态
            If PriorityListSubscript >= PriorityList.Length - 1 Then
                '返回首队列
                PriorityListSubscript = 0
            Else
                '跳至下一队列
                PriorityListSubscript += 1
            End If
        End If

        '刷新调度区域图像
        DispatchPanel.Image = CreateDispatchImageMFQ()
        '刷新图像日志记录
        RecordPanel.Image = CreateRecordImageRR_MFQ()
        '检查所有作业结束
        If CheckAllJobCompeletMFQ() Then AllJobCompelet()
    End Sub

    ''' <summary>
    ''' 检查作业到达（针对 MFQ 调度算法）
    ''' </summary>
    ''' <return>是否加入了新作业</return> 
    Private Sub CheckJobArriveMFQ()
        Dim JIDs As String = vbNullString
        Dim Index As Integer = 0, ListCount As Integer = AllJobList.Count
        Dim HasBreak As Boolean = False
        Do While Index < ListCount
            If AllJobList(Index).StartTime = SystemClock Then
                JIDs &= AllJobList(Index).ID & " 和 "
                If PriorityListSubscript > 0 Then
                    '当前执行队列不是首队列时发生抢!断!
                    HasBreak = True
                    NextJobSubscript = 0
                    ExecuteTimeSlice = 0
                End If
                '作业到达，队列指示器回到队首
                PriorityListSubscript = 0
                '作业添加至首队列，并从所有作业列表里移除
                PriorityList(0).Add(AllJobList(Index))
                AllJobList.RemoveAt(Index)
                '总作业数减一
                ListCount -= 1
            Else
                '无作业到达，遍历下一作业
                Index += 1
            End If
        Loop
        If JIDs <> vbNullString Then
            '有作业到达
            LogLabel.TextBox.Text &= String.Format("系统时间：{0}  ||  作业-{1} 加入最高优先级执行队列！", SystemClock, JIDs.Remove(JIDs.Length - 3)) & vbCrLf & IIf(HasBreak, "    触发抢断！" & vbCrLf, vbNullString)
        End If
    End Sub

    ''' <summary>
    ''' 绘制调度区图像（针对 MFQ 调度算法）
    ''' </summary>
    Private Function CreateDispatchImageMFQ() As Image
        On Error Resume Next
        Dim DispatchImage As Bitmap = New Bitmap(DispatchPanel.Width, DispatchPanel.Height)
        Dim DispatchGraphics As Graphics = Graphics.FromImage(DispatchImage)
        Dim WaitJobPoint As Point
        Dim WaitJobSize As Size
        Dim TempPen As Pen
        Dim InsWaitJob As JobClass
        Dim LastTop As Integer
        DispatchPriorityListRectangle.Location = New Point(DispatchRectangle.Left, DispatchRectangle.Top)
        '遍历所有级队列
        For ListIndex As Integer = 0 To PriorityList.Length - 1
            DispatchPriorityListRectangle.Offset(5, 5)
            DispatchGraphics.DrawRectangle(Pens.DimGray, DispatchPriorityListRectangle)
            DispatchPriorityListRectangle.Offset(-5, -5)
            DispatchGraphics.DrawLine(Pens.LightGray, DispatchPriorityListRectangle.Left, DispatchPriorityListRectangle.Top, DispatchPriorityListRectangle.Left + 5, DispatchPriorityListRectangle.Top + 5)
            DispatchGraphics.DrawLine(Pens.LightGray, DispatchPriorityListRectangle.Left, DispatchPriorityListRectangle.Bottom, DispatchPriorityListRectangle.Left + 5, DispatchPriorityListRectangle.Bottom + 5)
            '绘制作业队列信息标题头
            DispatchGraphics.DrawString("第 " & ListIndex & " 优先级", Me.Font, Brushes.Red, New Point(DispatchPriorityListRectangle.Left + 11, DispatchPriorityListRectangle.Top + 16))
            DispatchGraphics.DrawString("时间片：" & (ListIndex + 1) * TimeSliceNumeric.Value, Me.Font, Brushes.Red, New Point(DispatchPriorityListRectangle.Left + 11, DispatchPriorityListRectangle.Top + 32))
            DispatchGraphics.DrawString("第 " & ListIndex & " 优先级", Me.Font, Brushes.Yellow, New Point(DispatchPriorityListRectangle.Left + 10, DispatchPriorityListRectangle.Top + 15))
            DispatchGraphics.DrawString("时间片：" & (ListIndex + 1) * TimeSliceNumeric.Value, Me.Font, Brushes.Yellow, New Point(DispatchPriorityListRectangle.Left + 10, DispatchPriorityListRectangle.Top + 31))
            LastTop = DispatchRectangle.Top + 60
            If PriorityList(ListIndex).Count > 0 Then
                '遍历该队列里所有作业
                For ElementIndex As Integer = 0 To PriorityList(ListIndex).Count - 1
                    InsWaitJob = PriorityList(ListIndex)(ElementIndex)
                    TempPen = New Pen(InsWaitJob.Color, 1)
                    '计算作业位置和大小
                    WaitJobPoint = New Point(DispatchPriorityListRectangle.Left, LastTop)
                    WaitJobSize = New Size(PriorityListCellWidth * (InsWaitJob.TimeLength - InsWaitJob.TimeSlice), DispatchCellHeight)
                    LastTop += DispatchCellHeight + 2
                    ''绘制作业立体阴影
                    DispatchGraphics.DrawLine(TempPen, WaitJobPoint.X, WaitJobPoint.Y + WaitJobSize.Height, WaitJobPoint.X + ShadowDistance, WaitJobPoint.Y + WaitJobSize.Height + ShadowDistance)
                    DispatchGraphics.DrawLine(TempPen, WaitJobPoint.X + WaitJobSize.Width, WaitJobPoint.Y, WaitJobPoint.X + WaitJobSize.Width + ShadowDistance, WaitJobPoint.Y + ShadowDistance)
                    DispatchGraphics.DrawLine(TempPen, WaitJobPoint.X + WaitJobSize.Width, WaitJobPoint.Y + WaitJobSize.Height, WaitJobPoint.X + WaitJobSize.Width + ShadowDistance, WaitJobPoint.Y + WaitJobSize.Height + ShadowDistance)
                    DispatchGraphics.DrawLine(TempPen, WaitJobPoint.X + ShadowDistance, WaitJobPoint.Y + ShadowDistance, WaitJobPoint.X + ShadowDistance, WaitJobPoint.Y + WaitJobSize.Height + ShadowDistance)
                    DispatchGraphics.DrawLine(TempPen, WaitJobPoint.X + ShadowDistance, WaitJobPoint.Y + ShadowDistance, WaitJobPoint.X + WaitJobSize.Width + ShadowDistance, WaitJobPoint.Y + ShadowDistance)
                    DispatchGraphics.DrawLine(TempPen, WaitJobPoint.X + WaitJobSize.Width + ShadowDistance, WaitJobPoint.Y + ShadowDistance, WaitJobPoint.X + WaitJobSize.Width + ShadowDistance, WaitJobPoint.Y + WaitJobSize.Height + ShadowDistance)
                    DispatchGraphics.DrawLine(TempPen, WaitJobPoint.X + ShadowDistance, WaitJobPoint.Y + WaitJobSize.Height + ShadowDistance, WaitJobPoint.X + WaitJobSize.Width + ShadowDistance, WaitJobPoint.Y + WaitJobSize.Height + ShadowDistance)
                    '填充作业并显示作业信息
                    DispatchGraphics.FillRectangle(New SolidBrush(InsWaitJob.Color), New Rectangle(WaitJobPoint, WaitJobSize))
                    WaitJobPoint.Offset(3, 3)
                    DispatchGraphics.DrawString(InsWaitJob.Name & " / 时间片：" & InsWaitJob.TimeSlice & " of " & InsWaitJob.TimeLength, Me.Font, Brushes.Black, WaitJobPoint)
                    WaitJobPoint.Offset(-1, -1)
                    DispatchGraphics.DrawString(InsWaitJob.Name & " / 时间片：" & InsWaitJob.TimeSlice & " of " & InsWaitJob.TimeLength, Me.Font, Brushes.White, WaitJobPoint)
                Next
            End If
            DispatchGraphics.DrawRectangle(Pens.White, DispatchPriorityListRectangle)
            DispatchPriorityListRectangle.Location = New Point(DispatchPriorityListRectangle.Left + DispatchPriorityListRectangle.Width, DispatchRectangle.Top)
        Next
        DispatchGraphics.DrawLine(Pens.LightGray, DispatchPriorityListRectangle.Left, DispatchPriorityListRectangle.Top, DispatchPriorityListRectangle.Left + 5, DispatchPriorityListRectangle.Top + 5)
        DispatchGraphics.DrawLine(Pens.LightGray, DispatchPriorityListRectangle.Left, DispatchPriorityListRectangle.Bottom, DispatchPriorityListRectangle.Left + 5, DispatchPriorityListRectangle.Bottom + 5)

        Return DispatchImage
    End Function

#End Region

#Region "纯色按钮事件"

    Private Sub SpeedUpButton_Click(sender As Object, e As EventArgs) Handles SpeedUpButton.Click
        SystemClockTimer.Interval = Math.Max(100, SystemClockTimer.Interval - 100)
    End Sub

    Private Sub SpeedDownButton_Click(sender As Object, e As EventArgs) Handles SpeedDownButton.Click
        SystemClockTimer.Interval = Math.Min(3000, SystemClockTimer.Interval + 100)
    End Sub

    Private Sub CreateJobListButton_Click(sender As Object, e As EventArgs) Handles CreateJobListButton.Click
        ResetSystem()
        CreateJobsList()
        TimeLinePanel.Image = CreateTimeLineImage()
        GC.Collect()
    End Sub

    Private Sub PlayPauseButton_Click(sender As Object, e As EventArgs) Handles PlayPauseButton.Click
        If PlayPauseButton.Text = "播放   " Then
            If SystemClock = 0 Then
                Select Case DispatchComboBox.SelectedIndex
                    Case < 4
                        NextJobTipLabel.Text = "下次应执行作业 ▶"
                        DispatchComboBox.Items.RemoveAt(4)
                        DispatchComboBox.Items.RemoveAt(4)
                    Case 4
                        NextJobTipLabel.Text = "正在执行作业 ▶"
                        DispatchComboBox.Enabled = False
                    Case 5
                        Dim TempDouble As Double
                        TempDouble = Math.Sqrt(TimeSliceNumeric.Value ^ 2 + 8 * TimeSliceNumeric.Value * Max_SystemTime) / (2 * TimeSliceNumeric.Value) - 1 / 2
                        TempDouble = Fix(TempDouble) - (Fix(TempDouble) < TempDouble)
                        PriorityListSubscript = CInt(TempDouble - 1)
                        ReDim PriorityList(PriorityListSubscript)
                        For Index As Integer = 0 To PriorityListSubscript
                            PriorityList(Index) = New List(Of JobClass)
                        Next
                        DispatchPriorityListRectangle = New Rectangle(0, 0, DispatchRectangle.Width / PriorityList.Count, DispatchRectangle.Height)
                        PriorityListCellWidth = DispatchPriorityListRectangle.Width / Max_SystemTime
                        LogLabel.TextBox.Text &= String.Format("系统时间：{0}  ||  生成 0~{1} 级调度队列，时间片递增！", SystemClock, PriorityListSubscript) & vbCrLf
                        DispatchComboBox.Enabled = False
                        TimeSliceNumeric.Enabled = False
                End Select
                If Not TimeLineLabel.Visible Then TimeLineLabel.Show()
            Else
                If DispatchComboBox.SelectedIndex < 5 Then
                    If CheckAllJobCompeletFCFS_SJF_HRN_HPF_RR() Then Exit Sub
                Else
                    If CheckAllJobCompeletMFQ() Then Exit Sub
                End If
            End If
            PlayPauseButton.Text = "停止   "
            PlayPauseButton.Image = My.Resources.UnityResource.Pause
            ExecuteFunctionComman()
            SystemClockTimer.Start()
        Else
            PlayPauseButton.Text = "播放   "
            PlayPauseButton.Image = My.Resources.UnityResource.Play
            SystemClockTimer.Stop()
        End If
    End Sub

#End Region

#Region "其他控件"

    Private Sub SystemClockTimer_Tick(sender As Object, e As EventArgs) Handles SystemClockTimer.Tick
        '系统全局时间加一
        SystemClock += 1
        SystemClockLabel.Text = SystemClock
        '调用核心函数
        ExecuteFunctionComman()
        '释放所有代内存
        GC.Collect()
    End Sub

    Private Sub DispatchComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DispatchComboBox.SelectedIndexChanged
        '变更调度模式时实时适配
        Select Case DispatchComboBox.SelectedIndex
            Case < 4
                If TimeSliceLabel.Visible Then TimeSliceLabel.Hide()
                If TimeSliceNumeric.Visible Then TimeSliceNumeric.Hide()
                If SystemClock > 0 Then
                    If DispatchComboBox.SelectedIndex < 4 AndAlso WaitJobList.Count > 0 Then
                        NextJobSubscript = GetNextJobSubscript()
                    End If
                    ExecuteFunctionComman()
                End If
            Case 4
                If Not TimeSliceLabel.Visible Then TimeSliceLabel.Show()
                If Not TimeSliceNumeric.Visible Then TimeSliceNumeric.Show()
            Case 5
                If Not TimeSliceLabel.Visible Then TimeSliceLabel.Show()
                If Not TimeSliceNumeric.Visible Then TimeSliceNumeric.Show()
        End Select
    End Sub

#End Region

End Class
