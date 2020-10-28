using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketGameProtocol;
using Google.Protobuf;

namespace Assets.Scripts.Socket.Tool
{
    class Message
    {
        private byte[] buffer = new byte[1024];

        private int startindex;

        public byte[] Buffer
        {
            get
            {
                return buffer;
            }
        }

        public int StartIndex
        {
            get
            {
                return startindex;
            }
        }

        public int Remsize
        {
            get
            {
                return buffer.Length - startindex;
            }
        }

        //解包
        public void ReadBuffer(int len, Action<MainPack> HandleRequest)
        {
            startindex += len;
            while (true)
            {
                if (startindex <= 4) return;
                int count = BitConverter.ToInt32(buffer, 0);
                if (startindex >= (count + 4))
                {
                    MainPack pack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(buffer, 4, count);
                    HandleRequest(pack);
                    Array.Copy(buffer, count + 4, buffer, 0, startindex - count - 4);
                    startindex -= (count + 4);
                }
                else
                {
                    break;
                }
            }
        }


        //打包
        public static byte[] PackData(MainPack pack)
        {
            byte[] data = pack.ToByteArray();//包体
            byte[] head = BitConverter.GetBytes(data.Length);//包头
            return head.Concat(data).ToArray();
        }

        public static Byte[] PackDataUDP(MainPack pack)
        {
            return pack.ToByteArray();
        }
    }
}
