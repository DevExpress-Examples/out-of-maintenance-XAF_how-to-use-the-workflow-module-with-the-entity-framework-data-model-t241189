Imports System
Imports System.Activities
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Security
Imports System.ServiceModel
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Model.NodeGenerators
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.ExpressApp.Workflow
Imports DevExpress.ExpressApp.Workflow.EF
Imports DevExpress.Persistent.Base
Imports DevExpress.Workflow
Imports WorkflowDemoEF.Module.Objects

Namespace WorkflowDemoEF.Module
    Public Class Updater
        Inherits ModuleUpdater

        Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
            MyBase.New(objectSpace, currentDBVersion)
        End Sub

        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()
            CreateDemoObjects()
        End Sub

        Private Sub CreateDemoObjects()
            If ObjectSpace.GetObjects(Of Issue)().Count = 0 Then
                Dim issue As Issue = ObjectSpace.CreateObject(Of Issue)()
                issue.Subject = "Processed issue"
                issue.Active = False

                Dim issue2 As Issue = ObjectSpace.CreateObject(Of Issue)()
                issue2.Subject = "Active issue"
                issue2.Active = True
            End If

            If ObjectSpace.GetObjects(Of EFWorkflowDefinition)().Count = 0 Then
                Dim definition As EFWorkflowDefinition = ObjectSpace.CreateObject(Of EFWorkflowDefinition)()
                definition.Name = "Create Task for active Issue"
                definition.Xaml = CreateTaskForActiveIssueWorkflowXaml
                definition.TargetObjectType = GetType(Issue)
                definition.AutoStartWhenObjectFitsCriteria = True
                definition.Criteria = "[Active] = True"
                definition.IsActive = True

                Dim codeActivityDefinition As EFWorkflowDefinition = ObjectSpace.CreateObject(Of EFWorkflowDefinition)()
                codeActivityDefinition.Name = "Create Task for active Issue (Code Activity)"
                codeActivityDefinition.Xaml = CodeActivityCreateTaskForActiveIssueWorkflowXaml
                codeActivityDefinition.TargetObjectType = GetType(Issue)
                codeActivityDefinition.AutoStartWhenObjectFitsCriteria = True
                codeActivityDefinition.Criteria = "Contains([Subject], 'Code Activity')"
                codeActivityDefinition.IsActive = True

                Dim customStartWorkflowActivityDefinition As EFWorkflowDefinition = ObjectSpace.CreateObject(Of EFWorkflowDefinition)()
                customStartWorkflowActivityDefinition.Name = "Custom start workflow"
                customStartWorkflowActivityDefinition.Xaml = StartWorkflowViaReceiveAndCustomContractXaml
                customStartWorkflowActivityDefinition.TargetObjectType = GetType(WorkflowDemoEF.Module.Objects.Task)
                customStartWorkflowActivityDefinition.IsActive = True

                Dim receiveCorrelationsActivityDefinition As EFWorkflowDefinition = ObjectSpace.CreateObject(Of EFWorkflowDefinition)()
                receiveCorrelationsActivityDefinition.Name = "Start/stop (correlations) demo"
                receiveCorrelationsActivityDefinition.Xaml = ReceiveCorrelationsXaml
                receiveCorrelationsActivityDefinition.TargetObjectType = GetType(WorkflowDemoEF.Module.Objects.Task)
                receiveCorrelationsActivityDefinition.IsActive = True
            End If
            ObjectSpace.CommitChanges()
        End Sub

        Private Const CreateTaskForActiveIssueWorkflowXaml As String = "<Activity mc:Ignorable=""sap"" x:Class=""DevExpress.Workflow.xWF1"" xmlns=""http://schemas.microsoft.com/netfx/2009/xaml/activities"" " & " xmlns:dpb=""clr-namespace:DevExpress.Persistent.BaseImpl.EF;assembly=DevExpress.Persistent.BaseImpl.EF.v" & AssemblyInfo.VersionShort & """" & " xmlns:dwa=""clr-namespace:DevExpress.Workflow.Activities;assembly=DevExpress.Workflow.Activities.v" & AssemblyInfo.VersionShort & """" & " xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006"" xmlns:mva=""clr-namespace:Microsoft.VisualBasic.Activities;assembly=System.Activities"" xmlns:s=""clr-namespace:System;assembly=mscorlib"" xmlns:s1=""clr-namespace:System;assembly=System"" xmlns:s2=""clr-namespace:System;assembly=System.Core"" xmlns:s3=""clr-namespace:System;assembly=System.ServiceModel"" xmlns:s4=""clr-namespace:System;assembly=System.Drawing.Design"" xmlns:s5=""clr-namespace:System;assembly=System.Configuration.Install"" xmlns:s6=""clr-namespace:System;assembly=System.DirectoryServices.Protocols"" xmlns:sa=""clr-namespace:System.Activities;assembly=System.Activities"" xmlns:sap=""http://schemas.microsoft.com/netfx/2009/xaml/activities/presentation"" xmlns:wc=""clr-namespace:WorkflowDemoEF.Module.Objects;assembly=WorkflowDemoEF.Module"" xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">" & ControlChars.CrLf & _
