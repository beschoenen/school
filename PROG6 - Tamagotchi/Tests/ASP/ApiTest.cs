using System;
using System.Text;
using System.Collections.Generic;
using ASP.Controllers;
using ASP.TamagotchiService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;

namespace Tests.ASP
{
    [TestClass]
    public class ApiTest
    {
        private Mock<ITamagotchiService> _service;
        private TamagotchiApiController _controller;

        [TestInitialize]
        public void TestInitialize()
        {
            _service = new Mock<ITamagotchiService>();

            _service.Setup(t => t.GetCurrentTamagotchi()).Returns(new Tamagotchi { Name = "Kevin" });
            _service.Setup(t => t.IsCurrentlyRunningAnAction()).Returns(false);
            _service.Setup(t => t.DoAction(It.IsAny<string>())).Returns(true);
            _service.Setup(t => t.ToggleRule(It.IsAny<string>())).Returns(true);

            _controller = new TamagotchiApiController(_service.Object);
        }

        [TestMethod]
        public void TestGetCurrentTamagotchi()
        {
            var response = _controller.CurrentTamagotchi();

            var json = JObject.Parse(response).Property("Name").Value;

            _service.Verify(m => m.GetCurrentTamagotchi(), Times.Once);

            Assert.AreEqual("Kevin", json);
        }

        [TestMethod]
        public void TestDoAction()
        {
            var response = _controller.DoAction("sleep");

            Assert.AreEqual("True", response);

            _service.Setup(t => t.DoAction(It.IsAny<string>())).Returns(false);

            response = _controller.DoAction("toilet-time");

            Assert.AreEqual("False", response);

            _service.Setup(t => t.IsCurrentlyRunningAnAction()).Returns(true);

            response = _controller.DoAction("sleep");

            Assert.AreEqual("There's already an action running!", response);
        }

        [TestMethod]
        public void TestToggleGameRule()
        {
            Assert.AreEqual(true, _controller.ToggleGameRule("sleep"));

            _service.Setup(t => t.ToggleRule(It.IsAny<string>())).Returns(false);

            _service.Verify(m => m.ToggleRule("sleep"), Times.Once);

            Assert.AreEqual(false, _controller.ToggleGameRule("toilet-time"));
        }
    }
}
