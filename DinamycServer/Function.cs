using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DinamycServer
{
    public static class Function //Функции
    {
        public static void SendClientMessage(TcpClient client, string message) //Отравить клиенту сообщение
        {
            try
            {
                client.Client.Send(Encoding.UTF8.GetBytes(message));
            }
            catch
            {
                CheckEmptyClients(client);
                Function.WriteColorText("ERRMESS!\n", ConsoleColor.Red);
            }
        }

        public static void CheckEmptyClients(TcpClient CheckingClient)//Поиск пустых клиентов и их удаление
        {
            if(CheckingClient != null)
            {
                check(CheckingClient);
            }
            else
            {
                Console.WriteLine(Data.TpClient.Count); 
                foreach(var ChClients in Data.TpClient)
                {
                    check(ChClients);
                }                
                WriteColorText($"Произведенна очистка клиетов", ConsoleColor.Yellow);   
                Console.WriteLine(Data.TpClient.Count); 
            }
            void check(TcpClient cl) //Метод по проверке определённого клиента
            {
                try
                {           
                    cl.Client.Send(new byte[1]); //Это работает, незнаю как, главное, что работает!
                }
                catch
                {
                    Data.TpClient.Remove(cl);
                    cl.Close();
                    WriteColorText("Удалён клиент", ConsoleColor.Yellow); 
                }
            }
        }

        public static void WriteColorText(string text, ConsoleColor color) //Отправка цветного сообщения в консоль
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void SendMessage(string nick, string message) //Отправить сообщение в ОБЩИЙ чат
        {
            foreach (var client in Data.TpClient)
            {
                try
                {
                    SendClientMessage(client, $"%MES:{nick}:{message}"); 
                }     
                catch
                {
                    CheckEmptyClients(client);
                }      
                
            }
        }
    
        public static void SendConsoleArgumentList(string[] needArg, string[] inArg) //Отправка в консоль нужных и входящих аргументов
        {
            Function.WriteColorText("[Нужный аргумент] | [Входящий аргумент]", default);
            for(int i = 0; i < needArg.Length; i++)
            {
                if(inArg[i] == null || inArg[i] == "" || inArg[i] == " ") inArg[i] = "пусто";
                
                Function.WriteColorText(needArg[i] + " | " + inArg[i], default);
            }
        }
    }
}