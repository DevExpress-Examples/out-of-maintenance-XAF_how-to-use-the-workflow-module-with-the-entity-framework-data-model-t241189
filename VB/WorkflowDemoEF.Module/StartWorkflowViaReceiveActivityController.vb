Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.ExpressApp
Imports System.ServiceModel
Imports DevExpress.ExpressApp.Actions
Imports WorkflowDemoEF.Module.Objects
Imports DevExpress.Persistent.Base

Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp.Workflow
Imports DevExpress.ExpressApp.Workflow.EF

Namespace WorkflowDemoEF.Module

    <ServiceContract> _
    Public Interface IPassIntegerData
        <OperationContract(IsOneWay := True)> _
        Sub PassIntegerData(ByVal data As Integer)
    End Interface

    <ServiceContract> _
    Public Interface IPassStringData
        <OperationContract(IsOneWay := True)> _
        Sub PassStringData(ByVal data As String)
    End Interface

    Public Class StartWorkflowViaReceiveActivityController
        Inherits ViewController

        Private Const WorkflowServerAddress As String = "http://localhost:46232/"
        Private passIntAction As ParametrizedAction
        Private passStringAction As ParametrizedAction

        Private Sub passIntAction_Execute(ByVal sender As Object, ByVal e As ParametrizedActionExecuteEventArgs)
            Dim definition As IWorkflowDefinition = View.ObjectSpace.FindObject(Of EFWorkflowDefinition)(CriteriaOperator.Parse("Name = 'Custom start workflow'"))
            If definition IsNot Nothing Then
                Dim serverWorkflow As IPassIntegerData = ChannelFactory(Of IPassIntegerData).CreateChannel(New BasicHttpBinding(), New EndpointAddress(WorkflowServerAddress & definition.GetUniqueId()))
                serverWorkflow.PassIntegerData(CInt((e.ParameterCurrentValue)))
            End If
        End Sub
        Private Sub passStringAction_Execute(ByVal sender As Object, ByVal e As ParametrizedActionExecuteEventArgs)
            Dim definition As IWorkflowDefinition = View.ObjectSpace.FindObject(Of EFWorkflowDefinition)(CriteriaOperator.Parse("Name = 'Custom start workflow'"))
            If definition IsNot Nothing Then
                Dim serverWorkflow As IPassStringData = ChannelFactory(Of IPassStringData).CreateChannel(New BasicHttpBinding(), New EndpointAddress(WorkflowServerAddress & definition.GetUniqueId()))
                serverWorkflow.PassStringData(CStr(e.ParameterCurrentValue))
            End If
        End Sub

        Public Sub New()
            TargetObjectType = GetType(Task)
            passIntAction = New ParametrizedAction(Me, "StartWithInteger", PredefinedCategory.Edit, GetType(Integer))
            AddHandler passIntAction.Execute, AddressOf passIntAction_Execute
            passStringAction = New ParametrizedAction(Me, "StartWithString", PredefinedCategory.Edit, GetType(String))
            AddHandler passStringAction.Execute, AddressOf passStringAction_Execute
        End Sub

    End Class
End Namespace
