using SU_Particle_Tool.Sparkle;
namespace SU_Particle_Tool;

public class SparkleBin
{
    public static Composite SparkleCompositeBin = new Composite();
    public static void ReadBin(string path)
    {
        FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        BinaryReader binaryReader = new BinaryReader(fileStream);
        Header SparkleIn = new Header();

        SparkleIn = Header.ReadBin(binaryReader);

        switch (SparkleIn.Type)
        {
            case "CEffect":
                Console.WriteLine("InportExportEffect");
                Sparkle.Particle.Effect CEffectIn = new Sparkle.Particle.Effect();
                CEffectIn = Sparkle.Particle.Effect.ReadBin(binaryReader, SparkleIn.EmitterCount);
                SparkleCompositeBin.CEffect = CEffectIn;
                break;
            case "Material":
                Console.WriteLine("InportExportMaterial");
                Sparkle.P_Material.Material MaterialIn = new Sparkle.P_Material.Material();
                MaterialIn = Sparkle.P_Material.Material.ReadBin(binaryReader, MaterialIn);
                SparkleCompositeBin.Material = MaterialIn;
                break;
            default:
                Console.WriteLine("Unknown Type");
                break;
        }
        
        SparkleCompositeBin.Header = SparkleIn;
    }
    
    public static void WriteBin(string path)
    {
        //string filePath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path);
        Composite.WriteBin(path, SparkleXml.SparkleCompositeXml);
    }
}