using UnityEngine;

[CreateAssetMenu(fileName = "WoolBall", menuName = "Spells/WoolBall", order = 50)]
public class WoolBall : Spell
{
	public override Vector2Int[] GetSpellReach(int fromX, int fromY, int toX, int toY)
	{
		ReachFinder _rf = new ReachFinder(LevelGrid.Instance, true);

		return _rf.FindLineReach(fromX, fromY, 8);
	}

	public override void ExecuteSpell(Entity _caster, Vector2Int _target)
	{
		if(LevelGrid.Instance.cells[_target.x, _target.y].entityOnCell != null)
		{
		FindObjectOfType<AudioManager>().Play("Slash");
			LevelGrid.Instance.cells[_target.x, _target.y].entityOnCell.TakeDamage(20, _caster);
			BattleInformationManager.Instance.Notifiate(new NotificationProps(_caster, LevelGrid.Instance.cells[_target.x, _target.y].entityOnCell, true,
				notificationSprite, spellName, $"{_caster.InstaCat.catName} threw a wool ball to {LevelGrid.Instance.cells[_target.x, _target.y].entityOnCell.InstaCat.catName}."));
		}
	}
}
