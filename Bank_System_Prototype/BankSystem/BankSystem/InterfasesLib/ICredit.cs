using System;
using System.Collections.Generic;
using System.Text;

namespace InterfasesLib
{
    public interface ICredit
    {
        void Credit(decimal sum_credit, int count_month, DateTime oldDate, DateTime currentDate);

        string TestCredit(decimal sum_credit, int count_month, DateTime oldDate, DateTime currentDate,
            out string OstatokPoCredit, out string firstSum);

        void CloseCredit();
    }
}