using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectOfInterest : MonoBehaviour
{
	[Range (0, 1)]
	public float weight;

	private float weight_distance;
	private float weight_angle;

    public bool forcedWeight;

    private Color colorOOI;
    private MeshRenderer rend;

    private Transform user;

    private Vector3 targetToUser, closest_distance, closestPoint, targetToClosest, closest_angle, targetToCollider;
    public float ANGLE_MIN;

    public float REAL_MIN_DISTANCE, dot_angle;

    private Collider collThis, collUser;

    public bool toBePicked = false;
    public bool collision = false;

    // public GameObject handsUser;


    // public GameObject handsUserLeft;

    // Start is called before the first frame update
    void Start()
    {
        // user = GameObject.Find("CenterEyeAnchor").transform;
        user = GameObject.Find("Main Camera").transform;
        user.gameObject.AddComponent<Collider>();
    
        // handsUser = GameObject.Find("Fingers");
        // palm = GameObject.Find("Palm");
       // handsUserLeft = GameObject.Find("LeftHand");

    }

    // Update is called once per frame
    void Update()
    {
    	forcedWeight = false;
    	collThis = this.GetComponent<Collider>();
    	collUser = GameObject.Find("CenterEyeAnchor").GetComponent<Collider>();

// CALCUL DISTANCE ET ANGLE

    	targetToUser = this.transform.position - user.position;
    	closest_angle = collThis.ClosestPoint((user.position + user.forward*targetToUser.magnitude));

    	closestPoint = collThis.ClosestPointOnBounds(user.transform.position);
    	targetToClosest = user.position - closestPoint;

        Debug.DrawRay(closestPoint, targetToClosest, Color.blue);

    	closest_distance = collThis.ClosestPoint(closestPoint);
    	REAL_MIN_DISTANCE = Vector3.Distance(closest_distance, user.position);

    	weight_distance = 1/(1+REAL_MIN_DISTANCE);

    	targetToCollider = closest_angle - user.position;
    	ANGLE_MIN = Vector3.Angle(user.forward, targetToCollider);
    	dot_angle = Mathf.Exp((Vector3.Dot(user.forward, targetToCollider/targetToCollider.magnitude)*3)-3f);

        
        weight_distance = 1/(1+REAL_MIN_DISTANCE);
        weight_angle = dot_angle;

        if(!forcedWeight){
	        // weight = (1-proxy.contribDistance)*weight_angle + proxy.contribDistance*weight_distance;
        	weight = 0.825f*weight_angle + 0.175f*weight_distance;
        }
        else{
        	weight = 0;
        }


        // if(weight <= 0.1f){
        // 	weight = 0;

        // }

        // if(weight >= 0.8f){
        // 	weight = 1;
        // }



        collision = false;
        if(toBePicked)
        {
            
            // if(proxy.realTestGame)
            // {
                
            //      if((Mathf.Abs(this.gameObject.transform.GetChild(1).gameObject.transform.position.x - handsUser.transform.position.x) <= 0.05f) && (Mathf.Abs(this.gameObject.transform.GetChild(1).gameObject.transform.position.y - handsUser.transform.position.y) <= 0.05f) && (Mathf.Abs(this.gameObject.transform.GetChild(1).gameObject.transform.position.z - handsUser.transform.position.z) <= 0.05f))
            //     {
            //         collision = true;
            //     }

            // }
            // else
            // {
            //     if((Mathf.Abs(this.transform.position.x - user.transform.position.x) <= 0.1f) && (Mathf.Abs(this.transform.position.y - user.transform.position.y) <= 0.1f) && (Mathf.Abs(this.transform.position.z - user.transform.position.z) <= 0.1f))
            //     {
            //         collision = true;
            //     }   
            // }
        }
    }

    // public void OnDrawGizmos()
    // {
    // 	Gizmos.DrawWireSphere(closest_distance, 0.015f);
    // 	Gizmos.DrawSphere(closest_angle, 0.035f);
    //     GUIStyle style = new GUIStyle();
    //     style.normal.textColor = Color.black; 
    //     Handles.Label(transform.position, "Weight: " + weight.ToString("F2"), style);
    // }
}
