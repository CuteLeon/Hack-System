<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WindowsTemplates
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
        Me.GIFControl = New System.Windows.Forms.PictureBox()
        Me.CloseButtonControl = New System.Windows.Forms.Label()
        CType(Me.GIFControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GIFControl
        '
        Me.GIFControl.BackColor = System.Drawing.Color.Black
        Me.GIFControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GIFControl.Location = New System.Drawing.Point(4, 30)
        Me.GIFControl.Name = "GIFControl"
        Me.GIFControl.Size = New System.Drawing.Size(272, 247)
        Me.GIFControl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.GIFControl.TabIndex = 0
        Me.GIFControl.TabStop = False
        '
        'CloseButtonControl
        '
        Me.CloseButtonControl.BackColor = System.Drawing.Color.Transparent
        Me.CloseButtonControl.Font = New System.Drawing.Font("新宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.CloseButtonControl.Image = Global.HackSystem.My.Resources.SystemAssets.CloseButton
        Me.CloseButtonControl.Location = New System.Drawing.Point(252, 1)
        Me.CloseButtonControl.Name = "CloseButtonControl"
        Me.CloseButtonControl.Size = New System.Drawing.Size(27, 27)
        Me.CloseButtonControl.TabIndex = 1
        '
        'WindowsTemplates
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(280, 280)
        Me.Controls.Add(Me.CloseButtonControl)
        Me.Controls.Add(Me.GIFControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "WindowsTemplates"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Tag = "00"
        Me.Text = "Windows"
        CType(Me.GIFControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GIFControl As PictureBox
    Friend WithEvents CloseButtonControl As Label
End Class
