using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DeletLetterKnop : MonoBehaviour {
    public TextMeshProUGUI text;
    public void DeletKey()
    {
        if(text.text.ToCharArray().Length>0)
        {
            FindObjectOfType<CurrentPlayerData>().DeletLetter();
        }
    }
}
