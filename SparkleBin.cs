namespace SU_Particle_Tool;

public class SparkleBin
{
    public static Structs.SparkleFile SparkleFile = new Structs.SparkleFile();
    public static void ReadBin(string path)
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
                SparkleFile.CEffect = CEffectIn;
                break;
            case "Material":
                Console.WriteLine("InportExportMaterial");
                Structs.SparkleMaterial MaterialIn = new Structs.SparkleMaterial();
                MaterialIn = SparkleFunctions.MaterialReadBin(binaryReader, MaterialIn);
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