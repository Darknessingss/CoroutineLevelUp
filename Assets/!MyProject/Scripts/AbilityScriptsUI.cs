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
    public int TimerAbility = 5;
    public Image IconAbility1;
    public Image IconAbility2;
    public Image IconAbility3;
    public TMP_Text TextCouldown1;
    public TMP_Text TextCouldown2;
    public TMP_Text TextCouldown3;

    [Header("SettingsObject")]
    [SerializeField] private GameObject Player;
    private Color DefaultColorPlayer;
    [SerializeField] private Renderer PlayerRender;
    private Coroutine ChangeColorCoroutine;
    private Coroutine ChangeTransform;

    [Header("Ability1")]
    public float TimerDurationChange = 1f;
    public float DurationCoroutineAbility1 = 5f;
    public Color CustomColor = Color.blue;

    [Header("Ability2")]
    [SerializeField] private Transform PlayerTransformTeleport;
    public Vector3 teleportOffset = new Vector3(0, 0, 5);
    public bool CustomDestination = false;
    public float DurationCoroutineAbility2 = 15f;
    public bool useCustomDestination = false;
    [SerializeField] private Transform TeleportDestination;


    [Header("Ability3")]


    [Header("Couldown")]
    private bool OnCouldown = false;
    private float CurrentCouldown = 0f;
    private Vector3 OriginalPosition;
    private bool IsTeleported;

    #endregion Settings

    #region ButtonAbility1-2-3

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

        if (Ability2 != null)
        {
            Ability2.onClick.AddListener(OnClickAbility2);
        }
        else
        {
            Debug.LogError("Ability2 не назначена в инспекторе!");
        }

        if (Ability3 != null)
        {
            Ability3.onClick.AddListener(OnClickAbility3);
        }
        else
        {
            Debug.LogError("Ability3 не назначена в инспекторе!");
        }

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
    }

    void Update()
    {
        if(OnCouldown)
            {
                CurrentCouldown -= Time.deltaTime;
                UpdateCouldownUI();
            }
        if(CurrentCouldown <= 0f)
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
        ChangeColorCoroutine = StartCoroutine(ColorChangerMaterial());
    }

    public void OnClickAbility2()
    {
        if (!OnCouldown)
        {
            StartCouldown();
            Debug.Log("Кнопка и есть КД!");
        }

        if (ChangeTransform != null)
        {
            Debug.Log("Корутина уже запущена!");
            return;
        }

        Debug.Log("Запускаем корутину");
        ChangeTransform = StartCoroutine(TeleportAbility());
    }

    public void OnClickAbility3()
    {

    }

    private void UpdateCouldownUI()
    {
        if (IconAbility1 != null && TextCouldown1 != null)
        {
            if (IconAbility1 != null && IconAbility1.type == Image.Type.Filled)
            {
                IconAbility1.fillAmount = CurrentCouldown / TimerAbility;
            }
            TextCouldown1.text = Mathf.CeilToInt(CurrentCouldown).ToString();
        }

        if (IconAbility2 != null && TextCouldown2 != null)
        {
            if (IconAbility2 != null && IconAbility2.type == Image.Type.Filled)
            {
                IconAbility2.fillAmount = CurrentCouldown / TimerAbility;
            }
            TextCouldown2.text = Mathf.CeilToInt(CurrentCouldown).ToString();
        }

        if (IconAbility3 != null && TextCouldown3 != null)
        {
            if (IconAbility3 != null && IconAbility3.type == Image.Type.Filled)
            {
                IconAbility3.fillAmount = CurrentCouldown / TimerAbility;
            }
            TextCouldown3.text = Mathf.CeilToInt(CurrentCouldown).ToString();
        }
    }

    private void EndCouldown()
    {
        OnCouldown = false;
        CurrentCouldown = 0f;

        if(Ability1 != null)
        {
            Ability1.interactable = true;
        }

        if (IconAbility1 != null)
            IconAbility1.gameObject.SetActive(false);

        if (TextCouldown1 != null)
            TextCouldown1.gameObject.SetActive(false);

        Debug.Log("Перезарядка завершена!");


        OnCouldown = false;
        CurrentCouldown = 0f;

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
        CurrentCouldown = 0f;

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

    private void StartCouldown()
    {
        OnCouldown = true;
        CurrentCouldown = TimerAbility;

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
            TextCouldown1.text = TimerAbility.ToString();
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
            TextCouldown2.text = TimerAbility.ToString();
        }

        Debug.Log("Начата перезарядка: " + TimerAbility + " секунд");

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
            TextCouldown3.text = TimerAbility.ToString();
        }

        Debug.Log("Начата перезарядка: " + TimerAbility + " секунд");
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

    private IEnumerator TeleportAbility()
    {
        Debug.Log("Начата телепортация");

        if(Player != null)
        {
            if(!IsTeleported)
            {
                OriginalPosition = Player.transform.position;
            }

            TeleportPlayer();

            yield return new WaitForSeconds(0.5f);

            IsTeleported = true;
            Debug.Log("Игрок телепортирован");
        }
        else
        {
            Debug.Log("Player не назначен!");
        }

        ChangeTransform = null;
        Debug.Log("Корутина завершена");
    }

    private void TeleportPlayer()
    {
        if(Player != null)
        {
            if(useCustomDestination && TeleportDestination != null)
            {
                Player.transform.position = TeleportDestination.position;
                Debug.Log("Телепортация в кастомную точку: " + TeleportDestination.position);
            }
            else
            {
                Vector3 teleportPosition = OriginalPosition + teleportOffset;
                Player.transform.position = teleportPosition;
                Debug.Log("Телепортация со смещением: " + teleportPosition);
            }
        }
    }   

    private void ReturnToOriginalPose()
    {
        if(Player != null)
        {
            Player.transform.position = OriginalPosition;
            IsTeleported = false;
            Debug.Log("Игрок возвращен в исходную позицию: " + OriginalPosition);
        }
    }

    public void ReturnOriginalPosition()
    {
        if(IsTeleported)
        {
            ReturnToOriginalPose();
        }
    }

    public Vector3 GetOriginalPosition()
    {
        return OriginalPosition;
    }    

    public bool IsAbilityReady()
    {
        return !OnCouldown;
    }

    public float GetRemainingCouldown()
    {
        return Math.Max(0f, CurrentCouldown);
    }

    #endregion ButtonAbility1-2-3
}