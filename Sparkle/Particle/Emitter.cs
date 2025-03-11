using System.Numerics;
using System.Xml;
namespace SU_Particle_Tool.Sparkle.Particle;

public class Emitter
{
    enum EmitterTypes
    {
        Box,
        Cylinder,
        Mesh,
        Sphere
    }

    enum EmitConditions
    {
        Time
    }

    enum cylinderEmittionTypes
    {
        ECylinder_UserVelocity
    }

    struct Cylinder
    {
        public bool m_equiangularly;
        public bool m_circumference;
        public bool m_isCone;
        public float m_angle;
        public float m_radius;
        public float m_height;
        public float m_minAngle;
        public float m_maxAngle;
        public int m_cylinderEmittionType;
    }

    public string Type;
    int ParticleCount;
    string EmitterName;
    int MaxGenerateCount;
    int GenerateCount;
    int ParticleDataFlags;
    bool Infinite;
    float InitialEmittionGap;

    Vector4 InitialPosition;
    Vector4 RotationXYZ;
    Vector4 RotationXYZBias;
    Vector4 InitialRotationXYZ;
    Vector4 InitialRotation;

    float InitialEmitterLifeTime;
    float EmitStartTime;
    int EmitCondition;
    int EmitterType;

    Cylinder CylinderParams;
    float m_latitude_max_angle;
    float m_longitude_max_angle;
    Vector4 m_size;
    string MeshName;
    int ukn0, ukn1, ukn2, ukn3;

    int AnimCount;
    Animation EmitterAnim;

    public List<Particle> ParticleSaveLoad;

