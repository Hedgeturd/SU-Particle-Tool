using System.Xml;

namespace SU_Particle_Tool.Sparkle;

public class Header
{
    string Editor;
    public string Type;
    public int EmitterCount;
    public int ParticleCount;
    public long ExportDate;
    public int Version;
    
    // Sort me bbg
    static string ExportDataXMLConv(string thing, bool export)
    {
        thing = thing.Remove(4, 1);
        thing = thing.Remove(6, 1);
        if (export)
        {
            thing = thing.Remove(8, 1);
            thing = thing.Remove(10, 1);
            thing = thing.Remove(12, 1);
        }
        return thing;
    }

    // Binary Functions
    public static Header ReadBin(BinaryReader reader) //, Header sparkleHeader)
    {
        Header sparkleHeader  = new Header();
        
        sparkleHeader.Editor = Common.ReadName(reader);
        Console.WriteLine(sparkleHeader.Editor);
        sparkleHeader.Type = Common.ReadName(reader);
        Console.WriteLine(sparkleHeader.Type);

        if (sparkleHeader.Type == "CEffect")
        {
            sparkleHeader.EmitterCount = reader.ReadInt32();
            sparkleHeader.ParticleCount = reader.ReadInt32();
        }
        
        Console.WriteLine("\nExportInfo");
        sparkleHeader.ExportDate = reader.ReadInt64();
        sparkleHeader.Version = reader.ReadInt32();
        
        return sparkleHeader;
    }
    
    public static void WriteBin(BinaryWriter writer, Header header)
    {
        //var header = sparkleComposite.Header;
        writer.Write("Sparkle");
        Common.WritePadding(writer);
        writer.Write(header.Type);
        Common.WritePadding(writer);

        if (header.Type == "CEffect")
        {
            writer.Write(header.EmitterCount);
            writer.Write(header.ParticleCount);
        }
        
        writer.Write(header.ExportDate);
        writer.Write(header.Version);
    }

    // XML Functions
    public static Header ReadXml(XmlElement node, string type)
    {
        Header ExportInfo  = new Header();
        Console.WriteLine("Reading ExportInfo");
        ExportInfo.Editor = "Sparkle";
        ExportInfo.Type = type;
        ExportInfo.ExportDate = long.Parse(ExportDataXMLConv(node.ChildNodes[0].InnerText, true));
        ExportInfo.Version = int.Parse(ExportDataXMLConv(node.ChildNodes[1].InnerText, false));
        return ExportInfo;
    }
    
    public static void WriteXml(string path)
    {
        
    }
}