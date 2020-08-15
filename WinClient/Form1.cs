using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.ServiceModel.WebSockets;
using WebSocketSharp;

namespace WinClient
{
    public partial class Form1 : Form
    {

        WebSocket ws = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ws != null)
            {
                string text = textBox2.Text;
                try
                {
                    ws.Send(text);
                    textBox1.Text += "sent" + text + Environment.NewLine;
                }
                catch (Exception ex)
                {
                    textBox1.Text += "warning" + ex + Environment.NewLine;
                    throw ex;
                }
           
            }
            else
            {
                throw new Exception("socket is null");
            }
        }
        protected override void OnClosed(EventArgs e)
        {
            if (ws != null)
            {
                ws.OnClose += (s, d) =>
                {
                    textBox1.Text += "Socket Status: " + ws.ReadyState + Environment.NewLine;
                    Console.WriteLine("OnClose:");
                };
            }
            base.OnClosed(e);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ws = new WebSocket("ws://localhost:20001/EchoService2");

            try
            {
                ws.OnMessage += (s, d) =>
                {
                    Invoke(new Action(() =>
                    {
                        textBox1.Text += "Received: " + d.Data + Environment.NewLine;
                    }));
                    Console.WriteLine("Laputa says: " + d.Data);
                };
                ws.OnOpen += (s, d) =>
                {
                    textBox1.Text += "Socket Status: " + ws.ReadyState + Environment.NewLine;
                    Console.WriteLine("OnOpen: ");
                };
               
                ws.OnError += (s, d) =>
                {
                    textBox1.Text += "Error" + Environment.NewLine;
                    Console.WriteLine("OnError: ");
                };
                ws.Connect();
            
            }
            catch (Exception ex)
            {
                textBox1.Text += "Error" + ex + Environment.NewLine;
                throw ex;
            }

        }
    }
}
