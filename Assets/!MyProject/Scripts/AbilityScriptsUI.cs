using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using Unity.VisualScripting;

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
    private Coroutine ChangeColorCoroutine;

    [Header("Ability1")]
    public float TimerDurationChange = 1f;
    public float DurationCoroutineAbility1 = 5f;
    public Color CustomColor = Color.blue;

    [Header("Couldown")]
    private bool OnCouldown = false;
    private float CurrentCouldown = 0f;

    #region ButtonAbility1


    void Start()
    {
        if (Ability1 != null)
        {
            Ability1.onClick.AddListener(OnClickAbility1);
        }
        else
        {
            Debug.LogError("Ability1 не назначена в инспекторе!");
        }

        ButtonOverlay();
    }

    void Update()
    {
        
    }

    void ButtonOverlay()
    {
        if (Icon != null)
            Icon.gameObject.SetActive(false);

        if (TextCouldown != null)
            TextCouldown.gameObject.SetActive(false);
    }

    public void OnClickAbility1()
    {
        if (ChangeColorCoroutine != null)
        {
            Debug.Log("Корутина уже запущена!");
            return;
        }

        Debug.Log("Запускаем корутину");
        ChangeColorCoroutine = StartCoroutine(ColorChangerMaterial());
    }

    private IEnumerator ColorChangerMaterial()
    {
        Debug.Log("Начата корутина");

        Color originalColor = PlayerRender.material.color;

        PlayerRender.material.color = CustomColor;

        yield return new WaitForSeconds(DurationCoroutineAbility1);

        PlayerRender.material.color = originalColor;

        ChangeColorCoroutine = null;
        Debug.Log("Корутина завершена");
    }
    #endregion ButtonAbility1
}