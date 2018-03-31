using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCF.Action;
using WCF.GameRule;
using WCF.GameRule.Condition;
using WCF.Helper;

namespace WCF.Tests
{
    [TestClass]
    public class ConverterTests
    {
        [TestMethod]
        public void TestGameRuleStringToEnum()
        {
            var list = new List<KeyValuePair<string, Enum.GameRule>>
            {
                new KeyValuePair<string, Enum.GameRule>("age", Enum.GameRule.Age),
                new KeyValuePair<string, Enum.GameRule>("sleep-deprevation", Enum.GameRule.SleepDeprevation),
                new KeyValuePair<string, Enum.GameRule>("boredom", Enum.GameRule.Boredom),
                new KeyValuePair<string, Enum.GameRule>("starvation", Enum.GameRule.Starvation),
                new KeyValuePair<string, Enum.GameRule>("hunger", Enum.GameRule.Hunger),
                new KeyValuePair<string, Enum.GameRule>("munchies", Enum.GameRule.Munchies),
                new KeyValuePair<string, Enum.GameRule>("sleep", Enum.GameRule.Sleep),
                new KeyValuePair<string, Enum.GameRule>("crazy", Enum.GameRule.Crazy)
            };

            list.ForEach(pair =>
            {
                Assert.AreEqual(Converter.GameRuleToEnum(pair.Key), pair.Value);
            });

            Assert.AreEqual(Converter.GameRuleToEnum("test"), null);
        }

        [TestMethod]
        public void TestGameRuleEnumToClass()
        {
            var list = new List<KeyValuePair<Enum.GameRule, IGameRule>>
            {
                new KeyValuePair<Enum.GameRule, IGameRule>(Enum.GameRule.Age, new Age()),
                new KeyValuePair<Enum.GameRule, IGameRule>(Enum.GameRule.Boredom, new Boredom()),
                new KeyValuePair<Enum.GameRule, IGameRule>(Enum.GameRule.Hunger, new Hunger()),
                new KeyValuePair<Enum.GameRule, IGameRule>(Enum.GameRule.Sleep, new Sleep()),
                new KeyValuePair<Enum.GameRule, IGameRule>(Enum.GameRule.Crazy, new Crazy()),
                new KeyValuePair<Enum.GameRule, IGameRule>(Enum.GameRule.Munchies, new Munchies()),
                new KeyValuePair<Enum.GameRule, IGameRule>(Enum.GameRule.Starvation, new Starvation()),
                new KeyValuePair<Enum.GameRule, IGameRule>(Enum.GameRule.SleepDeprevation, new SleepDeprivation())
            };

            list.ForEach(pair =>
            {
                Assert.AreEqual(Converter.GameRuleToClass(pair.Key).GetType(), pair.Value.GetType());
            });
        }

        [TestMethod]
        public void TestGameRuleStringToClass()
        {
            Assert.AreEqual(Converter.GameRuleToClass("test"), null);
            Assert.AreEqual(Converter.GameRuleToClass("age").GetType(), typeof(Age));
        }

        [TestMethod]
        public void TestGameRuleClassToEnum()
        {
            var list = new List<KeyValuePair<Enum.GameRule, IGameRule>>
            {
                new KeyValuePair<Enum.GameRule, IGameRule>(Enum.GameRule.Age, new Age()),
                new KeyValuePair<Enum.GameRule, IGameRule>(Enum.GameRule.Boredom, new Boredom()),
                new KeyValuePair<Enum.GameRule, IGameRule>(Enum.GameRule.Hunger, new Hunger()),
                new KeyValuePair<Enum.GameRule, IGameRule>(Enum.GameRule.Sleep, new Sleep()),
                new KeyValuePair<Enum.GameRule, IGameRule>(Enum.GameRule.Crazy, new Crazy()),
                new KeyValuePair<Enum.GameRule, IGameRule>(Enum.GameRule.Munchies, new Munchies()),
                new KeyValuePair<Enum.GameRule, IGameRule>(Enum.GameRule.Starvation, new Starvation()),
                new KeyValuePair<Enum.GameRule, IGameRule>(Enum.GameRule.SleepDeprevation, new SleepDeprivation())
            };

            list.ForEach(pair =>
            {
                Assert.AreEqual(Converter.GameRuleToEnum(pair.Value).Value, pair.Key);
            });

            Assert.AreEqual(Converter.GameRuleToEnum("test"), null);
        }

        [TestMethod]
        public void TestActionStringToClass()
        {
            var list = new List<KeyValuePair<string, BaseAction>>
            {
                new KeyValuePair<string, BaseAction>("feed", new FeedAction()),
                new KeyValuePair<string, BaseAction>("hug", new HugAction()),
                new KeyValuePair<string, BaseAction>("play", new PlayAction()),
                new KeyValuePair<string, BaseAction>("sleep", new SleepAction())
            };

            list.ForEach(pair =>
            {
                Assert.AreEqual(Converter.ActionToClass(pair.Key).GetType(), pair.Value.GetType());
            });

            Assert.AreEqual(Converter.ActionToClass("test"), null);
        }
    }
}
