﻿using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RestSharp;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace ScaCopilot
{
    public static class ExtensionMethods
    {
        public static bool EqualsInteger(this string str, int value)
        {
            int v = 0;
            var parsed = int.TryParse(str, out v);
            if (parsed)
            {
                return value == v;
            }
            return false;
        }

        public static int ToInteger(this JToken jt)
        {
            int v = 0;
            if (jt != null)
            {
                var str = jt.ToString();
                int.TryParse(str, out v);
            }

            return v;
        }
        public static string TryGet(this Dictionary<string,string> dict,string key)
        {
            string value = "";
            bool got = dict.TryGetValue(key, out value);
           return value;

        }

        public static string ToJsonString<T>(this List<T> TItems) where T: class
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(TItems,settings);
        }

        public static string ToJsonString<T>(this T TItem) where T : class
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(TItem,settings);
        }


        public static string ToStringOrEmpty(this JToken? jt)
        {
            try
            {
                if (jt == null)
                {
                    return "";
                }
                var str = jt.ToString();
                return str;
            }
            catch (Exception)
            {

                return "";
            }

        }
 

        public static decimal ToDecimal(this JToken? jt)
        {
            decimal v = 0;
            if (jt != null)
            {
                var str = jt.ToString();
                decimal.TryParse(str, out v);
            }

            return v;
        }

        public static DateTime ToDateTime(this JToken jt)
        {
            DateTime dt = DateTime.Now;
            if (jt != null)
            {
                var str = jt.ToString();
                DateTime.TryParse(str, out dt);
            }
            return dt;
        }

        public static bool ToBoolean(this JToken? jt)
        {
            try
            {
                if (jt != null)
                {
                    var str = jt.ToString().ToLower();
                    return str.Equals("true");
                }
            }
            catch (Exception e)
            {
                return false;
            }
           
            return false;
        }
        public static bool IsProduction(this string? env)
        {
            if (env == null)
            {
                return false;
            }
            var isProduction = env.Equals("Production", StringComparison.InvariantCultureIgnoreCase);
            return isProduction;
        }
        
        public static bool IsDevelopment(this string? env)
        {
            if (env == null)
            {
                return false;
            }
            var IsDev = env.Equals("Development", StringComparison.InvariantCultureIgnoreCase);
            Logger.Info(typeof(ExtensionMethods),"the current environment is>>>"+env);
            return IsDev;
        }
        
        
        public static string UrlEncode(this string str)
        {
            if (str != null)
            {
                str = HttpUtility.UrlEncode(str,Encoding.UTF8);
                return str;
            }
            return string.Empty;

        }
        

        public static string FormatMobile(this string mobile)
        {
            if(mobile.IndexOf("233") == 0)
            {
                return mobile;
            }
            if (mobile[0] == '+')
            {
                return mobile.Substring(1);
            }
            if (mobile[0] == '0')
            {
                return "233" + mobile.Substring(1);
            }
            return mobile.Replace(" ","");
        }

        public static string GenerateOtp(int length =5)
        {
            string numbers = "1234567890";
            string characters = numbers;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }

        public static IRestResponse ExecuteAndLog(this RestClient client, IRestRequest request)
        {
            Logger.Info(typeof(RestClient), "Request: " + JsonConvert.SerializeObject(request.Parameters));
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            var response = client.Execute(request);
            Logger.Info(typeof(RestClient), "Response: " + response.StatusCode + ">>>" +response.Content);
            return response;
        }

        public static string GetActiveClass(this string currentPath, string pathLink)
        {
            if (currentPath.IndexOf(pathLink.ToLower(), StringComparison.Ordinal)>=0)
            {
                return " side-menu--active ";
            }
            return "";
        }
        
        public static string GetActiveSubClass(this string currentPath, string pathLink)
        {
            if (currentPath.IndexOf(pathLink.ToLower(), StringComparison.Ordinal)>=0)
            {
                return " side-menu__sub-open ";
            }
            return "";
        }

        public static string EscapeQuotes(this string text,string quotes="'")
        {
            if(!string.IsNullOrEmpty(text))
                return text.Replace("'", "''");
            return text;
        }
        
        public static string ToSlug(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
              return String.Empty;
              ;
            }
            string slug = Regex.Replace(input, @"[^a-zA-Z0-9\s-]", "");
            slug = Regex.Replace(slug, @"\s+", "-");
            slug = slug.ToLower();
            return slug;
        }
  
    }
}
