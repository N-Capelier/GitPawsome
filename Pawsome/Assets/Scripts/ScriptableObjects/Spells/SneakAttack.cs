using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SneakAttack", menuName = "Spells/SneakAttack", order = 50)]
public class SneakAttack : Spell
{
	bool tp = false;

	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		ReachFinder _rf = new ReachFinder(LevelGrid.Instance, true);
		return _rf.FindDiamondReach(fromX, fromY, 3);
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		Entity _targetEntity = LevelGrid.Instance.cells[_target.x, _target.y].entityOnCell;

		if (_targetEntity == null)
			return;

		if(LevelGrid.Instance.cells[_target.x + 1, _target.y].entityOnCell == null && !LevelGrid.Instance.cells[_target.x + 1, _target.y].isWall)
		{
			tp = true;

			Vector2Int _casterPos = _caster.GetGridPosition();

			LevelGrid.Instance.cells[_casterPos.x, _casterPos.y].entityOnCell = null;
			_caster.transform.position = new Vector3(_target.x + 1, 0f, _target.y);
			LevelGrid.Instance.cells[_target.x + 1, _target.y].entityOnCell = _caster;
		}
		else if (LevelGrid.Instance.cells[_target.x - 1, _target.y].entityOnCell == null && !LevelGrid.Instance.cells[_target.x - 1, _target.y].isWall)
		{
			tp = true;

			Vector2Int _casterPos = _caster.GetGridPosition();

			LevelGrid.Instance.cells[_casterPos.x, _casterPos.y].entityOnCell = null;
			_caster.transform.position = new Vector3(_target.x - 1, 0f, _target.y);
			LevelGrid.Instance.cells[_target.x - 1, _target.y].entityOnCell = _caster;
		}
		else if (LevelGrid.Instance.cells[_target.x, _target.y + 1].entityOnCell == null && !LevelGrid.Instance.cells[_target.x, _target.y + 1].isWall)
		{
			tp = true;

			Vector2Int _casterPos = _caster.GetGridPosition();

			LevelGrid.Instance.cells[_casterPos.x, _casterPos.y].entityOnCell = null;
			_caster.transform.position = new Vector3(_target.x, 0f, _target.y + 1);
			LevelGrid.Instance.cells[_target.x, _target.y + 1].entityOnCell = _caster;
		}
		else if (LevelGrid.Instance.cells[_target.x, _target.y - 1].entityOnCell == null && !LevelGrid.Instance.cells[_target.x, _target.y - 1].isWall)
		{
			tp = true;

			Vector2Int _casterPos = _caster.GetGridPosition();

			LevelGrid.Instance.cells[_casterPos.x, _casterPos.y].entityOnCell = null;
			_caster.transform.position = new Vector3(_target.x, 0f, _target.y - 1);
			LevelGrid.Instance.cells[_target.x, _target.y - 1].entityOnCell = _caster;
		}

		if (tp)
			BattleInformationManager.Instance.Notifiate(new NotificationProps(_caster, _targetEntity, true, spellSprite, spellName, $"{_caster.InstaCat.name} reached {_targetEntity.InstaCat.name}."));
	}
}
