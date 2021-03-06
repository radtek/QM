﻿using System;
using System.Collections.Generic;
using System.Data;
using QM.Core.Data;
using QM.Core.Model;
using QM.Core.Excel;
using QM.Core.Log;
using QM.Core.Exception;

namespace QM.Excel
{
    public class Class1 : DllTask
    {
        private static ILogger log;
        private static string dbcon = "Provider=MSDAORA.1;Data Source=WHDB;Password=wh123;User ID=whfront";
        private static string error;
        private QMDBHelper db = new QMDBHelper(dbcon);
        public override void Run()
        {
            DataSet ds = db.ExecuteDataset("select * from AF_LOGIN_HIS",null);
            string title = "测试标题";
            string subject = @"E:\ASECode\Test\QM.git\QM.Excel\bin\Debug\1.xls";
            QMExcel ex = new QMExcel(title,subject);
            if (ex.Export(ds.Tables[0],title,subject, out error) == false)
            {
                throw new QMException(error);
            }
        }

        public override void Dispose()
        {
            db.Disponse();
            base.Dispose();
        }
    }
}
