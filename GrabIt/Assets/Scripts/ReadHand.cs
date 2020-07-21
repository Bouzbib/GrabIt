using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.IO;


public class ReadHand : MonoBehaviour
{
	public List<Transform> readBones;
	public OVRCustomSkeleton readFrom;
	public GameObject skeleton;

    void Start()
    {
		
    }

    void Update()
    {
       readBones = readFrom.CustomBones;
    	for(int i = 0; i < readBones.Count; i++)
    	{
        	// skeleton.CustomBones[i].transform.position = new Vector3(readBones[i].transform.position.x, readBones[i].transform.position.y, readBones[i].transform.position.z);
        	// skeleton.CustomBones[i].transform.eulerAngles = new Vector3(readBones[i].transform.eulerAngles.x, readBones[i].transform.eulerAngles.y, readBones[i].transform.eulerAngles.z);
	        
	        this.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.position = new Vector3(readBones[i].transform.position.x, readBones[i].transform.position.y, readBones[i].transform.position.z);
			this.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.eulerAngles = new Vector3(readBones[i].transform.eulerAngles.x, readBones[i].transform.eulerAngles.y, readBones[i].transform.eulerAngles.z);

    	}

    }
}
