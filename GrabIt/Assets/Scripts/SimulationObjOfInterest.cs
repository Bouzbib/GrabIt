using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.IO;
using System.Globalization;

public class SimulationObjOfInterest : MonoBehaviour
{
	private static float time0;
	private string path, FileName;
	public SimulationHand fromHand;

    public ObjectOfInterest[] OOI;

    private string[] dataSplit;
    private string[] splitSentence;
    private int frame = 1;

    private float frame_max;

    private int etat;
    private int boneId;

    private StreamReader sr;	
    // Start is called before the first frame update
    void Start()
    {
    	fromHand = FindObjectOfType<SimulationHand>();
    	FileName = fromHand.FileName;
    	OOI = FindObjectsOfType<ObjectOfInterest>();
		for(int i = 0; i < OOI.Length; i++)
		{
			OOI[i].GetComponent<Rigidbody>().isKinematic = true;
			OOI[i].GetComponent<Rigidbody>().useGravity = false;
		}

		string path = "Assets/Resources/DataCollection/" + FileName + "-OOI.csv";       
        StreamReader sr = new StreamReader(path, true);
		if (sr.Peek() > -1) 
        {
            string line = sr.ReadToEnd();     
            dataSplit = line.Split('\n');
        }
        frame_max = (dataSplit.Length - 1);
    }

    // Update is called once per frame
    void Update()
    {
    	etat = fromHand.etat;
        switch(etat)
        {
            case 0:
               	frame = frame + 1;
                
               	splitSentence = dataSplit[frame].Split(';');


                for(int i = 1; i < (OOI.Length*7); i = i+7)
                {
                    int OOI_id = int.Parse(splitSentence[i]);
                    OOI[OOI_id].transform.position = new Vector3(float.Parse((splitSentence[i+1]), CultureInfo.InvariantCulture), float.Parse((splitSentence[i+2]), CultureInfo.InvariantCulture), float.Parse((splitSentence[i+3]), CultureInfo.InvariantCulture));
                    OOI[OOI_id].transform.eulerAngles = new Vector3(float.Parse((splitSentence[i+4]), CultureInfo.InvariantCulture), float.Parse((splitSentence[i+5]), CultureInfo.InvariantCulture), float.Parse((splitSentence[i+6]), CultureInfo.InvariantCulture));
                }

                

// NOW ADD OBJECTS (CAN REMOVE COLLIDERS/RB IF NEEDED )
                
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
            	sr.Close();
			break;
        }
    }
}
