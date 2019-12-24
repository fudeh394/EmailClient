using EmailPushJob;
using EmailPushJob.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace EmailPushJob.Services
{


    public class InfoBipEmailService
    {

        private readonly string Uri;
        private readonly string UsrN;
        private readonly string Pass;
        private readonly Util taskUtil;

        public InfoBipEmailService(Util _taskUtil)
        {
            taskUtil = _taskUtil;
            Uri = taskUtil.InfoBeepServiceUrl;
            UsrN = taskUtil.InfoBeepUserName;
            Pass = taskUtil.InfoBeepPassword;
        }

        public InfoBipEmailService(string _url)
        {
            Uri = _url;
            UsrN = "nibss-plc";
            Pass = "Nibss@1230";
            taskUtil = new Util(null);

        }

        public EmailResponse SendSimple(MailModel mailModel)
        {
            var res = new EmailResponse() {
                EmailRef = Guid.Parse(mailModel.EmailRef),
                ResponseCode = 0, ResponseDesc = "Failed" };// false;
            try
            {
                taskUtil.Logger.Info("Abount Sending Email With Ref =" + mailModel.EmailRef);
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Uri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(UsrN + ":" + Pass));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encoded);
                var request = new MultipartFormDataContent();
                request.Add(new StringContent(mailModel.From), "from");


                if (string.IsNullOrEmpty(mailModel.To))
                    return res;

                if (mailModel.To.Contains(','))
                {
                    var mails = mailModel.To.Split(',');
                    for (int i = 0; i < mails.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(mails[i]))
                        {
                            request.Add(new StringContent(mails[i]), "to");
                        }
                    }
                }
                else
                {
                    request.Add(new StringContent(mailModel.To), "to");

                }

                request.Add(new StringContent(mailModel.Subject), "subject");
                if (mailModel.Attachement != null)
                {
                    Stream strm = new MemoryStream(mailModel.Attachement);
                    request.Add(new StreamContent(strm), "attachment", "attachment");
                }
                if (!string.IsNullOrEmpty(mailModel.AttachementInBase64))
                {
                    try
                    {
                        var bytString = mailModel.AttachementInBase64.Split(',')[1];
                        var bytArray = System.Convert.FromBase64String(bytString);
                        Stream strm = new MemoryStream(bytArray);

                        //get the file type
                        var fileTypeString = mailModel.AttachementInBase64.Split(',')[0];
                        fileTypeString = fileTypeString.Split('/')[1];
                        fileTypeString = fileTypeString.Split(';')[0];
                        var fileType = mailModel.AttachementInBase64.Split(',')[1];
                        request.Add(new StreamContent(strm), "attachment", "attachment." + fileTypeString);
                    }
                    catch (Exception ex)
                    {
                        taskUtil.Logger.Error("Error Occured @ SendSimple", ex);
                      
                        return res;
                    }
                }
                if (mailModel.isBodyHtml)
                    request.Add(new StringContent(mailModel.MailContent), "html");
                else
                    request.Add(new StringContent(mailModel.MailContent), "text");
                var response = client.PostAsync("email/1/send", request).Result;
                taskUtil.Logger.Info(string.Format("Response from InfoBeep {0}", JsonConvert.SerializeObject(response)));
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    string responseString = responseContent.ReadAsStringAsync().Result;
                    taskUtil.Logger.Info("Request was successful  with response code " + responseString);
                    res.ResponseCode = 1; //success
                    res.ResponseDesc = responseString;
                }
                
            }
            catch (Exception ex)
            {
                taskUtil.Logger.Error("Error Occured @ SendSimple", ex);
                res.ResponseCode = 0; res.ResponseDesc = "Failed";
                return res;
            }
            return res;
        }

    }
}
