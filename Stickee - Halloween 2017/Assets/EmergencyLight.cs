using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyLight : MonoBehaviour
{
    public float spinsPerSecond;

    void Update()
    {
        transform.Rotate(new Vector3(0,(spinsPerSecond*360)*Time.deltaTime,0));
    }
}
