using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sacrifice", menuName = "Spells/Sacrifice", order = 50)]
public class Sacrifice : Spell
{
	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		ReachFinder _rf = new ReachFinder(LevelGrid.Instance, true);
		return _rf.FindDiamondReach(fromX, fromY, 2);
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		FindObjectOfType<AudioManager>().Play("Buff");
		Entity _targetEntity = LevelGrid.Instance.cells[_target.x, _target.y].entityOnCell;


		if (_targetEntity == null)
			return;

		BattleInformationManager.Instance.Notifiate(new NotificationProps(_caster, _targetEntity, true, notificationSprite, spellName, $"{_caster.InstaCat.catName} protects {_targetEntity.InstaCat.catName}."));
		_targetEntity.animationHandler.StatsUp();
		_caster.animationHandler.Spell();

		_targetEntity.redirectedDamages = _caster;
	}
}
