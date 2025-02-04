using System.Numerics;
using System.Xml;

namespace SU_Particle_Tool;

public class MaterialFunctions
{
    public static Structs.SparkleMaterial MaterialReadBin(BinaryReader binaryReader, Structs.SparkleHeader sparkleIn, Structs.SparkleMaterial material)
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

        Console.WriteLine((Structs.Address)material.addressMode);

        Console.WriteLine((Structs.Blends)material.blendMode);

        Console.WriteLine();

        for (int i = 0; i < 4; i++)
        {
            Console.Write(System.Text.Encoding.UTF8.GetString(binaryReader.ReadBytes(1)));
            binaryReader.BaseStream.Position += 3;
        }
        
        return material;
    }

    public static void MaterialWriteXml(XmlWriter writer, Structs.SparkleMaterial material)
    {
        writer.WriteStartElement("InportExportMaterial");
        writer.WriteAttributeString("xmlns", "xsi",null, "http://www.w3.org/2001/XMLSchema-instance");
        writer.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
        
        SparkleFunctions.WriteExportXml(writer);

        writer.WriteStartElement("MaterialSaveLoad");
        SparkleFunctions.WriteExportXml(writer);
        
        writer.WriteElementString("materialName", material.materialName);
        writer.WriteElementString("textureName", material.textureName);
        writer.WriteElementString("deflectionTextureName", material.deflectionTextureName);
        writer.WriteElementString("shaderName", material.shaderName);

        writer.WriteElementString("blendMode", ((Structs.Blends)material.blendMode).ToString());
        writer.WriteElementString("addressMode", ((Structs.Address)material.addressMode).ToString());
        writer.WriteElementString("uvDescType", "UVDesc_1x1");
        writer.WriteEndElement();
    }
}