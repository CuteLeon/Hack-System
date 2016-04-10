<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ShutdowningUI
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
        Me.StartUpLogo = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'StartUpLogo
        '
        Me.StartUpLogo.BackColor = System.Drawing.Color.Transparent
        Me.StartUpLogo.Image = Global.HackSystem.My.Resources.SystemAssets.HackSystemLogo
        Me.StartUpLogo.Location = New System.Drawing.Point(60, 121)
        Me.StartUpLogo.Name = "StartUpLogo"
        Me.StartUpLogo.Size = New System.Drawing.Size(487, 100)
        Me.StartUpLogo.TabIndex = 4
        '
        'ShutdowningUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BackgroundImage = Global.HackSystem.My.Resources.SystemAssets.ShutdownBGI
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(606, 343)
        Me.Controls.Add(Me.StartUpLogo)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ShutdowningUI"
        Me.Opacity = 0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Shutdowning"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents StartUpLogo As Label
End Class
