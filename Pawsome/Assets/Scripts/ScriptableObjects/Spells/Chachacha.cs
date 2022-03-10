using UnityEngine;

[CreateAssetMenu(fileName = "Chachacha", menuName = "Spells/Chachacha", order = 50)]
public class Chachacha : Spell
{
	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		return new Vector2Int[] { new Vector2Int(fromX, fromY) };
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		_caster.chachacha = 3;
	}
}