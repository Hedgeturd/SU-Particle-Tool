using System.Xml;
using SU_Particle_Tool.Sparkle;

namespace SU_Particle_Tool;

public class SparkleXml
{
    public static Composite SparkleCompositeXml = new Composite();
    public static void ReadXml(string path)
    {
        string filePath = Path.GetDirectoryName(path) + "\\" + Path.GetFileName(path);
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(filePath);
        Common.RemoveComments(xDoc);
        XmlElement? xRoot = xDoc.DocumentElement;
        
        SparkleCompositeXml = Composite.ReadXml(xRoot);
        Console.WriteLine("Done Reading XML");
    }
    
    public static void WriteXml(string path)
    {
        string extension = null;
        switch (SparkleBin.SparkleCompositeBin.Header.Type)
        {
            case "Material":
                extension = ".p-material";
                break;
            case "CEffect":
                extension = ".particle";
                break;
            default:
                Console.WriteLine("Unknown Type");
                return;
        }
        
        File.Delete(Path.Combine(Path.GetFileNameWithoutExtension(path) + extension));
        var xmlWriterSettings = new XmlWriterSettings { Indent = true };
        using var writer = XmlWriter.Create(Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + extension, xmlWriterSettings);
        
        writer.WriteStartDocument();

        switch (SparkleBin.SparkleCompositeBin.Header.Type)
        {
            case "CEffect":
                Sparkle.Particle.Effect.WriteXml(writer, SparkleBin.SparkleCompositeBin.CEffect);
                break;
            case "Material":
                Sparkle.P_Material.Material.WriteXml(writer, SparkleBin.SparkleCompositeBin.Material);
                break;
            default:
                Console.WriteLine("Unknown Type");
                break;
        }
        
        writer.WriteEndDocument();
        writer.Close();
    }
}