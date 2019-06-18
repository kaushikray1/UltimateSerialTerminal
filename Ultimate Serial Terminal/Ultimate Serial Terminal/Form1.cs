using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;

namespace Ultimate_Serial_Terminal
{
    public partial class Form1 : Form
    {
        readonly SerialPort ComPort = new SerialPort();
        delegate void SetTextCallback(string text);
        

        public Form1()
        {
            InitializeComponent();
            ComPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(DataReceivedHandler);
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            string InComingData = String.Empty;
            InComingData = ComPort.ReadExisting();
            if (InComingData != String.Empty)
            {
                this.BeginInvoke(new SetTextCallback(SetText), new object[] { InComingData });
            }
        }
        private void SetText(string text)
        {
            this.richTextBox1 .Text += text;
            if (checkBox1.Checked)
            {
                this.richTextBox1.SelectionStart = this.richTextBox1.Text.Length;
                this.richTextBox1.ScrollToCaret();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ArrayComPortsNames = null;
            int index = -1;
            string ComPortName = null;
            button1.BackColor = Color.PaleVioletRed;
            //Com Ports
            try
            {
                ArrayComPortsNames = SerialPort.GetPortNames();
                do
                {
                    index += 1;
                    comboBox3.Items.Add(ArrayComPortsNames[index]);

                } while (!((ArrayComPortsNames[index] == ComPortName) || (index == ArrayComPortsNames.GetUpperBound(0))));
                Array.Sort(ArrayComPortsNames);


                if (index == ArrayComPortsNames.GetUpperBound(0))
                {
                    ComPortName = ArrayComPortsNames[0];
                }
                //get first item print in text
                comboBox3.Text = ArrayComPortsNames[0];
            }
            catch
            {
                MessageBox.Show("No Serial Device found", "Error", MessageBoxButtons.OK);
            }

            //Baud Rate
            comboBox1.Items.Add(2000000);
            comboBox1.Items.Add(300);
            comboBox1.Items.Add(600);
            comboBox1.Items.Add(1200);
            comboBox1.Items.Add(2400);
            comboBox1.Items.Add(4800);
            comboBox1.Items.Add(9600);
            comboBox1.Items.Add(14400);
            comboBox1.Items.Add(19200);
            comboBox1.Items.Add(38400);
            comboBox1.Items.Add(56000);
            comboBox1.Items.Add(57600);
            comboBox1.Items.Add(74880);
            comboBox1.Items.Add(115200);
            comboBox1.Items.Add(230400);
            comboBox1.Items.Add(250000);
            comboBox1.Items.Add(500000);
            comboBox1.Items.Add(1000000);

            comboBox1.Items.ToString();
            //get first item print in text
            comboBox1.Text = comboBox1.Items[0].ToString();

            //Data Bits
            comboBox2.Items.Add(8);
            comboBox2.Items.Add(7);
            //get the first item print it in the text 
            comboBox2.Text = comboBox2.Items[0].ToString();

            //Stop Bits
            comboBox4.Items.Add("One");
            comboBox4.Items.Add("OnePointFive");
            comboBox4.Items.Add("Two");
            //get the first item print in the text
            comboBox4.Text = comboBox4.Items[0].ToString();
            //Parity 
            comboBox5.Items.Add("None");
            comboBox5.Items.Add("Even");
            comboBox5.Items.Add("Mark");
            comboBox5.Items.Add("Odd");
            comboBox5.Items.Add("Space");
            //get the first item print in the text
            comboBox5.Text = comboBox5.Items[0].ToString();
            //Handshake
            comboBox6.Items.Add("None");
            comboBox6.Items.Add("XOnXOff");
            comboBox6.Items.Add("RequestToSend");
            comboBox6.Items.Add("RequestToSendXOnXOff");
            //get the first item print it in the text 
            comboBox6.Text = comboBox6.Items[0].ToString();

            //Item for endline
            comboBox7.Items.Add("No End Line");
            comboBox7.Items.Add("New Line");
            comboBox7.Items.Add("Carriage Return");
            comboBox7.Items.Add("Both NL & CR");
            //get the first item print it in the text 
            comboBox7.Text = comboBox7.Items[0].ToString();
        }

        private void Button8_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Port Closed")
            {
                try
                {
                    ComPort.PortName = Convert.ToString(comboBox3.Text);
                    ComPort.BaudRate = Convert.ToInt32(comboBox1.Text);
                    ComPort.DataBits = Convert.ToInt16(comboBox2.Text);
                    ComPort.Parity = (Parity)Enum.Parse(typeof(Parity), comboBox5.Text);
                    ComPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), comboBox6.Text);
                    ComPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), comboBox4.Text);
                
