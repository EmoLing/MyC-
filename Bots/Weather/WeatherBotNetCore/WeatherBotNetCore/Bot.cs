using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Http;
using Telegram.Bot;
using Newtonsoft.Json;
using System.Threading;
using Telegram.Bot.Args;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace WeatherBotNetCore
{
    struct Bot
    {
        #region Поля

        TelegramBotClient bot;
        string token;
        string token_weather;
        string city;
        #endregion

        #region Конструктор

        public Bot(string token_bot, string TokenWeather)
        {
            this.city = "Moscow";
            this.token_weather = TokenWeather;
            this.token = token_bot;
            bot = new TelegramBotClient(this.token);
        }

        private string Input_City(MessageEventArgs e)
        {
            string my_city;

            my_city = e.Message.Text.ToString();

            if (my_city == string.Empty)
                my_city = "Moscow";

            return my_city;
        }

        #endregion

        #region Методы
        public void StartBot()
        {
            InicializationBot();
        }
        private void InicializationBot()
        {
            bot.OnMessage += MessageListener;
            bot.StartReceiving();
        }

        private async void MessageListener(object sender, MessageEventArgs e)
        {
            Dictionary<string, string> City = new Dictionary<string, string>();

            if (File.Exists($"{e.Message.Chat.Id.ToString()}.json"))
            {
                string json = File.ReadAllText($"{e.Message.Chat.Id.ToString()}.json");

                City = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }

            string text = $"{DateTime.Now.ToLongTimeString()}: {e.Message.Chat.Id} {e.Message.Text}";

            Console.WriteLine($"{text} TypeMessage: {e.Message.Text.ToString()}");
            if (City.ContainsKey(e.Message.Chat.Id.ToString()))
            {
                this.city = City[e.Message.Chat.Id.ToString()];
            }
            else city = "Moscow";
            if (e.Message.Text.ToString() != null)
            {
                string message = e.Message.Text.ToString();
                if (ScanCity(message))
                {
                    city = message;
                    GetCity(sender, e, City);
                    goto EXIT;
                }

                else
                {
                    if (City.ContainsKey(e.Message.Chat.Id.ToString()))
                        city = City[e.Message.Chat.Id.ToString()];
                    else city = "Moscow";

                }
                switch (message)
                {
                    case "/start":
                        {
                            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Привет, {e.Message.Chat.FirstName} :)");
                            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Я буду спамить тебе погодой каждый час :*");
                            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Напиши /weather, чтобы получать погоду сейчас :)");
                            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Напиши /foreverweather, чтобы получать погоду каждый час :)");
                            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"/help - для повтора инструкций :)");
                            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Напиши мне свой город (НА РУССКОМ) и я его запомню :)");
                            break;
                        }
                    case "/weather":
                        {
                            string url = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&units=metric&appid=" + token_weather;

                            Weather MyWeather = new Weather(city, token_weather, url);

                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);//Запрос

                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();//Ответ

                            string stream_response;
                            using (StreamReader reader = new StreamReader(response.GetResponseStream()))//обработка ответа
                            {
                                stream_response = reader.ReadToEnd();
                            }


                            var name = JObject.Parse(stream_response)["name"].ToString();

                            double temp = double.Parse(JObject.Parse(stream_response)["main"]["temp"].ToString());
                            double lon = double.Parse(JObject.Parse(stream_response)["coord"]["lon"].ToString());//lng
                            double lat = double.Parse(JObject.Parse(stream_response)["coord"]["lat"].ToString());
                            string main_weather = string.Empty;
                            var weather = JObject.Parse(stream_response)["weather"].ToArray();
                            foreach (var item in weather)
                            {
                                main_weather = ((item["main"]).ToString());
                            }

                            //Получение времени
                            string token_time = "30K6JWL8J0CR";
                            string url_time = $"http://api.timezonedb.com/v2.1/get-time-zone?key="+token_time+"&format=json&by=position&lat="+lat+"&lng="+lon;
                            HttpWebRequest time_zone_Request = (HttpWebRequest)WebRequest.Create(url_time);
                            HttpWebResponse time_zone_Response = (HttpWebResponse)time_zone_Request.GetResponse();
                            string stream_response_time;
                            using (StreamReader reader = new StreamReader(time_zone_Response.GetResponseStream()))//обработка ответа
                            {
                                stream_response_time = reader.ReadToEnd();
                            }
                            DateTime user_response_time = DateTime.Parse(JObject.Parse(stream_response_time)["formatted"].ToString());
                            string abbreviation = JObject.Parse(stream_response_time)["abbreviation"].ToString();

                            string gif_weather;

                            if (temp > 30)
                                gif_weather = "hot";
                            else if (temp < 0)
                                gif_weather = "cold";
                            else gif_weather = main_weather;

                            //Получение gif
                            string gif_token = "D5zCmfW60YVy1jF2i8S6VeTARdxAIVLB";
                            string gif_url = "https://api.giphy.com/v1/gifs/random?api_key=" + gif_token+ "&tag=" + gif_weather + "&rating=g";
                            HttpWebRequest gifRequest = (HttpWebRequest)WebRequest.Create(gif_url);
                            HttpWebResponse gifResponse = (HttpWebResponse)gifRequest.GetResponse();
                            string stream_gif_response;
                            using (StreamReader reader = new StreamReader (gifResponse.GetResponseStream()))
                            {
                                stream_gif_response = reader.ReadToEnd();
                            }
                            string gif = JObject.Parse(stream_gif_response)["data"]["image_url"].ToString();

                            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Сейчас: {user_response_time} (временной пояс - {abbreviation})");
                            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Город: {name}");
                            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Температура: {temp:0.0}°C");
                            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Атмосферное состояние: {WeatherTranslate(main_weather)}");
                            await bot.SendDocumentAsync(e.Message.Chat.Id, gif);

                            break;
                        }
                    case "Привет":
                        {
                            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Привет, {e.Message.Chat.FirstName} :)");
                            break;
                        }

                    case "/foreverweather":
                        {
                            GetWeather(sender, e);

                            break;
                        }
                    case "/help":
                        {
                            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"/weather - погода сейчас :)");
                            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"/foreverweather - получение погоды каждый час :)");
                            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"/help - для повтора инструкций :)");
                            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Название города (НА РУССКОМ) - запоминание его в памяти :)");
                            break;
                        }

                    default:
                        {
                            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Неизвестная команда");
                            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"/weather - погода сейчас :)");
                            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"/foreverweather - получение погоды каждый час :)");
                            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"/help - для повтора инструкций :)");
                            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Название города(НА РУССКОМ) - запоминание его в памяти :)");
                            break;
                        }
                }
            EXIT:;
                Thread.Sleep(1000);
            }

        }


        private async void GetWeather(object sender, MessageEventArgs e)
        {
            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Начинаю спам погодой раз в час");
            while (true)
            {
                Dictionary<string, string> City = new Dictionary<string, string>();

                if (File.Exists($"{e.Message.Chat.Id.ToString()}.json"))
                {
                    string json = File.ReadAllText($"{e.Message.Chat.Id.ToString()}.json");

                    City = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                }


                if (City.ContainsKey(e.Message.Chat.Id.ToString()))
                {
                    this.city = City[e.Message.Chat.Id.ToString()];
                }
                else city = "Moscow";

                string url = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&units=metric&appid=" + token_weather;

                Weather MyWeather = new Weather(city, token_weather, url);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);//Запрос

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();//Ответ

                string stream_response;
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))//обработка ответа
                {
                    stream_response = reader.ReadToEnd();
                }

                var name = JObject.Parse(stream_response)["name"].ToString();

                double temp = double.Parse(JObject.Parse(stream_response)["main"]["temp"].ToString());

                double lon = double.Parse(JObject.Parse(stream_response)["coord"]["lon"].ToString());//lng
                double lat = double.Parse(JObject.Parse(stream_response)["coord"]["lat"].ToString());

                string main_weather = string.Empty;
                var weather = JObject.Parse(stream_response)["weather"].ToArray();
                foreach (var item in weather)
                {
                    main_weather = ((item["main"]).ToString());
                }

                //Получение времени
                string token_time = "30K6JWL8J0CR";
                string url_time = $"http://api.timezonedb.com/v2.1/get-time-zone?key=" + token_time + "&format=json&by=position&lat=" + lat + "&lng=" + lon;
                HttpWebRequest time_zone_Request = (HttpWebRequest)WebRequest.Create(url_time);
                HttpWebResponse time_zone_Response = (HttpWebResponse)time_zone_Request.GetResponse();
                string stream_response_time;
                using (StreamReader reader = new StreamReader(time_zone_Response.GetResponseStream()))//обработка ответа
                {
                    stream_response_time = reader.ReadToEnd();
                }
                DateTime user_response_time = DateTime.Parse(JObject.Parse(stream_response_time)["formatted"].ToString());
                string abbreviation = JObject.Parse(stream_response_time)["abbreviation"].ToString();

                //Получение gif

                string gif_weather;

                if (temp > 30)
                    gif_weather = "hot";
                else if (temp < 0)
                    gif_weather = "cold";
                else gif_weather = main_weather;

                string gif_token = "D5zCmfW60YVy1jF2i8S6VeTARdxAIVLB";
                string gif_url = "https://api.giphy.com/v1/gifs/random?api_key=" + gif_token + "&tag=" + gif_weather + "&rating=g";
                HttpWebRequest gifRequest = (HttpWebRequest)WebRequest.Create(gif_url);
                HttpWebResponse gifResponse = (HttpWebResponse)gifRequest.GetResponse();
                string stream_gif_response;
                using (StreamReader reader = new StreamReader(gifResponse.GetResponseStream()))
                {
                    stream_gif_response = reader.ReadToEnd();
                }

                string gif = JObject.Parse(stream_gif_response)["data"]["image_url"].ToString();
                await bot.SendTextMessageAsync(e.Message.Chat.Id, $"{e.Message.Chat.FirstName}, Прогноз погоды ждет тебя!:)");
                await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Сейчас: {user_response_time} (временной пояс - {abbreviation})");
                await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Город: {name}");
                await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Температура: {temp:0.0}°C");
                await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Атмосферное состояние: {WeatherTranslate(main_weather)}");
                await bot.SendDocumentAsync(e.Message.Chat.Id, gif);
                Thread.Sleep(3_600_000);
                //Thread.Sleep(6_000);
            }
        }

        private static string WeatherTranslate(string description)
        {
            Dictionary<string, string> weather_translate = new Dictionary<string, string>();
            weather_translate.Add("Clear", "Ясно");
            weather_translate.Add("Clouds", "Облачно");
            weather_translate.Add("Drizzle", "Морось");
            weather_translate.Add("Rain", "Дождь");
            weather_translate.Add("Thunderstorm", "Гроза");
            weather_translate.Add("Snow", "Снег");
            weather_translate.Add("Mist", "Туман");
            weather_translate.Add("Smoke", "Смок");
            weather_translate.Add("Haze", "Мгла");
            weather_translate.Add("Dust", "Пыль");
            weather_translate.Add("Fog", "Туман");
            weather_translate.Add("Sand", "Песок");
            weather_translate.Add("Ash", "Пепел");
            weather_translate.Add("Squall", "Шквальный ветер");
            weather_translate.Add("Tornado", "Торнадо");

            string ru_description = description;
            if (weather_translate.ContainsKey(description))
            {
                weather_translate.TryGetValue(description, out ru_description);
            }
            return ru_description;
        }

        private async void GetCity(object sender, MessageEventArgs e, Dictionary<string, string> City)
        {
            var my_town = e.Message.Text.ToString();
            if (!City.ContainsKey(e.Message.Chat.Id.ToString()))
            {
                City.Add(e.Message.Chat.Id.ToString(), e.Message.Text.ToString());
                string json = JsonConvert.SerializeObject(City);
                File.WriteAllText($"{e.Message.Chat.Id.ToString()}.json", json);
            }
            else
            {

                City[e.Message.Chat.Id.ToString()] = my_town;

                /*string json1 = JsonConvert.SerializeObject(City, Formatting.Indented);

                using (StreamWriter streamWriter = File.AppendText($"{e.Message.Chat.Id.ToString()}.json"),)
                {
                    await streamWriter.WriteLineAsync(json1);
                }*/
                string json = JsonConvert.SerializeObject(City);
                File.WriteAllText($"{e.Message.Chat.Id.ToString()}.json", json);
            }
            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Я запомнил твой город");
            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Можешь в этом убедиться /weather");
        }

        private bool ScanCity(string message)
        {
            bool check = false;
            string cities;
            using (StreamReader reader = new StreamReader("cities/cities.json"))
            {
                cities = reader.ReadToEnd();
            }


            var name = JObject.Parse(cities)["city"].ToArray();
            List<string> main_city = new List<string>();
            int j = 0;
            foreach (var item in name)
            {
                main_city.Add((item["name"]).ToString());

                j++;
            }
            for (int i = 0; i < main_city.Count; i++)
            {
                if (message == main_city[i].ToString())
                {
                    check = true;
                    break;

                }
            }
            return check;
        }
        #endregion
    }
}

