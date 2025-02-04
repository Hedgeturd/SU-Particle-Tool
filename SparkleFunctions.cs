using System.Numerics;
using System.Xml;

namespace SU_Particle_Tool;

public class SparkleFunctions
{
    public static void WriteExportXml(XmlWriter writer)
    {
        string exportDate = SparkleBin.SparkleFile.Header.ExportDate.ToString();
        string exportDateYear = exportDate.Substring(0, 4);
        string exportDateMonth = exportDate.Substring(4, 2);
        string exportDateDate = exportDate.Substring(6, 2);
        
        string exportDateHour = exportDate.Substring(8, 2);
        string exportDateMinute = exportDate.Substring(10, 2);
        string exportDateSecond = exportDate.Substring(12, 2);

        string exportDateReformat = exportDateYear + "/" + exportDateMonth + "/" + exportDateDate + "/" +
                                    exportDateHour + ":" + exportDateMinute + ":" + exportDateSecond;
        
        string version = SparkleBin.SparkleFile.Header.Version.ToString();
        string versionYear = version.Substring(0, 4);
        string versionMonth = version.Substring(4, 2);
        string versionDate = version.Substring(6, 2);

        string versionReformat = versionYear + "/" + versionMonth + "/" + versionDate;
                                    writer.WriteStartElement("ExportInfo");
        writer.WriteElementString("ExportDate", exportDateReformat);
        writer.WriteElementString("Version", versionReformat);
        writer.WriteElementString("Author", "Sonic_Team");
        writer.WriteEndElement();
    }
}