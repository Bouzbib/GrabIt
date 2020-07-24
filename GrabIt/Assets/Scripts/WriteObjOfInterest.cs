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
	public ObjectOfInterest[] OOI;
	private int state = 0;
	private GameObject user;

    void Start()
    {
        time0 = Time.time;
		time1 = System.DateTime.Now.ToString("ddMMyyyy-HHmm");
		OOI = FindObjectsOfType<ObjectOfInterest>();		
    }

    // Update is called once per frame
    void Update()
    {
        path = $"/mnt/sdcard/" + time1 + "-OOI.csv";
        writer = new StreamWriter(path, true);
// NEED TO WRITE OBJECT TYPE / NEED TO WRITE SIZE???
    switch(state)
		{
			case 0:
				writer.WriteLine();
				writer.Write("Time;");
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
				
				writer.Write((Time.unscaledTime - time0));
		    	for(int i = 0; i < OOI.Length; i++)
		    	{
					writer.Write(";" + i.ToString() +";"+ OOI[i].transform.position.x.ToString("F4") +";"+ OOI[i].transform.position.y.ToString("F4") +";"+ OOI[i].transform.position.z.ToString("F4") +";"+ OOI[i].transform.eulerAngles.x.ToString("F4") +";"+ OOI[i].transform.eulerAngles.y.ToString("F4") +";"+ OOI[i].transform.eulerAngles.z.ToString("F4"));
		    	}
		    	writer.Close();
			break;
		}

	}
}
