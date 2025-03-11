using System.Numerics;
using System.Xml;

namespace SU_Particle_Tool;

public class SparkleFunctions
{
    static string ExportDateConversion()
    {
        string exportDate = SparkleBin.SparkleCompositeBin.Header.ExportDate.ToString();
        string exportDateYear = exportDate.Substring(0, 4);
        string exportDateMonth = exportDate.Substring(4, 2);
        string exportDateDate = exportDate.Substring(6, 2);
        
        string exportDateHour = exportDate.Substring(8, 2);
        string exportDateMinute = exportDate.Substring(10, 2);
        string exportDateSecond = exportDate.Substring(12, 2);

        return exportDateYear + "/" + exportDateMonth + "/" + exportDateDate + "/" +
                                    exportDateHour + ":" + exportDateMinute + ":" + exportDateSecond;
    }

    static string VersionConversion()
    {
        string version = SparkleBin.SparkleCompositeBin.Header.Version.ToString();
        string versionYear = version.Substring(0, 4);
        string versionMonth = version.Substring(4, 2);
        string versionDate = version.Substring(6, 2);

        return versionYear + "/" + versionMonth + "/" + versionDate;
    }

    public static void WriteExportXml(XmlWriter writer)
    {
        string exportDateReformat = ExportDateConversion();
        string versionReformat = VersionConversion();
        
        writer.WriteStartElement("ExportInfo");
        writer.WriteElementString("ExportDate", exportDateReformat);
        writer.WriteElementString("Version", versionReformat);
        writer.WriteElementString("Author", "Sonic_Team");
        writer.WriteEndElement();
    }
    
    public static void WriteQuickExportXml(XmlWriter writer)
    {
        string exportDateReformat = ExportDateConversion();
        string versionReformat = VersionConversion();
        
        writer.WriteElementString("ExportDate", exportDateReformat);
        writer.WriteElementString("Version", versionReformat);
        writer.WriteElementString("Author", "Sonic_Team");
    }

    public static Vector4 VectorReadXml(XmlNode node, bool vec4)
    {
        Vector4 vec = new Vector4();
        vec.X = float.Parse(node.ChildNodes[0].InnerText);
        vec.Y = float.Parse(node.ChildNodes[1].InnerText);
        vec.Z = float.Parse(node.ChildNodes[2].InnerText);
        if (vec4) vec.W = float.Parse(node.ChildNodes[3].InnerText);
        else vec.W = 0;

        return vec;
    }

    public static void VectorWriteBin(BinaryWriter writer, Vector4 vec)
    {
        writer.Write(vec.X);
        writer.Write(vec.Y);
        writer.Write(vec.Z);
        writer.Write(vec.W);
    }
    
    public static void VectorWriteXml(XmlWriter writer, string field, Vector4 vecfield, bool vec4)
    {
        writer.WriteStartElement(field);
        writer.WriteElementString("X", vecfield.X.ToString());
        writer.WriteElementString("Y", vecfield.Y.ToString());
        writer.WriteElementString("Z", vecfield.Z.ToString());
        if (vec4) writer.WriteElementString("W", vecfield.W.ToString());
        writer.WriteEndElement();
    }
}