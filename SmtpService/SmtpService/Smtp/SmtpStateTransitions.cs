namespace Fenton.SmtpService
{
    internal static class SmtpStateTransition
    {
        internal const string HeloCommand = "HELO";
        internal const string EhloCommand = "EHLO";
        internal const string MailCommand = "MAIL";
        internal const string RcptCommand = "RCPT";
        internal const string DataCommand = "DATA";
        internal const string RsetCommand = "RSET";
        internal const string NoOpCommand = "NOOP";
        internal const string QuitCommand = "QUIT";
        internal const string VrfyCommand = "VRFY";
    }
}
