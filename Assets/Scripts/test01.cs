using UnityEngine;
using System.Collections;


//using System.Linq;

using System.Collections.Generic;//C# 包中的类

public class test01 : MonoBehaviour {

	private int width = 40;
	private int height = 40;

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

		Debug.Log ("Maze start");
		Maze maze = new Maze (width,height);
		maze.perfect = true;
		maze.braid = false;

		maze.init ();

		maze.startNode = maze.getRandomNode ();
		do {
			maze.endNode = maze.getRandomNode ();
		} while(maze.startNode == maze.endNode);
			
		maze.generate();

		List<List<Node>> grid = maze.grid;

		List<Node> ln = null;
		Node n = null;
		for(int i = 0;i<grid.Count;i++){
			ln = grid [i];
			for(int j = 0;j<ln.Count;j++){
				n = ln [j];
				initGrid(n);
			}
		}

		Debug.Log ("Maze end");

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	private void initGrid(Node node){
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.localPosition = new Vector3 (9.0f*node.x,0.5F,9.0f*node.y);
		cube.transform.localScale = new Vector3(9.0F, 1.0F, 9.0F);  // w z h
		cube.GetComponent<MeshRenderer>().material.color = new Color(1f*node.x/10, 0.92f*node.y/10, 0.016f, 1f);

		bool left = (node.value & Maze.Direction.W) != Maze.Direction.W;
		bool top = (node.value & Maze.Direction.N) != Maze.Direction.N;

		if(left&&top){
			GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube1.transform.localPosition = new Vector3 ((9.0f*node.x),1.5F,9.0f*node.y);
			cube1.transform.localScale = new Vector3(1.0F, 1.0F, 9.0F);  // w z h
			cube1.GetComponent<MeshRenderer>().material.color = new Color(1f, 0.92f, 0.016f*node.y/10, 1f*node.x/10);


			GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube2.transform.localPosition = new Vector3 ((9.0f*node.x),1.5F,9.0f*node.y);
			cube2.transform.localScale = new Vector3(9.0F, 1.0F, 1.0F);  // w z h
			cube2.GetComponent<MeshRenderer>().material.color = new Color(1f, 0.92f, 0.016f*node.y/10, 1f*node.x/10);
		}

	}
}

public class Maze : MazeBase{
//	new public int width = 20;
//	new public int height = 20;

	public bool perfect= true;
	public bool braid = false;
	public bool checkOver = false;

	public bool foundEndNode;

	// 默认构造
	public Maze(){

	}

	// 初始化
	public Maze(int width,int height){
		this.width = width;
		this.height = height;
	}

	// 这里就发生了重写,也可以说是隐藏了父类的方法. 这样做了之后就不能再使用父类的同名方法了.
	// 获取邻居
	public override List<object> getNeighbor(){
		List<List<object>> list = (List<List<object>>)this.neighbors["list"];

		System.Random random = new System.Random();
		int key = random.Next(0,list.Count);

		List<object> n = list[key];
		return n;
	}
		

	// 验证 附近的节点是否有效
	public override bool isValid(Node nearNode,Node node,int dir) {
		if (nearNode == null  || nearNode.value != 0) {
			return false;
		}
		if (nearNode.value == 0) {
			return true;
		}
		if (this.perfect || this.braid) {
			return false;
		}
//		int c = nearNode.x,
//		r = nearNode.y;
		// 用于生成一种非Perfect迷宫
//		this.checkCount[c + "-" + r] = this.checkCount[c + "-" + r] || 0;
//		var count = ++this.checkCount[c + "-" + r];
//		return Math.random() < 0.3 && count < 3;
		return false;
	}

	public override void beforeBacktrace(){
		if (!this.braid) {
			return;
		}

	} 

	public override void updateCurrent(){
		if (this.braid) {
			return;
		}
		System.Random random = new System.Random();
		// 每步有 10% 的概率 进行回溯
		if (random.Next(1,10) == 1) {
			this.backtrace();
		}
	}

	public override int getTraceIndex(){
		int len = this.trace.Count;

		if (this.braid) {
			return len - 1;
		}

		// 按一定的概率随机选择回溯策略
		System.Random random = new System.Random();
		int r = random.Next (0, 9)/10;
		var idx = 0;
		if (r < 0.5) {
			idx = len - 1;
		} else if (r < 0.7) {
			idx = len >> 1;
		} else if (r < 0.8) {
			idx = len * random.Next (0, 9)/10 >> 0;
		}
		return idx;
	}

	public override void afterGenrate(){
		if (this.braid && this.getRoadCount(this.startNode)<2) {
			this.currentDirCount = 1000;
			this.setCurrent(this.startNode);
			this.nextStep();
		}
	}

	public override bool isOver(){
		if (!this.checkOver) {
			return false;
		}
		if (this.currentNode == this.endNode) {
			this.foundEndNode = true;
		}
		// 当探索到迷宫终点, 且探索了至少一半的区域时,终止迷宫的生成
		if (this.foundEndNode && this.stepCount >= this.size / 2) {
			return true;
		}
		return false;
	}

}