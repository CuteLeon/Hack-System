Imports Microsoft.VisualBasic.Devices

Namespace My
    ' 以下事件可用于 MyApplication: 
    ' Startup: 应用程序启动时在创建启动窗体之前引发。
    ' Shutdown: 在关闭所有应用程序窗体后引发。 如果应用程序异常终止，则不会引发此事件。
    ' UnhandledException: 在应用程序遇到未经处理的异常时引发。
    ' StartupNextInstance: 在启动单实例应用程序且应用程序已处于活动状态时引发。
    ' NetworkAvailabilityChanged: 在连接或断开网络连接时引发。
    Partial Friend Class MyApplication
        'Private Sub MyApplication_NetworkAvailabilityChanged(sender As Object, e As NetworkAvailableEventArgs) Handles Me.NetworkAvailabilityChanged
        '    '重新联接网络后自动读取IP和城市定位
        '    If My.Computer.Network.IsAvailable Then SystemWorkStation.GetIPAndAddress()
        'End Sub
    End Class
End Namespace
