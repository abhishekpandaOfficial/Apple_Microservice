using HangFire_Email_Job.Services.Email.Interfaces;

namespace HangFire_Email_Job.Services.Email
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string backGroundJobType, string startTime)
        {
            Console.WriteLine(backGroundJobType + " - " + startTime + " - Email Sent - " + DateTime.Now.ToLongTimeString());
        }
    }
}
