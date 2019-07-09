using Project.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.SingletonPattern
{
    public class DBTool : DbContext
    {
        private DBTool() { }

        private static MyContext _dbInstance;

        public static MyContext DBInstance
        {
            get
            {
                if (_dbInstance == null)
                {
                    _dbInstance = new MyContext();
                }
                return _dbInstance;
            }
        }
    }
}
