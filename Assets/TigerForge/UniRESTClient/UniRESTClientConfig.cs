/*
* =================================
*  TIGERFORGE UniREST Client v.3.5
* ---------------------------------
*     Configuration settings
* =================================
*/

namespace TigerForge
{
    public class UniRESTClientConfig
    {
        public static string Key1 = "hE16iPFIewl9eLrc9bFmJisUbxIeeIhy";
        public static string Key2 = "H0JROpbM2l11Z9zW";
        public static string AppID = "OpbM";
        public static string ServerUrl = "http://shaktisocialmedia.local/UNIREST/";
    }
    public static class API
    {
    }
    [System.Serializable]
    public static class DB
    {
        [System.Serializable]
        public class Photos
        {
            public int id = 0;
            public string Name = "";
        }

        [System.Serializable]
        public class Users
        {
            public int id = 0;
            public string username = "";
            public string password = "";
            public string tokenl = "";
            public string tokenr = "";
            public string tokenw = "";
            public string regdate = "";
            public string logdate = "";
        }

    }
}
