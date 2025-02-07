using System.Numerics;
using System.Xml;

namespace SU_Particle_Tool;

public class CEffectFunctions
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

    static void AnimReadBin(BinaryReader binaryReader, ref CEffectStructs.Animation animation)
    {
        animation.CurveType = binaryReader.ReadInt32();
        animation.KeyCount = binaryReader.ReadInt32();
        animation.UField0 = binaryReader.ReadInt32();

        if (animation.KeyCount > 0)
        {
            animation.KeyFrames = new List<CEffectStructs.KeyFrame>();
            for (int i = 0; i < animation.KeyCount; i++)
            {
                CEffectStructs.KeyFrame keyFrame = new CEffectStructs.KeyFrame
                {
                    Time = binaryReader.ReadSingle(),
                    Value = binaryReader.ReadSingle(),
                    ValueUpperBias = binaryReader.ReadSingle(),
                    ValueLowerBias = binaryReader.ReadSingle(),
                    SlopeL = binaryReader.ReadSingle(),
                    SlopeR = binaryReader.ReadSingle(),
                    SlopeLUpperBias = binaryReader.ReadSingle(),
                    SlopeLLowerBias = binaryReader.ReadSingle(),
                    SlopeRUpperBias = binaryReader.ReadSingle(),
                    SlopeRLowerBias = binaryReader.ReadSingle(),
                    KeyBreak = Common.ReadBool(binaryReader)
                };
                animation.KeyFrames.Add(keyFrame);
            }
        }
    }

    static void AnimsReadBin(BinaryReader binaryReader, ref CEffectStructs.Animations animations)
    {
        animations.TypeName = Common.ReadName(binaryReader);
        AnimReadBin(binaryReader, ref animations.ColorA);
        AnimReadBin(binaryReader, ref animations.ColorB);
        AnimReadBin(binaryReader, ref animations.ColorG);
        AnimReadBin(binaryReader, ref animations.ColorR);
        AnimReadBin(binaryReader, ref animations.TransX);
        AnimReadBin(binaryReader, ref animations.TransY);
        AnimReadBin(binaryReader, ref animations.TransZ);
        AnimReadBin(binaryReader, ref animations.ScaleX);
        AnimReadBin(binaryReader, ref animations.ScaleY);
        AnimReadBin(binaryReader, ref animations.ScaleZ);
        Console.WriteLine("Anim Read");
    }

    static void ParticleReadBin(BinaryReader binaryReader, ref CEffectStructs.ParticleSaveLoad particleSaveLoad)
    {
        particleSaveLoad = new CEffectStructs.ParticleSaveLoad {
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
            
            A = binaryReader.ReadInt32(),
            R = binaryReader.ReadInt32(),
            G = binaryReader.ReadInt32(),
            B = binaryReader.ReadInt32(),
            
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
            
            MaterialName = Common.ReadName(binaryReader)
        };
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
            Console.WriteLine("\nEmitterSaveLoad");
            CEffectStructs.EmitterSaveLoad emitterSaveLoad = new CEffectStructs.EmitterSaveLoad();
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
            
            emitterSaveLoad.AnimCount = binaryReader.ReadInt32();

            if (emitterSaveLoad.AnimCount > 0)
            {
                emitterSaveLoad.EmitterAnim = new CEffectStructs.Animations();
                AnimsReadBin(binaryReader, ref emitterSaveLoad.EmitterAnim);
            }

            if (emitterSaveLoad.ParticleCount > 0)
            {
                emitterSaveLoad.ParticleSaveLoad = new List<CEffectStructs.ParticleSaveLoad>();
            
                for (int p = 0; p < emitterSaveLoad.ParticleCount; p++)
                {
                    CEffectStructs.ParticleSaveLoad particleSaveLoad = new CEffectStructs.ParticleSaveLoad();

                    particleSaveLoad.Type = Common.ReadName(binaryReader);

                    if (particleSaveLoad.Type == "ParticleChunk")
                    {
                        ParticleReadBin(binaryReader, ref particleSaveLoad);
                    
                        for (int j = 0; j < 4; j++)
                        {
                            Console.Write(System.Text.Encoding.UTF8.GetString(binaryReader.ReadBytes(1)));
                            binaryReader.BaseStream.Position += 3;
                        }
                
                        particleSaveLoad.AnimCount = binaryReader.ReadInt32();

                        if (particleSaveLoad.AnimCount > 0)
                        {
                            particleSaveLoad.ParticleAnim = new CEffectStructs.Animations();
                            AnimsReadBin(binaryReader, ref particleSaveLoad.ParticleAnim);
                        }
                    
                        emitterSaveLoad.ParticleSaveLoad.Add(particleSaveLoad);
                    }
                }
            }

            Console.WriteLine("\nEnd");
            emitterSaveLoadList.Add(emitterSaveLoad);
        }

        effect.Emitters = emitterSaveLoadList;
        
        return effect;
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

    static void EmitterWriteXml(XmlWriter writer, CEffectStructs.EmitterSaveLoad emitter)
    {
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
        writer.WriteElementString("EmitCondition", ((CEffectStructs.EmitConditions)emitter.EmitCondition).ToString());
        writer.WriteElementString("EmitterType", ((CEffectStructs.EmitterTypes)emitter.EmitterType).ToString());
        
        writer.WriteStartElement("CylinderParams");
            writer.WriteElementString("m_equiangularly", emitter.CylinderParams.m_equiangularly.ToString().ToLower());
            writer.WriteElementString("m_circumference", emitter.CylinderParams.m_circumference.ToString().ToLower());
            writer.WriteElementString("m_isCone", emitter.CylinderParams.m_isCone.ToString().ToLower());
            writer.WriteElementString("m_angle", emitter.CylinderParams.m_angle.ToString());
            writer.WriteElementString("m_radius", emitter.CylinderParams.m_radius.ToString());
            writer.WriteElementString("m_height", emitter.CylinderParams.m_height.ToString());
            writer.WriteElementString("m_minAngle", emitter.CylinderParams.m_minAngle.ToString());
            writer.WriteElementString("m_maxAngle", emitter.CylinderParams.m_maxAngle.ToString());
            writer.WriteElementString("m_cylinderEmittionType", ((CEffectStructs.cylinderEmittionTypes)emitter.CylinderParams.m_cylinderEmittionType).ToString());
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
    }
    
    static void AnimsWriteXml(XmlWriter writer, CEffectStructs.Animations Anim) 
    {
        writer.WriteStartElement("Animation");
        AnimElementXml(writer, "TransX", Anim.TransX);
        AnimElementXml(writer, "TransY", Anim.TransY);
        AnimElementXml(writer, "TransZ", Anim.TransZ);

        AnimElementXml(writer, "ScaleX", Anim.ScaleX);
        AnimElementXml(writer, "ScaleY", Anim.ScaleY);
        AnimElementXml(writer, "ScaleZ", Anim.ScaleZ);

        AnimElementXml(writer, "ColorR", Anim.ColorR);
        AnimElementXml(writer, "ColorG", Anim.ColorG);
        AnimElementXml(writer, "ColorB", Anim.ColorB);
        AnimElementXml(writer, "ColorA", Anim.ColorA);
        writer.WriteEndElement();
    }

    static void ParticleWriteXml(XmlWriter writer, CEffectStructs.ParticleSaveLoad particle)
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
        writer.WriteElementString("LocusUVType", ((CEffectStructs.LocusUVTypes)particle.LocusUVType).ToString());
        
        writer.WriteElementString("IsBillboard", particle.IsBillboard.ToString().ToLower());
        writer.WriteElementString("IsEmitterLocal", particle.IsEmitterLocal.ToString().ToLower());
        
        writer.WriteElementString("LayerType", ((CEffectStructs.LayerTypes)particle.LayerType).ToString());
        writer.WriteElementString("PivotType", ((CEffectStructs.PivotTypes)particle.PivotType).ToString());
        writer.WriteElementString("UVDescType", ((CEffectStructs.UVDescTypes)particle.UVDescType).ToString());
        
        writer.WriteElementString("TextureIndexType", ((CEffectStructs.TextureIndexTypes)particle.TextureIndexType).ToString());
        writer.WriteElementString("TextureIndexChangeInterval", particle.TextureIndexChangeInterval.ToString());
        writer.WriteElementString("TextureIndexChangeIntervalBias", particle.TextureIndexChangeIntervalBias.ToString());
        
        writer.WriteElementString("InitialTextureIndex", particle.InitialTextureIndex.ToString());
        writer.WriteElementString("DirectionType", ((CEffectStructs.DirectionTypes)particle.DirectionType).ToString());
        writer.WriteElementString("ParticleDataFlags", particle.ParticleDataFlags.ToString());
        
        writer.WriteStartElement("Color");
            writer.WriteElementString("A", particle.A.ToString());
            writer.WriteElementString("R", particle.R.ToString());
            writer.WriteElementString("G", particle.G.ToString());
            writer.WriteElementString("B", particle.B.ToString());
        writer.WriteEndElement();
        
        VectorWriteXml(writer, "Gravity", particle.Gravity, false);
        VectorWriteXml(writer, "ExternalForce", particle.ExternalForce, false);
        VectorWriteXml(writer, "InitialDirection", particle.InitialDirection, false);
        VectorWriteXml(writer, "InitialDirectionBias", particle.InitialDirectionBias, false);
        VectorWriteXml(writer, "InitialScale", particle.InitialScale, false);
        VectorWriteXml(writer, "InitialScaleBias", particle.InitialScaleBias, false);
        
        writer.WriteElementString("MeshName", particle.MeshName);
        
        VectorWriteXml(writer, "RotationXYZ", particle.RotationXYZ, false);
        VectorWriteXml(writer, "RotationXYZBias", particle.RotationXYZBias, false);
        VectorWriteXml(writer, "InitialRotationXYZ", particle.InitialRotationXYZ, false);
        VectorWriteXml(writer, "InitialRotationXYZBias", particle.InitialRotationXYZBias, false);
        VectorWriteXml(writer, "UVScrollParam", particle.UVScrollParam, false);
        VectorWriteXml(writer, "UVScrollParamAlpha", particle.UVScrollParamAlpha, false);
        
        writer.WriteElementString("RefEffectName", particle.RefEffectName);
        writer.WriteElementString("RefEffectEmitTimingType", ((CEffectStructs.RefEffectEmitTimingTypes)particle.RefEffectEmitTimingType).ToString());
        writer.WriteElementString("RefEffectDelayTime", particle.RefEffectDelayTime.ToString());
        
        writer.WriteElementString("DirectionalVelocityRatio", particle.DirectionalVelocityRatio.ToString());
        writer.WriteElementString("DeflectionScale", particle.DeflectionScale.ToString());
        writer.WriteElementString("SoftScale", particle.SoftScale.ToString());
        writer.WriteElementString("VelocityOffset", particle.VelocityOffset.ToString());
        writer.WriteElementString("UserData", particle.UserData.ToString());
    }

    static void AnimElementXml(XmlWriter writer, string field, CEffectStructs.Animation animation)
    {
        writer.WriteStartElement(field);
            writer.WriteElementString("CurveType", ((CEffectStructs.CurveTypes)animation.CurveType).ToString());
            writer.WriteStartElement("KeyFrameList");
            if (animation.KeyCount > 0)
            {
                foreach (var KeyFrame in animation.KeyFrames.ToList())
                {
                    writer.WriteStartElement("KeyFrame");
                    writer.WriteElementString("Time", KeyFrame.Time.ToString());
                    writer.WriteElementString("Value", KeyFrame.Value.ToString());
                    writer.WriteElementString("ValueUpperBias", KeyFrame.ValueUpperBias.ToString());
                    writer.WriteElementString("ValueLowerBias", KeyFrame.ValueLowerBias.ToString());
                    writer.WriteElementString("SlopeL", KeyFrame.SlopeL.ToString());
                    writer.WriteElementString("SlopeR", KeyFrame.SlopeR.ToString());
                    writer.WriteElementString("SlopeLUpperBias", KeyFrame.SlopeLUpperBias.ToString());
                    writer.WriteElementString("SlopeLLowerBias", KeyFrame.SlopeLLowerBias.ToString());
                    writer.WriteElementString("SlopeRUpperBias", KeyFrame.SlopeRUpperBias.ToString());
                    writer.WriteElementString("SlopeRLowerBias", KeyFrame.SlopeRLowerBias.ToString());
                    writer.WriteElementString("KeyBreak", KeyFrame.KeyBreak.ToString().ToLower());
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
        writer.WriteEndElement();
    }

    public static void CEffectWriteXml(XmlWriter writer, Structs.SparkleCEffect effect)
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
                
                VectorWriteXml(writer, "InitialPosition", effect.InitialPosition, false);
                VectorWriteXml(writer, "InitialRotation", effect.InitialRotation, true);
                
                writer.WriteElementString("IsLoop", effect.IsLoop.ToString().ToLower());
                writer.WriteElementString("DeleteChildren", effect.DeleteChildren.ToString().ToLower());
                writer.WriteElementString("VelocityOffset", effect.VelocityOffset.ToString());

                writer.WriteStartElement("EmitterSaveLoadList");
                foreach (var emitter in effect.Emitters)
                {
                    writer.WriteStartElement("EmitterSaveLoad");
                    SparkleFunctions.WriteExportXml(writer);
                    EmitterWriteXml(writer, emitter);
                    
                    if (emitter.AnimCount > 0)
                    {
                        AnimsWriteXml(writer, emitter.EmitterAnim);
                    }

                    writer.WriteStartElement("ParticleSaveLoadList");
                    foreach (var particle in emitter.ParticleSaveLoad)
                    {
                        writer.WriteStartElement("ParticleSaveLoad");
                        SparkleFunctions.WriteExportXml(writer);
                        ParticleWriteXml(writer, particle);

                        if (particle.AnimCount > 0)
                        {
                            AnimsWriteXml(writer, particle.ParticleAnim);
                        }

                        writer.WriteElementString("MaterialName", particle.MaterialName);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            
            writer.WriteEndElement();
        
        writer.WriteEndElement();
    }
}