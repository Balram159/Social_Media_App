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
        public static string Key1 = "eARPb9zcPiSuATwjigC5iE0s5iziDaaq";
        public static string Key2 = "xIR5dZeo15a6CCDl";
        public static string AppID = "dZeo";
        public static string ServerUrl = "http://test.local/UNIREST/";
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
