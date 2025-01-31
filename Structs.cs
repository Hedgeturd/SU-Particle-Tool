namespace SU_Particle_Tool;

public class Structs
{
    public struct SparkleFile
    {
        public SparkleHeader Header { get; set; }
        public SparkleCEffect CEffect { get; set; }
        public SparkleMaterial Material { get; set; }
    }

    public struct SparkleHeader
    {
        public string editor { get; set; }
        public string type { get; set; }
        public long exportDate { get; set; }
        public int version { get; set; }
    }

    public struct SparkleCEffect
    {
        
    }
    
    public struct SparkleMaterial
    {
        public string materialName { get; set; }
        public string shaderName { get; set; }
        public string textureName { get; set; }
        public string deflectionTextureName { get; set; }
        
        public int addressMode { get; set; } 
        public int blendMode { get; set; }
    }
}