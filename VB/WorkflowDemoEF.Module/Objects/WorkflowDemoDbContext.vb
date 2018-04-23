Imports System
Imports System.Collections.Generic
Imports System.Data.Common
Imports System.Data.Entity
Imports System.Linq
Imports System.Text
Imports DevExpress.ExpressApp.EF.Updating
Imports DevExpress.ExpressApp.Workflow.EF
Imports DevExpress.ExpressApp.Workflow.Versioning
Imports DevExpress.Workflow.EF

Namespace WorkflowDemoEF.Module.Objects
    Public Class WorkflowDemoDbContext
        Inherits DbContext

        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal connectionString As String)
            MyBase.New(connectionString)
        End Sub

        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            MyBase.OnModelCreating(modelBuilder)
            modelBuilder.Entity(Of ModuleInfo)().ToTable("ModulesInfo")
        End Sub

        Public Property Issue() As DbSet(Of Issue)
        Public Property Task() As DbSet(Of Task)
        Public Property EFWorkflowDefinition() As DbSet(Of EFWorkflowDefinition)
        Public Property EFStartWorkflowRequest() As DbSet(Of EFStartWorkflowRequest)
        Public Property EFRunningWorkflowInstanceInfo() As DbSet(Of EFRunningWorkflowInstanceInfo)
        Public Property EFWorkflowInstanceControlCommandRequest() As DbSet(Of EFWorkflowInstanceControlCommandRequest)
        Public Property EFInstanceKey() As DbSet(Of EFInstanceKey)
        Public Property EFTrackingRecord() As DbSet(Of EFTrackingRecord)
        Public Property EFWorkflowInstance() As DbSet(Of EFWorkflowInstance)
        Public Property EFUserActivityVersion() As DbSet(Of EFUserActivityVersion)
        Public Property ModulesInfo() As DbSet(Of ModuleInfo)
    End Class
End Namespace
