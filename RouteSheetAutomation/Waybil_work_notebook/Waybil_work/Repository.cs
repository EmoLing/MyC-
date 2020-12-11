using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waybil_work
{
    public class Repository
    {
        public ObservableCollection<Waybil_Class> waybils { get; set; }

        public Repository()
        {
            waybils = new ObservableCollection<Waybil_Class>();
        }

        public Waybil_Class this[int ID]
        {
            get
            {
                Waybil_Class t = null;
                foreach (var e in this.waybils)
                {
                    if (e.ID == ID) { t = e; break; }
                }
                return t;
            }
        }
    }
}
