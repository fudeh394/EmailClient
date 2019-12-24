

using Newtonsoft.Json;
using Nibss.Net.EmailClient.Models;
using RestSharp;
using System.Collections.Generic;

namespace Nibss.Net.EmailClient.Services
{
    public class VenterService
    {
        public readonly  string Url;
        public VenterService(string _Url)
        {
            Url = _Url;
        }

        private List<Recipient> GetRecipients(string recipientsInCommas)
        {
            var recipientObject = new List<Recipient>();
            var recipients = recipientsInCommas.Split(',');
            if (recipients == null || recipients.Length <= 0)
                return recipientObject;

            for (int i = 0; i < recipients.Length; i++)
            {
                recipientObject.Add(new Recipient { email = recipients[i], name = string.Empty });
            }
            return recipientObject;

        }

        public VenterResponse Send(Email mailModel)
        {
            var result = new VenterResponse()
            {
                code= Constants.RESPONSE_STATUS_FAILED.ToString(),
                message = Constants.RESPONSE_STATUS_FAILED_DESC
            };


            var client = new RestClient(Url);
            var request = new RestRequest(Method.POST);

            var apiReq = new VenterRequest()
            {
                attachment = mailModel.Attachment,
                body = mailModel.MailContent,
                subject = mailModel.Subject
            };

            apiReq.recipients = GetRecipients(mailModel.To);
           
            

            string jsonToSend = JsonConvert.SerializeObject(apiReq);


            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "282");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "192.168.101.21:8290");
            request.AddHeader("Postman-Token", "c6c6e774-ecdd-41a5-ba17-85a85fa76c0c,cf5cb397-1e40-4864-a19f-c09f8c2265d1");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.20.1");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", jsonToSend, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
           return JsonConvert.DeserializeObject<VenterResponse>(response.Content.ToString());
           
        }
    }


}
