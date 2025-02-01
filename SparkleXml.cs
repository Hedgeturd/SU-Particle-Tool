using System.Xml;
namespace SU_Particle_Tool;

public class SparkleXml
{
    public static void ReadXml(string path)
    {
        
    }
    
    public static void WriteXml(string path)
    {
        File.Delete(Path.Combine(Path.GetFileNameWithoutExtension(path) + ".xml"));
        var xmlWriterSettings = new XmlWriterSettings { Indent = true };
        using var writer = XmlWriter.Create(Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + ".xml", xmlWriterSettings);
        
        writer.WriteStartDocument();

        switch (SparkleBin.SparkleFile.Header.type)
        {
            case "CEffect":
                break;
            case "Material":
                SparkleFunctions.MaterialWriteXML(writer, SparkleBin.SparkleFile.Material);
                break;
            default:
                Console.WriteLine("Unknown Type");
                break;
        }
        
        writer.WriteEndDocument();
        writer.Close();
    }
}