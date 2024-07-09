using System;
using System.Media;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Globalization; // Importar el espacio de nombres


namespace ClientApp
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private string clientName;
        private string serverIP; // Campo para la direcci�n IP

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            clientName = Prompt.ShowDialog("Nombre:", "Nombre de Usuario");
            if (string.IsNullOrEmpty(clientName))
            {
                MessageBox.Show("El nombre no puede estar vac�o.");
                this.Close();
                return;
            }

            serverIP = Prompt.ShowDialog("Direcci�n IP:", "Conectar al Servidor");
            if (string.IsNullOrEmpty(serverIP))
            {
                MessageBox.Show("La direcci�n IP no puede estar vac�a.");
                this.Close();
                return;
            }

            // Validar el formato de la direcci�n IP
            if (!IPAddress.TryParse(serverIP, out _))
            {
                MessageBox.Show("Direcci�n IP no v�lida.");
                this.Close();
                return;
            }

            try
            {
                client = new TcpClient(serverIP, 5000);
                stream = client.GetStream();
                UpdateStatus("Conectado al servidor...");

                // Send the client's name to the server
                byte[] nameData = Encoding.ASCII.GetBytes(clientName);
                stream.Write(nameData, 0, nameData.Length);

                Thread receiveThread = new Thread(ReceiveMessages);
                receiveThread.IsBackground = true; // Ensure the thread closes when the application closes
                receiveThread.Start();
            }
            catch (SocketException)
            {
                MessageBox.Show("No se pudo conectar al servidor. Verifique la direcci�n IP e intente de nuevo.");
                this.Close();
            }
            catch (Exception ex)
            {
                UpdateStatus("Error al conectarse al servidor: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendMessage();
                e.Handled = true; // Prevent the beep sound on Enter
            }
        }

        private void SendMessage()
        {
            string message = textBox1.Text;
            if (!string.IsNullOrEmpty(message))
            {
                string timeStamp = DateTime.Now.ToString("HH:mm", CultureInfo.InvariantCulture);
                string messageWithTime = $"{message} ({timeStamp})";
                UpdateStatus("Yo: " + messageWithTime); // Show the sent message in the UI
                byte[] data = Encoding.ASCII.GetBytes(messageWithTime);
                stream.Write(data, 0, data.Length);
                textBox1.Clear(); // Clear the text box after sending
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendBuzz();
        }

        private void SendBuzz()
        {
            string buzzMessage = "BUZZ";
            byte[] data = Encoding.ASCII.GetBytes(buzzMessage);
            stream.Write(data, 0, data.Length);
        }

        private void ReceiveMessages()
        {
            byte[] buffer = new byte[1024];
            int bytesRead;

            try
            {
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    if (message == "BUZZ")
                    {
                        ShowBuzz();
                    }
                    else
                    {
                        UpdateStatus(message);
                    }
                }
            }
            catch (Exception e)
            {
                UpdateStatus("Error: " + e.Message);
            }
        }

        private void ShowBuzz()
        {
            SystemSounds.Beep.Play();
            // Additional code to vibrate the window
            var originalLocation = this.Location;
            var rnd = new Random();
            const int shakeAmplitude = 20;
            for (int i = 0; i < 10; i++)
            {
                this.Location = new System.Drawing.Point(
                    originalLocation.X + rnd.Next(-shakeAmplitude, shakeAmplitude),
                    originalLocation.Y + rnd.Next(-shakeAmplitude, shakeAmplitude));
                Thread.Sleep(20);
            }
            this.Location = originalLocation;
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
            client.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}
