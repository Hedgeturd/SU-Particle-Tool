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
            
            emitterSaveLoad.AnimCount = binaryReader.ReadInt32();

            if (emitterSaveLoad.AnimCount > 0)
            {
                // Hi :D
                
                // Gonna take this all to a separate method
                
                /*emitterSaveLoad.EmitterAnim = new CEffectStructs.Animations();
                emitterSaveLoad.EmitterAnim.TypeName = Common.ReadName(binaryReader);

                for (int j = 0; j < UPPER; j++)
                {
                    emitterSaveLoad.EmitterAnim.ColorA.CurveType = binaryReader.ReadInt32();
                    emitterSaveLoad.EmitterAnim.ColorA.KeyCount = binaryReader.ReadInt32();
                    emitterSaveLoad.EmitterAnim.ColorA.UField0 = binaryReader.ReadInt32();
                }*/
            }

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
}