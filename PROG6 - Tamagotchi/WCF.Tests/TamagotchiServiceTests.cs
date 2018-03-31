using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCF.Repository;
using WCF.Service;

namespace WCF.Tests
{
    [TestClass]
    public class TamagotchiServiceTests
    {
        private TamagotchiService _service;
        private ITamagotchiRepository _repo;

        [TestInitialize]
        public void TestInitialize()
        {
            _repo = new DummyTamagotchiRepository();
            _service = new TamagotchiService(_repo);
        }

        [TestMethod]
        public void TestGetTamagotchi()
        {
            _repo.AddOrUpdate(new Database.Tamagotchi
            {
                Id = 1,
                Name = "Kevin"
            });

            Assert.AreEqual("Kevin", _service.GetTamagotchi(1).Name);
        }

        [TestMethod]
        public void TestGetAllTamagotchi()
        {
            _repo.AddOrUpdate(new Database.Tamagotchi
            {
                Id = 1,
                Name = "Kevin"
            });

            _repo.AddOrUpdate(new Database.Tamagotchi
            {
                Id = 2,
                Name = "Daniel"
            });

            Assert.AreEqual(2, _service.GetAllTamagotchi().Count());
        }

        [TestMethod]
        public void TestCreateTamagotchi()
        {
            var tama = new Tamagotchi
            {
                Name = "Kevin"
            };

            _service.CreateTamagotchi(tama);

            Assert.IsTrue(_repo.GetAll().Any(t => t.Name == "Kevin"));
        }

        [TestMethod]
        public void TestUpdateTamagotchi()
        {
            var tama = new Tamagotchi
            {
                Id = 1,
                Name = "Kevin"
            };

            _service.CreateTamagotchi(tama);

            Assert.IsTrue(_repo.GetAll().Any(t => t.Name == "Kevin"));

            var newTama = new Tamagotchi
            {
                Id = 1,
                Name = "Daniel"
            };

            Assert.IsTrue(_service.UpdateTamagotchi(1, newTama));

            Assert.IsFalse(_repo.GetAll().Any(t => t.Name == "Kevin"));

            Assert.IsTrue(_repo.GetAll().Any(t => t.Name == "Daniel"));
        }

        [TestMethod]
        public void TestUpdateNonExistingTamagotchi()
        {
            var newTama = new Tamagotchi
            {
                Id = 1,
                Name = "Daniel"
            };

            Assert.IsFalse(_service.UpdateTamagotchi(1, newTama));
        }

        [TestMethod]
        public void TestDeleteTamagotchi()
        {
            var tama = new Tamagotchi
            {
                Id = 1,
                Name = "Kevin"
            };

            _service.CreateTamagotchi(tama);

            Assert.IsTrue(_repo.GetAll().Any(t => t.Name == "Kevin"));

            _service.DeleteTamagotchi(1);

            Assert.IsFalse(_repo.GetAll().Any());
        }

        [TestMethod]
        public void TestDeleteNonExistingTamagotchi()
        {
            Assert.IsFalse(_service.DeleteTamagotchi(1));
        }

        [TestMethod]
        public void TestSwitchTamagotchi()
        {
            var tama = new Database.Tamagotchi
            {
                Id = 1,
                Name = "Kevin"
            };

            _repo.AddOrUpdate(tama);

            Assert.IsTrue(_service.SwitchTamagotchi(1));

            Assert.AreEqual(1, _service.GetCurrentTamagotchi().Id);
        }

        [TestMethod]
        public void TestSwitchTamagotchiFails()
        {
            Assert.IsFalse(_service.SwitchTamagotchi(1));
        }

        [TestMethod]
        public void TestDismissTamagotchi()
        {
            var tama = new Database.Tamagotchi
            {
                Id = 1,
                Name = "Kevin"
            };

            _repo.AddOrUpdate(tama);

            _service.SwitchTamagotchi(1);

            Assert.IsTrue(_service.DismissTamagotchi());

            Assert.IsNull(_service.GetCurrentTamagotchi());
        }

        [TestMethod]
        public void TestDismissTamagotchiNoTamagotchi()
        {
            Assert.IsFalse(_service.DismissTamagotchi());
        }

        [TestMethod]
        public void TestCurrentActionCanBeNull()
        {
            Assert.IsNull(_service.GetCurrentAction());
        }

        [TestMethod]
        public void TestDoAction()
        {
            var tama = new Database.Tamagotchi
            {
                Id = 1,
                Name = "Kevin"
            };

            _repo.AddOrUpdate(tama);

            _service.SwitchTamagotchi(1);

            Assert.IsTrue(_service.DoAction("sleep"));

            Assert.IsNotNull(_service.GetCurrentAction());
        }

        [TestMethod]
        public void TestDoWrongAction()
        {
            var tama = new Database.Tamagotchi
            {
                Id = 1,
                Name = "Kevin"
            };

            _repo.AddOrUpdate(tama);

            _service.SwitchTamagotchi(1);

            Assert.IsFalse(_service.DoAction("toilet-time"));

            Assert.IsNull(_service.GetCurrentAction());
        }

        [TestMethod]
        public void TestCantRun2Actions()
        {
            var tama = new Database.Tamagotchi
            {
                Id = 1,
                Name = "Kevin"
            };

            _repo.AddOrUpdate(tama);

            _service.SwitchTamagotchi(1);

            Assert.IsTrue(_service.DoAction("sleep"));

            Assert.IsFalse(_service.DoAction("sleep"));
        }

