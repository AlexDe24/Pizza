using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.UI.Client
{
    public class ComboBoxesDatesList
    {
        private List<int> _days = new List<int>();
        public List<int> Days
        {
            get
            {
                for (int i = 1; i <= 31; i++)
                {
                    _days.Add(i);
                }

                return _days;
            }
        }

        private List<int> _month = new List<int>();
        public List<int> Months
        {
            get
            {
                for (int i = 1; i <= 12; i++)
                {
                    _month.Add(i);
                }
                return _month;
            }
        }

        private List<int> _year = new List<int>();
        public List<int> Years
        {
            get
            {
                for (int i = 1900; i <= 2017; i++)
                {
                    _year.Add(i);
                }

                return _year;
            }
        }
    }
}
