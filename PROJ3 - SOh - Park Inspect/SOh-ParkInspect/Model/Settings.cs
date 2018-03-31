using Database;

namespace SOh_ParkInspect.Model
{
    public static class Settings
    {
        ///////////////
        // Offline Mode

        private static bool _offlineMode;
        public static bool OfflineMode
        {
            set { _offlineMode = value; }
        }

        public static bool IsOnlineMode => !_offlineMode;
        public static bool IsOfflineMode => _offlineMode;

        ///////////////
        // Current user

        public static bool IsLoggedIn => CurrentUser != null;

        public static Employee CurrentUser { get; set; }

        // false uses real databases
        public static bool DEBUGGING = false;
    }
}
