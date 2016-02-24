Public Class XYMail

    Private Sub Btn_Send_Click(sender As Object, e As EventArgs) Handles Btn_Send.Click
        On Error GoTo MyERR
        '创建SMTP连接和MAIL对象
        Dim Smtp As New System.Net.Mail.SmtpClient("smtp.***.com", 25)
        Smtp.Credentials = New System.Net.NetworkCredential("******", "******")
        Dim Mail As New System.Net.Mail.MailMessage()
        Mail.From = New System.Net.Mail.MailAddress("******@***.COM")
        Mail.To.Add(Txt_ToAddress.Text)
        Mail.SubjectEncoding = System.Text.Encoding.GetEncoding("GB2312")
        Mail.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312")
        Mail.Priority = System.Net.Mail.MailPriority.Normal
        Mail.Subject = Txt_MailTitle.Text
        '邮件内容支持HTML格式
        Mail.IsBodyHtml = True
        'HTML格式需要改变换行符vbCrLf为<br>
        Mail.Body = Replace(Txt_MailBody.Text, vbCrLf, "<br>") & "<br><br><i><small>    ——来自：Hack System (V " & Application.ProductVersion & ")</small></i>"
        '发送邮件
        Smtp.Send(Mail)
        Mail.Dispose()
        Me.Hide()
        SystemWorkStation.Focus()
        Exit Sub

MyERR:
        '捕获到异常时，把错误信息显示在发信地址栏里
        Txt_ToAddress.Text = Err.Description
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

    Private Sub XYMail_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        '判断是否需要置后显示
        If SystemWorkStation.ShowMeBehind Then SystemWorkStation.SetWindowPos(SystemWorkStation.Handle, 1, 0, 0, 0, 0, &H10 Or &H40 Or &H2 Or &H1)
    End Sub

    Private Sub Txt_ToAddress_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ToAddress.KeyPress
        '发信人地址栏响应回车键
        If Asc(e.KeyChar) = 13 Then Btn_Send_Click(New Object, New EventArgs)
    End Sub

    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        Me.Hide()
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
#End Region

End Class