"  <x:Members>" & ControlChars.CrLf & _
"    <x:Property Name=""targetObjectId"" Type=""InArgument(x:Object)"" />" & ControlChars.CrLf & _
"  </x:Members>" & ControlChars.CrLf & _
"  <sap:VirtualizedContainerService.HintSize>307.2,412.8</sap:VirtualizedContainerService.HintSize>" & ControlChars.CrLf & _
"  <mva:VisualBasic.Settings>Assembly references and imported namespaces serialized as XML namespaces</mva:VisualBasic.Settings>" & ControlChars.CrLf & _
"  <dwa:ObjectSpaceTransactionScope AutoCommit=""True"" sap:VirtualizedContainerService.HintSize=""267.2,372.8"" mva:VisualBasic.Settings=""Assembly references and imported namespaces serialized as XML namespaces"">" & ControlChars.CrLf & _
"    <dwa:ObjectSpaceTransactionScope.Variables>" & ControlChars.CrLf & _
"      <Variable x:TypeArguments=""wc:Issue"" Name=""issue"" />" & ControlChars.CrLf & _
"      <Variable x:TypeArguments=""wc:Task"" Name=""task"" />" & ControlChars.CrLf & _
"    </dwa:ObjectSpaceTransactionScope.Variables>" & ControlChars.CrLf & _
"    <dwa:GetObjectByKey x:TypeArguments=""wc:Issue"" sap:VirtualizedContainerService.HintSize=""241.6,59.2"" Key=""[targetObjectId]"" Result=""[issue]"" />" & ControlChars.CrLf & _
"    <dwa:CreateObject x:TypeArguments=""wc:Task"" sap:VirtualizedContainerService.HintSize=""241.6,22.4"" Result=""[task]"" />" & ControlChars.CrLf & _
"    <Assign sap:VirtualizedContainerService.HintSize=""241.6,59.2"">" & ControlChars.CrLf & _
"      <Assign.To>" & ControlChars.CrLf & _
"        <OutArgument x:TypeArguments=""x:String"">[task.Subject]</OutArgument>" & ControlChars.CrLf & _
"      </Assign.To>" & ControlChars.CrLf & _
"      <Assign.Value>" & ControlChars.CrLf & _
"        <InArgument x:TypeArguments=""x:String"">[""New active issue: "" + issue.Subject]</InArgument>" & ControlChars.CrLf & _
"      </Assign.Value>" & ControlChars.CrLf & _
"    </Assign>" & ControlChars.CrLf & _
"    <Assign sap:VirtualizedContainerService.HintSize=""241.6,59.2"">" & ControlChars.CrLf & _
"      <Assign.To>" & ControlChars.CrLf & _
"        <OutArgument x:TypeArguments=""wc:Issue"">[task.Issue]</OutArgument>" & ControlChars.CrLf & _
"      </Assign.To>" & ControlChars.CrLf & _
"      <Assign.Value>" & ControlChars.CrLf & _
"        <InArgument x:TypeArguments=""wc:Issue"">[issue]</InArgument>" & ControlChars.CrLf & _
"      </Assign.Value>" & ControlChars.CrLf & _
"    </Assign>" & ControlChars.CrLf & _
"    <Delay Duration=""00:00:01"" sap:VirtualizedContainerService.HintSize=""242,22"" />" & ControlChars.CrLf & _
"  </dwa:ObjectSpaceTransactionScope>" & ControlChars.CrLf & _
"</Activity>"

        Private Const CodeActivityCreateTaskForActiveIssueWorkflowXaml As String = "<Activity mc:Ignorable=""sap"" x:Class=""DevExpress.Workflow.xWF1"" xmlns=""http://schemas.microsoft.com/netfx/2009/xaml/activities"" xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006"" xmlns:mva=""clr-namespace:Microsoft.VisualBasic.Activities;assembly=System.Activities"" xmlns:s=""clr-namespace:System;assembly=mscorlib"" xmlns:s1=""clr-namespace:System;assembly=System"" xmlns:s2=""clr-namespace:System;assembly=System.Core"" xmlns:s3=""clr-namespace:System;assembly=System.ServiceModel"" xmlns:sa=""clr-namespace:System.Activities;assembly=System.Activities"" xmlns:sap=""http://schemas.microsoft.com/netfx/2009/xaml/activities/presentation"" xmlns:scg=""clr-namespace:System.Collections.Generic;assembly=mscorlib"" xmlns:wm=""clr-namespace:WorkflowDemoEF.Module.Activities;assembly=WorkflowDemoEF.Module"" xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">" & ControlChars.CrLf & _
