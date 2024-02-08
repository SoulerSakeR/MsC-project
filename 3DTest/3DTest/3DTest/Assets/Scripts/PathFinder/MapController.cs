using FT;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tile 
{  
    public int x;
    public int y;
}
public class MapController : MonoBehaviour
{
    public static MapController instance;
    [Header("Map Obstacles")]
    public int mapSize = 50;//地图大小

    private PlayerManager playerManager;
    public GameObject cube;
    public GameObject player;
    public Transform target;
    public int cubeRowCount = 7;
    public int cubeColCount = 7;
    public float cubeSpacing = 20.0f;
    public Vector3 cubeOffset = new Vector3(0.0f, 0.0f, 20.0f);
    public int playerRowCount = 5;
    public int playerColCount = 20;
    public float playerSpacing = 6.0f;
    public Vector3 playerOffset = new Vector3(0.0f, 0.0f, -80.0f);
    private AbsPathFinder jpsPathFinder;      
    void Start()
    {
        instance = this;   
        playerManager = GetComponent<PlayerManager>();
        jpsPathFinder = new JPSPathFinder(this);
        jpsPathFinder.SetNode2Pos(Node2Pos);
        jpsPathFinder.InitMap(mapSize, mapSize);    
        //设置终点
        Tile endNode = Pos2Node(target.position);
        // 生成障碍物     
        for (int i = 0; i < cubeRowCount; i++)
        {
            for (int j = 0; j < cubeColCount; j++)
            {
                Vector3 d = new Vector3((j - cubeColCount / 2) * cubeSpacing, 0, (i - cubeRowCount / 2) * cubeSpacing);
                if (i % 2 == 1) { d.x += cubeSpacing * 0.5f; }
                Instantiate(cube, d + cubeOffset, Quaternion.identity);
            }
        }
       
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                Tile t = new Tile() { x=i,y=j};
                Vector3 p= Node2Pos(i, j);    
                bool za = Physics.CheckBox(p, Vector3.one*3, Quaternion.identity, LayerMask.GetMask("Obstacles"));
                if (za) { SetNodeBlock(t); }
            }
        }

        //生成人物      
        for (int i = 0; i < playerRowCount; i++)
        {
            for (int j = 0; j < playerColCount; j++)
            {
                Vector3 d = new Vector3((j - playerColCount / 2) * playerSpacing, 0, (i - playerRowCount / 2) * playerSpacing);
                GameObject newPlayer = Instantiate(player, d + playerOffset, Quaternion.identity);
                PlayerJPS jps = newPlayer.GetComponent<PlayerJPS>();
                jps.Init(endNode);
            }
        }
	    playerManager.SetTotalPlayers(playerRowCount * playerColCount * 2);
        playerManager.StartPathFinding();
        Invoke("xsxlzsj", 1);
    }
    void xsxlzsj()
    {
	    string s = "All paths found，Time" + jpsPathFinder.xlzsj + "Second（s）";
        Debug.LogError(s);
    }
    public Vector3 Node2Pos(int i, int j)
    {
        int d= 200/mapSize;
        return new Vector3(i * d, 0, j * d);
    }
    public Tile Pos2Node(Vector3 pos)
    {
        int d = 200 / mapSize;
        Tile t = new Tile();
        t.x =(int)( pos.x / d);
        t.y = (int)(pos.z / d);
        return t;
    }
    public void SetNodeBlock(Tile node)
    {
        jpsPathFinder.RefreshWalkable(node.x, node.y, false);
    }
    public List<PathNode> FindJpsPath(Tile startNode, Tile endNode)
    {      
        List<PathNode> path = jpsPathFinder.FindPath(startNode, endNode);
        return path;
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                Gizmos.color = Color.red;
                Vector3 p = Node2Pos(i, j);
                bool za = Physics.CheckBox(p, Vector3.one , Quaternion.identity, LayerMask.GetMask("Obstacles"));
                if (za) { Gizmos.DrawCube(p, new Vector3(3f, 1f, 3f)); }
            }
        }
    }
}
