using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Nicolas
/// Last modified by Nicolas
/// </summary>
[CreateAssetMenu(fileName = "NineLives", menuName = "Spells/NineLives", order = 50)]
public class NineLives : Spell
{
	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		return new Vector2Int[] { new Vector2Int(fromX, fromY) };
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		FindObjectOfType<AudioManager>().Play("Buff");
		_caster.animationHandler.StatsUp();
		_caster.hasNineLives = true;
		BattleInformationManager.Instance.Notifiate(new NotificationProps(_caster, null, false, notificationSprite, spellName, $"{_caster.InstaCat.catName} has 9 lives and cannot die."));
	}
}
