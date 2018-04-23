using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.EF.Updating;
using DevExpress.ExpressApp.Workflow.EF;
using DevExpress.ExpressApp.Workflow.Versioning;
using DevExpress.Workflow.EF;

namespace WorkflowDemoEF.Module.Objects {
    public class WorkflowDemoDbContext : DbContext {
        public WorkflowDemoDbContext() : base() { }
        public WorkflowDemoDbContext(string connectionString) : base(connectionString) { }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ModuleInfo>().ToTable("ModulesInfo");
        }

        public DbSet<Issue> Issue { get; set; }
        public DbSet<Task> Task { get; set; }        
        public DbSet<EFWorkflowDefinition> EFWorkflowDefinition { get; set; }
        public DbSet<EFStartWorkflowRequest> EFStartWorkflowRequest { get; set; }
        public DbSet<EFRunningWorkflowInstanceInfo> EFRunningWorkflowInstanceInfo { get; set; }
        public DbSet<EFWorkflowInstanceControlCommandRequest> EFWorkflowInstanceControlCommandRequest { get; set; }
        public DbSet<EFInstanceKey> EFInstanceKey { get; set; }
        public DbSet<EFTrackingRecord> EFTrackingRecord { get; set; }
        public DbSet<EFWorkflowInstance> EFWorkflowInstance { get; set; }
        public DbSet<EFUserActivityVersion> EFUserActivityVersion { get; set; }
        public DbSet<ModuleInfo> ModulesInfo { get; set; }
    }
}
