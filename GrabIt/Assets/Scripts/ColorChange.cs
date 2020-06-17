using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
	public GameObject hand;
	private float distance;
	private Mesh thisMesh;
	private Vector3[] thisVertices, closestPointPerVertex;
	private Color[] colors;
	private Vector3 worldVertex;

	private Collider handColliders;
	private Collider[] handCollili;

    void Start()
    {
    	thisMesh = this.GetComponent<MeshFilter>().sharedMesh;
    	thisVertices = this.GetComponent<MeshFilter>().sharedMesh.vertices;
    	colors = new Color[thisVertices.Length];
		closestPointPerVertex = new Vector3[thisVertices.Length];
		hand = GameObject.Find("Hand");
    }

    void Update()
    {
    	for(int j = 0; j < hand.GetComponentsInChildren<Collider>().Length; j++)
    	{
    		handColliders = hand.GetComponentsInChildren<Collider>()[j];
			// print(handColliders);
    	}

        for(int k = 0; k < thisVertices.Length; k++)
        {

        	worldVertex = transform.TransformPoint(thisVertices[k]);

	        closestPointPerVertex[k] = handColliders.ClosestPoint(worldVertex);
			distance = Vector3.Distance(closestPointPerVertex[k], worldVertex);

			if(distance >= 0.99f)
			{
				colors[k] = Color.Lerp(Color.white, Color.yellow, (1-distance));
			}
			if(distance < 0.99f)
			{
				colors[k] = Color.Lerp(Color.yellow, Color.red, (1-distance));
			}

		}
		thisMesh.colors = colors;
    }

    public void OnDrawGizmos()
    {

    	for(int j = 0; j < hand.GetComponentsInChildren<Collider>().Length; j++)
    	{
			handColliders = hand.GetComponentsInChildren<Collider>()[j];
    	}

        for(int i = 0; i < thisVertices.Length; i++)
        {
        	worldVertex = transform.TransformPoint(thisVertices[i]);
	        closestPointPerVertex[i] = handColliders.ClosestPoint(worldVertex);

        	// Gizmos.DrawSphere(worldVertex, 0.1f);
	        // Gizmos.DrawWireSphere(closestPointPerVertex[i], 0.05f);
			Debug.DrawRay(closestPointPerVertex[i], (worldVertex - closestPointPerVertex[i]), Color.blue);

		}
    }
}
