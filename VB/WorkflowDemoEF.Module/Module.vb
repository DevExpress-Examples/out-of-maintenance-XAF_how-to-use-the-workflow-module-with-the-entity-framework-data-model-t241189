Imports System
Imports System.Collections.Generic
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.EF.Updating
Imports DevExpress.ExpressApp.Updating
Imports WorkflowDemoEF.Module.Objects

Namespace WorkflowDemoEF.Module
    Public NotInheritable Partial Class WorkflowDemoEFModule
        Inherits ModuleBase

        Public Sub New()
            InitializeComponent()
            Me.AdditionalExportedTypes.Add(GetType(Issue))
            Me.AdditionalExportedTypes.Add(GetType(Task))
            Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.Workflow.WorkflowModule))
            Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule))
        End Sub
        Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
            Dim updater As ModuleUpdater = New Updater(objectSpace, versionFromDB)
            Return New ModuleUpdater() { updater }
        End Function
    End Class
End Namespace