Imports System.ComponentModel

Public Class VMMainForm
    ''' <summary>
    ''' 内存区域大小
    ''' </summary>
    Private Const Max_MemorySize As Integer = 1024
    ''' <summary>
    ''' 纯色按钮默认透明度
    ''' </summary>
    Private Const ButtonAlphaDefault As Integer = 25
    ''' <summary>
    ''' 纯色按钮鼠标进入透明度
    ''' </summary>
    Private Const ButtonAlphaActive As Integer = 50
    ''' <summary>
    ''' 纯色按钮鼠标按下透明度
    ''' </summary>
    Private Const ButtonAlphaExecute As Integer = 75
    ''' <summary>
    ''' 纯色按钮默认颜色
    ''' </summary>
    Dim ButtonColorDefault As Color = Color.Orange
    ''' <summary>
    ''' 纯色按钮鼠标进入颜色
    ''' </summary>
    Dim ButtonColorActive As Color = Color.Aqua
    ''' <summary>
    ''' 纯色按钮鼠标按下颜色
    ''' </summary>
    Dim ButtonColorExecute As Color = Color.Red
    ''' <summary>
    ''' 纯色按钮组
    ''' </summary>
    Dim Buttons() As Label
    ''' <summary>
    ''' 内存区域首节点
    ''' </summary>
    Dim FirstMemoryNode As MemoryNodeClass
    ''' <summary>
    ''' 进程列表
    ''' </summary>
    Dim ProcessList As List(Of ProcessClass) = New List(Of ProcessClass)
    ''' <summary>
    ''' 图形化内存区域
    ''' </summary>
    Dim MemoryRectangle As Rectangle
    ''' <summary>
    ''' 图形化内存区域单位高度
    ''' </summary>
    Dim MemoryCellHeight As Double
    ''' <summary>
    ''' 空白内存区域
    ''' </summary>
    Dim FreeMemoryRectangle As Rectangle
    ''' <summary>
    ''' 空白内存区域单位内存对应的图像宽度
    ''' </summary>
    Dim FreeMemoryCellWidth As Double
    ''' <summary>
    ''' 下次创建的进程ID
    ''' </summary>
    Dim NextPID As Integer = 0
    ''' <summary>
    ''' 全局随机数发生器
    ''' </summary>
    Dim UnityRandom As Random = New Random
    ''' <summary>
    ''' 总空闲内存大小
    ''' </summary>
    Dim FreeMemorySize As Integer = Max_MemorySize
    ''' <summary>
    ''' 不分页下次空闲内存节点（依次对应：首次适应、循环首次适应、最佳适应、最坏适应）
    ''' </summary>
    Dim NextFreeMemoryNodes(3) As MemoryNodeClass
    ''' <summary>
    ''' 记录空闲内存的列表-按大小排序
    ''' </summary>
    Dim FreeMemoryListBySize As List(Of MemoryNodeClass) = New List(Of MemoryNodeClass)
    ''' <summary>
    ''' 记录空闲内存的列表-按地址排序
    ''' </summary>
    Dim FreeMemoryListByAddr As List(Of MemoryNodeClass) = New List(Of MemoryNodeClass)
    ''' <summary>
    ''' 分页表（记录使用分页的进程）
    ''' </summary>
    Dim PageTabel(127) As ProcessClass
    ''' <summary>
    ''' 空闲分页数量
    ''' </summary>
    Dim FreePageCount As Integer = 128
    ''' <summary>
    ''' 记录循环首次适应算法上次使用的内存块下标
    ''' </summary>
    Dim LastCFFIndex As Integer = -1
    ''' <summary>
    ''' 记录循环首次适应算法使用的内存块图像坐标
    ''' </summary>
    Dim LastCFFPoint As Point = Point.Empty

