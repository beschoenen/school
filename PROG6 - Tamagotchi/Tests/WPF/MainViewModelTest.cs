using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WPF.TamagotchiService;
using WPF.ViewModel;

namespace Tests.WPF
{
    [TestClass]
    public class MainViewModelTest
    {
        private Mock<ITamagotchiService> _service;
        private MainViewModel _mainViewModel;

        [TestInitialize]
        public void TestInitialize()
        {
            _service = new Mock<ITamagotchiService>();
            _service.Setup(t => t.GetAllTamagotchi()).Returns(new List<Tamagotchi>
            {
                new Tamagotchi {Name = "Kevin", Deceased = false},
                new Tamagotchi {Name = "Daniel", Deceased = true}
            });

            _mainViewModel = new MainViewModel(_service.Object);
        }

        [TestMethod]
        public void TestUpdateCurrentStatus()
        {
            var tamagotchi = new Tamagotchi {Name = "Henk"};

            Assert.AreEqual(string.Empty, _mainViewModel.UpdateCurrentStatus(tamagotchi));

            tamagotchi.Statuses = new List<string> {"hug", "sleep"};

            Assert.AreEqual("Henk is currently:\r\nhug\r\nsleep", _mainViewModel.UpdateCurrentStatus(tamagotchi));
        }

        [TestMethod]
        public void TestFillTamagotchiList()
        {
            _mainViewModel.FillTamagotchiList();

            _service.Verify(n => n.GetAllTamagotchi(), Times.Exactly(2));

            Assert.AreEqual(2, _mainViewModel.TamagotchiList.Count);
        }
    }
}