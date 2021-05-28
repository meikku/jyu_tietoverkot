using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace TCPasiakas
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                 s.Connect("localhost", 25000);
            }
            catch (Exception ex)
            {
                Console.Write("Virhe: " + ex.Message);
                Console.ReadKey();
                return;
            }

            NetworkStream ns = new NetworkStream(s);

            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);

            String snd = "GET / HTTP/1.1\r\nHost: localhost\r\n\r\n";


            // byte[] buffer = Encoding.ASCII.GetBytes(snd);

            // s.Send(buffer);

            
            sw.WriteLine(snd);
            sw.Flush();
            String vastaus = sr.ReadToEnd();
            String[] sivu = vastaus.Split("\r\n\r\n", 2);

            

            // int count = 0;

            // do {
            // byte[] rec = new byte[1024];

            // count = s.Receive(rec);
            // Console.Write("tavuja vastaanotettu " + count + "\r\n");
            // sivu += Encoding.ASCII.GetString(rec, 0, count); 
            // } while (count > 0);

            Console.Write(sivu[1]);

            Console.ReadKey();

            sw.Close();
            sr.Close();
            ns.Close();
            s.Close();
           
        }
    }
}
