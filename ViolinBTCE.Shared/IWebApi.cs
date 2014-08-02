using System.Collections.Generic;

namespace ViolinBtce.Shared
{
    public interface IWebApi
    {
        string GetJsonStringFromQuery(Dictionary<string, string> operations);

        string RequestHttpInformation(string url);
        
        T Deserialize<T>(string jsonString);
    }
}