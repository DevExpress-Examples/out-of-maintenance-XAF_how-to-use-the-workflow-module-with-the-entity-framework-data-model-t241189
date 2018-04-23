using DevExpress.ExpressApp;
using DevExpress.ExpressApp.EF;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Win;
using WorkflowDemoEF.Module.Objects;
using DevExpress.ExpressApp.EF.Updating;

namespace WorkflowDemoEF.Win
{
    public partial class WorkflowDemoEFWindowsFormsApplication : WinApplication
    {
		public WorkflowDemoEFWindowsFormsApplication()
        {
            InitializeComponent();
        }

        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args)
        {
            args.ObjectSpaceProvider = new EFObjectSpaceProvider(typeof(WorkflowDemoDbContext), args.ConnectionString);
        }
        
        private void WorkflowDemoWindowsFormsApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e)
        {
            //try {
                e.Updater.Update();
                e.Handled = true;
            //}
            //catch(CompatibilityException exception) {
            //    if(exception.Error is CompatibilityUnableToOpenDatabaseError) {
            //        throw new UserFriendlyException(
            //        "The connection to the database failed. This demo requires the local instance of Microsoft SQL Server Express. To use another database server,\r\nopen the demo solution in Visual Studio and modify connection string in the \"app.config\" file.");
            //    }
            //}
        }
    }
}
