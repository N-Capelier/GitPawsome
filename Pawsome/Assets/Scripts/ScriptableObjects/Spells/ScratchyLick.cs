using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScratchyLick", menuName = "Spells/ScratchyLick", order = 50)]
public class ScratchyLick : Spell
{
	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		ReachFinder _rf = new ReachFinder(LevelGrid.Instance, true);

		return _rf.FindLineReach(fromX, fromY, 4);
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		FindObjectOfType<AudioManager>().Play("Lick");
		Entity _targetEntity = LevelGrid.Instance.cells[_target.x, _target.y].entityOnCell;

		if (_targetEntity == null)
			return;

		BattleInformationManager.Instance.Notifiate(new NotificationProps(_caster, _targetEntity, true, notificationSprite, spellName, $"{_caster.InstaCat.catName} licked {_targetEntity.InstaCat.catName}. Stinky!"));

		_targetEntity.TakeDamage(10, _caster);
		_targetEntity.TakeManaDamage(1);
	}
}
