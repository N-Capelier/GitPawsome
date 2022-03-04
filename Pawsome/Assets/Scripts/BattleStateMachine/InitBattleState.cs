using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBattleState : MonoState
{
	BattleStateMachine _fsm;

	public override void OnStateEnter()
	{
		_fsm = StateMachine as BattleStateMachine;

		//get player's information
		_fsm.playerCats = BattleInformationManager.Instance.GetInstaCats();

		//Place entities on playground
		for (int i = 0; i < 3; i++)
		{
			//init player entity
			GameObject _entity = Instantiate(_fsm.playerPrefab, new Vector3(i, 0f, i), Quaternion.identity);
			Entity _entityComponent = _entity.GetComponent<Entity>();

			_entityComponent.Init(_fsm.playerCats[i]);
			LevelGrid.Instance.cells[i, i].entityOnCell = _entityComponent;
			_fsm.entities.Add(_entityComponent);

			//init enemy entity
			_entity = Instantiate(_fsm.enemyPrefab, new Vector3(LevelGrid.Instance.GetWidth() - i - 1, 0f, LevelGrid.Instance.GetHeigth() - i - 1), Quaternion.identity);
			_entityComponent = _entity.GetComponent<Entity>();

			_entityComponent.Init(_fsm.AICats[i]);
			LevelGrid.Instance.cells[LevelGrid.Instance.GetWidth() - i - 1, LevelGrid.Instance.GetHeigth() - i - 1].entityOnCell = _entityComponent;
			_fsm.entities.Add(_entityComponent);
		}
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