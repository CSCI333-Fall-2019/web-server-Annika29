using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebServer2._0
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
            System.Threading.ThreadPool.QueueUserWorkItem(StartServer, portNum);
            StartServer(portNum);
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
        private void StopEnd_Click(object sender, EventArgs e)
        {
            server.Stop();
        }

        public void SetForegroundData(string value)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetForegroundData), new object[] { value });
                return;
            }
        }

    }
}
