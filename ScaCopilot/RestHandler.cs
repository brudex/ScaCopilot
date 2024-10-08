﻿using System.Collections.Concurrent;
using System.Net;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using RestSharp;

namespace ScaCopilot
{
    public  class RestHandler
    {
        private static RestHandler instance = null;
        private static readonly object padlock = new object();
        private string _apiKey;
        private ConcurrentDictionary<string, string> _headers;
        private Tuple<string, string> _basicAuth;
        private string _result = "";
        private RestHandler()
        {
            _headers = new ConcurrentDictionary<string, string>();
        }

        public static RestHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new RestHandler();
                        }
                    }
                }
                instance.ResetHeaders();
                return instance;
            }
        }

      private void ResetHeaders()
      {
          if (_headers != null)
          {
              _headers.Clear();
              
          }
            _apiKey = null;
            _basicAuth = null;
        }


        public RestPostResponse DoPostFormValues(List<KeyValuePair<string,string>> formData, string url)
        {
            
            var client = new RestClient(url);
            Logger.Info(this,"Calling Url >>>"+url);
            
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            var resp = new RestPostResponse();
            if (!string.IsNullOrEmpty(_apiKey))
            {
                _headers["API-KEY"] = _apiKey;
            }
            foreach (var header in _headers)
            {
                Logger.Info(this,"Adding Header>>"+header.Key+">>"+header.Value);
                request.AddHeader(header.Key, header.Value);
            }
            foreach (KeyValuePair<string,string> kv in formData)
            { 
                request.AddParameter(kv.Key, kv.Value);
            }
            var str = "";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, certificate, chain, sslPolicyErrors) => true;
                var response1 = client.ExecuteAndLog(request);
                Logger.Info(this,"Response Status from Post Form Values>>"+response1.StatusCode);
                Logger.Info(this,"Response Content from Post Form Values>>"+response1.Content);
                resp.content = response1.Content;
                resp.Status = response1.StatusCode;
                return resp;
            }
            catch (Exception ex)
            {
                Logger.Error(this,$"Error DoPostFormValues url : {url}>>"+ex.Message,ex);
                resp.content = ex.Message;
            }
            return resp; 
        }

        public RestHandler AddCustomHeader(string key, string value)
        {
            string? val;
            if (_headers.TryGetValue(key, out val))
            {
                return this;
            }
            _headers[key] = value;
            return this;
        }

        public RestHandler AddJsonHeaders()
        {
            string? val;
            if (_headers.TryGetValue("Content-Type", out val))
            {
                return this;
            }
           _headers["Content-Type"] = "application/json";
           return this;
        }

        public RestHandler SetBasicAuthentication(string userName, string password)
        {
            _basicAuth = new Tuple<string, string>(userName, password);
            return this;
        }

        public RestHandler SetApiKey(string key)
        {
            _apiKey = key;
            return this;
        }

        public RestPostResponse DoGetString(string url, string endpoint = "")
        {
            var client = new RestClient(url);
            Logger.Info(this,"Setting Proxy Settings>>>"+SettingsData.AspNetEnv);
             
            RestRequest request;
            if(string.IsNullOrEmpty(endpoint))
            {
                request = new RestRequest(Method.GET);
            }
            else
            {
                request = new RestRequest(endpoint, Method.GET);
            }
            request.AddHeader("Content-Type", "application/json");
            if (!string.IsNullOrEmpty(_apiKey))
            {
                _headers["API-KEY"] = _apiKey;
            }
            foreach (var header in _headers)
            {
                Console.WriteLine("Adding Header>>"+header.Key+">>"+header.Value);
                request.AddHeader(header.Key, header.Value);
            }
            if (_basicAuth != null)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(_basicAuth.Item1 + ":" + _basicAuth.Item2);
                string base64 = Convert.ToBase64String(bytes);
                request.AddHeader("Authorization", "Basic " + base64);
            }
            var resp = new RestPostResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, certificate, chain, sslPolicyErrors) => true;
                var response1 = client.ExecuteAndLog(request);
                resp.content = response1.Content;
                resp.Status = response1.StatusCode;
                return resp;
            }
            catch (Exception ex)
            {
                resp.Status = System.Net.HttpStatusCode.InternalServerError;
                Logger.Info(typeof(RestHandler),"RestHandler - DoPost>>>" + ex.Message);
                return null;
            }
        }


        private RestSharp.Method GetMethod(string method)
        {
            var dict = new Dictionary<string,RestSharp.Method>()
            {
                {  "GET",RestSharp.Method.GET},
                {  "POST",RestSharp.Method.POST},
                {  "PUT",RestSharp.Method.PUT},
                {  "DELETE",RestSharp.Method.DELETE},
                {  "COPY",RestSharp.Method.PATCH},
            };
            var mymethod = RestSharp.Method.POST;
            dict.TryGetValue(method, out mymethod);
            return mymethod;
        }

         
        
        
        public RestPostResponse DoPostGetString(string url, string content, string contentType="")
        {
            var client = new RestClient(url);
             
            Logger.Info("Calling url >>>", url);
            var request = new RestRequest(GetMethod("POST"));
            if (!string.IsNullOrEmpty(_apiKey))
            {
                _headers["API-KEY"] = _apiKey;
            }
            foreach (var header in _headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
            if (_basicAuth != null)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(_basicAuth.Item1 + ":" + _basicAuth.Item2);
                string base64 = Convert.ToBase64String(bytes);
                request.AddHeader("Authorization", "Basic " + base64);
            }
            try
            {
                if (string.IsNullOrEmpty(contentType))
                {
                    contentType = "application/json";
                }
                request.AddParameter(contentType, content, ParameterType.RequestBody);
                Logger.Info("Calling url >>>", url);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var response1 = client.Execute(request);
                 var resp = new RestPostResponse();
                resp.content = response1.Content;
                Logger.Info(this,"Response from service>>"+content);
                resp.Status = response1.StatusCode;
                return resp;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "RestHandler - DoPost",ex);
                return null;
            }
        }

        public RestPostResponse DoPostWithCertificate(string url, string content,string certPath,string certPassword="")
        {
            var client = new RestClient(url);
            Logger.Info("Calling url >>>", url);
            var request = new RestRequest(Method.POST);
            X509Certificate2 certificates = new X509Certificate2();
            if (string.IsNullOrEmpty(certPassword))
            {
                certificates.Import(certPath);
            }
            else
            {
               SecureString secureString=new SecureString();
                for (int i = 0; i < certPassword.Length; i++)
                {
                    Char c = certPassword[i];
                    secureString.AppendChar(c);
                }
                certificates.Import(certPath,secureString,X509KeyStorageFlags.DefaultKeySet);
                client.ClientCertificates = new X509Certificate2Collection() { certificates };
            }
           
            if (!string.IsNullOrEmpty(_apiKey))
            {
                _headers["API-KEY"] = _apiKey;
            }
            foreach (var header in _headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
            if (_basicAuth != null)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(_basicAuth.Item1 + ":" + _basicAuth.Item2);
                string base64 = Convert.ToBase64String(bytes);
                request.AddHeader("Authorization", "Basic " + base64);
            }
            try
            {
                request.AddParameter("application/json", content, ParameterType.RequestBody);
                Logger.Info("Calling url >>>", url);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
                var response1 = client.Execute(request);
                Logger.Info(typeof(RestHandler),  response1.Content);
                var resp = new RestPostResponse();
                resp.content = response1.Content;
                Logger.Info(this, "Response from service>>" + content);
                resp.Status = response1.StatusCode;
                return resp;
            }
            catch (Exception ex)
            {
                Logger.Error(this, "RestHandler - DoPost",ex);
                return null;
            }
        }

    }
}
