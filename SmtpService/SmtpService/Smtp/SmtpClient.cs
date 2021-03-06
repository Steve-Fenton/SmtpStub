﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Fenton.SmtpService
{
    public class SmtpClient
    {
        private readonly NetworkStream _clientStream;
        private readonly ASCIIEncoding _encoder;
        private readonly string _loggingDirectory;
        private readonly bool _saveMessages;
        private readonly int _messageLimit;
        private readonly string _emailFileExtension = ".eml";

        public SmtpClient(NetworkStream clientStream, ConfigurationProvider configurationProvider)
        {
            _clientStream = clientStream;
            _encoder = new ASCIIEncoding();
            _loggingDirectory = configurationProvider.LoggingDirectory;
            _saveMessages = configurationProvider.SaveMessages;
            _messageLimit = configurationProvider.MessageLimit;
            Directory.CreateDirectory(configurationProvider.LoggingDirectory);
        }

        public string SmtpMessage { get; set; }

        public void PersistMessage()
        {
            if (_saveMessages)
            {
                string path = Path.Combine(_loggingDirectory, Guid.NewGuid().ToString() + _emailFileExtension);
                File.WriteAllText(path, SmtpMessage);

                CleanOldMessages();
            }
        }

        public void ResetMessage()
        {
            SmtpMessage = string.Empty;
        }

        public void SendMessage(string message)
        {
            try
            {
                byte[] buffer = _encoder.GetBytes(message);

                _clientStream.Write(buffer, 0, buffer.Length);
                _clientStream.Flush();
            }
            catch (IOException)
            {
                // Cannot write to stream.
            }
        }

        public async Task<string> ReadMessage(byte[] messageTerminator)
        {
            var messageData = new List<byte>();
            var buffer = new byte[8196];
            do
            {
                var bytesRead = await _clientStream.ReadAsync(buffer, 0, buffer.Length);

                if (bytesRead == 0)
                {
                    break;
                }

                messageData.AddRange(new ArraySegment<byte>(buffer, 0, bytesRead));
            }
            while (!MessageTerminated(messageData, messageTerminator));

            return _encoder.GetString(messageData.ToArray());
        }


        private void CleanOldMessages()
        {
            if (_messageLimit > 0)
            {
                var oldFiles = Directory.GetFiles(_loggingDirectory).Where(f => new FileInfo(f).Extension == _emailFileExtension).OrderByDescending(f => new FileInfo(f).CreationTimeUtc).Skip(_messageLimit).ToList();
                foreach (var file in oldFiles)
                {
                    File.Delete(file);
                }
            }
        }

        private bool MessageTerminated(List<byte> messageData, byte[] messageTerminator)
        {
            if (TheTerminatorCouldNotFitInTheData(messageData, messageTerminator))
            {
                return false;
            }

            var comparisonIndex = messageData.Count - messageTerminator.Length;
            for (int i = comparisonIndex; i < messageData.Count; i++)
            {
                if (messageData[i] != messageTerminator[i - comparisonIndex])
                {
                    return false;
                }
            }

            return true;
        }

        private static bool TheTerminatorCouldNotFitInTheData(IReadOnlyCollection<byte> messageData, ICollection<byte> messageTerminator)
        {
            return messageData.Count < messageTerminator.Count;
        }
    }
}
