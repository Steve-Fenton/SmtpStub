namespace Fenton.SmtpService
{
    internal class SmtpInitialState : SmtpState
    {
        public SmtpInitialState(SmtpClient smtpClient)
            : base(smtpClient)
        {
        }

        internal override SmtpState Connected()
        {
            smtpClient.SendMessage(SmtpResponses.Ready);
            return new SmtpReadyState(smtpClient);
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
