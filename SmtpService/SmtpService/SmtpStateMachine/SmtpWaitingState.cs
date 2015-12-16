namespace Fenton.SmtpService
{
    internal class SmtpWaitingState : SmtpState
    {
        internal SmtpWaitingState(SmtpClient smtpClient)
            : base(smtpClient)
        {
        }

        protected override SmtpState Mail(string[] command)
        {
            if (!IsCommandNameValid(command, "FROM"))
            {
                smtpClient.SendMessage(SmtpResponses.SyntaxError);
                return this;
            }

            smtpClient.SendMessage(SmtpResponses.Okay);

            return new SmtpCreatedState(smtpClient);
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
