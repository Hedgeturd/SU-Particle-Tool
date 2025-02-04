using System.Numerics;
using System.Xml;

namespace SU_Particle_Tool;

public class SparkleFunctions
{
    static CEffectStructs.CylinderParams CylinderParamReadBin(BinaryReader binaryReader, CEffectStructs.EmitterSaveLoad emitterSaveLoad)
    {
        var cylinder = emitterSaveLoad.CylinderParams;
        cylinder.m_equiangularly = BitConverter.ToBoolean(binaryReader.ReadBytes(1), 0);
        binaryReader.BaseStream.Seek(3, SeekOrigin.Current);
        cylinder.m_circumference = BitConverter.ToBoolean(binaryReader.ReadBytes(1), 0);
        binaryReader.BaseStream.Seek(3, SeekOrigin.Current);
        cylinder.m_isCone = BitConverter.ToBoolean(binaryReader.ReadBytes(1), 0);
        binaryReader.BaseStream.Seek(3, SeekOrigin.Current);
        cylinder.m_angle = binaryReader.ReadSingle();
        cylinder.m_radius = binaryReader.ReadSingle();
        cylinder.m_height = binaryReader.ReadSingle();
        cylinder.m_minAngle = binaryReader.ReadSingle();
        cylinder.m_maxAngle = binaryReader.ReadSingle();
        cylinder.m_cylinderEmittionType = binaryReader.ReadInt32();
        return cylinder;
    }

    static void SphereParamReadBin(BinaryReader binaryReader, ref CEffectStructs.EmitterSaveLoad emitterSaveLoad)
    {
        emitterSaveLoad.m_latitude_max_angle = binaryReader.ReadSingle();
        emitterSaveLoad.m_longitude_max_angle = binaryReader.ReadSingle();
    }

    static void WriteExportXml(XmlWriter writer)
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

    public static Structs.SparkleCEffect CEffectReadBin(BinaryReader binaryReader, Structs.SparkleHeader sparkleIn, Structs.SparkleCEffect effect)
    {
        Console.WriteLine("\nEffectSaveLoad");
        effect.EffectName = Common.ReadName(binaryReader);
        effect.InitialLifeTime = binaryReader.ReadSingle();
        effect.ScaleRatio = binaryReader.ReadSingle();
        effect.GenerateCountRatio = binaryReader.ReadSingle();
        
        effect.InitialPosition = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
        effect.InitialRotation = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());

        effect.IsLoop = Common.ReadBool(binaryReader);
        effect.DeleteChildren = Common.ReadBool(binaryReader);
        effect.VelocityOffset = binaryReader.ReadSingle();
        
        for (int i = 0; i < 8; i++)
        {
            Console.Write(System.Text.Encoding.UTF8.GetString(binaryReader.ReadBytes(1)));
            binaryReader.BaseStream.Position += 3;
        }

        Console.WriteLine("\nEmitterSaveLoadList");
        List<CEffectStructs.EmitterSaveLoad> emitterSaveLoadList = new List<CEffectStructs.EmitterSaveLoad>();

        for (int i = 0; i < sparkleIn.EmitterCount; i++)
        {
            CEffectStructs.EmitterSaveLoad emitterSaveLoad = new CEffectStructs.EmitterSaveLoad();
            Console.WriteLine("\nEmitterSaveLoad");
            emitterSaveLoad.Type = Common.ReadName(binaryReader);
            emitterSaveLoad.ParticleCount = binaryReader.ReadInt32();
            emitterSaveLoad.EmitterName = Common.ReadName(binaryReader);

            emitterSaveLoad.MaxGenerateCount = binaryReader.ReadInt32();
            emitterSaveLoad.GenerateCount = binaryReader.ReadInt32();
            emitterSaveLoad.ParticleDataFlags = binaryReader.ReadInt32();
            emitterSaveLoad.Infinite = Common.ReadBool(binaryReader);
            emitterSaveLoad.InitialEmittionGap = binaryReader.ReadSingle();
            
            emitterSaveLoad.InitialPosition = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
            emitterSaveLoad.RotationXYZ = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
            emitterSaveLoad.RotationXYZBias = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
            emitterSaveLoad.InitialRotationXYZ = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
            emitterSaveLoad.InitialRotation = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
            
            emitterSaveLoad.InitialEmitterLifeTime = binaryReader.ReadSingle();
            emitterSaveLoad.EmitStartTime = binaryReader.ReadSingle();
            emitterSaveLoad.EmitCondition = binaryReader.ReadInt32();
            emitterSaveLoad.EmitterType = binaryReader.ReadInt32();
            
            emitterSaveLoad.CylinderParams = CylinderParamReadBin(binaryReader, emitterSaveLoad);
            SphereParamReadBin(binaryReader, ref emitterSaveLoad);
            emitterSaveLoad.m_size = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
            emitterSaveLoad.MeshName = Common.ReadName(binaryReader);
            
            for (int j = 0; j < 4; j++)
            {
                Console.Write(System.Text.Encoding.UTF8.GetString(binaryReader.ReadBytes(1)));
                binaryReader.BaseStream.Position += 3;
            }
            binaryReader.BaseStream.Position += 4;

            for (int p = 0; p < emitterSaveLoad.ParticleCount; p++)
            {
                CEffectStructs.ParticleSaveLoad particleSaveLoad = new CEffectStructs.ParticleSaveLoad();
                particleSaveLoad.Type = Common.ReadName(binaryReader);
                particleSaveLoad.ParticleName = Common.ReadName(binaryReader);
                
                particleSaveLoad.LifeTime = binaryReader.ReadSingle();
                particleSaveLoad.LifeTimeBias = binaryReader.ReadSingle();
                
                particleSaveLoad.RotationZ = binaryReader.ReadSingle();
                particleSaveLoad.RotationZBias = binaryReader.ReadSingle();
                particleSaveLoad.InitialRotationZ = binaryReader.ReadSingle();
                particleSaveLoad.InitialRotationZBias = binaryReader.ReadSingle();
                
                particleSaveLoad.InitialSpeed = binaryReader.ReadSingle();
                particleSaveLoad.InitialSpeedBias = binaryReader.ReadSingle();
                particleSaveLoad.ZOffset = binaryReader.ReadSingle();
                particleSaveLoad.LocusDiff = binaryReader.ReadSingle();
            }

            Console.WriteLine("\nEnd");
            emitterSaveLoadList.Add(emitterSaveLoad);
        }

        effect.Emitters = emitterSaveLoadList;
        
        return effect;
    }

    public static void CEffectWriteXml(XmlWriter writer, Structs.SparkleCEffect effect)
    {
        writer.WriteStartElement("InportExportMaterial");
        writer.WriteAttributeString("xmlns", "xsi",null, "http://www.w3.org/2001/XMLSchema-instance");
        writer.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
    }
    
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
        
        WriteExportXml(writer);

        writer.WriteStartElement("MaterialSaveLoad");
        WriteExportXml(writer);
        
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