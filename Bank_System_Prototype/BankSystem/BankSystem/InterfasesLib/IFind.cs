using System;
using System.Collections.Generic;
using System.Text;

namespace InterfasesLib
{
    public interface IFind
    {
        /// <summary>
        /// Поиск по имени и фамилии
        /// </summary>
        /// <typeparam name="T">Возращается выбранный клиент</typeparam>
        /// <param name="FirstName">Имя</param>
        /// <param name="LastName">Фамилия</param>
        /// <param name="Department">Отдел</param>
        /// <returns></returns>
        object Find<T>(string FirstName, string LastName, string Department);

        /// <summary>
        /// Поиск по Названию
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Name"></param>
        /// <param name="Department"></param>
        /// <returns></returns>
        object Find<T>(string Name, string Department);

        /// <summary>
        /// Поиск по номеру счета
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="AccountNumber"></param>
        /// <param name="Department"></param>
        /// <returns></returns>
        object Find<T>(int AccountNumber, string Department);
    }
}