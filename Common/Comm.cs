using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ESBasic.Logger;
using DataAccess;

namespace DAl
{
    public enum DataBaseType
    {
        AccessClient,
        OracleClient,
        SqlClient
    }

    public enum BaseRecordType
    {
        Division = 0
    }
    public class Config
    {
        public static DataBaseType dataBaseType = DataBaseType.SqlClient;
        public static string constr = ConfigurationSettings.AppSettings["ConnectionString"];
    }

    public class DataBase : DBOperatorBase
    {
        public DataBase()
        {

        }

        public override IDBTypeElementFactory GetDBTypeElementFactory()
        {
            if (DataBaseType.SqlClient == Config.dataBaseType)
            {
                return new SqlDBTypeElementFactory();
            }
            else if (DataBaseType.OracleClient == Config.dataBaseType)
            {
                return new OracleDBTypeElementFactory();
            }

            return new OleDBTypeElementFactory();
        }
    }

    public class Comm
    {
        public static IEsbLogger EsbLogger = new EmptyEsbLogger();

        public static DataTable GetDataTableFromIDataReader(IDataReader reader)
        {
            DataTable dt = new DataTable();
            bool init = false;
            dt.BeginLoadData();
            object[] vals = new object[0];
            while (reader.Read())
            {
                if (!init)
                {
                    init = true;
                    int fieldCount = reader.FieldCount;
                    for (int i = 0; i < fieldCount; i++)
                    {
                        dt.Columns.Add(reader.GetName(i), typeof(String));
                    }
                    vals = new object[fieldCount];
                }
                reader.GetValues(vals);
                dt.LoadDataRow(vals, true);
            }
            reader.Close();
            dt.EndLoadData();
            return dt;
        }
    }
}
