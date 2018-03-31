using System.Net;

namespace SOh_ParkInspect.Helper
{
    public static class Internet
    {
        public static bool IsAvailable
        {
            get
            {
                try
                {
                    using (new WebClient().OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}