using UnityEngine;

[CreateAssetMenu(fileName = "TheWire", menuName = "Spells/TheWire", order = 50)]
public class TheWire : Spell
{
	bool attacked = false;

	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		ReachFinder _rf = new ReachFinder(LevelGrid.Instance, true);
		return _rf.FindLineReach(fromX, fromY, 1);
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		FindObjectOfType<AudioManager>().Play("Slash");
		Vector2Int _casterPos = _caster.GetGridPosition();

		Entity _targetEntity = LevelGrid.Instance.cells[_target.x, _target.y].entityOnCell;
		var lookingVector = (Vector2)(_target - _caster.GetGridPosition());

		_caster.models.transform.forward = lookingVector.normalized;

		if (_casterPos.x < _target.x && !LevelGrid.Instance.cells[_casterPos.x + 1, _casterPos.y].isWall && LevelGrid.Instance.cells[_casterPos.x + 1, _casterPos.y].entityOnCell == null)
		{
			_caster.MoveAlongPath(new Vector2Int[] { new Vector2Int(_casterPos.x + 1, _casterPos.y) });
			attacked = true;
		}
		else if (_casterPos.x > _target.x && !LevelGrid.Instance.cells[_casterPos.x - 1, _casterPos.y].isWall && LevelGrid.Instance.cells[_casterPos.x - 1, _casterPos.y].entityOnCell == null)
		{
			_caster.MoveAlongPath(new Vector2Int[] { new Vector2Int(_casterPos.x - 1, _casterPos.y) });
			attacked = true;
		}
		else if (_casterPos.y < _target.y && !LevelGrid.Instance.cells[_casterPos.x, _casterPos.y + 1].isWall && LevelGrid.Instance.cells[_casterPos.x, _casterPos.y + 1].entityOnCell == null)
		{
			_caster.MoveAlongPath(new Vector2Int[] { new Vector2Int(_casterPos.x, _casterPos.y + 1) });
			attacked = true;
		}
		else if (_casterPos.y > _target.y && !LevelGrid.Instance.cells[_casterPos.x, _casterPos.y - 1].isWall && LevelGrid.Instance.cells[_casterPos.x, _casterPos.y - 1].entityOnCell == null)
		{
			_caster.MoveAlongPath(new Vector2Int[] { new Vector2Int(_casterPos.x, _casterPos.y - 1) });
			attacked = true;
		}

		if (attacked)
        {
			BattleInformationManager.Instance.Notifiate(new NotificationProps(_caster, null, false, spellSprite, spellName, $"{_caster.InstaCat.catName} catched a wire!"));
			_caster.animationHandler.Attack();
		}
	}
}