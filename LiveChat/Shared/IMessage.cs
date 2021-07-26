using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Shared
{
    public interface IMessage
    {
        public string Msg { get; set; }
        public DateTime Date { get; set; }
        public string User { get; set; }
    }
}
