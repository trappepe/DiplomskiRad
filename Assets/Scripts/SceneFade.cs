using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    private Image sceneFadeImage;
    private void Awake()
    {
        sceneFadeImage = GetComponent<Image>();
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator FadeInCourutine(float duration)
    {
        gameObject.SetActive(false);
        Color startColor = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 1);
        Color targetColor = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 0);
        yield return FadeCoroutine(startColor, targetColor, duration);
    }
    public IEnumerator FadeOutCoroutine(float duration)
    {
        Color startColor = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 0);
        Color targetColor = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 1); 
        gameObject.SetActive(true);
        yield return FadeCoroutine(startColor, targetColor, duration);
    }
    private IEnumerator FadeCoroutine(Color startColor, Color targetColor, float duration)
    {
        float time = 0;

    while (time < duration)
    {
        float t = time / duration;
        sceneFadeImage.color = Color.Lerp(startColor, targetColor, t);

        time += Time.deltaTime;
        yield return null;
    }
    sceneFadeImage.color = targetColor;
    }
}
