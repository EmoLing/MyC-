﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIbot
{
    struct MessageLog
    {
        public string Time { get; set; }

        public long Id { get; set; }

        public string TypeMsg { get; set; }

        public string Msg { get; set; }

        public string FirstName { get; set; }

        public MessageLog(string Time, string TypeMsg, string Msg, string FirstName, long Id)
        {
            this.Time = Time;
            this.TypeMsg = TypeMsg;
            this.Msg = Msg;
            this.FirstName = FirstName;
            this.Id = Id;
        }
    }
}
