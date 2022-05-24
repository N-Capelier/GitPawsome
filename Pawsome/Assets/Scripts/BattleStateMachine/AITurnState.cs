using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Nicolas
/// Last modified by Nicolas
/// </summary>
public class AITurnState : MonoState
{
	BattleStateMachine fsm;
	EnemyEntity activeEntity;

	List<Vector2Int> levelCellsList = new List<Vector2Int>();
	Vector2Int[] levelCells;

	public override void OnStateEnter()
	{
		fsm = StateMachine as BattleStateMachine;
		activeEntity = fsm.entities[fsm.turnIndex] as EnemyEntity;

		fsm.FixGridEntities();

		//fsm.PlayNextTurn();

		for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
		{
			for (int y = 0; y < LevelGrid.Instance.GetHeigth(); y++)
			{
				levelCellsList.Add(new Vector2Int(x, y));
			}
		}

		levelCells = levelCellsList.ToArray();

		switch (activeEntity.InstaCat.catClass)
		{
			case Archetype.Support:
				Support();
				break;
			case Archetype.Tank:
				Tank();
				break;
			case Archetype.Dps:
				Dps();
				break;
			default:
				throw new System.ArgumentException("AI does not support Common class archetype.");
		}
	}

	void Dps()
	{
		fsm.FixGridEntities();

		ReachFinder rf = new ReachFinder(LevelGrid.Instance, true);
		int _ran = Random.Range(0, 5);
		if(_ran < 4) //attack
		{
			Vector2Int[] reachables = rf.FindDiamondReach(activeEntity.GetGridPosition().x, activeEntity.GetGridPosition().y, activeEntity.InstaCat.movePoints);

			Vector2Int targetCell = new Vector2Int(100, 100);
			Entity targetEntity = activeEntity;

			int _ran2 = Random.Range(0, 2);

			foreach (Entity entity in fsm.entities)
			{
				if (_ran2 == 0)
				{
					if (entity.isPlayerEntity && entity.InstaCat.catClass == Archetype.Support)
					{
						targetEntity = entity;
						break;
					}
					
				}
				else
				{
					if (entity.isPlayerEntity && entity.InstaCat.catClass == Archetype.Tank)
					{
						targetEntity = entity;
						break;
					}
				}
			}

			if (targetEntity == activeEntity)
			{
				fsm.WaitAndPlayNextTurn();
				return;
			}

			targetCell = reachables[0];

			foreach(Vector2Int cell in reachables)
			{
				if(Mathf.Abs(cell.x - targetEntity.GetGridPosition().x) + Mathf.Abs(cell.y - targetEntity.GetGridPosition().y) < Mathf.Abs(targetCell.x - targetEntity.GetGridPosition().x) + Mathf.Abs(targetCell.y - targetEntity.GetGridPosition().y))
				{
					targetCell = cell;
				}
			}

			#region Franchement t'as pas envie de l'ouvrir celle-là

			if(LevelGrid.Instance.GetCell(targetCell).entityOnCell != null || LevelGrid.Instance.GetCell(targetCell).isWall)
			{
				if(LevelGrid.Instance.Exist(new Vector2Int(targetCell.x + 1, targetCell.y)))
				{
					if (LevelGrid.Instance.cells[targetCell.x + 1, targetCell.y].entityOnCell == null && !LevelGrid.Instance.cells[targetCell.x + 1, targetCell.y].isWall)
					{
						targetCell = new Vector2Int(targetCell.x + 1, targetCell.y);
					}
				}

				if (LevelGrid.Instance.Exist(new Vector2Int(targetCell.x - 1, targetCell.y)))
				{
					if (LevelGrid.Instance.cells[targetCell.x - 1, targetCell.y].entityOnCell == null && !LevelGrid.Instance.cells[targetCell.x - 1, targetCell.y].isWall)
					{
						targetCell = new Vector2Int(targetCell.x - 1, targetCell.y);
					}
				}

				if (LevelGrid.Instance.Exist(new Vector2Int(targetCell.x, targetCell.y + 1)))
				{
					if (LevelGrid.Instance.cells[targetCell.x, targetCell.y + 1].entityOnCell == null && !LevelGrid.Instance.cells[targetCell.x, targetCell.y + 1].isWall)
					{
						targetCell = new Vector2Int(targetCell.x, targetCell.y + 1);
					}
				}

				if (LevelGrid.Instance.Exist(new Vector2Int(targetCell.x, targetCell.y - 1)))
				{
					if (LevelGrid.Instance.cells[targetCell.x, targetCell.y - 1].entityOnCell == null && !LevelGrid.Instance.cells[targetCell.x, targetCell.y - 1].isWall)
					{
						targetCell = new Vector2Int(targetCell.x, targetCell.y - 1);
					}
				}
			}

			#endregion

			PathFinder pf = new PathFinder();
			activeEntity.MoveAlongPath(pf.FindPath(levelCells, activeEntity.GetGridPosition().x, activeEntity.GetGridPosition().y, targetCell.x, targetCell.y));

			if(Vector2Int.Distance(activeEntity.GetGridPosition(), targetEntity.GetGridPosition()) < 4)
			{
				fsm.AttackTarget(activeEntity, targetEntity);
				BattleInformationManager.Instance.Notifiate(new NotificationProps(activeEntity, targetEntity, true, null, "Strike", $"{activeEntity.InstaCat.catName} scratched {targetEntity.InstaCat.catName}."));
			}
		}
		fsm.WaitAndPlayNextTurn();
	}

