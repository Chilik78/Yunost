using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeBackground : MonoBehaviour
{
    private Image _background;
    private Color _color;

    [SerializeField]
    private float _fadeSpeed = 1f;
    void Start()
    {
        _background = GameObject.Find("PreloaderBackground").GetComponent<Image>();
        _color = _background.color;
    }

    public IEnumerator StartFading()
    {
        while (_color.a < 1f)
        {
            _color.a += _fadeSpeed * Time.deltaTime;
            _background.color = _color;
            yield return null;
        }
    }

    public void ClearFading()
    {
        _color.a += 0f;
        _background.color = _color;
    }
}
