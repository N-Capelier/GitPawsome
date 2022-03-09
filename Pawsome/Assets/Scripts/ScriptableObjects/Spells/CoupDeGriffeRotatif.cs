using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CoupDeGriffeRotatif", menuName = "Spells/Coup de griffe rotatif", order = 50)]
public class CoupDeGriffeRotatif : Spell
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
		ReachFinder rf = new ReachFinder(LevelGrid.Instance, true);

		Vector2Int[] reach = rf.FindDiamondReach(fromX, fromY, 2);

		Entity _entity;

		foreach (Vector2Int pos in reach)
		{
			_entity = LevelGrid.Instance.cells[pos.x, pos.y].entityOnCell;

			if (_entity != null)
			{
				_entity.TakeDamage(10, _caster);
			}
		}
	}
}
