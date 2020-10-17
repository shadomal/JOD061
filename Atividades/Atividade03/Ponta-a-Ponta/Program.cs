using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace JOD061
{
    class Program
    {
        const int porta_ = 7000;
        const string ip = "10.0.0.216";
        static void Main(string[] args)
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
                UdpClient meupar = new UdpClient(porta_);

                Console.WriteLine("Para sair, pressione - CTRL + C");
                while (true)
                {
                    byte[] bytesRecebidos = meupar.Receive(ref endPoint);
                    Console.WriteLine("Mensagem recebida: {0}", Encoding.ASCII.GetString(bytesRecebidos));
                    Console.WriteLine("Mensagem enviada por {0}:{1}",endPoint.Address.ToString(), endPoint.Port.ToString());
                    
                }
            }
            catch (Exception)
            {
                UdpClient par = new UdpClient();
                par.EnableBroadcast = true;

                Console.WriteLine("Por favor envie sua mensagem. Para sair, Pressione ENTER");
                Console.WriteLine("> ");
                string msg = Console.ReadLine();

                while (msg.Length > 0)
                {
                    byte[] bytesEnviados = Encoding.ASCII.GetBytes(msg);
                    par.Send(bytesEnviados, bytesEnviados.Length, ip, porta_);

                    Console.Write("> ");
                    msg = Console.ReadLine();

                }
                par.Close();

            }
        }
    }
}
