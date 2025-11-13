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

    [Header("Ability1")]
    public float TimerDurationChange = 1f;
    public float DurationCoroutineAbility1 = 5f;
    public Color CustomColor1 = Color.blue;
    public Color CustomColor2 = Color.black;
    public Color CustomColor3 = Color.white;
    public float DurationCoroutineAbility2 = 15f;



    [Header("Couldown")]
    private bool OnCouldown = false;
    private float CurrentCouldown1 = 0f;
    private float CurrentCouldown2 = 0f;
    private float CurrentCouldown3 = 0f;

    //private bool[] onCouldown = new bool[3];
    //private float[] currentCouldown = new float[3];

    #endregion Settings

    #region ButtonAbility1-2-3

    void Start()
    {
        if (Ability1 != null)
            Ability1.onClick.AddListener(OnClickAbility1);
        else
            Debug.LogError("Ability1 не назначена в инспекторе!");

        if (Ability2 != null)
            Ability2.onClick.AddListener(OnClickAbility2);
        else
            Debug.LogError("Ability2 не назначена в инспекторе!");

        if (Ability3 != null)
            Ability3.onClick.AddListener(OnClickAbility3);
        else
            Debug.LogError("Ability3 не назначена в инспекторе!");

        if (IconAbility3 != null)
            IconAbility3.gameObject.SetActive(false);

        if (IconAbility2 != null)
            IconAbility2.gameObject.SetActive(false);

        if (IconAbility1 != null)
            IconAbility1.gameObject.SetActive(false);

        if (TextCouldown1 != null)
            TextCouldown1.gameObject.SetActive(false);

        if (TextCouldown2 != null)
            TextCouldown2.gameObject.SetActive(false);

        if (TextCouldown3 != null)
            TextCouldown3.gameObject.SetActive(false);

        //SetAbilityUI(false, 0);
        //SetAbilityUI(false, 1);
        //SetAbilityUI(false, 2);

    }

    void Update()
    {
        //for(int i = 0; i < 3 ; i++)
        //    if (OnCouldown[i])
        //    {
        //        CurrentColdown[i] -= Time.deltaTime;
        //        UpdateCouldownUI();
        //    }
        //if (CurrentCouldown1[i])
        //{
        //    EndCouldown(i);
        //}

        if (OnCouldown)
        {
            CurrentCouldown1 -= Time.deltaTime;
            UpdateCouldownUI();
        }
        if (CurrentCouldown1 <= 0f)
        {
            EndCouldown();
        }

        if (OnCouldown)
        {
            CurrentCouldown2 -= Time.deltaTime;
            UpdateCouldownUI();
        }
        if (CurrentCouldown2 <= 0f)
        {
            EndCouldown();
        }

        if (OnCouldown)
        {
            CurrentCouldown3 -= Time.deltaTime;
            UpdateCouldownUI();
        }
        if (CurrentCouldown3 <= 0f)
        {
            EndCouldown();
        }
    }

    public void OnClickAbility1()
    {
        if(!OnCouldown)
        {
            StartCouldown();
            Debug.Log("Кнопка и есть КД!");
        }

        if (ChangeColorCoroutine != null)
        {
            Debug.Log("Корутина уже запущена!");
            return;
        }

        Debug.Log("Запускаем корутину");
        ChangeColorCoroutine = StartCoroutine(ColorChangerMaterial1());


        //if (!OnCouldown[0])
        //{
        //    StartCouldown(0);
        //    Debug.Log("Кнопка и есть КД!");
        //}

        //if (ChangeColorCoroutine[0] != null)
        //{
        //    Debug.Log("Корутина уже запущена!");
        //    return;
        //}

        //Debug.Log("Запускаем корутину");
        //ChangeColorCoroutine[0] = StartCoroutine(ColorChangerMaterial(0));
    }



    public void OnClickAbility2()
    {
        if (!OnCouldown)
        {
            StartCouldown();
            Debug.Log("Кнопка и есть КД!");
        }

        if (ChangeColorCoroutine != null)
        {
            Debug.Log("Корутина уже запущена!");
            return;
        }

        Debug.Log("Запускаем корутину");
        ChangeColorCoroutine = StartCoroutine(ColorChangerMaterial2());


        //if (!OnCouldown[1])
        //{
        //    StartCouldown(1);
        //    Debug.Log("Кнопка и есть КД!");
        //}

        //if (ChangeColorCoroutine[1] != null)
        //{
        //    Debug.Log("Корутина уже запущена!");
        //    return;
        //}

        //Debug.Log("Запускаем корутину");
        //ChangeColorCoroutine[1] = StartCoroutine(ColorChangerMaterial(1));
    }

    public void OnClickAbility3()
    {
        if (!OnCouldown)
        {
            StartCouldown();
            Debug.Log("Кнопка и есть КД!");
        }

        if (ChangeColorCoroutine != null)
        {
            Debug.Log("Корутина уже запущена!");
            return;
        }

        Debug.Log("Запускаем корутину");
        ChangeColorCoroutine = StartCoroutine(ColorChangerMaterial3());

        //if (!OnCouldown[2])
        //{
        //    StartCouldown(2);
        //    Debug.Log("Кнопка и есть КД!");
        //}

        //if (ChangeColorCoroutine[2] != null)
        //{
        //    Debug.Log("Корутина уже запущена!");
        //    return;
        //}

        //Debug.Log("Запускаем корутину");
        //ChangeColorCoroutine[2] = StartCoroutine(ColorChangerMaterial(2));
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


        //switch (AbilityIndex)
        //{
        //    case 0:
        //        if (IconAbility1 != null && IconAbility1.type == Image.Type.Filled)
        //            IconAbility1.fillAmount = currentCouldown[0] / TimerAbility1;
        //        if (TextCouldown1 != null)
        //            TextCouldown1.text = Mathf.CeilToInt(currentCouldown[0]).ToString();
        //        break;
        //    case 1:
        //        if (IconAbility2 != null && IconAbility2.type == Image.Type.Filled)
        //            IconAbility2.fillAmount = currentCouldown[1] / TimerAbility2;
        //        if (TextCouldown2 != null)
        //            TextCouldown2.text = Mathf.CeilToInt(currentCouldown[1]).ToString();
        //        break;
        //    case 2:
        //        if (IconAbility3 != null && IconAbility3.type == Image.Type.Filled)
        //            IconAbility3.fillAmount = currentCouldown[2] / TimerAbility3;
        //        if (TextCouldown3 != null)
        //            TextCouldown3.text = Mathf.CeilToInt(currentCouldown[2]).ToString();
        //        break;
        //}
    }

    private void EndCouldown()
    {
        OnCouldown = false;
        CurrentCouldown1 = 0f;
        CurrentCouldown2 = 0f;
        CurrentCouldown3 = 0f;

        if (Ability1 != null)
        {
            Ability1.interactable = true;
        }

        if (IconAbility1 != null)
            IconAbility1.gameObject.SetActive(false);

        if (TextCouldown1 != null)
            TextCouldown1.gameObject.SetActive(false);

        Debug.Log("Перезарядка завершена!");


        OnCouldown = false;
        CurrentCouldown1 = 0f;
        CurrentCouldown2 = 0f;
        CurrentCouldown3 = 0f;

        if (Ability2 != null)
        {
            Ability2.interactable = true;
        }

        if (IconAbility2 != null)
            IconAbility2.gameObject.SetActive(false);

        if (TextCouldown2 != null)
            TextCouldown2.gameObject.SetActive(false);

        Debug.Log("Перезарядка завершена!");

        OnCouldown = false;
        CurrentCouldown1 = 0f;
        CurrentCouldown2 = 0f;
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

    //private void EndCouldown(int AbilityIndex)
    //{
    //    OnCouldown[AbilityIndex] = false;
    //    CurrentCouldown[AbilityIndex] = 0f;
    //    SetAbilityUI(false, AbilityIndex);

    //    switch (AbilityIndex)
    //    {
    //        case 0:
    //            if (Ability1 != null) Ability1.interactable = true;
    //            break;
    //        case 1:
    //            if (Ability2 != null) Ability2.interactable = true;
    //            break;
    //        case 2:
    //            if (Ability3 != null) Ability3.interactable = true;
    //            break;
    //    }

    //    Debug.Log($"Перезарядка способности {AbilityIndex + 1} завершена!");
    //}

    private void StartCouldown()
    {
        OnCouldown = true;
        CurrentCouldown1 = TimerAbility1;
        CurrentCouldown2 = TimerAbility2;
        CurrentCouldown3 = TimerAbility3;

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

        Debug.Log("Начата перезарядка: " + TimerAbility2 + " секунд");

        if (Ability2 != null)
        {
            Ability2.interactable = false;
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

        Debug.Log("Начата перезарядка: " + TimerAbility3 + " секунд");
    }

    //private void StartCouldown(int AbilityIndex)
    //{
    //    OnCouldown[AbilityIndex] = true;

    //    switch (AbilityIndex)
    //    {
    //        case 0:
    //            CurrentCouldown[0] = TimerAbility1;
    //            if (Ability1 != null) Ability1.interactable = false;
    //            break;
    //        case 1:
    //            CurrentCouldown[1] = TimerAbility2;
    //            if (Ability2 != null) Ability2.interactable = false;
    //            break;
    //        case 2:
    //            CurrentCouldown[2] = TimerAbility3;
    //            if (Ability3 != null) Ability3.interactable = false;
    //            break;
    //    }

    //    SetAbilityUI(true, AbilityIndex);
    //    Debug.Log($"Начата перезарядка способности {AbilityIndex + 1}: {GetTimerForAbility(AbilityIndex)} секунд");
    //}

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

        yield return new WaitForSeconds(DurationCoroutineAbility1);

        PlayerRender.material.color = originalColor;

        ChangeColorCoroutine = null;
        Debug.Log("Корутина завершена");
    }

    private IEnumerator ColorChangerMaterial3()
    {
        Debug.Log("Начата корутина");

        Color originalColor = PlayerRender.material.color;

        PlayerRender.material.color = CustomColor3;

        yield return new WaitForSeconds(DurationCoroutineAbility1);

        PlayerRender.material.color = originalColor;

        ChangeColorCoroutine = null;
        Debug.Log("Корутина завершена");
    }

    public bool IsAbilityReady()
    {
        return !OnCouldown;
    }

    public float GetRemainingCouldown()
    {
        return Math.Max(0f, CurrentCouldown1);
    }

    #endregion ButtonAbility1-2-3
}