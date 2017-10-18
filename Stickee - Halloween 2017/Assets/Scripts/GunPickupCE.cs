using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickupCE : MonoBehaviour {



    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.transform.tag=="Player")
        {
            if(GameManager.instance.leftAutoGrab.gameObject.activeSelf==false)
            {
                GameManager.instance.leftAutoGrab.enabled=true;
            }
        }
    }
}
