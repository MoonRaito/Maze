using UnityEngine;
using System.Collections;

public class test01 : MonoBehaviour {

	private float width = 40;
	private float height = 40;

	// Use this for initialization
	void Start () {
////		Debug.Log ("1");
//
//		//在场景中找到名为MyPlayer物体
////		player = GameObject.Find("MyPlayer");
//
////		transform
//		Transform tf = this.transform;
//		//		tf.position
//		tf.localPosition = new Vector3 (0.0f,-0.5F,0F);
//		tf.localScale = new Vector3(width, 1.0F, height);
//		//		this.GetComponent<MeshRenderer>().material.color = Color.black;
////		this.GetComponent<MeshRenderer>().material.color = new Color(1f, 0.92f, 0.016f, 1f);
//
//		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
//		cube.transform.localPosition = new Vector3 (0,0.5F,0);
//		cube.transform.localScale = new Vector3(1.0F, 1.0F, 1.0F);
//		//		this.GetComponent<MeshRenderer>().material.color = new Color(r, g, b, a);
//		cube.GetComponent<MeshRenderer>().material.color = new Color(1f, 0.92f, 0.016f, 1f);
////		mycube.renderer.material.color = Color.red;


//		Transform tf = this.transform;
//		tf.localPosition = new Vector3 (0.0f,-0.5F,0F);
//		tf.localScale = new Vector3(width, 1.0F, height);
//
//		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
//		cube.transform.localPosition = new Vector3 (0,0.5F,0);
//		cube.transform.localScale = new Vector3(1.0F, 1.0F, 1.0F);
//		cube.GetComponent<MeshRenderer>().material.color = new Color(1f, 0.92f, 0.016f, 1f);

		Debug.Log ("1");
		Maze maze = new Maze ();
		Debug.Log ("2");



	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public class Maze : MazeBase{
	public int width = 20;
	public int height = 20;

	public bool perfect= true;
	public bool braid = false;
	public bool checkOver = false;
}