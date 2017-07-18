﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using QM.Core.Common;
using log4net;
using QM.Core.Exception;
using QM.Core.Model;
using QM.Core.Data;
using System.Net;

namespace QM.Core.QuartzNet
{
    public class QMDllTaskJob : IJob
    {
        private static ILog log = LogManager.GetLogger(typeof(QMDllTaskJob));

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                string taskid = context.JobDetail.Key.Name;
                var taskinfo = QMBaseServer.CreateInstance().GetTask(taskid);
                if (taskinfo == null)
                {
                    log.Error(string.Format("当前任务信息为空,taskid：{0} - {1}", taskid, new QMException()));
                    return;       
                }
                Data.TaskData t = new TaskData();
                t.UpdateLastStartTime(taskid, DateTime.Now);

                TaskLog tlog = new TaskLog();
                SequenceData seq = new SequenceData();
                tlog.idx = seq.GetIdx();
                tlog.taskid = taskid;
                tlog.type = Dns.GetHostName();
                tlog.createtime = DateTime.Now.ToString();
                tlog.message = string.Format("任务ID：{0}，开始运行时间:{1}", taskid, DateTime.Now);
                Data.TaskLogData tl = new TaskLogData();
                tl.Insert(tlog);

                taskinfo.dllTask.TryRun();

                t.UpdateLastEndTime(taskid, DateTime.Now);

                tlog.idx = seq.GetIdx();
                tlog.taskid = taskid;
                tlog.type = Dns.GetHostName();
                tlog.createtime = DateTime.Now.ToString();
                tlog.message = string.Format("任务ID：{0}，结束运行时间:{1}", taskid, DateTime.Now);
                tl.Insert(tlog);
            }
            catch (QMException ex)
            {
                log.Fatal(string.Format("任务回调时发生严重错误，{0}",ex));
            }
        }
    }
}
