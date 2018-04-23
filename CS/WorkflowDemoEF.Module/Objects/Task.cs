using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WorkflowDemoEF.Module.Objects {
	[DefaultClassOptions]
    [ImageName("BO_Task")]
    public class Task {
        [Browsable(false)]
        public int Id { get; private set; }
        public string Subject { get; set; }
        public virtual Issue Issue { get; set; }
	}
}
