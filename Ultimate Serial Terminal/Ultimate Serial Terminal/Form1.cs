﻿using System;
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
        SerialPort ComPort = new SerialPort();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ArrayComPortsNames = null;
            int index = -1;
            string ComPortName = null;

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
                button1.Text = "Port Open";
                ComPort.PortName = Convert.ToString(comboBox3.Text);
                ComPort.BaudRate = Convert.ToInt32(comboBox1.Text);
                ComPort.DataBits = Convert.ToInt16(comboBox2.Text);
                ComPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), comboBox6.Text);
                ComPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), comboBox5.Text);
                ComPort.Parity = (Parity)Enum.Parse(typeof(Parity), comboBox4.Text);
                ComPort.Open();
            }
            else if (button1.Text == "Port Open")
            {
                button1.Text = "Port Closed";
                ComPort.Close();

            }
        }
    }
}