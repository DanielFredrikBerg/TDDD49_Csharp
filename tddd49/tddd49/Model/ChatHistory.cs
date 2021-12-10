using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tddd49.Model
{
    class ChatHistory
    {

        private static readonly String filePath = "chat_history.json";

        private static ObservableCollection<ChatConversation> conversations;

        public static ObservableCollection<ChatConversation> Conversations { get => conversations; }

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
            string jsonString = JsonConvert.SerializeObject(conversations);
            File.WriteAllText(filePath, jsonString);
        }

        public static void Load()
        {
            conversations = new ObservableCollection<ChatConversation>();
            if (File.Exists(filePath))
            {
                using (StreamReader r = new StreamReader(filePath))
                {
                    string json = r.ReadToEnd();
                    conversations = JsonConvert.DeserializeObject<ObservableCollection<ChatConversation>>(json);
                }
            }
        }

    }
}
