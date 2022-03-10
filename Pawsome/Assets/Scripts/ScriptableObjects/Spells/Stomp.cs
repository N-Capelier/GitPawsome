using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stomp", menuName = "Spells/Stomp", order = 50)]
public class Stomp : Spell
{
	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		ReachFinder _rf = new ReachFinder(LevelGrid.Instance, true);

		return _rf.FindDiamondReach(fromX, fromY, 2);
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		Entity _targetEntity = LevelGrid.Instance.cells[_target.x, _target.y].entityOnCell;

		if (_targetEntity == null)
			return;

		_targetEntity.TakeDamage(20, _caster);
		BattleInformationManager.Instance.Notifiate(new NotificationProps(_caster, _targetEntity, true, notificationSprite, spellName, $"{_caster.InstaCat.name} stomped {_targetEntity.InstaCat.name}."));
	}
}
