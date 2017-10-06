using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawn : MonoBehaviour {
    public GameObject zombie;
    public Transform testLoc;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.K))
        {
            print("dshfhgsdkjgjhfdsjkn");
            zombie.GetComponent<ZombieMotor>().player=GameObject.FindWithTag("MainCamera");
            Instantiate(zombie,testLoc.transform.position,zombie.transform.rotation);
        }
	}
}
