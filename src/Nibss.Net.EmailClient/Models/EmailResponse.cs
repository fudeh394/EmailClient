
using System;
using System.Collections.Generic;

namespace Nibss.Net.EmailClient.Models
{
    public class EmailResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDesc { get; set; }
        public Guid EmailRef { get; set; }
    }

    public class EmailSentStatus
    {
        public string Status { get; set; }
        public string EmailRef { get; set; }
    }

    public class EmailRequest
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string MailContent { get; set; }
        public string Attachement { get; set; }
       
    }

    public class EmailReq
    {
        public bool IsBodyHtml { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public string Recipients { get; set; }
        public string Subject { get; set; }
        public string Attachement { get; set; }


    }

    public class Recipients
    {
        public string Name { get; set; }
        public string Email { get; set; }

    }

    public class Email
    {

        public long Id { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Name { get; set; }
        public string CarbonCopy { get; set; }
        public string Subject { get; set; }
        public string MailContent { get; set; }
    
        public int RetryCount { get; set; }
        public bool IsSent { get; set; }
        public bool IsBodyHtml { get; set; }
        public string Request { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseDesc { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid EmailRef { get; set; }

        /// <summary>
        /// Image in base64
        /// </summary>
        public string Attachment { get;  set; }
    }







    public class Recipient
    {
        public string name { get; set; }
        public string email { get; set; }
    }

    public class VenterRequest
    {
        public string subject { get; set; }
        public string body { get; set; }
        public string attachment { get; set; }
        public List<Recipient> recipients { get; set; }
    }

    public class VenterResponse
    {
        public string code { get; set; }
        public string message { get; set; }
    }
}
