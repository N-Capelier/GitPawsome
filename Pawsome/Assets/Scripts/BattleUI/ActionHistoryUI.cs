using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionHistoryUI : MonoBehaviour
{
    [SerializeField]
    Transform notificationsRoot;
    [SerializeField]
    HistoryNotification notificationTemplate;
    [SerializeField]
    int maxDisplayNotification;

    List<HistoryNotification> allNotifications;
    BattleUIMode mode;

    private void OnDestroy()
    {
        OnUnmount();
    }

    public void OnMount(BattleUIMode _mode)
    {
        mode = _mode;
        notificationTemplate.gameObject.SetActive(false);
        allNotifications = new List<HistoryNotification>();
    }

    public void OnUnmount()
    {

    }

    public void PushNotification(NotificationProps props)
    {
        var noti = Instantiate(notificationTemplate, notificationsRoot);
        allNotifications.Add(noti);
        noti.OnMount(props);
        noti.gameObject.transform.SetAsFirstSibling();
        noti.gameObject.SetActive(true);

        if(allNotifications.Count > maxDisplayNotification)
        {
            var notiToRemove = allNotifications[0];
            allNotifications.RemoveAt(0);
            Destroy(notiToRemove.gameObject);
        }
    }
}
