Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.Devices

Namespace My
    ' 以下事件可用于 MyApplication: 
    ' Startup: 应用程序启动时在创建启动窗体之前引发。
    ' Shutdown: 在关闭所有应用程序窗体后引发。 如果应用程序异常终止，则不会引发此事件。
    ' UnhandledException: 在应用程序遇到未经处理的异常时引发。
    ' StartupNextInstance: 在启动单实例应用程序且应用程序已处于活动状态时引发。
    ' NetworkAvailabilityChanged: 在连接或断开网络连接时引发。
    Partial Friend Class MyApplication
        Private Sub MyApplication_NetworkAvailabilityChanged(sender As Object, e As NetworkAvailableEventArgs) Handles Me.NetworkAvailabilityChanged
            '重新联接网络后自动读取IP和城市定位
            If My.Computer.Network.IsAvailable Then SystemWorkStation.GetIPAndAddress(True)
        End Sub

        Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
            If e.CommandLine.Count = 0 Then Exit Sub
            '命令行参数为"reset"时重置存档
            If InStr(e.CommandLine.First.ToLower, "reset") Then
                My.Settings.Reset()
                MsgBox("配置已重置，请重新运行！", MsgBoxStyle.Information, "Hack-System :")
                End
            End If
        End Sub
    End Class
End Namespace
