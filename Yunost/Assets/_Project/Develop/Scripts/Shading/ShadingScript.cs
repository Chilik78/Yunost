using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public enum ShadingMode
{
    Both,
    Forward,
    Backward,
}

public class ShadingScript : MonoBehaviour
{
    private Image _fadeImage;

    void Start()
    {
        _fadeImage = GetComponent<Image>();
    }

    public void StartShading(int milliseconds, ShadingMode mode = ShadingMode.Both)
    {
        if (milliseconds < 0)
        {
            Debug.LogError("Неверное время затемнения в FadeImage");
            return;
        }

        switch(mode)
        { 
            case ShadingMode.Both: StartCoroutine(DoShadingInBothMode(milliseconds)); break;
            case ShadingMode.Forward: StartCoroutine(DoShadingInForwardMode(milliseconds)); break;
            case ShadingMode.Backward: StartCoroutine(DoShadingInBackwardMode(milliseconds)); break;
        }
    }

    private IEnumerator DoShadingInBothMode(int milliseconds)
    {
        int halfTime = milliseconds / 2;
        int timeShading = halfTime - (halfTime / 4);
        int timeBlackScreen = (halfTime / 4) * 2;

        yield return DoShadingForward(timeShading);
        yield return ShowBlackScreen(timeBlackScreen);
        yield return DoShadingBackward(timeShading);

        StopAllCoroutines();
    }

    private IEnumerator DoShadingInForwardMode(int milliseconds)
    {
        yield return DoShadingForward(milliseconds);
        StopAllCoroutines();
    }

    private IEnumerator DoShadingInBackwardMode(int milliseconds)
    {
        yield return DoShadingBackward(milliseconds);
        StopAllCoroutines();
    }

    private IEnumerator DoShadingForward(int milliseconds)
    {
        float stepAlpha =  (1f / (float) milliseconds);
        yield return ChangeAlpha(ShadingMode.Forward, stepAlpha);
    }

    private IEnumerator ShowBlackScreen(int milliseconds)
    {
        for (int i = 0; i < milliseconds; i++)
        {
            yield return null;
        }
    }

    private IEnumerator DoShadingBackward(int milliseconds)
    {
        float stepAlpha = (1f / (float)milliseconds);
        yield return ChangeAlpha(ShadingMode.Backward, stepAlpha);
    }

    private IEnumerator ChangeAlpha(ShadingMode mode, float stepAlpha)
    {
        Color color = _fadeImage.color;
        int direction = (mode == ShadingMode.Forward ? 1 : -1);

        while (isNotExit(mode, color.a))
        {
            color.a += stepAlpha * direction;
            _fadeImage.color = color;
            Debug.Log("Меняем альфу");
            yield return new WaitForSeconds(0.001f);
        }
    }

    private bool isNotExit(ShadingMode mode, float alpha)
    {
        return mode == ShadingMode.Forward ? alpha <= 1 : alpha >= 0;   
    }
}
