using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RagdollZombie : MonoBehaviour
{
    public bool isRegularMesh = false;
	// Use this for initialization
	void Start () {
		
	}
	

    public void ChangeToRegularMesh(bool burst = false)
    {
        if(!isRegularMesh)
        {
            Destroy(GetComponent<ZombieMotor>());
            Destroy(GetComponent<NavMeshAgent>());
            Destroy(GetComponent<Animator>());
            isRegularMesh = true;
            foreach (SkinnedMeshRenderer skin in GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                Mesh mesh;
                Material material;
                GameObject meshObj = skin.gameObject;
                var smr = skin;
                mesh = new Mesh();
                smr.BakeMesh(mesh);
                material = smr.sharedMaterial;


                Destroy(skin);
                var filter = meshObj.AddComponent<MeshFilter>();
                var renderer = meshObj.AddComponent<MeshRenderer>();
                
                renderer.sharedMaterial=material;
                filter.sharedMesh = mesh;
                meshObj.GetComponent<BodyPart>().isDead=true;
                RagDoll(meshObj.GetComponent<Rigidbody>());
                Destroy(meshObj.GetComponent<Sliceable>(),1.5f);
                Destroy(meshObj.GetComponent<Collider>(),1.5f);
                Destroy(meshObj,3f);
            }
        }
    }

    private void RagDoll(Rigidbody rigidbody)
    {
        rigidbody.constraints = RigidbodyConstraints.None;
    }
}
