using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using Glorysoft.BC.Entity;
namespace Glorysoft.BC.RV.RVService
{
    public class MessageInfo
    {
        public MessageInfo()
        {
            DaemonList = new List<string>();
            ListenSubjectList = new List<string>();
        }
        public string Name { get; set; }
        public virtual string Service { get; set; }
        public virtual string Network { get; set; }
        public virtual string Daemon { get; set; }
        public virtual List<string> DaemonList { get; set; }
        public virtual string SourceSubject { get; set; }
        public virtual List<string> ListenSubjectList { get; set; }
        public virtual string TargetSubject { get; set; }
        public virtual string FieldName { get; set; }
        public virtual string MessageRequestRoot { get; set; }
        public virtual string MessageReplyRoot { get; set; }
        
        public virtual int TimeOut { get; set; }
        public virtual string OwnSubject { get; set; }
        public string EnvName { get; set; }
    }
}
