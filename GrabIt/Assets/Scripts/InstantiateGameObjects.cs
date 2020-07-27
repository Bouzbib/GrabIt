using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateGameObjects : MonoBehaviour
{
	private string[] tasks;
	private GameObject[] objectTypes;
	private GameObject phantomObject;
	private Vector3[] scales;

	public List<int> configException;

    // private Transform[] posPhantoms;
    private Vector3[] posPhantoms;
    private Vector3 posOrigin;

    [Range(0, 127)][SerializeField]
    private int config;
    public Vector3 scaleObj;
    public GameObject objToCatch;
    public string taskToDo;
    private int taskNumber, objId;

    private int nbBloc = 0;
    public int nbBlocMax = 2;
    public int state = 0;

    public Material phantomMaterialRed, phantomMaterialGreen;

    // Start is called before the first frame update
    void Start()
    {
    	posOrigin = new Vector3(-0.05f, -0.3f, 0.335f);
    	
    	tasks = new string[]{"Hold", "Push", "Pull", "Move Over", "Raise", "Push Down", "Fit", "Roll"};
    	//0:Hold, 1:Push: 2:Pull: 3:MoveOver; 4:Raise; 5:PushDown; 6:Fit, 7:Roll
    	objectTypes = new GameObject[]{GameObject.Find("Cylinder"), GameObject.Find("Cube"), GameObject.Find("Sphere"), GameObject.Find("Capsule")}; // j
    	//0:Cylinder, 1:Cube, 2:Sphere, 3:Capsule

    	for(int i = 0; i < objectTypes.Length; i++)
    	{
	    	objectTypes[i].SetActive(false); 
	    	// 0: cylinder; 1: cube; 2: sphere; 3: capsule
    	}

    	scales = new Vector3[4];
    	scales[0] = new Vector3(0.1f, 0.1f, 0.1f);
    	scales[1] = new Vector3(0.13f, 0.13f, 0.13f);
    	scales[2] = new Vector3(0.2f, 0.2f, 0.2f);
    	scales[3] = new Vector3(0.3f, 0.3f, 0.3f);

		// posPhantoms = new Transform[tasks.Length];
  //   	posPhantoms[0].position = new Vector3(posOrigin.x, posOrigin.y, posOrigin.z);
  //   	posPhantoms[1].position = new Vector3(posOrigin.x, posOrigin.y, posOrigin.z + 0.5f);
  //   	posPhantoms[2].position = new Vector3(posOrigin.x, posOrigin.y, posOrigin.z - 0.5f);
  //   	posPhantoms[3].position = new Vector3(posOrigin.x + 0.8f, posOrigin.y, posOrigin.z - 0.5f);
		// posPhantoms[4].position = new Vector3(posOrigin.x, posOrigin.y + 0.5f, posOrigin.z);
		// posPhantoms[5].position = new Vector3(posOrigin.x, posOrigin.y - 0.2f, posOrigin.z);
		// posPhantoms[6].position = new Vector3(posOrigin.x - 0.8f, posOrigin.y, posOrigin.z + 0.5f);
		// posPhantoms[7].position = new Vector3(posOrigin.x + 0.8f, posOrigin.y, posOrigin.z);

		posPhantoms = new Vector3[tasks.Length];
    	posPhantoms[0] = new Vector3(posOrigin.x, posOrigin.y, posOrigin.z);
    	posPhantoms[1] = new Vector3(posOrigin.x, posOrigin.y, posOrigin.z + 0.5f);
    	posPhantoms[2] = new Vector3(posOrigin.x, posOrigin.y, posOrigin.z - 0.5f);
    	posPhantoms[3] = new Vector3(posOrigin.x + 0.8f, posOrigin.y, posOrigin.z - 0.5f);
		posPhantoms[4] = new Vector3(posOrigin.x, posOrigin.y + 0.5f, posOrigin.z);
		posPhantoms[5] = new Vector3(posOrigin.x, posOrigin.y - 0.2f, posOrigin.z);
		posPhantoms[6] = new Vector3(posOrigin.x - 0.8f, posOrigin.y, posOrigin.z + 0.5f);
		posPhantoms[7] = new Vector3(posOrigin.x + 0.8f, posOrigin.y, posOrigin.z);


		configException = new List<int>();
		config = Random.Range(0, tasks.Length*objectTypes.Length*scales.Length);
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

		switch(state)
		{
			case -1:
				config = Random.Range(0, tasks.Length*objectTypes.Length*scales.Length);
				while(configException.Contains(config))
				{
					config = Random.Range(0, tasks.Length*objectTypes.Length*scales.Length);
				}
				state = 0;

			break;

			case 0:
				configException.Add(config);
				
                objToCatch.SetActive(true);
                objectTypes[objId].transform.position = new Vector3(posOrigin.x, posOrigin.y, posOrigin.z);
                objectTypes[objId].GetComponent<ObjectOfInterest>().collision = false;
                objectTypes[objId].transform.localScale = new Vector3(scaleObj.x, scaleObj.y, scaleObj.z);

                // Debug.Log(posPhantoms[objId].transform);
                phantomObject = (GameObject) Instantiate(objectTypes[objId], objToCatch.transform);
                //new Vector3(posPhantoms[taskNumber].x, posPhantoms[taskNumber].y, posPhantoms[taskNumber].z));
                phantomObject.transform.position = new Vector3(posPhantoms[taskNumber].x, posPhantoms[taskNumber].y, posPhantoms[taskNumber].z);
                //Vector3(posPhantoms[taskNumber].position.x, posPhantoms[taskNumber].position.y, posPhantoms[taskNumber].position.z);
                phantomObject.GetComponent<MeshRenderer>().material = phantomMaterialRed;
                phantomObject.GetComponent<Rigidbody>().isKinematic = true;
                phantomObject.GetComponent<Collider>().enabled = false;
                state = 1;
			break;

			case 1:
				print(posPhantoms[taskNumber]);
				print(taskNumber);
				print(posPhantoms[0]);
			break;
		}



		// CONFIG = RANDOM NUMBER .. ADD TO EXCEPTION
		// DO n = 2 BLOCS
		// SWICH CASE
		// INSTANTIATE GameObj objToCatch, localScale = scaleObj, posOrig, Write "taskToDo" on wall
		// INSTANTIATE GameObj Phantom (mesh renderer transparent), pos = posPhantom[taskNumber] 
		// CASE 1
		// COLLISION - POS - POSGHOST = 0.05f
    }
}
