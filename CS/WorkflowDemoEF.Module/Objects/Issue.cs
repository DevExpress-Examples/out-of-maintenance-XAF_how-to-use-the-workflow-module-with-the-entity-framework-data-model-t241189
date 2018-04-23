using DevExpress.Persistent.Base;
using System.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using DevExpress.ExpressApp;
namespace WorkflowDemoEF.Module.Objects {
    [DefaultClassOptions]
    [DefaultProperty("Subject")]
    [ImageName("BO_Note")]
    public class Issue {
        [Browsable(false)]
        public int Id { get; private set; }
        public string Subject { get; set; }
        public bool Active { get; set; }
    }
}
