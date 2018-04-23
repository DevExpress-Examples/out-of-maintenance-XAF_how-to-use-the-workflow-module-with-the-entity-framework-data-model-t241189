using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;

namespace WorkflowDemoEF.Module.Win
{
    [ToolboxItemFilter("Xaf.Platform.Win")]
    public sealed partial class WorkflowDemoEFWindowsFormsModule : ModuleBase
    {
		public WorkflowDemoEFWindowsFormsModule()
        {
            InitializeComponent();
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            ModuleUpdater updater = new Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }
    }
}
