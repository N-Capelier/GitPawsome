using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FierceGrowl", menuName = "Spells/FierceGrowl", order = 50)]
public class FierceGrowl : Spell
{
	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		ReachFinder _rf = new ReachFinder(LevelGrid.Instance, true);
		return _rf.FindLineReach(fromX, fromY, 2);
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		Entity _targetEntity = LevelGrid.Instance.cells[_target.x, _target.y].entityOnCell;

		if (_targetEntity == null)
			return;

		_targetEntity.TakeManaDamage(1);

		Vector2Int _casterPos = _caster.GetGridPosition();

		LevelCell _cell;

		if (_casterPos.x < _target.x)
		{
			_cell = LevelGrid.Instance.cells[_target.x + 1, _target.y];
			if (_cell.entityOnCell == null && !_cell.isWall)
			{
				_targetEntity.MoveAlongPath(new Vector2Int[] { new Vector2Int(_target.x + 1, _target.y) });
			}
		}
		else if (_casterPos.x > _target.x)
		{
			_cell = LevelGrid.Instance.cells[_target.x - 1, _target.y];
			if (_cell.entityOnCell == null && !_cell.isWall)
			{
				_targetEntity.MoveAlongPath(new Vector2Int[] { new Vector2Int(_target.x - 1, _target.y) });
			}
		}
		else if (_casterPos.y < _target.y)
		{
			_cell = LevelGrid.Instance.cells[_target.x, _target.y + 1];
			if (_cell.entityOnCell == null && !_cell.isWall)
			{
				_targetEntity.MoveAlongPath(new Vector2Int[] { new Vector2Int(_target.x, _target.y + 1) });
			}
		}
		else if (_casterPos.y > _target.y)
		{
			_cell = LevelGrid.Instance.cells[_target.x, _target.y - 1];
			if (_cell.entityOnCell == null && !_cell.isWall)
			{
				_targetEntity.MoveAlongPath(new Vector2Int[] { new Vector2Int(_target.x, _target.y - 1) });
			}
		}

		BattleInformationManager.Instance.Notifiate(new NotificationProps(_caster, _targetEntity, true, spellSprite, spellName, $"{_caster.InstaCat.name} feared {_targetEntity.InstaCat.name}."));
	}
}
