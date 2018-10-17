Imports System.Windows.Forms

Imports HackSystem.ProgramTemplate

Public Class TwosGameClass
    Inherits ProgramTemplateClass

    Public Sub New()
        Me.Name = "2048"
        Me.Description = "2048 [via : Leon]"
        Me.Icon = My.Resources.DefaultGameResource.TwosGameIcon
    End Sub


    Public Overrides ReadOnly Property FileName As String
        Get
            Return System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName
        End Get
    End Property

    Protected Overrides Function CreateProgramForm() As Form
        Return New TwosGameForm
    End Function

End Class
