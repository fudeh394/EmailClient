
using Newtonsoft.Json;
using Nibss.Net.EmailClient.Entities;
using Nibss.Net.EmailClient.Interfaces;
using Nibss.Net.EmailClient.Models;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Net.Mail;

namespace Nibss.Net.EmailClient.Services
{
    public class EmailService : IEmail
    {
        public readonly  Database Connection;
        public EmailService(Database connection)
        {
            Connection = connection;
        }

        public EmailSentStatus QueryStatus(string requestID)
        {
            var result = new EmailSentStatus()
            {
                EmailRef = string.Empty,
                Status = Constants.RESPONSE_STATUS_PENDING_DESC
            };

            var reqParam = new SqlParameter("@Ref",  requestID);

            if (!string.IsNullOrEmpty(requestID))
            {
                var record = Connection.Database.SqlQuery<Email>(
                "exec Sp_SelectEmail @Ref ", reqParam
                 );

                if (record != null)
                {
                    try
                    {

                        var resultSet = record.ToListAsync().Result[0];
                        result = new EmailSentStatus
                        {


                            EmailRef = resultSet.EmailRef.ToString(),
                            Status = (resultSet.IsSent) ? Constants.RESPONSE_STATUS_SENT_DESC : Constants.RESPONSE_STATUS_PENDING_DESC
                        };

                    }
                    catch (Exception ex)
                    {
                        result = new EmailSentStatus
                        {
                            EmailRef = string.Empty,
                            Status = Constants.RESPONSE_STATUS_PENDING_DESC
                        };
                    }
                }
            }

            return result;
           
        }

        private bool ValidateAttachment(string AttachementInBase64)
        {
            var res = false;
            if (!string.IsNullOrEmpty(AttachementInBase64))
            {
                try
                {
                    var bytString = AttachementInBase64.Split(',')[1];
                    var bytArray = System.Convert.FromBase64String(bytString);
                    Stream strm = new MemoryStream(bytArray);

                    //get the file type
                    var fileTypeString = AttachementInBase64.Split(',')[0];
                    fileTypeString = fileTypeString.Split('/')[1];
                    fileTypeString = fileTypeString.Split(';')[0];
                    var fileType = AttachementInBase64.Split(',')[1];
                    if(!string.IsNullOrEmpty(fileType) && (!string.IsNullOrEmpty(fileTypeString)))
                    {
                        res = true;
                    }
                }
                catch (Exception ex)
                {
                    
                     res = false;
                }
            }


            return res;
        }


        private bool ValidateEmail(string emailaddress)
        {
            var result = false;
            try
            {
                if (string.IsNullOrEmpty(emailaddress))
                    return result;

                if (!emailaddress.Contains(","))
                {
                    MailAddress m = new MailAddress(emailaddress);
                    return true;
                }

                var listEmails = emailaddress.Split(',');
                for (int i = 0; i < listEmails.Length; i++)
                {
                    var email = listEmails[i];
                    result = true;
                    if (!string.IsNullOrEmpty(email))
                    {
                        try
                        {
                            MailAddress m = new MailAddress(email);

                        }
                        catch (Exception)
                        {

                            result = false;

                        }
                    }

                    if (result.Equals(false))
                        break;

                }
                return result;

            }
            catch (FormatException)
            {
                return false;
            }
        }
        public EmailResponse Send(Email mailModel)
        {

            Logger.Info(string.Format("Received Request = {0}", JsonConvert.SerializeObject(mailModel)));
            var result = new EmailResponse()
            {
                ResponseCode = Constants.RESPONSE_STATUS_FAILED,
                ResponseDesc = Constants.RESPONSE_STATUS_FAILED_DESC
            };


            if(mailModel==null)
            {
                result.ResponseDesc = "Null Request is not allowed";
                return result;
            }

            if((!string.IsNullOrEmpty(mailModel.Attachment)) &&!ValidateAttachment(mailModel.Attachment))
            {
                result.ResponseDesc = "Invalid Attachemet format";
                return result;
            }

            if(string.IsNullOrEmpty(mailModel.From))
            {
                result.ResponseDesc = "From email can not be empty";
                return result;
            }

            if (string.IsNullOrEmpty(mailModel.To))
            {
                result.ResponseDesc = "Recipient email can not be empty";
                return result;
            }

            if (mailModel.From.Contains(","))
            {
                result.ResponseDesc = "Only one email address is expected in the Sender's email address";
                return result;
            }

            if (!ValidateEmail(mailModel.From))
            {
                result.ResponseDesc = "Invalid sender Email";
                return result;
            }

            if (!ValidateEmail(mailModel.To))
            {
                result.ResponseDesc = "Invalid recepient email address";
                return result;
            }


            if (mailModel != null)
            {
                  var record = Connection.Database.SqlQuery<Email>(
                  "exec Sp_InsertEmail @TO, @FROM,@Name,@Attachment, @CarbonCopy, @Subject, @MailContent , @IsSent,@IsBodyHtml, @DateCreated , @RetryCount, @Request  , @ResponseCode, @ResponseDesc ",
                  new Object[]
                  {

                            new SqlParameter("@To", mailModel.To)
                           ,new SqlParameter("@FROM", mailModel.From)
                           ,new SqlParameter("@Name", mailModel.Name)
                           ,new SqlParameter("@Attachment", mailModel.Attachment)
                           ,new SqlParameter("@CarbonCopy", mailModel.CarbonCopy)
                           ,new SqlParameter("@Subject", mailModel.Subject)
                           ,new SqlParameter("@MailContent", mailModel.MailContent)
                           ,new SqlParameter("@IsSent", mailModel.IsSent)
                           ,new SqlParameter("@IsBodyHtml",mailModel.IsBodyHtml)
                           ,new SqlParameter("@DateCreated", DateTime.Now)
                           ,new SqlParameter("@RetryCount", mailModel.RetryCount)
                           ,new SqlParameter("@Request", string.Empty)
                           ,new SqlParameter("@ResponseCode", Constants.REQUEST_STATUS_PENDING)
                           ,new SqlParameter("@ResponseDesc", Constants.REQUEST_STATUS_PENDING_DESC)

                      }
                  );

                if (record != null)
                {
                    try
                    {
                        result = new EmailResponse
                        {
                            EmailRef = record.ToListAsync().Result[0].EmailRef,
                            ResponseCode = Constants.RESPONSE_STATUS_PENDING,
                            ResponseDesc = Constants.RESPONSE_STATUS_PENDING_DESC
                        };

                    }
                    catch (Exception ex)
                    {
                        Logger.Error("SendMail Error", ex);
                        result = new EmailResponse
                        {
                           
                            ResponseCode = Constants.RESPONSE_STATUS_FAILED,
                            ResponseDesc = Constants.RESPONSE_STATUS_FAILED_DESC
                        };
                    }
                }
            }


            Logger.Info(string.Format("Response = {0}", JsonConvert.SerializeObject(result)));
            return result;
        }
    }


}
