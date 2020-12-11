using System;
using System.Collections.Generic;
using System.Text;

namespace InterfasesLib
{
    public interface IContribution
    {
        /// <summary>
        /// Вклад
        /// </summary>
        /// <param name="Capitalization">С капитализацией или без</param>
        /// <param name="currentDateTime">Текущая дата</param>
        /// <param name="oldDateTime">Дата оформления</param>
        /// <param name="sum">сумма</param>
        void Contribution(bool Capitalization, DateTime currentDateTime, DateTime oldDateTime, decimal sum);

        /// <summary>
        /// Функция предварительного расчета вклада
        /// </summary>
        /// <param name="Capitalization">С капитализацией или без</param>
        /// <param name="currentDateTime">Текущая дата</param>
        /// <param name="oldDateTime">Дата оформления</param>
        /// <param name="sum">сумма</param>
        /// <returns></returns>
        string Test_Contribution(bool Capitalization, DateTime currentDateTime, DateTime oldDateTime, decimal sum);
        /// <summary>
        /// Закрытие вклада
        /// </summary>
        void CloseContribution();
    }
}