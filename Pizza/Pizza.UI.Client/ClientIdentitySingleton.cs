using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.UI.Client
{
    internal class ClientIdentitySingleton
    {
        #region Instance
        private static ClientIdentitySingleton _instance;
        public static ClientIdentitySingleton Instance
        {
            get
            {
                if (_instance == null) _instance = new ClientIdentitySingleton();

                return _instance;
            }
        }
        #endregion

        #region Properties

        public Logic.DTO.Client CurrentClient { get; set; }

        #endregion

    }
}
