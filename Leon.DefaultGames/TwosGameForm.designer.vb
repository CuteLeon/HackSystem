Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TwosGameForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TwosGameForm))
        Me.CloseButton = New System.Windows.Forms.Label()
        Me.KeyRightLabel = New System.Windows.Forms.Label()
        Me.KeyLeftLabel = New System.Windows.Forms.Label()
        Me.KeyDownLabel = New System.Windows.Forms.Label()
        Me.KeyUpLabel = New System.Windows.Forms.Label()
        Me.ScoreLabel = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'CloseButton
        '
        Me.CloseButton.BackColor = System.Drawing.Color.Transparent
        Me.CloseButton.Image = Global.Leon.DefaultGames.My.Resources.DefaultGameResource.GameClose_0
        Me.CloseButton.Location = New System.Drawing.Point(366, 10)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(25, 25)
        Me.CloseButton.TabIndex = 7
        '
        'KeyRightLabel
        '
        Me.KeyRightLabel.BackColor = System.Drawing.Color.Transparent
        Me.KeyRightLabel.Font = New System.Drawing.Font("微软雅黑", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.KeyRightLabel.ForeColor = System.Drawing.Color.Gray
        Me.KeyRightLabel.Image = Global.Leon.DefaultGames.My.Resources.DefaultGameResource.DirectiionKey
        Me.KeyRightLabel.Location = New System.Drawing.Point(289, 467)
        Me.KeyRightLabel.Name = "KeyRightLabel"
        Me.KeyRightLabel.Size = New System.Drawing.Size(50, 50)
        Me.KeyRightLabel.TabIndex = 4
        Me.KeyRightLabel.Tag = "39"
        Me.KeyRightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'KeyLeftLabel
        '
        Me.KeyLeftLabel.BackColor = System.Drawing.Color.Transparent
        Me.KeyLeftLabel.Font = New System.Drawing.Font("微软雅黑", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.KeyLeftLabel.ForeColor = System.Drawing.Color.Gray
        Me.KeyLeftLabel.Image = Global.Leon.DefaultGames.My.Resources.DefaultGameResource.DirectiionKey
        Me.KeyLeftLabel.Location = New System.Drawing.Point(233, 467)
        Me.KeyLeftLabel.Name = "KeyLeftLabel"
        Me.KeyLeftLabel.Size = New System.Drawing.Size(50, 50)
        Me.KeyLeftLabel.TabIndex = 3
        Me.KeyLeftLabel.Tag = "37"
        Me.KeyLeftLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'KeyDownLabel
        '
        Me.KeyDownLabel.BackColor = System.Drawing.Color.Transparent
        Me.KeyDownLabel.Font = New System.Drawing.Font("微软雅黑", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.KeyDownLabel.ForeColor = System.Drawing.Color.Gray
        Me.KeyDownLabel.Image = Global.Leon.DefaultGames.My.Resources.DefaultGameResource.DirectiionKey
        Me.KeyDownLabel.Location = New System.Drawing.Point(177, 467)
        Me.KeyDownLabel.Name = "KeyDownLabel"
        Me.KeyDownLabel.Size = New System.Drawing.Size(50, 50)
        Me.KeyDownLabel.TabIndex = 2
        Me.KeyDownLabel.Tag = "40"
        Me.KeyDownLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'KeyUpLabel
        '
        Me.KeyUpLabel.BackColor = System.Drawing.Color.Transparent
        Me.KeyUpLabel.Font = New System.Drawing.Font("微软雅黑", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.KeyUpLabel.ForeColor = System.Drawing.Color.Gray
        Me.KeyUpLabel.Image = Global.Leon.DefaultGames.My.Resources.DefaultGameResource.DirectiionKey
        Me.KeyUpLabel.Location = New System.Drawing.Point(121, 467)
        Me.KeyUpLabel.Name = "KeyUpLabel"
        Me.KeyUpLabel.Size = New System.Drawing.Size(50, 50)
        Me.KeyUpLabel.TabIndex = 1
        Me.KeyUpLabel.Tag = "38"
        Me.KeyUpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ScoreLabel
        '
        Me.ScoreLabel.BackColor = System.Drawing.Color.Transparent
        Me.ScoreLabel.Font = New System.Drawing.Font("微软雅黑", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ScoreLabel.ForeColor = System.Drawing.Color.DimGray
        Me.ScoreLabel.Location = New System.Drawing.Point(263, 46)
        Me.ScoreLabel.Name = "ScoreLabel"
        Me.ScoreLabel.Size = New System.Drawing.Size(120, 32)
        Me.ScoreLabel.TabIndex = 0
        Me.ScoreLabel.Text = "0"
        Me.ScoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TwosGameForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Leon.DefaultGames.My.Resources.DefaultGameResource.TwosBackground
        Me.ClientSize = New System.Drawing.Size(400, 540)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.KeyRightLabel)
        Me.Controls.Add(Me.KeyLeftLabel)
        Me.Controls.Add(Me.KeyDownLabel)
        Me.Controls.Add(Me.KeyUpLabel)
        Me.Controls.Add(Me.ScoreLabel)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "TwosGameForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "2048"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CloseButton As Label
    Friend WithEvents KeyRightLabel As Label
    Friend WithEvents KeyLeftLabel As Label
    Friend WithEvents KeyDownLabel As Label
    Friend WithEvents KeyUpLabel As Label
    Friend WithEvents ScoreLabel As Label
End Class
