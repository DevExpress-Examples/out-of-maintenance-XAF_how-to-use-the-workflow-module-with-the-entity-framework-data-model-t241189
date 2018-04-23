Imports DevExpress.ExpressApp.EF.Updating
Imports DevExpress.ExpressApp.Workflow.EF
Imports DevExpress.Workflow.EF
Namespace WorkflowDemoEF.Win
    Partial Public Class WorkflowDemoEFWindowsFormsApplication
        ''' <summary> 
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary> 
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        #Region "Component Designer generated code"

        ''' <summary> 
        ''' Required method for Designer support - do not modify 
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
            Me.module2 = New DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule()
            Me.module4 = New WorkflowDemoEF.Module.Win.WorkflowDemoEFWindowsFormsModule()
            Me.module6 = New DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule()
            Me.module7 = New DevExpress.ExpressApp.FileAttachments.Win.FileAttachmentsWindowsFormsModule()
            Me.module8 = New DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule()
            Me.htmlPropertyEditorWindowsFormsModule1 = New DevExpress.ExpressApp.HtmlPropertyEditor.Win.HtmlPropertyEditorWindowsFormsModule()
            Me.viewVariantsModule1 = New DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule()
            Me.conditionalAppearanceModule1 = New DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule()
            Me.wfModule = New DevExpress.ExpressApp.Workflow.WorkflowModule()
            Me.wfWinModule = New DevExpress.ExpressApp.Workflow.Win.WorkflowWindowsFormsModule()

            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()

            Me.wfModule.RunningWorkflowInstanceInfoType = GetType(EFRunningWorkflowInstanceInfo)
            Me.wfModule.StartWorkflowRequestType = GetType(EFStartWorkflowRequest)
            Me.wfModule.UserActivityVersionType = GetType(DevExpress.ExpressApp.Workflow.Versioning.EFUserActivityVersion)
            Me.wfModule.WorkflowControlCommandRequestType = GetType(EFWorkflowInstanceControlCommandRequest)
            Me.wfModule.WorkflowDefinitionType = GetType(EFWorkflowDefinition)
            Me.wfModule.WorkflowInstanceKeyType = GetType(EFInstanceKey)
            Me.wfModule.WorkflowInstanceType = GetType(EFWorkflowInstance)
            Me.wfModule.UserActivityVersionType = GetType(DevExpress.ExpressApp.Workflow.Versioning.EFUserActivityVersion)
            ' 
            ' WorkflowDemoWindowsFormsApplication
            ' 
            Me.ApplicationName = "WorkflowDemo"
            Me.Modules.Add(Me.module1)
            Me.Modules.Add(Me.module2)
            Me.Modules.Add(Me.module4)
            Me.Modules.Add(Me.module6)
            Me.Modules.Add(Me.module7)
            Me.Modules.Add(Me.module8)
            Me.Modules.Add(Me.htmlPropertyEditorWindowsFormsModule1)
            Me.Modules.Add(Me.viewVariantsModule1)
            Me.Modules.Add(Me.conditionalAppearanceModule1)
            Me.Modules.Add(Me.wfModule)
            Me.Modules.Add(Me.wfWinModule)
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub

        #End Region
        Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
        Private module2 As DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule
        Private module4 As WorkflowDemoEF.Module.Win.WorkflowDemoEFWindowsFormsModule
        Private module6 As DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule
        Private module7 As DevExpress.ExpressApp.FileAttachments.Win.FileAttachmentsWindowsFormsModule
        Private module8 As DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule
        Private htmlPropertyEditorWindowsFormsModule1 As DevExpress.ExpressApp.HtmlPropertyEditor.Win.HtmlPropertyEditorWindowsFormsModule
        Private viewVariantsModule1 As DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule
        Private conditionalAppearanceModule1 As DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule
        Private wfModule As DevExpress.ExpressApp.Workflow.WorkflowModule
        Private wfWinModule As DevExpress.ExpressApp.Workflow.Win.WorkflowWindowsFormsModule
    End Class
End Namespace
