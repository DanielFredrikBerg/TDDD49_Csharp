using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tddd49.Model
{
    class ChatHistory
    {

        public static List<ChatConversation> conversations;
        private static Boolean isUpdated = false;

        public static Boolean IsUpdated {get => isUpdated; private set{ isUpdated = value; } }

        private ChatHistory()
        {

        }

        public static void AddConversation(ChatConversation conversation)
        {
            isUpdated = true;
            conversations.Add(conversation);
        }

        public static void Save()
        {

        }

        public static void Load()
        {

        }

    }
}
