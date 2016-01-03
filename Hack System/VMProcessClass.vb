Public Class ProcessClass
    ''' <summary>
    ''' 进程PID
    ''' </summary>
    Public ID As Integer
    ''' <summary>
    ''' 进程名称
    ''' </summary>
    Public Name As String
    ''' <summary>
    ''' 进程内存大小
    ''' </summary>
    Public Size As Integer
    ''' <summary>
    ''' 进程分页信息表
    ''' </summary>
    Public PageList As List(Of Integer) = New List(Of Integer)
    ''' <summary>
    ''' 进程标识颜色
    ''' </summary>
    Public Color As Color

    ''' <summary>
    ''' 创建新进程
    ''' </summary>
    ''' <param name="pID">进程ID</param>
    ''' <param name="pName">进程名称</param>
    ''' <param name="pColor">进程颜色</param>
    Public Sub New(pID As Integer, pName As String, pSize As Integer, pColor As Color)
        ID = pID
        Size = pSize
        Name = pName
        Color = pColor
    End Sub
End Class
