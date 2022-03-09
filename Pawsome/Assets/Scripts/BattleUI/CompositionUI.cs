using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CompositionUI : MonoBehaviour
{
    [Serializable]
    internal class Composition
    {
        [SerializeField]
        internal TextMeshProUGUI playerName;
        [SerializeField]
        internal CompositionPortrait portraitTemplate;
        [SerializeField]
        internal Transform portraitRoot;

        public void OnMount(string _playerName, Entity[] playersEntities, CompositionUI compUI)
        {
            playerName.text = _playerName;

            foreach(Entity entity in playersEntities)
            {
                var por = Instantiate(portraitTemplate, portraitRoot);
                por.OnMount(entity, compUI);
            }

            portraitTemplate.gameObject.SetActive(false);
        }
    }

    [SerializeField]
    Composition playerOneComp;
    [SerializeField]
    Composition playerTwoComp;

    [HideInInspector]
    public BattleUIMode mode;

    public void OnMount(PlayerInfo playerOne, PlayerInfo playerTwo, BattleUIMode _mode)
    {
        mode = _mode;
        playerOneComp.OnMount(playerOne.playerName, playerOne.entities, this);
        playerTwoComp.OnMount(playerTwo.playerName, playerTwo.entities, this);
    }

}
