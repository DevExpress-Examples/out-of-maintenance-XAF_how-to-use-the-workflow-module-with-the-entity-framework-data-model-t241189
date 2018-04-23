Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Namespace WorkflowDemoEF.Module.Objects
    <DefaultClassOptions, ImageName("BO_Task")> _
    Public Class Task
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
        Public Overridable Property Issue() As Issue
    End Class
End Namespace
