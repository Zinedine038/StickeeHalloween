using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraEffectHit : MonoBehaviour {
    public PostProcessingProfile ppp;
    private VignetteModel.Settings vign;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P))
        {
            GetHit();
        }
	}

    public void GetHit()
    {
        StopAllCoroutines();
        StartCoroutine(Hit());
    }

    private IEnumerator Hit()
    {
        StartCoroutine(HitFadeOut());
        while(ppp.vignette.settings.color != Color.red)
        {
            vign = ppp.vignette.settings;
            vign.color = Color.Lerp(vign.color, Color.red, 5 * Time.deltaTime);
            ppp.vignette.settings = vign;
            yield return null;
        }
    }

    private IEnumerator HitFadeOut()
    {
        yield return new WaitForSeconds(2f);
        StopCoroutine(Hit());
        while (ppp.vignette.settings.color != Color.black)
        {
            print("kdjskjgfhkdjshfskdjfdskjhfdsk");
            vign = ppp.vignette.settings;
            vign.color = Color.Lerp(vign.color, Color.black, 5 * Time.deltaTime);
            ppp.vignette.settings = vign;
            yield return null;
        }
    }
}