        [TestMethod]
        public void TestIsCurrentlyRunningAction()
        {
            var tama = new Database.Tamagotchi
            {
                Id = 1,
                Name = "Kevin"
            };

            _repo.AddOrUpdate(tama);

            _service.SwitchTamagotchi(1);

            Assert.IsFalse(_service.IsCurrentlyRunningAnAction());

            _service.DoAction("sleep");

            Assert.IsTrue(_service.IsCurrentlyRunningAnAction());
        }

        [TestMethod]
        public void TestActiveGameRules()
        {
            var tama = new Database.Tamagotchi
            {
                Id = 1,
                Name = "Kevin"
            };

            _repo.AddOrUpdate(tama);

            _service.SwitchTamagotchi(1);

            Assert.AreEqual(8, _service.ActiveGameRules().Count);
        }

        [TestMethod]
        public void TestActiveGameRulesWrongAction()
        {
            var tama = new Database.Tamagotchi
            {
                Id = 1,
                Name = "Kevin"
            };

            _repo.AddOrUpdate(tama);

            _service.SwitchTamagotchi(1);

            Assert.IsFalse(_service.ToggleRule("toilet-time"));
        }

        [TestMethod]
        public void TestActiveGameRulesWithoutActiveTamagotchi()
        {
            Assert.IsFalse(_service.ToggleRule("sleep"));
        }

        [TestMethod]
        public void TestToggleAction()
        {
            var tama = new Database.Tamagotchi
            {
                Id = 1,
                Name = "Kevin"
            };

            _repo.AddOrUpdate(tama);

            _service.SwitchTamagotchi(1);

            _service.ToggleRule("sleep");

            Assert.AreEqual(7, _service.ActiveGameRules().Count);

            _service.ToggleRule("sleep");

            Assert.AreEqual(8, _service.ActiveGameRules().Count);
        }

        [TestMethod]
        public void TestSleepAction()
        {
            var tama = new Database.Tamagotchi
            {
                Id = 1,
                Name = "Kevin",
                Sleep = 30,
                Health = 10
            };

            _repo.AddOrUpdate(tama);

            _service.SwitchTamagotchi(1);

            _service.ToggleRule("sleep");

            _service.DoAction("sleep");

            Thread.Sleep(16000);

            var newTama = _service.GetCurrentTamagotchi();

            Assert.AreEqual(20, newTama.Health);
            Assert.AreEqual(5, newTama.Sleep);
        }

        [TestMethod]
        public void TestHugAction()
        {
            var tama = new Database.Tamagotchi
            {
                Id = 1,
                Name = "Kevin",
                Sleep = 30,
                Health = 30,
                Hunger = 30,
                Boredom = 30
            };

            _repo.AddOrUpdate(tama);

            _service.SwitchTamagotchi(1);

            _service.ToggleRule("sleep");
            _service.ToggleRule("boredom");
            _service.ToggleRule("hunger");

            _service.DoAction("hug");

            Thread.Sleep(4000);

            var newTama = _service.GetCurrentTamagotchi();

            Assert.AreEqual(40, newTama.Health);
            Assert.AreEqual(20, newTama.Sleep);
            Assert.AreEqual(20, newTama.Hunger);
            Assert.AreEqual(20, newTama.Boredom);
        }

        [TestMethod]
        public void TestFeedAction()
        {
            var tama = new Database.Tamagotchi
            {
                Id = 1,
                Name = "Kevin",
                Sleep = 30,
                Health = 30,
                Hunger = 50,
                Boredom = 30
            };

            _repo.AddOrUpdate(tama);

            _service.SwitchTamagotchi(1);

            _service.ToggleRule("sleep");
            _service.ToggleRule("boredom");
            _service.ToggleRule("hunger");

            _service.DoAction("feed");

            Thread.Sleep(6000);

            var newTama = _service.GetCurrentTamagotchi();

            Assert.IsTrue(newTama.Health == 30 || newTama.Health == 10);
            Assert.AreEqual(0, newTama.Hunger);
        }

        [TestMethod]
        public void TestPlayAction()
        {
            var tama = new Database.Tamagotchi
            {
                Id = 1,
                Name = "Kevin",
                Sleep = 30,
                Health = 30,
                Hunger = 50,
                Boredom = 40
            };

            _repo.AddOrUpdate(tama);

            _service.SwitchTamagotchi(1);

            _service.ToggleRule("sleep");
            _service.ToggleRule("boredom");
            _service.ToggleRule("hunger");

            _service.DoAction("play");

            Thread.Sleep(9000);

            var newTama = _service.GetCurrentTamagotchi();

            Assert.IsTrue(newTama.Health == 30 || newTama.Health == 20);
            Assert.AreEqual(5, newTama.Boredom);
        }

        [TestMethod]
        public void TestTamagotchiCanDie()
        {
            var tama = new Database.Tamagotchi
            {
                Id = 1,
                Name = "Kevin",
                Sleep = 100,
                Health = 10,
                Hunger = 100,
                Boredom = 100
            };

            _repo.AddOrUpdate(tama);

            _service.SwitchTamagotchi(1);

            Thread.Sleep(11000);

            Assert.IsTrue(_service.GetCurrentTamagotchi().Deceased);
        }
    }
}
