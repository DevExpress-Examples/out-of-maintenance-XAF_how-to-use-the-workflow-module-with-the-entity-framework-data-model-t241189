<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128594856/15.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T241189)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [DemoAboutInfoController.cs](./CS/WorkflowDemoEF.Module.Win/DemoAboutInfoController.cs) (VB: [DemoAboutInfoController.vb](./VB/WorkflowDemoEF.Module.Win/DemoAboutInfoController.vb))
* [Model.DesignedDiffs.xafml](./CS/WorkflowDemoEF.Module.Win/Model.DesignedDiffs.xafml) (VB: [Model.DesignedDiffs.xafml](./VB/WorkflowDemoEF.Module.Win/Model.DesignedDiffs.xafml))
* [ReceiveCorrelationsDemoController.cs](./CS/WorkflowDemoEF.Module.Win/ReceiveCorrelationsDemoController.cs) (VB: [ReceiveCorrelationsDemoController.vb](./VB/WorkflowDemoEF.Module.Win/ReceiveCorrelationsDemoController.vb))
* [WinModule.cs](./CS/WorkflowDemoEF.Module.Win/WinModule.cs) (VB: [WinModule.vb](./VB/WorkflowDemoEF.Module.Win/WinModule.vb))
* [CreateTask.xaml](./CS/WorkflowDemoEF.Module/Activities/CreateTask.xaml) (VB: [CreateTask.xaml](./VB/WorkflowDemoEF.Module/Activities/CreateTask.xaml))
* [Model.DesignedDiffs.xafml](./CS/WorkflowDemoEF.Module/Model.DesignedDiffs.xafml) (VB: [Model.DesignedDiffs.xafml](./VB/WorkflowDemoEF.Module/Model.DesignedDiffs.xafml))
* [Module.cs](./CS/WorkflowDemoEF.Module/Module.cs) (VB: [Module.vb](./VB/WorkflowDemoEF.Module/Module.vb))
* [Issue.cs](./CS/WorkflowDemoEF.Module/Objects/Issue.cs) (VB: [Issue.vb](./VB/WorkflowDemoEF.Module/Objects/Issue.vb))
* [Task.cs](./CS/WorkflowDemoEF.Module/Objects/Task.cs) (VB: [Task.vb](./VB/WorkflowDemoEF.Module/Objects/Task.vb))
* [WorkflowDemoDbContext.cs](./CS/WorkflowDemoEF.Module/Objects/WorkflowDemoDbContext.cs) (VB: [WorkflowDemoDbContext.vb](./VB/WorkflowDemoEF.Module/Objects/WorkflowDemoDbContext.vb))
* [StartWorkflowViaReceiveActivityController.cs](./CS/WorkflowDemoEF.Module/StartWorkflowViaReceiveActivityController.cs) (VB: [StartWorkflowViaReceiveActivityController.vb](./VB/WorkflowDemoEF.Module/StartWorkflowViaReceiveActivityController.vb))
* [Updater.cs](./CS/WorkflowDemoEF.Module/Updater.cs) (VB: [Updater.vb](./VB/WorkflowDemoEF.Module/Updater.vb))
* [Model.xafml](./CS/WorkflowDemoEF.Win/Model.xafml) (VB: [Model.xafml](./VB/WorkflowDemoEF.Win/Model.xafml))
* [Program.cs](./CS/WorkflowDemoEF.Win/Program.cs) (VB: [Program.vb](./VB/WorkflowDemoEF.Win/Program.vb))
* [DemoApplicationVersionInfo.cs](./CS/WorkflowDemoEF.Win/Properties/DemoApplicationVersionInfo.cs)
* [WinApplication.cs](./CS/WorkflowDemoEF.Win/WinApplication.cs) (VB: [WinApplication.vb](./VB/WorkflowDemoEF.Win/WinApplication.vb))
* [WorkflowServerAuthentication.cs](./CS/WorkflowDemoEF.Win/WorkflowServerAuthentication.cs) (VB: [WorkflowServerAuthentication.vb](./VB/WorkflowDemoEF.Win/WorkflowServerAuthentication.vb))
<!-- default file list end -->
# OBSOLETE - How to use the Workflow Module with the Entity Framework 6 data model

>**NOTE**: We encourage our Entity Framework 6 (EF 6) users to consider Entity Framework Core (EF Core) for new XAF's Blazor and WinForms .NET Core projects.
Microsoft has moved EF 6 into maintenance mode, and as such, EF 6 will not mirror XAF’s .NET 5+ offering. At present, EF Core supports key XAF technologies/capabilities including advanced security and comprehensive audit trail support. EF Core also offers better performance when compared to EF 6. For more information, see [Porting from EF 6 to EF Core](https://docs.microsoft.com/en-us/ef/efcore-and-ef6/porting/) | [XAF Business Model Design with Entity Framework Core](https://docs.devexpress.com/eXpressAppFramework/401886/business-model-design-orm/business-model-design-with-entity-framework-core).

<p>This example uses the EF 6-based version of the XPO-based Workflow Demo application that is shipped with XAF. It demonstrates how to use the <a href="https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument113343">Workflow Module</a>  in XAF applications where <a href="https://msdn.microsoft.com/en-US/data/ef.aspx">Microsoft ADO.NET Entity Framework</a> is used for data access. The Workflow Module integrates <a href="https://msdn.microsoft.com/en-us/vstudio/jj684582.aspx">Windows Workflow Foundation(WF) 4.0/4.5</a> support into XAF. WF is a workflow management framework designed to assist you in creating more manageable, workflow-enabled applications. Leveraging the WF functionality, the Workflow module is designed specifically for applications that need to handle automatic long-running business processes with intermediate steps, for example, hours, days or weeks apart, allowing users to modify automated business processes without writing any code and visualizing complex business processes. <br /><br /></p>
<p><strong>Important: </strong><em>You need to run Visual Studio as an administrator to try this example. Otherwise, the "This task requires the application to have elevated permissions" error will occur. The reason is that this example starts the service that listens to the 46232 port.</em></p>

<br/>


