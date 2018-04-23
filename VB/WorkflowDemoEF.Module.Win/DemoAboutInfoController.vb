Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.ExpressApp.Win.SystemModule
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.Utils.About

Namespace Demo.Module.Win
    Public Class DemoAboutInfoController
        Inherits AboutInfoController

        Private demoAboutInfoAction As SimpleAction

        Public Sub New()
            MyBase.New()
            demoAboutInfoAction = New SimpleAction(Me, "Demo About Info", DevExpress.Persistent.Base.PredefinedCategory.About)
            demoAboutInfoAction.Caption = "About..."
        End Sub
        Protected Overrides Sub OnActivated()
            MyBase.OnActivated()
            demoAboutInfoAction.ImageName = AboutInfoAction.ImageName
            AboutInfoAction.Active("DemoDisabled") = False
            AddHandler demoAboutInfoAction.Execute, AddressOf demoAboutInfoAction_Execute
        End Sub
        Protected Overrides Sub OnDeactivated()
            MyBase.OnDeactivated()
            RemoveHandler demoAboutInfoAction.Execute, AddressOf demoAboutInfoAction_Execute
        End Sub

        Private Sub demoAboutInfoAction_Execute(ByVal sender As Object, ByVal e As SimpleActionExecuteEventArgs)
            DevExpress.Utils.About.AboutHelper.Show(DevExpress.Utils.About.ProductKind.XAF, New DevExpress.Utils.About.ProductStringInfo("DXperience Universal", "eXpressApp Framework"))
        End Sub
    End Class
End Namespace
