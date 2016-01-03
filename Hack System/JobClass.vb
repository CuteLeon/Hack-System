Public Class JobClass
    ''' <summary>
    ''' 作业ID
    ''' </summary>
    Public ID As Integer
    ''' <summary>
    ''' 作业名称
    ''' </summary>
    Public Name As String
    ''' <summary>
    ''' 作业到达时间
    ''' </summary>
    Public StartTime As Integer
    ''' <summary>
    ''' 作业结束时间
    ''' </summary>
    Public EndTime As Integer
    ''' <summary>
    ''' 作业执行时间长度
    ''' </summary>
    Public TimeLength As Integer
    ''' <summary>
    ''' 作业对应线条颜色
    ''' </summary>
    Public Color As Color
    ''' <summary>
    ''' 作业优先级
    ''' </summary>
    Public Priority As Integer
    ''' <summary>
    ''' 作业已经执行的时间片
    ''' </summary>
    Public TimeSlice As Integer

    ''' <summary>
    ''' 作业对象构建函数
    ''' </summary>
    ''' <param name="jID">作业ID</param>
    ''' <param name="jName">作业名称</param>
    ''' <param name="jStartTime">作业到达时间</param>
    ''' <param name="jEndTime">作业预计结束时间</param>
    ''' <param name="jColor">作业标记颜色</param>
    ''' <param name="jPriority">作业优先级（越大越优先）</param>
    Public Sub New(ByVal jID As Integer, ByVal jName As String, ByVal jStartTime As Integer, ByVal jEndTime As Integer, ByVal jColor As Color, ByVal jPriority As Integer)
        ID = jID
        Name = jName
        StartTime = jStartTime
        EndTime = jEndTime
        TimeLength = EndTime - StartTime
        Color = jColor
        Priority = jPriority
    End Sub
End Class
