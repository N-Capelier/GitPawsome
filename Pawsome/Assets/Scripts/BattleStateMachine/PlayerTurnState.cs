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

	public override void OnStateEnter()
	{
		fsm = StateMachine as BattleStateMachine;
		activeEntity = fsm.entities[fsm.turnIndex] as PlayerEntity;

		DisplaySpellNames();

		if (!alreadyMoved)
			BattleInputManager.PrimaryInteraction += OnClickSelf;

		if(!alreadyAttacked)
			BattleStateMachine.SelectSpell += OnEquipSpell;
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
		BattleStateMachine.SelectSpell -= OnEquipSpell;

		BattleInputManager.PrimaryInteraction -= OnClickSelf;

		equipedSpell = _spellIndex;

		Spell _spell = Instantiate(activeEntity.deck[_spellIndex]);

		DrawAttackableCells(_spell.attackPattern);
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

		if(!alreadyAttacked)
		{
			LevelGrid.Instance.HideAllInteractors();
			BattleStateMachine.SelectSpell += OnEquipSpell;
		}

		if (!alreadyMoved)
			BattleInputManager.PrimaryInteraction += OnClickSelf;
	}

	#endregion

	#region Movement

	private void OnClickSelf(CellInteractor _interactor)
	{
		if(_interactor.levelCell.entityOnCell == fsm.entities[fsm.turnIndex])
		{
			BattleStateMachine.SelectSpell -= OnEquipSpell;

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

		if (!alreadyMoved)
		{
			LevelGrid.Instance.HideAllInteractors();
			BattleInputManager.PrimaryInteraction += OnClickSelf;
		}

		if (!alreadyAttacked)
		{
			BattleStateMachine.SelectSpell += OnEquipSpell;
		}
	}

	#endregion

	#region UI

	//UI setup and update

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
		BattleInputManager.PrimaryInteraction -= OnClickSelf;

		BattleStateMachine.SelectSpell -= OnEquipSpell;
	}
}