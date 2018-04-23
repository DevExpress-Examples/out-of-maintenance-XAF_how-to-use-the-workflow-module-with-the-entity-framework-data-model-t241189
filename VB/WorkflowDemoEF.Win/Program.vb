Imports System
Imports System.Configuration
Imports System.Windows.Forms
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.MiddleTier
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.ExpressApp.Workflow
Imports DevExpress.ExpressApp.Workflow.CommonServices
Imports DevExpress.ExpressApp.Workflow.Server
Imports DevExpress.ExpressApp.Workflow.Versioning
Imports DevExpress.ExpressApp.Workflow.Win
Imports DevExpress.Persistent.Base
Imports WorkflowDemoEF.Module
Imports WorkflowDemoEF.Module.Activities
Imports DevExpress.Internal
Imports WorkflowDemoEF.Module.Objects
Imports DevExpress.ExpressApp.EF
Imports DevExpress.Workflow.Utils
Imports DevExpress.Workflow

Namespace WorkflowDemoEF.Win
    Friend NotInheritable Class Program

        Private Sub New()
        End Sub

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread> _
        Shared Sub Main(ByVal arguments() As String)

            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Dim xafApplication As New WorkflowDemoEFWindowsFormsApplication()
#If DEBUG Then
            DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register()
#End If

            Dim connectionStringSettings As ConnectionStringSettings = ConfigurationManager.ConnectionStrings("ConnectionString")
            If connectionStringSettings IsNot Nothing Then
                xafApplication.ConnectionString = connectionStringSettings.ConnectionString
            ElseIf String.IsNullOrEmpty(xafApplication.ConnectionString) AndAlso xafApplication.Connection Is Nothing Then
                connectionStringSettings = ConfigurationManager.ConnectionStrings("SqlExpressConnectionString")
                If connectionStringSettings IsNot Nothing Then
                    xafApplication.ConnectionString = DbEngineDetector.PatchConnectionString(connectionStringSettings.ConnectionString)
                End If
            End If

            AddHandler xafApplication.Modules.FindModule(Of WorkflowWindowsFormsModule)().QueryAvailableActivities, Sub(sender As Object, e As ActivitiesInformationEventArgs)
                e.ActivitiesInformation.Add(New ActivityInformation(GetType(CreateTask), "Code Activities", "Create Task", ImageLoader.Instance.GetImageInfo("CreateTask").Image))
            End Sub

            Dim starter As WorkflowServerStarter = Nothing
            AddHandler xafApplication.LoggedOn, Sub(sender As Object, e As LogonEventArgs)
                If starter Is Nothing Then
                    starter = New WorkflowServerStarter()
                    AddHandler starter.OnCustomHandleException, Sub(sender1 As Object, args1 As ExceptionEventArgs)
                        MessageBox.Show(args1.Message)
                    End Sub

                    starter.Start(xafApplication.ConnectionString, xafApplication.ApplicationName)
                End If
            End Sub

            Try
                xafApplication.Setup()
                xafApplication.Start()
            Catch e As Exception
                xafApplication.HandleException(e)
            End Try

            If starter IsNot Nothing Then
                starter.Stop()
            End If
        End Sub
    End Class

    <Serializable> _
    Public Class ExceptionEventArgs
        Inherits EventArgs

        Public Sub New(ByVal message As String)
            Me.Message = message
        End Sub
        Private privateMessage As String
        Public Property Message() As String
            Get
                Return privateMessage
            End Get
            Private Set(ByVal value As String)
                privateMessage = value
            End Set
        End Property
    End Class

    Public Class WorkflowServerStarter
        Inherits MarshalByRefObject

        Private Class ServerApplication
            Inherits XafApplication

            Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
                args.ObjectSpaceProvider = New EFObjectSpaceProvider(GetType(WorkflowDemoDbContext),args.ConnectionString)
            End Sub
            Protected Overrides Function CreateLayoutManagerCore(ByVal simple As Boolean) As DevExpress.ExpressApp.Layout.LayoutManager
                Throw New NotImplementedException()
            End Function
            Public Sub Logon()
                MyBase.Logon(Nothing)
            End Sub
        End Class
        Private Shared starter As WorkflowServerStarter
        Private server As WorkflowServer
        Private domain As AppDomain
        Private Sub starter_OnCustomHandleException_(ByVal sender As Object, ByVal e As ExceptionEventArgs)
            RaiseEvent OnCustomHandleException(Nothing, e)
        End Sub
        Private Sub Start_(ByVal connectionString As String, ByVal applicationName As String)
            Dim serverApplication As New ServerApplication()
            serverApplication.ApplicationName = applicationName
            serverApplication.Modules.Add(New WorkflowDemoEFModule())
            serverApplication.ConnectionString = connectionString
            Dim workflowModule As WorkflowModule = serverApplication.Modules.FindModule(Of WorkflowModule)()
            serverApplication.Setup()
            serverApplication.Logon()

            Dim objectSpaceProvider As IObjectSpaceProvider = serverApplication.ObjectSpaceProvider

            server = New WorkflowServer("http://localhost:46232", objectSpaceProvider, objectSpaceProvider)
            server.StartWorkflowListenerService.DelayPeriod = TimeSpan.FromSeconds(10)
            server.StartWorkflowByRequestService.DelayPeriod = TimeSpan.FromSeconds(10)
            server.RefreshWorkflowDefinitionsService.DelayPeriod = TimeSpan.FromSeconds(15)
            AddHandler server.CustomizeHost, Sub(sender As Object, e As CustomizeHostEventArgs)
            '    // NOTE: Uncomment this section to use alternative workflow configuration.
            '    //
            '    // SqlWorkflowInstanceStoreBehavior
            '    //
            '    //e.WorkflowInstanceStoreBehavior = null;
            '    //System.ServiceModel.Activities.Description.SqlWorkflowInstanceStoreBehavior sqlWorkflowInstanceStoreBehavior = new System.ServiceModel.Activities.Description.SqlWorkflowInstanceStoreBehavior("Integrated Security=SSPI;Pooling=false;Data Source=(local);Initial Catalog=WorkflowsStore");
            '    //sqlWorkflowInstanceStoreBehavior.RunnableInstancesDetectionPeriod = TimeSpan.FromSeconds(2);
            '    //e.Host.Description.Behaviors.Add(sqlWorkflowInstanceStoreBehavior);
                'e.WorkflowIdleBehavior.TimeToPersist = TimeSpan.FromSeconds(10);
                e.WorkflowIdleBehavior.TimeToUnload = TimeSpan.FromSeconds(10)
                e.WorkflowInstanceStoreBehavior.WorkflowInstanceStore.RunnableInstancesDetectionPeriod = TimeSpan.FromSeconds(1)
            End Sub

            AddHandler server.CustomHandleException, Sub(sender As Object, e As CustomHandleServiceExceptionEventArgs)
                Tracing.Tracer.LogError(e.Exception)
                RaiseEvent OnCustomHandleException_(Me, New ExceptionEventArgs("Exception occurs:" & ControlChars.CrLf & ControlChars.CrLf & e.Exception.Message & ControlChars.CrLf & ControlChars.CrLf & "'" & e.Service.GetType().FullName & "' service"))
                e.Handled = True
            End Sub
            server.Start()
        End Sub
        Private Sub Stop_()
            server.Stop()
        End Sub
        Public Sub Start(ByVal connectionString As String, ByVal applicationName As String)
            Try
                domain = AppDomain.CreateDomain("ServerDomain")
                starter = DirectCast(domain.CreateInstanceAndUnwrap(System.Reflection.Assembly.GetEntryAssembly().FullName, GetType(WorkflowServerStarter).FullName), WorkflowServerStarter)
                AddHandler starter.OnCustomHandleException_, AddressOf starter_OnCustomHandleException_
                starter.Start_(connectionString, applicationName)
            Catch e As Exception
                Tracing.Tracer.LogError(e)
                RaiseEvent OnCustomHandleException(Nothing, New ExceptionEventArgs("Exception occurs:" & ControlChars.CrLf & ControlChars.CrLf & e.Message))
            End Try
        End Sub
        Public Sub [Stop]()
            If starter IsNot Nothing Then
                starter.Stop_()
            End If
            If domain IsNot Nothing Then
                AppDomain.Unload(domain)
            End If
        End Sub
        Public Event OnCustomHandleException_ As EventHandler(Of ExceptionEventArgs)
        Public Event OnCustomHandleException As EventHandler(Of ExceptionEventArgs)
    End Class
End Namespace
