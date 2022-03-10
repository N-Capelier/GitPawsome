using UnityEngine;

[CreateAssetMenu(fileName = "Strike", menuName = "Spells/Strike", order = 50)]
public class Strike : Spell
{
	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		ReachFinder rf = new ReachFinder(LevelGrid.Instance, true);
		return rf.FindLineReach(fromX, fromY, 1);
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		Entity _targetEntity = LevelGrid.Instance.cells[_target.x, _target.y].entityOnCell;

		if (_targetEntity == null)
			return;

		LevelGrid.Instance.cells[_target.x, _target.y].entityOnCell.TakeDamage(10, _caster);
	}
}
