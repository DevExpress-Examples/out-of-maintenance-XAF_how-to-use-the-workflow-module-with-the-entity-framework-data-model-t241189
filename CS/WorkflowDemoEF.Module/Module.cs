using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.EF.Updating;
using DevExpress.ExpressApp.Updating;
using WorkflowDemoEF.Module.Objects;

namespace WorkflowDemoEF.Module {
	public sealed partial class WorkflowDemoEFModule : ModuleBase {
		public WorkflowDemoEFModule() {
			InitializeComponent();
            this.AdditionalExportedTypes.Add(typeof(Issue));
            this.AdditionalExportedTypes.Add(typeof(Task));
            this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Workflow.WorkflowModule));
            this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule));            
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            ModuleUpdater updater = new Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }
    }
}