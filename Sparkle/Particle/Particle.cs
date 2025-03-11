using System.Numerics;
using System.Xml;
namespace SU_Particle_Tool.Sparkle.Particle;

public class Particle
{
    public enum LocusUVTypes
    {
        EAllOver,
        ESeparate
    }
    public enum LayerTypes
    {
        TypeQuad,
        TypeVolume,
        TypeMesh,
        TypeLayered,
        TypeCEffect,
        TypeLocus,
        TypeDeflection
    }
    public enum PivotTypes
    {
        EPivot_Center,
        EPivot_Top,
        EPivot_Bottom,
        EPivot_Left,
        EPivot_Right,
        EPivot_Top_Left,
        EPivot_Top_Right,
        EPivot_Bottom_Left,
        EPivot_Bottom_Right,
        EPivot_UserSet
    }
    public enum UVDescTypes
    {
        UVDesc_1x1,
        UVDesc_2x2,
        UVDesc_3x3,
        UVDesc_4x4,
        UVDesc_5x5,
        UVDesc_6x6
    }
    public enum TextureIndexTypes
    {
        FixedIndex,
        InitialRandom,
        RandomOrder,
        SequentialOrder,
        ReverseOrder,
        UserSet
    }
    public enum DirectionTypes
    {
        EDirection_Billboard,
        EDirection_EmitterAxis,
        EDirection_DirectionalAngle,
        EDirection_YAxis,
        EDirection_XAxis,
        EDirection_ZAxis,
        EDirection_YRotOnly
    }
    public enum RefEffectEmitTimingTypes
    {
        ECreated,
        EKilled,
        EUserDelay
    }
    
    public string Type;
    string ParticleName;
    float LifeTime;
    float LifeTimeBias;

    float RotationZ;
    float RotationZBias;
    float InitialRotationZ;
    float InitialRotationZBias;
        
    float InitialSpeed ;
    float InitialSpeedBias;
    float ZOffset;
    float LocusDiff;

    int NumDivision;
    int LocusUVType;
        
    bool IsBillboard;
    bool IsEmitterLocal;
        
    int LayerType;
    int PivotType;
    int UVDescType;
    int TextureIndexType;
    int TextureIndexChangeInterval;
    int TextureIndexChangeIntervalBias;
    int InitialTextureIndex;
    int DirectionType;
    int ParticleDataFlags;

    struct Colour
    {
        public int A, R, G, B;
    }

    Colour Color;

    Vector4 Gravity;
    Vector4 ExternalForce;
    Vector4 InitialDirection;
    Vector4 InitialDirectionBias;
    Vector4 InitialScale;
    Vector4 InitialScaleBias;
        
    string MeshName;
        
    Vector4 RotationXYZ;
    Vector4 RotationXYZBias;
    Vector4 InitialRotationXYZ;
    Vector4 InitialRotationXYZBias;
    Vector4 UVScrollParam;
    Vector4 UVScrollParamAlpha;
        
    string RefEffectName;    
    int RefEffectEmitTimingType;
    float RefEffectDelayTime;

    float DirectionalVelocityRatio;
    float DeflectionScale;
    float SoftScale;
    float VelocityOffset;
    float UserData;
        
    string MaterialName;
    
    public int ukn0, ukn1, ukn2, ukn3;

    int AnimCount;
    Animation ParticleAnim;
    
