using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.IO;
using System.Globalization;

public class SimulationHand : MonoBehaviour
{
	private static float time0;
	private string path;
	public string FileName;
    private Transform posUser;

    private GameObject handLeft, handRight;

    private string[] dataSplit;
    private string[] splitSentence;
    private int frame = 1;

    private float frame_max;

    private int etat;
    private int boneId;

    private StreamReader sr;


	
	void Start()
	{
		posUser = GameObject.Find("Main Camera").transform;
		handLeft = GameObject.Find("OVRCustomHandPrefab_L");
		handRight = GameObject.Find("OVRCustomHandPrefab_R");

		string path = "Assets/Resources/DataCollection/" + FileName + ".csv";
		StreamReader sr = new StreamReader(path, true);
		if (sr.Peek() > -1) 
        {
            string line = sr.ReadToEnd();     
            dataSplit = line.Split('\n');
        }

		frame_max = (dataSplit.Length - 1); //952
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
	
            	for(int j = 0; j < splitSentence.Length; j++)
            	{
            	    splitSentence[j] = splitSentence[j].Replace("(","");
            	    splitSentence[j] = splitSentence[j].Replace(")", "");
            	}

            	posUser.position = new Vector3(float.Parse((splitSentence[1].Split(','))[0], CultureInfo.InvariantCulture)/1000, float.Parse((splitSentence[1].Split(','))[1], CultureInfo.InvariantCulture)/1000, float.Parse((splitSentence[1].Split(','))[2], CultureInfo.InvariantCulture)/1000);
            	posUser.eulerAngles = new Vector3(float.Parse((splitSentence[2].Split(','))[0], CultureInfo.InvariantCulture), float.Parse((splitSentence[2].Split(','))[1], CultureInfo.InvariantCulture), float.Parse((splitSentence[2].Split(','))[2], CultureInfo.InvariantCulture));

            	for(int i = 3; i < (handLeft.GetComponent<OVRCustomSkeleton>().CustomBones.Count)*5; i = i+5)
            	{
            		boneId = int.Parse(splitSentence[i]);
            		handLeft.GetComponent<OVRCustomSkeleton>().CustomBones[boneId].transform.position = new Vector3(float.Parse((splitSentence[i+1].Split(','))[0], CultureInfo.InvariantCulture)/1000, float.Parse((splitSentence[i+1].Split(','))[1], CultureInfo.InvariantCulture)/1000, float.Parse((splitSentence[i+1].Split(','))[2], CultureInfo.InvariantCulture)/1000);
            		handLeft.GetComponent<OVRCustomSkeleton>().CustomBones[boneId].transform.eulerAngles = new Vector3(float.Parse((splitSentence[i+2].Split(','))[0], CultureInfo.InvariantCulture), float.Parse((splitSentence[i+2].Split(','))[1], CultureInfo.InvariantCulture), float.Parse((splitSentence[i+2].Split(','))[2], CultureInfo.InvariantCulture));
					
					handRight.GetComponent<OVRCustomSkeleton>().CustomBones[boneId].transform.position = new Vector3(float.Parse((splitSentence[i+3].Split(','))[0], CultureInfo.InvariantCulture)/1000, float.Parse((splitSentence[i+3].Split(','))[1], CultureInfo.InvariantCulture)/1000, float.Parse((splitSentence[i+3].Split(','))[2], CultureInfo.InvariantCulture)/1000);
            		handRight.GetComponent<OVRCustomSkeleton>().CustomBones[boneId].transform.eulerAngles = new Vector3(float.Parse((splitSentence[i+4].Split(','))[0], CultureInfo.InvariantCulture), float.Parse((splitSentence[i+4].Split(','))[1], CultureInfo.InvariantCulture), float.Parse((splitSentence[i+4].Split(','))[2], CultureInfo.InvariantCulture));

            	}
            	
                // target.nbTargetInScene = int.Parse(splitSentence[11]);
                // target.targetToTouch = int.Parse(splitSentence[13]);

                // for(int i = target.nbTargetInScene; i < OOI.Length; i++)
                // {
                //     OOI[i].gameObject.SetActive(false);
                // }

                // for(int i = 0; i < target.nbTargetInScene; i++)
                // {

                //     OOI[i].gameObject.SetActive(true);
                //     if(i != target.targetToTouch)
                //     {
                //         OOI[i].toBePicked = false;
                //     }
                //     OOI[target.targetToTouch].toBePicked = true;
                // }

                // for(int i = 0; i <  target.nbTargetInScene; i++) // OU OOI.Length
                // {
                //     OOI[i].transform.position = new Vector3(float.Parse((splitSentence[2*i+15].Split(','))[0], CultureInfo.InvariantCulture), float.Parse((splitSentence[2*i+15].Split(','))[1], CultureInfo.InvariantCulture), float.Parse((splitSentence[2*i+15].Split(','))[2], CultureInfo.InvariantCulture));
                // }
                


            break;

            case 1:
            	Debug.Break();
            	Debug.Log("STOP");
            	sr.Close();
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
