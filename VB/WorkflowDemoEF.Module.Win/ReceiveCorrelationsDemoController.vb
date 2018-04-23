Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.ExpressApp.Actions
Imports System.ServiceModel
Imports DevExpress.ExpressApp.Workflow
Imports DevExpress.Data.Filtering

Imports System.Threading
Imports DevExpress.ExpressApp
Imports WorkflowDemoEF.Module.Objects
Imports DevExpress.Persistent.Base
Imports DevExpress.XtraEditors
Imports DevExpress.ExpressApp.Workflow.EF

Namespace WorkflowDemoEF.Module

    <ServiceContract> _
    Public Interface IStartStop

        <OperationContract(IsOneWay := False)> _
        Function Start(ByVal clientId As String) As <MessageParameterAttribute(Name:="result")> String

        <OperationContract(IsOneWay := False)> _
        Function [Stop](ByVal clientId As String) As <MessageParameterAttribute(Name:="result")> String
    End Interface

    Public Class ReceiveCorrelationsDemoController
        Inherits ObjectViewController

        Private Const WorkflowServerAddress As String = "http://localhost:46232/"
        Private startWorkflowAction As ParametrizedAction
        Private stopWorkflowAction As ParametrizedAction

        Private ReadOnly Property StartStopClient() As IStartStop
            Get
                Dim definition As IWorkflowDefinition = View.ObjectSpace.FindObject(Of EFWorkflowDefinition)(CriteriaOperator.Parse("Name = 'Start/stop (correlations) demo'"))
                If definition IsNot Nothing Then
                    Return ChannelFactory(Of IStartStop).CreateChannel(New BasicHttpBinding(), New EndpointAddress(WorkflowServerAddress & definition.GetUniqueId()))
                End If
                Return Nothing
            End Get
        End Property

        Private Sub startWorkflowAction_Execute(ByVal sender As Object, ByVal e As ParametrizedActionExecuteEventArgs)
            If StartStopClient IsNot Nothing Then
                Dim result As String = StartStopClient.Start(CStr(e.ParameterCurrentValue))
                XtraMessageBox.Show(result)
            End If
        End Sub
        Private Sub stopWorkflowAction_Execute(ByVal sender As Object, ByVal e As ParametrizedActionExecuteEventArgs)
            If StartStopClient IsNot Nothing Then
                Dim result As String = StartStopClient.Stop(CStr(e.ParameterCurrentValue))
                XtraMessageBox.Show(result)
            End If
        End Sub

        Public Sub New()
            TargetObjectType = GetType(Task)
            startWorkflowAction = New ParametrizedAction(Me, "StartWorkflow", PredefinedCategory.Edit, GetType(String))
            AddHandler startWorkflowAction.Execute, AddressOf startWorkflowAction_Execute
            stopWorkflowAction = New ParametrizedAction(Me, "StopWorkflow", PredefinedCategory.Edit, GetType(String))
            AddHandler stopWorkflowAction.Execute, AddressOf stopWorkflowAction_Execute
        End Sub

    End Class
End Namespace
