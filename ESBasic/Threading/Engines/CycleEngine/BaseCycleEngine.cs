using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace ESBasic.Threading.Engines
{
    /// <summary>
    /// BaseCycleEngine ICycleEngine的抽象实现，循环引擎直接继承它并实现DoDetect方法即可。
    /// zhuweisky 2006.12.21
    /// </summary>
    public abstract class BaseCycleEngine : ICycleEngine
    {
        private const int SleepTime = 1000;//ms 
        private volatile bool isStop = true;
        private ManualResetEvent manualResetEvent4Stop = new ManualResetEvent(false);
        private int totalSleepCount = 0;
        public event CbException OnEngineStopped;

        #region abstract
        /// <summary>
        /// DoDetect 每次循环时，引擎需要执行的核心动作。
        /// (1)该方法不允许抛出异常。
        /// (2)该方法中不允许调用BaseCycleEngine.Stop()方法，否则会导致死锁。
        /// </summary>
        /// <returns>返回值如果为false，表示退出引擎循环线程</returns>
        protected abstract bool DoDetect();
        #endregion

        #region Property
        #region DetectSpanInSecs
        private int detectSpanInSecs = 0;
        public int DetectSpanInSecs
        {
            get { return detectSpanInSecs; }
            set { detectSpanInSecs = value; }
        }
        #endregion    

        #region IsRunning
        public bool IsRunning
        {
            get
            {
                return !this.isStop;
            }
        }
        #endregion
        #endregion

        public BaseCycleEngine()
        {
            this.OnEngineStopped += delegate { };
        }

        #region ICycleEngine 成员

        #region Start
        public virtual void Start()
        {
            this.totalSleepCount = this.detectSpanInSecs * 1000 / BaseCycleEngine.SleepTime;

            if (this.detectSpanInSecs < 0)
            {
                return;
            }

            if (!this.isStop)
            {
                return;
            }

            this.manualResetEvent4Stop.Reset();
            this.isStop = false;
            ESBasic.CbSimple cb = new CbSimple(this.Worker);
            cb.BeginInvoke(null, null);
        }
        #endregion

        #region Stop
        public virtual void Stop()
        {
            if (this.isStop)
            {
                return;
            }

            this.isStop = true;
            this.manualResetEvent4Stop.WaitOne();
            this.manualResetEvent4Stop.Reset();
        }
        #endregion

        #region Worker
        protected virtual void Worker()
        {
            Exception exception = null;
            try
            {
                while (!this.isStop)
                {
                    #region Sleep
                    for (int i = 0; i < this.totalSleepCount; i++)
                    {
                        if (this.isStop)
                        {
                            break;
                        }
                        Thread.Sleep(BaseCycleEngine.SleepTime);
                    }
                    #endregion

                    if (!this.DoDetect())
                    {
                        this.isStop = true;
                        break;
                    }
                }

                this.manualResetEvent4Stop.Set();
            }
            catch(Exception ee)
            {
                exception  = ee ;
                throw;
            }
            finally
            {
                this.OnEngineStopped(exception);
            }
        }
        #endregion
        #endregion
    }
}
