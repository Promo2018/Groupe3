using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyages.Model
{
    /**
     * Base class for all Data structure classes.
     * Provides abstract mathos for start and end transaction.
     */

    public abstract class Table
    {
        public Table() { }

        public abstract void startTransaction();

        public abstract int endTransaction(bool commit);

    }
}
