﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Mime;
using System.Net.Mail;

namespace QM.Core.Mail
{
    /// <summary>
    /// 邮件发送类
    /// </summary>
    public class QMMail : IMail
    {
        private string _subject;
        private string _body;
        private string _from = "AllianceWH@xxx.COM";
        private string _fromName;
        private string _recipientName;
        private string _mailDomain = "10.68.10.8";
        private int _mailserverport = 25;
        private string _username;
        private string _password;
        private bool _html;
        private string _recipient;
        private string _recipientCC;
        private string _recipientBCC;
        private string _priority;
        private MailMessage myEmail = new MailMessage();

        public QMMail()
        { }

        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject
        {
            get
            {
                return this._subject;
            }
            set
            {
                this._subject = value;
            }
        }

        /// <summary>
        /// 邮件正文
        /// </summary>
        public string Body
        {
            get
            {
                return this._body;
            }
            set
            {
                this._body = value;
            }
        }


        /// <summary>
        /// 发件人地址
        /// </summary>
        public string From
        {
            get
            {
                return _from;
            }
            set
            {
                this._from = value;
            }
        }


        /// <summary>
        /// 发件人姓名
        /// </summary>
        public string FromName
        {
            get
            {
                return this._fromName;
            }
            set
            {
                this._fromName = value;
            }
        }


        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string RecipientName
        {
            get
            {
                return this._recipientName;
            }
            set
            {
                this._recipientName = value;
            }
        }

        /// <summary>
        /// 邮箱域
        /// </summary>
        public string MailDomain
        {
            get
            {
                return this._mailDomain;
            }
            set
            {
                this._mailDomain = value;
            }
        }

        /// <summary>
        /// 邮件服务器端口号
        /// </summary>	
        public int MailDomainPort
        {
            set
            {
                this._mailserverport = value;
            }
            get
            {
                return this._mailserverport;
            }
        }


        /// <summary>
        /// SMTP认证时使用的用户名
        /// </summary>
        public string MailServerUserName
        {
            set
            {
                if (value.Trim() != "")
                {
                    this._username = value.Trim();
                }
                else
                {
                    this._username = "";
                }
            }
            get
            {
                return _username;
            }
        }

        /// <summary>
        /// SMTP认证时使用的密码
        /// </summary>
        public string MailServerPassWord
        {
            set
            {
                this._password = value;
            }
            get
            {
                return _password;
            }
        }

        /// <summary>
        ///  是否Html邮件
        /// </summary>
        public bool Html
        {
            get
            {
                return this._html;
            }
            set
            {
                this._html = value;
            }
        }



        //收件人的邮箱地址
        public bool AddRecipient(params string[] username)
        {
            //this._recipient= null;
            this._recipient = username[0].Trim();

            return true;
        }


        //抄送人的邮箱地址
        public bool AddRecipientCC(params string[] username)
        {
            this._recipientCC = username[0].Trim();

            return true;
        }

        //密送人的邮箱地址
        public bool AddRecipientBCC(params string[] username)
        {
            this._recipientBCC = username[0].Trim();

            return true;
        }

        /// <summary>
        /// 将字符串编码为Base64字符串
        /// </summary>
        /// <param name="str">要编码的字符串</param>
        private string Base64Encode(string str)
        {
            byte[] barray;
            barray = Encoding.Default.GetBytes(str);
            return Convert.ToBase64String(barray);
        }

        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="path"></param>
        public void AddAttachment(string path)
        {
            Attachment attach = new Attachment(path, MediaTypeNames.Application.Octet);
            myEmail.Attachments.Add(attach);
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <returns></returns>
        public bool Send()
        {            
            Encoding eEncod = Encoding.GetEncoding("utf-8");
            myEmail.From = new System.Net.Mail.MailAddress(this.From, this.Subject, eEncod);
            myEmail.To.Add(this._recipient);
            myEmail.CC.Add(this._recipientCC);
            myEmail.Subject = this.Subject;
            myEmail.IsBodyHtml = true;
            myEmail.Body = this.Body;
            myEmail.Priority = System.Net.Mail.MailPriority.Normal;
            myEmail.BodyEncoding = Encoding.GetEncoding("utf-8");
            //myEmail.BodyFormat = this.Html?MailFormat.Html:MailFormat.Text; //邮件形式，.Text、.Html 

            //优先级
            switch (this._priority)
            {
                case "High":
                    myEmail.Priority = MailPriority.High;
                    break;
                case "Low":
                    myEmail.Priority = MailPriority.Low;
                    break;
                default:
                    myEmail.Priority = MailPriority.Normal;
                    break;
            }

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = this.MailDomain;
            smtp.Port = this.MailDomainPort;
            smtp.Credentials = new System.Net.NetworkCredential(this.MailServerUserName, this.MailServerPassWord);
            //smtp.UseDefaultCredentials = true;
            //smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            //当不是25端口(gmail:587)
            if (this.MailDomainPort != 25)
            {
                smtp.EnableSsl = true;
            }
            //System.Web.Mail.SmtpMail.SmtpServer = this.MailDomain;

            try
            {
                smtp.Send(myEmail);
            }
            catch (System.Net.Mail.SmtpException e)
            {
                string result = e.Message;
                return false;
            }

            return true;
        }
    }
}