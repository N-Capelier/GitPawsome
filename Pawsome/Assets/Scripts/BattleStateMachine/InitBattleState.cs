public class InitBattleState : MonoState
{
	BattleStateMachine fsm;

	public override void OnStateEnter()
	{
		fsm = StateMachine as BattleStateMachine;

		//get player's information
		fsm.playerCats = BattleInformationManager.Instance.GetInstaCats();

		fsm.playerInfo.entities = new Entity[3];
		fsm.enemyInfo.entities = new Entity[3];

		//CoinFlip order
		fsm.CoinFlip += OnCoinFlipCompleted;
		fsm.StartCoroutine(fsm.CoinFlipCoroutine());
	}

	void OnCoinFlipCompleted()
	{
		fsm.CoinFlip -= OnCoinFlipCompleted;

		fsm.SetState("EntityPlacingState");
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