﻿using System;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace SMTPasiakas
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                s.Connect("localhost", 25000);
                //s.Connect("smtp.jyu.fi", 25);
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

            String email = "testi posti";

            Boolean on = true;
            String viesti = "";

            while (on) {

                viesti = sr.ReadLine();
                Console.WriteLine(viesti);
                String[] status = viesti.Split(' ');

                switch (status[0]) {
                    case "220":
                        sw.WriteLine("HELO jyu.fi");
                        break;
                    case "250":
                        switch (status[1])
                        {
                            
                            case "2.1.5":
                            sw.WriteLine("DATA: ");
                            break;
                            case "2.1.0":
                            sw.WriteLine("RCPT TO: ");
                            break;
                            case "2.0.0":
                            sw.WriteLine("MAIL FROM: ");
                            break;
                            default: //ITKP104 Postipalvelin HELO localhost[127.0.0.1], good to see you! 

                            sw.WriteLine("QUIT");
                            break;
                        }
                        break;
                    case "221":
                        on = false;
                        break;

                    default: 
                        Console.WriteLine("Virhe...");
                        sw.WriteLine("QUIT");
                        break;
                } // switch
                sw.Flush();
            } //while

            Console.ReadKey();

            sw.Close();
            sr.Close();
            ns.Close();
            s.Close();
        }
    }
}