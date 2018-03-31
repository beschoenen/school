using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ASP.Controllers;
using ASP.TamagotchiService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.ASP
{
    [TestClass]
    public class ControllerTest
    {
        private Mock<ITamagotchiService> _service;
        private TamagotchiController _controller;

        [TestInitialize]
        public void TestInitialize()
        {
            _service = new Mock<ITamagotchiService>();

            _service.Setup(t => t.GetCurrentTamagotchi()).Returns((Tamagotchi) null);
            _service.Setup(t => t.CreateTamagotchi(It.IsAny<Tamagotchi>())).Returns(It.IsAny<Tamagotchi>());
            _service.Setup(t => t.GetTamagotchi(It.IsAny<int>())).Returns(It.IsAny<Tamagotchi>());
            _service.Setup(t => t.GetAllTamagotchi()).Returns(new List<Tamagotchi>
            {
                new Tamagotchi {Name = "Kevin", Deceased = false},
                new Tamagotchi {Name = "Daniel", Deceased = true}
            });
            _service.Setup(t => t.DismissTamagotchi()).Returns(true);

            _controller = new TamagotchiController(_service.Object);
        }

        [TestMethod]
        public void TestIndex()
        {
            Assert.IsInstanceOfType(_controller.Index(), typeof(ViewResult));

            _service.Verify(m => m.GetAllTamagotchi(), Times.Once);

            _service.Verify(m => m.GetCurrentTamagotchi(), Times.Once);
        }

        [TestMethod]
        public void TestCreatePage()
        {
            Assert.IsInstanceOfType(_controller.Create(), typeof(ViewResult));
        }

        [TestMethod]
        public void TestCreate()
        {
            var response = _controller.Create(new List<string> {"Kevin", "Daniel", ""});

            _service.Verify(m => m.CreateTamagotchi(It.IsAny<Tamagotchi>()), Times.Exactly(2));

            Assert.IsInstanceOfType(response, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void TestDelete()
        {
            Assert.IsInstanceOfType(_controller.Delete(null), typeof(HttpStatusCodeResult));

            Assert.IsInstanceOfType(_controller.Delete(1), typeof(HttpNotFoundResult));

            _service.Setup(m => m.GetTamagotchi(It.IsAny<int>())).Returns(new Tamagotchi());

            Assert.IsInstanceOfType(_controller.Delete(1), typeof(ViewResult));

            _service.Verify(m => m.GetTamagotchi(It.IsAny<int>()), Times.Exactly(2));
        }

        [TestMethod]
        public void TestDeleteConfirmed()
        {
            Assert.IsInstanceOfType(_controller.DeleteConfirmed(1), typeof(RedirectToRouteResult));

            _service.Verify(m => m.DeleteTamagotchi(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void TestSetTamagotchi()
        {
            Assert.IsInstanceOfType(_controller.SetTamagotchi(1), typeof(HttpNotFoundResult));

            _service.Setup(m => m.GetTamagotchi(It.IsAny<int>())).Returns(new Tamagotchi {Id = 1});

            Assert.IsInstanceOfType(_controller.SetTamagotchi(1), typeof(RedirectToRouteResult));

            _service.Verify(m => m.SwitchTamagotchi(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void TestDismiss()
        {
            Assert.IsInstanceOfType(_controller.Dismiss(), typeof(RedirectToRouteResult));

            _service.Verify(m => m.DismissTamagotchi(), Times.Once);
        }

        [TestMethod]
        public void TestPlay()
        {
            Assert.IsInstanceOfType(_controller.Play(), typeof(RedirectToRouteResult));

            _service.Setup(t => t.GetCurrentTamagotchi()).Returns(new Tamagotchi());

            Assert.IsInstanceOfType(_controller.Play(), typeof(ViewResult));

            _service.Verify(m => m.GetCurrentTamagotchi(), Times.Exactly(2));
            _service.Verify(m => m.ActiveGameRules(), Times.Once);
        }
    }
}