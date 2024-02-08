using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
	public TextMeshProUGUI uiText;
	private int updateCount = 0;
	private int totalPlayers = 0;
	private int playersHidden = 0;
	private float pathFindingStartTime;
	void Update()
	{
		++updateCount;
	}

	public int GetUpdateCount()
	{
		return updateCount;
	}

	public void SetTotalPlayers(int total)
	{
		totalPlayers = total;
	}

	public void StartPathFinding()
	{
		pathFindingStartTime = Time.time;
	}
    
	public void IncrementPlayersHidden()
	{
		++playersHidden;

		// Debug.Log($"playersHidden: {playersHidden}, Total Players: {totalPlayers}");
		
		if (playersHidden == 40)
		{
			float pathFindingElapsed = Time.time - pathFindingStartTime;
			uiText.text = $"All players reached target point!\nSim Steps: {updateCount}" +
				$"\nPath Finding Time: {pathFindingElapsed} seconds";
		}
	}
}
