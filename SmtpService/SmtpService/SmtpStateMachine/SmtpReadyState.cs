namespace Fenton.SmtpService
{
    internal class SmtpReadyState : SmtpState
    {
        internal SmtpReadyState(SmtpClient smtpClient)
            : base(smtpClient)
        {
        }

        protected override SmtpState Helo(string[] command)
        {
            if (!IsNameArgumentPresent(command))
            {
                smtpClient.SendMessage(SmtpResponses.SyntaxError);
                return this;
            }

            var message = string.Format(SmtpResponses.HeloFormat, command[1]);

            smtpClient.SendMessage(message);

            return new SmtpWaitingState(smtpClient);
        }

        protected override SmtpState Noop()
        {
            return AllowNoop();
        }

        protected override SmtpState Quit()
        {
            return AllowQuit();
        }

        private static bool IsNameArgumentPresent(string[] command)
        {
            return command.Length > 1 && !string.IsNullOrWhiteSpace(command[1]);
        }
    }
}
