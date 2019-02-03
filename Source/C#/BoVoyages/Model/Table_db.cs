using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BoVoyages.Model
{
    /**
     * Base class for all Database classes.
     * Provides generic methods to start and end transactions and to recuperate a database created identity id after an insertion.
     */

    public abstract class Table_db
    {
        // Starts a transaction
        public void startTransaction()
        {
            DBAccess.getInstance().execNonQuery("begin transaction;");
        }

        // Ends a transaction. Commit if true otherwise rollback.
        public int endTransaction(bool commit)
        {
            int rc = 0;
            if(commit)
            {
                rc = DBAccess.getInstance().execNonQuery("commit;");
            } else
            {
                rc = DBAccess.getInstance().execNonQuery("rollback;");
            }
            return rc;
        }

        // Get the id of the last insert into a table with an identity primary key.
        public int getLastIdentityId()
        {
            int id = -1;
            DataSet ds = DBAccess.getInstance().execSelect("SELECT SCOPE_IDENTITY() as id;");
            foreach (DataRow row in ds.Tables[DBAccess.SELECT_RESULT].Rows)
            {
                id = int.Parse(row["id"].ToString());
            }

            return id;
        }

    }
}
