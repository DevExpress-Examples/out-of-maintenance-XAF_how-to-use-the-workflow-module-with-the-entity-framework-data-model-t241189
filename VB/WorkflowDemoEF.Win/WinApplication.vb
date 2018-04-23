Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.EF
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Win
Imports WorkflowDemoEF.Module.Objects
Imports DevExpress.ExpressApp.EF.Updating

Namespace WorkflowDemoEF.Win
    Partial Public Class WorkflowDemoEFWindowsFormsApplication
        Inherits WinApplication

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
            args.ObjectSpaceProvider = New EFObjectSpaceProvider(GetType(WorkflowDemoDbContext), args.ConnectionString)
        End Sub

        Private Sub WorkflowDemoWindowsFormsApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles Me.DatabaseVersionMismatch
            'try {
                e.Updater.Update()
                e.Handled = True
            '}
            'catch(CompatibilityException exception) {
            '    if(exception.Error is CompatibilityUnableToOpenDatabaseError) {
            '        throw new UserFriendlyException(
            '        "The connection to the database failed. This demo requires the local instance of Microsoft SQL Server Express. To use another database server,\r\nopen the demo solution in Visual Studio and modify connection string in the \"app.config\" file.");
            '    }
            '}
        End Sub
    End Class
End Namespace
