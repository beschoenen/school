using System;
using System.Windows;
using Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOh_ParkInspect.Enum;
using SOh_ParkInspect.Helper;
using SOh_ParkInspect.Model;

namespace Tests.Helper
{
    [TestClass]
    public class RouterTests
    {
        private RouterService _router;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            Application.LoadComponent(new Uri("/SOh-ParkInspect;component/App.xaml", UriKind.Relative));
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Settings.DEBUGGING = true;

            Settings.CurrentUser = new Employee
            {
                JobTitle = new JobTitle { Name = "Admin" }
            };

            _router = new RouterService();
        }

        [TestMethod]
        public void Router_ChangesView()
        {
            Assert.IsNull(_router.CurrentView);

            _router.GoTo(ControlType.UserManagementControl);

            Assert.IsInstanceOfType(_router.CurrentView, ControlType.UserManagementControl.Control);
        }

        [TestMethod]
        public void Router_IsAtRoot()
        {
            _router.GoTo(ControlType.ChangePasswordControl);

            Assert.IsTrue(_router.IsAtRoot());

            _router.GoTo(ControlType.UserManagementControl);

            Assert.IsFalse(_router.IsAtRoot());

            _router.GoBack();

            Assert.IsTrue(_router.IsAtRoot());
        }

        [TestMethod]
        public void Router_Fails_WhenGoingBackTooFar()
        {
            _router.GoTo(ControlType.UserManagementControl);

            _router.GoTo(ControlType.ChangePasswordControl);

            _router.GoBack();
            _router.GoBack();

            Assert.IsInstanceOfType(_router.CurrentView, ControlType.UserManagementControl.Control);
        }

        [TestMethod]
        public void Router_HistoryCanBeCleared()
        {
            _router.GoTo(ControlType.UserManagementControl);

            _router.GoTo(ControlType.ChangePasswordControl, null, true);

            _router.GoBack();

            Assert.IsInstanceOfType(_router.CurrentView, ControlType.ChangePasswordControl.Control);
        }

        [TestMethod]
        public void Router_GoesBackToCorrectView()
        {
            _router.GoTo(ControlType.UserManagementControl);

            Assert.IsInstanceOfType(_router.CurrentView, ControlType.UserManagementControl.Control);

            _router.GoTo(ControlType.ChangePasswordControl);

            Assert.IsInstanceOfType(_router.CurrentView, ControlType.ChangePasswordControl.Control);

            _router.GoBack();

            Assert.IsInstanceOfType(_router.CurrentView, ControlType.UserManagementControl.Control);
        }

        [TestMethod]
        public void Router_CanPassDataViaViewBagToView()
        {
            dynamic viewBag = new ViewBag();
            viewBag.Foo = "Bar";

            _router.GoTo(ControlType.UserManagementControl, viewBag);

            Assert.AreEqual(_router.CurrentView.ViewBag.Foo, "Bar");
        }

        [TestMethod]
        public void Router_CanPassDataViaViewBagToViewModel()
        {
            dynamic viewBag = new ViewBag();
            viewBag.Foo = "Bar";

            _router.GoTo(ControlType.UserManagementControl, viewBag);

            Assert.AreEqual(((ParkInspectViewModel)_router.CurrentView.DataContext).ViewBag.Foo, "Bar");
        }

        [TestMethod]
        public void Router_CantGoToTheSameView()
        {
            _router.GoTo(ControlType.UserManagementControl);

            _router.GoTo(ControlType.ChangePasswordControl);

            _router.GoTo(ControlType.ChangePasswordControl);

            _router.GoBack();

            Assert.IsInstanceOfType(_router.CurrentView, ControlType.UserManagementControl.Control);
        }
    }
}