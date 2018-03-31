using System.Runtime.Serialization;

namespace WCF.Enum
{
    [DataContract(Name = "GameRule")]
    public enum GameRule
    {
        [EnumMember]
        Age,

        [EnumMember]
        Boredom,

        [EnumMember]
        Hunger,

        [EnumMember]
        Sleep,

        [EnumMember]
        SleepDeprevation,

        [EnumMember]
        Starvation,

        [EnumMember]
        Munchies,

        [EnumMember]
        Crazy
    }
}