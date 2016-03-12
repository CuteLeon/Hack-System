Public Class XYMail
    Dim NormalColor As Color = Color.FromArgb(19, 132, 205)
    Dim FailColor As Color = Color.FromArgb(248, 97, 97)

    Private Sub Btn_Send_Click(sender As Object, e As EventArgs) Handles Btn_Send.Click
        On Error GoTo MyERR '容错处理
        ReturnInfo.ForeColor = NormalColor
        ReturnInfo.Text = "Mail Sending ..."
        Application.DoEvents()
        '创建SMTP连接和MAIL对象
        Dim Smtp As New System.Net.Mail.SmtpClient("smtp.yeah.net", 25)
        Smtp.UseDefaultCredentials = True
        Smtp.Credentials = New System.Net.NetworkCredential("HackSystem", "HackSystem123") '"I'mHackSystem")
        Dim Mail As New System.Net.Mail.MailMessage()
        Mail.From = New System.Net.Mail.MailAddress("HackSystem@yeah.net")
        Mail.To.Add(Txt_ToAddress.Text)
        Mail.SubjectEncoding = System.Text.Encoding.GetEncoding("GB2312")
        Mail.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312")
        Mail.Priority = System.Net.Mail.MailPriority.Normal
        Mail.Subject = Txt_MailTitle.Text
        '邮件内容支持HTML格式
        Mail.IsBodyHtml = True
        'HTML格式需要改变换行符vbCrLf为<br>
        Mail.Body = Replace(Txt_MailBody.Text, vbCrLf, "<br>") & "<br><br><i><small>    ——Form：Hack System (V " & Application.ProductVersion & ")</small></i>"
        '发送邮件
        Smtp.Send(Mail)
        Mail.Dispose()
        ReturnInfo.Text = "Sent Successfully"
        Exit Sub

MyERR:
        '捕获到异常
        Beep()
        ReturnInfo.ForeColor = FailColor
        ReturnInfo.Text = "Failed! Error: " & Err.Number
    End Sub

    Private Sub MoveWindow(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        '鼠标通过控件拖动窗体
        WindowsTemplates.ReleaseCapture()
        WindowsTemplates.SendMessageA(Me.Handle, &HA1, 2, 0&)
    End Sub

#Region "发送按钮响应鼠标动态效果"
    Private Sub Btn_Send_MouseDown(sender As Object, e As MouseEventArgs) Handles Btn_Send.MouseDown
        Btn_Send.Image = My.Resources.SystemAssets.SendBtn_2
    End Sub

    Private Sub Btn_Send_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Send.MouseEnter
        Btn_Send.Image = My.Resources.SystemAssets.SendBtn_1
    End Sub

    Private Sub Btn_Send_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Send.MouseLeave
        Btn_Send.Image = My.Resources.SystemAssets.SendBtn_0
    End Sub

    Private Sub Btn_Send_MouseUp(sender As Object, e As MouseEventArgs) Handles Btn_Send.MouseUp
        Btn_Send.Image = My.Resources.SystemAssets.SendBtn_1
    End Sub
#End Region

    Private Sub Txt_ToAddress_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ToAddress.KeyPress
        '发信人地址栏响应回车键
        If Asc(e.KeyChar) = 13 Then Btn_Send_Click(New Object, New EventArgs)
    End Sub

    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        Me.Hide()
    End Sub

    '获取IP和真实地址
    Private Sub GetIPAndAddress(ByRef IP As String, ByRef Address As String)
        'Not My.Computer.Network.IsAvailable'网络未连接
        Dim IPWebClient As Net.WebClient = New Net.WebClient
        Dim WebString As String = vbNullString
        Dim RegIP As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex("\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}")
        Try
            IPWebClient.Encoding = System.Text.Encoding.UTF8
            WebString = IPWebClient.DownloadString("http://ip.chinaz.com/getip.aspx")
            IP = RegIP.Match(WebString).ToString
            Address = Strings.Mid(WebString, IP.Length + 17, WebString.Length - IP.Length - 18)
        Catch ex As Exception
            IP = "Unknown" : Address = "Unknown"
        Finally
            If Not IPWebClient Is Nothing Then IPWebClient.Dispose()
        End Try
    End Sub

#Region "关闭按钮响应鼠标动态效果"
    Private Sub Btn_Close_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Close.MouseEnter
        Btn_Close.Image = My.Resources.XYBrowserRes.Close_E
    End Sub

    Private Sub Btn_Close_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Close.MouseLeave
        Btn_Close.Image = My.Resources.XYBrowserRes.Close_N
    End Sub

    Private Sub Btn_Close_MouseDown(sender As Object, e As MouseEventArgs) Handles Btn_Close.MouseDown
        Btn_Close.Image = My.Resources.XYBrowserRes.Close_D
    End Sub

    Private Sub Btn_Close_MouseUp(sender As Object, e As MouseEventArgs) Handles Btn_Close.MouseUp
        Btn_Close.Image = My.Resources.XYBrowserRes.Close_E
    End Sub

    Private Sub XYMail_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible Then
            ReturnInfo.ForeColor = NormalColor
            ReturnInfo.Text = "Mail function is ready."
        End If
    End Sub
#End Region

End Class
