using System;
using System.Net.Sockets;
using System.IO;

namespace TCPasiakas2b
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

            String snd = "testiviesti";
 
            sw.WriteLine(snd);
            sw.Flush();
            String vastaus = sr.ReadLine();
            String[] split = vastaus.Split(";");

            Console.WriteLine(split[0]);
            Console.WriteLine(split[1]);

            Console.ReadKey();

            sw.Close();
            sr.Close();
            ns.Close();
            s.Close();
        }
    }
}
