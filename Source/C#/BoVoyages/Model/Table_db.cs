using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BoVoyages.Model
{
    public abstract class Table_db
    {
        public void startTransaction()
        {
            DBAccess.getInstance().execNonQuery("begin transaction;");
        }

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
