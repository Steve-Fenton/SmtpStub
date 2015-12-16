namespace Fenton.SmtpService
{
    public class SmtpState
    {
        protected SmtpClient smtpClient;

        private ConfigurationProvider _configurationProvider = new ConfigurationProvider();

        protected SmtpState(SmtpClient smtpClient)
        {
            this.smtpClient = smtpClient;
        }

        internal virtual bool IsActive
        {
            get
            {
                return true;
            }
        }

        internal virtual string Terminator
        {
            get
            {
                return "\r\n";
            }
        }

        internal virtual SmtpState RunCommand(string message)
        {
            //Split on any whitespace
            string[] command = message.Split(null);
            if (command.Length < 1)
            {
                smtpClient.SendMessage(SmtpResponses.SyntaxError);
                return this;
            }

            switch (command[0].ToUpperInvariant())
            {
                case SmtpStateTransition.HeloCommand:
                    return Helo(command);
                case SmtpStateTransition.EhloCommand:
                    return Ehlo(command);
                case SmtpStateTransition.MailCommand:
                    return Mail(command);
                case SmtpStateTransition.RcptCommand:
                    return Rcpt(command);
                case SmtpStateTransition.DataCommand:
                    return Data();
                case SmtpStateTransition.RsetCommand:
                    return Rset();
                case SmtpStateTransition.NoOpCommand:
                    return Noop();
                case SmtpStateTransition.QuitCommand:
                    return Quit();
                case SmtpStateTransition.VrfyCommand:
                    return Vrfy();
                default:
                    return Content(message);
            }
        }

        internal virtual SmtpState Connected()
        {
            smtpClient.SendMessage(SmtpResponses.BadSequenceOfCommands);
            return this;
        }

        protected virtual SmtpState Helo(string[] command)
        {
            smtpClient.SendMessage(SmtpResponses.BadSequenceOfCommands);
            return this;
        }

        protected virtual SmtpState Ehlo(string[] command)
        {
            smtpClient.SendMessage(SmtpResponses.CommandNotRecognised);
            return this;
        }

        protected virtual SmtpState Mail(string[] command)
        {
            smtpClient.SendMessage(SmtpResponses.BadSequenceOfCommands);
            return this;
        }

        protected virtual SmtpState Rcpt(string[] command)
        {
            smtpClient.SendMessage(SmtpResponses.BadSequenceOfCommands);
            return this;
        }

        protected virtual SmtpState Data()
        {
            smtpClient.SendMessage(SmtpResponses.BadSequenceOfCommands);
            return this;
        }

        protected virtual SmtpState Rset()
        {
            smtpClient.SendMessage(SmtpResponses.BadSequenceOfCommands);
            return this;
        }

        protected virtual SmtpState Noop()
        {
            smtpClient.SendMessage(SmtpResponses.BadSequenceOfCommands);
            return this;
        }

        protected virtual SmtpState Quit()
        {
            smtpClient.SendMessage(SmtpResponses.BadSequenceOfCommands);
            return this;
        }

        protected virtual SmtpState Vrfy()
        {
            smtpClient.SendMessage(SmtpResponses.CannotVrfyButContinue);
            return this;
        }

        protected virtual SmtpState Content(string content)
        {
            smtpClient.SendMessage(SmtpResponses.CommandNotRecognised);
            return this;
        }

        /// <summary>
        /// Allows all sub classes to share the same Rset method
        /// </summary>
        protected SmtpState AllowRset()
        {
            smtpClient.ResetMessage();
            smtpClient.SendMessage(SmtpResponses.Okay);
            return new SmtpWaitingState(smtpClient);
        }

        /// <summary>
        /// Allows all sub classes to share the same Noop method
        /// </summary>
        protected SmtpState AllowNoop()
        {
            smtpClient.SendMessage(SmtpResponses.Okay);
            return this;
        }

        /// <summary>
        /// Allows all sub classes to share the same Quit method
        /// </summary>
        protected SmtpState AllowQuit()
        {
            smtpClient.SendMessage(SmtpResponses.Bye);
            return new SmtpEndState(smtpClient);
        }

        protected static bool IsCommandNameValid(string[] command, string expected)
        {
            return command.Length > 1 && command[1].Trim().ToUpperInvariant().StartsWith(expected);
        }
    }
}
