<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TipsForm
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
        Me.components = New System.ComponentModel.Container()
        Me.IconTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'IconTimer
        '
        Me.IconTimer.Interval = 70
        '
        'TipsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(358, 108)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Location = New System.Drawing.Point(-368, -118)
        Me.Name = "TipsForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Tips"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IconTimer As Timer
End Class
