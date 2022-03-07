using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerTurnState : MonoState
{
	BattleStateMachine fsm;
	Vector2Int[] movableCells;
	Vector2Int[] attackableCells;
	bool alreadyMoved = false;
	bool alreadyAttacked = false;

	public override void OnStateEnter()
	{
		Debug.Log("Player Turn");
		fsm = StateMachine as BattleStateMachine;

		if(!alreadyMoved)
			BattleInputManager.PrimaryInteraction += OnClickSelf;

		if(!alreadyAttacked)
		{
			BattleStateMachine.Spell1 += OnEquipSpell;
			BattleStateMachine.Spell2 += OnEquipSpell;
			BattleStateMachine.Spell3 += OnEquipSpell;
		}
	}

	private void OnEquipSpell(Spell _spell)
	{
		BattleStateMachine.Spell1 -= OnEquipSpell;
		BattleStateMachine.Spell2 -= OnEquipSpell;
		BattleStateMachine.Spell3 -= OnEquipSpell;

		BattleInputManager.PrimaryInteraction -= OnClickSelf;

		DrawAttackableCells();
	}

	private void DrawAttackableCells()
	{
		ReachFinder _reachFinder = new ReachFinder(LevelGrid.Instance, true);

		CellInteractor _interactor = LevelGrid.Instance.cells[(int)fsm.entities[fsm.turnIndex].transform.position.x, (int)fsm.entities[fsm.turnIndex].transform.position.z].interactor;

		attackableCells = _reachFinder.FindLineReach((int)_interactor.levelCell.position.x, (int)_interactor.levelCell.position.y, 3);

		foreach(Vector2Int _cellPosition in attackableCells)
		{
			LevelGrid.Instance.cells[_cellPosition.x, _cellPosition.y].interactor.SetRendererColor(Color.red);
			LevelGrid.Instance.cells[_cellPosition.x, _cellPosition.y].interactor.SetRendererAlpha(1f);
		}

		BattleInputManager.PrimaryInteraction += OnClickAttackableCell;
	}

	private void OnClickAttackableCell(CellInteractor interactor)
	{
		foreach(Vector2Int _attackableCell in attackableCells)
		{
			if(_attackableCell == interactor.levelCell.position)
			{
				BattleInputManager.PrimaryInteraction -= OnClickAttackableCell;
				alreadyAttacked = true;

				LevelGrid.Instance.HideAllInteractors();

				if(interactor.levelCell.entityOnCell != null)
				{
					Debug.Log(interactor.levelCell.entityOnCell.InstaCat.catName + " lost 5 health points");
					interactor.levelCell.entityOnCell.TakeDamage(5);
				}
				break;
			}
		}
		if (!alreadyMoved)
			BattleInputManager.PrimaryInteraction += OnClickSelf;
	}

	private void OnClickSelf(CellInteractor _interactor)
	{
		if(_interactor.levelCell.entityOnCell == fsm.entities[fsm.turnIndex])
		{
			BattleStateMachine.Spell1 -= OnEquipSpell;
			BattleStateMachine.Spell2 -= OnEquipSpell;
			BattleStateMachine.Spell3 -= OnEquipSpell;

			BattleInputManager.PrimaryInteraction -= OnClickSelf;
			DrawMovableCells(_interactor);
		}
	}

	void DrawMovableCells(CellInteractor _interactor)
	{
		ReachFinder _reachFinder = new ReachFinder(LevelGrid.Instance, false);

		movableCells = _reachFinder.FindDiamondReach((int)_interactor.levelCell.position.x, (int)_interactor.levelCell.position.y, fsm.entities[fsm.turnIndex].InstaCat.movePoints);
		
		foreach(Vector2Int _cellPosition in movableCells)
		{
			LevelGrid.Instance.cells[_cellPosition.x, _cellPosition.y].interactor.SetRendererColor(Color.blue);
			LevelGrid.Instance.cells[_cellPosition.x, _cellPosition.y].interactor.SetRendererAlpha(1f);
		}

		BattleInputManager.PrimaryInteraction += OnClickMovableCell;
	}

	private void OnClickMovableCell(CellInteractor interactor)
	{
		foreach(Vector2Int _movableCell in movableCells)
		{
			if (_movableCell == interactor.levelCell.position)
			{
				BattleInputManager.PrimaryInteraction -= OnClickMovableCell;
				alreadyMoved = true;

				LevelGrid.Instance.HideAllInteractors();
				PathFinder _pathFinder = new PathFinder(LevelGrid.Instance, false);
				fsm.entities[fsm.turnIndex].MoveAlongPath(_pathFinder.FindPath(movableCells, (int)fsm.entities[fsm.turnIndex].transform.position.x, (int)fsm.entities[fsm.turnIndex].transform.position.z, _movableCell.x, _movableCell.y));
				break;
			}
		}
		if(!alreadyAttacked)
		{
			BattleStateMachine.Spell1 += OnEquipSpell;
			BattleStateMachine.Spell2 += OnEquipSpell;
			BattleStateMachine.Spell3 += OnEquipSpell;
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

	public override void OnStateExit()
	{
		BattleInputManager.PrimaryInteraction -= OnClickSelf;

		BattleStateMachine.Spell1 -= OnEquipSpell;
		BattleStateMachine.Spell2 -= OnEquipSpell;
		BattleStateMachine.Spell3 -= OnEquipSpell;
		Debug.Log("End of Player Turn");
	}
}