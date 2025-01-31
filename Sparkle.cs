using System.Drawing;

namespace SU_Particle_Tool;

public class Sparkle
{
    public static Structs.SparkleFile sparkleFile = new Structs.SparkleFile();
    public static void SparkleReadBin(string path)
    {
        // K1 Sparkles
        FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        BinaryReader binaryReader = new BinaryReader(fileStream);
        Structs.SparkleHeader SparkleIn = new Structs.SparkleHeader();
        
        SparkleIn.editor = Common.ReadName(binaryReader);
        Console.WriteLine(SparkleIn.editor);
        SparkleIn.type = Common.ReadName(binaryReader);
        Console.WriteLine(SparkleIn.type);
        
        Console.WriteLine("\nExportInfo");
        SparkleIn.exportDate = binaryReader.ReadInt64();
        Console.WriteLine(SparkleIn.exportDate);
        SparkleIn.version = binaryReader.ReadInt32();
        Console.WriteLine(SparkleIn.version);

        switch (SparkleIn.type)
        {
            case "CEffect":
                Console.WriteLine("InportExportEffect");
                Structs.SparkleCEffect CEffectIn = new Structs.SparkleCEffect();
                sparkleFile.CEffect = CEffectIn;
                break;
            case "Material":
                Console.WriteLine("InportExportMaterial");
                Structs.SparkleMaterial MaterialIn = new Structs.SparkleMaterial();
                MaterialIn = SparkleFunctions.MaterialReadBin(binaryReader, MaterialIn);
                sparkleFile.Material = MaterialIn;
                break;
            default:
                Console.WriteLine("Unknown Type");
                break;
        }
        
        sparkleFile.Header = SparkleIn;
    }
    
    public static void SparkleWriteBin()
    {
        // K1 Sparkles
        
    }
}