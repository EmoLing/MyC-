using System;

namespace Clients.VIP
{
    public class VipNaturalClient : VipClient
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        public VipNaturalClient(string firstName, string lastName, string department, DateTime Birthday, decimal amountOfMoney)
            : base(department, Birthday, amountOfMoney)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public VipNaturalClient(int ID,string firstName, string lastName, string department, DateTime Birthday, decimal amountOfMoney)
            : base(ID,department, Birthday, amountOfMoney)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}