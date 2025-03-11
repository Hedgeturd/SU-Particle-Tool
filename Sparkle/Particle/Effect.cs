using System.Numerics;
using System.Xml;

namespace SU_Particle_Tool.Sparkle.Particle;

public class Effect
{
    string EffectName;
    float InitialLifeTime;
    float ScaleRatio;
    float GenerateCountRatio ;
    Vector4 InitialPosition;
    Vector4 InitialRotation;
    bool IsLoop;
    bool DeleteChildren;
    float VelocityOffset;
    int EmitterCount;
    int ukn0, ukn1, ukn2, ukn3;
    public List<Emitter> Emitters;
    
    // Binary Functions
    public static Effect ReadBin(BinaryReader reader, int EmitterCount) //, Header sparkleIn, Effect effect)
    {
        Effect effect = new Effect();
        
        Console.WriteLine("\nEffectSaveLoad");
        effect.EffectName = Common.ReadName(reader);
        effect.InitialLifeTime = reader.ReadSingle();
        effect.ScaleRatio = reader.ReadSingle();
        effect.GenerateCountRatio = reader.ReadSingle();
        
        effect.InitialPosition = new Vector4(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        effect.InitialRotation = new Vector4(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

        effect.IsLoop = Common.ReadBool(reader);
        effect.DeleteChildren = Common.ReadBool(reader);
        effect.VelocityOffset = reader.ReadSingle();
        
        effect.ukn0 = reader.ReadInt32();
        effect.ukn1 = reader.ReadInt32();
        effect.ukn2 = reader.ReadInt32();
        effect.ukn3 = reader.ReadInt32();
        
        for (int i = 0; i < 4; i++)
        {
            Console.Write(System.Text.Encoding.UTF8.GetString(reader.ReadBytes(1)));
            reader.BaseStream.Position += 3;
        }

        Console.WriteLine("\nEmitterSaveLoadList");
        List<Emitter> emitterSaveLoadList = new List<Emitter>();

        for (int i = 0; i < EmitterCount; i++)
        {
            emitterSaveLoadList.Add(Emitter.ReadBin(reader));
        }
        
        // Footer SEGA
        for (int i = 0; i < 4; i++)
        {
            Console.Write(System.Text.Encoding.UTF8.GetString(reader.ReadBytes(1)));
            reader.BaseStream.Position += 3;
        }

        effect.Emitters = emitterSaveLoadList;
        
        return effect;
    }

    public static void WriteBin(BinaryWriter writer, Effect effect)
    {
        writer.Write(effect.EffectName);
        Common.WritePadding(writer);
        writer.Write(effect.InitialLifeTime);
        writer.Write(effect.ScaleRatio);
        writer.Write(effect.GenerateCountRatio);
        
        writer.Write(effect.InitialPosition.X);
        writer.Write(effect.InitialPosition.Y);
        writer.Write(effect.InitialPosition.Z);
        writer.Write(effect.InitialPosition.W);
        
        writer.Write(effect.InitialRotation.X);
        writer.Write(effect.InitialRotation.Y);
        writer.Write(effect.InitialRotation.Z);
        writer.Write(effect.InitialRotation.W);
        
        writer.Write(effect.IsLoop);
        Common.BoolPadding(writer);
        writer.Write(effect.DeleteChildren);
        Common.BoolPadding(writer);
        writer.Write(effect.VelocityOffset);

        writer.Write(effect.ukn0);
        writer.Write(effect.ukn1);
        writer.Write(effect.ukn2);
        writer.Write(effect.ukn3);
        
        writer.Write(83);
        writer.Write(69);
        writer.Write(71);
        writer.Write(65);

        for (int i = 0; i < effect.EmitterCount; i++)
        {
            Emitter.WriteBin(writer, effect.Emitters[i]);
        }
    }
    
    // Xml Functions
    public static Effect ReadXml(XmlElement node)
    {
        Effect effect = new Effect();
        Console.WriteLine("Reading EffectSaveLoad");
        effect.EffectName = node.GetElementsByTagName("EffectName")[0].InnerText;
        effect.InitialLifeTime = float.Parse(node.GetElementsByTagName("InitialLifeTime")[0].InnerText);
        effect.ScaleRatio = float.Parse(node.GetElementsByTagName("ScaleRatio")[0].InnerText);
        effect.GenerateCountRatio = float.Parse(node.GetElementsByTagName("GenerateCountRatio")[0].InnerText);
        effect.InitialPosition = SparkleFunctions.VectorReadXml(node.GetElementsByTagName("InitialPosition")[0], false);
        effect.InitialRotation = SparkleFunctions.VectorReadXml(node.GetElementsByTagName("InitialRotation")[0], true);
        effect.IsLoop = bool.Parse(node.GetElementsByTagName("IsLoop")[0].InnerText);
        effect.DeleteChildren = bool.Parse(node.GetElementsByTagName("DeleteChildren")[0].InnerText);
        effect.VelocityOffset = float.Parse(node.GetElementsByTagName("VelocityOffset")[0].InnerText);
        
        effect.ukn0 = int.Parse(node.GetElementsByTagName("Ukn0")[0].InnerText);
        effect.ukn1 = int.Parse(node.GetElementsByTagName("Ukn1")[0].InnerText);
        effect.ukn2 = int.Parse(node.GetElementsByTagName("Ukn2")[0].InnerText);
        effect.ukn3 = int.Parse(node.GetElementsByTagName("Ukn3")[0].InnerText);
        
        List<Emitter> Emitters = new List<Emitter>();
        
        foreach (XmlElement childnode in node.GetElementsByTagName("EmitterSaveLoadList")[0])
        {
            Emitters.Add(Emitter.ReadXml(childnode));
        }
        
        effect.EmitterCount = Emitters.Count;
        effect.Emitters = Emitters;
        
        return effect;
    }
    
    public static void WriteXml(XmlWriter writer, Effect effect)
    {
        writer.WriteStartElement("InportExportEffect");
        writer.WriteAttributeString("xmlns", "xsi",null, "http://www.w3.org/2001/XMLSchema-instance");
        writer.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
        
        SparkleFunctions.WriteExportXml(writer);
        
        writer.WriteStartElement("EffectSaveLoad");
            SparkleFunctions.WriteQuickExportXml(writer);
            SparkleFunctions.WriteExportXml(writer);
            
            writer.WriteElementString("EffectName", effect.EffectName);
            writer.WriteElementString("InitialLifeTime", effect.InitialLifeTime.ToString());
            writer.WriteElementString("ScaleRatio", effect.ScaleRatio.ToString());
            writer.WriteElementString("GenerateCountRatio", effect.GenerateCountRatio.ToString());
            
            SparkleFunctions.VectorWriteXml(writer, "InitialPosition", effect.InitialPosition, false);
            SparkleFunctions.VectorWriteXml(writer, "InitialRotation", effect.InitialRotation, true);
            
            writer.WriteElementString("IsLoop", effect.IsLoop.ToString().ToLower());
            writer.WriteElementString("DeleteChildren", effect.DeleteChildren.ToString().ToLower());
            writer.WriteElementString("VelocityOffset", effect.VelocityOffset.ToString());
            
            writer.WriteElementString("Ukn0", effect.ukn0.ToString());
            writer.WriteElementString("Ukn1", effect.ukn1.ToString());
            writer.WriteElementString("Ukn2", effect.ukn2.ToString());
            writer.WriteElementString("Ukn3", effect.ukn3.ToString());
            
            writer.WriteStartElement("EmitterSaveLoadList");
            foreach (var emitter in effect.Emitters)
            {
                Emitter.WriteXml(writer, emitter);
            }
            writer.WriteEndElement();

            writer.WriteEndElement();
        
        writer.WriteEndElement();
    }
}