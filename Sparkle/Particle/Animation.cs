using System.Xml;
namespace SU_Particle_Tool.Sparkle.Particle;

public class Animation
{
    string TypeName;
    Track ColorA;
    Track ColorB;
    Track ColorG;
    Track ColorR;
    
    Track TransX;
    Track TransY;
    Track TransZ;
    
    Track ScaleX;
    Track ScaleY;
    Track ScaleZ;
    
    enum CurveTypes
    {
        LINEAR,
        SPLINE,
        CONSTANT,
        ROTATE
    }
    
    struct Track
    {
        public int CurveType;
        public int KeyCount;
        public int UField0;
        public List<KeyFrame> KeyFrames;
    }
    struct KeyFrame
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
    
    // Binary
    static Track TrackReadBin(BinaryReader binaryReader)
    {
        Track track = new Track();
        track.CurveType = binaryReader.ReadInt32();
        track.KeyCount = binaryReader.ReadInt32();
        track.UField0 = binaryReader.ReadInt32();

        if (track.KeyCount > 0)
        {
            track.KeyFrames = new List<KeyFrame>();
            for (int i = 0; i < track.KeyCount; i++)
            {
                KeyFrame keyFrame = new KeyFrame
                {
                    Time = binaryReader.ReadSingle(),
                    Value = binaryReader.ReadSingle(),
                    ValueUpperBias = binaryReader.ReadSingle(),
                    ValueLowerBias = binaryReader.ReadSingle(),
                    SlopeL = binaryReader.ReadSingle(),
                    SlopeR = binaryReader.ReadSingle(),
                    SlopeLUpperBias = binaryReader.ReadSingle(),
                    SlopeLLowerBias = binaryReader.ReadSingle(),
                    SlopeRUpperBias = binaryReader.ReadSingle(),
                    SlopeRLowerBias = binaryReader.ReadSingle(),
                    KeyBreak = Common.ReadBool(binaryReader)
                };
                track.KeyFrames.Add(keyFrame);
            }
        }
        
        return track;
    }

    static void TrackWriteBin(BinaryWriter writer, Track track)
    {
        writer.Write(track.CurveType);
        writer.Write(track.KeyFrames.Count);
        writer.Write(41);

        for (int i = 0; i < track.KeyFrames.Count; i++)
        {
            writer.Write(track.KeyFrames[i].Time);
            writer.Write(track.KeyFrames[i].Value);
            writer.Write(track.KeyFrames[i].ValueUpperBias);
            writer.Write(track.KeyFrames[i].ValueLowerBias);
            writer.Write(track.KeyFrames[i].SlopeL);
            writer.Write(track.KeyFrames[i].SlopeR);
            writer.Write(track.KeyFrames[i].SlopeLUpperBias);
            writer.Write(track.KeyFrames[i].SlopeLLowerBias);
            writer.Write(track.KeyFrames[i].SlopeRUpperBias);
            writer.Write(track.KeyFrames[i].SlopeRLowerBias);
            writer.Write(track.KeyFrames[i].KeyBreak);
            Common.BoolPadding(writer);
        }
    }

    public static Animation ReadBin(BinaryReader binaryReader)
    {
        Animation animtrack = new Animation();
        animtrack.TypeName = Common.ReadName(binaryReader);
        
        animtrack.ColorA = TrackReadBin(binaryReader);
        animtrack.ColorB = TrackReadBin(binaryReader);
        animtrack.ColorG = TrackReadBin(binaryReader);
        animtrack.ColorR = TrackReadBin(binaryReader);
        
        animtrack.TransX = TrackReadBin(binaryReader);
        animtrack.TransY = TrackReadBin(binaryReader);
        animtrack.TransZ = TrackReadBin(binaryReader);
        
        animtrack.ScaleX = TrackReadBin(binaryReader);
        animtrack.ScaleY = TrackReadBin(binaryReader);
        animtrack.ScaleZ = TrackReadBin(binaryReader);
        
        Console.WriteLine("Anim Read");

        return animtrack;
    }

    public static void WriteBin(BinaryWriter writer, Animation animtrack)
    {
        writer.Write("AnimationChunk");
        Common.WritePadding(writer);

        TrackWriteBin(writer, animtrack.ColorA);
        TrackWriteBin(writer, animtrack.ColorB);
        TrackWriteBin(writer, animtrack.ColorG);
        TrackWriteBin(writer, animtrack.ColorR);
        
        TrackWriteBin(writer, animtrack.TransX);
        TrackWriteBin(writer, animtrack.TransY);
        TrackWriteBin(writer, animtrack.TransZ);
        
        TrackWriteBin(writer, animtrack.ScaleX);
        TrackWriteBin(writer, animtrack.ScaleY);
        TrackWriteBin(writer, animtrack.ScaleZ);
    }
    
