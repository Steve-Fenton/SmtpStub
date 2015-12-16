using System;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace Fenton.SmtpService
{
    public partial class SmtpStub : ServiceBase
    {
        private readonly ConfigurationProvider _configurationProvider;

        private TcpListener _tcpListener;
        private Thread _listenThread;

        public SmtpStub(ConfigurationProvider configurationProvider)
        {
            InitializeComponent();
            _configurationProvider = configurationProvider;
        }

        public void Init()
        {
            RunSmtpServer();
        }

        protected override void OnStart(string[] args)
        {
            Init();
        }

        protected override void OnStop()
        {
        }

        private void RunSmtpServer()
        {
            var listeningPort = _configurationProvider.ListeningPort;
            _tcpListener = TcpListener.Create(listeningPort);
            _listenThread = new Thread(new ThreadStart(ListenForClients));
            _listenThread.Start();
        }

        private void ListenForClients()
        {
            _tcpListener.Start();

            while (true)
            {
                    TcpClient client = _tcpListener.AcceptTcpClient();
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientCommunication));
                    clientThread.Start(client);
            }
        }

        private void HandleClientCommunication(object client)
        {
            try
            {
                using (TcpClient tcpClient = (TcpClient)client)
                {
                    ASCIIEncoding encoder = new ASCIIEncoding();
                    SmtpClient smtpClient = new SmtpClient(tcpClient.GetStream(), _configurationProvider);
                    SmtpState smtpState = new SmtpInitialState(smtpClient);

                    smtpState = smtpState.Connected();
                    Console.Write(".");

                    while (smtpState.IsActive && tcpClient.Client.Connected)
                    {
                        var terminator = encoder.GetBytes(smtpState.Terminator);

                        if (IsConnectionAlive(tcpClient))
                        {
                            break;
                        }

                        string message = smtpClient.ReadMessage(terminator).Result;

                        smtpState = smtpState.RunCommand(message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static bool IsConnectionAlive(TcpClient tcpClient)
        {
            try
            {
                return tcpClient.Client.Poll(1000, SelectMode.SelectRead) && tcpClient.Client.Available == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}