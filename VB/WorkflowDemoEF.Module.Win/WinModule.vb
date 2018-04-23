Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Updating

Namespace WorkflowDemoEF.Module.Win
    <ToolboxItemFilter("Xaf.Platform.Win")> _
    Public NotInheritable Partial Class WorkflowDemoEFWindowsFormsModule
        Inherits ModuleBase

        Public Sub New()
            InitializeComponent()
        End Sub
        Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
            Dim updater As ModuleUpdater = New Updater(objectSpace, versionFromDB)
            Return New ModuleUpdater() { updater }
        End Function
    End Class
End Namespace
