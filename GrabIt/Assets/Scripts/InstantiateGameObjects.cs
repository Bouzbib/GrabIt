using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateGameObjects : MonoBehaviour
{
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
    public int nbBlocMax = 2;
    public int state = 0;

    public Material phantomMaterialRed, phantomMaterialGreen;

    private Transform[] walls;
    private TextMesh[] otherwalls;
	public GameObject Text3D;

	public bool realGame;



    // Start is called before the first frame update
    void Start()
    {
    	posOrigin = new Vector3(-0.05f, -0.54f, 0.335f);
    	
    	tasks = new string[]{"Hold", "Push", "Pull", "Move Over", "Raise", "Push Down", "Fit", "Roll"};
    	//0:Hold, 1:Push: 2:Pull: 3:MoveOver; 4:Raise; 5:PsuhDown; 6:Fit; 7:Roll 
    	objectTypes = new GameObject[]{GameObject.Find("ObjectsOfInterest/Cylinder"), GameObject.Find("ObjectsOfInterest/Cylinder90"), GameObject.Find("ObjectsOfInterest/Sphere"), GameObject.Find("ObjectsOfInterest/Capsule")};//, GameObject.Find("ObjectsOfInterest/Cylinder90")}; // j
    	//0:Cylinder, 1:Cube, 2:Sphere, 3:Capsule //, 4:LyingCylinder
		phantomObject = new GameObject[]{GameObject.Find("PhantomObjects/Cylinder"), GameObject.Find("PhantomObjects/Cylinder90"), GameObject.Find("PhantomObjects/Sphere"), GameObject.Find("PhantomObjects/Capsule")};//, GameObject.Find("ObjectsOfInterest/Cylinder90")};


    	for(int i = 0; i < objectTypes.Length; i++)
    	{
	    	objectTypes[i].SetActive(false);
	    	phantomObject[i].SetActive(false);
	    	// 0: cylinder; 1: cube; 2: sphere; 3: capsule
    	}

    	scales = new Vector3[4];
    	scales[0] = new Vector3(0.1f, 0.1f, 0.1f);
    	scales[1] = new Vector3(0.13f, 0.13f, 0.13f);
    	scales[2] = new Vector3(0.2f, 0.2f, 0.2f);
    	scales[3] = new Vector3(0.3f, 0.3f, 0.3f);

		posPhantoms = new Vector3[tasks.Length];
    	posPhantoms[0] = new Vector3(posOrigin.x, posOrigin.y, posOrigin.z);
    	posPhantoms[1] = new Vector3(posOrigin.x, posOrigin.y, posOrigin.z + 0.4f);
    	posPhantoms[2] = new Vector3(posOrigin.x, posOrigin.y, posOrigin.z - 0.4f);
    	posPhantoms[3] = new Vector3(posOrigin.x + 0.8f, posOrigin.y, posOrigin.z - 0.5f);
		posPhantoms[4] = new Vector3(posOrigin.x, posOrigin.y + 0.8f, posOrigin.z);
		posPhantoms[5] = new Vector3(posOrigin.x, posOrigin.y - 0.05f, posOrigin.z);
		posPhantoms[6] = new Vector3(posOrigin.x - 0.8f, posOrigin.y, posOrigin.z + 0.5f);
		posPhantoms[7] = new Vector3(posOrigin.x + 0.8f, posOrigin.y, posOrigin.z);

		configException = new List<int>();
		config = Random.Range(0, tasks.Length*objectTypes.Length*scales.Length);

    	walls = GameObject.Find("Walls").GetComponentsInChildren<Transform>();
		Text3D = GameObject.Find("Text3D");
		otherwalls = new TextMesh[walls.Length];

    	for (int i = 2; i < otherwalls.Length; i++)
    	{
	    	otherwalls[i] = (TextMesh)Instantiate(Text3D, walls[i].gameObject.transform).GetComponent<TextMesh>();
	    	otherwalls[i].transform.position = walls[i].gameObject.transform.position;
	    	otherwalls[i].transform.eulerAngles = new Vector3(walls[i].gameObject.transform.eulerAngles.x, walls[i].gameObject.transform.eulerAngles.y - 90f, walls[i].gameObject.transform.eulerAngles.z);

	    	otherwalls[i].characterSize = 0.03f;
    	}


    }

    // Update is called once per frame
    void Update()
    {
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
		if(realGame)
		{
			objectTypes[objId].GetComponent<ColorChange>().graspContact = false;
			if(objectTypes[objId].GetComponent<ColorChange>().distance < 0.1f)
			{
				objectTypes[objId].GetComponent<ColorChange>().graspContact = true;
			}
		}
		else
		{
			objectTypes[objId].GetComponent<ColorChange>().graspContact = true;
		}

		switch(state)
		{
			case -1:
				config = Random.Range(0, tasks.Length*objectTypes.Length*scales.Length);
				while(configException.Contains(config))
				{
					config = Random.Range(0, tasks.Length*objectTypes.Length*scales.Length);
				}
				for(int i = 0; i < objectTypes.Length; i++)
				{
					objectTypes[i].GetComponent<ObjectOfInterest>().toBePicked = false;
					objectTypes[i].GetComponent<ObjectOfInterest>().collision = false;
					objectTypes[i].SetActive(false);
					phantomObject[i].SetActive(false);
				}
				StartCoroutine(CollisionWait());
				state = 0;

			break;

			case 0:
				configException.Add(config);
				
                objectTypes[objId].SetActive(true);
                objectTypes[objId].transform.position = new Vector3(posOrigin.x, posOrigin.y, posOrigin.z);
                objectTypes[objId].GetComponent<ObjectOfInterest>().collision = false;
                objectTypes[objId].GetComponent<ObjectOfInterest>().toBePicked = true;
                objectTypes[objId].transform.localScale = new Vector3(scaleObj.x, scaleObj.y, scaleObj.z);

                // phantomObject = (GameObject) Instantiate(objectTypes[objId], objToCatch.transform);
                phantomObject[objId].SetActive(true);
                phantomObject[objId].transform.position = new Vector3(posPhantoms[taskNumber].x, posPhantoms[taskNumber].y, posPhantoms[taskNumber].z);
                phantomObject[objId].transform.localScale = new Vector3(scaleObj.x, scaleObj.y, scaleObj.z);

                //Vector3(posPhantoms[taskNumber].position.x, posPhantoms[taskNumber].position.y, posPhantoms[taskNumber].position.z);
                phantomObject[objId].GetComponent<MeshRenderer>().material = phantomMaterialRed;
                phantomObject[objId].GetComponent<Collider>().enabled = false;
                state = 1;
			break;

			case 1:
				StartCoroutine(Consignes());
				Debug.Log("State = 1");
				if((Mathf.Abs(objectTypes[objId].transform.position.x - phantomObject[objId].transform.position.x) < 0.07f) && (Mathf.Abs(objectTypes[objId].transform.position.y - phantomObject[objId].transform.position.y) < 0.07f) && (Mathf.Abs(objectTypes[objId].transform.position.z - phantomObject[objId].transform.position.z) < 0.07f))
				{
					phantomObject[objId].GetComponent<MeshRenderer>().material = phantomMaterialGreen;
					if(objectTypes[objId].GetComponent<ColorChange>().graspContact && (Mathf.Abs(objectTypes[objId].transform.position.x - phantomObject[objId].transform.position.x) < 0.05f) && (Mathf.Abs(objectTypes[objId].transform.position.y - phantomObject[objId].transform.position.y) < 0.05f) && (Mathf.Abs(objectTypes[objId].transform.position.z - phantomObject[objId].transform.position.z) < 0.05f))
					{
						objectTypes[objId].GetComponent<ObjectOfInterest>().collision = true;

						if(configException.Count == tasks.Length*objectTypes.Length*scales.Length)
						{
							nbBloc = nbBloc + 1;
							configException.Clear();
							configException = new List<int>();
							if(nbBloc >= nbBlocMax)
							{
								state = 2;
							}
							else
							{
								state = -1;
							}
						}
						state = -1;
					}
				}

			break;

			case 2:
				StartCoroutine(EndOfTheGame());
			break;
		}


		// CONSTRAIN ON TASKS?
		// ADD CYLINDER 90DEGRES?
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
		objectTypes[objId].GetComponent<Renderer>().enabled = false;
		phantomObject[objId].GetComponent<Renderer>().enabled = false;

        yield return new WaitForSeconds(1);
		objectTypes[objId].GetComponent<Renderer>().enabled = true;
		phantomObject[objId].GetComponent<Renderer>().enabled = true;
		objectTypes[objId].GetComponent<ObjectOfInterest>().collision = false;
    }

    IEnumerator EndOfTheGame()
    {
    	for(int i = 0; i < objectTypes.Length; i++)
    	{
	    	objectTypes[i].SetActive(false);
	    	phantomObject[i].SetActive(false);
    	}

    	Debug.Log("The Game is Over. Thank you!");
    	yield return new WaitForSeconds(5);
        Debug.Break();
    	Application.Quit();
    }
}
