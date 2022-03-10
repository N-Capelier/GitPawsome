using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Carton", menuName = "Spells/Carton", order = 50)]
public class Carton : Spell
{
	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		return new Vector2Int[] { new Vector2Int(fromX, fromY) };
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		_caster.SetInvincibility();
	}
}

