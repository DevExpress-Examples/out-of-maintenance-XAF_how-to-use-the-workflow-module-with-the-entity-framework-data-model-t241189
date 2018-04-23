Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp

Namespace WorkflowDemo.Win
    Public Class WorkflowServerAuthentication
        Inherits AuthenticationBase

        Private findUserCriteria As CriteriaOperator
        Public Sub New(ByVal findUserCriteria As CriteriaOperator)
            Me.findUserCriteria = findUserCriteria
        End Sub
        Public Overrides Function Authenticate(ByVal objectSpace As IObjectSpace) As Object
            Dim user As Object = objectSpace.FindObject(UserType, findUserCriteria)
            If user Is Nothing Then
                Throw New AuthenticationException(findUserCriteria.ToString())
            End If
            Return user
        End Function
        Public Overrides ReadOnly Property IsLogoffEnabled() As Boolean
            Get
                Return False
            End Get
        End Property
        Public Overrides Property UserType() As Type
    End Class
End Namespace
