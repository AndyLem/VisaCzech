using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VisaCzech.BL;

namespace VisaCzech.DL
{
    public sealed class PacketStorage : Storage<Packet>
    {
        private static PacketStorage _instance;
        public static PacketStorage Instance
        {
            get { return _instance ?? (_instance = new PacketStorage()); }
        }

        private PacketStorage()
        {
            _dirName = "Packets\\";
        }

        internal bool PacketExists(string packetName)
        {
            var allPackets = LoadAll();
            return allPackets.Any(packet => packet.Name == packetName);
        }
    }
}
