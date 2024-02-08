using FT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJPS : MonoBehaviour
{
    List<Vector3> lj = new List<Vector3>();
	public float sd = 20;
    private int dqwzbh;
    private Tile startNode;
    private Tile endNode;
    private bool ksyd;
    private Vector3 fx;
    private CharacterController jskzq;
    public void Init(Tile end)
    {
        jskzq = GetComponent<CharacterController>();
        endNode = end;
        FindJpsPath();
    }
    public void FindJpsPath()
    {          
        startNode = MapController.instance.Pos2Node(transform.position);     
        List<PathNode> path  = MapController.instance.FindJpsPath(startNode,endNode);   
        if (path.Count > 0)
        {         
            for (int i = 0; i < path.Count; i += 1)
            {
                Vector3 p= MapController.instance.Node2Pos(path[i].x,path[i].y);
                lj.Add(p);
            }
            dqwzbh =0;         
            ksyd = true;           
        }
    }
    private void Update()
    {
        if (ksyd)
        {       
            fx =new Vector3(lj[dqwzbh].x,transform.position.y, lj[dqwzbh].z) - transform.position;
            transform.forward = fx;
            jskzq.Move(fx.normalized * sd * Time.deltaTime);
           // transform.position = Vector3.MoveTowards(transform.position, lj[dqwzbh], sd*Time.deltaTime);
            if (Vector3.Distance(transform.position, lj[dqwzbh]) <=5f)
            {
                if (dqwzbh + 1 < lj.Count)
                {
                    dqwzbh += 1;  
                }
            }
        }
    }
 
    private void OnDrawGizmos()
    {
        if (lj.Count > 0)
        {    
            Gizmos.color = Color.green;
            foreach (var p in lj)
            {             
                Gizmos.DrawCube(p, new Vector3(2f, 0.5f, 2f));
            }
        }
    }
   
}
