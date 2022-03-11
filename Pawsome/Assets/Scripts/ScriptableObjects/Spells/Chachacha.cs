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
		FindObjectOfType<AudioManager>().Play("ZoneHeal");
		_caster.chachacha = 3;
		BattleInformationManager.Instance.Notifiate(new NotificationProps(_caster, null, false, notificationSprite, spellName, $"{_caster.InstaCat.catName} is dancing the Chachacha!"));
	}
}
