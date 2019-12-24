
using Nibss.Net.EmailClient.Entities;
using Nibss.Net.EmailClient.Models;
using Nibss.Net.EmailClient.Services;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using static Nibss.Net.EmailClient.CustomLogging;

namespace Nibss.EmailClientWeb.Controllers
{
    public class EmailController : ApiController
    {
       private readonly EmailService emailService;

       public EmailController()
        {

            emailService = new EmailService(new Database(ConfigurationManager.AppSettings["AppConnectionString"]));
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostEmail(EmailReq request)
        {
            try
            {
                var result = emailService.Send(new Email
                {
                    Attachment = request.Attachement,
                    DateCreated = DateTime.Now,
                    MailContent = request.Body,
                    To = request.Recipients,
                    From = request.From,
                    Name = string.Empty,
                    CarbonCopy = string.Empty,
                    Subject = request.Subject,
                    IsSent = false,
                    IsBodyHtml = request.IsBodyHtml,
                    RetryCount = 0
                });


                return await Task.FromResult(Ok(result));
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }


        [HttpGet]
        public async Task<IHttpActionResult> QueryStatus(string requestID)
        {

            var ip = GetIp();
            Logger.Info(string.Format("Calling from {0}", ip));

          

            var result = emailService.QueryStatus(requestID);
            return await Task.FromResult(Ok(result));
        }




        private string GetIp()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }





    }
}
