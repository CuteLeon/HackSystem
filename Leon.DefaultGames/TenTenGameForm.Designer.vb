Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TenTenGameForm
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TenTenGameForm))
        Me.ScoreLabel = New System.Windows.Forms.Label()
        Me.ObjectLabel0 = New System.Windows.Forms.Label()
        Me.ObjectLabel1 = New System.Windows.Forms.Label()
        Me.ObjectLabel2 = New System.Windows.Forms.Label()
        Me.MaskLabel = New System.Windows.Forms.Label()
        Me.CloseButton = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ScoreLabel
        '
        Me.ScoreLabel.BackColor = System.Drawing.Color.Transparent
        Me.ScoreLabel.Font = New System.Drawing.Font("微软雅黑", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ScoreLabel.ForeColor = System.Drawing.Color.DimGray
        Me.ScoreLabel.Location = New System.Drawing.Point(256, 39)
        Me.ScoreLabel.Name = "ScoreLabel"
        Me.ScoreLabel.Size = New System.Drawing.Size(120, 32)
        Me.ScoreLabel.TabIndex = 1
        Me.ScoreLabel.Text = "0"
        Me.ScoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ObjectLabel0
        '
        Me.ObjectLabel0.BackColor = System.Drawing.Color.Transparent
        Me.ObjectLabel0.Location = New System.Drawing.Point(380, 155)
        Me.ObjectLabel0.Name = "ObjectLabel0"
        Me.ObjectLabel0.Size = New System.Drawing.Size(0, 0)
        Me.ObjectLabel0.TabIndex = 2
        Me.ObjectLabel0.Tag = "0"
        '
        'ObjectLabel1
        '
        Me.ObjectLabel1.BackColor = System.Drawing.Color.Transparent
        Me.ObjectLabel1.Location = New System.Drawing.Point(380, 240)
        Me.ObjectLabel1.Name = "ObjectLabel1"
        Me.ObjectLabel1.Size = New System.Drawing.Size(0, 0)
        Me.ObjectLabel1.TabIndex = 3
        Me.ObjectLabel1.Tag = "1"
        '
        'ObjectLabel2
        '
        Me.ObjectLabel2.BackColor = System.Drawing.Color.Transparent
        Me.ObjectLabel2.Location = New System.Drawing.Point(380, 325)
        Me.ObjectLabel2.Name = "ObjectLabel2"
        Me.ObjectLabel2.Size = New System.Drawing.Size(0, 0)
        Me.ObjectLabel2.TabIndex = 4
        Me.ObjectLabel2.Tag = "2"
        '
        'MaskLabel
        '
        Me.MaskLabel.BackColor = System.Drawing.Color.Transparent
        Me.MaskLabel.Image = Global.Leon.DefaultGames.My.Resources.DefaultGameResource.Mask
        Me.MaskLabel.Location = New System.Drawing.Point(40, 88)
        Me.MaskLabel.Name = "MaskLabel"
        Me.MaskLabel.Size = New System.Drawing.Size(280, 280)
        Me.MaskLabel.TabIndex = 5
        '
        'CloseButton
        '
        Me.CloseButton.BackColor = System.Drawing.Color.Transparent
        Me.CloseButton.Image = Global.Leon.DefaultGames.My.Resources.DefaultGameResource.GameClose_0
        Me.CloseButton.Location = New System.Drawing.Point(404, 12)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(25, 25)
        Me.CloseButton.TabIndex = 6
        '
        'TenTenGameForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Leon.DefaultGames.My.Resources.DefaultGameResource.TenTenBackground
        Me.ClientSize = New System.Drawing.Size(450, 400)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.MaskLabel)
        Me.Controls.Add(Me.ObjectLabel2)
        Me.Controls.Add(Me.ObjectLabel1)
        Me.Controls.Add(Me.ObjectLabel0)
        Me.Controls.Add(Me.ScoreLabel)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "TenTenGameForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "1010"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ScoreLabel As Label
    Friend WithEvents ObjectLabel0 As Label
    Friend WithEvents ObjectLabel1 As Label
    Friend WithEvents ObjectLabel2 As Label
    Friend WithEvents MaskLabel As Label
    Friend WithEvents CloseButton As Label
End Class
