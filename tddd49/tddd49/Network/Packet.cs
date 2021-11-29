using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tddd49.Network
{
    class Packet
    {
        public PacketType packetType { get; set; }
        public string userName { get; set; }
        public String message { get; set; }

        public Packet(PacketType packetType, string userName, string message)
        {
            this.packetType = packetType;
            this.userName = userName;
            this.message = message;
        }
        public enum PacketType : byte
        {
            RequestChat = 0,
            AcceptChat = 1,
            DeclineChat = 2,
            ChatMessage = 3
        }
    }
}
