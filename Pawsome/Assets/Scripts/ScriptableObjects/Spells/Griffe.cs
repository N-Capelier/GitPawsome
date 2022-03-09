using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Griffe", menuName = "Spells/Griffe", order = 50)]
public class Griffe : Spell
{
	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		ReachFinder rf = new ReachFinder(LevelGrid.Instance, true);
		return rf.FindLineReach(fromX, fromY, 1);
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		LevelGrid.Instance.cells[_target.x, _target.y].entityOnCell.TakeDamage(10, _caster);
	}
}
