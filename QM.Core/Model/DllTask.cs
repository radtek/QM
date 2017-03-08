﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;

namespace QM.Core.Model
{
    /// <summary>
    /// DLL任务
    /// </summary>
    public abstract class DllTask : MarshalByRefObject, IDisposable
    {
        public DllTask()
        { }

        public void TryRun()
        {
            try
            {
                Run();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        /// <summary>
        /// 与第三方约定的运行接口
        /// </summary>
        public abstract void Run();

        /// <summary>
        /// 系统资源释放
        /// </summary>
        public void Dispose()
        {
            Dispose();
        }
    }
}