	void Tank()
	{
		fsm.FixGridEntities();

		ReachFinder rf = new ReachFinder(LevelGrid.Instance, true);
		int _ran = Random.Range(0, 5);
		if (_ran < 4) //attack
		{
			Vector2Int[] reachables = rf.FindDiamondReach(activeEntity.GetGridPosition().x, activeEntity.GetGridPosition().y, activeEntity.InstaCat.movePoints);

			Vector2Int targetCell = new Vector2Int(100, 100);
			Entity targetEntity = activeEntity;

			int _ran2 = Random.Range(0, 2);

			foreach (Entity entity in fsm.entities)
			{
				if (_ran2 == 0)
				{
					if (entity.isPlayerEntity && entity.InstaCat.catClass == Archetype.Support)
					{
						targetEntity = entity;
						break;
					}

				}
				else
				{
					if (entity.isPlayerEntity && entity.InstaCat.catClass == Archetype.Dps)
					{
						targetEntity = entity;
						break;
					}
				}
			}

			if (targetEntity == activeEntity)
			{
				fsm.WaitAndPlayNextTurn();
				return;
			}

			targetCell = reachables[0];

			foreach (Vector2Int cell in reachables)
			{
				if (Mathf.Abs(cell.x - targetEntity.GetGridPosition().x) + Mathf.Abs(cell.y - targetEntity.GetGridPosition().y) < Mathf.Abs(targetCell.x - targetEntity.GetGridPosition().x) + Mathf.Abs(targetCell.y - targetEntity.GetGridPosition().y))
				{
					targetCell = cell;
				}
			}

			#region Franchement t'as pas envie de l'ouvrir celle-là

			if (LevelGrid.Instance.GetCell(targetCell).entityOnCell != null || LevelGrid.Instance.GetCell(targetCell).isWall)
			{
				if (LevelGrid.Instance.Exist(new Vector2Int(targetCell.x + 1, targetCell.y)))
				{
					if (LevelGrid.Instance.cells[targetCell.x + 1, targetCell.y].entityOnCell == null && !LevelGrid.Instance.cells[targetCell.x + 1, targetCell.y].isWall)
					{
						targetCell = new Vector2Int(targetCell.x + 1, targetCell.y);
					}
				}

				if (LevelGrid.Instance.Exist(new Vector2Int(targetCell.x - 1, targetCell.y)))
				{
					if (LevelGrid.Instance.cells[targetCell.x - 1, targetCell.y].entityOnCell == null && !LevelGrid.Instance.cells[targetCell.x - 1, targetCell.y].isWall)
					{
						targetCell = new Vector2Int(targetCell.x - 1, targetCell.y);
					}
				}

				if (LevelGrid.Instance.Exist(new Vector2Int(targetCell.x, targetCell.y + 1)))
				{
					if (LevelGrid.Instance.cells[targetCell.x, targetCell.y + 1].entityOnCell == null && !LevelGrid.Instance.cells[targetCell.x, targetCell.y + 1].isWall)
					{
						targetCell = new Vector2Int(targetCell.x, targetCell.y + 1);
					}
				}

				if (LevelGrid.Instance.Exist(new Vector2Int(targetCell.x, targetCell.y - 1)))
				{
					if (LevelGrid.Instance.cells[targetCell.x, targetCell.y - 1].entityOnCell == null && !LevelGrid.Instance.cells[targetCell.x, targetCell.y - 1].isWall)
					{
						targetCell = new Vector2Int(targetCell.x, targetCell.y - 1);
					}
				}
			}

			#endregion

			PathFinder pf = new PathFinder();
			activeEntity.MoveAlongPath(pf.FindPath(levelCells, activeEntity.GetGridPosition().x, activeEntity.GetGridPosition().y, targetCell.x, targetCell.y));

			if (Vector2Int.Distance(activeEntity.GetGridPosition(), targetEntity.GetGridPosition()) < 4)
			{
				fsm.AttackTarget(activeEntity, targetEntity);
				BattleInformationManager.Instance.Notifiate(new NotificationProps(activeEntity, targetEntity, true, null, "Strike", $"{activeEntity.InstaCat.catName} scratched {targetEntity.InstaCat.catName}."));
			}
		}
		fsm.WaitAndPlayNextTurn();
	}

