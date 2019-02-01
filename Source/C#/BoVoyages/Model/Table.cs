using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyages.Model
{
    public abstract class Table
    {
        public Table_db table_db;

        public Table() { }

        public abstract void startTransaction();

        public abstract int endTransaction(bool commit);

    }
}