#Region "窗体事件"

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Buttons = New Label() {ResetSystemButton, AddProcessButton, DisposeProcessButton, RelocateButton, SegmentPageButton}
        SetStyle(ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)

        Me.Icon = My.Resources.UnityResource.VMSystemICON
        CloseButton.Left = Me.Width - CloseButton.Width

        '设置容器控件的背景颜色
        ControlPanel.BackColor = Color.FromArgb(50, Color.Gray)
        MemoryBackUpPanel.BackColor = Color.FromArgb(50, Color.Gray)
        MemoryManagerPanel.BackColor = Color.FromArgb(50, Color.Gray)
        LogLabel.BackColor = Color.FromArgb(50, Color.Gray)
        FreeMemorySortByAddressPanel.BackColor = Color.FromArgb(50, Color.Gray)
        FreeMemorySortBySizePanel.BackColor = Color.FromArgb(50, Color.Gray)
        LogLabel.BackColor = Color.FromArgb(50, Color.Gray)

        '调整容器控件的位置和尺寸
        ControlPanel.Location = New Point(15, LogoLabel.Bottom + 10)
        ControlPanel.Size = New Size((Me.Width - 50) * 0.2, (Me.Height - ControlPanel.Top - 15) * 0.8)
        MemoryBackUpPanel.Location = New Point(ControlPanel.Right + 10, ControlPanel.Top)
        MemoryBackUpPanel.Size = New Size((Me.Width - 50) * 0.3, ControlPanel.Height)
        MemoryManagerPanel.Location = New Point(MemoryBackUpPanel.Right + 10, ControlPanel.Top)
        MemoryManagerPanel.Size = New Size(MemoryBackUpPanel.Width, ControlPanel.Height)
        LogLabel.Location = New Point(MemoryManagerPanel.Right + 10, ControlPanel.Top)
        LogLabel.Size = New Size(ControlPanel.Width, ControlPanel.Height)
        FreeMemorySortByAddressPanel.Location = New Point(15, ControlPanel.Bottom + 10)
        FreeMemorySortByAddressPanel.Size = New Size(ControlPanel.Width + MemoryManagerPanel.Width + 10, (Me.Height - ControlPanel.Top - 25) * 0.2)
        FreeMemorySortBySizePanel.Location = New Point(FreeMemorySortByAddressPanel.Right + 10, FreeMemorySortByAddressPanel.Top)
        FreeMemorySortBySizePanel.Size = FreeMemorySortByAddressPanel.Size

        '初始化纯色按钮
        For Each InsButton As Label In Buttons
            InsButton.BackColor = Color.FromArgb(ButtonAlphaDefault, ButtonColorDefault)
            InsButton.Parent = ControlPanel
            InsButton.Width = ControlPanel.Width - 30
            AddHandler InsButton.MouseEnter, AddressOf ColorButton_MouseEnter
            AddHandler InsButton.MouseDown, AddressOf ColorButton_MouseDown
            AddHandler InsButton.MouseUp, AddressOf ColorButton_MouseUp
            AddHandler InsButton.MouseLeave, AddressOf ColorButton_MouseLeave
        Next
        AddProcessLabel.Parent = ControlPanel
        ProcessMemorySizeNumeric.Parent = ControlPanel
        ProcessMemorySizeNumeric.Width = ControlPanel.Width - 30

        DisposeProcessLabel.Parent = ControlPanel
        ProcessListComboBox.Parent = ControlPanel
        ProcessListComboBox.Width = ControlPanel.Width - 30

        SegmentPageLabel.Parent = ControlPanel
        DispatchComboBox.Parent = ControlPanel
        DispatchComboBox.Width = ControlPanel.Width - 30
        DispatchComboBox.SelectedIndex = 0

        MemoryRectangle = New Rectangle(15, 35, MemoryManagerPanel.Width - 30, MemoryManagerPanel.Height - 50)
        MemoryCellHeight = MemoryRectangle.Height / Max_MemorySize
        FreeMemoryRectangle = New Rectangle(15, 50, FreeMemorySortByAddressPanel.Width - 30, FreeMemorySortByAddressPanel.Height - 65)

        ResetSystemButton_Click(New Object, New EventArgs)
        AddHandler ProcessMemorySizeNumeric.ValueChanged, AddressOf ProcessMemorySizeNumeric_ValueChanged
    End Sub

#End Region

#Region "标题栏按钮事件"

    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        Me.Hide()
    End Sub

#End Region

#Region "图像按钮动态效果"

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

