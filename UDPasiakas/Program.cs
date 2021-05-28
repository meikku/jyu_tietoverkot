using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace UDPasiakas
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            int port = 9999;

            IPEndPoint iep = new IPEndPoint(IPAddress.Loopback, port);
            byte[] rec = new byte[256];

            EndPoint ep = (EndPoint)iep;
            s.ReceiveTimeout = 1000;
            String msg;
            Boolean on = true;
            do
            {
                Console.Write(">");
                msg = Console.ReadLine();
                if (msg.Equals("q"))
                {
                    on = false;
                }
                else 
                {
                    s.SendTo(Encoding.ASCII.GetBytes(msg), ep);

                    while(!Console.KeyAvailable)
                    {
                        IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);
                        EndPoint Palvelinep = (EndPoint)remote;
                        int p = 0; 

                        try 
                        {
                            s.ReceiveFrom(rec, ref Palvelinep);
                            // split string , onko pituus 2? 
                            // jos on, tulosta näyttöön
                        } 
                        catch 
                        {
                            // timeout
                        } 
                    }
                }
            } while (on);
            //Console.ReadKey();
            s.Close();
        }
    }
}
