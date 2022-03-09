using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public struct NotificationProps
{
    public Entity initiator;
    public Entity target;
    public Sprite icon;

    public string title;
    public string description;

    public bool IsPositive => initiator.isPlayerEntity == target.isPlayerEntity;

    public NotificationProps(Entity _initiator, Entity _target, Sprite _icon, string _title, string _description)
    {
        initiator = _initiator;
        target = _target;
        icon = _icon;
        title = _title;
        description = _description;
    }
}

public class HistoryNotification : MonoBehaviour
{
    [SerializeField]
    Image icon;
    [SerializeField]
    TextMeshProUGUI title;
    [SerializeField]
    TextMeshProUGUI description;

    [Header("Initiator/Target Section")]
    [SerializeField]
    Image initiatorPortrait;
    [SerializeField]
    Image initiatorBorder;
    [SerializeField]
    Image targetPortrait;
    [SerializeField]
    Image targetBorder;
    [SerializeField]
    Image arrow;

    public void OnMount(NotificationProps props)
    {
        icon.sprite = props.icon;
        title.text = props.title;
        description.text = props.description;

        initiatorPortrait.sprite = props.initiator.InstaCat.CatSprite;
        targetPortrait.sprite = props.target.InstaCat.CatSprite;
        initiatorBorder.color = BattleUIMode.GetBorderColor(props.initiator, true);
        targetBorder.color = BattleUIMode.GetBorderColor(props.target, true);

        if (props.IsPositive) arrow.color = Color.green;
        else arrow.color = Color.red;
    }


}
