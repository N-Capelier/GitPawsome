using UnityEngine;

[CreateAssetMenu(fileName = "DrainingClaw", menuName = "Spells/DrainingClaw", order = 50)]
public class DrainingClaw : Spell
{
	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		ReachFinder rf = new ReachFinder(LevelGrid.Instance, true);
		return rf.FindLineReach(fromX, fromY, 1);
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		FindObjectOfType<AudioManager>().Play("Slash");
		Vector2Int _casterPos = _caster.GetGridPosition();

		if (_target.x != _casterPos.x)
		{
			for (int i = -1; i <= 1; i++)
			{
				DamageCell(_caster, new Vector2Int(_target.x, _target.y + i));
			}
		}
		else if (_target.y != _casterPos.y)
		{
			for (int i = -1; i <= 1; i++)
			{
				DamageCell(_caster, new Vector2Int(_target.x + i, _target.y));
			}
		}
	}

	void DamageCell(Entity _caster, Vector2Int _cell)
	{
		Entity _entity = LevelGrid.Instance.cells[_cell.x, _cell.y].entityOnCell;

		if (_entity == null)
			return;

		BattleInformationManager.Instance.Notifiate(new NotificationProps(_caster, _entity, true, notificationSprite, spellName, $"{_caster.InstaCat.catName} drained {_entity.InstaCat.catName}."));
		_caster.animationHandler.Attack();

		_entity.TakeDamage(20, _caster);
		_caster.HealMana(1);
	}
}
