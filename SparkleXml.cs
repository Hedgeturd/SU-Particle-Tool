using System.Xml;
namespace SU_Particle_Tool;

public class SparkleXml
{
    public static void ReadXml(string path)
    {
        /*
         * This may not end up being used.
         * Unless someone wants to make mods that include binaries and use merge fs.
         */
    }
    
    public static void WriteXml(string path)
    {
        string extension = null;
        switch (SparkleBin.SparkleFile.Header.Type)
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

        switch (SparkleBin.SparkleFile.Header.Type)
        {
            case "CEffect":
                CEffectFunctions.CEffectWriteXml(writer, SparkleBin.SparkleFile.CEffect);
                break;
            case "Material":
                MaterialFunctions.MaterialWriteXml(writer, SparkleBin.SparkleFile.Material);
                break;
            default:
                Console.WriteLine("Unknown Type");
                break;
        }
        
        writer.WriteEndDocument();
        writer.Close();
    }
}