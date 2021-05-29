using System;
using System.Net.Sockets;
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
            String lahettaja = "meikku@jyu.fi";
            String vastaanottaja = "joku@jyu.fi";
            Boolean on = true;
            String viesti = "";
            String lahetys = "";

            while (on) {

                viesti = sr.ReadLine();
                Console.WriteLine(viesti);
                String[] status = viesti.Split(' ');

                switch (status[0]) {
                    case "220":
                        lahetys = "HELO jyu.fi";
                        sw.WriteLine(lahetys);
                        break;
                    case "250":
                        switch (status[1])
                        {
                            case "2.0.0":
                            lahetys = "QUIT"; // täältä ei tule lopetuskuittausta
                            sw.WriteLine(lahetys);
                            return;
                            case "2.1.0":
                            lahetys = "RCPT TO: " + vastaanottaja;
                            sw.WriteLine(lahetys);
                            break;
                            case "2.1.5":
                            lahetys = "DATA";
                            sw.WriteLine(lahetys);
                            break;
                            default: //ITKP104 Postipalvelin HELO localhost[127.0.0.1], good to see you! 
                            lahetys = "MAIL FROM: " + lahettaja;
                            sw.WriteLine(lahetys);
                            break; 
                        }
                        break;
                    case "221":
                        on = false;
                        break;
                    case "354":
                        lahetys = email + "\r\n.\r\n";
                        sw.WriteLine(lahetys);
                        break;

                    default: 
                        Console.WriteLine("Virhe...");
                        lahetys= "QUIT"; // täältä tulee lopetuskuittaus "Postipalvelin sulkeepi yhteyden"
                        sw.WriteLine(lahetys);
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
