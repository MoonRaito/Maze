using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset; // 初始位置

	// Use this for initialization
	void Start () {
		offset = transform.position;// 摄像机的初始位置

		// yly
//		transform.position = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// 偏移的位置 加 初始位置
		transform.position = player.transform.position + offset;

		// yly
//		transform.position = player.transform.position;


	}
}
