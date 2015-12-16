namespace Fenton.SmtpService
{
    /// <summary>
    /// This class is unusual because it doesn't inherit directly from SmtpState,
    /// this is because it is identical to SmtpWaitingState with the exception
    /// of automatically clearing any previous message.
    /// </summary>
    internal sealed class SmtpAcceptedState : SmtpWaitingState
    {
        internal SmtpAcceptedState(SmtpClient smtpClient)
            : base(smtpClient)
        {
            smtpClient.ResetMessage();
        }
    }
}
