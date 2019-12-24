
using Nibss.Net.EmailClient.Entities;
using Nibss.Net.EmailClient.Interfaces;
using Nibss.Net.EmailClient.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Nibss.Net.EmailClient.Services
{
    //public class EmailServiceDapper : IEmail
    //{
    //    private  string ConnectionString;
    //    private IDbConnection MyIDbConnection;
    //    public EmailServiceDapper(string connectionString)
    //    {
    //        MyIDbConnection = new DapperConnection(connectionString).DapperCon;
    //    }

    //    public EmailSentStatus QueryStatus(string requestID)
    //    {
    //        var result = new EmailSentStatus()
    //        {
    //            EmailRef = string.Empty,
    //            IsSent = false
    //        };

    //        var sql = "Invoice_Insert";

    //        using (var connection = MyIDbConnection())
    //        {
    //            connection.Open();

    //            var affectedRows = connection.Execute(sql,
    //                new[]
    //                {
    //        new {Kind = InvoiceKind.WebInvoice, Code = "Many_Insert_1"},
    //        new {Kind = InvoiceKind.WebInvoice, Code = "Many_Insert_2"},
    //        new {Kind = InvoiceKind.StoreInvoice, Code = "Many_Insert_3"}
    //                },
    //                commandType: CommandType.StoredProcedure
    //            );

    //            My.Result.Show(affectedRows);
    //        }



    //        if (!string.IsNullOrEmpty(requestID))
    //        {
    //            var record = Connection.Database.SqlQuery<Email>(
    //            "exec Sp_SelectEmail @EmailRef ",
    //            new Object[] { new SqlParameter("@EmailRef", Guid.Parse(requestID)) });

    //            if (record != null)
    //            {
    //                try
    //                {
    //                    result = new EmailSentStatus
    //                    {
    //                        EmailRef = record.ToListAsync().Result[0].EmailRef.ToString(),
    //                        IsSent  = record.ToListAsync().Result[0].IsSent
    //                    };

    //                }
    //                catch (Exception)
    //                {
    //                    result = new EmailSentStatus
    //                    {
    //                        EmailRef = string.Empty,
    //                        IsSent =  false
    //                    };
    //                }
    //            }
    //        }

    //        return result;
           
    //    }

    //    public EmailResponse Send(Email mailModel)
    //    {
    //        var result = new EmailResponse()
    //        {
    //            ResponseCode = Constants.RESPONSE_STATUS_FAILED,
    //            ResponseDesc = Constants.RESPONSE_STATUS_FAILED_DESC
    //        };


    //        if (mailModel != null)
    //        {
    //              var record = Connection.Database.SqlQuery<Email>(
    //              "exec Sp_InsertEmail @TO, @FROM, @Attachment, @CarbonCopy, @Subject, @MailContent , @IsSent, @DateCreated , @RetryCount, @Request  , @ResponseCode, @ResponseDesc ",
    //              new Object[]
    //              {

    //                        new SqlParameter("@To", mailModel.To)
    //                       ,new SqlParameter("@FROM", mailModel.From)
    //                       ,new SqlParameter("@Attachment", mailModel.Attachment)
    //                       ,new SqlParameter("@CarbonCopy", mailModel.CarbonCopy)
    //                       ,new SqlParameter("@Subject", mailModel.Subject)
    //                       ,new SqlParameter("@MailContent", mailModel.MailContent)
    //                       ,new SqlParameter("@IsSent", mailModel.IsSent)
    //                       ,new SqlParameter("@DateCreated", DateTime.Now)
    //                       ,new SqlParameter("@RetryCount", mailModel.RetryCount)
    //                       ,new SqlParameter("@Request", string.Empty)
    //                       ,new SqlParameter("@ResponseCode", Constants.REQUEST_STATUS_PENDING)
    //                       ,new SqlParameter("@ResponseDesc", Constants.REQUEST_STATUS_PENDING_DESC)

    //                  }
    //              );

    //            if (record != null)
    //            {
    //                try
    //                {
    //                    result = new EmailResponse
    //                    {
    //                        EmailRef = record.ToListAsync().Result[0].EmailRef,
    //                        ResponseCode = Constants.RESPONSE_STATUS_PENDING,
    //                        ResponseDesc = Constants.RESPONSE_STATUS_PENDING_DESC
    //                    };

    //                }
    //                catch (Exception)
    //                {
    //                    result = new EmailResponse
    //                    {
                           
    //                        ResponseCode = Constants.RESPONSE_STATUS_FAILED,
    //                        ResponseDesc = Constants.RESPONSE_STATUS_FAILED_DESC
    //                    };
    //                }
    //            }
    //        }
    //        return result;
    //    }
    //}


}
