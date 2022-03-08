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

        public void OnMount(string _playerName, List<Entity> playersEntities, CompositionUI compUI)
        {
            playerName.text = _playerName;

            foreach(Entity entity in playersEntities)
            {
                var por = Instantiate(portraitTemplate, portraitRoot);
                por.OnMount(entity, compUI);
            }
        }
    }

    [SerializeField]
    Composition playerOneComp;
    [SerializeField]
    Composition playerTwoComp;

    [HideInInspector]
    public BattleUIMode mode;

    public void OnMount(PlayerEntity playerOne, PlayerEntity playerTwo, BattleUIMode _mode)
    {
        playerOneComp.OnMount(playerOne.name, new List<Entity>(), this);
        playerTwoComp.OnMount(playerTwo.name, new List<Entity>(), this);
        mode = _mode;
    }

}
