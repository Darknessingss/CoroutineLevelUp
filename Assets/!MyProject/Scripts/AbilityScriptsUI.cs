using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

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

    private Color DefaultColorPlayer;
    [SerializeField] private Renderer PlayerRender;
    private Coroutine changeColorCoroutine;

    public static class Ability1Spell
    {
        public static event Action<int> ChangeColorPlayer;
    }

    private void Awake()
    {
        if (PlayerRender != null)
        {
            DefaultColorPlayer = PlayerRender.material.color;
        }

        Ability1Spell.ChangeColorPlayer += ChangePlayerMaterial;
    }

    private void Start()
    {
        if (Ability1 != null)
        {
            Ability1.onClick.AddListener(CastAbility1);
        }
    }

    private void OnDestroy()
    {
        Ability1Spell.ChangeColorPlayer -= ChangePlayerMaterial;

        if (changeColorCoroutine != null)
        {
            StopCoroutine(changeColorCoroutine);
        }
    }

    private void ChangePlayerMaterial(int duration)
    {
    }

    public void CastAbility1()
    {
        StartCoroutine(Ability1CooldownRoutine());
        StartColorChangeCoroutine();
    }

    private IEnumerator Ability1CooldownRoutine()
    {
        Ability1.interactable = false;

        float cooldownTime = TimerAbility;

        while (cooldownTime > 0)
        {
            if (TextCouldown != null)
            {
                TextCouldown.text = cooldownTime.ToString("F1");
            }

            if (Icon != null)
            {
                Color iconColor = Icon.color;
                iconColor.a = 0.5f;
                Icon.color = iconColor;
            }

            cooldownTime -= Time.deltaTime;
            yield return null;
        }

        Ability1.interactable = true;

        if (TextCouldown != null)
        {
            TextCouldown.text = "";
        }

        if (Icon != null)
        {
            Color iconColor = Icon.color;
            iconColor.a = 1f;
            Icon.color = iconColor;
        }
    }

    private void StartColorChangeCoroutine()
    {
        if (changeColorCoroutine != null)
        {
            StopCoroutine(changeColorCoroutine);
        }

        changeColorCoroutine = StartCoroutine(ChangeColorRoutine());
    }

    private IEnumerator ChangeColorRoutine()
    {
        if (PlayerRender == null) yield break;

        Color currentColor = PlayerRender.material.color;

        PlayerRender.material.color = Color.red;
        Debug.Log("Color changed to RED");

        yield return new WaitForSeconds(5f);

        PlayerRender.material.color = DefaultColorPlayer;
        Debug.Log("Color returned to default");

        changeColorCoroutine = null;
    }
}