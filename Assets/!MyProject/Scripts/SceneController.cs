using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    public Image FadeImageScreen;
    public float FadeDuration = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (FadeImageScreen == null)
            {
                CreateFadeCanvas();
            }

            InitializeFadeImage();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void CreateFadeCanvas()
    {
        GameObject canvasGO = new GameObject("FadeCanvas");
        canvasGO.transform.SetParent(transform);

        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 9999;

        CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        GameObject imageGO = new GameObject("FadeImage");
        imageGO.transform.SetParent(canvasGO.transform);

        FadeImageScreen = imageGO.AddComponent<Image>();
        FadeImageScreen.color = Color.black;
        FadeImageScreen.rectTransform.anchorMin = Vector2.zero;
        FadeImageScreen.rectTransform.anchorMax = Vector2.one;
        FadeImageScreen.rectTransform.offsetMin = Vector2.zero;
        FadeImageScreen.rectTransform.offsetMax = Vector2.zero;
    }

    private void InitializeFadeImage()
    {
        if (FadeImageScreen != null)
        {
            Color color = FadeImageScreen.color;
            color.a = 0f;
            FadeImageScreen.color = color;
            FadeImageScreen.gameObject.SetActive(true);
        }
    }

    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeLayout(sceneName));
    }

    private IEnumerator FadeLayout(string sceneName)
    {
        yield return StartCoroutine(FadeCoroutine(0f, 1f));

        SceneManager.LoadScene(sceneName);

        yield return new WaitForEndOfFrame();

        yield return StartCoroutine(FadeCoroutine(1f, 0f));
    }

    private IEnumerator FadeCoroutine(float startAlpha, float targetAlpha)
    {
        if (FadeImageScreen == null)
        {
            yield break;
        }

        float elapsedTime = 0f;
        Color color = FadeImageScreen.color;

        while (elapsedTime < FadeDuration)
        {
            if (FadeImageScreen == null)
            {
                yield break;
            }

            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / FadeDuration);
            color.a = alpha;
            FadeImageScreen.color = color;
            yield return null;
        }

        if (FadeImageScreen != null)
        {
            color.a = targetAlpha;
            FadeImageScreen.color = color;
        }
    }
}