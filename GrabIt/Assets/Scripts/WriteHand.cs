using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.IO;

public class WriteHand : MonoBehaviour
{
    private static float time0;
	private string path, pathColl, time1;
	private StreamWriter writer, writerCollider;
	public GameObject handLeft, handRight;
	private int state = 0;
	private GameObject user;
	
	public IList<OVRBoneCapsule> Capsules { get; private set; }
	public IList<OVRBoneCapsule> CapsulesR { get; private set; }

	private string patht, patht2;
	private StreamWriter test, test2;

	void Start()
	{
		time0 = Time.time;
		time1 = System.DateTime.Now.ToString("ddMMyyyy-HHmm");
		user = GameObject.Find("CenterEyeAnchor");
		Capsules = handLeft.GetComponent<OVRCustomSkeleton>().Capsules;
		CapsulesR = handRight.GetComponent<OVRCustomSkeleton>().Capsules;
	}

	void Update()
	{
		path = $"/mnt/sdcard/" + time1 + ".csv";
		pathColl = $"/mnt/sdcard/" + time1 + "-Coll.csv";
		
		patht = $"/mnt/sdcard/" + time1 + "pos-orient.csv";
		// patht2 = $"/mnt/sdcard/" + time1 + "-names.csv";

		writer = new StreamWriter(path, true);
		writerCollider = new StreamWriter(pathColl, true);

		test = new StreamWriter(patht, true);
		// test2 = new StreamWriter(patht2, true);
		
		switch(state)
		{
			case 0:
				// writer.WriteLine("Time;UserPosX;UserPosY;UserPosZ;UserOrientX;UserOrientY;UserOrientZ");//;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ");
				writer.WriteLine("Time;UserPosX;UserPosY;UserPosZ;UserOrientX;UserOrientY;UserOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ;BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ");
				// writerCollider.WriteLine("BoneIndex;LeftCenterX;LeftCenterY;LeftCenterZ;LeftRadius;LeftHeight;LeftDir;RightCenterX;RightCenterY;RightCenterZ;RightRadius;RightHeight;RightDir");
				writerCollider.WriteLine("BoneIndex;LeftCenterX;LeftCenterY;LeftCenterZ;LeftRadius;LeftHeight;LeftDir;RightCenterX;RightCenterY;RightCenterZ;RightRadius;RightHeight;RightDir;BoneIndex;LeftCenterX;LeftCenterY;LeftCenterZ;LeftRadius;LeftHeight;LeftDir;RightCenterX;RightCenterY;RightCenterZ;RightRadius;RightHeight;RightDir;BoneIndex;LeftCenterX;LeftCenterY;LeftCenterZ;LeftRadius;LeftHeight;LeftDir;RightCenterX;RightCenterY;RightCenterZ;RightRadius;RightHeight;RightDir;BoneIndex;LeftCenterX;LeftCenterY;LeftCenterZ;LeftRadius;LeftHeight;LeftDir;RightCenterX;RightCenterY;RightCenterZ;RightRadius;RightHeight;RightDir;BoneIndex;LeftCenterX;LeftCenterY;LeftCenterZ;LeftRadius;LeftHeight;LeftDir;RightCenterX;RightCenterY;RightCenterZ;RightRadius;RightHeight;RightDir;BoneIndex;LeftCenterX;LeftCenterY;LeftCenterZ;LeftRadius;LeftHeight;LeftDir;RightCenterX;RightCenterY;RightCenterZ;RightRadius;RightHeight;RightDir;BoneIndex;LeftCenterX;LeftCenterY;LeftCenterZ;LeftRadius;LeftHeight;LeftDir;RightCenterX;RightCenterY;RightCenterZ;RightRadius;RightHeight;RightDir;BoneIndex;LeftCenterX;LeftCenterY;LeftCenterZ;LeftRadius;LeftHeight;LeftDir;RightCenterX;RightCenterY;RightCenterZ;RightRadius;RightHeight;RightDir;BoneIndex;LeftCenterX;LeftCenterY;LeftCenterZ;LeftRadius;LeftHeight;LeftDir;RightCenterX;RightCenterY;RightCenterZ;RightRadius;RightHeight;RightDir;BoneIndex;LeftCenterX;LeftCenterY;LeftCenterZ;LeftRadius;LeftHeight;LeftDir;RightCenterX;RightCenterY;RightCenterZ;RightRadius;RightHeight;RightDir;BoneIndex;LeftCenterX;LeftCenterY;LeftCenterZ;LeftRadius;LeftHeight;LeftDir;RightCenterX;RightCenterY;RightCenterZ;RightRadius;RightHeight;RightDir;BoneIndex;LeftCenterX;LeftCenterY;LeftCenterZ;LeftRadius;LeftHeight;LeftDir;RightCenterX;RightCenterY;RightCenterZ;RightRadius;RightHeight;RightDir;BoneIndex;LeftCenterX;LeftCenterY;LeftCenterZ;LeftRadius;LeftHeight;LeftDir;RightCenterX;RightCenterY;RightCenterZ;RightRadius;RightHeight;RightDir;BoneIndex;LeftCenterX;LeftCenterY;LeftCenterZ;LeftRadius;LeftHeight;LeftDir;RightCenterX;RightCenterY;RightCenterZ;RightRadius;RightHeight;RightDir;BoneIndex;LeftCenterX;LeftCenterY;LeftCenterZ;LeftRadius;LeftHeight;LeftDir;RightCenterX;RightCenterY;RightCenterZ;RightRadius;RightHeight;RightDir;BoneIndex;LeftCenterX;LeftCenterY;LeftCenterZ;LeftRadius;LeftHeight;LeftDir;RightCenterX;RightCenterY;RightCenterZ;RightRadius;RightHeight;RightDir;BoneIndex;LeftCenterX;LeftCenterY;LeftCenterZ;LeftRadius;LeftHeight;LeftDir;RightCenterX;RightCenterY;RightCenterZ;RightRadius;RightHeight;RightDir;BoneIndex;LeftCenterX;LeftCenterY;LeftCenterZ;LeftRadius;LeftHeight;LeftDir;RightCenterX;RightCenterY;RightCenterZ;RightRadius;RightHeight;RightDir;BoneIndex;LeftCenterX;LeftCenterY;LeftCenterZ;LeftRadius;LeftHeight;LeftDir;RightCenterX;RightCenterY;RightCenterZ;RightRadius;RightHeight;RightDir");
				test.WriteLine("i;x;y;z;orX;orY;orZ;x;y;z;orX;orY;orZ;i;x;y;z;orX;orY;orZ;x;y;z;orX;orY;orZ;i;x;y;z;orX;orY;orZ;x;y;z;orX;orY;orZ;i;x;y;z;orX;orY;orZ;x;y;z;orX;orY;orZ;i;x;y;z;orX;orY;orZ;x;y;z;orX;orY;orZ;i;x;y;z;orX;orY;orZ;x;y;z;orX;orY;orZ;i;x;y;z;orX;orY;orZ;x;y;z;orX;orY;orZ;i;x;y;z;orX;orY;orZ;x;y;z;orX;orY;orZ;i;x;y;z;orX;orY;orZ;x;y;z;orX;orY;orZ;i;x;y;z;orX;orY;orZ;x;y;z;orX;orY;orZ;i;x;y;z;orX;orY;orZ;x;y;z;orX;orY;orZ;i;x;y;z;orX;orY;orZ;x;y;z;orX;orY;orZ;i;x;y;z;orX;orY;orZ;x;y;z;orX;orY;orZ;i;x;y;z;orX;orY;orZ;x;y;z;orX;orY;orZ;i;x;y;z;orX;orY;orZ;x;y;z;orX;orY;orZ;i;x;y;z;orX;orY;orZ;x;y;z;orX;orY;orZ;i;x;y;z;orX;orY;orZ;x;y;z;orX;orY;orZ;i;x;y;z;orX;orY;orZ;x;y;z;orX;orY;orZ;i;x;y;z;orX;orY;orZ;x;y;z;orX;orY;orZ");
				// writer.WriteLine("Time;UserPos;UserOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient;BoneID;LeftPos;LeftOrient;RightPos;RightOrient");
				// for(int j = 0; j < handLeft.GetComponent<OVRCustomSkeleton>().CustomBones.Count; j++)
				// {
				// 	writer.Write(";");
				// 	writer.Write(";BoneID;LeftPosX;LeftPosY;LeftPosZ;LeftOrientX;LeftOrientY;LeftOrientZ;RightPosX;RightPosY;RightPosZ;RightOrientX;RightOrientY;RightOrientZ");//;LeftCaps;RightCaps");
				// }
				
// ADD HERE AND REMOVE IN CASE 1				
				// writerCollider.WriteLine(";");
				// for(int i = 0; i < Capsules.Count ; i++)
		  //   	{
				// 	// test.WriteLine(i.ToString() + ";" + Capsules[i].CapsuleCollider.gameObject.transform.position.x.ToString() + ";" + Capsules[i].CapsuleCollider.gameObject.transform.position.y.ToString() + ";" + Capsules[i].CapsuleCollider.gameObject.transform.position.z.ToString() + ";" + Capsules[i].CapsuleCollider.gameObject.transform.eulerAngles.x.ToString() + ";" + Capsules[i].CapsuleCollider.gameObject.transform.eulerAngles.y.ToString() + ";" + Capsules[i].CapsuleCollider.gameObject.transform.eulerAngles.z.ToString());
				// 	test2.WriteLine(i.ToString() + ";" + Capsules[i].CapsuleCollider.gameObject.name);
				// 	// test.Write(Capsules[i].CapsuleCollider.gameObject.name + ";x;y;z;orX;orY;orZ;");
				// }
				test.Close();
				// test2.Close();
				writerCollider.Close();
				writer.Close();
				state = 1;
			break;

// WRITE OBJECTS OF INTEREST / REMOVE RB/COLLIDER AND ADD ROT/POS?
			case 1:
				writer.WriteLine();
				// writer.Write((Time.unscaledTime - time0) + ";" + (user.transform.position*1000).ToString("F4") + ";" + user.transform.eulerAngles.ToString("F4"));
				
				writer.Write((Time.unscaledTime - time0) + ";" + (user.transform.position.x*1000).ToString("F4") + ";" + (user.transform.position.y*1000).ToString("F4") + ";" + (user.transform.position.z*1000).ToString("F4") + ";" + (user.transform.eulerAngles.x*1000).ToString("F4") + ";" + (user.transform.eulerAngles.y*1000).ToString("F4") + ";" + (user.transform.eulerAngles.z*1000).ToString("F4"));
		    	for(int i = 0; i < handLeft.GetComponent<OVRCustomSkeleton>().CustomBones.Count; i++)
		    	{
					writer.Write(";" + i.ToString() +";"+ handLeft.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.position.x.ToString("F4") +";"+ handLeft.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.position.y.ToString("F4") +";"+ handLeft.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.position.z.ToString("F4") +";"+ handLeft.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.eulerAngles.x.ToString("F4") +";"+ handLeft.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.eulerAngles.y.ToString("F4") +";"+ handLeft.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.eulerAngles.z.ToString("F4")+";"+ handRight.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.position.x.ToString("F4") +";"+ handRight.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.position.y.ToString("F4") +";"+ handRight.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.position.z.ToString("F4") +";"+ handRight.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.eulerAngles.x.ToString("F4") +";"+ handRight.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.eulerAngles.y.ToString("F4") +";"+ handRight.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.eulerAngles.z.ToString("F4"));
					// writer.Write(";" + i.ToString() +";"+ (handLeft.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.position*1000).ToString("F4") +";"+ handLeft.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.eulerAngles.ToString("F4")+";"+ (handRight.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.position*1000).ToString("F4") +";"+ handRight.GetComponent<OVRCustomSkeleton>().CustomBones[i].transform.eulerAngles.ToString("F4"));
		    	}

		    	writerCollider.WriteLine();
				for(int i = 0; i < Capsules.Count ; i++)
		    	{
					writerCollider.Write(i.ToString() + ";" + Capsules[i].CapsuleCollider.center.x.ToString() + ";" + Capsules[i].CapsuleCollider.center.y.ToString() + ";" + Capsules[i].CapsuleCollider.center.z.ToString() + ";" + Capsules[i].CapsuleCollider.radius.ToString() + ";" + Capsules[i].CapsuleCollider.height.ToString() + ";"+ Capsules[i].CapsuleCollider.direction.ToString() + ";" + CapsulesR[i].CapsuleCollider.center.x.ToString() + ";" + CapsulesR[i].CapsuleCollider.center.y.ToString() + ";" + CapsulesR[i].CapsuleCollider.center.z.ToString() + ";" + CapsulesR[i].CapsuleCollider.radius.ToString() + ";" + CapsulesR[i].CapsuleCollider.height.ToString() + ";"+ CapsulesR[i].CapsuleCollider.direction.ToString() + ";");
				}

				test.WriteLine();
				for(int i = 0; i < Capsules.Count ; i++)
		    	{
					test.Write(i.ToString() + ";" + Capsules[i].CapsuleCollider.gameObject.transform.position.x.ToString() + ";" + Capsules[i].CapsuleCollider.gameObject.transform.position.y.ToString() + ";" + Capsules[i].CapsuleCollider.gameObject.transform.position.z.ToString() + ";" + Capsules[i].CapsuleCollider.gameObject.transform.eulerAngles.x.ToString() + ";" + Capsules[i].CapsuleCollider.gameObject.transform.eulerAngles.y.ToString() + ";" + Capsules[i].CapsuleCollider.gameObject.transform.eulerAngles.z.ToString() + ";" + CapsulesR[i].CapsuleCollider.gameObject.transform.position.x.ToString() + ";" + CapsulesR[i].CapsuleCollider.gameObject.transform.position.y.ToString() + ";" + CapsulesR[i].CapsuleCollider.gameObject.transform.position.z.ToString() + ";" + CapsulesR[i].CapsuleCollider.gameObject.transform.eulerAngles.x.ToString() + ";" + CapsulesR[i].CapsuleCollider.gameObject.transform.eulerAngles.y.ToString() + ";" + CapsulesR[i].CapsuleCollider.gameObject.transform.eulerAngles.z.ToString() + ";");
				}
				
				test.Close();
				writerCollider.Close();
		    	writer.Close();
			break;
		}

	}
}
