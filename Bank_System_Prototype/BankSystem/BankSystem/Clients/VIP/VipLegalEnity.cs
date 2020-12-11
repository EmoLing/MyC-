using System;

namespace Clients.VIP
{
    public class VipLegalEnity : VipClient
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }


        public VipLegalEnity(string name, string department, DateTime Birthday, decimal amountOfMoney)
            : base(department, Birthday, amountOfMoney)
        {
            this.Name = name;
        }

        public VipLegalEnity(int ID,string name, string department, DateTime Birthday, decimal amountOfMoney)
            : base(ID,department, Birthday, amountOfMoney)
        {
            this.Name = name;
        }
    }
}