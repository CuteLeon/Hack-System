Public Class MemoryNodeClass
    ''' <summary>
    ''' 当前占用内存的进程
    ''' </summary>
    Public Process As ProcessClass
    ''' <summary>
    ''' 内存块开始地址
    ''' </summary>
    Public StartPoint As Integer
    ''' <summary>
    ''' 内存块大小
    ''' </summary>
    Public Size As Integer
    ''' <summary>
    ''' 当前块的前一节点
    ''' </summary>
    Public LastNode As MemoryNodeClass
    ''' <summary>
    ''' 当前块的后一节点
    ''' </summary>
    Public NextNode As MemoryNodeClass

    ''' <summary>
    ''' 创建新的内存节点
    ''' </summary>
    ''' <param name="pProcess">占用内存的进程</param>
    ''' <param name="pStartPoint">内存节点开始地址</param>
    ''' <param name="pSize">内存块大小</param>
    Public Sub New(pProcess As ProcessClass, pStartPoint As Integer, pSize As Integer)
        Process = pProcess
        StartPoint = pStartPoint
        Size = pSize
    End Sub

End Class
