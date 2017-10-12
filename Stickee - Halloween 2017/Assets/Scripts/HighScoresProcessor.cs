using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using TMPro;

public class HighScoresProcessor : MonoBehaviour
{
    public HighScores highScores;
    public TextMeshPro[] highScoreNames;
    public TextMeshPro[] highScoresScores;
    private void Awake()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, "highscores.xml")))
        {
            var oldScores = HighScores.LoadOldData(Path.Combine(Application.persistentDataPath, "highscores.xml"));
            highScores = oldScores;
            VisualizeHighscores();
        }
    }

    public void AddNewData(PlayerData newData)
    {
        if(highScores==null)
        {
            highScores = new HighScores();
        }
        highScores.scores.Add(newData);
        highScores.SaveData(Path.Combine(Application.persistentDataPath, "highscores.xml"));
        VisualizeHighscores();
    }

    public void VisualizeHighscores()
    {
        List<PlayerData> highScoreList = highScores.scores.OrderBy(o => o.score  ).ToList();
        highScoreList.Reverse();
        for(int i =0; i<highScoreNames.Length; i++)
        {
            print(i);
            if(highScoreList.Count>i)
            {
                highScoreNames[i].text = highScoreList[i].name;
                highScoresScores[i].text = highScoreList[i].score.ToString();
            }
            else
            {
                highScoreNames[i].text="";
                highScoresScores[i].text="-";
            }

        }
    }
}
    