using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tddd49.Model
{
    class ChatMessage
    {
        public String message;
        public String sender;

        public ChatMessage(String message, String sender)
        {
            this.message = message;
            this.sender = sender;
        }


        public override string ToString()
        {
            return sender + ":\n" + message;
        }
    }
}
