using System.Numerics;

namespace SU_Particle_Tool;

public class Structs
{
    // Sparkle
    public struct SparkleFile
    {
        public SparkleHeader Header;
        public SparkleCEffect CEffect;
        public SparkleMaterial Material;
    }
    public struct SparkleHeader
    {
        public string Editor;
        public string Type;
        public int EmitterCount;
        public int ParticleCount;
        public long ExportDate;
        public int Version;
    }
    public struct SparkleCEffect
    {
        public string EffectName;
        public float InitialLifeTime;
        public float ScaleRatio;
        public float GenerateCountRatio ;
        public Vector4 InitialPosition;
        public Vector4 InitialRotation;
        public bool IsLoop;
        public bool DeleteChildren;
        public float VelocityOffset;
        public List<CEffectStructs.EmitterSaveLoad> Emitters;
    }
    
    // Materials
    public enum Address {
        CLAMP,
        WRAP
    }
    public enum Blends {
        Typical = 1,
        Add = 2
    }
    public struct SparkleMaterial
    {
        public string materialName;
        public string shaderName;
        public string textureName;
        public string deflectionTextureName;
        
        public int addressMode; 
        public int blendMode;
    }
}