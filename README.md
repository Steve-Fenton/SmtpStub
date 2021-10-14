# Smtp Stub

A simple SMTP server for testing that collects emails on a specified port and logs them to the file system.

Runs as a Windows Service or as a Console application.

 - Acts as a fully RFC 5310 compliant SMTP server
 - Can either record or discard emails

The full source code is available in this repository, or you can download a zip containing the working application from:

Just grab SmtpConsolev1.1.0.0.zip from the root folder.

You can install this as a Windows Service using PowerShell:

    New-Service -Name "SmtpStub" -BinaryPathName "C:\SmtpStub\Fenton.SmtpService.exe"

And there is a bit more about it here:

https://www.stevefenton.co.uk/2015/12/one-way-to-never-send-an-email-from-your-test-environment/