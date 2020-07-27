using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
	public GameObject rightHand, leftHand;
	private float distance, distanceL, distanceR;
	private Mesh thisMesh;
	private Vector3[] thisVertices, closestPointPerVertexRight, closestPointPerVertexLeft;
	private Color[] colors;
	private Vector3 worldVertex;

	private Collider handCollidersRight, handCollidersLeft;
	private Shader shader;

    void Start()
    {
		// hand = GameObject.Find("OVRCustomHandPrefab_R");
		// shader = Shader.Find("Particles/Priority Additive");
		shader = Shader.Find("Legacy Shaders/Particles/VertexLit Blended");
		this.GetComponent<MeshRenderer>().material.shader = shader;
		thisMesh = this.GetComponent<MeshFilter>().mesh;
		thisVertices = this.GetComponent<MeshFilter>().mesh.vertices;	
		colors = new Color[thisVertices.Length];
		closestPointPerVertexRight = new Vector3[thisVertices.Length];	
		closestPointPerVertexLeft = new Vector3[thisVertices.Length];
			
    }

    void Update()
    {
		// for(int i = 0; i < this.GetComponentsInChildren<Collider>().Length; i++)
		// {
		// 	this.GetComponentsInChildren<MeshRenderer>()[i].material.shader = shader;
		// 	thisMesh = this.GetComponentsInChildren<MeshFilter>()[i].mesh;
		// 	thisVertices = this.GetComponentsInChildren<MeshFilter>()[i].mesh.vertices;	
		// 	colors = new Color[thisVertices.Length];
		// 	closestPointPerVertex = new Vector3[thisVertices.Length];	
		// }

		
		// colors = new Color[thisVertices.Length];
		// closestPointPerVertex = new Vector3[thisVertices.Length];

    	for(int j = 0; j < leftHand.GetComponentsInChildren<Collider>().Length; j++)
    	{
    		handCollidersRight = rightHand.GetComponentsInChildren<Collider>()[j];
    		handCollidersLeft = leftHand.GetComponentsInChildren<Collider>()[j];
    	}

        for(int k = 0; k < thisVertices.Length; k++)
        {

        	worldVertex = transform.TransformPoint(thisVertices[k]);


	        closestPointPerVertexRight[k] = handCollidersRight.ClosestPoint(worldVertex);
			closestPointPerVertexLeft[k] = handCollidersLeft.ClosestPoint(worldVertex);
			distanceL = Vector3.Distance(closestPointPerVertexLeft[k], worldVertex);
			distanceR = Vector3.Distance(closestPointPerVertexRight[k], worldVertex);

			if(distanceL < distanceR)
			{
				distance = distanceL;

			}
			else
			{
				distance = distanceR;
			}

			if(distance >= 0.15f)
			{
				colors[k] = Color.Lerp(Color.white, Color.yellow, (1-distance));
			}

			if(distance < 0.15f)
			{
				colors[k] = Color.Lerp(Color.yellow, Color.red, (1-distance));
			}

		}
		thisMesh.colors = colors;
    }

// SHOW GIZMOOOOOOOS -> vertex between user's hands and OOI
  //   public void OnDrawGizmos()
  //   {

  //   	for(int j = 0; j < rightHand.GetComponentsInChildren<Collider>().Length; j++)
  //   	{
		// 	handCollidersRight = rightHand.GetComponentsInChildren<Collider>()[j];
		// 	handCollidersLeft = leftHand.GetComponentsInChildren<Collider>()[j];
  //   	}

  //       for(int i = 0; i < thisVertices.Length; i++)
  //       {
  //       	worldVertex = transform.TransformPoint(thisVertices[i]);
	 //        closestPointPerVertexRight[i] = handCollidersRight.ClosestPoint(worldVertex);
		// 	closestPointPerVertexLeft[i] = handCollidersLeft.ClosestPoint(worldVertex);

  //       	// Gizmos.DrawSphere(worldVertex, 0.1f);
	 //        // Gizmos.DrawWireSphere(closestPointPerVertex[i], 0.05f);
		// 	Debug.DrawRay(closestPointPerVertexRight[i], (worldVertex - closestPointPerVertexRight[i]), Color.blue);
		// 	Debug.DrawRay(closestPointPerVertexLeft[i], (worldVertex - closestPointPerVertexLeft[i]), Color.red);

		// }
  //   }
}
