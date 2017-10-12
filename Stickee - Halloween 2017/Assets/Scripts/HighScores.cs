using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("HighScoresCollection")]
public class HighScores
{
    [XmlArray("Scores"),XmlArrayItem("Score")]
    public List<PlayerData> scores = new List<PlayerData>();

    public static HighScores LoadOldData(string path)
    {
        var serializer = new XmlSerializer(typeof(HighScores));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as HighScores;
        }
    }

    public void SaveData(string path)
    {
        var serializer = new XmlSerializer(typeof(HighScores));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream,this);
        }
    }

}
