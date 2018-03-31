using System.Runtime.Serialization;

namespace WCF.Enum
{
    [DataContract(Name = "Action")]
    public enum Action
    {
        [EnumMember]
        Play,

        [EnumMember]
        Hug,

        [EnumMember]
        Feed,

        [EnumMember]
        Sleep
    }
}