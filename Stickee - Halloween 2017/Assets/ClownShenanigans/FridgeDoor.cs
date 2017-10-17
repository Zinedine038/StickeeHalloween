using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeDoor : MonoBehaviour {

    public bool mayCreak = false;
	public IEnumerator CreakDoor()
    {
        mayCreak=true;
        while(mayCreak)
        {
            transform.Rotate(0,0,-5*Time.deltaTime);
            yield return null;
        }
    }
}
