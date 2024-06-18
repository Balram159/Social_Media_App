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
        public static string Key1 = "kmtjN1wVlm7iMgaTqUadN0OgMwPB5R46";
        public static string Key2 = "RHncfGgh6GYIcoNQ";
        public static string AppID = "fGgh";
        public static string ServerUrl = "https://indocoshaktiforher.com/UNIREST/";
    }
    public static class API
    {
        public static string data_emp_data = "data/emp_data/";
    }
    [System.Serializable]
    public static class DB
    {
        [System.Serializable]
        public class Details
        {
            public int id = 0;
            public string division = "";
            public string emp_code = "";
            public string emp_name = "";
            public string designation = "";
            public string email_Id = "";
            public int contact_no = 0;
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
