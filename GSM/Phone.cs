using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using SMSapplication;

namespace SmsBulkSender.GSM
{
    public class Phone : IDisposable
    {
        private const string SerialPortName = "/dev/ttyAMA0";
        private const int SerialPortBaudRate = 38400;
        private const int DataBits = 8;
        private const int ReadTimeout = 300;
        private const int WriteTimeout = 300;

        private const int SeriousIntervalInMs = 25;

        private SerialPort phoneSerialPort;
        private clsSMS objClsSms = new clsSMS();
    
        private PhoneBook phoneBook = new PhoneBook();

        /// <summary>
        /// Creates new instance of GSM phone.
        /// </summary>
        public Phone()
        {
            phoneSerialPort = objClsSms.OpenPort(SerialPortName, SerialPortBaudRate, DataBits, ReadTimeout, WriteTimeout);
        }

        public void Dispose()
        {
            objClsSms.ClosePort(phoneSerialPort);
        }

        /// <summary>
        /// Send SMS to specified receiver.
        /// </summary>
        /// <param name="receiver">Mobile phone number of receiver</param>
        /// <param name="content">Sms text</param>
        public void SendSMS(string receiver, string content)
        {
            objClsSms.SendMsg(phoneSerialPort, receiver, content);
        }

        /// <summary>
        /// Send SMS to specified group members.
        /// </summary>
        /// <param name="group">Name of receiving group</param>
        /// <param name="content">Sms text</param>
        public void SendGroupSMS(string group, string content)
        {
            foreach(var record in phoneBook.ListAllGroupMembers(group))
            {
                SendSMS(record.PhoneNumber, content);

                // Wait is used just to make app looking more serious
                Thread.Sleep(SeriousIntervalInMs);
            }
        }
    }
}
