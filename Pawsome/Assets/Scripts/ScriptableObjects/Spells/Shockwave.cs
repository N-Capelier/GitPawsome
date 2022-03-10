using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Shockwave", menuName = "Spells/Shockwave", order = 50)]
public class Shockwave : Spell
{
	List<Entity> _entities = new List<Entity>();

	int fromX, fromY;

	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		this.fromX = fromX;
		this.fromY = fromY;

		return new Vector2Int[] { new Vector2Int(fromX, fromY) };
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		FindObjectOfType<AudioManager>().Play("Impact");
		ReachFinder rf = new ReachFinder(LevelGrid.Instance, true);

		Vector2Int[] reach = rf.FindDiamondReach(fromX, fromY, 2);

		Entity _entity;

		foreach (Vector2Int pos in reach)
		{
			_entity = LevelGrid.Instance.cells[pos.x, pos.y].entityOnCell;
			
			if (_entity != null)
			{
				_entities.Add(_entity);
				_entity.TakeDamage(30, _caster);
			}
		}

		_caster.TakeDamage(20, _caster);

		if(_entities.Count > 0)
		{
			foreach (Entity _orbitedCat in _entities)
			{
				BattleInformationManager.Instance.Notifiate(new NotificationProps(_caster, _orbitedCat, true, notificationSprite, spellName, $"{_caster.InstaCat.name} put {_orbitedCat.InstaCat.name} in orbit!"));
			}
		}
	}
}
