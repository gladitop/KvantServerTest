using System;
using System.Net.Sockets;

namespace DinamycServer
{
    public partial class Commands
    {
        public static string[] argLOG = {"email","password"} ; // строка-подсказка с необходимыми аргументами
        private void LOG(TcpClient client, string[] argumets) // %LOG:email:pass
        {

            string email = argumets[0];
            string password = argumets[1];

            try{
                

                if (Database.CheckEmail(email))
                {
                    if (Database.CheckPassword(email, password))
                    {
                        var info = Database.GetClientInfo(email);
                        string point = "";
                        if(info.Point == null) { point = "null"; }
                        else{ point = Convert.ToString(info.Point); } 
                        
                        Function.SendClientMessage(client, $"%LOGOOD:{info.Email}:{info.ID}:{info.Nick}:{point}");
                    }
                    else
                    {
                        Function.SendClientMessage(client, "%BLOG");
                    }
                }
                else
                {
                    Function.SendClientMessage(client, "%BLOG");
                }
            }
            catch (Exception e)
            {
                Function.WriteColorText($"LOG:{e.Message}", ConsoleColor.Red);               
            }
        }
    }
}