using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace OfdRuApi
{
    class OfdRuApi
    {
        public OfdRuApi()
        {

        }

        public OfdRuApi(Company company)
        {
            Company = company;
        }

        public OfdRuAuthToken AuthToken { get; set; }
        public OfdAgreement Agreement { get; set; }
        public Company Company { get; set; }

        /// <summary>
        /// Метод для получения токена авторизации по логин/паролю.
        /// </summary>
        /// <param name="OfdLogin">Логин ОФД.РУ</param>
        /// <param name="OfdPassword">Пароль ОФД.РУ</param>
        public async void CreateOfdAuthToken(string OfdLogin, string OfdPassword)
        {
            try
            {
                WebRequest request = WebRequest.Create("https://ofd.ru/api/Authorization/CreateAuthToken");
                request.Method = "POST";
                string Creditionals = JsonConvert.SerializeObject(new OfdCreditionals { Login = OfdLogin, Password = OfdPassword });
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(Creditionals);
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
                        if (ResponseString.ContainsKey("AuthToken"))
                        {
                            AuthToken = JsonConvert.DeserializeObject<OfdRuAuthToken>(ResponseString.ToString());
                        }
                        response.Close();
                    }
                }
                CreateOfdAgreementId();
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Метод для получения OfdAgreementId ОФД.ру.
        /// </summary>
        private async void CreateOfdAgreementId()
        {
            try
            {
                WebRequest request = WebRequest.Create($"https://ofd.ru/api/customer/reports?&Inn={Company.CompanyINN}&Kpp={Company.CompanyKPP}&SkipPageFilter=true&AuthToken={AuthToken.Token}");
                WebResponse response = await request.GetResponseAsync();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {

                        JObject ResponseString = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(reader.ReadToEnd());
                        if (ResponseString["Status"].ToString() == "Success")
                        {
                            Agreement = JsonConvert.DeserializeObject<OfdAgreement>(ResponseString["Data"].ToString());
                        }
                        response.Close();
                    }
                }
            }
            catch (Exception)
            {

            }
        }


        /// <summary>
        /// Метод получения информации о касса из ОФД.ру
        /// </summary>
        /// <param name="kktSn">Серийный номер кассы</param>
        /// <param name="FnSn">Серийный номер ФН кассы</param>
        /// <param name="RegNumber">Регистрационный номер кассы</param>
        /// <returns></returns>
        public async Task<OfdKktInfo> GetOfdKktInfo(string kktSn, string FnSn, string RegNumber)
        {
            try
            {
                Uri get = new Uri
                ($"https://ofd.ru/api/integration/v1/inn/{Company.CompanyINN}/kkts?FNSerialNumber={FnSn}&KKTSerialNumber={kktSn}&KKTRegNumber={RegNumber}&AuthToken={AuthToken.Token}");
                WebRequest request = WebRequest.Create(get);
                using (WebResponse response = await request.GetResponseAsync())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            JObject ResponseString = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(reader.ReadToEnd());
                            OfdKktInfo ofdKktInfo;
                            if (ResponseString["Status"].ToString() == "Success")
                            {
                                ofdKktInfo = JsonConvert.DeserializeObject<OfdKktInfo>(ResponseString["Data"].ToString());
                                response.Close();
                                return ofdKktInfo;
                            }
                            else
                            {
                                response.Close();
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Метод для скачивания архива с отчетом ОФД.ру по его Id
        /// </summary>
        /// <param name="reportId">Id отчета в ОФД.ру</param>
        public void GetOFDReportById(string reportId)
        {
            try
            {
                WebClient client = new WebClient();
                client.DownloadFileAsync(new Uri($"https://ofd.ru/api/customer/reports/{reportId}?OfdAgreementId={Agreement}"),
                    Directory.GetCurrentDirectory() + @"\" + $"someFile_{DateTime.Now.Second}.zip");
            }
            catch (Exception)
            {
            }
        }


        //TODO переписать 
        //private static async Task<JObject> GetAllZReports(string AuthTokenOFD, string CompanyInn, DateTime beginDate, DateTime endDate)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(AuthTokenOFD.ToString())
        //        || string.IsNullOrEmpty(CompanyInn)
        //        || beginDate == null
        //        || endDate == null
        //        || beginDate > endDate)
        //            throw new NullReferenceException();


        //        Uri get = new Uri
        //        ($"https://ofd.ru/api/integration/v1/inn/{CompanyInn}/zreports?DateFrom={beginDate.Year}-{beginDate.Month}-{beginDate.Day}&DateTo={endDate.Year}-{endDate.Month}-{endDate.Day}&AuthToken={AuthTokenOFD}");
        //        //Uri get = new Uri
        //        //($"https://ofd.ru/api/integration/v1/inn/{CompanyInn}/kkt/{kktSn}/zreports?dateFrom={beginDate.Year}-{beginDate.Month}-{beginDate.Day}&dateTo={endDate.Year}-{endDate.Month}-{endDate.Day}&AuthToken={AuthTokenOFD}");
        //        WebRequest request = WebRequest.Create(get);
        //        WebResponse response = await request.GetResponseAsync();
        //        using (Stream stream = response.GetResponseStream())
        //        {
        //            using (StreamReader reader = new StreamReader(stream))
        //            {
        //                JObject ResponseString = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(reader.ReadToEnd());
        //                response.Close();
        //                return ResponseString;
        //            }
        //        }
        //    }
        //    catch (NullReferenceException ex)
        //    {
        //        MessageBox.Show("Пустое значение недопустимо.");
        //        return null;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //}

    }
}
