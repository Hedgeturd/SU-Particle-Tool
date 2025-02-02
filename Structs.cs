using System.Numerics;

namespace SU_Particle_Tool;

public class Structs
{
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
        public List<EmitterSaveLoad> Emitters;
    }

    public struct EmitterSaveLoad
    {
        public string Type;
        public int ParticleCount;
        public string EmitterName;
        public int MaxGenerateCount;
        public int GenerateCount;
        public int ParticleDataFlags;
        public bool Infinite;
        public float InitialEmittionGap;
        
        public Vector4 InitialPosition;
        public Vector4 RotationXYZ;
        public Vector4 RotationXYZBias;
        public Vector4 InitialRotationXYZ;
        public Vector4 InitialRotation;
        
        public float InitialEmitterLifeTime;
        public float EmitStartTime;
        public int EmitCondition;
        public int EmitterType;
        
        public CylinderParams CylinderParams;
        public float m_latitude_max_angle ;
        public float m_longitude_max_angle;
        public Vector4 m_size;
        public string MeshName;
    }

    public struct CylinderParams
    {
        public bool m_equiangularly ;
        public bool m_circumference;
        public bool m_isCone;
        public float m_angle;
        public float m_radius;
        public float m_height;
        public float m_minAngle;
        public float m_maxAngle;
        public int m_cylinderEmittionType;
    }

    public struct ParticleSaveLoad
    {
        public string Type;
        public string ParticleName;
        public float LifeTime;
        public float LifeTimeBias;

        public float RotationZ;
        public float RotationZBias;
        public float InitialRotationZ;
        public float InitialRotationZBias;
        
        public float InitialSpeed ;
        public float InitialSpeedBias;
        
        public bool IsBillboard;
        public bool IsEmitterLocal;
        
        public int LayerType;
        public int UVDescType;
        public int TextureIndexType;
        public int TextureIndexChangeInterval;
        public int TextureIndexChangeIntervalBias;
        public int InitialTextureIndex;
        public int DirectionType;
        public int ParticleDataFlags;

        public int A, R, G, B;
        
        public Vector4 Gravity;
        public Vector4 ExternalForce;
        
        public Vector4 InitialDirection;
        public Vector4 InitialDirectionBias;
        
        public Vector4 InitialScale;
        public Vector4 InitialScaleBias;
        
        public string MeshName;
        
        public Vector4 RotationXYZ;
        public Vector4 RotationXYZBias;
        public Vector4 InitialRotationXYZ;
        public Vector4 InitialRotationXYZBias;
        
        public string RefEffectName;
        
        public int RefEffectEmitTimingType;
        public int RefEffectDelayTime;
        
        
        
        public string MaterialName;
    }

    public struct Animation
    {
        
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