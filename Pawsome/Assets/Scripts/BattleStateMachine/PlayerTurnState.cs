using UnityEngine;
using System;

public class PlayerTurnState : MonoState
{
	BattleStateMachine fsm;
	Vector2Int[] movableCells;
	Vector2Int[] attackableCells;
	bool alreadyMoved = false;
	PlayerEntity activeEntity;
	Spell activeSpell;
	int equipedSpell;

	int movementSub = 0, attackSub = 0;

	static Action<int> ForcedSpellCast;

	//We are playing with fire
	//Hope I dont get burned
	public static void EmergencySpellCast(int spellIndex)
    {
		ForcedSpellCast?.Invoke(spellIndex);
    }

	public override void OnStateEnter()
	{
		fsm = StateMachine as BattleStateMachine;
		activeEntity = fsm.entities[fsm.turnIndex] as PlayerEntity;

		fsm.FixGridEntities();

		activeEntity.InstaCat.mana = activeEntity.InstaCatRef.GetMana();
		activeEntity.ManaChanged?.Invoke();

		DisplaySpellNames();
		activeEntity.SetPossessionRenderer(true);

		foreach(LevelCell _cell in LevelGrid.Instance.cells)
		{
			_cell.BonAppetit();
		}

		ForcedSpellCast += OnEquipSpell;

		if (!alreadyMoved)
			MovementSub(true);

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

		MovementSub(false);

		equipedSpell = _spellIndex;

		activeSpell = activeEntity.hand[_spellIndex];
		DrawAttackableCells();
	}

	private void DrawAttackableCells()
	{
		fsm.FixGridEntities();

		if (activeSpell.manaCost > activeEntity.InstaCat.mana)
		{
			AttackSub(true);
			if (!alreadyMoved)
				MovementSub(true);
			return;
		}

		ReachFinder _reachFinder = new ReachFinder(LevelGrid.Instance, true);

		CellInteractor _interactor = LevelGrid.Instance.cells[(int)activeEntity.transform.position.x, (int)activeEntity.transform.position.z].interactor;

		attackableCells = activeSpell.GetSpellReach((int)_interactor.levelCell.position.x, (int)_interactor.levelCell.position.y, 0, 0);

		foreach(Vector2Int _cellPosition in attackableCells)
		{
			LevelGrid.Instance.cells[_cellPosition.x, _cellPosition.y].interactor.SetRendererColor(Color.red);
			LevelGrid.Instance.cells[_cellPosition.x, _cellPosition.y].interactor.SetRendererAlpha(.6f);
		}

		BattleInputManager.PrimaryInteraction += OnClickAttackableCell;
	}

	private void OnClickAttackableCell(CellInteractor interactor)
	{
		fsm.FixGridEntities();

		foreach (Vector2Int _attackableCell in attackableCells)
		{
			if (interactor == null)
				break;
			if(_attackableCell == interactor.levelCell.position)
			{
				BattleInputManager.PrimaryInteraction -= OnClickAttackableCell;

				LevelGrid.Instance.HideAllInteractors();

				activeEntity.TakeManaDamage(activeSpell.manaCost);
				fsm.StartCoroutine(fsm.AnimateParticle(activeSpell.particleEffect, interactor.levelCell.position));
				activeSpell.ExecuteSpell(activeEntity, _attackableCell);

				activeEntity.DiscardSpell(equipedSpell);
				DisplaySpellNames();

				break;
			}
		}

		fsm.FixGridEntities();


		LevelGrid.Instance.HideAllInteractors(); ///Debugging

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

		fsm.FixGridEntities();

		if (_interactor.levelCell.entityOnCell == fsm.entities[fsm.turnIndex])
		{
			AttackSub(false);

			MovementSub(false);
			DrawMovableCells(_interactor);
		}
	}

	void DrawMovableCells(CellInteractor _interactor)
	{
		fsm.FixGridEntities();

		ReachFinder _reachFinder = new ReachFinder(LevelGrid.Instance, false);

		movableCells = _reachFinder.FindDiamondReach((int)_interactor.levelCell.position.x, (int)_interactor.levelCell.position.y, fsm.entities[fsm.turnIndex].InstaCat.movePoints);
		
		foreach(Vector2Int _cellPosition in movableCells)
		{
			LevelGrid.Instance.cells[_cellPosition.x, _cellPosition.y].interactor.SetRendererColor(Color.blue);
			LevelGrid.Instance.cells[_cellPosition.x, _cellPosition.y].interactor.SetRendererAlpha(.4f);
		}

		BattleInputManager.PrimaryInteraction += OnClickMovableCell;
	}

	private void OnClickMovableCell(CellInteractor interactor)
	{
		fsm.FixGridEntities();

		foreach(Vector2Int _movableCell in movableCells)
		{
			if (interactor == null)
				break;
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

		fsm.FixGridEntities();

		if (!alreadyMoved)
		{
			LevelGrid.Instance.HideAllInteractors();

			MovementSub(true);
		}

			AttackSub(true);
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
		activeEntity.ClearInvincibility();
		activeEntity.redirectedDamages = null;
		activeEntity.HealInZone();
		activeEntity.hasNineLives = false;

		fsm.FixGridEntities();

		activeEntity.SetPossessionRenderer(false);

		AttackSub(false);
		MovementSub(false);
	}
}