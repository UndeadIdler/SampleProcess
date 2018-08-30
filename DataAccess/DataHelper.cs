using System;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Configuration;

namespace DataAccess
{
    /// <summary>
    /// DataHelper DataAccess命名空间帮助类。
    /// </summary>
    public class DataHelper
    {

        public static Boolean ParseToBoolean(string s)
        {
            Boolean d=false;
            try
            {
                s = s.Trim().ToLower();
                if (s == "false" || s == "0" || s==string.Empty)
                {
                    d = false;
                }
                else
                {
                    d = true;
                }
            }
            catch
            {
                d = false;
            }
            return d;
        }

        public static DateTime? ParseToDate(string s)
        {
            DateTime? d;
            try
            {
                s = s.Trim();
                if (s.Equals(string.Empty))
                {
                    d = null;
                }
                else
                {
                    d = DateTime.Parse(s);
                }
            }
            catch
            {
                d = null;
            }
            return d;
        }

        public static double ParseToDouble(string s)
        {
            double d = double.NaN;
            try
            {
                s = s.Trim();
                if (s.Equals(string.Empty))
                {

                }
                else
                {
                    d = double.Parse(s);
                }
            }
            catch
            {

            }
            return d;
        }

        public static double? ParseToDoubleEx(string s)
        {
            double? d = null;
            try
            {
                s = s.Trim();
                if (s.Equals(string.Empty))
                {

                }
                else
                {
                    d = double.Parse(s);
                }
            }
            catch
            {
                d = null;
            }
            return d;
        }

        public static int ParseToInt(string s)
        {
            int d =0;
            try
            {
                s = s.Trim();
                if (s.Equals(string.Empty))
                {

                }
                else
                {
                    d = int.Parse(s);
                }
            }
            catch
            {
                d = 0;
            }
            return d;
        }


        public static int? ParseToIntEx(string s)
        {
            int? d = null;
            try
            {
                s = s.Trim();
                if (s.Equals(string.Empty))
                {

                }
                else
                {
                    d = int.Parse(s);
                }
            }
            catch
            {
                d = null;
            }
            return d;
        }

        #region GetConnectString
        public static string GetConnectString(string dbIP, string userID, string pwd, string dbName)
        {
            return string.Format("Data Source = {0}; user id ={1} ;password = {2}; Initial Catalog = {3}", dbIP, userID, pwd, dbName);
        }

        public static string GetConnectString()
        {
            return ConfigurationSettings.AppSettings["ConnectionString"];
        }
        #endregion
    }


    /// <summary>
    /// IFixCacher 固定大小的缓存器，当到达maxSize时，缓存新object，则最先缓存的object会被删除掉。
    /// 支持this索引
    /// 2005.12.04
    /// </summary>
    public interface IFixCacher
    {
        void PutIn(object key, object val);
        object GetValue(object key);
        int Size { get;set;}
    }

    public delegate void CbSimple();
    public delegate void CbSimpleInt(int val);
    public delegate void CbSimpleBool(bool val);
    public delegate void CbSimpleStr(string msg);
    public delegate void CbSimpleObj(object obj);

    public class FixCacher : IFixCacher
    {
        private ArrayList listKey;
        private ArrayList listVal;
        private int maxSize = 0;

        #region FixCacher
        public FixCacher(int size)
        {
            this.maxSize = size;
            this.listKey = new ArrayList(size);
            this.listVal = new ArrayList(size);
        }

        public FixCacher()
        {
            this.maxSize = int.MaxValue;
            this.listKey = new ArrayList();
            this.listVal = new ArrayList();
        }
        #endregion

        public object this[object key]
        {
            get
            {
                return this.GetValue(key);
            }
        }

        #region IFixCacher 成员
        /// <summary>
        /// 将需要缓存的对象放入/压入缓存器中
        /// </summary>		
        public void PutIn(object key, object val)
        {
            if ((key == null) || (val == null))
            {
                return;
            }

            int index = -1;
            object dest = this.GetValue(key, out index);
            if (dest != null)
            {
                this.listKey.RemoveAt(index);
                this.listVal.RemoveAt(index);
            }

            if ((this.listKey.Count == this.maxSize) && (this.listKey.Count > 0))
            {
                this.listKey.RemoveAt(0);
                this.listVal.RemoveAt(0);
            }

            this.listKey.Add(key);
            this.listVal.Add(val);
        }

        private object GetValue(object key, out int index)
        {
            index = -1;
            for (int i = 0; i < this.listKey.Count; i++)
            {
                if (listKey[i].Equals(key))
                {
                    index = i;
                    return this.listVal[i];
                }
            }

            return null;
        }

        public object GetValue(object key)
        {
            int index = -1;

            return this.GetValue(key, out index);
        }

        public int Size
        {
            get
            {
                return this.maxSize;
            }
            set
            {
                this.maxSize = value;
            }
        }

        #endregion
    }


    public class AdvancedFunction
    {
        #region GetType
        // assemblyName 不用带扩展名 ，如果目标类型在当前程序集中，assemblyName传入null	
        public static Type GetType(string typeFullName, string assemblyName)
        {
            if (assemblyName == null)
            {
                return Type.GetType(typeFullName);
            }

            //搜索当前域中已加载的程序集
            Assembly[] asses = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly ass in asses)
            {
                string[] names = ass.FullName.Split(',');
                if (names[0].Trim() == assemblyName.Trim())
                {
                    return ass.GetType(typeFullName);
                }
            }

            //加载目标程序集
            Assembly tarAssem = Assembly.LoadWithPartialName(assemblyName);
            if (tarAssem != null)
            {
                return tarAssem.GetType(typeFullName);
            }

            return null;
        }
        #endregion
    }
}
