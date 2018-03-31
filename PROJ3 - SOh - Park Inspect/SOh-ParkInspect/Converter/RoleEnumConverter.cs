using System;
using SOh_ParkInspect.Enum;

namespace SOh_ParkInspect.Converter
{
    public static class RoleEnumConverter
    {
        public static RoleType Convert(string type)
        {
            switch (type)
            {
                case "Admin":
                    return RoleType.Admin;
                case "Inspector":
                    return RoleType.Inspector;
                case "Manager":
                    return RoleType.Manager;
                default:
                    throw new InvalidOperationException("This role does not exist.");
            }
        }

        public static string Convert(RoleType type)
        {
            switch (type)
            {
                case RoleType.Admin:
                    return "Admin";
                case RoleType.Inspector:
                    return "Inspector";
                case RoleType.Manager:
                    return "Manager";
                default:
                    throw new InvalidOperationException("This role does not exist.");
            }
        }
    }
}
