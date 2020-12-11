using System;
using System.Collections.Generic;
using System.Text;
using Clients.VIP;

namespace Clients.Interfaces
{
    public interface IEdit<T1, T2, T3>
        where T1 : NaturalClient
        where T2 : LegalEnity
        where T3 : VipClient
    {
        /// <summary>
        /// Редактирование физического лица
        /// </summary>
        /// <param name="item">NaturalClient</param>
        /// <param name="FirstName">Имя</param>
        /// <param name="LastName">Фамилия</param>
        /// <param name="Age">Возраст</param>
        void Edit(T1 item, string FirstName, string LastName, int Age);
        /// <summary>
        /// Редактирование юридического лица
        /// </summary>
        /// <param name="item">LegalEnity</param>
        /// <param name="Name">Название</param>
        /// <param name="Age">Возраст</param>
        void Edit(T2 item, string Name, int Age);

        /// <summary>
        /// Редактирование вип лица(физическое лицо)
        /// </summary>
        /// <param name="item">VipClient</param>
        /// <param name="FirstName">Имя</param>
        /// <param name="LastName">Фамилия</param>
        /// <param name="Age">Возраст</param>
        void Edit(T3 item, string FirstName, string LastName, int Age);

        /// <summary>
        /// Редактирование вип лица(юридическое лицо)
        /// </summary>
        /// <param name="item">VipClient</param>
        /// <param name="Name">Название</param>
        /// <param name="Age">Возраст</param>
        void Edit(T3 item, string Name, int Age);
    }
}