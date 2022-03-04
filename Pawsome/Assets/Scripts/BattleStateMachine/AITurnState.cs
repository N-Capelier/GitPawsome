using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITurnState : MonoState
{
	BattleStateMachine fsm;

	public override void OnStateEnter()
	{
		Debug.Log("Enemy Turn");
		fsm = StateMachine as BattleStateMachine;
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

	public override void OnStateExit()
	{
		Debug.Log("End of Enemy Turn");
	}
}