namespace SU_Particle_Tool
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) {
                Console.WriteLine("SU-Particle-Tool v2.0\nUsage: SU-Particle-Tool <Path to .part-bin/.p-mat-bin>");
                Console.ReadKey();
                return;
            }
            
            string file = Path.GetDirectoryName(args[0]) + "\\" + Path.GetFileName(args[0]);
            if (File.Exists(file))
            {
                switch (Path.GetExtension(file))
                {
                    case ".part-bin":
                        SparkleBin.ReadBin(file);
                        SparkleXml.WriteXml(file);
                        break;
                    case ".p-mat-bin":
                        SparkleBin.ReadBin(file);
                        SparkleXml.WriteXml(file);
                        break;
                    case ".particle":
                        SparkleXml.ReadXml(file);
                        SparkleBin.WriteBin(file);
                        break;
                    case ".p-material":
                        SparkleXml.ReadXml(file);
                        SparkleBin.WriteBin(file);
                        break;
                    default:
                        Console.WriteLine("Invalid file extension");
                        break;
                }
            }
            else
            {
                Console.WriteLine($"Can't find file " + Path.GetFileName(args[0]) + ", aborting.");
            }
        }
    }
}