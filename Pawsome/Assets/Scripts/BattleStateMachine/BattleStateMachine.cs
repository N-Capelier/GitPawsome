using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BattleStateMachine : MonoStateMachine
{
	[Header("References")]
	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	[Header("Params")]
	[HideInInspector] public InstaCat[] playerCats;
	public InstaCat[] AICats;

	[HideInInspector] public List<Entity> entities = new List<Entity>();

	int turn = 0;
	int turnIndex = -1;

	public void PlayNextTurn()
	{
		if(turnIndex > -1)
			entities[turnIndex].isPlaying = false;

		turn++;
		turnIndex++;
		if(turnIndex > entities.Count)
		{
			turnIndex = 0;
		}

		entities[turnIndex].isPlaying = true;

		if(entities[turnIndex].isPlayerEntity)
		{
			SetState("PlayerTurnState");
		}
		else
		{
			SetState("AITurnState");
		}
	}
}
