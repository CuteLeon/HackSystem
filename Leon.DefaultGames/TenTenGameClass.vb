Imports System.Windows.Forms

Imports HackSystem.ProgramTemplate

Public Class TenTenGameClass
    Inherits ProgramTemplateClass

    Public Sub New()
        Me.Name = "1010"
        Me.Description = "1010 [via : Leon]"
        Me.Icon = My.Resources.DefaultGameResource.TenTenGameIcon
    End Sub


    Public Overrides ReadOnly Property FileName As String
        Get
            Return System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName
        End Get
    End Property

    Protected Overrides Function CreateProgramForm() As Form
        Return New TenTenGameForm
    End Function

End Class
