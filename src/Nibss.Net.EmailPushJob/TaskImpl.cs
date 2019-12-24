using EmailPushJob.Entities;
using EmailPushJob.Models;
using Newtonsoft.Json;
using EmailPushJob.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace EmailPushJob
{
    public class TaskImpl
    {
        readonly Util taskUtil;
        public TaskImpl(Util taskUtil)
        {
            this.taskUtil = taskUtil;
        }

        

        static TaskImpl()
        {
        }

        private System.Collections.Generic.List<Email> GetEmails(Database connection)
        {
            var mails = connection.Database.SqlQuery<Email>(
                "exec Sp_SelectSpecial1 @count, @myFlag",
                new Object[] { new SqlParameter("@count", taskUtil.ChunkSize), new SqlParameter("@myFlag", taskUtil.Flag) }
                ).ToListAsync().Result;

            return mails;
        }




        private List<Email> Update(Database connection, Email email)
        {
            var mails = new List<Email>();
            try
            {
                 mails = connection.Database.SqlQuery<Email>(
                "exec Sp_UpdateEmail @id ,@IsSent, @RetryCount, @ResponseCode, @ResponseDesc",
                new Object[] {
                    new SqlParameter("@id", email.Id)
                    ,new SqlParameter("@IsSent", email.IsSent ?1:0)
                    ,new SqlParameter("@RetryCount", email.RetryCount)
                    ,new SqlParameter("@ResponseCode", email.ResponseCode)
                     ,new SqlParameter("@ResponseDesc", email.ResponseDesc)
                }
                ).ToListAsync().Result;

                return mails;
            }
            catch (Exception ex)
            {
                taskUtil.Logger.Error(ex);
                
            }

            return mails;
        }

        public void DoJob()
        {
            try
            {
                var Connection = new Database(taskUtil.DbConnectionString);
                taskUtil.Logger.Info("Email Push Started");
                var InfoBeepService = new InfoBipEmailService(taskUtil);

                var mails = GetEmails(Connection);
                taskUtil.Logger.Info(string.Format("Fetched {0} Items", mails.Count));

                foreach(var email in mails)
                {
                    try
                    {
                        email.RetryCount++;
                        taskUtil.Logger.Info(string.Format("Sending email with Ref {0}", email.EmailRef));

                      
                        var response = InfoBeepService.SendSimple(new MailModel
                        {
                            AttachementInBase64 = email.Attachment,
                            EmailRef = email.EmailRef.ToString(),
                            isBodyHtml = email.IsBodyHtml,
                            Subject = email.Subject,
                            MailContent = email.MailContent,
                            To = email.To,
                            From = email.From
                        });
                        taskUtil.Logger.Info(string.Format("Response from Venter {0}", JsonConvert.SerializeObject(response)));
                        if (response != null)
                        {
                            email.ResponseDesc = response.ResponseDesc;
                            email.ResponseCode = response.ResponseCode.ToString();
                            if (response.ResponseCode.ToString().Equals("1"))
                            {
                                email.IsSent = true;
                                email.ResponseCode = "00";
                                email.ResponseDesc = "Successful";
                            }
                        }
                        Update(Connection, email);
                    }
                    catch (Exception ex)
                    {
                         taskUtil.Logger.Error(ex.Message, ex);
                    }
                } 
            }
            catch (Exception ex)
            {
                taskUtil.Logger.Error(ex.Message, ex);
            }
        }



        public void DoMaintenance()
        {
            try
            {
                var Connection = new Database(taskUtil.DbConnectionString);
                taskUtil.Logger.Info("Maintenance job started ...");
                var mails = Connection.Database.SqlQuery<Email>(
               "exec Sp_Maintenance",
               new Object[] { }
               ).ToListAsync().Result;
               taskUtil.Logger.Info("Maintenance job done ...");
            }
            catch (Exception ex)
            {
                taskUtil.Logger.Info("Error in DoMaintenance");
                taskUtil.Logger.Error(ex.Message, ex);
            }
        }
    }
}
