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
}
