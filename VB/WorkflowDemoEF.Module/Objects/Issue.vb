Imports DevExpress.Persistent.Base
Imports System.ComponentModel
Imports System
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.ExpressApp
Namespace WorkflowDemoEF.Module.Objects
    <DefaultClassOptions, DefaultProperty("Subject"), ImageName("BO_Note")> _
    Public Class Issue
        Private privateId As Integer
        <Browsable(False)> _
        Public Property Id() As Integer
            Get
                Return privateId
            End Get
            Private Set(ByVal value As Integer)
                privateId = value
            End Set
        End Property
        Public Property Subject() As String
        Public Property Active() As Boolean
    End Class
End Namespace