#Region "纯色按钮事件"

    Private Sub ResetSystemButton_Click(sender As Object, e As EventArgs) Handles ResetSystemButton.Click
        ProcessListComboBox.Items.Clear()
        ProcessList.Clear()
        ReDim PageTabel(UBound(PageTabel))
        FirstMemoryNode = New MemoryNodeClass(Nothing, 0, Max_MemorySize)
        FreeMemoryListBySize = New List(Of MemoryNodeClass)
        NextFreeMemoryNodes = New MemoryNodeClass() {FirstMemoryNode, FirstMemoryNode, FirstMemoryNode, FirstMemoryNode}
        FreeMemoryListBySize.Add(FirstMemoryNode)
        FreeMemoryListByAddr.Add(FirstMemoryNode)
        FreeMemorySize = Max_MemorySize
        FreePageCount = 128
        NextPID = 0
        LastCFFIndex = 0
        SegmentPageButton.Enabled = True
        If DispatchComboBox.Visible Then
            MemoryManagerPanel.Image = CreateMemoryBitmapUnSegmant()
            CreateFreeMemoryBitmap(False) '重置系统
        Else
            MemoryManagerPanel.Image = CreateMemoryBitmapSegmant()
        End If
        MemoryBackUpPanel.Image = MemoryManagerPanel.Image
        GC.Collect()
        LogLabel.TextBox.Text = "系统初始化完毕！" & Now.ToString & vbCrLf
    End Sub

    Private Sub SegmentPageButton_Click(sender As Object, e As EventArgs) Handles SegmentPageButton.Click
        DispatchComboBox.Visible = Not DispatchComboBox.Visible
        Select Case DispatchComboBox.Visible
            Case True
                SegmentPageLabel.Text = "请选择空闲内存选择算法："
                SegmentPageButton.Text = "     禁止分页"
                RelocateButton.Visible = True
                MemoryManagerPanel.Image = CreateMemoryBitmapUnSegmant()
                CreateFreeMemoryBitmap(False) '切换分页
                AddHandler ProcessMemorySizeNumeric.ValueChanged, AddressOf ProcessMemorySizeNumeric_ValueChanged
            Case False
                SegmentPageLabel.Text = "默认分页大小：8"
                SegmentPageButton.Text = "     允许分页"
                RelocateButton.Visible = False
                MemoryManagerPanel.Image = CreateMemoryBitmapSegmant()
                FreeMemorySortByAddressPanel.Image = Nothing
                FreeMemorySortBySizePanel.Image = Nothing
                RemoveHandler ProcessMemorySizeNumeric.ValueChanged, AddressOf ProcessMemorySizeNumeric_ValueChanged
        End Select
        MemoryBackUpPanel.Image = MemoryManagerPanel.Image

        'LogLabel.TextBox.Text &= "切换分页模式：" & IIf(DispatchComboBox.Visible, "不", vbNullString) & "分页" & vbCrLf
        GC.Collect()
    End Sub

    Private Sub AddProcessButton_Click(sender As Object, e As EventArgs) Handles AddProcessButton.Click
        If SegmentPageButton.Enabled Then SegmentPageButton.Enabled = False

        Select Case DispatchComboBox.Visible
            Case True '不分页
                AddProcessUnSegment()
            Case False '分页
                AddProcessSegment()
        End Select

        NextPID += 1
        GC.Collect()
    End Sub

    Private Sub DisposeProcessButton_Click(sender As Object, e As EventArgs) Handles DisposeProcessButton.Click
        If ProcessListComboBox.SelectedIndex = -1 Then Exit Sub

        Select Case DispatchComboBox.Visible
            Case True '不分页
                DisposeProcessUnSegment()
            Case False '分页
                DisposeProcessSegment()
        End Select

        GC.Collect()
    End Sub

    Private Sub RelocateButton_Click(sender As Object, e As EventArgs) Handles RelocateButton.Click
        Dim InsMemoryNode As MemoryNodeClass = FirstMemoryNode
        Do While True
            If IsNothing(InsMemoryNode.Process) Then
                If Not IsNothing(InsMemoryNode.LastNode) Then
                    '消除两个节点中间的空节点
                    InsMemoryNode.LastNode.NextNode = InsMemoryNode.NextNode
                    InsMemoryNode.NextNode.LastNode = InsMemoryNode.LastNode
                    InsMemoryNode.NextNode.StartPoint = InsMemoryNode.LastNode.StartPoint + InsMemoryNode.LastNode.Size
                    InsMemoryNode = InsMemoryNode.NextNode
                Else
                    '首节点为空
                    If Not IsNothing(InsMemoryNode.NextNode) Then
                        InsMemoryNode.NextNode.StartPoint = 0
                        InsMemoryNode = InsMemoryNode.NextNode
                        FirstMemoryNode = InsMemoryNode
                        InsMemoryNode.LastNode = Nothing
                    End If
                End If
            Else
                '重定位非空节点的地址
                If InsMemoryNode.Size <> InsMemoryNode.Process.Size Then
                    FreeMemorySize += InsMemoryNode.Size - InsMemoryNode.Process.Size
                    InsMemoryNode.Size = InsMemoryNode.Process.Size
                End If
                If Not IsNothing(InsMemoryNode.LastNode) Then
                    InsMemoryNode.StartPoint = InsMemoryNode.LastNode.StartPoint + InsMemoryNode.LastNode.Size
                End If
                InsMemoryNode = InsMemoryNode.NextNode
            End If

            If IsNothing(InsMemoryNode.NextNode) Then
                '处理最后一个节点
                If IsNothing(InsMemoryNode.Process) Then
                    If Not IsNothing(InsMemoryNode.LastNode) Then InsMemoryNode.StartPoint = InsMemoryNode.LastNode.StartPoint + InsMemoryNode.LastNode.Size
                    InsMemoryNode.Size = FreeMemorySize
                Else
                    If InsMemoryNode.Size <> InsMemoryNode.Process.Size Then
                        FreeMemorySize += InsMemoryNode.Size - InsMemoryNode.Process.Size
                        InsMemoryNode.Size = InsMemoryNode.Process.Size
                        Dim LastFreeMemoryNode As MemoryNodeClass = New MemoryNodeClass(Nothing, InsMemoryNode.StartPoint + InsMemoryNode.Size, FreeMemorySize)
                        InsMemoryNode.NextNode = LastFreeMemoryNode
                        LastFreeMemoryNode.LastNode = InsMemoryNode
                    End If
                    If Not IsNothing(InsMemoryNode.LastNode) Then
                        InsMemoryNode.StartPoint = InsMemoryNode.LastNode.StartPoint + InsMemoryNode.LastNode.Size
                    End If
                End If
                Exit Do
            End If
        Loop
        LastCFFIndex = 0
        LogLabel.TextBox.Text &= "重定位内存！" & Now.ToString & vbCrLf
        MemoryBackUpPanel.Image = MemoryManagerPanel.Image
        MemoryManagerPanel.Image = CreateMemoryBitmapUnSegmant()
        CreateFreeMemoryBitmap(False) '重定位

        GC.Collect()
    End Sub

