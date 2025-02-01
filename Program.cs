namespace SU_Particle_Tool
{
    static class Program
    {
        static void Main(string[] args)
        {
            string file = Path.GetDirectoryName(args[0]) + "\\" + Path.GetFileName(args[0]);

            if (File.Exists(file))
            {
                switch (Path.GetExtension(file))
                {
                    case ".part-bin":
                        // Code :D
                        break;
                    case ".p-mat-bin":
                        SparkleBin.ReadBin(file);
                        SparkleXml.WriteXml(file);
                        break;
                    case ".xml":
                        //XML.ReadXML(args[0]);
                        //XML.WriteBIN(args[0]);
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