"  <x:Members>" & ControlChars.CrLf & _
"    <x:Property Name=""targetObjectId"" Type=""InArgument(x:Object)"" />" & ControlChars.CrLf & _
"  </x:Members>" & ControlChars.CrLf & _
"  <sap:VirtualizedContainerService.HintSize>273,285</sap:VirtualizedContainerService.HintSize>" & ControlChars.CrLf & _
"  <mva:VisualBasic.Settings>Assembly references and imported namespaces serialized as XML namespaces</mva:VisualBasic.Settings>" & ControlChars.CrLf & _
"  <Sequence sap:VirtualizedContainerService.HintSize=""233,245"" mva:VisualBasic.Settings=""Assembly references and imported namespaces serialized as XML namespaces"">" & ControlChars.CrLf & _
"    <Sequence.Variables>" & ControlChars.CrLf & _
"      <Variable x:TypeArguments=""x:Object"" Name=""createdTaskKey"" />" & ControlChars.CrLf & _
"    </Sequence.Variables>" & ControlChars.CrLf & _
"    <sap:WorkflowViewStateService.ViewState>" & ControlChars.CrLf & _
"      <scg:Dictionary x:TypeArguments=""x:String, x:Object"">" & ControlChars.CrLf & _
"        <x:Boolean x:Key=""IsExpanded"">True</x:Boolean>" & ControlChars.CrLf & _
"      </scg:Dictionary>" & ControlChars.CrLf & _
"    </sap:WorkflowViewStateService.ViewState>" & ControlChars.CrLf & _
"    <wm:CreateTask sap:VirtualizedContainerService.HintSize=""211,22"" createdTaskKey=""[createdTaskKey]"" issueKey=""[targetObjectId]"" />" & ControlChars.CrLf & _
"    <WriteLine sap:VirtualizedContainerService.HintSize=""211,59"" Text=""[&quot;Write to track record: &quot; + createdTaskKey.ToString()]"" />" & ControlChars.CrLf & _
"  </Sequence>" & ControlChars.CrLf & _
"</Activity>"

        Private Shared StartWorkflowViaReceiveAndCustomContractXaml As String = "<Activity mc:Ignorable=""sap"" x:Class=""DevExpress.Workflow.xWF1"" sap:VirtualizedContainerService.HintSize=""812,627"" mva:VisualBasic.Settings=""Assembly references and imported namespaces serialized as XML namespaces"" xmlns=""http://schemas.microsoft.com/netfx/2009/xaml/activities"" " & " xmlns:dpb=""clr-namespace:DevExpress.Persistent.BaseImpl.EF;assembly=DevExpress.Persistent.BaseImpl.EF.v" & AssemblyInfo.VersionShort & """" & " xmlns:dwa=""clr-namespace:DevExpress.Workflow.Activities;assembly=DevExpress.Workflow.Activities.v" & AssemblyInfo.VersionShort & """" & " xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006"" xmlns:mva=""clr-namespace:Microsoft.VisualBasic.Activities;assembly=System.Activities"" xmlns:p=""http://schemas.microsoft.com/netfx/2009/xaml/servicemodel"" xmlns:s=""clr-namespace:System;assembly=mscorlib"" xmlns:s1=""clr-namespace:System;assembly=System"" xmlns:s2=""clr-namespace:System;assembly=System.Core"" xmlns:s3=""clr-namespace:System;assembly=System.ServiceModel"" xmlns:sa=""clr-namespace:System.Activities;assembly=System.Activities"" xmlns:sap=""http://schemas.microsoft.com/netfx/2009/xaml/activities/presentation"" xmlns:scg=""clr-namespace:System.Collections.Generic;assembly=mscorlib"" xmlns:wmo=""clr-namespace:WorkflowDemoEF.Module.Objects;assembly=WorkflowDemoEF.Module"" xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">" & ControlChars.CrLf & _
