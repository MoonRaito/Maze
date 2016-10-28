using UnityEngine;
using System.Collections;


//using System.Linq;

using System.Collections.Generic;//C# 包中的类

public class test01 : MonoBehaviour {

	public int width = 20;
	public int height = 20;

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

		maze.setStart (0,0);
		maze.setEnd (maze.width-1,maze.height-1);
			
		maze.generate();

		List<List<Node>> grid = maze.grid;


//		List<Node> ln1 = new List<Node>();
//		Node node11 = new Node (0,0,2);
//		Node node12 = new Node (1,0,6);
//		Node node13 = new Node (2,0,12);
//		Node node14 = new Node (3,0,8);
//		ln1.Add (node11);
//		ln1.Add (node12);
//		ln1.Add (node13);
//		ln1.Add (node14);
//		List<Node> ln2 = new List<Node>();
//		Node node21 = new Node (0,1,3);
//		Node node22 = new Node (1,1,7);
//		Node node23 = new Node (2,1,12);
//		Node node24 = new Node (3,1,10);
//		ln2.Add (node21);
//		ln2.Add (node22);
//		ln2.Add (node23);
//		ln2.Add (node24);
//		List<Node> ln3 = new List<Node>();
//		Node node31 = new Node (0,2,3);
//		Node node32 = new Node (1,2,5);
//		Node node33 = new Node (2,2,8);
//		Node node34 = new Node (3,2,3);
//		ln3.Add (node31);
//		ln3.Add (node32);
//		ln3.Add (node33);
//		ln3.Add (node34);
//		List<Node> ln4 = new List<Node>();
//		Node node41 = new Node (0,3,5);
//		Node node42 = new Node (1,3,12);
//		Node node43 = new Node (2,3,12);
//		Node node44 = new Node (3,3,9);
//		ln4.Add (node41);
//		ln4.Add (node42);
//		ln4.Add (node43);
//		ln4.Add (node44);
//
//		grid = new List<List<Node>> ();
//		grid.Add (ln1);
//		grid.Add (ln2);
//		grid.Add (ln3);
//		grid.Add (ln4);



		initWidthHeight(grid);

