using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CurrentPlayerData : MonoBehaviour {

	public string name;
    public int zombiesKilled;
    public int score;
    public TextMeshProUGUI nameText;
    public void ResetData()
    {
        //Stash dat old shit somewhere
        zombiesKilled = 0;
        name = "";
    }

    public void AddLetterToName(Transform letter)
    {
        name = name + letter.transform.name;
        SetNameGUI();
    }

    public void DeletLetter()
    {
        char[] chars = name.ToCharArray();
        string newName = "";
        for (int i = 0; i < chars.Length - 1; i++)
        {
            newName = newName + chars[i];
        }
        name = newName;
        SetNameGUI();
    }

    void SetNameGUI()
    {
        nameText.text = name;
    }

    public void PlayerDied()
    {
        PlayerData newData = new PlayerData();
        newData.name = name;
        newData.difficultyPlayed = GameManager.instance.difficulty.ToString();
        newData.zombiesKilled = zombiesKilled;
        newData.score=score;
        FindObjectOfType<HighScoresProcessor>().AddNewData(newData);
        ResetData();
    }
}
