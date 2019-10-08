using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkingServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int portNum = 5555;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        TcpListener server = null;

        private void button1_Click(object sender, EventArgs e)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(StartServer, this);
        }

        private void StartServer(object state)
        {
            throw new NotImplementedException();
        }

        private void StartServer(int port)
        {
            server = new TcpListener(localAddr, port);
            server.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            server.Stop();
        }

        public void SetForegroundData(string value)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetForegroundData), new object[] { value });
            }
        }

        static string CreateHeader(string sHttpVersion, string sMIMEHeader, long iTotBytes, string sStatusCode, ref Socket mySocket)
        {
            String sBuffer = "";
            // if Mime type is not provided set default to text/html  
            if (sMIMEHeader.Length == 0)
            {
                sMIMEHeader = "text/html";// Default Mime Type is text/html  
            }
            sBuffer = sBuffer + sHttpVersion + sStatusCode + "\r\n";
            sBuffer = sBuffer + "Server: cx1193719-b\r\n";
            sBuffer = sBuffer + "Content-Type: " + sMIMEHeader + "\r\n";
            sBuffer = sBuffer + "Accept-Ranges: bytes\r\n";
            sBuffer = sBuffer + "Content-Length: " + iTotBytes + "\r\n\r\n";
            Byte[] bSendData = Encoding.ASCII.GetBytes(sBuffer);
            //SendToBrowser(bSendData, ref mySocket);
            Console.WriteLine("Total Bytes : " + iTotBytes.ToString());
            return sBuffer; 
        }

        //string header = CreateHeader(@"HTTP/1.0", @"text/html", destination.Length, " 200 OK");
        //lClient.Client.Send(Encoding.ASCII.GetBytes(header));

        static void StartListen(Object stateInfo)
        {
            if (stateInfo == null)
            {
                return;
            }

            frMain main = (frMain)stateInfo;
            string response = String.Empty;
            int numBytes = 0;

            while (main.IsRunning)
            {
                try
                {
                    //TcpClient client = main.listener.AcceptTcpClient();
                   // ThreadPool.QueueUserWorkItem(GetRequestedItem, client);
                }
                catch (Exception err)
                {
                    Console.WriteLine("Error: " + err.Message);
                }
            }
        }

        private static void GetRequestedItem(object state)
        {
            throw new NotImplementedException();
        }

        public void SendToBrowser(String sData, ref Socket mySocket)
        {
            SendToBrowser(Encoding.ASCII.GetBytes(sData), ref mySocket);
        }
        public void SendToBrowser(Byte[] bSendData, ref Socket mySocket)
        {
            int numBytes = 0;
            try
            {
                if (mySocket.Connected)
                {
                    if ((numBytes = mySocket.Send(bSendData, bSendData.Length, 0)) == -1)
                        Console.WriteLine("Socket Error cannot Send Packet");
                    else
                    {
                        Console.WriteLine("No. of bytes send {0}", numBytes);
                    }
                }
                else Console.WriteLine("Connection Dropped....");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Occurred : {0} ", e);
            }
        }

        private bool _isRunning;
        static readonly object _isRunningLock = new object();
        public bool IsRunning { 
            get
            {
                lock (_isRunningLock)
                {
                    return this._isRunning;
                }
            } 
            set
            {
                lock (_isRunningLock)
                {
                    this._isRunning = value;
                }
                button1.Enabled = !value;
                button2.Enabled = value;
            }
        }
    }

    internal class frMain
    {
        internal object listener;

        public bool IsRunning { get; internal set; }
    }
}
