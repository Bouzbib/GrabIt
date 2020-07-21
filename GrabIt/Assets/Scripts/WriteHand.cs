using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.IO;

public class WriteHand : MonoBehaviour
{
    private static float time0;
	private string path, time1;
	private StreamWriter writer;
	public GameObject handLeft, handRight;
	private int state = 0;
	private GameObject user;
	
	void Start()
	{
		time0 = Time.time;
		time1 = System.DateTime.Now.ToString("ddMMyyyy-HHmm");
		user = GameObject.Find("CenterEyeAnchor");
	}

	void Update()
	{
		path = $"/mnt/sdcard/" + time1 + ".csv";

		writer = new StreamWriter(path, true);
		switch(state)
		{
			case 0:
				// writer.WriteLine("Time;UserPosX;UserPosY;UserPosZ;UserOrientX;UserOrientY;UserOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ");
				writer.WriteLine("Time;UserPos;UserOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient");
				writer.Close();
				state = 1;
			break;

			case 1:
				writer.WriteLine();
				writer.Write((Time.unscaledTime - time0) + ";" + (user.transform.position*1000).ToString("F4") + ";" + user.transform.eulerAngles.ToString("F4"));
		    	for(int i = 0; i < handLeft.GetComponent<OVRCustomSkeleton>().CustomBones.Count; i++)
		    	{
					// writer.WriteLine((Time.unscaledTime - time0) + ";" + user.transform.position.x*1000.ToString("F4") + ";" + user.transform.position.y*1000.ToString("F4") + ";" + user.transform.position.z*1000.ToString("F4") + ";" + user.transform.eulerAngles.x + ";" + user.transform.eulerAngles.y + ";" + user.transform.eulerAngles.z + ";" + i.ToString() +";"+ handLeft.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.position.x.ToString("F4") +";"+ handLeft.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.position.y.ToString("F4") +";"+ handLeft.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.position.z.ToString("F4") +";"+ handLeft.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.eulerAngles.x.ToString("F4") +";"+ handLeft.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.eulerAngles.y.ToString("F4") +";"+ handLeft.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.eulerAngles.z.ToString("F4")+";"+ handRight.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.position.x.ToString("F4") +";"+ handRight.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.position.y.ToString("F4") +";"+ handRight.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.position.z.ToString("F4") +";"+ handRight.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.eulerAngles.x.ToString("F4") +";"+ handRight.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.eulerAngles.y.ToString("F4") +";"+ handRight.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.eulerAngles.z.ToString("F4"));
					writer.Write(";" + i.ToString() +";"+ (handLeft.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.position*1000).ToString("F4") +";"+ handLeft.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.eulerAngles.ToString("F4")+";"+ (handRight.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.position*1000).ToString("F4") +";"+ handRight.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.eulerAngles.ToString("F4"));
		    	}
		    	writer.Close();
			break;
		}

	}
}