"  <Sequence sap:VirtualizedContainerService.HintSize=""772,587"" mva:VisualBasic.Settings=""Assembly references and imported namespaces serialized as XML namespaces"">" & ControlChars.CrLf & _
"    <sap:WorkflowViewStateService.ViewState>" & ControlChars.CrLf & _
"      <scg:Dictionary x:TypeArguments=""x:String, x:Object"">" & ControlChars.CrLf & _
"        <x:Boolean x:Key=""IsExpanded"">True</x:Boolean>" & ControlChars.CrLf & _
"      </scg:Dictionary>" & ControlChars.CrLf & _
"    </sap:WorkflowViewStateService.ViewState>" & ControlChars.CrLf & _
"    <Pick sap:VirtualizedContainerService.HintSize=""750,463"">" & ControlChars.CrLf & _
"      <PickBranch sap:VirtualizedContainerService.HintSize=""298,417"">" & ControlChars.CrLf & _
"        <PickBranch.Variables>" & ControlChars.CrLf & _
"          <Variable x:TypeArguments=""x:Int32"" Name=""intData"" />" & ControlChars.CrLf & _
"        </PickBranch.Variables>" & ControlChars.CrLf & _
"        <PickBranch.Trigger>" & ControlChars.CrLf & _
"          <p:Receive CanCreateInstance=""True"" sap:VirtualizedContainerService.HintSize=""268,100"" OperationName=""PassIntegerData"" ServiceContractName=""IPassIntegerData"">" & ControlChars.CrLf & _
"            <p:ReceiveParametersContent>" & ControlChars.CrLf & _
"              <OutArgument x:TypeArguments=""x:Int32"" x:Key=""data"">[intData]</OutArgument>" & ControlChars.CrLf & _
"            </p:ReceiveParametersContent>" & ControlChars.CrLf & _
"          </p:Receive>" & ControlChars.CrLf & _
"        </PickBranch.Trigger>" & ControlChars.CrLf & _
"        <dwa:ObjectSpaceTransactionScope AutoCommit=""True"" sap:VirtualizedContainerService.HintSize=""268,203"">" & ControlChars.CrLf & _
"          <dwa:ObjectSpaceTransactionScope.Variables>" & ControlChars.CrLf & _
"            <Variable x:TypeArguments=""wmo:Task"" Name=""task"" />" & ControlChars.CrLf & _
"          </dwa:ObjectSpaceTransactionScope.Variables>" & ControlChars.CrLf & _
"          <dwa:CreateObject x:TypeArguments=""wmo:Task"" sap:VirtualizedContainerService.HintSize=""242,22"" Result=""[task]"" />" & ControlChars.CrLf & _
"          <Assign sap:VirtualizedContainerService.HintSize=""242,58"">" & ControlChars.CrLf & _
"            <Assign.To>" & ControlChars.CrLf & _
"              <OutArgument x:TypeArguments=""x:String"">[task.Subject]</OutArgument>" & ControlChars.CrLf & _
"            </Assign.To>" & ControlChars.CrLf & _
"            <Assign.Value>" & ControlChars.CrLf & _
"              <InArgument x:TypeArguments=""x:String"">[""Integer data:"" + intData.ToString()]</InArgument>" & ControlChars.CrLf & _
"            </Assign.Value>" & ControlChars.CrLf & _
"          </Assign>" & ControlChars.CrLf & _
"        </dwa:ObjectSpaceTransactionScope>" & ControlChars.CrLf & _
"      </PickBranch>" & ControlChars.CrLf & _
"      <PickBranch sap:VirtualizedContainerService.HintSize=""298,417"">" & ControlChars.CrLf & _
"        <PickBranch.Variables>" & ControlChars.CrLf & _
"          <Variable x:TypeArguments=""x:String"" Name=""stringData"" />" & ControlChars.CrLf & _
"        </PickBranch.Variables>" & ControlChars.CrLf & _
"        <PickBranch.Trigger>" & ControlChars.CrLf & _
"          <p:Receive CanCreateInstance=""True"" sap:VirtualizedContainerService.HintSize=""268,100"" OperationName=""PassStringData"" ServiceContractName=""IPassStringData"">" & ControlChars.CrLf & _
"            <p:ReceiveParametersContent>" & ControlChars.CrLf & _
"              <OutArgument x:TypeArguments=""x:String"" x:Key=""data"">[stringData]</OutArgument>" & ControlChars.CrLf & _
"            </p:ReceiveParametersContent>" & ControlChars.CrLf & _
"          </p:Receive>" & ControlChars.CrLf & _
"        </PickBranch.Trigger>" & ControlChars.CrLf & _
"        <dwa:ObjectSpaceTransactionScope AutoCommit=""True"" sap:VirtualizedContainerService.HintSize=""268,203"">" & ControlChars.CrLf & _
"          <dwa:ObjectSpaceTransactionScope.Variables>" & ControlChars.CrLf & _
"            <Variable x:TypeArguments=""wmo:Task"" Name=""task"" />" & ControlChars.CrLf & _
"          </dwa:ObjectSpaceTransactionScope.Variables>" & ControlChars.CrLf & _
"          <dwa:CreateObject x:TypeArguments=""wmo:Task"" sap:VirtualizedContainerService.HintSize=""242,22"" Result=""[task]"" />" & ControlChars.CrLf & _
"          <Assign sap:VirtualizedContainerService.HintSize=""242,58"">" & ControlChars.CrLf & _
"            <Assign.To>" & ControlChars.CrLf & _
"              <OutArgument x:TypeArguments=""x:String"">[task.Subject]</OutArgument>" & ControlChars.CrLf & _
"            </Assign.To>" & ControlChars.CrLf & _
"            <Assign.Value>" & ControlChars.CrLf & _
"              <InArgument x:TypeArguments=""x:String"">[""String data:"" + stringData]</InArgument>" & ControlChars.CrLf & _
"            </Assign.Value>" & ControlChars.CrLf & _
"          </Assign>" & ControlChars.CrLf & _
"        </dwa:ObjectSpaceTransactionScope>" & ControlChars.CrLf & _
"      </PickBranch>" & ControlChars.CrLf & _
"    </Pick>" & ControlChars.CrLf & _
"  </Sequence>" & ControlChars.CrLf & _
"</Activity>"

        Private Const ReceiveCorrelationsXaml As String = "<Activity mc:Ignorable=""sap"" x:Class=""DevExpress.Workflow.xWF1"" sap:VirtualizedContainerService.HintSize=""330,1198"" mva:VisualBasic.Settings=""Assembly references and imported namespaces for internal implementation"" xmlns=""http://schemas.microsoft.com/netfx/2009/xaml/activities"" " & " xmlns:dpb=""clr-namespace:DevExpress.Persistent.BaseImpl.EF;assembly=DevExpress.Persistent.BaseImpl.EF.v" & AssemblyInfo.VersionShort & """" & " xmlns:dwa=""clr-namespace:DevExpress.Workflow.Activities;assembly=DevExpress.Workflow.Activities.v" & AssemblyInfo.VersionShort & """" & " xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006"" xmlns:mv=""clr-namespace:Microsoft.VisualBasic;assembly=System"" xmlns:mva=""clr-namespace:Microsoft.VisualBasic.Activities;assembly=System.Activities"" xmlns:p=""http://schemas.microsoft.com/netfx/2009/xaml/servicemodel"" xmlns:s=""clr-namespace:System;assembly=mscorlib"" xmlns:s1=""clr-namespace:System;assembly=System"" xmlns:s2=""clr-namespace:System;assembly=System.Xml"" xmlns:s3=""clr-namespace:System;assembly=System.Core"" xmlns:s4=""clr-namespace:System;assembly=System.ServiceModel"" xmlns:sa=""clr-namespace:System.Activities;assembly=System.Activities"" xmlns:sad=""clr-namespace:System.Activities.Debugger;assembly=System.Activities"" xmlns:sap=""http://schemas.microsoft.com/netfx/2009/xaml/activities/presentation"" xmlns:scg=""clr-namespace:System.Collections.Generic;assembly=System"" xmlns:scg1=""clr-namespace:System.Collections.Generic;assembly=System.ServiceModel"" xmlns:scg2=""clr-namespace:System.Collections.Generic;assembly=System.Core"" xmlns:scg3=""clr-namespace:System.Collections.Generic;assembly=mscorlib"" xmlns:sd=""clr-namespace:System.Data;assembly=System.Data"" xmlns:sl=""clr-namespace:System.Linq;assembly=System.Core"" xmlns:ssa=""clr-namespace:System.ServiceModel.Activities;assembly=System.ServiceModel.Activities"" xmlns:ssx=""clr-namespace:System.ServiceModel.XamlIntegration;assembly=System.ServiceModel"" xmlns:st=""clr-namespace:System.Text;assembly=mscorlib"" xmlns:wmo=""clr-namespace:WorkflowDemoEF.Module.Objects;assembly=WorkflowDemoEF.Module"" xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">" & ControlChars.CrLf & _
