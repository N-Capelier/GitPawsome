using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Appat", menuName = "Spells/Appat", order = 50)]
public class Appat : Spell
{
	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		ReachFinder _rf = new ReachFinder(LevelGrid.Instance, true);
		return _rf.FindLineReach(fromX, fromY, 4);
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{

	}
}
