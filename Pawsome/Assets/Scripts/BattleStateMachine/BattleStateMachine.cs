using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerInfo
{
	public string playerName;
	public Entity[] entities;
}

public class BattleStateMachine : MonoStateMachine
{
	[Header("References")]
	public GameObject playerPrefab;
	public GameObject enemyPrefab;
	public BattleUIMode battleUIMode;
	public BattleInitUIMode battleInitUIMode;
	[SerializeField] GameObject[] archetypeButtons;

	[Header("Params")]
	[HideInInspector] public InstaCat[] playerCats;
	[Tooltip("Order must be DPS > TANK > SUPPORT")]
	public InstaCat[] AICats;
	public float coinFlipAnimationDuration;

	[Header("Players")]
	public PlayerInfo playerInfo;
	public PlayerInfo enemyInfo;

	[HideInInspector] public List<Entity> entities = new List<Entity>();

	bool placedDPS, placedTank, placedSupport;
	[HideInInspector] public bool playerFirst;

	int turn = 0;
	[HideInInspector] public int turnIndex = -1;

	#region Events & Actions

	public Action CoinFlip; //true for player first, false for enemy first

	public delegate void SpellInputHandler(int _spellIndex);
	public static event SpellInputHandler SelectSpell;

	public Action EnterTurn;

	public Action<Archetype> PickArchetype;

	#endregion

	[HideInInspector] public List<(InstaCat, InstaCat)> playerDeadCats = new List<(InstaCat, InstaCat)>();
	[HideInInspector] public List<(InstaCat, InstaCat)> enemyDeadCats = new List<(InstaCat, InstaCat)>();

	public void PlayNextTurn()
	{
		if(turnIndex > -1)
			entities[turnIndex].isPlaying = false;

		turn++;
		turnIndex++;
		
		if (turnIndex >= entities.Count)
		{
			turnIndex = 0;
		}

		entities[turnIndex].isPlaying = true;
		EnterTurn?.Invoke();

		if(entities.Count <= 3)
		{
			if(playerDeadCats.Count >= 3 || enemyDeadCats.Count >= 3)
			{
				foreach((InstaCat cat, InstaCat refCat) cat in playerDeadCats)
				{
					cat.refCat.Dead = true;
					cat.refCat.baseHealth = cat.cat.health;
					cat.refCat.baseMana = cat.cat.mana;
				}

				UnityEngine.SceneManagement.SceneManager.LoadScene("Lounge");
			}
		}

		if (entities[turnIndex].isPlayerEntity)
		{
			SetState("PlayerTurnState", true);
		}
		else
		{
			SetState("AITurnState", true);
		}
	}

	public void RemoveEntity(Entity _entity)
	{
		for (int i = 0; i < entities.Count; i++)
		{
			if (entities[i] == _entity)
			{
				if(_entity.isPlayerEntity)
				{
					playerDeadCats.Add((_entity.InstaCat, _entity.InstaCatRef));
				}
				else
				{
					enemyDeadCats.Add((_entity.InstaCat, _entity.InstaCatRef));
				}

				entities.RemoveAt(i);
				return;
			}
		}
	}

	public Action<bool> CoinTossed;

	public IEnumerator CoinFlipCoroutine()
	{
		int _random = UnityEngine.Random.Range(0, 2);
		
		if (_random == 0)
			playerFirst = true;
		else
			playerFirst = false;

		//play coinflip animation

		yield return new WaitForSeconds(coinFlipAnimationDuration); // Animation time

		CoinTossed?.Invoke(playerFirst);
	}

	public void StartPlacingPhase()
	{
		CoinFlip?.Invoke();
	}

	public void EndTurnButton()
	{
		if(ActiveState.StateName == "PlayerTurnState")
		{
			LevelGrid.Instance.HideAllInteractors();
			PlayNextTurn();
		}
	}

	public void InputSpell1()
	{
		if(ActiveState.StateName == "PlayerTurnState")
			SelectSpell?.Invoke(0);
	}

	public void InputSpell2()
	{
		if (ActiveState.StateName == "PlayerTurnState")
			SelectSpell?.Invoke(1);
	}

	public void InputSpell3()
	{
		if (ActiveState.StateName == "PlayerTurnState")
			SelectSpell?.Invoke(2);
	}

	public void EnableArchetypeButtons()
	{
		if (!placedDPS) archetypeButtons[0].SetActive(true);

		if (!placedTank) archetypeButtons[1].SetActive(true);

		if (!placedSupport) archetypeButtons[2].SetActive(true);
	}

	public void DisableArchetypeButtons()
	{
		foreach (GameObject _go in archetypeButtons)
		{
			_go.SetActive(false);
		}
	}

	public void InputDPS()
	{
		placedDPS = true;
		PickArchetype?.Invoke(Archetype.Dps);
	}

	public void InputTank()
	{
		placedTank = true;
		PickArchetype?.Invoke(Archetype.Tank);
	}

	public void InputSupport()
	{
		placedSupport = true;
		PickArchetype?.Invoke(Archetype.Support);
	}

	private void OnDestroy()
	{
		Entity.CatDeath -= RemoveEntity;
	}

	public IEnumerator AnimateParticle(GameObject _particle, Vector2 _position/*, bool _move, Vector2Int _targetPos*/)
	{
		Vector3 _spawnPos = new Vector3(_position.x, 0f, _position.y);
		_particle = Instantiate(_particle, _spawnPos, Quaternion.identity);

		yield return new WaitForSeconds(1.1f);

		Destroy(_particle);
	}

	public void AttackTarget(Entity _caster, Entity _target)
	{
		StartCoroutine(AttackTargetCoroutine(_caster, _target));
	}

	IEnumerator AttackTargetCoroutine(Entity _caster, Entity _target)
	{
		yield return new WaitForSeconds(3f);

		_target.TakeDamage(20, _caster);
	}

	public void HealTarget(Entity _caster, Entity _target)
	{
		StartCoroutine(HealTargetCoroutine(_caster, _target));
	}

	IEnumerator HealTargetCoroutine(Entity _caster, Entity _target)
	{
		yield return new WaitForSeconds(1.2f);

		_target.Heal(10);
	}

	public void WaitAndPlayNextTurn()
	{
		StartCoroutine(WaitAndPlayNextTurnCoroutine());
	}

	IEnumerator WaitAndPlayNextTurnCoroutine()
	{
		yield return new WaitForSeconds(1.4f);
		PlayNextTurn();
	}

	public void FixGridEntities()
	{
		for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
		{
			for (int y = 0; y < LevelGrid.Instance.GetHeigth(); y++)
			{
				LevelGrid.Instance.cells[x, y].entityOnCell = null;
			}
		}

		foreach(Entity _entity in entities)
		{
			Vector2Int _entityPos = _entity.GetGridPosition();
			LevelGrid.Instance.cells[_entityPos.x, _entityPos.y].entityOnCell = _entity;
		}
	}
}
