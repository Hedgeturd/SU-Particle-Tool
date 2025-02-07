using System.Text;
using System.Xml;

namespace SU_Particle_Tool {
    class Common {

        /*public static int EndianSwap(int a) {
            byte[] x = BitConverter.GetBytes(a);
            Array.Reverse(x);
            int b = BitConverter.ToInt32(x, 0);
            return b;
        }

        public static float EndianSwapFloat(float a) {
            byte[] x = BitConverter.GetBytes(a);
            Array.Reverse(x);
            float b = BitConverter.ToSingle(x, 0);
            return b;
        }*/
        
        public static void SkipPadding(int NameLength, BinaryReader binaryReader)
        {
            long currentPos = binaryReader.BaseStream.Position;
            binaryReader.BaseStream.Position = (currentPos + (4 - 1)) & ~(4 - 1);
        }

        public static string ReadName(BinaryReader binaryReader)
        {
            byte NameLength = binaryReader.ReadByte();
            string Name = System.Text.Encoding.UTF8.GetString(binaryReader.ReadBytes(NameLength));
            Common.SkipPadding(NameLength, binaryReader);
            return Name;
        }

        public static bool ReadBool(BinaryReader binaryReader)
        {
            bool Value = BitConverter.ToBoolean(binaryReader.ReadBytes(1), 0);
            binaryReader.BaseStream.Seek(3, SeekOrigin.Current);
            return Value;
        }

        public static void WritePadding(string Name, BinaryWriter binaryWriter)
        {
            int padding = (4 - Name.Length % 4) % 4;
            if (padding == 0)
            {
                binaryWriter.Write(0);
            }
            else
            {
                for (int i = 0; i < padding; i++)
                {
                    byte hi = 0;
                    binaryWriter.Write(hi);
                }
            }
        }

        // XML Functions
        public static void ConvString(BinaryWriter writer, string value)  {
            // Turning string into Byte Array so the data can be written properly
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(value);
            writer.Write(utf8Bytes);
        }

        public static void RemoveComments(XmlNode node)
        {
            if (node == null) return;

            // Remove comment nodes
            for (int i = node.ChildNodes.Count - 1; i >= 0; i--)
            {
                XmlNode? child = node.ChildNodes[i];
                if (child.NodeType == XmlNodeType.Comment)
                {
                    node.RemoveChild(child);
                }
                else
                {
                    // Recursively remove comments from child nodes
                    RemoveComments(child);
                }
            }
        }
    }
}