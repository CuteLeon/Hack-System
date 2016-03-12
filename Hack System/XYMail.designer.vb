<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class XYMail
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XYMail))
        Me.Txt_ToAddress = New System.Windows.Forms.TextBox()
        Me.Txt_MailTitle = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Txt_MailBody = New System.Windows.Forms.TextBox()
        Me.Btn_Send = New System.Windows.Forms.Label()
        Me.Btn_Close = New System.Windows.Forms.Label()
        Me.ReturnInfo = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Txt_ToAddress
        '
        Me.Txt_ToAddress.BackColor = System.Drawing.Color.Snow
        Me.Txt_ToAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_ToAddress.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Txt_ToAddress.Location = New System.Drawing.Point(67, 25)
        Me.Txt_ToAddress.Name = "Txt_ToAddress"
        Me.Txt_ToAddress.Size = New System.Drawing.Size(133, 23)
        Me.Txt_ToAddress.TabIndex = 0
        Me.Txt_ToAddress.Text = "Leon.ID@QQ.COM"
        Me.Txt_ToAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Txt_MailTitle
        '
        Me.Txt_MailTitle.BackColor = System.Drawing.Color.Snow
        Me.Txt_MailTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_MailTitle.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Txt_MailTitle.Location = New System.Drawing.Point(25, 111)
        Me.Txt_MailTitle.Name = "Txt_MailTitle"
        Me.Txt_MailTitle.Size = New System.Drawing.Size(270, 23)
        Me.Txt_MailTitle.TabIndex = 1
        Me.Txt_MailTitle.Text = "Hack System"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(33, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "To :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 20)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Subject："
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label3.Location = New System.Drawing.Point(21, 137)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 20)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Content："
        '
        'Txt_MailBody
        '
        Me.Txt_MailBody.BackColor = System.Drawing.Color.Snow
        Me.Txt_MailBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_MailBody.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Txt_MailBody.Location = New System.Drawing.Point(25, 160)
        Me.Txt_MailBody.Multiline = True
        Me.Txt_MailBody.Name = "Txt_MailBody"
        Me.Txt_MailBody.Size = New System.Drawing.Size(270, 123)
        Me.Txt_MailBody.TabIndex = 4
        Me.Txt_MailBody.Text = "<h1>  Welcome to Hack System.</h1>" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "<a href=""http://www.hacksystem.icoc.in/"" targ" &
    "et=""_blank"">访问PC端官方网站</a>" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "<br/>" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "<a href=""http://m.hacksystem.icoc.in/index.jsp" &
    """ target=""_blank"">访问手机端官方网站</a>"
        '
        'Btn_Send
        '
        Me.Btn_Send.BackColor = System.Drawing.Color.Transparent
        Me.Btn_Send.Image = Global.HackSystem.My.Resources.SystemAssets.SendBtn_0
        Me.Btn_Send.Location = New System.Drawing.Point(205, 31)
        Me.Btn_Send.Name = "Btn_Send"
        Me.Btn_Send.Size = New System.Drawing.Size(105, 44)
        Me.Btn_Send.TabIndex = 6
        '
        'Btn_Close
        '
        Me.Btn_Close.BackColor = System.Drawing.Color.Transparent
        Me.Btn_Close.Image = Global.HackSystem.My.Resources.XYBrowserRes.Close_N
        Me.Btn_Close.Location = New System.Drawing.Point(281, 0)
        Me.Btn_Close.Name = "Btn_Close"
        Me.Btn_Close.Size = New System.Drawing.Size(28, 28)
        Me.Btn_Close.TabIndex = 22
        Me.Btn_Close.Tag = "Close"
        '
        'ReturnInfo
        '
        Me.ReturnInfo.BackColor = System.Drawing.Color.Transparent
        Me.ReturnInfo.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ReturnInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(19, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.ReturnInfo.Location = New System.Drawing.Point(35, 51)
        Me.ReturnInfo.Name = "ReturnInfo"
        Me.ReturnInfo.Size = New System.Drawing.Size(165, 24)
        Me.ReturnInfo.TabIndex = 23
        Me.ReturnInfo.Text = "Mail function is ready."
        Me.ReturnInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'XYMail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Aqua
        Me.BackgroundImage = Global.HackSystem.My.Resources.SystemAssets.MailBackgroundImage
        Me.ClientSize = New System.Drawing.Size(320, 295)
        Me.Controls.Add(Me.ReturnInfo)
        Me.Controls.Add(Me.Btn_Close)
        Me.Controls.Add(Me.Btn_Send)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Txt_MailBody)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Txt_MailTitle)
        Me.Controls.Add(Me.Txt_ToAddress)
        Me.ForeColor = System.Drawing.Color.Firebrick
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "XYMail"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "小眼邮箱轰炸机"
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.Color.Aqua
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Txt_ToAddress As TextBox
    Friend WithEvents Txt_MailTitle As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Txt_MailBody As TextBox
    Friend WithEvents Btn_Send As Label
    Friend WithEvents Btn_Close As Label
    Friend WithEvents ReturnInfo As Label
End Class
