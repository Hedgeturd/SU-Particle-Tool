using System.Xml;
using SU_Particle_Tool.Sparkle.Particle;

namespace SU_Particle_Tool.Sparkle;

public class Composite
{
    public Header Header;
    public Particle.Effect CEffect;
    public P_Material.Material Material;
    
    public static void WriteBin(string filePath, Composite sparkleComposite)
    {
        string ext = null;
        
        switch (sparkleComposite.Header.Type)
        {
            case "CEffect":
                ext = ".part-bin";
                break;
            case "Material":
                ext = ".p-mat-bin";
                break;
            default:
                Console.WriteLine("Unknown Type");
                break;
        }
        
        var newfp = Path.GetDirectoryName(filePath) + "\\" + Path.GetFileNameWithoutExtension(filePath);
        File.Delete(Path.Combine(newfp + ".xmlbin"));
        BinaryWriter binaryWriter = new BinaryWriter(File.Open(newfp + ext, FileMode.OpenOrCreate));
        
        Header.WriteBin(binaryWriter, sparkleComposite.Header);

        if (sparkleComposite.Header.Type == "CEffect") Effect.WriteBin(binaryWriter, sparkleComposite.CEffect);
        if (sparkleComposite.Header.Type == "Material") P_Material.Material.WriteBin(binaryWriter, sparkleComposite.Material);
        
        binaryWriter.Write(83);
        binaryWriter.Write(69);
        binaryWriter.Write(71);
        binaryWriter.Write(65);
        
        binaryWriter.Close();
    }
    
    public static Composite ReadXml(XmlElement xRoot) //, Composite composite)
    {
        Composite composite = new Composite();
        string type = null;

        switch (xRoot.Name)
        {
            case "InportExportEffect":
                type = "CEffect";
                break;
            case "InportExportMaterial":
                type = "Material";
                break;
            default:
                Console.WriteLine("Unknown Type");
                return null;
        }
        
        foreach (XmlElement node in xRoot)
        {
            if (node.Name == "ExportInfo")
            {
                composite.Header = Header.ReadXml(node, type);
            }

            switch (composite.Header.Type)
            {
                case "CEffect":
                    if (node.Name == "EffectSaveLoad") composite.CEffect = Particle.Effect.ReadXml(node);
                    break;
                case "Material":
                    if (node.Name == "MaterialSaveLoad") composite.Material = P_Material.Material.ReadXml(node);
                    break;
                default:
                    Console.WriteLine("Unknown Type");
                    break;
            }
        }

        if (composite.Header.Type == "CEffect")
        {
            composite.Header.EmitterCount += composite.CEffect.Emitters.Count;
            for (int i = 0; i < composite.Header.EmitterCount; i++)
            {
                composite.Header.ParticleCount += composite.CEffect.Emitters[i].ParticleSaveLoad.Count;
            }
        }

        Console.WriteLine("Done Reading EffectSaveLoad");
        return composite;
    }
}