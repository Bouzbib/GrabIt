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

    // public ObjectOfInterest[] OOI;

    private string[] dataSplit;
    private string[] splitSentence;
    private int frame = 1;

    private float frame_max;

    private int etat;
    private int boneId;

    private StreamReader sr;

    private string[] tasks;
    public GameObject[] objectTypes;
    private GameObject[] phantomObject;
    private Vector3[] scales;

    public List<int> configException;

    // private Transform[] posPhantoms;
    private Vector3[] posPhantoms;
    private Vector3 posOrigin;

    [Range(0, 127)]
    public int config;
    public Vector3 scaleObj;
    public GameObject objToCatch;
    public string taskToDo;
    private int taskNumber, objId;

    private int nbBloc = 0;
    public int nbBlocMax = 1;
    public int state = 0;

    public Material phantomMaterialRed, phantomMaterialGreen;

    private Transform[] walls;
    private TextMesh[] otherwalls;
    public GameObject Text3D;

    public GameObject resetButton, nextButton;
    private GameObject[] hands;

    private Vector3 previousAngles;

    private int nbTouch = 0;	
    // Start is called before the first frame update
    void Start()
    {
    	fromHand = FindObjectOfType<SimulationHand>();
    	FileName = fromHand.FileName;
  //   	OOI = FindObjectsOfType<ObjectOfInterest>();
		// for(int i = 0; i < OOI.Length; i++)
		// {
		// 	OOI[i].GetComponent<Rigidbody>().isKinematic = true;
		// 	OOI[i].GetComponent<Rigidbody>().useGravity = false;
		// }

		string path = "Assets/Resources/DataCollection/" + FileName + "-OOI.csv";       
        StreamReader sr = new StreamReader(path, true);
		if (sr.Peek() > -1) 
        {
            string line = sr.ReadToEnd();     
            dataSplit = line.Split('\n');
        }
        frame_max = (dataSplit.Length - 1);




        posOrigin = new Vector3(-0.05f, -0.50f, 0.335f);
        
        tasks = new string[]{"Hold", "Push", "Pull", "Move Over", "Raise", "Push Down", "Fit", "Roll"};
        //0:Hold, 1:Push: 2:Pull: 3:MoveOver; 4:Raise; 5:PushDown; 6:Fit; 7:Roll 
        objectTypes = new GameObject[]{GameObject.Find("ObjectsOfInterest/Cylinder"), GameObject.Find("ObjectsOfInterest/Cube"), GameObject.Find("ObjectsOfInterest/Sphere")};//, GameObject.Find("ObjectsOfInterest/Capsule")};//, GameObject.Find("ObjectsOfInterest/Cylinder90")}; // j
        //0:Cylinder, 1:Cube, 2:Sphere, 3:Capsule //, 4:LyingCylinder
        phantomObject = new GameObject[]{GameObject.Find("PhantomObjects/Cylinder"), GameObject.Find("PhantomObjects/Cube"), GameObject.Find("PhantomObjects/Sphere")};//, GameObject.Find("PhantomObjects/Capsule")};//, GameObject.Find("ObjectsOfInterest/Cylinder90")};


        for(int i = 0; i < objectTypes.Length; i++)
        {
            objectTypes[i].SetActive(false);
            phantomObject[i].SetActive(false);
            // 0: cylinder; 1: cube; 2: sphere; 3: capsule
        }

        // scales = new Vector3[4];
        scales = new Vector3[2];
        scales[0] = new Vector3(0.1f, 0.1f, 0.1f);
        scales[1] = new Vector3(0.13f, 0.13f, 0.13f);
        // scales[2] = new Vector3(0.2f, 0.2f, 0.2f);
        // scales[3] = new Vector3(0.3f, 0.3f, 0.3f);

        posPhantoms = new Vector3[tasks.Length];
        posPhantoms[0] = new Vector3(posOrigin.x, posOrigin.y, posOrigin.z);
        posPhantoms[1] = new Vector3(posOrigin.x, posOrigin.y, posOrigin.z + 0.2f);
        posPhantoms[2] = new Vector3(posOrigin.x, posOrigin.y, posOrigin.z - 0.2f);
        posPhantoms[3] = new Vector3(posOrigin.x + 0.6f, posOrigin.y, posOrigin.z - 0.15f);
        posPhantoms[4] = new Vector3(posOrigin.x, posOrigin.y + 0.5f, posOrigin.z);
        posPhantoms[5] = new Vector3(posOrigin.x, posOrigin.y - 0.1f, posOrigin.z);
        posPhantoms[6] = new Vector3(posOrigin.x - 0.6f, posOrigin.y, posOrigin.z + 0.35f);
        posPhantoms[7] = new Vector3(posOrigin.x + 0.8f, posOrigin.y, posOrigin.z);

        configException = new List<int>();

        walls = GameObject.Find("Walls").GetComponentsInChildren<Transform>();
        Text3D = GameObject.Find("Text3D");
        otherwalls = new TextMesh[walls.Length];

        for (int i = 2; i < otherwalls.Length; i++)
        {
            otherwalls[i] = (TextMesh)Instantiate(Text3D, walls[i].gameObject.transform).GetComponent<TextMesh>();
            otherwalls[i].transform.position = walls[i].gameObject.transform.position;
            otherwalls[i].transform.eulerAngles = new Vector3(walls[i].gameObject.transform.eulerAngles.x, walls[i].gameObject.transform.eulerAngles.y - 90f, walls[i].gameObject.transform.eulerAngles.z);

            otherwalls[i].characterSize = 0.02f;
        }

        resetButton = GameObject.Find("ResetButton");
        nextButton = GameObject.Find("NextButton");
        hands = new GameObject[]{GameObject.Find("OVRCustomHandPrefab_L"), GameObject.Find("OVRCustomHandPrefab_R")};

    }

    // Update is called once per frame
    void Update()
    {
    	etat = fromHand.etat;

        objectTypes[objId].GetComponent<ColorChange>().graspContact = false;
        if(objectTypes[objId].GetComponent<ColorChange>().distance < 0.08f)
        {
            objectTypes[objId].GetComponent<ColorChange>().graspContact = true;
        }

        switch(etat)
        {
            case 0:
               	frame = frame + 1;
                
               	splitSentence = dataSplit[frame].Split(';');
                config = int.Parse(splitSentence[1]);

                for(int i = 2; i < (objectTypes.Length*7); i = i+7)
                {
                    int OOI_id = int.Parse(splitSentence[i]);
                    objectTypes[OOI_id].transform.position = new Vector3(float.Parse((splitSentence[i+1]), CultureInfo.InvariantCulture), float.Parse((splitSentence[i+2]), CultureInfo.InvariantCulture), float.Parse((splitSentence[i+3]), CultureInfo.InvariantCulture));
                    objectTypes[OOI_id].transform.eulerAngles = new Vector3(float.Parse((splitSentence[i+4]), CultureInfo.InvariantCulture), float.Parse((splitSentence[i+5]), CultureInfo.InvariantCulture), float.Parse((splitSentence[i+6]), CultureInfo.InvariantCulture));
                }

                // Going through i, j, k and assigning tasks/objects/scales to configurations numbers.
                for(int k = 0; k < scales.Length; k++)
                {
                    if((config < tasks.Length*objectTypes.Length*(k+1)) && (config >= tasks.Length*objectTypes.Length*k))
                    {
                        taskToDo = tasks[config%tasks.Length];
                        taskNumber = config%tasks.Length;
                        for(int j = 0; j < objectTypes.Length; j++)
                        {
                            if((config < tasks.Length*(objectTypes.Length*k + j+1)) && (config >= tasks.Length*(objectTypes.Length*k + j)))
                            {
                                objToCatch = objectTypes[j];
                                objId = j;
                            }
                        }
                        scaleObj = scales[k];
                    }
                }
                if(configException.Contains(config))
                {
                    // DO NOTHIIIING
                }
                else
                {
                    configException.Add(config);
                }

                for(int i = 0; i < objectTypes.Length; i++)
                {
                    if(i != objId)
                    {
                        objectTypes[i].SetActive(false);
                        phantomObject[i].SetActive(false);
                    }
                }
                
                objectTypes[objId].SetActive(true);
                objectTypes[objId].GetComponent<Rigidbody>().isKinematic = true;
                objectTypes[objId].GetComponent<ObjectOfInterest>().collision = false;
                objectTypes[objId].GetComponent<ObjectOfInterest>().toBePicked = true;
                objectTypes[objId].transform.localScale = new Vector3(scaleObj.x, scaleObj.y, scaleObj.z);

                phantomObject[objId].SetActive(true);
                phantomObject[objId].transform.position = new Vector3(posPhantoms[taskNumber].x, posPhantoms[taskNumber].y, posPhantoms[taskNumber].z);
                phantomObject[objId].transform.localScale = new Vector3(scaleObj.x, scaleObj.y, scaleObj.z);

                phantomObject[objId].GetComponent<MeshRenderer>().material = phantomMaterialRed;
                phantomObject[objId].GetComponent<Collider>().enabled = false;
                previousAngles = new Vector3(objectTypes[objId].transform.eulerAngles.x, objectTypes[objId].transform.eulerAngles.y, objectTypes[objId].transform.eulerAngles.z);
                StartCoroutine(Consignes());

                if((Mathf.Abs(objectTypes[objId].transform.position.x - phantomObject[objId].transform.position.x) < 0.07f) && (Mathf.Abs(objectTypes[objId].transform.position.y - phantomObject[objId].transform.position.y) < 0.07f) && (Mathf.Abs(objectTypes[objId].transform.position.z - phantomObject[objId].transform.position.z) < 0.07f))
                {
                    phantomObject[objId].GetComponent<MeshRenderer>().material = phantomMaterialGreen;
                    if(objectTypes[objId].GetComponent<ColorChange>().graspContact)// && (Mathf.Abs(objectTypes[objId].transform.position.x - phantomObject[objId].transform.position.x) < 0.05f) && (Mathf.Abs(objectTypes[objId].transform.position.y - phantomObject[objId].transform.position.y) < 0.05f) && (Mathf.Abs(objectTypes[objId].transform.position.z - phantomObject[objId].transform.position.z) < 0.05f))
                    {
                        objectTypes[objId].GetComponent<ObjectOfInterest>().collision = true;
                        StartCoroutine(CollisionWait());
                        nbTouch = nbTouch + 1;
                    }
                }

            break;

            case 1:
            	sr.Close();
			break;
        }
    }

    IEnumerator Consignes()
    {
        yield return new WaitForSeconds(1);
        for (int i = 2; i < otherwalls.Length; i++)
        {
            otherwalls[i].text = taskToDo + " the WHITE " + objToCatch.gameObject.name + "\n into the RED " + objToCatch.gameObject.name + " until it becomes GREEN. \n Task #" + configException.Count + "/" + (tasks.Length*objectTypes.Length*scales.Length*nbBlocMax);
        }
        yield return new WaitForSeconds(5);
    }

    IEnumerator CollisionWait()
    {
        for (int i = 2; i < otherwalls.Length; i++)
        {
            otherwalls[i].text = "";
        }
        objectTypes[objId].GetComponent<Renderer>().enabled = false;
        phantomObject[objId].GetComponent<Renderer>().enabled = false;      
        yield return new WaitForSeconds(1);
        objectTypes[objId].GetComponent<Renderer>().enabled = true;
        phantomObject[objId].GetComponent<Renderer>().enabled = true;
        objectTypes[objId].GetComponent<ObjectOfInterest>().collision = false;
    }

}
