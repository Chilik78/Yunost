using Global;
using ProgressModul;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PreloaderController : MonoBehaviour
{
    // вместо обычной локальной переменной класса
    // можно использовать slider и менять его значение value
    private SceneControl _sceneControl;
    private Slider _slider;
    private GameObject _preloader;
    private GameObject _background;
    
    private void Start()
    {
        _sceneControl = ServiceLocator.Get<SceneControl>();
        _preloader = GameObject.Find("Preloader");
        _background = GameObject.Find("PreloaderBackground");

        _slider = _preloader.GetComponentInChildren<Slider>();
        _slider.enabled = false;
        _preloader.SetActive(false);

        FadeBackground fader = _background.GetComponent<FadeBackground>();

        _sceneControl.StartLoading += () => {
            _preloader.SetActive(true);
            StartCoroutine(fader.StartFading());
        };
        _sceneControl.StoptLoading += () => {
            _preloader.SetActive(false);
            fader.ClearFading();
        };
        _sceneControl.ProgressLoading += (progress) => _slider.value = progress;
        
        StartCoroutine(test());


    }

    private IEnumerator test()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(_sceneControl.LoadNewSceneAsync(Scenes.MainCamp));
    }
    
}
