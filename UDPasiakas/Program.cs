﻿using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

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
                        int received =0;

                        try 
                        {
                            received = s.ReceiveFrom(rec, ref Palvelinep);
                            String rec_string = Encoding.ASCII.GetString(rec);
                            char[] delim = { ';' };
                            String[] viesti = rec_string.Split(delim, 2);
                            if (viesti.Length < 2)
                            {   
                                // virheviesti
                            }
                            else
                            {
                                Console.WriteLine("{0}: {1}", viesti[0], viesti[1]); 
                            }
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
