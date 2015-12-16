namespace Fenton.SmtpService
{
    /// <summary>
    /// Describes the responses and replies codes for SMTP
    /// RFC 5310, 4.2
    /// </summary>
    internal static class SmtpResponses
    {
        // Positive Completion 2xx
        internal const string Ready = "220 READY\r\n";
        internal const string Bye = "221 BYE\r\n";
        internal const string Okay = "250 OK\r\n";
        internal const string HeloFormat = "250 HELO {0}\r\n";
        internal const string CannotVrfyButContinue = "252 Cannot VRFY user, but will accept message and attempt delivery\r\n";

        // Positive Intermediate 3xx
        internal const string SendData = "354 Send message content\r\n";

        // Transient Negative Completion 4xx
        internal const string ServiceNotAvailable = "421 Service not available\r\n";
        internal const string CannotAccomodateParameters = "455 Server unable to accomodate parameters\r\n";

        // Permanent Negative Completion 5xx
        internal const string SyntaxError = "500 Syntax error\r\n";
        internal const string CommandNotRecognised = "500 Command not recognized\r\n";
        internal const string SyntaxErrorInParameters = "501 Syntax error in parameters or arguments\r\n";
        internal const string InvalidOnwardDomain = "553 Requested action not taken: mailbox name not allowed\r\n";  
        internal const string BadSequenceOfCommands = "503 Bad sequence of commands\r\n";
    }
}
