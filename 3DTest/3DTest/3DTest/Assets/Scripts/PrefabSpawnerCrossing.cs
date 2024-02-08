using UnityEngine;
using Pathfinding;

public class PrefabSpawnerCrossing : MonoBehaviour
{
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

	void Start()
	{
		playerManager = FindObjectOfType<PlayerManager>();
		
		for (int i = 0; i < cubeRowCount; i++)
		{
			for (int j = 0; j < cubeColCount; j++)
			{
				Vector3 cubePosition = new Vector3((j - cubeColCount / 2) * cubeSpacing, 0, (i - cubeRowCount / 2) * cubeSpacing);
				
				if (i % 2 == 1)
				{
					cubePosition.x += cubeSpacing * 0.5f;
				}
				
				Instantiate(cube, cubePosition + cubeOffset, Quaternion.identity);
			}
		}
		
		
		for (int i = 0; i < playerRowCount; i++)
		{
			for (int j = 0; j < playerColCount; j++)
			{
				Vector3 playerPosition = new Vector3((j - playerColCount / 2) * playerSpacing, 0, (i - playerRowCount / 2) * playerSpacing);
				GameObject newPlayer = Instantiate(player, playerPosition + playerOffset, Quaternion.identity) as GameObject;
				AIDestinationSetter destSetterScript = newPlayer.GetComponent<AIDestinationSetter>();
				destSetterScript.target = target;

				// 计算对称的玩家位置并创建对称玩家实体
				Vector3 symPlayerPosition = new Vector3(playerPosition.x, playerPosition.y, -playerPosition.z); // 对称位置
				GameObject symNewPlayer = Instantiate(player, symPlayerPosition + playerOffset, Quaternion.identity) as GameObject;
				AIDestinationSetter symDestSetterScript = symNewPlayer.GetComponent<AIDestinationSetter>();
				symDestSetterScript.target = target;
			}
		}
        
		playerManager.SetTotalPlayers(playerRowCount * playerColCount * 2); // 因为创建了对称玩家，所以数量翻倍
		playerManager.StartPathFinding();
		if (AstarPath.active) AstarPath.active.Scan();
	}
}
