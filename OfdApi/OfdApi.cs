using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace OfdApi
{

    public sealed class OfdApi
    {
        public static async Task<JObject> GetAufTokenOFD(string OfdLogin, string OfdPassword)
        {
            try
            {
                WebRequest request = WebRequest.Create("https://ofd.ru/api/Authorization/CreateAuthToken");
                request.Method = "POST";
                string data = @"{" +
                    "\"Login\":" +
                    $"\"{OfdLogin}\"" +
                    ",\"Password\": " +
                    $"\"{OfdPassword}\"" +
                    @"}";
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                using (Stream dataStream = await request.GetRequestStreamAsync())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                WebResponse response = await request.GetResponseAsync();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {

                        JObject ResponseString = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(reader.ReadToEnd());
                        response.Close();
                        return ResponseString;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<JObject> GetKktInfo(string AuthTokenOFD, string CompanyInn, string kktSn, string FnSn, string RegNumber)
        {
            if (string.IsNullOrEmpty(AuthTokenOFD.ToString())
                || string.IsNullOrEmpty(CompanyInn)
                || string.IsNullOrEmpty(kktSn)
                || string.IsNullOrEmpty(FnSn)
                || string.IsNullOrEmpty(RegNumber))
                return null;
            else
            {
                try
                {
                    Uri get = new Uri
                    ($"https://ofd.ru/api/integration/v1/inn/{CompanyInn}/kkts?FNSerialNumber={FnSn}&KKTSerialNumber={kktSn}&KKTRegNumber={RegNumber}&AuthToken={AuthTokenOFD}");
                    WebRequest request = WebRequest.Create(get);
                    WebResponse response = await request.GetResponseAsync();
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            JObject ResponseString = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(reader.ReadToEnd());
                            response.Close();
                            return ResponseString;
                        }
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        //public static async Task<JObject>  void GetOFDReportList(string AuthTokenOFD)
        //{
        //    try
        //    {
        //        Uri get = new Uri($"https://ofd.ru/api/customer/reports?AuthToken={AuthTokenOFD}");
        //        WebRequest request = WebRequest.Create(get);
        //        using (WebResponse response = request.GetResponse())
        //        {
        //            using (Stream stream = response.GetResponseStream())
        //            {
        //                using (StreamReader reader = new StreamReader(stream))
        //                {
        //                    JObject ResponseString = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(reader.ReadToEnd());
        //                    var Data = ResponseString["Data"];
        //                }
        //            }
        //        }
        // return Data;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

    }
}

