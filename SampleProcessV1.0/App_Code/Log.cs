using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;

using System.IO;
using System.Runtime.Remoting.Messaging;

using WebApp.Components;
using System.Configuration;
using System.Collections;



namespace Log
{
    public partial class log
    {
        public void Log(string s)
        {
            WriteLog(s, true);
        }
        private static object Locked = new object();
        private static Dictionary<long, long> lockDic = new Dictionary<long, long>();
        public static string directory = ConfigurationManager.AppSettings["Logdirectory"].ToString();
        public static bool WriteLog(string content, bool append)
        {
            lock (Locked)
            {
                try
                {
                    string name = DateTime.Now.ToString("yyyy-MM-dd") + "log.txt";
                    string filename = directory + "\\" + name;
                    if (!File.Exists(filename))
                    {
                        using (System.IO.FileStream fs = System.IO.File.Create(filename))
                        {
                            fs.Close();
                        }

                        // File.Create(filename);//创建该文件
                    }

                    Write(content, System.Environment.NewLine, filename);

                    return true;

                }
                catch
                {
                    return false;
                }
            }
        }
        private static void Write(string content, string newLine, string _fileName)
        {
            if (string.IsNullOrEmpty(_fileName))
            {
                throw new Exception("FileName不能为空！");
            }
            using (System.IO.FileStream fs = new System.IO.FileStream(_fileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite, 8, System.IO.FileOptions.Asynchronous))
            {
                // Byte[] dataArray = System.Text.Encoding.Unicode.GetBytes(System.DateTime.Now.ToString() + content + "\r\n");
                Byte[] dataArray = System.Text.Encoding.UTF8.GetBytes(content + newLine);

                bool flag = true;
                long slen = dataArray.Length;
                long len = 0;
                while (flag)
                {
                    try
                    {
                        if (len >= fs.Length)
                        {
                            fs.Lock(len, slen);
                            lockDic[len] = slen;
                            flag = false;
                        }
                        else
                        {
                            len = fs.Length;
                        }
                    }
                    catch (Exception ex)
                    {
                        while (!lockDic.ContainsKey(len))
                        {
                            len += lockDic[len];
                        }
                    }
                }
                fs.Seek(len, System.IO.SeekOrigin.Begin);
                fs.Write(dataArray, 0, dataArray.Length);
                fs.Close();
            }
        }



    }
}


