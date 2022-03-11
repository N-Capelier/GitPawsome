using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityPlacingState : MonoState
{
	BattleStateMachine fsm;

	Archetype currentArchetype;
	int placed = 0;
	bool enemyPlacedEntities = false;

	public override void OnStateEnter()
	{
		fsm = StateMachine as BattleStateMachine;

		//fsm.EnableArchetypeButtons();

		fsm.PickArchetype += PickArchetype;

		fsm.StartCoroutine(CreateEnemyEntitiesCoroutine());
	}

	void PickArchetype(Archetype _archetype)
	{
		fsm.PickArchetype -= PickArchetype;
		fsm.DisableArchetypeButtons();
		currentArchetype = _archetype;
		BattleInputManager.PrimaryInteraction += OnClickInteractor;
	}

	void OnClickInteractor(CellInteractor _interactor)
	{
		if(_interactor.levelCell.isPlayerStartCell && _interactor.levelCell.entityOnCell == null)
		{
			BattleInputManager.PrimaryInteraction -= OnClickInteractor;

			//init entity
			CreatePlayerEntity(_interactor);

			if(placed <= 2)
			{
				fsm.PickArchetype += PickArchetype;
				//fsm.EnableArchetypeButtons();
				fsm.battleInitUIMode.InitPlacement();
			}
		}
	}

	void CreatePlayerEntity(CellInteractor _interactor)
	{
		int _xPos = (int)_interactor.levelCell.position.x;
		int _yPos = (int)_interactor.levelCell.position.y;
		GameObject _entity = Instantiate(fsm.playerPrefab, new Vector3(_xPos, 0f, _yPos), Quaternion.identity);

		Entity _entityComponent = _entity.GetComponent<Entity>();
		foreach (InstaCat _instaCat in BattleInformationManager.Instance.GetInstaCats())
		{
			if(_instaCat.catClass == currentArchetype)
			{
				_entityComponent.Init(_instaCat);
				break;
			}
		}
		LevelGrid.Instance.cells[_xPos, _yPos].entityOnCell = _entityComponent;
		_entityComponent.isPlayerEntity = true;
		fsm.playerInfo.entities[placed] = _entityComponent;
		placed++;
	}

	WaitForSeconds waitOneSecond = new WaitForSeconds(1f);

	IEnumerator CreateEnemyEntitiesCoroutine()
	{
		List<Entity> _entities = new List<Entity>();

		yield return waitOneSecond;

		_entities.Add(Instantiate(fsm.enemyPrefab, new Vector3(5, 0f, 6f), Quaternion.identity).GetComponent<Entity>());
		LevelGrid.Instance.cells[5, 6].entityOnCell = _entities[0];

		yield return waitOneSecond;

		_entities.Add(Instantiate(fsm.enemyPrefab, new Vector3(6, 0f, 5f), Quaternion.identity).GetComponent<Entity>());
		LevelGrid.Instance.cells[6, 5].entityOnCell = _entities[1];

		yield return waitOneSecond;

		_entities.Add(Instantiate(fsm.enemyPrefab, new Vector3(6, 0f, 6f), Quaternion.identity).GetComponent<Entity>());
		LevelGrid.Instance.cells[6, 6].entityOnCell = _entities[2];

		for (int i = 0; i < _entities.Count; i++)
		{
			_entities[i].Init(fsm.AICats[i]);
			fsm.enemyInfo.entities[i] = _entities[i];
		}

		yield return waitOneSecond;

		enemyPlacedEntities = true;
	}

	void InitTurn()
	{
		if(fsm.playerFirst)
		{
			for (int i = 0; i < 3; i++)
			{
				fsm.entities.Add(fsm.playerInfo.entities[i]);
				fsm.entities.Add(fsm.enemyInfo.entities[i]);
			}
		}
		else
		{
			for (int i = 0; i < 3; i++)
			{
				fsm.entities.Add(fsm.enemyInfo.entities[i]);
				fsm.entities.Add(fsm.playerInfo.entities[i]);
			}
		}
	}

	public override void OnStateUpdate()
	{
		if(placed > 2 && enemyPlacedEntities)
		{
			InitTurn();

			fsm.battleUIMode.gameObject.SetActive(true);
			fsm.battleUIMode.Init();

			fsm.PlayNextTurn();
		}
	}

	//public override void OnStateFixedUpdate()
	//{

	//}

	//public override void OnStateLateUpdate()
	//{

	//}

	public override void OnStateExit()
	{
		LevelGrid.Instance.HideAllInteractors();
	}
}