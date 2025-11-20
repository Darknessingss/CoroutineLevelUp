using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using Unity.VisualScripting;

public class AbilityScriptsUI : MonoBehaviour
{
    #region Settings
    [Header("ButtonAbility")]
    public Button Ability1;
    public Button Ability2;
    public Button Ability3;

    [Header("ButtonSettings")]
    public int TimerAbility1 = 5;
    public int TimerAbility2 = 15;
    public int TimerAbility3 = 25;
    public Image IconAbility1;
    public Image IconAbility2;
    public Image IconAbility3;
    public TMP_Text TextCouldown1;
    public TMP_Text TextCouldown2;
    public TMP_Text TextCouldown3;

    [Header("SettingsObject")]
    [SerializeField] private GameObject Player;
    [SerializeField] private Renderer PlayerRender;
    private Coroutine ChangeColorCoroutine;
    private Coroutine ChangeColorCoroutineBlack;
    private Coroutine ChangeColorCoroutineWhite;

    [Header("Ability1")]
    public float TimerDurationChange = 1f;
    public float DurationCoroutineAbility1 = 5f;
    public float DurationCoroutineAbility2 = 15f;
    public float DurationCoroutineAbility3 = 25f;
    public Color CustomColor1 = Color.blue;
    public Color CustomColor2 = Color.green;
    public Color CustomColor3 = Color.white;

    [Header("Couldown")]
    private bool OnCouldown1 = false;
    private bool OnCouldown2 = false;
    private bool OnCouldown3 = false;
    private float CurrentCouldown1 = 0f;
    private float CurrentCouldown2 = 0f;
    private float CurrentCouldown3 = 0f;

    #endregion Settings

    #region ButtonAbility1-2-3

    void Start()
    {

        if (IconAbility1 != null)
            IconAbility1.gameObject.SetActive(false);

        if (IconAbility2 != null)
            IconAbility2.gameObject.SetActive(false);

        if (IconAbility3 != null)
            IconAbility3.gameObject.SetActive(false);

        if (TextCouldown1 != null)
            TextCouldown1.gameObject.SetActive(false);

        if (TextCouldown2 != null)
            TextCouldown2.gameObject.SetActive(false);

        if (TextCouldown3 != null)
            TextCouldown3.gameObject.SetActive(false);

    }

    void Update()
    {
        if (OnCouldown1)
        {
            CurrentCouldown1 -= Time.deltaTime;
            UpdateCouldownUI();
        }
        if (CurrentCouldown1 <= 0f)
        {
            EndCouldown1();
        }

        if (OnCouldown2)
        {
            CurrentCouldown2 -= Time.deltaTime;
            UpdateCouldownUI();
        }
        if (CurrentCouldown2 <= 0f)
        {
            EndCouldown2();
        }

        if (OnCouldown3)
        {
            CurrentCouldown3 -= Time.deltaTime;
            UpdateCouldownUI();
        }
        if (CurrentCouldown3 <= 0f)
        {
            EndCouldown3();
        }
    }

    public void OnClickAbility1()
    {
        if(!OnCouldown1)
        {
            StartCouldown1();
            Debug.Log("Кнопка и есть КД!");
        }

            Ability1.interactable = false;

        Debug.Log("Запускаем корутину");
        ChangeColorCoroutine = StartCoroutine(ColorChangerMaterial1());
    }

    public void OnClickAbility2()
    {
        if (!OnCouldown2)
        {
            StartCouldown2();
            Debug.Log("Кнопка и есть КД!");
        }

            Ability2.interactable = false;

        Debug.Log("Запускаем корутину");
        ChangeColorCoroutineBlack = StartCoroutine(ColorChangerMaterial2());
    }

    public void OnClickAbility3()
    {
        if (!OnCouldown3)
        {
            StartCouldown3();
            Debug.Log("Кнопка и есть КД!");
        }

            Ability3.interactable = false;

        Debug.Log("Запускаем корутину");
        ChangeColorCoroutineWhite = StartCoroutine(ColorChangerMaterial3());
    }

    private void UpdateCouldownUI()
    {
        if (IconAbility1 != null && TextCouldown1 != null)
        {
            if (IconAbility1 != null && IconAbility1.type == Image.Type.Filled)
            {
                IconAbility1.fillAmount = CurrentCouldown1 / TimerAbility1;
            }
            TextCouldown1.text = Mathf.CeilToInt(CurrentCouldown1).ToString();
        }

        if (IconAbility2 != null && TextCouldown2 != null)
        {
            if (IconAbility2 != null && IconAbility2.type == Image.Type.Filled)
            {
                IconAbility2.fillAmount = CurrentCouldown2 / TimerAbility2;
            }
            TextCouldown2.text = Mathf.CeilToInt(CurrentCouldown2).ToString();
        }

        if (IconAbility3 != null && TextCouldown3 != null)
        {
            if (IconAbility3 != null && IconAbility3.type == Image.Type.Filled)
            {
                IconAbility3.fillAmount = CurrentCouldown3 / TimerAbility3;
            }
            TextCouldown3.text = Mathf.CeilToInt(CurrentCouldown3).ToString();
        }
    }

