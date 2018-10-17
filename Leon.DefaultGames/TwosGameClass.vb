Imports System.Windows.Forms

Imports HackSystem.ProgramTemplate

Public Class TwosGameClass
    Inherits ProgramTemplateClass

    Public Sub New()
        Name = "2048"
        Description = "2048 [via : Leon]"
        Icon = My.Resources.DefaultGameResource.TwosGameIcon
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