                    ComPort.Open();
                    ComPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                    button1.Text = "Port Open";
                    button1.BackColor = Color.PaleGreen;
                }
                catch
                {
                    MessageBox.Show("Cannot open port", "Error", MessageBoxButtons.OK);
                }
            }
            else if (button1.Text == "Port Open")
            {
                button1.Text = "Port Closed";
                button1.BackColor = Color.PaleVioletRed;
                ComPort.Close();
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkBox4.Checked)
                {
                    switch (comboBox7.Text)
                    {
                        case "No End Line":
                            ComPort.Write(textBox1.Text);
                            richTextBox1.Text += textBox1.Text;
                            break;
                        case "New Line":
                            ComPort.Write(textBox1.Text);
                            ComPort.Write("\n");
                            richTextBox1.Text += textBox1.Text;
                            richTextBox1.Text += "\n";
                            break;
                        case "Carriage Return":
                            ComPort.Write(textBox1.Text);
                            ComPort.Write("\r");
                            richTextBox1.Text += textBox1.Text;
                            richTextBox1.Text += "\r";
                            break;
                        case "Both NL & CR":
                            ComPort.Write(textBox1.Text);
                            ComPort.Write("\n\r");
                            richTextBox1.Text += textBox1.Text;
                            richTextBox1.Text += "\n\r";
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("NOT IMPLEMENTED YET", "need to be added", MessageBoxButtons.OK);
                }
            }
            catch
            {
                MessageBox.Show("Port is not open.", "Error", MessageBoxButtons.OK);
            }
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                ComPort.DtrEnable = true;
            }
            else
            {
                ComPort.DtrEnable = false;
            }
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                ComPort.RtsEnable = true;
            }
            else
            {
                ComPort.RtsEnable = false;
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkBox7.Checked)
                {
                    switch (comboBox7.Text)
                    {
                        case "No End Line":
                            ComPort.Write(textBox1.Text);
                            richTextBox1.Text += textBox2.Text;
                            break;
                        case "New Line":
                            ComPort.Write(textBox2.Text);
                            ComPort.Write("\n");
                            richTextBox1.Text += textBox2.Text;
                            richTextBox1.Text += "\n";
                            break;
                        case "Carriage Return":
                            ComPort.Write(textBox2.Text);
                            ComPort.Write("\r");
                            richTextBox1.Text += textBox2.Text;
                            richTextBox1.Text += "\r";
                            break;
                        case "Both NL & CR":
                            ComPort.Write(textBox2.Text);
                            ComPort.Write("\n\r");
                            richTextBox1.Text += textBox2.Text;
                            richTextBox1.Text += "\n\r";
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("NOT IMPLEMENTED YET", "need to be added", MessageBoxButtons.OK);
                }
            }
            catch
            {
                MessageBox.Show("Port is not open.", "Error", MessageBoxButtons.OK);
            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkBox8.Checked)
                {
                    switch (comboBox7.Text)
                    {
                        case "No End Line":
                            ComPort.Write(textBox1.Text);
                            richTextBox1.Text += textBox3.Text;
                            break;
                        case "New Line":
                            ComPort.Write(textBox3.Text);
                            ComPort.Write("\n");
                            richTextBox1.Text += textBox3.Text;
                            richTextBox1.Text += "\n";
                            break;
                        case "Carriage Return":
                            ComPort.Write(textBox3.Text);
                            ComPort.Write("\r");
                            richTextBox1.Text += textBox3.Text;
                            richTextBox1.Text += "\r";
                            break;
                        case "Both NL & CR":
                            ComPort.Write(textBox3.Text);
                            ComPort.Write("\n\r");
                            richTextBox1.Text += textBox3.Text;
                            richTextBox1.Text += "\n\r";
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("NOT IMPLEMENTED YET", "need to be added", MessageBoxButtons.OK);
                }
            }
            catch
            {
                MessageBox.Show("Port is not open.", "Error", MessageBoxButtons.OK);
            }
        }

    }
}