    // Binary Functions
    public static void ReadBin(BinaryReader binaryReader, ref Particle particleSaveLoad)
    {
        particleSaveLoad = new Particle {
            Type = particleSaveLoad.Type,
            ParticleName = Common.ReadName(binaryReader),
    
            LifeTime = binaryReader.ReadSingle(),
            LifeTimeBias = binaryReader.ReadSingle(),
    
            RotationZ = binaryReader.ReadSingle(),
            RotationZBias = binaryReader.ReadSingle(),
            InitialRotationZ = binaryReader.ReadSingle(),
            InitialRotationZBias = binaryReader.ReadSingle(),
    
            InitialSpeed = binaryReader.ReadSingle(),
            InitialSpeedBias = binaryReader.ReadSingle(),
        
            ZOffset = binaryReader.ReadSingle(),
        
            LocusDiff = binaryReader.ReadSingle(),
            NumDivision = binaryReader.ReadInt32(),
            LocusUVType = binaryReader.ReadInt32(),
        
            IsBillboard = Common.ReadBool(binaryReader),
            IsEmitterLocal = Common.ReadBool(binaryReader),
        
            LayerType = binaryReader.ReadInt32(),
            PivotType = binaryReader.ReadInt32(),
            UVDescType = binaryReader.ReadInt32(),
            TextureIndexType = binaryReader.ReadInt32(),
            TextureIndexChangeInterval = binaryReader.ReadInt32(),
            TextureIndexChangeIntervalBias = binaryReader.ReadInt32(),
            InitialTextureIndex = binaryReader.ReadInt32(),
            DirectionType = binaryReader.ReadInt32(),
            ParticleDataFlags = binaryReader.ReadInt32(),
            
            Color = new Colour {A = binaryReader.ReadInt32(), R = binaryReader.ReadInt32(), G = binaryReader.ReadInt32(), B = binaryReader.ReadInt32()},
            
            Gravity = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()),
            ExternalForce = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()),
            