		GameObject start = GameObject.CreatePrimitive(PrimitiveType.Cube);
		start.transform.localPosition = new Vector3 (9.0f*maze.startNode.x,1.5F,9.0f*maze.startNode.y);
		start.transform.localScale = new Vector3(1.0f, 9.0F, 1.0F);  // w z h
		start.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 0.92f, 1f);

		GameObject end = GameObject.CreatePrimitive(PrimitiveType.Cube);
		//		height.transform.localPosition = new Vector3 (9.0f*(h-1)+4.0f,1.5F,9.0f*((w-1)/2)+4.0F);
		end.transform.localPosition = new Vector3 (9.0f*maze.endNode.x,1.5F,9.0f*maze.endNode.y);
		end.transform.localScale = new Vector3(1.0F, 9.0F, 1.0F);  // w z h
		end.GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 0.016f, 0f);

		Debug.Log ("Maze end");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void initWidthHeight(List<List<Node>> grid){
		List<Node> ln = null;
		Node n = null;

		int w = grid.Count;
		int h = 0;
		for (int i = 0; i < w; i++) {
			ln = grid [i];
			h = ln.Count;
			for (int j = 0; j < h; j++) {
				n = ln [j];
				initGrid (n);
			}
		}

		GameObject width = GameObject.CreatePrimitive(PrimitiveType.Cube);
		width.transform.localPosition = new Vector3 (9.0f*(h/2),1.5F,9.0f*(w-1)+4.0F);
		width.transform.localScale = new Vector3(9.0f*h, 1.0F, 1.0F);  // w z h
		width.GetComponent<MeshRenderer>().material.color = new Color(1f, 0.92f, 0.016f, 1f);

		GameObject height = GameObject.CreatePrimitive(PrimitiveType.Cube);
		//		height.transform.localPosition = new Vector3 (9.0f*(h-1)+4.0f,1.5F,9.0f*((w-1)/2)+4.0F);
		height.transform.localPosition = new Vector3 (9.0f*(h-1)+4.0f,1.5F,9.0f*(w/2));
		height.transform.localScale = new Vector3(1.0F, 1.0F, 9.0F*w);  // w z h
		height.GetComponent<MeshRenderer>().material.color = new Color(1f, 0.92f, 0.016f, 1f);
	}


	private void initGrid(Node node){
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.localPosition = new Vector3 (9.0f*node.x,0.5F,9.0f*node.y);
		cube.transform.localScale = new Vector3(9.0F, 1.0F, 9.0F);  // w z h
		cube.GetComponent<MeshRenderer>().material.color = new Color(1f*node.x/10, 0.92f*node.y/10, 0.016f, 1f);

		int w = Maze.Direction.W;
		int n = Maze.Direction.N;

		bool left = (node.value & Maze.Direction.W) != Maze.Direction.W;
		left = (node.value & w) != w;
		bool top = (node.value & Maze.Direction.N) != Maze.Direction.N;
		top = (node.value & n) != n;

//		float x = node.x == 0 ? 9.0f * node.x - 4.0f : 9.0f * node.x;
//		float y = node.y == 0 ? 9.0f * node.y - 4.0f : 9.0f * node.y;
		float x = 9.0f * node.x;
		float y = 9.0f * node.y;

//		float x = 9.0f * node.x - 4f;
//		float y = 9.0f * node.y - 4f;



		if(left&&top){
			GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube1.transform.localPosition = new Vector3 (x-4F,1.5F,y);
			cube1.transform.localScale = new Vector3(1.0F, 1.0F, 9.0F);  // w z h
			cube1.GetComponent<MeshRenderer>().material.color = new Color(1f, 0.92f, 0.016f*node.y/10, 0.3f*node.x/10);


			GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube2.transform.localPosition = new Vector3 (x,1.5F,y-4F);
			cube2.transform.localScale = new Vector3(9.0F, 1.0F, 1.0F);  // w z h
			cube2.GetComponent<MeshRenderer>().material.color = new Color(1f, 0.92f, 0.016f*node.y/10, 0.4f*node.x/10);
		}

		else if(left){
			GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube2.transform.localPosition = new Vector3 (x-4F,1.5F,y);
			cube2.transform.localScale = new Vector3(1.0F, 1.0F, 9.0F);  // w z h
			cube2.GetComponent<MeshRenderer>().material.color = new Color(1f, 0.42f, 0.46f, 0.4f);
		}
		else if(top){
			GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube2.transform.localPosition = new Vector3 (x,1.5F,y-4F);
			cube2.transform.localScale = new Vector3(9.0F, 1.0F, 1.0F);  // w z h
//			cube2.GetComponent<MeshRenderer>().material.color = new Color(1f, 0.92f, 0.016f*node.y/10, 0.6f*node.x/10);
			cube2.GetComponent<MeshRenderer>().material.color = new Color(0.32f, 0.92f, 0.16f, 0.6f);
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
//		Debug.Log (key);
		List<object> n = list[key];
		return n;
	}
		

	// 验证 附近的节点是否有效
	public override bool isValid(Node nearNode,Node node,int dir) {
//		if (!nearNode || nearNode.value === null) {
		if (nearNode == null) {
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

//		Debug.Log (len);

		if (this.braid) {
			return len - 1;
		}

		// 按一定的概率随机选择回溯策略
		System.Random random = new System.Random();
		float r = random.Next (0, 9)/10.0f;
		int idx = 0;
		if (r < 0.5) {
			idx = len - 1;
		} else if (r < 0.7) {
			idx = len >> 1;
		} else if (r < 0.8) {
			idx = len * random.Next (0, 9)/10 >> 0;
		}
		var teast = 15 >> 1;
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