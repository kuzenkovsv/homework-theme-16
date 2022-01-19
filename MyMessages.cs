using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_theme_16
{
    public class MyMessages
    {
        public DateTime MessageTime { get; set; }
        public string MessageText { get; set; }

        public MyMessages(DateTime DateTime, string MessageText)
        {
            this.MessageTime = DateTime;
            this.MessageText = MessageText;
        }
    }
}