	void Support()
	{
		fsm.FixGridEntities();

		ReachFinder rf = new ReachFinder(LevelGrid.Instance, true);
		int _ran = Random.Range(0, 5);
		if (_ran < 4) //attack
		{
			Vector2Int[] reachables = rf.FindDiamondReach(activeEntity.GetGridPosition().x, activeEntity.GetGridPosition().y, activeEntity.InstaCat.movePoints);

			Vector2Int targetCell = new Vector2Int(100, 100);
			Entity targetEntity = activeEntity;

			int _ran2 = Random.Range(0, 2);

			foreach (Entity entity in fsm.entities)
			{
				if (_ran2 == 0)
				{
					if (!entity.isPlayerEntity && entity.InstaCat.catClass == Archetype.Dps)
					{
						targetEntity = entity;
						break;
					}

				}
				else
				{
					if (!entity.isPlayerEntity && entity.InstaCat.catClass == Archetype.Tank)
					{
						targetEntity = entity;
						break;
					}
				}
			}

			if (targetEntity == activeEntity)
			{
				fsm.WaitAndPlayNextTurn();
				return;
			}


			targetCell = reachables[0];

			foreach (Vector2Int cell in reachables)
			{
				if (Mathf.Abs(cell.x - targetEntity.GetGridPosition().x) + Mathf.Abs(cell.y - targetEntity.GetGridPosition().y) < Mathf.Abs(targetCell.x - targetEntity.GetGridPosition().x) + Mathf.Abs(targetCell.y - targetEntity.GetGridPosition().y))
				{
					targetCell = cell;
				}
			}

			#region Franchement t'as pas envie de l'ouvrir celle-là

			if (LevelGrid.Instance.GetCell(targetCell).entityOnCell != null || LevelGrid.Instance.GetCell(targetCell).isWall)
			{
				if (LevelGrid.Instance.Exist(new Vector2Int(targetCell.x + 1, targetCell.y)))
				{
					if (LevelGrid.Instance.cells[targetCell.x + 1, targetCell.y].entityOnCell == null && !LevelGrid.Instance.cells[targetCell.x + 1, targetCell.y].isWall)
					{
						targetCell = new Vector2Int(targetCell.x + 1, targetCell.y);
					}
				}

				if (LevelGrid.Instance.Exist(new Vector2Int(targetCell.x - 1, targetCell.y)))
				{
					if (LevelGrid.Instance.cells[targetCell.x - 1, targetCell.y].entityOnCell == null && !LevelGrid.Instance.cells[targetCell.x - 1, targetCell.y].isWall)
					{
						targetCell = new Vector2Int(targetCell.x - 1, targetCell.y);
					}
				}

				if (LevelGrid.Instance.Exist(new Vector2Int(targetCell.x, targetCell.y + 1)))
				{
					if (LevelGrid.Instance.cells[targetCell.x, targetCell.y + 1].entityOnCell == null && !LevelGrid.Instance.cells[targetCell.x, targetCell.y + 1].isWall)
					{
						targetCell = new Vector2Int(targetCell.x, targetCell.y + 1);
					}
				}

				if (LevelGrid.Instance.Exist(new Vector2Int(targetCell.x, targetCell.y - 1)))
				{
					if (LevelGrid.Instance.cells[targetCell.x, targetCell.y - 1].entityOnCell == null && !LevelGrid.Instance.cells[targetCell.x, targetCell.y - 1].isWall)
					{
						targetCell = new Vector2Int(targetCell.x, targetCell.y - 1);
					}
				}
			}

			#endregion

			PathFinder pf = new PathFinder();
			activeEntity.MoveAlongPath(pf.FindPath(levelCells, activeEntity.GetGridPosition().x, activeEntity.GetGridPosition().y, targetCell.x, targetCell.y));

			if (Vector2Int.Distance(activeEntity.GetGridPosition(), targetEntity.GetGridPosition()) < 4)
			{
				fsm.HealTarget(activeEntity, targetEntity);
				BattleInformationManager.Instance.Notifiate(new NotificationProps(activeEntity, targetEntity, true, null, "Strike", $"{activeEntity.InstaCat.catName} scratched {targetEntity.InstaCat.catName}."));
			}
		}

		fsm.FixGridEntities();

		fsm.WaitAndPlayNextTurn();
	}

	//public override void OnStateUpdate()
	//{

	//}

	//public override void OnStateFixedUpdate()
	//{

	//}

	//public override void OnStateLateUpdate()
	//{

	//}

	public override void OnStateExit()
	{
		fsm.FixGridEntities();
	}
}