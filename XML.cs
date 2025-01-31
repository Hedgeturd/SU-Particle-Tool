using System.Xml;
namespace SU_Particle_Tool;

public class XML
{
    public static void SparkleReadXml(string path)
    {
        
    }
    
    public static void SparkleWriteXml(string path)
    {
        File.Delete(Path.Combine(Path.GetFileNameWithoutExtension(path) + ".xml"));
        var xmlWriterSettings = new XmlWriterSettings { Indent = true };
        using var writer = XmlWriter.Create(Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + ".xml", xmlWriterSettings);
        
        writer.WriteStartDocument();

        switch (Sparkle.sparkleFile.Header.type)
        {
            case "CEffect":
                break;
            case "Material":
                SparkleFunctions.MaterialWriteXML(writer, Sparkle.sparkleFile.Material);
                break;
            default:
                Console.WriteLine("Unknown Type");
                break;
        }
        
        writer.WriteEndDocument();
        writer.Close();
    }
}