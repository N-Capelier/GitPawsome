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
	PlayerEntity activeEntity;
	int equipedSpell;

	int movementSub = 0, attackSub = 0;

	public override void OnStateEnter()
	{
		fsm = StateMachine as BattleStateMachine;
		activeEntity = fsm.entities[fsm.turnIndex] as PlayerEntity;

		DisplaySpellNames();
		activeEntity.SetPossessionRenderer(true);

		if (!alreadyMoved)
			MovementSub(true);

		if (!alreadyAttacked)
			AttackSub(true);
	}

	#region Spell

	void DisplaySpellNames()
	{
		for (int i = 0; i < 3; i++)
		{
			BattleCanvasManager.Instance.spellTexts[i].text = activeEntity.hand[i].spellName;
		}
	}

	private void OnEquipSpell(int _spellIndex)
	{
		AttackSub(false);

		if (alreadyAttacked)
		{
			return;
		}

		MovementSub(false);

		equipedSpell = _spellIndex;

		DrawAttackableCells(activeEntity.deck[_spellIndex].attackPattern);
	}

	private void DrawAttackableCells(AttackPattern _attackType)
	{
		ReachFinder _reachFinder = new ReachFinder(LevelGrid.Instance, true);

		CellInteractor _interactor = LevelGrid.Instance.cells[(int)activeEntity.transform.position.x, (int)activeEntity.transform.position.z].interactor;

		switch(_attackType)
		{
			case AttackPattern.Line:
				attackableCells = _reachFinder.FindLineReach((int)_interactor.levelCell.position.x, (int)_interactor.levelCell.position.y, 3);
				break;
			case AttackPattern.Diamond:
				attackableCells = _reachFinder.FindDiamondReach((int)_interactor.levelCell.position.x, (int)_interactor.levelCell.position.y, 3);
				break;
			case AttackPattern.Square:
				attackableCells = _reachFinder.FindSquareReach((int)_interactor.levelCell.position.x, (int)_interactor.levelCell.position.y, 3);
				break;
			case AttackPattern.Circle:
				attackableCells = _reachFinder.FindCircleReach((int)_interactor.levelCell.position.x, (int)_interactor.levelCell.position.y, 3);
				break;
		}

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

				activeEntity.DiscardSpell(equipedSpell);
				DisplaySpellNames();

				if (interactor.levelCell.entityOnCell != null)
				{
					//Deal damages to target
					Debug.Log(interactor.levelCell.entityOnCell.InstaCat.catName + " lost 5 health points");
					if(interactor.levelCell.entityOnCell.TakeDamage(5))
					{
						fsm.RemoveEntity(interactor.levelCell.entityOnCell);
						interactor.levelCell.entityOnCell.Death();
					}
				}
				break;
			}
		}

		LevelGrid.Instance.HideAllInteractors(); ///Debugging

		if (!alreadyAttacked)
			AttackSub(true);

		if (!alreadyMoved)
			MovementSub(true);
	}

	#endregion

	#region events

	void AttackSub(bool value)
	{
		if(value)
		{
			if (attackSub > 0)
				return;
			attackSub++;
			BattleStateMachine.SelectSpell += OnEquipSpell;
		}
		else
		{
			if (attackSub == 0)
				return;
			attackSub--;
			BattleStateMachine.SelectSpell -= OnEquipSpell;
		}
	}
	void MovementSub(bool value)
	{
		if (value)
		{
			if (movementSub > 0)
				return;
			movementSub++;
			BattleInputManager.PrimaryInteraction += OnClickSelf;
		}
		else
		{
			if (movementSub == 0)
				return;
			movementSub--;
			BattleInputManager.PrimaryInteraction -= OnClickSelf;
		}
	}

	#endregion

	#region Movement

	private void OnClickSelf(CellInteractor _interactor)
	{
		if(alreadyMoved)
		{
			MovementSub(false);
			return;
		}

		if (_interactor.levelCell.entityOnCell == fsm.entities[fsm.turnIndex])
		{
			AttackSub(false);

			MovementSub(false);
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

		if (!alreadyMoved)
		{
			LevelGrid.Instance.HideAllInteractors();

			MovementSub(true);
		}

		if (!alreadyAttacked)
		{
			AttackSub(true);
		}
	}

	#endregion

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
		activeEntity.SetPossessionRenderer(false);

		AttackSub(false);
		MovementSub(false);
	}
}