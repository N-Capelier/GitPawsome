using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityPlacingState : MonoState
{
	BattleStateMachine fsm;

	public override void OnStateEnter()
	{
		fsm = StateMachine as BattleStateMachine;

		fsm.PickArchetype += PickArchetype;
	}

	void PickArchetype(Archetype _archetype)
	{
		fsm.PickArchetype -= PickArchetype;

	}

	void OnClickInteractor(CellInteractor _interactor)
	{
		if(_interactor.levelCell.isPlayerStartCell)
		{

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