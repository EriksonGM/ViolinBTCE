using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Newtonsoft.Json.Linq;
using ViolinBtce.Dto.Helpers;

namespace ViolinBtce.Shared
{
    public class WebApi : IWebApi
    {
        private readonly string _key;
        private readonly HMACSHA512 _hashMaker;
        private static UInt32 _nonce;

        public WebApi(string key, string secret)
        {
            _key = key;
            _hashMaker = new HMACSHA512(Encoding.ASCII.GetBytes(secret));
            _nonce = UnixTimeHelper.Now;
        }

        public static string Query(string url)
        {
            var request = WebRequest.Create(url);
            request.Proxy = WebRequest.DefaultWebProxy;
            request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            if (request == null)
                throw new Exception("Non HTTP WebRequest");
            return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
        }

        public string GetJsonStringFromQuery(Dictionary<string, string> operations)
        {
            operations.Add("nonce", GetNonce().ToString());

            var dataStr = BuildPostData(operations);
            var data = Encoding.ASCII.GetBytes(dataStr);

            var request = WebRequest.Create(new Uri("https://btc-e.com/tapi")) as HttpWebRequest;
            if (request == null)
                throw new Exception("Non HTTP WebRequest");

            request.Method = "POST";
            request.Timeout = 15000;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            request.Headers.Add("Key", _key);
            request.Headers.Add("Sign", ByteArrayToString(_hashMaker.ComputeHash(data)).ToLower());
            var reqStream = request.GetRequestStream();
            reqStream.Write(data, 0, data.Length);
            reqStream.Close();
            return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
        }

        public string RequestHttpInformation(string url)
        {
            var request = WebRequest.Create(url);
            request.Proxy = WebRequest.DefaultWebProxy;
            request.Proxy.Credentials = CredentialCache.DefaultCredentials;
            if (request == null)
                throw new Exception("Non HTTP WebRequest");
            return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
        }

        public T Deserialize<T>(string jsonString)
        {
            var jObject = JObject.Parse(jsonString);

            int success = (int) jObject["success"].ToObject(typeof (int));

            if (success != 1) throw new OperationCanceledException(jObject["error"].ToString());

            var deserializedObject = jObject["return"].ToObject<T>();
            return deserializedObject;
        }

        public T DeserializeURLInfo<T>(string jsonString, string specialName = null)
        {
            var jObject = JObject.Parse(jsonString);

            T deserializedObject;
            
            if(specialName == null)
                deserializedObject = (T) jObject[typeof(T).Name.ToLowerInvariant()].ToObject(typeof(T));
            else
                deserializedObject = (T)jObject[specialName].ToObject(typeof(T));

            return deserializedObject;
        }

        private static UInt32 GetNonce()
        {
            return _nonce++;
        }

        private static string BuildPostData(Dictionary<string, string> d)
        {
            StringBuilder s = new StringBuilder();
            foreach (var item in d)
            {
                s.AppendFormat("{0}={1}", item.Key, HttpUtility.UrlEncode(item.Value));
                s.Append("&");
            }
            if (s.Length > 0) s.Remove(s.Length - 1, 1);
            return s.ToString();
        }

        private static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }
    }
}