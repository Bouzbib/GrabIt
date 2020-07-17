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

	public string FileName;
	private static float time0;
	public bool test = true;
	private string path;
	private StreamWriter writer;

    // Start is called before the first frame update
    void Start()
    {
    	path = $"/mnt/sdcard/KIKOU2.txt";

        writer = new StreamWriter(path, true);
		time0 = Time.time;
		writer.WriteLine("HEY BOOMBOOM");
    }

    // Update is called once per frame
    void Update()
    {

        // string path = "Assets/Resources/DataCollection/" + FileName + ".csv";

        // string path = Application.persistentDataPath +"./Data/BIBOU.txt";
        writer = new StreamWriter(path, true);

    	readBones = readFrom.CustomBones;
        this.transform.position = new Vector3(skeleton.transform.position.x + 1.0f, skeleton.transform.position.y, skeleton.transform.position.z);
        this.transform.eulerAngles = new Vector3(skeleton.transform.eulerAngles.x, skeleton.transform.eulerAngles.y, skeleton.transform.eulerAngles.z); 
        writer.WriteLine("HEY BIMBOOM");
        writer.WriteLine("Time;" + (Time.unscaledTime - time0) + ";PosG;" + skeleton.transform.position.ToString("F4") + ";RotG;" + skeleton.transform.eulerAngles.ToString("F4"));

    	for(int i = 0; i < readBones.Count; i++)
    	{
        	// skeleton.CustomBones[i].transform.position = new Vector3(readBones[i].transform.position.x, readBones[i].transform.position.y, readBones[i].transform.position.z);
        	// skeleton.CustomBones[i].transform.eulerAngles = new Vector3(readBones[i].transform.eulerAngles.x, readBones[i].transform.eulerAngles.y, readBones[i].transform.eulerAngles.z);
	        
	        this.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.position = new Vector3(readBones[i].transform.position.x, readBones[i].transform.position.y, readBones[i].transform.position.z);
			this.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.eulerAngles = new Vector3(readBones[i].transform.eulerAngles.x, readBones[i].transform.eulerAngles.y, readBones[i].transform.eulerAngles.z);
			
	     //    if(test)
		    // {
		    	writer.WriteLine("Time;" + (Time.unscaledTime - time0) + "BoneID;" + i.ToString() + ";Pos;" + readBones[i].transform.position.ToString("F4") + ";Rot;" + readBones[i].transform.eulerAngles.ToString("F4"));
    			writer.Close();
		    // }

    	}
		// writer.WriteLine((Time.unscaledTime - time0));
	    // writer.Close();
    }
}
