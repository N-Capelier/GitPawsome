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
	[HideInInspector] public int turnIndex = -1;

	public delegate void SpellInputHandler(Spell _spell);
	public static event SpellInputHandler Spell1;
	public static event SpellInputHandler Spell2;
	public static event SpellInputHandler Spell3;

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

	public void EndTurnButton()
	{
		if(ActiveState.StateName == "PlayerTurnState")
		{
			PlayNextTurn();
		}
	}

	public void InputSpell1()
	{
		if(ActiveState.StateName == "PlayerTurnState")
			Spell1?.Invoke(null);
	}

	public void InputSpell2()
	{
		if (ActiveState.StateName == "PlayerTurnState")
			Spell2?.Invoke(null);
	}

	public void InputSpell3()
	{
		if (ActiveState.StateName == "PlayerTurnState")
			Spell3?.Invoke(null);
	}

}
