using Pizza.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Logic.Repositories
{
    public class StatusSQLWork : IDisposable
    {
        BaseContext _BaseCt;

        public StatusSQLWork()
        {
            _BaseCt = new BaseContext();
        }

        public void Dispose()
        {
            _BaseCt.Dispose();
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

        /// <summary>
        /// Чтение списка состояний асинхронно
        /// </summary>
        public async Task<List<Status>> GetStatusesAsync()
        {
            return await _BaseCt.Statuses.ToListAsync().ConfigureAwait(false);
        }
    }
}
