using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SuperClaw", menuName = "Spells/SuperClaw", order = 50)]
public class SuperClaw : Spell
{
	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		ReachFinder _rf = new ReachFinder(LevelGrid.Instance, true);

		return _rf.FindLineReach(fromX, fromY, 1);
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		Entity _targetEntity = LevelGrid.Instance.cells[_target.x, _target.y].entityOnCell;

		if (_targetEntity == null)
			return;

		_targetEntity.TakeDamage(20, _caster);

		Vector2Int _casterPos = _caster.GetGridPosition();

		if(_target.x < _casterPos.x)
		{
			if(LevelGrid.Instance.cells[_target.x - 1, _target.y].entityOnCell == null && !LevelGrid.Instance.cells[_target.x - 1, _target.y].isWall)
			{
				_targetEntity.MoveAlongPath(new Vector2Int[] { new Vector2Int(_target.x - 1, _target.y) });
			}
		}
		else if (_target.x > _casterPos.x)
		{
			if (LevelGrid.Instance.cells[_target.x + 1, _target.y].entityOnCell == null && !LevelGrid.Instance.cells[_target.x + 1, _target.y].isWall)
			{
				_targetEntity.MoveAlongPath(new Vector2Int[] { new Vector2Int(_target.x + 1, _target.y) });
			}
		}
		else if (_target.y < _casterPos.y)
		{
			if (LevelGrid.Instance.cells[_target.x, _target.y - 1].entityOnCell == null && !LevelGrid.Instance.cells[_target.x, _target.y - 1].isWall)
			{
				_targetEntity.MoveAlongPath(new Vector2Int[] { new Vector2Int(_target.x, _target.y - 1) });
			}
		}
		else if (_target.y > _casterPos.y)
		{
			if (LevelGrid.Instance.cells[_target.x, _target.y + 1].entityOnCell == null && !LevelGrid.Instance.cells[_target.x, _target.y + 1].isWall)
			{
				_targetEntity.MoveAlongPath(new Vector2Int[] { new Vector2Int(_target.x, _target.y + 1) });
			}
		}
	}
}
