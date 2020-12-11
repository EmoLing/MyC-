using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Telegram.Bot;
using Newtonsoft.Json;
using System.Threading;
namespace WeatherBotNetCore
{
    class Program
    {
        static void Main(string[] args)
        {

            //string token_bot = "1382932325:AAEaEuiLy5YQ68-89I4iypMt1goFUmW9AmE"; //MyTestWeatherBot_bot

            string token_bot = "1104267969:AAEnpgZAK1GIi4Xq2v1ot7DhfYIrv6JAqR0";//BestWeatherBot
            string token_weather = "0fab7bf16f726fb93b95d671910ebfd2";

            Bot bot = new Bot(token_bot, token_weather);
            int i = 1000;
            bot.StartBot();
            while (true)
            {
                
            }
        }
    }
}
