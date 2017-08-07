using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SpeechLuisOwin.Src.Static
{
    public class Configurations
    {
        public static string luisAppId = "8a3aeb1c-525c-44c9-9be9-856b3b35fe53";

        public static string luisSubKey = "1ccb9a5dfc844d21b91f762b7d07e5ef";

        public static string aad_Tenant = ConfigurationManager.AppSettings["ida:Tenant"];

        public static string aad_Audience = ConfigurationManager.AppSettings["ida:Audience"];

        public static string aad_ClientId = ConfigurationManager.AppSettings["ida:ClientId"];

        public static string aad_AuthUri = ConfigurationManager.AppSettings["ida:AuthUri"];

        public static string aad_Key = ConfigurationManager.AppSettings["ida:Key"];

        public static string aad_Resource = ConfigurationManager.AppSettings["ida:Resource"];

        public static string[] speechSubKeys = {
            "24ddfaef8980439f9dd90508a3ed3c9f",
            "3470179be1a141e5ad3e1fbfbedec2f2",
            "6bc8be66833543f1b506240284b5e537",
            "ec6f3210532647d48fe2ca4e3b4a2640"
        };

    }
}