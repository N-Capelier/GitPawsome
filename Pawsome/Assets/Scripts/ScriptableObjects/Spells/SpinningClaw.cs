using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpinningClaw", menuName = "Spells/SpinningClaw", order = 50)]
public class SpinningClaw : Spell
{
	int fromX, fromY;

	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		this.fromX = fromX;
		this.fromY = fromY;

		return new Vector2Int[] { new Vector2Int(fromX, fromY) };
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		FindObjectOfType<AudioManager>().Play("Slash");
		ReachFinder rf = new ReachFinder(LevelGrid.Instance, true);

		Vector2Int[] reach = rf.FindDiamondReach(fromX, fromY, 2);

		Entity _entity;

		foreach (Vector2Int pos in reach)
		{
			_entity = LevelGrid.Instance.cells[pos.x, pos.y].entityOnCell;

			if (_entity != null)
			{
				_entity.TakeDamage(10, _caster);
				BattleInformationManager.Instance.Notifiate(new NotificationProps(_caster, _entity, true, notificationSprite, spellName, $"{_caster.InstaCat.catName} scratched {_entity.InstaCat.catName}."));
				_caster.animationHandler.Attack();
			}
		}
	}
}
