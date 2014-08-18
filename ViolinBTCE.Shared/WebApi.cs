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
    public class WebApi
    {
        private readonly string _key;
        private readonly HMACSHA512 _hashMaker;
        private static UInt32 _nonce;

        public WebApi(string key, string secret)
        {
            if(string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key","The key must be passed in the constructor of a new instance of WebApi.");
            if(string.IsNullOrEmpty(secret))
                throw new ArgumentNullException("secret", "The secret must be passed in the constructor of a new instance of WebApi.");

            _key = key;
            _hashMaker = new HMACSHA512(Encoding.ASCII.GetBytes(secret));
            _nonce = UnixTimeHelper.Now;
        }

        #region Query
        public static string Query(string url)
        {
            if (!ValidateURL(url)) throw new WebException("Non HTTP WebRequest");
            
            var httpInformation = RequestHttpInformation(url);
            
            return httpInformation;
        }

        private static string GetResponse(WebRequest webRequest)
        {
            try
            {
                Stream responseStream = webRequest.GetResponse().GetResponseStream();
                string response = new StreamReader(responseStream).ReadToEnd();
                return response;
            }
            catch (Exception)
            {
                throw new WebException("It was not possible to read the stream content.");
            }
        }

        #region Private methods
            private static WebRequest CreateWebRequest(string url)
            {
                var request = WebRequest.Create(url);
                request.Proxy = WebRequest.DefaultWebProxy;
                request.Proxy.Credentials = CredentialCache.DefaultCredentials;
                return request;
            }

            private static bool ValidateURL(string url)
            {
                Uri uriResult;
                bool b = Uri.TryCreate(url, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;
                return b;
            }
            #endregion
        #endregion

        #region GetAnswerAsJsonString
        public string GetAnswerAsJsonString(Dictionary<string, string> operations, string apiUri)
        {
            if (string.IsNullOrEmpty(apiUri))
                throw new HttpException("Uri is empty");

            operations.Add("nonce", GetNonce().ToString());

            var dataStr = BuildPostData(operations);
            var data = Encoding.ASCII.GetBytes(dataStr);
            
            //TODO check method and probably use it in a specialist class.
            var request = ConfigureRequest(data, new Uri(apiUri));
            
            doRequisition(request, data);

            var responseStream = request.GetResponse().GetResponseStream();
            
            var jsonString = new StreamReader(responseStream).ReadToEnd();
            
            return jsonString;
        }

        #region Private methods
            private static string BuildPostData(Dictionary<string, string> operations)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var item in operations)
                {
                    stringBuilder.AppendFormat("{0}={1}", item.Key, HttpUtility.UrlEncode(item.Value));
                    stringBuilder.Append("&");
                }
                if (stringBuilder.Length > 0) stringBuilder.Remove(stringBuilder.Length - 1, 1);
                return stringBuilder.ToString();
            }

            private HttpWebRequest ConfigureRequest(byte[] data, Uri uri)
            {
                HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;

                if (request == null)
                    throw new HttpException("Non HTTP WebRequest");

                request.Method = "POST";
                request.Timeout = 15000;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                request.Headers.Add("Key", _key);
                request.Headers.Add("Sign", ByteArrayToString(_hashMaker.ComputeHash(data)).ToLower());
                return request;
            }

            private static void doRequisition(HttpWebRequest request, byte[] data)
            {
                var reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion
        #endregion

        public static string RequestHttpInformation(string url)
        {
            var webRequest = CreateWebRequest(url);

            return GetResponse(webRequest);
        }

        public static T Deserialize<T>(string jsonString, string specialName = null)
        {
            var jObject = JObject.Parse(jsonString);

            T deserializedObject;

            if (specialName == null)
            {
                int success = (int) jObject["success"].ToObject(typeof (int));

                if (success != 1) throw new OperationCanceledException(jObject["error"].ToString());

                deserializedObject = jObject["return"].ToObject<T>();
            }
            else
                deserializedObject = (T)jObject[specialName].ToObject(typeof(T));

            return deserializedObject;
        }

        private static UInt32 GetNonce()
        {
            return _nonce++;
        }


        private static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }
    }
}