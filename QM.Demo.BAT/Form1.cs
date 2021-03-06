﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;

namespace QM.BAT
{
    public partial class Form1 : Form
    {
        private static ILog log = LogManager.GetLogger(typeof(Form1));
        public Form1(string[] parms)
        {
            InitializeComponent();

            if (parms.Length > 0)
            {
                switch (parms[0])
                {
                    case "asy":
                        log.Debug("parms:asy");                        
                        test();
                        log.Debug("exit");
                        Environment.Exit(Environment.ExitCode);                        
                        break;
                }
            }
        }

        private void test()
        {
            string[] dirs = Directory.GetFiles(@"c:\qm\file\", "*.txt");

            if (dirs.Count() > 0)
            {
                log.Debug(dirs.Count());

                foreach (string var in dirs)
                {
                    try
                    {
                        File.Delete(var);
                        log.Debug(var);
                    }
                    catch (Exception ex)
                    {
                        log.Fatal(ex.Message);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            test();
        }
    }
}
