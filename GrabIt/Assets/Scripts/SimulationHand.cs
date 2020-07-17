using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.IO;

public class SimulationHand : MonoBehaviour
{

	// public List<Transform> readBones;
	// public OVRCustomSkeleton skeleton;

	private static float time0;
	private string path;
	private StreamWriter writer;
	
	void Start()
	{
		
		time0 = Time.time;
		// readBones = skeleton.CustomBones;
	}

	void Update()
	{
		path = Application.persistentDataPath +"./BREH.txt";//$"/mnt/sdcard/BEH.txt";

        writer = new StreamWriter(path, true);
		writer.WriteLine("BLABLA");

		writer.WriteLine((Time.unscaledTime - time0));
	    	// writer.WriteLine("Time;" + (Time.unscaledTime - time0) + "BoneID;" + i.ToString() + ";Pos;" + skeleton.CustomBones[i].transform.position.ToString("F4") + ";Rot;" + skeleton.CustomBones[i].transform.eulerAngles.ToString("F4"));
    	writer.Close();

		// readBones = skeleton.CustomBones;
		// Transform t = readBones[0].transform;
		// // print(t.position);
		// // print(t.rotation);
		// Debug.Log(readBones[0]);
		

		// for(int i = 0; i < readBones.Count; i++)
  //   	{
  //       	skeleton.CustomBones[i].transform.position = new Vector3(readBones[i].transform.position.x, readBones[i].transform.position.y, readBones[i].transform.position.z);
  //       	skeleton.CustomBones[i].transform.eulerAngles = new Vector3(readBones[i].transform.eulerAngles.x, readBones[i].transform.eulerAngles.y, readBones[i].transform.eulerAngles.z);
	        
	 //        this.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.position = new Vector3(readBones[i].transform.position.x + 1f, readBones[i].transform.position.y, readBones[i].transform.position.z);
		// 	this.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.eulerAngles = new Vector3(readBones[i].transform.eulerAngles.x, readBones[i].transform.eulerAngles.y, readBones[i].transform.eulerAngles.z);
		// 	writer.WriteLine((Time.unscaledTime - time0));
	 //    	// writer.WriteLine("Time;" + (Time.unscaledTime - time0) + "BoneID;" + i.ToString() + ";Pos;" + skeleton.CustomBones[i].transform.position.ToString("F4") + ";Rot;" + skeleton.CustomBones[i].transform.eulerAngles.ToString("F4"));
	 //    	writer.Close();
  //   	}
		
	}
}