"  <Sequence DisplayName=""Main"" sap:VirtualizedContainerService.HintSize=""290,1158"">" & ControlChars.CrLf & _
"    <Sequence.Variables>" & ControlChars.CrLf & _
"      <Variable x:TypeArguments=""p:CorrelationHandle"" Name=""clientIdHandle"" />" & ControlChars.CrLf & _
"      <Variable x:TypeArguments=""x:String"" Name=""localClientId"" />" & ControlChars.CrLf & _
"      <Variable x:TypeArguments=""s:Int32"" Name=""taskId"" />" & ControlChars.CrLf & _
"    </Sequence.Variables>" & ControlChars.CrLf & _
"    <sap:WorkflowViewStateService.ViewState>" & ControlChars.CrLf & _
"      <scg3:Dictionary x:TypeArguments=""x:String, x:Object"">" & ControlChars.CrLf & _
"        <x:Boolean x:Key=""IsExpanded"">True</x:Boolean>" & ControlChars.CrLf & _
"      </scg3:Dictionary>" & ControlChars.CrLf & _
"    </sap:WorkflowViewStateService.ViewState>" & ControlChars.CrLf & _
"    <p:Receive x:Name=""__ReferenceID0"" CanCreateInstance=""True"" CorrelatesWith=""[clientIdHandle]"" sap:VirtualizedContainerService.HintSize=""268,86"" OperationName=""Start"" ServiceContractName=""IStartStop"">" & ControlChars.CrLf & _
"      <p:Receive.CorrelatesOn>" & ControlChars.CrLf & _
"        <p:XPathMessageQuery x:Key=""key1"">" & ControlChars.CrLf & _
"          <p:XPathMessageQuery.Namespaces>" & ControlChars.CrLf & _
"            <ssx:XPathMessageContextMarkup>" & ControlChars.CrLf & _
"              <x:String x:Key=""xgSc"">http://tempuri.org/</x:String>" & ControlChars.CrLf & _
"            </ssx:XPathMessageContextMarkup>" & ControlChars.CrLf & _
"          </p:XPathMessageQuery.Namespaces>sm:body()/xgSc:Start/xgSc:clientId</p:XPathMessageQuery>" & ControlChars.CrLf & _
"      </p:Receive.CorrelatesOn>" & ControlChars.CrLf & _
"      <p:ReceiveParametersContent>" & ControlChars.CrLf & _
"        <OutArgument x:TypeArguments=""x:String"" x:Key=""clientId"">[localClientId]</OutArgument>" & ControlChars.CrLf & _
"      </p:ReceiveParametersContent>" & ControlChars.CrLf & _
"    </p:Receive>" & ControlChars.CrLf & _
"    <dwa:ObjectSpaceTransactionScope AutoCommit=""True"" sap:VirtualizedContainerService.HintSize=""268,333"">" & ControlChars.CrLf & _
"      <dwa:ObjectSpaceTransactionScope.Variables>" & ControlChars.CrLf & _
"        <Variable x:TypeArguments=""wmo:Task"" Name=""task"" />" & ControlChars.CrLf & _
"      </dwa:ObjectSpaceTransactionScope.Variables>" & ControlChars.CrLf & _
"      <dwa:CreateObject x:TypeArguments=""wmo:Task"" sap:VirtualizedContainerService.HintSize=""242,22"" Result=""[task]"" />" & ControlChars.CrLf & _
"      <Assign sap:VirtualizedContainerService.HintSize=""242,58"">" & ControlChars.CrLf & _
"        <Assign.To>" & ControlChars.CrLf & _
"          <OutArgument x:TypeArguments=""x:String"">[task.Subject]</OutArgument>" & ControlChars.CrLf & _
"        </Assign.To>" & ControlChars.CrLf & _
"        <Assign.Value>" & ControlChars.CrLf & _
"          <InArgument x:TypeArguments=""x:String"">[""Task for client "" + localClientId]</InArgument>" & ControlChars.CrLf & _
"        </Assign.Value>" & ControlChars.CrLf & _
"      </Assign>" & ControlChars.CrLf & _
"      <dwa:CommitChanges sap:VirtualizedContainerService.HintSize=""242,22"" />" & ControlChars.CrLf & _
"      <Assign sap:VirtualizedContainerService.HintSize=""242,58"">" & ControlChars.CrLf & _
"        <Assign.To>" & ControlChars.CrLf & _
"          <OutArgument x:TypeArguments=""s:Int32"">[taskId]</OutArgument>" & ControlChars.CrLf & _
"        </Assign.To>" & ControlChars.CrLf & _
"        <Assign.Value>" & ControlChars.CrLf & _
"          <InArgument x:TypeArguments=""s:Int32"">[task.Id]</InArgument>" & ControlChars.CrLf & _
"        </Assign.Value>" & ControlChars.CrLf & _
"      </Assign>" & ControlChars.CrLf & _
"    </dwa:ObjectSpaceTransactionScope>" & ControlChars.CrLf & _
"    <p:SendReply Request=""{x:Reference __ReferenceID0}"" DisplayName=""SendReplyToReceive"" sap:VirtualizedContainerService.HintSize=""268,86"">" & ControlChars.CrLf & _
"      <p:SendParametersContent>" & ControlChars.CrLf & _
"        <InArgument x:TypeArguments=""x:String"" x:Key=""result"">[""Task is created for client "" + localClientId + "".""]</InArgument>" & ControlChars.CrLf & _
"      </p:SendParametersContent>" & ControlChars.CrLf & _
"    </p:SendReply>" & ControlChars.CrLf & _
"    <p:Receive x:Name=""__ReferenceID1"" CorrelatesWith=""[clientIdHandle]"" sap:VirtualizedContainerService.HintSize=""268,86"" OperationName=""Stop"" ServiceContractName=""IStartStop"">" & ControlChars.CrLf & _
"      <p:Receive.CorrelatesOn>" & ControlChars.CrLf & _
"        <p:XPathMessageQuery x:Key=""key1"">" & ControlChars.CrLf & _
"          <p:XPathMessageQuery.Namespaces>" & ControlChars.CrLf & _
"            <ssx:XPathMessageContextMarkup>" & ControlChars.CrLf & _
"              <x:String x:Key=""xgSc"">http://tempuri.org/</x:String>" & ControlChars.CrLf & _
"            </ssx:XPathMessageContextMarkup>" & ControlChars.CrLf & _
"          </p:XPathMessageQuery.Namespaces>sm:body()/xgSc:Stop/xgSc:clientId</p:XPathMessageQuery>" & ControlChars.CrLf & _
"      </p:Receive.CorrelatesOn>" & ControlChars.CrLf & _
"      <p:ReceiveParametersContent>" & ControlChars.CrLf & _
"        <OutArgument x:TypeArguments=""x:String"" x:Key=""clientId"" />" & ControlChars.CrLf & _
"      </p:ReceiveParametersContent>" & ControlChars.CrLf & _
"    </p:Receive>" & ControlChars.CrLf & _
"    <dwa:ObjectSpaceTransactionScope AutoCommit=""True"" sap:VirtualizedContainerService.HintSize=""268,157"">" & ControlChars.CrLf & _
"      <dwa:DeleteObject x:TypeArguments=""wmo:Task"" sap:VirtualizedContainerService.HintSize=""200,59"" Key=""[taskId]"" />" & ControlChars.CrLf & _
"    </dwa:ObjectSpaceTransactionScope>" & ControlChars.CrLf & _
"    <p:SendReply Request=""{x:Reference __ReferenceID1}"" DisplayName=""SendReplyToReceive"" sap:VirtualizedContainerService.HintSize=""268,86"">" & ControlChars.CrLf & _
"      <p:SendParametersContent>" & ControlChars.CrLf & _
"        <InArgument x:TypeArguments=""x:String"" x:Key=""result"">[""Task for client "" + localClientId + "" was deleted.""]</InArgument>" & ControlChars.CrLf & _
"      </p:SendParametersContent>" & ControlChars.CrLf & _
"    </p:SendReply>" & ControlChars.CrLf & _
"  </Sequence>" & ControlChars.CrLf & _
"</Activity>"


    End Class
End Namespace



