using System.Xml;
namespace SU_Particle_Tool.Sparkle.P_Material;

public class Material
{
    string materialName;
    string shaderName;
    string textureName;
    string deflectionTextureName;
    int addressMode; 
    int blendMode;
    
    enum Address {
        CLAMP,
        WRAP
    }
    enum Blends {
        Typical = 1,
        Add = 2
    }

    // Binary Functions
    public static Material ReadBin(BinaryReader reader, Material material)
    {
        Console.WriteLine("\nMaterialSaveLoad");
        material.materialName = Common.ReadName(reader);
        Console.WriteLine(material.materialName);

        material.shaderName = Common.ReadName(reader);
        Console.WriteLine(material.shaderName);

        material.textureName = Common.ReadName(reader);
        Console.WriteLine(material.textureName);

        material.deflectionTextureName = Common.ReadName(reader);
        Console.WriteLine(material.deflectionTextureName);

        material.addressMode = reader.ReadInt32();
        material.blendMode = reader.ReadInt32();

        Console.WriteLine((Address)material.addressMode);
        Console.WriteLine((Blends)material.blendMode);

        Console.WriteLine();

        for (int i = 0; i < 4; i++)
        {
            Console.Write(System.Text.Encoding.UTF8.GetString(reader.ReadBytes(1)));
            reader.BaseStream.Position += 3;
        }

        return material;
    }
    
    public static void WriteBin(BinaryWriter writer, Material material)
    {
        writer.Write(material.materialName);
        Common.WritePadding(writer);
        writer.Write(material.shaderName);
        Common.WritePadding(writer);
        writer.Write(material.textureName);
        Common.WritePadding(writer);
        writer.Write(material.deflectionTextureName);
        Common.WritePadding(writer);
        writer.Write(material.addressMode);
        writer.Write(material.blendMode);
    }

    // XML Functions
    public static Material ReadXml(XmlElement node)
    {
        Material materialSaveLoad = new Material();
        
        Console.WriteLine("Reading MaterialSaveLoad");
        materialSaveLoad.materialName = node.ChildNodes[1].InnerText;
        materialSaveLoad.textureName = node.ChildNodes[2].InnerText;
        materialSaveLoad.deflectionTextureName = node.ChildNodes[3].InnerText;
        materialSaveLoad.shaderName = node.ChildNodes[4].InnerText;
        materialSaveLoad.blendMode = (int)(Blends)Enum.Parse(typeof(Blends), node.ChildNodes[5].InnerText);
        materialSaveLoad.addressMode = (int)(Address)Enum.Parse(typeof(Address), node.ChildNodes[6].InnerText);
        
        return materialSaveLoad;
    }
    
    public static void WriteXml(XmlWriter writer, Material material)
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

        writer.WriteElementString("blendMode", ((Blends)material.blendMode).ToString());
        writer.WriteElementString("addressMode", ((Address)material.addressMode).ToString());
        //writer.WriteElementString("uvDescType", "UVDesc_1x1");    // This is part of the preview build spec however not in final version
        writer.WriteEndElement();
    }
}