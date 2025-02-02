﻿namespace SU_Particle_Tool;

public class SparkleBin
{
    public static Structs.SparkleFile SparkleFile = new Structs.SparkleFile();
    public static void ReadBin(string path)
    {
        // K1 Sparkles
        FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        BinaryReader binaryReader = new BinaryReader(fileStream);
        Structs.SparkleHeader SparkleIn = new Structs.SparkleHeader();
        
        SparkleIn.Editor = Common.ReadName(binaryReader);
        Console.WriteLine(SparkleIn.Editor);
        SparkleIn.Type = Common.ReadName(binaryReader);
        Console.WriteLine(SparkleIn.Type);

        if (SparkleIn.Type == "CEffect")
        {
            SparkleIn.EmitterCount = binaryReader.ReadInt32();
            Console.WriteLine(SparkleIn.EmitterCount);
            SparkleIn.ParticleCount = binaryReader.ReadInt32();
            Console.WriteLine(SparkleIn.ParticleCount);
        }
        
        Console.WriteLine("\nExportInfo");
        SparkleIn.ExportDate = binaryReader.ReadInt64();
        Console.WriteLine(SparkleIn.ExportDate);
        SparkleIn.Version = binaryReader.ReadInt32();
        Console.WriteLine(SparkleIn.Version);

        switch (SparkleIn.Type)
        {
            case "CEffect":
                Console.WriteLine("InportExportEffect");
                Structs.SparkleCEffect CEffectIn = new Structs.SparkleCEffect();
                CEffectIn = SparkleFunctions.CEffectReadBin(binaryReader, SparkleIn, CEffectIn);
                SparkleFile.CEffect = CEffectIn;
                break;
            case "Material":
                Console.WriteLine("InportExportMaterial");
                Structs.SparkleMaterial MaterialIn = new Structs.SparkleMaterial();
                MaterialIn = SparkleFunctions.MaterialReadBin(binaryReader, SparkleIn, MaterialIn);
                SparkleFile.Material = MaterialIn;
                break;
            default:
                Console.WriteLine("Unknown Type");
                break;
        }
        
        SparkleFile.Header = SparkleIn;
    }
    
    public static void WriteBin()
    {
        // K1 Sparkles
        
    }
}