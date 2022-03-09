using UnityEngine;

[CreateAssetMenu(fileName = "New LancerDePelote", menuName = "Spells/Lancer de pelote", order = 50)]
public class LancerDePelote : Spell
{
	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		ReachFinder _rf = new ReachFinder(LevelGrid.Instance, true);

		return _rf.FindLineReach(fromX, fromY, 8);
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		LevelGrid.Instance.cells[_target.x, _target.y].entityOnCell.TakeDamage(20, _caster);
	}
}
