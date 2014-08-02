/*
 * Base for making api class for btc-e.com
 * DmT
 * 2012
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Newtonsoft.Json.Linq;
using ViolinBtce.Dto;
using ViolinBtce.Dto.Enums;
using ViolinBtce.Dto.Helpers;
using ViolinBtce.Shared;

namespace ViolinBTCE
{
    public class ViolinBtceApi
    {
        string key;
        HMACSHA512 hashMaker;
        UInt32 nonce;
        public ViolinBtceApi(string key, string secret)
        {
            this.key = key;
            hashMaker = new HMACSHA512(Encoding.ASCII.GetBytes(secret));
            nonce = UnixTimeHelper.Now;
        }
        public DtoUserInfo GetInfo()
        {
            var resultStr = Query(new Dictionary<string, string>()
            {
                { "method", "getInfo" }
            });
            var result = JObject.Parse(resultStr);
            if (result.Value<int>("success") == 0)
                throw new Exception(result.Value<string>("error"));
            return JObjectParser.ReadFromJObject(result["return"] as JObject, typeof(DtoUserInfo)) as DtoUserInfo;
        }

        public DtoTransHistory GetTransHistory( int? from = null, int? count = null, int? fromId = null,
                                                int? endId = null, bool? orderAsc = null, DateTime? since = null, DateTime? end = null)
        {
            var args = new Dictionary<string, string>()
            {
                { "method", "TransHistory" }
            };

            if (from != null) args.Add("from", from.Value.ToString());
            if (count != null) args.Add("count", count.Value.ToString());
            if (fromId != null) args.Add("from_id", fromId.Value.ToString());
            if (endId != null) args.Add("end_id", endId.Value.ToString());
            if (orderAsc != null) args.Add("order", orderAsc.Value ? "ASC" : "DESC");
            if (since != null) args.Add("since", UnixTimeHelper.GetFromDateTime(since.Value).ToString());
            if (end != null) args.Add("end", UnixTimeHelper.GetFromDateTime(end.Value).ToString());
            var result = JObject.Parse(Query(args));
            if (result.Value<int>("success") == 0)
                throw new Exception(result.Value<string>("error"));
            return JObjectParser.ReadFromJObject(result["return"] as JObject, typeof(DtoTransHistory)) as DtoTransHistory;
        }

        public DtoTradeHistory GetTradeHistory(int? from = null, int? count = null, int? fromId = null,
                                                int? endId = null, bool? orderAsc = null, DateTime? since = null, DateTime? end = null)
        {
            var args = new Dictionary<string, string>()
            {
                { "method", "TradeHistory" }
            };

            if (from != null) args.Add("from", from.Value.ToString());
            if (count != null) args.Add("count", count.Value.ToString());
            if (fromId != null) args.Add("from_id", fromId.Value.ToString());
            if (endId != null) args.Add("end_id", endId.Value.ToString());
            if (orderAsc != null) args.Add("order", orderAsc.Value ? "ASC" : "DESC");
            if (since != null) args.Add("since", UnixTimeHelper.GetFromDateTime(since.Value).ToString());
            if (end != null) args.Add("end", UnixTimeHelper.GetFromDateTime(end.Value).ToString());

            var result = JObject.Parse(Query(args));
            if (result.Value<int>("success") == 0)
                throw new Exception(result.Value<string>("error"));
            return JObjectParser.ReadFromJObject(result["return"] as JObject, typeof(DtoTradeHistory)) as DtoTradeHistory;
        }

        public DtoActiveOrders GetOrderList(int? from = null, int? count = null, int? fromId = null, 
                                                int? endId = null, bool? orderAsc = null, DateTime? since = null, 
                                                DateTime? end = null, Pair? pair = null, bool? active = null )
        {
            var args = new Dictionary<string, string>()
            {
                { "method", "OrderDictionary" }
            };

            if (from != null) args.Add("from", from.Value.ToString());
            if (count != null) args.Add("count", count.Value.ToString());
            if (fromId != null) args.Add("from_id", fromId.Value.ToString());
            if (endId != null) args.Add("end_id", endId.Value.ToString());
            if (orderAsc != null) args.Add("order", orderAsc.Value ? "ASC" : "DESC");
            if (since != null) args.Add("since", UnixTimeHelper.GetFromDateTime(since.Value).ToString());
            if (end != null) args.Add("end", UnixTimeHelper.GetFromDateTime(end.Value).ToString());
            if (pair != null) args.Add("pair", ConvertionHelper.ToString(pair.Value));
            if (active != null) args.Add("active", active.Value ? "1" : "0");
            var result = JObject.Parse(Query(args));
            if (result.Value<int>("success") == 0)
                throw new Exception(result.Value<string>("error"));
            return JObjectParser.ReadFromJObject(result["return"] as JObject, typeof(DtoActiveOrders)) as DtoActiveOrders;
        }

        public DtoTradeAnswer Trade(Pair pair, TradeType type, decimal rate, decimal amount)
        {
            var args = new Dictionary<string, string>()
            {
                { "method", "Trade" },
                { "pair", ConvertionHelper.ToString(pair) },
                { "type", ConvertionHelper.ToString(type) },
                { "rate", DecimalToString(rate) },
                { "amount", DecimalToString(amount) }
            };
            var result = JObject.Parse(Query(args));
            if (result.Value<int>("success") == 0)
                throw new Exception(result.Value<string>("error"));
            return JObjectParser.ReadFromJObject(result["return"] as JObject, typeof(DtoTradeAnswer)) as DtoTradeAnswer;
        }

        public DtoCancelOrderAnswer CancelOrder(int orderId)
        {
            var args = new Dictionary<string, string>()
            {
                { "method", "CancelOrder" },
                { "order_id", orderId.ToString() }
            };
            var result = JObject.Parse(Query(args));
            if (result.Value<int>("success") == 0)
                throw new Exception(result.Value<string>("error"));
            return JObjectParser.ReadFromJObject(result["return"] as JObject, typeof(DtoCancelOrderAnswer)) as DtoCancelOrderAnswer;
        }

        string Query(Dictionary<string, string> args)
        {
            args.Add("nonce", GetNonce().ToString());

            var dataStr = BuildPostData(args);
            var data = Encoding.ASCII.GetBytes(dataStr);

            var request = WebRequest.Create(new Uri("https://btc-e.com/tapi")) as HttpWebRequest;
            if (request == null)
                throw new Exception("Non HTTP WebRequest");

            request.Method = "POST";
            request.Timeout = 15000;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            request.Headers.Add("Key", key);
            request.Headers.Add("Sign", ByteArrayToString(hashMaker.ComputeHash(data)).ToLower());
            var reqStream = request.GetRequestStream();
            reqStream.Write(data, 0, data.Length);
            reqStream.Close();
            return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
        }
        static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }
        static string BuildPostData(Dictionary<string, string> d)
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

        UInt32 GetNonce()
        {
            return nonce++;
        }
        static string DecimalToString(decimal d)
        {
            return d.ToString(CultureInfo.InvariantCulture);
        }
        public static DtoDepth GetDepth(Pair pair)
        {
            string queryStr = string.Format("https://btc-e.com/api/2/{0}/depth", ConvertionHelper.ToString(pair));
            return JObjectParser.ReadFromJObject(JObject.Parse(WebApi.Query(queryStr)), typeof(DtoDepth)) as DtoDepth;
        }
        public DtoTicker GetTicker(Pair pair)
        {
            string queryStr = string.Format("https://btc-e.com/api/2/{0}/ticker", ConvertionHelper.ToString(pair));
            return JObjectParser.ReadFromJObject(JObject.Parse(WebApi.Query(queryStr))["ticker"] as JObject, typeof(DtoTicker)) as DtoTicker;
        }
        //public static List<DtoTradeInfo> GetTrades(Pair pair)
        //{
        //    string queryStr = string.Format("https://btc-e.com/api/2/{0}/trades", BtcePairHelper.ToString(pair));
        //    return JObjectParser.ReadFromJObject( JObject.Parse(WebApi.Query(queryStr)) );
        //}
        public static decimal GetFee(Pair pair)
        {
            string queryStr = string.Format("https://btc-e.com/api/2/{0}/fee", ConvertionHelper.ToString(pair));
            return JObject.Parse(WebApi.Query(queryStr)).Value<decimal>("trade");
        }
    }
}
