using System.Text;
using System.Xml;

namespace SU_Particle_Tool {
    class Common {
        public static void SkipPadding(BinaryReader binaryReader)
        {
            long currentPos = binaryReader.BaseStream.Position;
            binaryReader.BaseStream.Position = (currentPos + (4 - 1)) & ~(4 - 1);
        }

        public static string ReadName(BinaryReader binaryReader)
        {
            byte NameLength = binaryReader.ReadByte();
            string Name = Encoding.UTF8.GetString(binaryReader.ReadBytes(NameLength));
            SkipPadding(binaryReader);
            return Name;
        }

        public static bool ReadBool(BinaryReader binaryReader)
        {
            bool Value = BitConverter.ToBoolean(binaryReader.ReadBytes(1), 0);
            binaryReader.BaseStream.Seek(3, SeekOrigin.Current);
            return Value;
        }

        public static void WritePadding(BinaryWriter binaryWriter)
        {
            long currentPos = binaryWriter.BaseStream.Position;
            long thing = (currentPos + (4 - 1)) & ~(4 - 1);

            while (binaryWriter.BaseStream.Position < thing )
            {
                binaryWriter.Write((byte)64);
            }
        }
        
        public static void BoolPadding(BinaryWriter writer)
        {
            long currentPos = writer.BaseStream.Position;
            long thing = (currentPos + (4 - 1)) & ~(4 - 1);

            while (writer.BaseStream.Position < thing )
            {
                writer.Write((byte)0);
            }
        }

        // XML Functions
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