namespace Fenton.SmtpService
{
    internal class SmtpCreatedState : SmtpState
    {
        internal SmtpCreatedState(SmtpClient smtpClient)
            : base(smtpClient)
        {
        }

        protected override SmtpState Rcpt(string[] command)
        {
            if (!IsCommandNameValid(command, "TO"))
            {
                smtpClient.SendMessage(SmtpResponses.SyntaxError);
                return this;
            }

            smtpClient.SendMessage(SmtpResponses.Okay);

            return new SmtpAddressedState(smtpClient);
        }

        protected override SmtpState Rset()
        {
            return AllowRset();
        }

        protected override SmtpState Noop()
        {
            return AllowNoop();
        }

        protected override SmtpState Quit()
        {
            return AllowQuit();
        }
    }
}