    // XML
    static Track TrackReadXml(XmlNode node)
    {
        Track track = new Track();
        
        track.CurveType = (int)Enum.Parse(typeof(CurveTypes), node.ChildNodes[0].InnerText);
        track.KeyFrames = new List<KeyFrame>();
        for (int i = 0; i < node.ChildNodes[1].ChildNodes.Count; i++)
        {
            var keyframe = node.ChildNodes[1].ChildNodes[i];
            KeyFrame keyFrame = new KeyFrame();
            keyFrame.Time = float.Parse(keyframe.ChildNodes[0].InnerText);
            keyFrame.Value = float.Parse(keyframe.ChildNodes[1].InnerText);
            keyFrame.ValueUpperBias = float.Parse(keyframe.ChildNodes[2].InnerText);
            keyFrame.ValueLowerBias = float.Parse(keyframe.ChildNodes[3].InnerText);
            keyFrame.SlopeL = float.Parse(keyframe.ChildNodes[4].InnerText);
            keyFrame.SlopeR = float.Parse(keyframe.ChildNodes[5].InnerText);
            keyFrame.SlopeLUpperBias = float.Parse(keyframe.ChildNodes[6].InnerText);
            keyFrame.SlopeLLowerBias = float.Parse(keyframe.ChildNodes[7].InnerText);
            keyFrame.SlopeRUpperBias = float.Parse(keyframe.ChildNodes[8].InnerText);
            keyFrame.SlopeRLowerBias = float.Parse(keyframe.ChildNodes[9].InnerText);
            keyFrame.KeyBreak = bool.Parse(keyframe.ChildNodes[10].InnerText);
            track.KeyFrames.Add(keyFrame);
        }

        return track;
    }
    
    static void TrackWriteXml(XmlWriter writer, string field, Track animation)
    {
        writer.WriteStartElement(field);
        writer.WriteElementString("CurveType", ((CurveTypes)animation.CurveType).ToString());
        writer.WriteStartElement("KeyFrameList");
        if (animation.KeyCount > 0)
        {
            foreach (var KeyFrame in animation.KeyFrames.ToList())
            {
                writer.WriteStartElement("KeyFrame");
                writer.WriteElementString("Time", KeyFrame.Time.ToString());
                writer.WriteElementString("Value", KeyFrame.Value.ToString());
                writer.WriteElementString("ValueUpperBias", KeyFrame.ValueUpperBias.ToString());
                writer.WriteElementString("ValueLowerBias", KeyFrame.ValueLowerBias.ToString());
                writer.WriteElementString("SlopeL", KeyFrame.SlopeL.ToString());
                writer.WriteElementString("SlopeR", KeyFrame.SlopeR.ToString());
                writer.WriteElementString("SlopeLUpperBias", KeyFrame.SlopeLUpperBias.ToString());
                writer.WriteElementString("SlopeLLowerBias", KeyFrame.SlopeLLowerBias.ToString());
                writer.WriteElementString("SlopeRUpperBias", KeyFrame.SlopeRUpperBias.ToString());
                writer.WriteElementString("SlopeRLowerBias", KeyFrame.SlopeRLowerBias.ToString());
                writer.WriteElementString("KeyBreak", KeyFrame.KeyBreak.ToString().ToLower());
                writer.WriteEndElement();
            }
        }
        writer.WriteEndElement();
        writer.WriteEndElement();
    }

    public static Animation ReadXml(XmlNode node)
    {
        Animation animation = new Animation
        {
            TransX = TrackReadXml(node.ChildNodes[0]),
            TransY = TrackReadXml(node.ChildNodes[1]),
            TransZ = TrackReadXml(node.ChildNodes[2]),
            
            ScaleX = TrackReadXml(node.ChildNodes[3]),
            ScaleY = TrackReadXml(node.ChildNodes[4]),
            ScaleZ = TrackReadXml(node.ChildNodes[5]),
            
            ColorR = TrackReadXml(node.ChildNodes[6]),
            ColorG = TrackReadXml(node.ChildNodes[7]),
            ColorB = TrackReadXml(node.ChildNodes[8]),
            ColorA = TrackReadXml(node.ChildNodes[9]),
        };
        return animation;
    }
    
    public static void WriteXml(XmlWriter writer, Animation Anim) 
    {
        writer.WriteStartElement("Animation");
        TrackWriteXml(writer, "TransX", Anim.TransX);
        TrackWriteXml(writer, "TransY", Anim.TransY);
        TrackWriteXml(writer, "TransZ", Anim.TransZ);

        TrackWriteXml(writer, "ScaleX", Anim.ScaleX);
        TrackWriteXml(writer, "ScaleY", Anim.ScaleY);
        TrackWriteXml(writer, "ScaleZ", Anim.ScaleZ);

        TrackWriteXml(writer, "ColorR", Anim.ColorR);
        TrackWriteXml(writer, "ColorG", Anim.ColorG);
        TrackWriteXml(writer, "ColorB", Anim.ColorB);
        TrackWriteXml(writer, "ColorA", Anim.ColorA);
        writer.WriteEndElement();
    }
}