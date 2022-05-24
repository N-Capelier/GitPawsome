using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Nicolas
/// Last modified by Nicolas
/// </summary>
[CreateAssetMenu(fileName = "CatCall", menuName = "Spells/CatCall", order = 50)]
public class CatCall : Spell
{
	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		ReachFinder _rf = new ReachFinder(LevelGrid.Instance, true);
		return _rf.FindLineReach(fromX, fromY, 4);
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		FindObjectOfType<AudioManager>().Play("Buff");
		Entity _targetEntity = LevelGrid.Instance.cells[_target.x, _target.y].entityOnCell;
		var lookingVector = (Vector2)(_target - _caster.GetGridPosition());

		_caster.models.transform.forward = lookingVector.normalized;

		if (_targetEntity == null)
			return;

		Vector2Int _casterPos = _caster.GetGridPosition();

		if(_casterPos.x > _target.x && Mathf.Abs(_casterPos.x - _target.x) > 1)
		{
			_targetEntity.MoveAlongPath(new Vector2Int[] { new Vector2Int(_target.x + 1, _target.y)});
		}
		else if (_casterPos.x < _target.x && Mathf.Abs(_casterPos.x - _target.x) > 1)
		{
			_targetEntity.MoveAlongPath(new Vector2Int[] { new Vector2Int(_target.x - 1, _target.y) });
		}
		else if (_casterPos.y > _target.y && Mathf.Abs(_casterPos.y - _target.y) > 1)
		{
			_targetEntity.MoveAlongPath(new Vector2Int[] { new Vector2Int(_target.x, _target.y + 1) });
		}
		else if (_casterPos.y > _target.y && Mathf.Abs(_casterPos.y - _target.y) > 1)
		{
			_targetEntity.MoveAlongPath(new Vector2Int[] { new Vector2Int(_target.x, _target.y - 1) });
		}

		BattleInformationManager.Instance.Notifiate(new NotificationProps(_caster, _targetEntity, true, notificationSprite, spellName, $"{_caster.InstaCat.catName} attracted {_targetEntity.InstaCat.catName}."));
		_caster.animationHandler.Attack();
	}
}
