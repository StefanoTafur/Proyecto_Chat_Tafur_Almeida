using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ServerApp
{
    public partial class Form1 : Form
    {
        private TcpListener listener;
        private bool isRunning = true;
        private List<TcpClient> clients = new List<TcpClient>();
        private Dictionary<TcpClient, string> clientNames = new Dictionary<TcpClient, string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread serverThread = new Thread(StartServer);
            serverThread.Start();
        }

        private void StartServer()
        {
            listener = new TcpListener(IPAddress.Any, 13002);
            listener.Start();
            UpdateStatus("Servidor iniciado en el puerto 13002...");

            while (isRunning)
            {
                var client = listener.AcceptTcpClient();
                lock (clients)
                {
                    clients.Add(client);
                }
                Thread clientThread = new Thread(HandleClient);
                clientThread.Start(client);
            }
        }

        private void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            try
            {
                // Read the client's name
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                string clientName = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                lock (clientNames)
                {
                    clientNames[client] = clientName;
                }
                UpdateStatus(clientName + " se ha conectado.");
                BroadcastMessage(client, clientName + " se ha conectado.");

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    // Check if the message is a buzz command
                    if (message == "BUZZ")
                    {
                        UpdateStatus(clientName + " envió un zumbido.");
                        BroadcastMessage(client, "BUZZ");
                    }
                    else
                    {
                        UpdateStatus(clientName + ": " + message);
                        BroadcastMessage(client, clientName + ": " + message);
                    }
                }
            }
            catch (Exception e)
            {
                UpdateStatus("Error: " + e.Message);
            }
            finally
            {
                lock (clients)
                {
                    clients.Remove(client);
                }
                string clientName;
                lock (clientNames)
                {
                    clientNames.TryGetValue(client, out clientName);
                    clientNames.Remove(client);
                }
                client.Close();
                UpdateStatus(clientName + " se ha desconectado.");
                BroadcastMessage(client, clientName + " se ha desconectado.");
            }
        }

        private void BroadcastMessage(TcpClient sender, string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            lock (clients)
            {
                foreach (var client in clients)
                {
                    if (client != sender)
                    {
                        try
                        {
                            NetworkStream stream = client.GetStream();
                            stream.Write(data, 0, data.Length);
                        }
                        catch (Exception e)
                        {
                            UpdateStatus("Error al enviar mensaje: " + e.Message);
                        }
                    }
                }
            }
        }

        private void UpdateStatus(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateStatus), message);
                return;
            }
            listBox1.Items.Add(message);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            isRunning = false;
            listener.Stop();
            lock (clients)
            {
                foreach (var client in clients)
                {
                    client.Close();
                }
            }
        }
    }
}
