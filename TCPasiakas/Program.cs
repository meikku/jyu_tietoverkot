﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace TCPasiakas
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Connect("localhost", 25000);
            String snd = "GET / HTTP/1.1\r\nHost: localhost\r\n\r\n";

            byte[] buffer = Encoding.ASCII.GetBytes(snd);

            s.Send(buffer);

            String sivu = "";
            int count = 0;

            do {
            byte[] rec = new byte[1024];

            count = s.Receive(rec);
            Console.Write("tavuja vastaanotettu " + count + "\r\n");
            sivu += Encoding.ASCII.GetString(rec, 0, count); 
            } while (count > 0);

            Console.Write(sivu);

            Console.ReadKey();
            s.Close();
        }
    }
}
