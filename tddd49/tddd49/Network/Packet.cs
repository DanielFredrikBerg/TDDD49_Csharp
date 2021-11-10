using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tddd49.Network
{
    class Packet
    {
        public PacketType _packetType;
        public String _content;

        public Packet(PacketType type, string content = null)
        {
            _packetType = type;
            _content = content;
        }
        public enum PacketType : byte
        {
            JoinChat = 0,
            AcceptChat = 1,
            DeclineChat = 2,
            ChatMessage = 3
        }
    }
}
