namespace Fenton.SmtpService
{
    internal class SmtpEndState : SmtpState
    {
        internal SmtpEndState(SmtpClient smtpClient)
            : base(smtpClient)
        {
        }

        internal override bool IsActive
        {
            get
            {
                return false;
            }
        }
    }
}
