using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCF.Enum;
using WCF.GameRule;
using WCF.GameRule.Condition;
using WCF.Service;

namespace WCF.Tests
{
    [TestClass]
    public class ConditionTests
    {
        private Tamagotchi _tamagotchi;

        [TestInitialize]
        public void TestInitialize()
        {
            _tamagotchi = new Tamagotchi();
        }

        [TestMethod]
        public void TestAge()
        {
            var age = new Age();

            age.Execute(_tamagotchi);

            Assert.AreEqual(_tamagotchi.Age, 10);
        }

        [TestMethod]
        public void TestBoredom()
        {
            var boredom = new Boredom();

            boredom.Execute(_tamagotchi);

            Assert.IsTrue(_tamagotchi.Boredom >= 15 && _tamagotchi.Boredom <= 35);
        }

        [TestMethod]
        public void TestHunger()
        {
            var hunger = new Hunger();

            hunger.Execute(_tamagotchi);

            Assert.IsTrue(_tamagotchi.Hunger >= 15 && _tamagotchi.Hunger <= 35);
        }

        [TestMethod]
        public void TestSleep()
        {
            var sleep = new Sleep();

            sleep.Execute(_tamagotchi);

            Assert.IsTrue(_tamagotchi.Sleep >= 15 && _tamagotchi.Sleep <= 35);
        }

        [TestMethod]
        public void TestSleepDeprevation()
        {
            _tamagotchi.Sleep = 81;
            _tamagotchi.Health = 50;

            var sleep = new SleepDeprivation();

            sleep.Execute(_tamagotchi);

            Assert.IsTrue(_tamagotchi.Statuses.Contains(Status.SleepDeprevated));
            Assert.AreEqual(_tamagotchi.Health, 30);

            sleep.Execute(_tamagotchi);

            Assert.AreEqual(_tamagotchi.Statuses.Count(e => e == Status.SleepDeprevated), 1);
            Assert.AreEqual(_tamagotchi.Health, 10);

            _tamagotchi.Sleep = 70;

            sleep.Execute(_tamagotchi);

            Assert.IsFalse(_tamagotchi.Statuses.Any(e => e == Status.SleepDeprevated));
            Assert.AreEqual(_tamagotchi.Health, 10);
        }

        [TestMethod]
        public void TestStarvation()
        {
            _tamagotchi.Hunger = 81;
            _tamagotchi.Health = 50;

            var statvation = new Starvation();

            statvation.Execute(_tamagotchi);

            Assert.IsTrue(_tamagotchi.Statuses.Contains(Status.Starved));
            Assert.AreEqual(_tamagotchi.Health, 30);

            statvation.Execute(_tamagotchi);

            Assert.AreEqual(_tamagotchi.Statuses.Count(e => e == Status.Starved), 1);
            Assert.AreEqual(_tamagotchi.Health, 10);

            _tamagotchi.Hunger = 70;

            statvation.Execute(_tamagotchi);

            Assert.IsFalse(_tamagotchi.Statuses.Any(e => e == Status.Starved));
            Assert.AreEqual(_tamagotchi.Health, 10);
        }

        [TestMethod]
        public void TestMunchies()
        {
            _tamagotchi.Boredom = 81;

            var munchies = new Munchies();

            munchies.Execute(_tamagotchi);

            Assert.IsTrue(_tamagotchi.Statuses.Contains(Status.Munchies));

            munchies.Execute(_tamagotchi);

            Assert.AreEqual(_tamagotchi.Statuses.Count(e => e == Status.Munchies), 1);

            _tamagotchi.Boredom = 70;

            munchies.Execute(_tamagotchi);

            Assert.IsFalse(_tamagotchi.Statuses.Any(e => e == Status.Munchies));
        }

        [TestMethod]
        public void TestCrazy()
        {
            _tamagotchi.Hunger = 71;
            _tamagotchi.Sleep = 71;

            var crazy = new Crazy();

            crazy.Execute(_tamagotchi);

            Assert.IsTrue(_tamagotchi.Statuses.Contains(Status.Crazy));

            crazy.Execute(_tamagotchi);

            Assert.AreEqual(_tamagotchi.Statuses.Count(e => e == Status.Crazy), 1);

            _tamagotchi.Hunger = 70;

            crazy.Execute(_tamagotchi);

            Assert.IsFalse(_tamagotchi.Statuses.Any(e => e == Status.Crazy));
        }
    }
}
