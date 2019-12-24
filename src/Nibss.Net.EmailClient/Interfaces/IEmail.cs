using Nibss.Net.EmailClient.Models;


namespace Nibss.Net.EmailClient.Interfaces
{
    interface IEmail
    {
        EmailResponse Send(Email mailModel);

        EmailSentStatus QueryStatus(string requestID);

    }
}
