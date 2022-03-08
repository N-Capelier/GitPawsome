using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InitBattleState : MonoState
{
	BattleStateMachine fsm;

	public override void OnStateEnter()
	{
		fsm = StateMachine as BattleStateMachine;

		//get player's information
		fsm.playerCats = BattleInformationManager.Instance.GetInstaCats();

		//Place entities on playground
		for (int i = 0; i < 3; i++)
		{
			//init player entity
			GameObject _entity = Instantiate(fsm.playerPrefab, new Vector3(i, 0f, i), Quaternion.identity);
			Entity _entityComponent = _entity.GetComponent<Entity>();

			_entityComponent.Init(fsm.playerCats[i]);
			LevelGrid.Instance.cells[i, i].entityOnCell = _entityComponent;
			_entityComponent.isPlayerEntity = true;
			fsm.entities.Add(_entityComponent);

			//init enemy entity
			_entity = Instantiate(fsm.enemyPrefab, new Vector3(LevelGrid.Instance.GetWidth() - i - 1, 0f, LevelGrid.Instance.GetHeigth() - i - 1), Quaternion.identity);
			_entityComponent = _entity.GetComponent<Entity>();

			_entityComponent.Init(fsm.AICats[i]);
			LevelGrid.Instance.cells[LevelGrid.Instance.GetWidth() - i - 1, LevelGrid.Instance.GetHeigth() - i - 1].entityOnCell = _entityComponent;
			fsm.entities.Add(_entityComponent);
		}

		//Order turn with entities initiative
		fsm.entities = fsm.entities.OrderBy(x => x.InstaCat.initiative).ToList();
		fsm.entities.Reverse();

		fsm.battleUIMode.Init();

		fsm.PlayNextTurn();
	}

	//public override void OnStateUpdate()
	//{

	//}

	//public override void OnStateFixedUpdate()
	//{

	//}

	//public override void OnStateLateUpdate()
	//{

	//}

	//public override void OnStateExit()
	//{

	//}
}