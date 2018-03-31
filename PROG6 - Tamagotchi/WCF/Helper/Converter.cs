using WCF.Action;
using WCF.GameRule;
using WCF.GameRule.Condition;

namespace WCF.Helper
{
    public class Converter
    {
        public static IGameRule GameRuleToClass(Enum.GameRule gameRule)
        {
            IGameRule ruleType;
            switch (gameRule)
            {
                case Enum.GameRule.Age:
                    ruleType = new Age();
                    break;
                case Enum.GameRule.Boredom:
                    ruleType = new Boredom();
                    break;
                case Enum.GameRule.Hunger:
                    ruleType = new Hunger();
                    break;
                case Enum.GameRule.Sleep:
                    ruleType = new Sleep();
                    break;
                case Enum.GameRule.SleepDeprevation:
                    ruleType = new SleepDeprivation();
                    break;
                case Enum.GameRule.Starvation:
                    ruleType = new Starvation();
                    break;
                case Enum.GameRule.Munchies:
                    ruleType = new Munchies();
                    break;
                case Enum.GameRule.Crazy:
                    ruleType = new Crazy();
                    break;
                default:
                    return null;
            }

            return ruleType;
        }

        public static IGameRule GameRuleToClass(string gameRule)
        {
            var value = GameRuleToEnum(gameRule);

            return value == null ? null : GameRuleToClass(value.Value);
        }

        public static Enum.GameRule? GameRuleToEnum(string gameRule)
        {
            Enum.GameRule rule;
            switch (gameRule)
            {
                case "age":
                    rule = Enum.GameRule.Age;
                    break;
                case "sleep-deprevation":
                    rule = Enum.GameRule.SleepDeprevation;
                    break;
                case "boredom":
                    rule = Enum.GameRule.Boredom;
                    break;
                case "starvation":
                    rule = Enum.GameRule.Starvation;
                    break;
                case "hunger":
                    rule = Enum.GameRule.Hunger;
                    break;
                case "munchies":
                    rule = Enum.GameRule.Munchies;
                    break;
                case "sleep":
                    rule = Enum.GameRule.Sleep;
                    break;
                case "crazy":
                    rule = Enum.GameRule.Crazy;
                    break;
                default:
                    return null;
            }

            return rule;
        }

        public static Enum.GameRule? GameRuleToEnum(IGameRule gameRule)
        {
            Enum.GameRule? ruleType = null;

            var ts = new TypeSwitch()
                .Case((Age x) => ruleType = Enum.GameRule.Age)
                .Case((Boredom x) => ruleType = Enum.GameRule.Boredom)
                .Case((Hunger x) => ruleType = Enum.GameRule.Hunger)
                .Case((Sleep x) => ruleType = Enum.GameRule.Sleep)
                .Case((SleepDeprivation x) => ruleType = Enum.GameRule.SleepDeprevation)
                .Case((Starvation x) => ruleType = Enum.GameRule.Starvation)
                .Case((Munchies x) => ruleType = Enum.GameRule.Munchies)
                .Case((Crazy x) => ruleType = Enum.GameRule.Crazy);

            ts.Switch(gameRule);

            return ruleType;
        }

        public static BaseAction ActionToClass(string action)
        {
            BaseAction actionClass;
            switch (action)
            {
                case "play":
                    actionClass = new PlayAction();
                    break;
                case "hug":
                    actionClass = new HugAction();
                    break;
                case "feed":
                    actionClass = new FeedAction();
                    break;
                case "sleep":
                    actionClass = new SleepAction();
                    break;
                default:
                    return null;
            }

            return actionClass;
        }
    }
}