using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {
	private bool horror = false;
	public CanvasGroup fadeToBlack;
	public GameObject scaryScene, normalScene;
	// Update is called once per frame
	void Update () 
	{

		if (Input.GetKeyDown (KeyCode.H)) 
		{
			StartCoroutine(SwitchScene());
		}
	}



	private IEnumerator SwitchScene()
	{
		horror = !horror;
		if (horror) 
		{
			fadeToBlack.DOFade (1, 1f);
			yield return new WaitForSeconds (1f);
			normalScene.SetActive (false);
			scaryScene.SetActive (true);
			fadeToBlack.DOFade (0, 1f);
		} 
		else 
		{
			fadeToBlack.DOFade (1, 1f);
			yield return new WaitForSeconds (1f);
			scaryScene.SetActive (false);
			normalScene.SetActive (true);
			fadeToBlack.DOFade (0, 1f);
		}
	}



}
