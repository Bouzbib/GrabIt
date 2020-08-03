using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.IO;
using System.Globalization;

public class SimulationHand : MonoBehaviour
{
	private static float time0;
	private string path, pathCollProperty, pathCollNames;
	public string FileName;
    private Transform posUser;

    private GameObject handLeft, handRight;

    private string[] dataSplit, dataCollProp, dataCollPos, dataNames;
    private string[] splitSentence, collProperties, collPosOrient;
    private int frame = 1;

    private float frame_max;

    public int etat = 0;
    private int boneId;

    private StreamReader sr, srCollProp, srCollPos, srNames;
    
    public IList<OVRBoneCapsule> Capsules { get; private set; }
    public IList<OVRBoneCapsule> CapsulesR { get; private set; }

    private GameObject leftHandColl, rightHandColl;
	
	void Start()
	{
		posUser = GameObject.Find("Main Camera").transform;
		handLeft = GameObject.Find("OVRCustomHandPrefab_L");
		handRight = GameObject.Find("OVRCustomHandPrefab_R");
        leftHandColl = GameObject.Find("LeftHandColl");
        rightHandColl = GameObject.Find("RightHandColl");

		string path = "Assets/Resources/DataCollection/" + FileName + ".csv";
        // string pathCollProperty = "Assets/Resources/DataCollection/" + FileName + "-Coll.csv";
        string pathCollProperty = "Assets/Resources/DataCollection/propsColliders.csv";

        string pathPosOrientColl = "Assets/Resources/DataCollection/" + FileName + "pos-orient.csv";
        string pathCollNames = "Assets/Resources/DataCollection/namesColliders.csv";

		StreamReader sr = new StreamReader(path, true);
		if (sr.Peek() > -1) 
        {
            string line = sr.ReadToEnd();     
            dataSplit = line.Split('\n');
        }

        StreamReader srCollProp = new StreamReader(pathCollProperty, true);
        if (srCollProp.Peek() > -1) 
        {
            string line = srCollProp.ReadToEnd();     
            dataCollProp = line.Split('\n');
        }
        collProperties = dataCollProp[1].Split(';');

        StreamReader srCollPos = new StreamReader(pathPosOrientColl, true);
        if (srCollPos.Peek() > -1) 
        {
            string line = srCollPos.ReadToEnd();     
            dataCollPos = line.Split('\n');
        }
        collPosOrient = dataCollPos[2].Split(';');

        frame_max = (dataSplit.Length - 1);

        StreamReader srNames = new StreamReader(pathCollNames, true);
        if (srNames.Peek() > -1) 
        {
            string line = srNames.ReadToEnd();     
            dataNames = line.Split('\n');
        }

        for(int i = 0; i < 19; i++)
        {
            leftHandColl.transform.GetChild(i).name = dataNames[i];
            rightHandColl.transform.GetChild(i).name = dataNames[i];

            leftHandColl.transform.GetChild(i).transform.gameObject.AddComponent<CapsuleCollider>();
            rightHandColl.transform.GetChild(i).transform.gameObject.AddComponent<CapsuleCollider>();
        
            leftHandColl.transform.GetChild(i).transform.gameObject.AddComponent<Rigidbody>();
            rightHandColl.transform.GetChild(i).transform.gameObject.AddComponent<Rigidbody>();
        
            leftHandColl.transform.GetChild(i).transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            rightHandColl.transform.GetChild(i).transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            leftHandColl.transform.GetChild(i).transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
            rightHandColl.transform.GetChild(i).transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
            leftHandColl.transform.GetChild(i).transform.gameObject.GetComponent<Rigidbody>().mass = 1.0f;
            rightHandColl.transform.GetChild(i).transform.gameObject.GetComponent<Rigidbody>().mass = 1.0f;
        }

        for(int i = 0; i < (19)*13; i = i+13)
        {
            int idProp = int.Parse(collProperties[i]);
            leftHandColl.transform.GetChild(idProp).transform.gameObject.GetComponent<CapsuleCollider>().center = new Vector3(float.Parse((collProperties[i+1]), CultureInfo.InvariantCulture), float.Parse((collProperties[i+2]), CultureInfo.InvariantCulture), float.Parse((collProperties[i+3]), CultureInfo.InvariantCulture));
            leftHandColl.transform.GetChild(idProp).transform.gameObject.GetComponent<CapsuleCollider>().radius = float.Parse((collProperties[i+4]), CultureInfo.InvariantCulture);
            leftHandColl.transform.GetChild(idProp).transform.gameObject.GetComponent<CapsuleCollider>().height = float.Parse((collProperties[i+5]), CultureInfo.InvariantCulture);
            leftHandColl.transform.GetChild(idProp).transform.gameObject.GetComponent<CapsuleCollider>().direction = int.Parse(collProperties[i+6]);

            rightHandColl.transform.GetChild(idProp).transform.gameObject.GetComponent<CapsuleCollider>().center = new Vector3(float.Parse((collProperties[i+7]), CultureInfo.InvariantCulture), float.Parse((collProperties[i+8]), CultureInfo.InvariantCulture), float.Parse((collProperties[i+9]), CultureInfo.InvariantCulture));
            rightHandColl.transform.GetChild(idProp).transform.gameObject.GetComponent<CapsuleCollider>().radius = float.Parse((collProperties[i+10]), CultureInfo.InvariantCulture);
            rightHandColl.transform.GetChild(idProp).transform.gameObject.GetComponent<CapsuleCollider>().height = float.Parse((collProperties[i+11]), CultureInfo.InvariantCulture);
            rightHandColl.transform.GetChild(idProp).transform.gameObject.GetComponent<CapsuleCollider>().direction = int.Parse(collProperties[i+12]);
        }
        
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        switch(etat)
        {
            case 0:
                
                Debug.Log("Frame: " + frame + " / " + frame_max);

			  	frame = frame + 1;
                	if(frame >= frame_max)
	                {
	                    Debug.Log("FRAMEMAX");
	                    etat = 1;
	                }
               	splitSentence = dataSplit[frame].Split(';');
                // collProperties = dataCollProp[frame].Split(';');
                collPosOrient = dataCollPos[frame].Split(';');
            	
                posUser.position = new Vector3(float.Parse((splitSentence[1]), CultureInfo.InvariantCulture)/1000, float.Parse((splitSentence[2]), CultureInfo.InvariantCulture)/1000, float.Parse((splitSentence[3]), CultureInfo.InvariantCulture)/1000);
                posUser.eulerAngles = new Vector3(float.Parse((splitSentence[4]), CultureInfo.InvariantCulture)/1000, float.Parse((splitSentence[5]), CultureInfo.InvariantCulture)/1000, float.Parse((splitSentence[6]), CultureInfo.InvariantCulture)/1000);

                for(int i = 7; i < (handLeft.GetComponent<OVRCustomSkeleton>().CustomBones.Count)*13; i = i+13)
                {
                    boneId = int.Parse(splitSentence[i]);
                    handLeft.GetComponent<OVRCustomSkeleton>().CustomBones[boneId].transform.position = new Vector3(float.Parse((splitSentence[i+1]), CultureInfo.InvariantCulture), float.Parse((splitSentence[i+2]), CultureInfo.InvariantCulture), float.Parse((splitSentence[i+3]), CultureInfo.InvariantCulture));
                    handLeft.GetComponent<OVRCustomSkeleton>().CustomBones[boneId].transform.eulerAngles = new Vector3(float.Parse((splitSentence[i+4]), CultureInfo.InvariantCulture), float.Parse((splitSentence[i+5]), CultureInfo.InvariantCulture), float.Parse((splitSentence[i+6]), CultureInfo.InvariantCulture));
                    
                    handRight.GetComponent<OVRCustomSkeleton>().CustomBones[boneId].transform.position = new Vector3(float.Parse((splitSentence[i+7]), CultureInfo.InvariantCulture), float.Parse((splitSentence[i+8]), CultureInfo.InvariantCulture), float.Parse((splitSentence[i+9]), CultureInfo.InvariantCulture));
                    handRight.GetComponent<OVRCustomSkeleton>().CustomBones[boneId].transform.eulerAngles = new Vector3(float.Parse((splitSentence[i+10]), CultureInfo.InvariantCulture), float.Parse((splitSentence[i+11]), CultureInfo.InvariantCulture), float.Parse((splitSentence[i+12]), CultureInfo.InvariantCulture));    
                }

                for(int i = 0; i < (19)*13; i = i+13)
                {
                    int idColl = int.Parse(collPosOrient[i]);

                    leftHandColl.transform.GetChild(idColl).transform.position = new Vector3(float.Parse((collPosOrient[i+1]), CultureInfo.InvariantCulture), float.Parse((collPosOrient[i+2]), CultureInfo.InvariantCulture), float.Parse((collPosOrient[i+3]), CultureInfo.InvariantCulture));
                    leftHandColl.transform.GetChild(idColl).transform.eulerAngles = new Vector3(float.Parse((collPosOrient[i+4]), CultureInfo.InvariantCulture), float.Parse((collPosOrient[i+5]), CultureInfo.InvariantCulture), float.Parse((collPosOrient[i+6]), CultureInfo.InvariantCulture));
                    
                    rightHandColl.transform.GetChild(idColl).transform.position = new Vector3(float.Parse((collPosOrient[i+7]), CultureInfo.InvariantCulture), float.Parse((collPosOrient[i+8]), CultureInfo.InvariantCulture), float.Parse((collPosOrient[i+9]), CultureInfo.InvariantCulture));
                    rightHandColl.transform.GetChild(idColl).transform.eulerAngles = new Vector3(float.Parse((collPosOrient[i+10]), CultureInfo.InvariantCulture), float.Parse((collPosOrient[i+11]), CultureInfo.InvariantCulture), float.Parse((collPosOrient[i+12]), CultureInfo.InvariantCulture));
                    
                    // leftHandColl.transform.GetChild(idColl).transform.gameObject.GetComponent<CapsuleCollider>().center = new Vector3(float.Parse((collProperties[i+1]), CultureInfo.InvariantCulture), float.Parse((collProperties[i+2]), CultureInfo.InvariantCulture), float.Parse((collProperties[i+3]), CultureInfo.InvariantCulture));
                    // leftHandColl.transform.GetChild(idColl).transform.gameObject.GetComponent<CapsuleCollider>().radius = float.Parse((collProperties[i+4]), CultureInfo.InvariantCulture);
                    // leftHandColl.transform.GetChild(idColl).transform.gameObject.GetComponent<CapsuleCollider>().height = float.Parse((collProperties[i+5]), CultureInfo.InvariantCulture);
                    // leftHandColl.transform.GetChild(idColl).transform.gameObject.GetComponent<CapsuleCollider>().direction = int.Parse(collProperties[i+6]);
// 
                    // rightHandColl.transform.GetChild(idColl).transform.gameObject.GetComponent<CapsuleCollider>().center = new Vector3(float.Parse((collProperties[i+7]), CultureInfo.InvariantCulture), float.Parse((collProperties[i+8]), CultureInfo.InvariantCulture), float.Parse((collProperties[i+9]), CultureInfo.InvariantCulture));
                    // rightHandColl.transform.GetChild(idColl).transform.gameObject.GetComponent<CapsuleCollider>().radius = float.Parse((collProperties[i+10]), CultureInfo.InvariantCulture);
                    // rightHandColl.transform.GetChild(idColl).transform.gameObject.GetComponent<CapsuleCollider>().height = float.Parse((collProperties[i+11]), CultureInfo.InvariantCulture);
                    // rightHandColl.transform.GetChild(idColl).transform.gameObject.GetComponent<CapsuleCollider>().direction = int.Parse(collProperties[i+12]);
                }


            break;

            case 1:
            	Debug.Break();
            	Debug.Log("STOP");
            	sr.Close();
                srCollPos.Close();
                srCollProp.Close();
                Debug.Break();
                
                // if(proxy.contribDistance > maxContrib)
                // {
                //     Debug.Log("STOP");
                //     Debug.Break();
                //     sr.Close();
                //     Debug.Break();
                //     Application.Quit();

                // }
                // else
                // {
                //     Debug.Log("Change Contrib");
                //     proxy.contribDistance = proxy.contribDistance + incremContrib;
                //     frame = 0;
                //     etat = 0;
                // }
            break;
        }

	}
}
