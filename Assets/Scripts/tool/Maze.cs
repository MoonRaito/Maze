using UnityEngine;
using System.Collections;

using System.Collections.Generic;//C# 包中的类

public class MazeBase  : MonoBehaviour {

	private int width;
	private int height;

	List<List<Node>> grid = new List<List<Node>>();  

	private int currentDir;      	// 当前的方向
	private int currentDirCount;	// 当前方向的累加
	private Node currentNode;		// 当前节点
	private Node startNode;			// 开始节点
	private Node endNode;			// 结束节点

	private bool alwaysBacktrace = false;// 是否每走一步, 都尝试回溯.

//	this.trace = [];

	ArrayList trace = new ArrayList();
	Dictionary<string,Object> neighbors; // 邻居节点
	public int size;



	// 默认构造
	public MazeBase(){
		
	}

	// 初始化
	public MazeBase(int width,int height){
		this.width = width;
		this.height = height;

		// 初始化
//		init ();
	}

	// 初始化
	public void init(){

		this.size = this.width * this.height;

		this.initGrid ();
		this.onInit ();
	}

	private void initGrid(){
		for(int r = 0;r<this.height;r++){
			List<Node> row = new List<Node>();  
			this.grid.Add (row);
			for (int c = 0; c < this.width; c++) {
				Node node = new Node (c, r, 0);
				row.Add (node);
			}
		}
	}

	// 初始化结束 重写
	public void onInit(){}


	// 获取随机数
	public int random(int min,int max){
		//		return ((max - min + 1) * Math.random() + min) >> 0; // js
		System.Random random = new System.Random();
		return random.Next(min,max)>> 0;
	}

	// unused 获取节点
	private Node getNode(int c,int r){
		return this.grid [r][c];
	}

	// 获取随机节点
	private Node getRandomNode(){
		int r = this.random (0,this.height-1);
		int c = this.random (0,this.width-1);
		return (Node)this.grid [r] [c];
	}

	// unused
	private int setMark(Node node,int value){
		return node.value |= value;
	}

	// unused
	private int removeMark(Node node,int value){
		return node.value &= ~value;
	}

	// unused
	private int isMarked(Node node,int value){
		return (node.value & value) == value;
	}

	// 设置开始节点
	public void setStart(int c,int r){
		this.startNode = this.grid [r] [c];
	}
	// 设置结束节点
	public void setEnd(int c,int r){
		this.endNode = this.grid [r] [c];
	}

	// 获取路线总和
	public int getRoadCount(Node node){
		int count=0;
//		this.isMarked(node, Direction.N)>0?count++:0;
//		this.isMarked(node, Direction.E)>0?count++:0;
//		this.isMarked(node, Direction.S)>0?count++:0;
//		this.isMarked(node, Direction.W)>0?count++:0;

		if (this.isMarked (node, Direction.N) > 0)
			count++;
			

		return count;
	}

	// 设置当前节点
	public void setCurrent(Node node){
		this.currentNode = node;

		this.neighbors = this.getValidNeighbors(node);

		if (this.neighbors && node.value == 0) {
			this.trace.Add(node);
			this.onTrace(node);
		}
	}

	// unused 跟踪节点完成之后
	public void onTrace(Node node){}

	// 移动到
	public void moveTo(Node node,int dir){
		this.beforeMove(node);
		if (this.currentDir == dir) {
			this.currentDirCount++;
		} else {
			this.currentDir = dir;
			this.currentDirCount = 1;
		}
		this.currentNode.value |= dir;
		this.setCurrent(node);
		node.value |= Direction.opposite[dir];
		this.afterMove(node);
	}
		
	public void beforeMove(Node node){} 
	public void afterMove(Node node){} 	

	// 生成
	public void generate(){
		this.beforeGenrate();
		this.setCurrent(this.startNode);
		int stepCount = 0;
		while (this.nextStep()) {
			stepCount++;
			if (this.isOver() == true) {
				break;
			}
			// console.log(step);
		}
		Debug.Log ("Step Count : " + stepCount);
		this.afterGenrate();
	} 	


	public void beforeGenrate(){} 
	public void afterGenrate(){} 	

	// 生成迷宫时的提前终止条件
	public void isOver(){} 	

	// 下一步
	public void nextStep(){
		//		if (!this.neighbors) {
		// 没有邻居节点时
		if (this.neighbors==null||this.neighbors.Count==0) {
			this.beforeBacktrace();
			return this.backtrace();
		}
		var n = this.getNeighbor();
		this.moveTo(n[0], n[1]);
		this.updateCurrent();
		return true;
	} 	

	public void beforeBacktrace(){} 
	public void backtrace(){
		int len = this.trace.Count;
		while (len > 0) {
			int idx = this.getTraceIndex();
			Node node = (Node)this.trace[idx];
			Dictionary<string,Object>  nm = this.getValidNeighbors(node);
			if (nm!=null&&nm.Count>0) {
				this.currentNode = node;
				this.neighbors = nm;
				return true;
			} else {
				this.trace.RemoveAt(idx);
				len--;
			}
		}
		return false;
	} 	

