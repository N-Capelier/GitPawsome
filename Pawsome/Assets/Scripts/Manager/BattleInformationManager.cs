using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Nicolas
/// Last modified by Nicolas
/// </summary>
public class BattleInformationManager : Singleton<BattleInformationManager>
{
	public InstaCat[] instaCats;
	[SerializeField] BattleUIMode battleUIMode;

	[Header("Debug")]
	[SerializeField] InstaCat[] fakeInstaCatArray;

	private void Awake()
	{
		CreateSingleton(true);
	}

	public InstaCat[] GetInstaCats()
	{
		//if(PlayerManager.Instance != null)
		//{
		//	List<CatBag> t = PlayerManager.Instance.MyCatBag;
		//	List<InstaCat> list = new List<InstaCat>();
		//	foreach(CatBag catBag in t)
		//	{
		//		list.Add(catBag.MyCat);
		//	}

		//	return list.ToArray();
		//}
		return fakeInstaCatArray;
	}

	public void Notifiate(NotificationProps props)
	{
		battleUIMode.historyUI.PushNotification(props);
	}
}
