using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyBoardKey : MonoBehaviour {
    public TextMeshProUGUI text;

	// Use this for initialization
	void Start () {
		text=GetComponentInChildren<TextMeshProUGUI>();
        text.text=transform.name;
	}
	
	
}
