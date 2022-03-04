using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerTurnState : MonoState
{
	BattleStateMachine fsm;
	Vector2Int[] movableCells;
	bool alreadyMoved = false;

	public override void OnStateEnter()
	{
		fsm = StateMachine as BattleStateMachine;

		if(!alreadyMoved)
			BattleInputManager.PrimaryInteraction += OnClickSelf;
	}

	private void OnClickSelf(CellInteractor _interactor)
	{
		if(_interactor.levelCell.entityOnCell == fsm.entities[fsm.turnIndex])
		{
			BattleInputManager.PrimaryInteraction -= OnClickSelf;
			DrawMovableCells(_interactor);
		}
	}

	void DrawMovableCells(CellInteractor _interactor)
	{
		ReachFinder _reachFinder = new ReachFinder(LevelGrid.Instance);

		movableCells = _reachFinder.FindDiamondReach((int)_interactor.levelCell.position.x, (int)_interactor.levelCell.position.y, fsm.entities[fsm.turnIndex].InstaCat.movePoints);
		
		foreach(Vector2Int _cellPosition in movableCells)
		{
			LevelGrid.Instance.cells[_cellPosition.x, _cellPosition.y].interactor.SetRendererColor(Color.blue);
			LevelGrid.Instance.cells[_cellPosition.x, _cellPosition.y].interactor.SetRendererAlpha(1f);
		}

		BattleInputManager.PrimaryInteraction += OnClickTargetCell;
	}

	private void OnClickTargetCell(CellInteractor interactor)
	{
		foreach(Vector2Int _movableCell in movableCells)
		{
			if (_movableCell == interactor.levelCell.position)
			{
				BattleInputManager.PrimaryInteraction -= OnClickTargetCell;
				alreadyMoved = true;

				LevelGrid.Instance.HideAllInteractors();
				PathFinder _pathFinder = new PathFinder(LevelGrid.Instance, false);
				fsm.entities[fsm.turnIndex].MoveAlongPath(_pathFinder.FindPath(movableCells, (int)fsm.entities[fsm.turnIndex].transform.position.x, (int)fsm.entities[fsm.turnIndex].transform.position.z, _movableCell.x, _movableCell.y));
			}
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