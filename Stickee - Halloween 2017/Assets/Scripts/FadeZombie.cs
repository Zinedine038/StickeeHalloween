using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeZombie : MonoBehaviour {

    [SerializeField] public float fadePerSecond = 0.5f;
    bool mayFade = false;

    private void OnEnable()
    {
        StartCoroutine("WaitForFade");
    }

    private void Update()
    {
        if(mayFade)
        {
            var material = GetComponent<Renderer>().material;
            var color = material.color;

            material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));
        }
    }

    private IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(2);
        mayFade=true;
        Destroy(this.gameObject.transform.parent.gameObject,5f);
    }
}
