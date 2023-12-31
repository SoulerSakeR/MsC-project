﻿using UnityEngine;

public class Player : MonoBehaviour
{
	private PlayerManager playerManager;

    void Start()
    {
	    playerManager = FindObjectOfType<PlayerManager>();
    }

	void OnTriggerEnter(Collider other)
	{
		// Debug.Log("OnTriggerEnter with: " + other.gameObject.name);
    	
	    if (other.gameObject.name == "Target")
	    {
	    	transform.parent.gameObject.SetActive(false);
		    playerManager.IncrementPlayersHidden();
	    }
    }
}
