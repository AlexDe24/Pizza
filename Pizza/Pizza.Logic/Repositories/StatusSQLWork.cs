using Pizza.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Logic.Repositories
{
    public class StatusSQLWork
    {
        BaseContext _BaseCt;

        public StatusSQLWork()
        {
            _BaseCt = new BaseContext();
        }

        //
        //Работа с состояниями
        //
        /// <summary>
        /// Чтение списка состояний
        /// </summary>
        public List<Status> ReadStatus()
        {
            List<Status> status = _BaseCt.Statuses.ToList();

            return status;
        }
    }
}
