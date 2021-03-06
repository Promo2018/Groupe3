﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BoVoyages.Model
{
    /**
     * This class provides access to the database. All calls to the database go through this class
     * This class implments the singleton design pattern, providing one instance, for all object wishing to have access to the Database.
     */

    class DBAccess
    {
        private static DBAccess dbAccess = null;
        public static readonly string SELECT_RESULT = "SelectResult";

        private SqlConnection connection = null;

        private DBAccess()
        {
            connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=" + Properties.getInstance().getProperty(Properties.HOST) + ";";
            connection.ConnectionString += @"Initial Catalog=" + Properties.getInstance().getProperty(Properties.DATABASE) + ";Integrated Security=SSPI;";
            open();
            //            Console.WriteLine("Connected to database: " + Properties.getInstance().getProperty(Properties.DATABASE) + ", on host: " + Properties.getInstance().getProperty(Properties.HOST));
        }

        public static DBAccess getInstance()
        {
            if(dbAccess == null)
            {
                dbAccess = new DBAccess();
            }
            return dbAccess;
        }

        public void open()
        {
            try
            {
                connection.Open();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Exeception caught :" + e.Message);
            }

        }

        public DataSet execSelect(string selectString)
        {
//            Console.WriteLine("Performing select :" + selectString);
//            Console.ReadKey();
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = selectString;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds, SELECT_RESULT);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Exeception caught :" + e.Message);
            }
            return ds;
        }

        public int execNonQuery(string nonQueryString)
        {
//            Console.WriteLine("Performing nonQuery :" + nonQueryString);
//            Console.ReadKey();
            int ret = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = nonQueryString;
                ret = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Exeception caught :" + e.Message);
            }
            return ret;
        }

        public int execProcedure(String procedure)
        {
            int ret = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = procedure;
                cmd.CommandType = CommandType.StoredProcedure;
                ret = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Exeception caught :" + e.Message);
            }
            return ret;
        }

        public int execProcedureWithParams(String procedure, String[] parms)
        {
            int lignes = -1;
            if (parms.Length == 0)
            {
                lignes = execProcedure(procedure);
            }
            else
            {
                SqlParameter par;

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = procedure;
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i < parms.Length; i = i + 2)
                    {
                        par = cmd.Parameters.AddWithValue('@' + parms[i], parms[i + 1]);
                    }
                    lignes = cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Exeception caught :" + e.Message);
                }
            }
            return lignes;
        }

        public void close()
        {
            try
            {
                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Exeception caught :" + e.Message);
            }

        }

    }
}
