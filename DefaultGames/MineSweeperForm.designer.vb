Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MineSweeperForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MineSweeperForm))
        Me.MinefieldPanel = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'MinefieldPanel
        '
        Me.MinefieldPanel.Cursor = System.Windows.Forms.Cursors.Cross
        Me.MinefieldPanel.Location = New System.Drawing.Point(10, 27)
        Me.MinefieldPanel.Name = "MinefieldPanel"
        Me.MinefieldPanel.Size = New System.Drawing.Size(320, 320)
        Me.MinefieldPanel.TabIndex = 0
        '
        'MineSweeperForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(340, 360)
        Me.Controls.Add(Me.MinefieldPanel)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MineSweeperForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mine Sweeper"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents MinefieldPanel As Panel
End Class