    private void EndCouldown1()
    {
        OnCouldown1 = false;
        CurrentCouldown1 = 0f;

        if (Ability1 != null)
        {
            Ability1.interactable = true;
        }

        if (IconAbility1 != null)
            IconAbility1.gameObject.SetActive(false);

        if (TextCouldown1 != null)
            TextCouldown1.gameObject.SetActive(false);

        Debug.Log("Перезарядка завершена!");
    }

    private void EndCouldown2()
    {
        OnCouldown2 = false;
        CurrentCouldown2 = 0f;

        if (Ability2 != null)
        {
            Ability2.interactable = true;
        }

        if (IconAbility2 != null)
            IconAbility2.gameObject.SetActive(false);

        if (TextCouldown2 != null)
            TextCouldown2.gameObject.SetActive(false);

        Debug.Log("Перезарядка завершена!");
    }

    private void EndCouldown3()
    {
        OnCouldown3 = false;
        CurrentCouldown3 = 0f;

        if (Ability3 != null)
        {
            Ability3.interactable = true;
        }

        if (IconAbility3 != null)
            IconAbility3.gameObject.SetActive(false);

        if (TextCouldown3 != null)
            TextCouldown3.gameObject.SetActive(false);

        Debug.Log("Перезарядка завершена!");
    }

    private void StartCouldown1()
    {
        OnCouldown1 = true;
        CurrentCouldown1 = TimerAbility1;

        if (Ability1 != null)
        {
            Ability1.interactable = false;
        }


        if (IconAbility1 != null)
        {
            IconAbility1.gameObject.SetActive(true);


            if (IconAbility1 != null && IconAbility1.type == Image.Type.Filled)
            {
                IconAbility1.fillAmount = 1f;
            }
        }
        if (TextCouldown1 != null)
        {
            TextCouldown1.gameObject.SetActive(true);
            TextCouldown1.text = TimerAbility1.ToString();
        }
    }

    private void StartCouldown2()
    {
        OnCouldown2 = true;
        CurrentCouldown2 = TimerAbility2;

        if (Ability2 != null)
        {
            Ability2.interactable = false;
        }


        if (IconAbility2 != null)
        {
            IconAbility2.gameObject.SetActive(true);


            if (IconAbility2 != null && IconAbility2.type == Image.Type.Filled)
            {
                IconAbility2.fillAmount = 1f;
            }
        }
        if (TextCouldown2 != null)
        {
            TextCouldown2.gameObject.SetActive(true);
            TextCouldown2.text = TimerAbility2.ToString();
        }
    }

    private void StartCouldown3()
    {
        OnCouldown3 = true;
        CurrentCouldown3 = TimerAbility3;

        if (Ability3 != null)
        {
            Ability3.interactable = false;
        }


        if (IconAbility3 != null)
        {
            IconAbility3.gameObject.SetActive(true);


            if (IconAbility3 != null && IconAbility3.type == Image.Type.Filled)
            {
                IconAbility3.fillAmount = 1f;
            }
        }
        if (TextCouldown3 != null)
        {
            TextCouldown3.gameObject.SetActive(true);
            TextCouldown3.text = TimerAbility3.ToString();
        }
    }

    private IEnumerator ColorChangerMaterial1()
    {
        Debug.Log("Начата корутина");

        Color originalColor = PlayerRender.material.color;

        PlayerRender.material.color = CustomColor1;

        yield return new WaitForSeconds(DurationCoroutineAbility1);

        PlayerRender.material.color = originalColor;

        ChangeColorCoroutine = null;
        Debug.Log("Корутина завершена");
    }

    private IEnumerator ColorChangerMaterial2()
    {
        Debug.Log("Начата корутина");

        Color originalColor = PlayerRender.material.color;

        PlayerRender.material.color = CustomColor2;

        yield return new WaitForSeconds(DurationCoroutineAbility2);

        PlayerRender.material.color = originalColor;

        ChangeColorCoroutineBlack = null;
        Debug.Log("Корутина завершена");
    }

    private IEnumerator ColorChangerMaterial3()
    {
        Debug.Log("Начата корутина");

        Color originalColor = PlayerRender.material.color;

        PlayerRender.material.color = CustomColor3;

        yield return new WaitForSeconds(DurationCoroutineAbility3);

        PlayerRender.material.color = originalColor;

        ChangeColorCoroutineWhite = null;
        Debug.Log("Корутина завершена");
    }

    public bool IsAbilityReady(int AbilityNumber)
    {
        switch (AbilityNumber)
        {
            case 1: return !OnCouldown1;
            case 2: return !OnCouldown2;
            case 3: return !OnCouldown3;
            default: return false;
        }
    }

    public float GetRemainingCouldown(int AbilityNumber)
    {
        switch (AbilityNumber)
        {
            case 1: return Mathf.Max(0f, CurrentCouldown1);
            case 2: return Mathf.Max(0f, CurrentCouldown2);
            case 3: return Mathf.Max(0f, CurrentCouldown3);
            default: return 0f;
        }
    }

    #endregion ButtonAbility1-2-3
}