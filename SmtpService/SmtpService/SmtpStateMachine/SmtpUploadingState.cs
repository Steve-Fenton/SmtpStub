using System;

namespace Fenton.SmtpService
{
    internal class SmtpUploadingState : SmtpState
    {
        internal SmtpUploadingState(SmtpClient smtpClient)
            : base(smtpClient)
        {
        }

        internal override string Terminator
        {
            get
            {
                return "\r\n.\r\n";
            }
        }

        internal override SmtpState RunCommand(string message)
        {
            return Content(message);
        }

        protected override SmtpState Content(string content)
        {
            try
            {
                smtpClient.SmtpMessage = content;
            }
            catch (Exception)
            {
                smtpClient.ResetMessage();
                smtpClient.SendMessage(SmtpResponses.SyntaxErrorInParameters);
                return this;
            }

            smtpClient.PersistMessage();

            smtpClient.SendMessage(SmtpResponses.Okay);

            return new SmtpAcceptedState(smtpClient);
        }
    }
}
