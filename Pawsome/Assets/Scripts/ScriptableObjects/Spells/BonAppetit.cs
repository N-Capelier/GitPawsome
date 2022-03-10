using UnityEngine;

[CreateAssetMenu(fileName = "BonAppetit", menuName = "Spells/BonAppetit", order = 50)]
public class BonAppetit : Spell
{
	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		ReachFinder _rf = new ReachFinder(LevelGrid.Instance, true);
		return _rf.FindDiamondReach(fromX, fromY, 3);
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		LevelGrid.Instance.cells[_target.x, _target.y].bonAppetit = 2;
		BattleInformationManager.Instance.Notifiate(new NotificationProps(_caster, null, false, notificationSprite, spellName, $"{_caster.InstaCat.name} placed some catfood on the ground.")); ;
	}
}
