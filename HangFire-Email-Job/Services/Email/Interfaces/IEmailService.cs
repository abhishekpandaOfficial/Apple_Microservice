namespace HangFire_Email_Job.Services.Email.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string backGroundJobType, string startTime);

    }
}
