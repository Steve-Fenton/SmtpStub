namespace Fenton.SmtpService
{
    /// <summary>
    /// This class is unusual because it doesn't inherit directly from SmtpState,
    /// this is because it is identical to SmtpCreatedState with the exception
    /// of allowing the Data command.
    /// </summary>
    internal sealed class SmtpAddressedState : SmtpCreatedState
    {
        internal SmtpAddressedState(SmtpClient smtpClient)
            : base(smtpClient)
        {
        }

        protected override SmtpState Data()
        {
            smtpClient.SendMessage(SmtpResponses.SendData);
            return new SmtpUploadingState(smtpClient);
        }
    }
}
