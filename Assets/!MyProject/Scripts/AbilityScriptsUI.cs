using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;


[System.Serializable]
public class AbilityScriptsUI : MonoBehaviour
{
    [Header("ButtonAbility")]
    public Button Ability1;
    public Button Ability2;
    public Button Ability3;

    [Header("ButtonSettings")]
    public int TimerAbility = 5;
    public Image Icon;
    public TMP_Text TextCouldown;


    [Header("SettingsObject")]
    [SerializeField] private GameObject Player;



    public static class Ability1Spell
    {
        public static event Action<int> ChangeColorPlayer;
    }

    private void Awake()
    {
        Ability1Spell.ChangeColorPlayer += ChangePlayerMaterial;
    }


    private void OnDestroy()
    {
        Ability1Spell.ChangeColorPlayer -= ChangePlayerMaterial;
    }
}

