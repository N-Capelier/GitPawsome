using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITurnState : MonoState
{
	BattleStateMachine fsm;
	EnemyEntity activeEntity;

	public override void OnStateEnter()
	{
		fsm = StateMachine as BattleStateMachine;
		activeEntity = fsm.entities[fsm.turnIndex] as EnemyEntity;

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