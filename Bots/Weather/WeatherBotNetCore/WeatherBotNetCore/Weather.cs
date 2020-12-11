using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherBotNetCore
{
    struct Weather
    {
        #region Поля
        string city;
        string token;
        string url;
        #endregion

        #region Конструктор

        public Weather(string City, string Token, string Url)
        {
            this.city = City;
            this.token = Token;
            this.url = Url;
        }

        #endregion

        #region Свойства

        public string City { get { return this.city; } set { city = value; } }

        public string Token { get { return this.token; } set { token = value; } }

        public string Url { get { return this.url; } set { url = value; } }
        #endregion

        #region Методы

        #endregion
    }
}