#End Region

#Region "功能函数"

    ''' <summary>
    ''' 不分页 载入进程
    ''' </summary>
    Private Sub AddProcessUnSegment()
        On Error Resume Next
        If FreeMemorySize < ProcessMemorySizeNumeric.Value Then
            LogLabel.TextBox.Text &= "内存不足，无法载入 " & ProcessMemorySizeNumeric.Value & Now.ToString & vbCrLf
            MsgBox("内存不足！")
            Exit Sub
        End If

        Dim NextFreeMemoryNode As MemoryNodeClass = NextFreeMemoryNodes(DispatchComboBox.SelectedIndex)
        Dim InsProcess As ProcessClass = New ProcessClass(NextPID, "进程-" & NextPID, ProcessMemorySizeNumeric.Value, Color.FromArgb(UnityRandom.Next(256), UnityRandom.Next(256), UnityRandom.Next(256)))

        If IsNothing(NextFreeMemoryNode) Then
            MsgBox("需要重定位！")
            Exit Sub
        End If

        LogLabel.TextBox.Text &= "载入 " & InsProcess.Name & " / " & InsProcess.Size & Now.ToString & vbCrLf
        If NextFreeMemoryNode.Size - InsProcess.Size < ProcessMemorySizeNumeric.Minimum Then
            '空闲内存块载入进程后剩余小于最小碎片大小，不生成新的内存块
            NextFreeMemoryNode.Process = InsProcess
            FreeMemorySize -= NextFreeMemoryNode.Size
            LogLabel.TextBox.Text &= "    空闲内存剩余较小" & vbCrLf & "   不生成新的空闲内存" & vbCrLf
        Else
            '空闲内存块载入进程后剩余大于最小碎片大小，生成新的内存块
            Dim InsMemoryNode As MemoryNodeClass = New MemoryNodeClass(InsProcess, NextFreeMemoryNode.StartPoint, InsProcess.Size)
            FreeMemorySize -= InsProcess.Size
            InsMemoryNode.NextNode = NextFreeMemoryNode
            If IsNothing(NextFreeMemoryNode.LastNode) Then
                FirstMemoryNode = InsMemoryNode
            Else
                InsMemoryNode.LastNode = NextFreeMemoryNode.LastNode
                NextFreeMemoryNode.LastNode.NextNode = InsMemoryNode
            End If
            LogLabel.TextBox.Text &= "    空闲内存剩余较大" & vbCrLf & "   生成新的空闲内存" & vbCrLf
            NextFreeMemoryNode.LastNode = InsMemoryNode
            NextFreeMemoryNode.Size -= InsMemoryNode.Size
            NextFreeMemoryNode.StartPoint += InsMemoryNode.Size
        End If

        MemoryBackUpPanel.Image = MemoryManagerPanel.Image
        MemoryManagerPanel.Image = CreateMemoryBitmapUnSegmant()
        CreateFreeMemoryBitmap(True) '载入进程-不分页

        ProcessList.Add(InsProcess)
        ProcessListComboBox.Items.Add(InsProcess.Name)
    End Sub

    ''' <summary>
    ''' 分页 载入进程
    ''' </summary>
    Private Sub AddProcessSegment()
        On Error Resume Next
        Dim PageSize As Integer = 8
        Dim PageCount As Double
        PageCount = ProcessMemorySizeNumeric.Value / PageSize
        PageCount = Fix(PageCount) - (Fix(PageCount) < PageCount)
        If FreePageCount < PageCount Then MsgBox("内存不足！") : Exit Sub
        FreePageCount -= PageCount
        Dim InsProcess As ProcessClass = New ProcessClass(NextPID, "进程-" & NextPID, ProcessMemorySizeNumeric.Value, Color.FromArgb(UnityRandom.Next(256), UnityRandom.Next(256), UnityRandom.Next(256)))
        LogLabel.TextBox.Text &= "载入 " & InsProcess.Name & " / " & InsProcess.Size & "   分页数：" & PageCount & vbCrLf & Now.ToString & vbCrLf

        For Index As Integer = 0 To UBound(PageTabel)
            If IsNothing(PageTabel(Index)) Then
                PageTabel(Index) = InsProcess
                InsProcess.PageList.Add(Index)
            End If
            If InsProcess.PageList.Count = PageCount Then Exit For
        Next

        ProcessList.Add(InsProcess)
        ProcessListComboBox.Items.Add(InsProcess.Name)

        MemoryBackUpPanel.Image = MemoryManagerPanel.Image
        MemoryManagerPanel.Image = CreateMemoryBitmapSegmant()
    End Sub

    ''' <summary>
    ''' 不分页 释放进程
    ''' </summary>
    Private Sub DisposeProcessUnSegment()
        On Error Resume Next
        FreeMemorySize += ProcessList(ProcessListComboBox.SelectedIndex).Size
        Dim InsMemoryNode As MemoryNodeClass = FirstMemoryNode
        Do While True
            '寻找进程关联的内存并释放内存
            If Not IsNothing(InsMemoryNode.Process) AndAlso InsMemoryNode.Process.Name = ProcessListComboBox.Text Then
                LogLabel.TextBox.Text &= "释放 " & InsMemoryNode.Process.Name & Now.ToString & vbCrLf
                InsMemoryNode.Process = Nothing
                If InsMemoryNode.StartPoint < FreeMemoryListByAddr(LastCFFIndex).StartPoint Then LastCFFIndex += 1
                '合并空闲分区
                If Not IsNothing(InsMemoryNode.LastNode) AndAlso IsNothing(InsMemoryNode.LastNode.Process) Then
                    LogLabel.TextBox.Text &= "    向上合并空闲内存" & vbCrLf
                    InsMemoryNode.LastNode.Size += InsMemoryNode.Size
                    InsMemoryNode.LastNode.NextNode = InsMemoryNode.NextNode
                    InsMemoryNode.NextNode.LastNode = InsMemoryNode.LastNode
                    InsMemoryNode = InsMemoryNode.LastNode
                    If InsMemoryNode.StartPoint < FreeMemoryListByAddr(LastCFFIndex).StartPoint Then LastCFFIndex -= 1
                End If
                If Not IsNothing(InsMemoryNode.NextNode) AndAlso IsNothing(InsMemoryNode.NextNode.Process) Then
                    LogLabel.TextBox.Text &= "    向下合并空闲内存" & vbCrLf
                    InsMemoryNode.Size += InsMemoryNode.NextNode.Size
                    InsMemoryNode.NextNode = InsMemoryNode.NextNode.NextNode
                    If Not IsNothing(InsMemoryNode.NextNode) Then
                        InsMemoryNode.NextNode.LastNode = InsMemoryNode
                    End If
                    If InsMemoryNode.StartPoint < FreeMemoryListByAddr(LastCFFIndex).StartPoint Then LastCFFIndex -= 1
                End If
                Exit Do
            End If
            If IsNothing(InsMemoryNode.NextNode) Then Exit Do
            InsMemoryNode = InsMemoryNode.NextNode
        Loop

        ProcessList.RemoveAt(ProcessListComboBox.SelectedIndex)
        ProcessListComboBox.Items.RemoveAt(ProcessListComboBox.SelectedIndex)

        MemoryBackUpPanel.Image = MemoryManagerPanel.Image
        MemoryManagerPanel.Image = CreateMemoryBitmapUnSegmant()
        CreateFreeMemoryBitmap(False) '释放进程
    End Sub

    ''' <summary>
    ''' 分页 释放进程
    ''' </summary>
    Private Sub DisposeProcessSegment()
        On Error Resume Next
        Dim InsProcess As ProcessClass = ProcessList(ProcessListComboBox.SelectedIndex)
        FreePageCount += InsProcess.PageList.Count

        LogLabel.TextBox.Text &= "释放 " & InsProcess.Name & "   分页数：" & InsProcess.PageList.Count & vbCrLf & Now.ToString & vbCrLf
        For Index As Integer = 0 To UBound(PageTabel)
            If PageTabel(Index) Is InsProcess Then
                PageTabel(Index) = Nothing
            End If
            If InsProcess.PageList.Count = 0 Then Exit For
        Next

        ProcessList.RemoveAt(ProcessListComboBox.SelectedIndex)
        ProcessListComboBox.Items.RemoveAt(ProcessListComboBox.SelectedIndex)

        MemoryBackUpPanel.Image = MemoryManagerPanel.Image
        MemoryManagerPanel.Image = CreateMemoryBitmapSegmant()
    End Sub

    ''' <summary>
    ''' 创建内存可视化图像-不分页
    ''' </summary>
    ''' <returns></returns>
    Private Function CreateMemoryBitmapUnSegmant() As Bitmap
        On Error Resume Next
        Dim UnityBitmap As Bitmap = New Bitmap(MemoryManagerPanel.Width, MemoryManagerPanel.Height)
        Dim UnityBrush As SolidBrush, UnityPen As Pen
        Dim UnityPoint As Point = Point.Empty
        Dim UnitySize As Size = Size.Empty
        Dim InsMemoryNode As MemoryNodeClass = FirstMemoryNode
        Using UnityGraphics As Graphics = Graphics.FromImage(UnityBitmap)
            Do While True
                UnityPoint = New Point(MemoryRectangle.Left, CInt(MemoryRectangle.Top + InsMemoryNode.StartPoint * MemoryCellHeight))
                UnitySize = New Size(MemoryRectangle.Width, CInt(MemoryCellHeight * InsMemoryNode.Size))
                If IsNothing(InsMemoryNode.Process) Then
                    UnityBrush = New SolidBrush(Color.FromArgb(150, Color.Gray))
                    UnityGraphics.FillRectangle(UnityBrush, UnityPoint.X, UnityPoint.Y, UnitySize.Width, UnitySize.Height)
                    UnityBrush.Color = Color.FromArgb(200, Color.White)
                    UnityGraphics.DrawString(String.Format(" *空闲内存 Addr:{0},Size:{1}", InsMemoryNode.StartPoint, InsMemoryNode.Size), Me.Font, UnityBrush, UnityPoint)
                Else
                    UnityBrush = New SolidBrush(Color.FromArgb(150, InsMemoryNode.Process.Color))
                    UnityGraphics.FillRectangle(UnityBrush, UnityPoint.X, UnityPoint.Y, UnitySize.Width, UnitySize.Height)
                    UnityBrush.Color = Color.Gold
                    UnityGraphics.DrawString(String.Format("{0} Addr:{1},Size:{2}{3}", InsMemoryNode.Process.Name, InsMemoryNode.StartPoint, InsMemoryNode.Size, IIf(InsMemoryNode.Size = InsMemoryNode.Process.Size, "", ",进程大小:" & InsMemoryNode.Process.Size)), Me.Font, UnityBrush, UnityPoint)
                End If

                If IsNothing(InsMemoryNode.NextNode) Then Exit Do

                InsMemoryNode = InsMemoryNode.NextNode
            Loop

            UnityPen = New Pen(Color.FromArgb(100, Color.White), 1)
            UnityGraphics.DrawRectangle(UnityPen, MemoryRectangle)
        End Using
        Return UnityBitmap
    End Function

    ''' <summary>
    ''' 创建内存可视化图像-分页
    ''' </summary>
    ''' <returns></returns>
    Private Function CreateMemoryBitmapSegmant() As Bitmap
        On Error Resume Next
        Dim UnityBitmap As Bitmap = New Bitmap(MemoryManagerPanel.Width, MemoryManagerPanel.Height)
        Dim UnityBrush As SolidBrush = New SolidBrush(Color.White), UnityPen As Pen = New Pen(UnityBrush, 1)
        Dim UnityPoint As Point = Point.Empty
        Dim PageSize As SizeF = New SizeF((MemoryRectangle.Width - 35) / 8, (MemoryRectangle.Height - 75) / 16)
        Dim PageIndex As Integer = 0
        Using UnityGraphics As Graphics = Graphics.FromImage(UnityBitmap)
            For Y As Integer = 0 To 15
                For X As Integer = 0 To 7
                    UnityPoint = New Point(MemoryRectangle.Left + X * (PageSize.Width + 5), MemoryRectangle.Top + Y * (PageSize.Height + 5))
                    If IsNothing(PageTabel(PageIndex)) Then
                        UnityBrush.Color = Color.FromArgb(150, Color.Gray)
                        UnityGraphics.FillRectangle(UnityBrush, UnityPoint.X, UnityPoint.Y, PageSize.Width, PageSize.Height)
                    Else
                        UnityBrush.Color = Color.FromArgb(200, PageTabel(PageIndex).Color)
                        UnityGraphics.FillRectangle(UnityBrush, UnityPoint.X, UnityPoint.Y, PageSize.Width, PageSize.Height)
                        UnityPen.Color = Color.FromArgb(200, Color.Gold)
                        UnityGraphics.DrawRectangle(UnityPen, UnityPoint.X, UnityPoint.Y, PageSize.Width, PageSize.Height)
                        UnityBrush.Color = Color.Gold
                        UnityGraphics.DrawString(PageTabel(PageIndex).Name, Me.Font, UnityBrush, UnityPoint.X + 1, UnityPoint.Y + 3)
                    End If
                    PageIndex += 1
                Next
            Next

            UnityPen = New Pen(Color.FromArgb(100, Color.White), 1)
            UnityGraphics.DrawRectangle(UnityPen, MemoryRectangle)
        End Using
        Return UnityBitmap
    End Function

    ''' <summary>
    ''' 创建空闲内存区域链表图像
    ''' </summary>
    Private Sub CreateFreeMemoryBitmap(GetNextCFFNode As Boolean)
        On Error Resume Next
        ReDim NextFreeMemoryNodes(3)
        Dim UnityBitmapSortByAddress As Bitmap = New Bitmap(FreeMemorySortByAddressPanel.Width, FreeMemorySortByAddressPanel.Height)
        Dim UnityBitmapSortBySize As Bitmap = New Bitmap(FreeMemorySortBySizePanel.Width, FreeMemorySortBySizePanel.Height)
        Dim UnityBrush As SolidBrush = New SolidBrush(Color.FromArgb(120, Color.Gray)),
                    UnityPen As Pen = New Pen(Color.FromArgb(150, Color.White), 1)
        Dim UnityPoint As Point = Point.Empty
        Dim UnitySize As Size = Size.Empty
        Dim IndexList As Integer
        Dim InsMemoryNode As MemoryNodeClass = FirstMemoryNode
        Dim LastX As Integer = FreeMemoryRectangle.Left

        FreeMemoryCellWidth = FreeMemoryRectangle.Width / FreeMemorySize
        FreeMemoryListBySize.Clear()
        FreeMemoryListByAddr.Clear()

        Using UnityGraphics As Graphics = Graphics.FromImage(UnityBitmapSortByAddress)
            Do While True
                If IsNothing(InsMemoryNode.Process) Then
                    UnityPoint = New Point(LastX, FreeMemoryRectangle.Top)
                    UnitySize = New Size(InsMemoryNode.Size * FreeMemoryCellWidth, FreeMemoryRectangle.Height)
                    LastX += UnitySize.Width
                    UnityBrush = New SolidBrush(Color.FromArgb(120, Color.Gray))
                    UnityGraphics.FillRectangle(UnityBrush, UnityPoint.X, UnityPoint.Y, UnitySize.Width, UnitySize.Height)
                    UnityBrush.Color = Color.FromArgb(200, Color.White)
                    UnityGraphics.DrawLine(UnityPen, UnityPoint.X, FreeMemoryRectangle.Top, UnityPoint.X, FreeMemoryRectangle.Bottom)
                    UnityGraphics.DrawString(String.Format("空闲区-Addr:- {0}-Size:- {1}".Replace("-", vbCrLf), InsMemoryNode.StartPoint, InsMemoryNode.Size), Me.Font, UnityBrush, UnityPoint)

                    If IsNothing(NextFreeMemoryNodes(0)) AndAlso InsMemoryNode.Size >= ProcessMemorySizeNumeric.Value Then
                        NextFreeMemoryNodes(0) = InsMemoryNode
                        UnityPoint.Offset(0, -15)
                        UnityBrush.Color = Color.FromArgb(200, Color.Orange)
                        LastCFFPoint = New Point(UnityPoint.X, UnityPoint.Y - 15)
                        UnityGraphics.DrawString("首次适应", Me.Font, UnityBrush, UnityPoint)
                    End If

                    For IndexList = 0 To FreeMemoryListBySize.Count - 1
                        If InsMemoryNode.Size > FreeMemoryListBySize(IndexList).Size Then Exit For
                    Next
                    FreeMemoryListBySize.Insert(IndexList, InsMemoryNode)
                    FreeMemoryListByAddr.Add(InsMemoryNode)
                End If

                If IsNothing(InsMemoryNode.NextNode) Then Exit Do
                InsMemoryNode = InsMemoryNode.NextNode
            Loop

            If Not IsNothing(NextFreeMemoryNodes(0)) AndAlso FreeMemoryListByAddr.Count > 0 Then
                LastX = FreeMemoryRectangle.Left
                IndexList = 0
                Do While True
                    If LastCFFIndex >= FreeMemoryListByAddr.Count Then LastCFFIndex = 0
                    InsMemoryNode = FreeMemoryListByAddr(LastCFFIndex)
                    If InsMemoryNode.Size >= ProcessMemorySizeNumeric.Value Then
                        Exit Do
                    End If
                    If GetNextCFFNode Or InsMemoryNode.Size < ProcessMemorySizeNumeric.Value Then LastCFFIndex += 1
                Loop

                For IndexList = 0 To LastCFFIndex - 1
                    LastX += FreeMemoryListByAddr(LastCFFIndex).Size * FreeMemoryCellWidth
                Next
                LastCFFPoint = New Point(LastX, 20)
                NextFreeMemoryNodes(1) = InsMemoryNode
                UnityBrush.Color = Color.FromArgb(200, Color.Orange)
                UnityGraphics.DrawString("循环首次适应", Me.Font, UnityBrush, LastCFFPoint)
            End If

            UnityPen = New Pen(Color.FromArgb(100, Color.White), 1)
            UnityGraphics.DrawRectangle(UnityPen, FreeMemoryRectangle)
        End Using
        FreeMemorySortByAddressPanel.Image = UnityBitmapSortByAddress

        LastX = FreeMemoryRectangle.Left
        Using UnityGraphics As Graphics = Graphics.FromImage(UnityBitmapSortBySize)
            For Index As Integer = 0 To FreeMemoryListBySize.Count - 1
                InsMemoryNode = FreeMemoryListBySize(Index)
                UnityPoint = New Point(LastX, FreeMemoryRectangle.Top)
                UnitySize = New Size(InsMemoryNode.Size * FreeMemoryCellWidth, FreeMemoryRectangle.Height)
                LastX += UnitySize.Width
                UnityBrush = New SolidBrush(Color.FromArgb(120, Color.Gray))
                UnityGraphics.FillRectangle(UnityBrush, UnityPoint.X, UnityPoint.Y, UnitySize.Width, UnitySize.Height)
                UnityBrush.Color = Color.FromArgb(200, Color.White)
                UnityGraphics.DrawLine(UnityPen, UnityPoint.X, FreeMemoryRectangle.Top, UnityPoint.X, FreeMemoryRectangle.Bottom)
                UnityGraphics.DrawString(String.Format("空闲区-Addr:- {0}-Size:- {1}".Replace("-", vbCrLf), InsMemoryNode.StartPoint, InsMemoryNode.Size), Me.Font, UnityBrush, UnityPoint)

                If (IsNothing(NextFreeMemoryNodes(3)) AndAlso FreeMemoryListBySize(Index).Size >= ProcessMemorySizeNumeric.Value) Then
                    NextFreeMemoryNodes(3) = FreeMemoryListBySize(Index)
                    UnityBrush.Color = Color.FromArgb(200, Color.Orange)
                    UnityPoint.Offset(0, -15)
                    UnityGraphics.DrawString("最坏适应", Me.Font, UnityBrush, UnityPoint)
                End If
            Next
            If Not IsNothing(NextFreeMemoryNodes(3)) Then
                LastX = FreeMemoryRectangle.Right
                For Index As Integer = FreeMemoryListBySize.Count - 1 To 0 Step -1
                    InsMemoryNode = FreeMemoryListBySize(Index)
                    LastX -= InsMemoryNode.Size * FreeMemoryCellWidth
                    If FreeMemoryListBySize(Index).Size >= ProcessMemorySizeNumeric.Value Then
                        UnityPoint = New Point(LastX, FreeMemoryRectangle.Top - 30)
                        UnityBrush.Color = Color.FromArgb(200, Color.Orange)
                        NextFreeMemoryNodes(2) = FreeMemoryListBySize(Index)
                        UnityGraphics.DrawString("最佳适应", Me.Font, UnityBrush, UnityPoint)
                        Exit For
                    End If
                Next
            End If

            UnityPen = New Pen(Color.FromArgb(100, Color.White), 1)
            UnityGraphics.DrawRectangle(UnityPen, FreeMemoryRectangle)
        End Using
        FreeMemorySortBySizePanel.Image = UnityBitmapSortBySize
    End Sub

    Private Sub ProcessMemorySizeNumeric_ValueChanged(sender As Object, e As EventArgs)
        CreateFreeMemoryBitmap(False) '下个进程大小改变
    End Sub

#End Region

End Class
