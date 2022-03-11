using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MilkyWay", menuName = "Spells/MilkyWay", order = 50)]
public class MilkyWay : Spell
{
	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		return new Vector2Int[] { new Vector2Int(fromX, fromY) };
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		FindObjectOfType<AudioManager>().Play("DrinkingMilk");
		_caster.Heal(20);

		BattleInformationManager.Instance.Notifiate(new NotificationProps(_caster, null, false, notificationSprite, spellName, $"{_caster.InstaCat.catName} drank some good milk."));
	}
}
