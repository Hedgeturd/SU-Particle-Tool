using System.Xml;
namespace SU_Particle_Tool;

public class SparkleXml
{
    public static void ReadXml(string path)
    {
        
    }
    
    public static void WriteXml(string path)
    {
        File.Delete(Path.Combine(Path.GetFileNameWithoutExtension(path) + ".p-material"));
        var xmlWriterSettings = new XmlWriterSettings { Indent = true };
        using var writer = XmlWriter.Create(Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + ".p-material", xmlWriterSettings);
        
        writer.WriteStartDocument();

        switch (SparkleBin.SparkleFile.Header.Type)
        {
            case "CEffect":
                //SparkleFunctions.CEffectWriteXml(writer, SparkleBin.SparkleFile.CEffect);
                break;
            case "Material":
                SparkleFunctions.MaterialWriteXml(writer, SparkleBin.SparkleFile.Material);
                break;
            default:
                Console.WriteLine("Unknown Type");
                break;
        }
        
        writer.WriteEndDocument();
        writer.Close();
    }
}