            InitialDirection = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()),
            InitialDirectionBias = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()),
            
            InitialScale = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()),
            InitialScaleBias = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()),
            
            MeshName = Common.ReadName(binaryReader),
            
            RotationXYZ = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()),
            RotationXYZBias = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()),
            InitialRotationXYZ = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()),
            InitialRotationXYZBias = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()),
            
            UVScrollParam = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()),
            UVScrollParamAlpha = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()),
            
            RefEffectName = Common.ReadName(binaryReader),
            RefEffectEmitTimingType = binaryReader.ReadInt32(),
            RefEffectDelayTime = binaryReader.ReadSingle(),
            
            DirectionalVelocityRatio = binaryReader.ReadSingle(),
            DeflectionScale = binaryReader.ReadSingle(),
            SoftScale = binaryReader.ReadSingle(),
            VelocityOffset = binaryReader.ReadSingle(),
            UserData = binaryReader.ReadInt32(),
            
            MaterialName = Common.ReadName(binaryReader),
            
            ukn0 = binaryReader.ReadInt32(),
            ukn1 = binaryReader.ReadInt32(),
            ukn2 = binaryReader.ReadInt32(),
            ukn3 = binaryReader.ReadInt32()
        };
        
        // Particle Animation
        particleSaveLoad.AnimCount = binaryReader.ReadInt32();
        if (particleSaveLoad.AnimCount > 0)
        {
            particleSaveLoad.ParticleAnim = Animation.ReadBin(binaryReader);
        }
    }

    public static void WriteBin(BinaryWriter writer, Particle particle)
    {
        writer.Write("ParticleChunk");
        Common.WritePadding(writer);
        writer.Write(particle.ParticleName);
        Common.WritePadding(writer);
        writer.Write(particle.LifeTime);
        writer.Write(particle.LifeTimeBias);
        
        writer.Write(particle.RotationZ);
        writer.Write(particle.RotationZBias);
        writer.Write(particle.InitialRotationZ);
        writer.Write(particle.InitialRotationZBias);
        
        writer.Write(particle.InitialSpeed);
        writer.Write(particle.InitialSpeedBias);
        
        writer.Write(particle.ZOffset);
        writer.Write(particle.LocusDiff);
        writer.Write(particle.NumDivision);
        writer.Write(particle.LocusUVType);
        writer.Write(particle.IsBillboard);
        Common.BoolPadding(writer);
        writer.Write(particle.IsEmitterLocal);
        Common.BoolPadding(writer);
        
        writer.Write(particle.LayerType);
        writer.Write(particle.PivotType);
        writer.Write(particle.UVDescType);
        
        writer.Write(particle.TextureIndexType);
        writer.Write(particle.TextureIndexChangeInterval);
        writer.Write(particle.TextureIndexChangeIntervalBias);
        writer.Write(particle.InitialTextureIndex);
        
        writer.Write(particle.DirectionType);
        writer.Write(particle.ParticleDataFlags);
        
        writer.Write(particle.Color.A);
        writer.Write(particle.Color.R);
        writer.Write(particle.Color.G);
        writer.Write(particle.Color.B);
        
        SparkleFunctions.VectorWriteBin(writer ,particle.Gravity);
        SparkleFunctions.VectorWriteBin(writer ,particle.ExternalForce);
        SparkleFunctions.VectorWriteBin(writer ,particle.InitialDirection);
        SparkleFunctions.VectorWriteBin(writer ,particle.InitialDirectionBias);
        SparkleFunctions.VectorWriteBin(writer ,particle.InitialScale);
        SparkleFunctions.VectorWriteBin(writer ,particle.InitialScaleBias);
        
        writer.Write(particle.MeshName);
        Common.WritePadding(writer);
        
        SparkleFunctions.VectorWriteBin(writer ,particle.RotationXYZ);
        SparkleFunctions.VectorWriteBin(writer ,particle.RotationXYZBias);
        SparkleFunctions.VectorWriteBin(writer ,particle.InitialRotationXYZ);
        SparkleFunctions.VectorWriteBin(writer ,particle.InitialRotationXYZBias);
        SparkleFunctions.VectorWriteBin(writer, particle.UVScrollParam);
        SparkleFunctions.VectorWriteBin(writer ,particle.UVScrollParamAlpha);
        
        writer.Write(particle.RefEffectName);
        Common.WritePadding(writer);
        writer.Write(particle.RefEffectEmitTimingType);
        writer.Write(particle.RefEffectDelayTime);
        
        writer.Write(particle.DirectionalVelocityRatio);
        writer.Write(particle.DeflectionScale);
        writer.Write(particle.SoftScale);
        writer.Write(particle.VelocityOffset);
        writer.Write(particle.UserData);
        writer.Write(particle.MaterialName);
        Common.WritePadding(writer);
        
        writer.Write(particle.ukn0);
        writer.Write(particle.ukn1);
        writer.Write(particle.ukn2);
        writer.Write(particle.ukn3);
        
        writer.Write(particle.AnimCount);
        if (particle.AnimCount > 0)
        {
            Animation.WriteBin(writer, particle.ParticleAnim);
        }

    }

    // XML Functions
    public static Particle ReadXml(XmlElement node)
    {
        Console.WriteLine("Reading Particle");
        Particle particle = new Particle();

        particle.ParticleName = node.GetElementsByTagName("ParticleName")[0].InnerText;
        particle.LifeTime = float.Parse(node.GetElementsByTagName("LifeTime")[0].InnerText);
        particle.LifeTimeBias = float.Parse(node.GetElementsByTagName("LifeTimeBias")[0].InnerText);
        
        particle.RotationZ = float.Parse(node.GetElementsByTagName("RotationZ")[0].InnerText);
        particle.RotationZBias = float.Parse(node.GetElementsByTagName("RotationZBias")[0].InnerText);
        particle.InitialRotationZ = float.Parse(node.GetElementsByTagName("InitialRotationZ")[0].InnerText);
        particle.InitialRotationZBias = float.Parse(node.GetElementsByTagName("InitialRotationZBias")[0].InnerText);
        
        particle.InitialSpeed = float.Parse(node.GetElementsByTagName("InitialSpeed")[0].InnerText);
        particle.InitialSpeedBias = float.Parse(node.GetElementsByTagName("InitialSpeedBias")[0].InnerText);
        
        particle.ZOffset = float.Parse(node.GetElementsByTagName("ZOffset")[0].InnerText);
        particle.LocusDiff = float.Parse(node.GetElementsByTagName("LocusDiff")[0].InnerText);
        particle.NumDivision = int.Parse(node.GetElementsByTagName("NumDivision")[0].InnerText);
        
        particle.LocusUVType = (int)Enum.Parse(typeof(LocusUVTypes), node.GetElementsByTagName("LocusUVType")[0].InnerText);
        particle.IsBillboard = bool.Parse(node.GetElementsByTagName("IsBillboard")[0].InnerText);
        particle.IsEmitterLocal = bool.Parse(node.GetElementsByTagName("IsEmitterLocal")[0].InnerText);
        
        particle.LayerType = (int)Enum.Parse(typeof(LayerTypes), node.GetElementsByTagName("LayerType")[0].InnerText);
        particle.PivotType = (int)Enum.Parse(typeof(PivotTypes), node.GetElementsByTagName("PivotType")[0].InnerText);
        particle.UVDescType = (int)Enum.Parse(typeof(UVDescTypes), node.GetElementsByTagName("UVDescType")[0].InnerText);
        
        particle.TextureIndexType = (int)Enum.Parse(typeof(TextureIndexTypes), node.GetElementsByTagName("TextureIndexType")[0].InnerText);
        particle.TextureIndexChangeInterval = int.Parse(node.GetElementsByTagName("TextureIndexChangeInterval")[0].InnerText);
        particle.TextureIndexChangeIntervalBias = int.Parse(node.GetElementsByTagName("TextureIndexChangeIntervalBias")[0].InnerText);
        
        particle.InitialTextureIndex = int.Parse(node.GetElementsByTagName("InitialTextureIndex")[0].InnerText);
        particle.DirectionType = (int)Enum.Parse(typeof(DirectionTypes), node.GetElementsByTagName("DirectionType")[0].InnerText);
        particle.ParticleDataFlags = int.Parse(node.GetElementsByTagName("ParticleDataFlags")[0].InnerText);
       
        particle.Color = new Colour
        {
            A = int.Parse(node.GetElementsByTagName("Color")[0].ChildNodes[0].InnerText),
            R = int.Parse(node.GetElementsByTagName("Color")[0].ChildNodes[1].InnerText),
            G = int.Parse(node.GetElementsByTagName("Color")[0].ChildNodes[2].InnerText),
            B = int.Parse(node.GetElementsByTagName("Color")[0].ChildNodes[3].InnerText),
        };
        
        particle.Gravity = SparkleFunctions.VectorReadXml(node.GetElementsByTagName("Gravity")[0], false);
        particle.ExternalForce = SparkleFunctions.VectorReadXml(node.GetElementsByTagName("ExternalForce")[0], false);
        particle.InitialDirection = SparkleFunctions.VectorReadXml(node.GetElementsByTagName("InitialDirection")[0], false);
        particle.InitialDirectionBias = SparkleFunctions.VectorReadXml(node.GetElementsByTagName("InitialDirectionBias")[0], false);
        particle.InitialScale = SparkleFunctions.VectorReadXml(node.GetElementsByTagName("InitialScale")[0], false);
        particle.InitialScaleBias = SparkleFunctions.VectorReadXml(node.GetElementsByTagName("InitialScaleBias")[0], false);

        particle.MeshName = node.GetElementsByTagName("MeshName")[0].InnerText;
        
        particle.RotationXYZ = SparkleFunctions.VectorReadXml(node.GetElementsByTagName("RotationXYZ")[0], false);
        particle.RotationXYZBias = SparkleFunctions.VectorReadXml(node.GetElementsByTagName("RotationXYZBias")[0], false);
        particle.InitialRotationXYZ = SparkleFunctions.VectorReadXml(node.GetElementsByTagName("InitialRotationXYZ")[0], false);
        particle.InitialRotationXYZBias = SparkleFunctions.VectorReadXml(node.GetElementsByTagName("InitialRotationXYZBias")[0], false);
        particle.UVScrollParam = SparkleFunctions.VectorReadXml(node.GetElementsByTagName("UVScrollParam")[0], false);
        particle.UVScrollParamAlpha = SparkleFunctions.VectorReadXml(node.GetElementsByTagName("UVScrollParamAlpha")[0], false);
        
        particle.RefEffectName = node.GetElementsByTagName("RefEffectName")[0].InnerText;
        particle.RefEffectEmitTimingType = (int)Enum.Parse(typeof(RefEffectEmitTimingTypes), node.GetElementsByTagName("RefEffectEmitTimingType")[0].InnerText);
        particle.RefEffectDelayTime = float.Parse(node.GetElementsByTagName("RefEffectDelayTime")[0].InnerText);
        
        particle.DirectionalVelocityRatio = float.Parse(node.GetElementsByTagName("DirectionalVelocityRatio")[0].InnerText);
        particle.DeflectionScale = float.Parse(node.GetElementsByTagName("DeflectionScale")[0].InnerText);
        particle.SoftScale = float.Parse(node.GetElementsByTagName("SoftScale")[0].InnerText);
        particle.VelocityOffset = float.Parse(node.GetElementsByTagName("VelocityOffset")[0].InnerText);
        particle.UserData = float.Parse(node.GetElementsByTagName("UserData")[0].InnerText);

        if (node.ChildNodes[47].Name == "Animation")
        {
            particle.AnimCount += 1;
            particle.ParticleAnim = Animation.ReadXml(node.ChildNodes[47]);
        }
        
        particle.MaterialName = node.GetElementsByTagName("MaterialName")[0].InnerText;
        
        particle.ukn0 = int.Parse(node.GetElementsByTagName("Ukn0")[0].InnerText);
        particle.ukn1 = int.Parse(node.GetElementsByTagName("Ukn1")[0].InnerText);
        particle.ukn2 = int.Parse(node.GetElementsByTagName("Ukn2")[0].InnerText);
        particle.ukn3 = int.Parse(node.GetElementsByTagName("Ukn3")[0].InnerText);
        
        return particle;
    }
    
    public static void WriteXml(XmlWriter writer, Particle particle)
    {
        writer.WriteElementString("ParticleName", particle.ParticleName);
        writer.WriteElementString("LifeTime", particle.LifeTime.ToString());
        writer.WriteElementString("LifeTimeBias", particle.LifeTimeBias.ToString());
        
        writer.WriteElementString("RotationZ", particle.RotationZ.ToString());
        writer.WriteElementString("RotationZBias", particle.RotationZBias.ToString());
        writer.WriteElementString("InitialRotationZ", particle.InitialRotationZ.ToString());
        writer.WriteElementString("InitialRotationZBias", particle.InitialRotationZBias.ToString());
        
        writer.WriteElementString("InitialSpeed", particle.InitialSpeed.ToString());
        writer.WriteElementString("InitialSpeedBias", particle.InitialSpeedBias.ToString());
        
        writer.WriteElementString("ZOffset", particle.ZOffset.ToString());
        writer.WriteElementString("LocusDiff", particle.LocusDiff.ToString());
        writer.WriteElementString("NumDivision", particle.NumDivision.ToString());
        writer.WriteElementString("LocusUVType", ((LocusUVTypes)particle.LocusUVType).ToString());
        
        writer.WriteElementString("IsBillboard", particle.IsBillboard.ToString().ToLower());
        writer.WriteElementString("IsEmitterLocal", particle.IsEmitterLocal.ToString().ToLower());
        
        writer.WriteElementString("LayerType", ((LayerTypes)particle.LayerType).ToString());
        writer.WriteElementString("PivotType", ((PivotTypes)particle.PivotType).ToString());
        writer.WriteElementString("UVDescType", ((UVDescTypes)particle.UVDescType).ToString());
        
        writer.WriteElementString("TextureIndexType", ((TextureIndexTypes)particle.TextureIndexType).ToString());
        writer.WriteElementString("TextureIndexChangeInterval", particle.TextureIndexChangeInterval.ToString());
        writer.WriteElementString("TextureIndexChangeIntervalBias", particle.TextureIndexChangeIntervalBias.ToString());
        
        writer.WriteElementString("InitialTextureIndex", particle.InitialTextureIndex.ToString());
        writer.WriteElementString("DirectionType", ((DirectionTypes)particle.DirectionType).ToString());
        writer.WriteElementString("ParticleDataFlags", particle.ParticleDataFlags.ToString());
        
        writer.WriteStartElement("Color");
            writer.WriteElementString("A", particle.Color.A.ToString());
            writer.WriteElementString("R", particle.Color.R.ToString());
            writer.WriteElementString("G", particle.Color.G.ToString());
            writer.WriteElementString("B", particle.Color.B.ToString());
        writer.WriteEndElement();
        
        SparkleFunctions.VectorWriteXml(writer, "Gravity", particle.Gravity, false);
        SparkleFunctions.VectorWriteXml(writer, "ExternalForce", particle.ExternalForce, false);
        SparkleFunctions.VectorWriteXml(writer, "InitialDirection", particle.InitialDirection, false);
        SparkleFunctions.VectorWriteXml(writer, "InitialDirectionBias", particle.InitialDirectionBias, false);
        SparkleFunctions.VectorWriteXml(writer, "InitialScale", particle.InitialScale, false);
        SparkleFunctions.VectorWriteXml(writer, "InitialScaleBias", particle.InitialScaleBias, false);
        
        writer.WriteElementString("MeshName", particle.MeshName);
        
        SparkleFunctions.VectorWriteXml(writer, "RotationXYZ", particle.RotationXYZ, false);
        SparkleFunctions.VectorWriteXml(writer, "RotationXYZBias", particle.RotationXYZBias, false);
        SparkleFunctions.VectorWriteXml(writer, "InitialRotationXYZ", particle.InitialRotationXYZ, false);
        SparkleFunctions.VectorWriteXml(writer, "InitialRotationXYZBias", particle.InitialRotationXYZBias, false);
        SparkleFunctions.VectorWriteXml(writer, "UVScrollParam", particle.UVScrollParam, false);
        SparkleFunctions.VectorWriteXml(writer, "UVScrollParamAlpha", particle.UVScrollParamAlpha, false);
        
        writer.WriteElementString("RefEffectName", particle.RefEffectName);
        writer.WriteElementString("RefEffectEmitTimingType", ((RefEffectEmitTimingTypes)particle.RefEffectEmitTimingType).ToString());
        writer.WriteElementString("RefEffectDelayTime", particle.RefEffectDelayTime.ToString());
        
        writer.WriteElementString("DirectionalVelocityRatio", particle.DirectionalVelocityRatio.ToString());
        writer.WriteElementString("DeflectionScale", particle.DeflectionScale.ToString());
        writer.WriteElementString("SoftScale", particle.SoftScale.ToString());
        writer.WriteElementString("VelocityOffset", particle.VelocityOffset.ToString());
        writer.WriteElementString("UserData", particle.UserData.ToString());
        
        if (particle.AnimCount > 0)
        {
            Animation.WriteXml(writer, particle.ParticleAnim);
        }

        writer.WriteElementString("MaterialName", particle.MaterialName);
        
        writer.WriteElementString("Ukn0", particle.ukn0.ToString());
        writer.WriteElementString("Ukn1", particle.ukn1.ToString());
        writer.WriteElementString("Ukn2", particle.ukn2.ToString());
        writer.WriteElementString("Ukn3", particle.ukn3.ToString());
    }
}