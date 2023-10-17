using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AplicaciondeSocketCsharp
{
    class Program
    {
        static void Main(string[] args) 
        {
            server();
        }

        public static void server() 
        {
            //configurar el servidor
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11200);

            try
            {
                //creacion del servidor
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                //unir el end point al socket
                listener.Bind(localEndPoint);
                //cantidad de conexiones que recibe
                listener.Listen(10);
                //recibe la conexion y se la entrega al socket cliente
                Console.WriteLine("esperando conexion");

                Socket handler = listener.Accept();
                string data = null;
                byte[] bytes = null;
                while(true) 
                { 
                    bytes = new byte [1024];
                    //recibe datos del cliente 
                    int byteRec = handler.Receive(bytes);
                    //Convertir datos bytes a string
                    data += Encoding.ASCII.GetString(bytes, 0, byteRec);
                    // verifica cuando el cliente dejo de enviar datos
                    if (data.IndexOf("<EOF>") > -1)
                        break;
                }
                Console.WriteLine("Texto del cliente:" + data);
            }
            catch(Exception e) 
            { Console.WriteLine(e.ToString()); }
        }
    }
}