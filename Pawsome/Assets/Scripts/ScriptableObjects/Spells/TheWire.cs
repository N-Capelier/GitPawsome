using UnityEngine;

[CreateAssetMenu(fileName = "TheWire", menuName = "Spells/TheWire", order = 50)]
public class TheWire : Spell
{
	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		ReachFinder _rf = new ReachFinder(LevelGrid.Instance, true);
		return _rf.FindLineReach(fromX, fromY, 1);
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		FindObjectOfType<AudioManager>().Play("Slash");
		Vector2Int _casterPos = _caster.GetGridPosition();

		if(_casterPos.x < _target.x && !LevelGrid.Instance.cells[_casterPos.x + 1, _casterPos.y].isWall && LevelGrid.Instance.cells[_casterPos.x + 1, _casterPos.y].entityOnCell == null)
		{
			_caster.MoveAlongPath(new Vector2Int[] { new Vector2Int(_casterPos.x + 1, _casterPos.y) });
		}
		else if (_casterPos.x > _target.x && !LevelGrid.Instance.cells[_casterPos.x - 1, _casterPos.y].isWall && LevelGrid.Instance.cells[_casterPos.x - 1, _casterPos.y].entityOnCell == null)
		{
			_caster.MoveAlongPath(new Vector2Int[] { new Vector2Int(_casterPos.x - 1, _casterPos.y) });
		}
		else if (_casterPos.y < _target.y && !LevelGrid.Instance.cells[_casterPos.x, _casterPos.y + 1].isWall && LevelGrid.Instance.cells[_casterPos.x, _casterPos.y + 1].entityOnCell == null)
		{
			_caster.MoveAlongPath(new Vector2Int[] { new Vector2Int(_casterPos.x, _casterPos.y + 1) });
		}
		else if (_casterPos.y > _target.y && !LevelGrid.Instance.cells[_casterPos.x, _casterPos.y - 1].isWall && LevelGrid.Instance.cells[_casterPos.x, _casterPos.y - 1].entityOnCell == null)
		{
			_caster.MoveAlongPath(new Vector2Int[] { new Vector2Int(_casterPos.x, _casterPos.y - 1) });
		}
	}
}