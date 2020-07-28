using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.IO;

public class WriteObjOfInterest : MonoBehaviour
{
	private static float time0;
	private string path, time1;
	private StreamWriter writer;
	private GameObject ParentOOI;
	private int state = 0;
	private GameObject user;
	public GameObject[] OOI;

	public InstantiateGameObjects objectInstance;

    void Start()
    {
    	objectInstance = FindObjectOfType<InstantiateGameObjects>();
		// OOI = FindObjectsOfType<ObjectOfInterest>();
    	// ParentOOI = GameObject.Find("ObjectsOfInterest");
    	// for(int i = 0; i < ParentOOI.GetComponentsInChildren<Transform>().Length; i++)
    	// {
    	// 	OOI[i] = GameObject.Find("ObjectsOfInterest").GetComponentsInChildren<Transform>()[i].gameObject;
    	// }
		// OOI = new GameObject[]{GameObject.Find("ObjectsOfInterest/Cylinder"), GameObject.Find("ObjectsOfInterest/Cube"), GameObject.Find("ObjectsOfInterest/Sphere"), GameObject.Find("ObjectsOfInterest/Capsule")};
        OOI = objectInstance.objectTypes;
        time0 = Time.time;
		time1 = System.DateTime.Now.ToString("ddMMyyyy-HHmm");
    }

    // Update is called once per frame
    void Update()
    {
		OOI = new GameObject[]{GameObject.Find("ObjectsOfInterest/Cylinder"), GameObject.Find("ObjectsOfInterest/Cube"), GameObject.Find("ObjectsOfInterest/Sphere"), GameObject.Find("ObjectsOfInterest/Capsule")};

        path = $"/mnt/sdcard/" + time1 + "-OOI.csv";
        writer = new StreamWriter(path, true);
// NEED TO WRITE OBJECT TYPE / NEED TO WRITE SIZE???
    switch(state)
		{
			case 0:
				writer.WriteLine();
				writer.Write("Time;Config;");
				for(int i = 0; i < OOI.Length; i++)
		    	{
					writer.Write("OOI;x;y;z;OrX;OrY;OrZ;");
		    	}
				writer.Close();
				state = 1;
			break;

// WRITE OBJECTS OF INTEREST / REMOVE RB/COLLIDER AND ADD ROT/POS?
			case 1:
				writer.WriteLine();
				
				writer.Write((Time.unscaledTime - time0) + ";" + objectInstance.config);
		    	for(int i = 0; i < OOI.Length; i++)
		    	{
					writer.Write(";" + i.ToString() +";"+ OOI[i].transform.position.x.ToString("F4") +";"+ OOI[i].transform.position.y.ToString("F4") +";"+ OOI[i].transform.position.z.ToString("F4") +";"+ OOI[i].transform.eulerAngles.x.ToString("F4") +";"+ OOI[i].transform.eulerAngles.y.ToString("F4") +";"+ OOI[i].transform.eulerAngles.z.ToString("F4"));
		    	}
		    	writer.Close();
			break;
		}

	}
}
