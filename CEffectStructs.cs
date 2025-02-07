using System.Numerics;

namespace SU_Particle_Tool;

public class CEffectStructs
{
    // Emitter Enums
    public enum EmitterTypes
    {
        Box,
        Cylinder,
        Mesh,
        Sphere
    }

    public enum EmitConditions
    {
        Time
    }
    public enum cylinderEmittionTypes
    {
        ECylinder_UserVelocity
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

    // Emitters
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

        public int AnimCount;
        public Animations EmitterAnim;
        
        public List<ParticleSaveLoad> ParticleSaveLoad;
    }
    
    // Particle Enums
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
    
    // Particles
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
        
        public float ZOffset;
        public float LocusDiff;

        public int NumDivision;
        public int LocusUVType;
        
        public bool IsBillboard;
        public bool IsEmitterLocal;
        
        public int LayerType;
        public int PivotType;
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

        public Vector4 UVScrollParam;
        public Vector4 UVScrollParamAlpha;
        
        public string RefEffectName;    
        public int RefEffectEmitTimingType;
        public float RefEffectDelayTime;

        public float DirectionalVelocityRatio;
        public float DeflectionScale;
        public float SoftScale;
        public float VelocityOffset;
        public float UserData;
        
        public string MaterialName;

        public int AnimCount;
        public Animations ParticleAnim;
    }

    // Animations
    public struct Animations
    {
        public string TypeName;
        
        public Animation ColorA;
        public Animation ColorB;
        public Animation ColorG;
        public Animation ColorR;
        
        public Animation TransX;
        public Animation TransY;
        public Animation TransZ;
        
        public Animation ScaleX;
        public Animation ScaleY;
        public Animation ScaleZ;
    }
    
    // Animation Structs and Enums
    public enum CurveTypes
    {
        LINEAR,
        SPLINE,
        CONSTANT,
        ROTATE
    }
    public struct Animation
    {
        public int CurveType;
        public int KeyCount;
        public int UField0;
        public List<KeyFrame> KeyFrames;
    }
    public struct KeyFrame
    {
        public float Time;
        public float Value;
        public float ValueUpperBias;
        public float ValueLowerBias;
        public float SlopeL;
        public float SlopeR;
        public float SlopeLUpperBias;
        public float SlopeLLowerBias;
        public float SlopeRUpperBias;
        public float SlopeRLowerBias;
        public bool KeyBreak;
    }
}