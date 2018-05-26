Imports System.Windows.Forms
Imports ProgramTemplate

Public Class MineSweeperClass
    Inherits ProgramTemplateClass

    Public Sub New()
        Name = "扫雷"
        Description = "扫雷 [via : Leon]"
        Icon = My.Resources.DefaultGameResource.MineSweeperGameIcon
    End Sub


    Public Overrides ReadOnly Property FileName As String
        Get
            Return System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName
        End Get
    End Property

    Protected Overrides Function CreateProgramForm() As Form
        Return New MineSweeperForm
    End Function

End Class