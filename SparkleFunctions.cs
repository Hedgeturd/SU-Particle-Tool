using System.Xml;

namespace SU_Particle_Tool;

public class SparkleFunctions
{
    public enum Address {
        CLAMP,
        WRAP
    }
    
    public enum Blends {
        Typical = 1,
        Add = 2
    }
    
    public static Structs.SparkleMaterial MaterialReadBin(BinaryReader binaryReader, Structs.SparkleMaterial material)
    {
        Console.WriteLine("\nMaterialSaveLoad");
        material.materialName = Common.ReadName(binaryReader);
        Console.WriteLine(material.materialName);
        
        material.shaderName = Common.ReadName(binaryReader);
        Console.WriteLine(material.shaderName);
        
        material.textureName = Common.ReadName(binaryReader);
        Console.WriteLine(material.textureName);
        
        material.deflectionTextureName = Common.ReadName(binaryReader);
        Console.WriteLine(material.deflectionTextureName);
        
        material.addressMode = binaryReader.ReadInt32();
        material.blendMode = binaryReader.ReadInt32();

        Console.WriteLine((Address)material.addressMode);

        Console.WriteLine((Blends)material.blendMode);

        Console.WriteLine();

        for (int i = 0; i < 4; i++)
        {
            Console.Write(System.Text.Encoding.UTF8.GetString(binaryReader.ReadBytes(1)));
            binaryReader.BaseStream.Position += 3;
        }
        
        return material;
    }

    public static void MaterialWriteXML(XmlWriter writer, Structs.SparkleMaterial material)
    {
        writer.WriteStartElement("InportExportMaterial");
        writer.WriteAttributeString("xsi", "http://www.w3.org/2001/XMLSchema-instance");
        writer.WriteAttributeString("xsd", "http://www.w3.org/2001/XMLSchema");
        
        writer.WriteStartElement("ExportInfo");
        writer.WriteElementString("ExportDate", SparkleBin.SparkleFile.Header.exportDate.ToString());
        writer.WriteElementString("Version", SparkleBin.SparkleFile.Header.version.ToString());
        writer.WriteElementString("Author", null);
        writer.WriteEndElement();

        writer.WriteStartElement("MaterialSaveLoad");
        writer.WriteStartElement("ExportInfo");
        writer.WriteElementString("ExportDate", SparkleBin.SparkleFile.Header.exportDate.ToString());
        writer.WriteElementString("Version", SparkleBin.SparkleFile.Header.version.ToString());
        writer.WriteElementString("Author", null);
        writer.WriteEndElement();
        
        writer.WriteElementString("materialName", material.materialName);
        writer.WriteElementString("textureName", material.textureName);
        writer.WriteElementString("deflectionTextureName", material.deflectionTextureName);
        writer.WriteElementString("shaderName", material.shaderName);

        writer.WriteElementString("blendMode", material.blendMode.ToString());
        writer.WriteElementString("addressMode", material.addressMode.ToString());
        writer.WriteElementString("uvDescType", "UVDesc_1x1");
        writer.WriteEndElement();
    }
}