	// unused
	private void setRoom(int x, int y, int width, int height) {
		var grid = this.grid;
		var ex = x + width;
		var ey = y + height;

		for (var r = y; r < ey; r++) {
			var row = grid[r];
			if (!row) {
				continue;
			}
			for (var c = x; c < ex; c++) {
				Node node = row[c];
				if (node) {
					node.value = Direction.ALL;
				}
			}
		}
	}

	private void setBlock(int x, int y, int width, int height) {
		var grid = this.grid;
		var ex = x + width;
		var ey = y + height;
		for (var r = y; r < ey; r++) {
			var row = grid[r];
			if (!row) {
				continue;
			}
			for (var c = x; c < ex; c++) {
				Node node = row [c];
				if (node) {
					node.value = null;
				}
			}
		}
	}

	/***************************************
      通过重写以下几个方法, 可以实现不同的迷宫效果
    **************************************/

	public Dictionary<string,Object> getValidNeighbors(Node node){
		ArrayList nList = new ArrayList();  
		Dictionary<string,Object> nMap = new Dictionary<string, Object>();

		int c = node.x;
		int r = node.y;
		int dir;
		Node nearNode; // 附近的节点

		dir = Direction.N;
		nearNode = r > 0 ? this.grid[r - 1][c] : null;
//		this.isValid(nearNode, node, dir) && nList.Add(nMap.Add(dir,new NearNode(nearNode,dir)));
		if(this.isValid(nearNode, node, dir))
			nList.Add(nMap.Add(dir,new NearNode(nearNode,dir)));

		dir = Direction.E;
		nearNode = this.grid[r][c + 1];
//		this.isValid(nearNode, node, dir) && nList.Add(nMap.Add(dir,new NearNode(nearNode,dir)));		
		if(this.isValid(nearNode, node, dir))
			nList.Add(nMap.Add(dir,new NearNode(nearNode,dir)));

		dir = Direction.S;
		nearNode = r < this.height - 1 ? this.grid[r + 1][c] : null;
//		this.isValid(nearNode, node, dir) && nList.Add(nMap.Add(dir,new NearNode(nearNode,dir)));
		if(this.isValid(nearNode, node, dir))
			nList.Add(nMap.Add(dir,new NearNode(nearNode,dir)));

		dir = Direction.W;
		nearNode = this.grid[r][c - 1];
//		this.isValid(nearNode, node, dir) && nList.Add(nMap.Add(dir,new NearNode(nearNode,dir)));
		if(this.isValid(nearNode, node, dir))
			nList.Add(nMap.Add(dir,new NearNode(nearNode,dir)));

		this.updateValidNeighbors(nList, nMap);

		if (nList.Count > 0) {
			nMap.Add ("list",nList);
			return nMap;
		}
		return null;
	}

	// 更新附近的邻居节点
	public void updateValidNeighbors(ArrayList nList,Dictionary<int,NearNode> nMap) {
		
	}

	// 验证 附近的节点是否有效
	public bool isValid(Node nearNode,Node node,int dir) {
		return nearNode != null && nearNode.value == 0;
	}

	public void updateCurrent() {
		if (this.alwaysBacktrace) {
			this.backtrace();
		}
	}

	// 获取邻居
	public int getNeighbor(){
		ArrayList list = (ArrayList)this.neighbors["list"];
		// js 中 随机数 0~1
//		var n = list[list.length * Math.random() >> 0];
		System.Random rd=new System.Random();
		double dResult; 
		dResult=rd.NextDouble();
		var n = list[list.Count * dResult >> 0];
		return n;
	}
	// 根据的节点
	public int getTraceIndex() {
		int idx = this.trace.Count - 1;
		return idx;
	}

}


public class Node{
	public int x,y,value;

	public Node(){
	}
	public Node(int x,int y,int value){
		this.x = x;
		this.y = y;
		this.value = value;
	}
}

public class NearNode{
	public Node node;
	int dir;

	public NearNode(){
	}
	public NearNode(Node node,int dir){
		this.node = node;
		this.dir = dir;
	}
}

public static class Direction{
	public static int N = 1;
	public static  int S = 2;
	public static int E = 4;
	public static int W = 8;
	public static int ALL = 1 | 2 | 4 | 8;

	// 对面 对立
	public static Dictionary<int,int> opposite = new Dictionary<int, int>{{1,2},{2,1},{4,8},{8,4}};
	// 步骤
	public static Dictionary<int,int> stepX = new Dictionary<int, int>{{1,0},{2,0},{4,1},{8,-1}};
	public static Dictionary<int,int> stepY = new Dictionary<int, int>{{1,-1},{2,1},{4,0},{8,0}};




}