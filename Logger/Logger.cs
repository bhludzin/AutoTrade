using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Logger
{

    public sealed class Logger
    {
        //private static volatile Logger instance;
        //private static object syncRoot = new Object();

        //public static Logger Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            lock (syncRoot)
        //            {
        //                if (instance == null)
        //                    instance = new Logger();
        //            }
        //        }
        //        return instance;
        //    }
        //}

        private Logger()
        {
        }


        public static void LogException(Exception e)
        {
            try
            {
                SqlConnection m_connDB = new SqlConnection(ConfigurationManager.AppSettings["DBConnection"]);
                m_connDB.Open();

                SqlCommand myCommand = new SqlCommand("C_LOG_ENTRY_INS", m_connDB);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add(new SqlParameter("@sDescription", "Error: " + e.Source + e.Message + e.StackTrace));
                myCommand.ExecuteNonQuery();

                m_connDB.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        public static void LogEvent(string sDescription)
        {
            try
            {
                SqlConnection m_connDB = new SqlConnection(ConfigurationManager.AppSettings["DBConnection"]);
                m_connDB.Open();

                SqlCommand myCommand = new SqlCommand("C_LOG_ENTRY_INS", m_connDB);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add(new SqlParameter("@sDescription", sDescription));
                myCommand.ExecuteNonQuery();

                m_connDB.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

    }
}