    static Cylinder CylinderParamReadBin(BinaryReader binaryReader) //, Emitter emitterSaveLoad)
    {
        //var cylinder = emitterSaveLoad.CylinderParams;
        Cylinder cylinder = new Cylinder();
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

    static void CylinderParamWriteBin(BinaryWriter writer, Cylinder cylinder)
    {
        writer.Write(cylinder.m_equiangularly);
        Common.BoolPadding(writer);
        writer.Write(cylinder.m_circumference);
        Common.BoolPadding(writer);
        writer.Write(cylinder.m_isCone);
        Common.BoolPadding(writer);
        writer.Write(cylinder.m_angle);
        writer.Write(cylinder.m_radius);
        writer.Write(cylinder.m_height);
        writer.Write(cylinder.m_minAngle);
        writer.Write(cylinder.m_maxAngle);
        writer.Write(cylinder.m_cylinderEmittionType);
    }

    static void SphereParamReadBin(BinaryReader binaryReader, ref Emitter emitterSaveLoad)
    {
        emitterSaveLoad.m_latitude_max_angle = binaryReader.ReadSingle();
        emitterSaveLoad.m_longitude_max_angle = binaryReader.ReadSingle();
    }

    static Cylinder CylinderParamsReadXml(XmlNode node)
    {
        Cylinder cylinder = new Cylinder
        {
            m_equiangularly = bool.Parse(node.ChildNodes[0].InnerText),
            m_circumference = bool.Parse(node.ChildNodes[1].InnerText),
            m_isCone = bool.Parse(node.ChildNodes[2].InnerText),
            m_angle = float.Parse(node.ChildNodes[3].InnerText),
            m_radius = float.Parse(node.ChildNodes[4].InnerText),
            m_height = float.Parse(node.ChildNodes[5].InnerText),
            m_minAngle = float.Parse(node.ChildNodes[6].InnerText),
            m_maxAngle = float.Parse(node.ChildNodes[7].InnerText),
            m_cylinderEmittionType = (int)Enum.Parse(typeof(cylinderEmittionTypes), node.ChildNodes[8].InnerText),
        };
        return cylinder;
    }

    public static Emitter ReadBin(BinaryReader binaryReader)
    {
        Console.WriteLine("\nEmitterSaveLoad");
        Emitter emitterSaveLoad = new Emitter();
        // Emitter Params
        emitterSaveLoad.Type = Common.ReadName(binaryReader);
        emitterSaveLoad.ParticleCount = binaryReader.ReadInt32();
        emitterSaveLoad.EmitterName = Common.ReadName(binaryReader);

        emitterSaveLoad.MaxGenerateCount = binaryReader.ReadInt32();
        emitterSaveLoad.GenerateCount = binaryReader.ReadInt32();
        emitterSaveLoad.ParticleDataFlags = binaryReader.ReadInt32();
        emitterSaveLoad.Infinite = Common.ReadBool(binaryReader);
        emitterSaveLoad.InitialEmittionGap = binaryReader.ReadSingle();

        emitterSaveLoad.InitialPosition = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(),
            binaryReader.ReadSingle(), binaryReader.ReadSingle());
        emitterSaveLoad.RotationXYZ = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(),
            binaryReader.ReadSingle(), binaryReader.ReadSingle());
        emitterSaveLoad.RotationXYZBias = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(),
            binaryReader.ReadSingle(), binaryReader.ReadSingle());
        emitterSaveLoad.InitialRotationXYZ = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(),
            binaryReader.ReadSingle(), binaryReader.ReadSingle());
        emitterSaveLoad.InitialRotation = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(),
            binaryReader.ReadSingle(), binaryReader.ReadSingle());

        emitterSaveLoad.InitialEmitterLifeTime = binaryReader.ReadSingle();
        emitterSaveLoad.EmitStartTime = binaryReader.ReadSingle();
        emitterSaveLoad.EmitCondition = binaryReader.ReadInt32();
        emitterSaveLoad.EmitterType = binaryReader.ReadInt32();

        emitterSaveLoad.CylinderParams = CylinderParamReadBin(binaryReader);
        SphereParamReadBin(binaryReader, ref emitterSaveLoad);
        emitterSaveLoad.m_size = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
        emitterSaveLoad.MeshName = Common.ReadName(binaryReader);

        emitterSaveLoad.ukn0 = binaryReader.ReadInt32();
        emitterSaveLoad.ukn1 = binaryReader.ReadInt32();
        emitterSaveLoad.ukn2 = binaryReader.ReadInt32();
        emitterSaveLoad.ukn3 = binaryReader.ReadInt32();

        // Emitter Animation
        emitterSaveLoad.AnimCount = binaryReader.ReadInt32();
        if (emitterSaveLoad.AnimCount > 0)
        {
            emitterSaveLoad.EmitterAnim = Animation.ReadBin(binaryReader);
        }

        // ParticleSaveLoadList
        if (emitterSaveLoad.ParticleCount > 0)
        {
            emitterSaveLoad.ParticleSaveLoad = new List<Particle>();

            // ParticleSaveLoad
            for (int p = 0; p < emitterSaveLoad.ParticleCount; p++)
            {
                Particle particleSaveLoad = new Particle();

                particleSaveLoad.Type = Common.ReadName(binaryReader);

                if (particleSaveLoad.Type == "ParticleChunk")
                {
                    Particle.ReadBin(binaryReader, ref particleSaveLoad);
                    emitterSaveLoad.ParticleSaveLoad.Add(particleSaveLoad);
                }
            }
        }

        Console.WriteLine("\nEnd");
        return emitterSaveLoad;
    }

    public static void WriteBin(BinaryWriter writer, Emitter emitter)
    {
        writer.Write("EmitterChunk");
        Common.WritePadding(writer);
        writer.Write(emitter.ParticleCount);
        writer.Write(emitter.EmitterName);
        Common.WritePadding(writer);
        writer.Write(emitter.MaxGenerateCount);
        writer.Write(emitter.GenerateCount);
        writer.Write(emitter.ParticleDataFlags);
        writer.Write(emitter.Infinite);
        Common.BoolPadding(writer);
        writer.Write(emitter.InitialEmittionGap);
        
        SparkleFunctions.VectorWriteBin(writer, emitter.InitialPosition);
        SparkleFunctions.VectorWriteBin(writer, emitter.RotationXYZ);
        SparkleFunctions.VectorWriteBin(writer, emitter.RotationXYZBias);
        SparkleFunctions.VectorWriteBin(writer, emitter.InitialRotationXYZ);
        SparkleFunctions.VectorWriteBin(writer, emitter.InitialRotation);
        
        writer.Write(emitter.InitialEmitterLifeTime);
        writer.Write(emitter.EmitStartTime);
        writer.Write(emitter.EmitCondition);
        writer.Write(emitter.EmitterType);

        CylinderParamWriteBin(writer, emitter.CylinderParams);
        writer.Write(emitter.m_latitude_max_angle);
        writer.Write(emitter.m_longitude_max_angle);
        SparkleFunctions.VectorWriteBin(writer, emitter.m_size);
        writer.Write(emitter.MeshName);
        Common.WritePadding(writer);
        
        writer.Write(emitter.ukn0);
        writer.Write(emitter.ukn1);
        writer.Write(emitter.ukn2);
        writer.Write(emitter.ukn3);

        writer.Write(emitter.AnimCount);
        if (emitter.AnimCount > 0)
        {
            Animation.WriteBin(writer, emitter.EmitterAnim);
        }

        for (int i = 0; i < emitter.ParticleCount; i++)
        {
            Particle.WriteBin(writer, emitter.ParticleSaveLoad[i]);
        }
    }

    public static Emitter ReadXml(XmlElement node)
    {
        Console.WriteLine("Reading EmitterSaveLoad");
        Emitter emitter = new Emitter();
        
        emitter.EmitterName = node.GetElementsByTagName("EmitterName")[0].InnerText;
        emitter.MaxGenerateCount = int.Parse(node.GetElementsByTagName("MaxGenerateCount")[0].InnerText);
        emitter.GenerateCount = int.Parse(node.GetElementsByTagName("GenerateCount")[0].InnerText);
        emitter.ParticleDataFlags = int.Parse(node.GetElementsByTagName("ParticleDataFlags")[0].InnerText);
        emitter.Infinite = bool.Parse(node.GetElementsByTagName("Infinite")[0].InnerText);
        emitter.InitialEmittionGap = float.Parse(node.GetElementsByTagName("InitialEmittionGap")[0].InnerText);
        
        emitter.InitialPosition = SparkleFunctions.VectorReadXml(node.GetElementsByTagName("InitialPosition")[0], false);
        emitter.RotationXYZ = SparkleFunctions.VectorReadXml(node.GetElementsByTagName("RotationXYZ")[0], false);
        emitter.RotationXYZBias = SparkleFunctions.VectorReadXml(node.GetElementsByTagName("RotationXYZBias")[0], false);
        emitter.InitialRotationXYZ = SparkleFunctions.VectorReadXml(node.GetElementsByTagName("InitialRotationXYZ")[0], false);
        emitter.InitialRotation = SparkleFunctions.VectorReadXml(node.GetElementsByTagName("InitialRotation")[0], true);
        
        emitter.InitialEmitterLifeTime = float.Parse(node.GetElementsByTagName("InitialEmitterLifeTime")[0].InnerText);
        emitter.EmitStartTime = float.Parse(node.GetElementsByTagName("EmitStartTime")[0].InnerText);
        emitter.EmitCondition = (int)Enum.Parse(typeof(EmitConditions), node.GetElementsByTagName("EmitCondition")[0].InnerText);
        emitter.EmitterType = (int)Enum.Parse(typeof(EmitterTypes), node.GetElementsByTagName("EmitterType")[0].InnerText);
        
        emitter.CylinderParams = CylinderParamsReadXml(node.GetElementsByTagName("CylinderParams")[0]);
        emitter.m_latitude_max_angle = float.Parse(node.GetElementsByTagName("SphereParams")[0].ChildNodes[0].InnerText);
        emitter.m_longitude_max_angle = float.Parse(node.GetElementsByTagName("SphereParams")[0].ChildNodes[1].InnerText);
        emitter.m_size = SparkleFunctions.VectorReadXml(node.GetElementsByTagName("BoxParams")[0].ChildNodes[0], false);
        emitter.MeshName = node.GetElementsByTagName("MeshParams")[0].ChildNodes[0].InnerText;
        
        emitter.ukn0 = int.Parse(node.GetElementsByTagName("Ukn0")[0].InnerText);
        emitter.ukn1 = int.Parse(node.GetElementsByTagName("Ukn1")[0].InnerText);
        emitter.ukn2 = int.Parse(node.GetElementsByTagName("Ukn2")[0].InnerText);
        emitter.ukn3 = int.Parse(node.GetElementsByTagName("Ukn3")[0].InnerText);

        if (node.ChildNodes[24].Name == "Animation")
        {
            emitter.AnimCount += 1;
            emitter.EmitterAnim = Animation.ReadXml(node.ChildNodes[24]);
        }
        
        List<Particle> Particles = new List<Particle>();
        foreach (XmlElement childnode in node.GetElementsByTagName("ParticleSaveLoadList")[0])
        {
            Particles.Add(Particle.ReadXml(childnode));
        }
        emitter.ParticleCount = Particles.Count;
        emitter.ParticleSaveLoad = Particles;
        
        return emitter;
    }
    
    public static void VectorWriteXml(XmlWriter writer, string field, Vector4 vecfield, bool vec4)
    {
        writer.WriteStartElement(field);
        writer.WriteElementString("X", vecfield.X.ToString());
        writer.WriteElementString("Y", vecfield.Y.ToString());
        writer.WriteElementString("Z", vecfield.Z.ToString());
        if (vec4) writer.WriteElementString("W", vecfield.W.ToString());
        writer.WriteEndElement();
    }
    
    public static void WriteXml(XmlWriter writer, Emitter emitter)
    {
        
        writer.WriteStartElement("EmitterSaveLoad");
            SparkleFunctions.WriteExportXml(writer);
            writer.WriteElementString("EmitterName", emitter.EmitterName);
            writer.WriteElementString("MaxGenerateCount", emitter.MaxGenerateCount.ToString());
            writer.WriteElementString("GenerateCount", emitter.GenerateCount.ToString());
            writer.WriteElementString("ParticleDataFlags", emitter.ParticleDataFlags.ToString());
            writer.WriteElementString("Infinite", emitter.Infinite.ToString().ToLower());
            writer.WriteElementString("InitialEmittionGap", emitter.InitialEmittionGap.ToString());
            
            VectorWriteXml(writer, "InitialPosition", emitter.InitialPosition, false);
            VectorWriteXml(writer, "RotationXYZ", emitter.RotationXYZ, false);
            VectorWriteXml(writer, "RotationXYZBias", emitter.RotationXYZBias, false);
            VectorWriteXml(writer, "InitialRotationXYZ", emitter.InitialRotationXYZ, false);
            VectorWriteXml(writer, "InitialRotation", emitter.InitialRotation, true);
            
            writer.WriteElementString("InitialEmitterLifeTime", emitter.InitialEmitterLifeTime.ToString());
            writer.WriteElementString("EmitStartTime", emitter.EmitStartTime.ToString());
            writer.WriteElementString("EmitCondition", ((EmitConditions)emitter.EmitCondition).ToString());
            writer.WriteElementString("EmitterType", ((EmitterTypes)emitter.EmitterType).ToString());
            
            writer.WriteStartElement("CylinderParams");
                writer.WriteElementString("m_equiangularly", emitter.CylinderParams.m_equiangularly.ToString().ToLower());
                writer.WriteElementString("m_circumference", emitter.CylinderParams.m_circumference.ToString().ToLower());
                writer.WriteElementString("m_isCone", emitter.CylinderParams.m_isCone.ToString().ToLower());
                writer.WriteElementString("m_angle", emitter.CylinderParams.m_angle.ToString());
                writer.WriteElementString("m_radius", emitter.CylinderParams.m_radius.ToString());
                writer.WriteElementString("m_height", emitter.CylinderParams.m_height.ToString());
                writer.WriteElementString("m_minAngle", emitter.CylinderParams.m_minAngle.ToString());
                writer.WriteElementString("m_maxAngle", emitter.CylinderParams.m_maxAngle.ToString());
                writer.WriteElementString("m_cylinderEmittionType", ((cylinderEmittionTypes)emitter.CylinderParams.m_cylinderEmittionType).ToString());
            writer.WriteEndElement();
            
            writer.WriteStartElement("SphereParams");
                writer.WriteElementString("m_latitude_max_angle", emitter.m_latitude_max_angle.ToString());
                writer.WriteElementString("m_longitude_max_angle", emitter.m_longitude_max_angle.ToString());
            writer.WriteEndElement();
            
            writer.WriteStartElement("BoxParams");
                writer.WriteStartElement("m_size");
                    writer.WriteElementString("X", emitter.m_size.X.ToString());
                    writer.WriteElementString("Y", emitter.m_size.Y.ToString());
                    writer.WriteElementString("Z", emitter.m_size.Z.ToString());
                writer.WriteEndElement();
            writer.WriteEndElement();
            
            writer.WriteStartElement("MeshParams");
                writer.WriteElementString("MeshName", emitter.MeshName);
            writer.WriteEndElement();
            
            writer.WriteElementString("Ukn0", emitter.ukn0.ToString());
            writer.WriteElementString("Ukn1", emitter.ukn1.ToString());
            writer.WriteElementString("Ukn2", emitter.ukn2.ToString());
            writer.WriteElementString("Ukn3", emitter.ukn3.ToString());
                    
            if (emitter.AnimCount > 0)
            {
                Animation.WriteXml(writer, emitter.EmitterAnim);
            }

            writer.WriteStartElement("ParticleSaveLoadList");
            foreach (var particle in emitter.ParticleSaveLoad)
            {
                writer.WriteStartElement("ParticleSaveLoad");
                SparkleFunctions.WriteExportXml(writer);
                Particle.WriteXml(writer, particle);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
                
        writer.WriteEndElement();
    }
}