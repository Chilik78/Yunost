using Global;
using ProgressModul;
using System;
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
    private GameObject _sliderObj;

    private void Start()
    {
        _sceneControl = ServiceLocator.Get<SceneControl>();
        _preloader = GameObject.Find("Preloader");
        _background = GameObject.Find("PreloaderBackground");
        _sliderObj = GameObject.Find("Slider");

        _slider = _preloader.GetComponentInChildren<Slider>();
        _slider.enabled = false;
        _preloader.SetActive(false);

        FadeBackground fader = _background.GetComponent<FadeBackground>();

        _sceneControl.StartLoading += () => {
            _preloader.SetActive(true);
        };
        _sceneControl.StoptLoading += () => {
            _preloader.SetActive(false);

            //fader.ClearFading();
        };
        _sceneControl.ProgressLoading += (progress) => _slider.value = progress;
        
        StartCoroutine(test());


    }

    private IEnumerator wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    private IEnumerator test()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(_sceneControl.LoadNewSceneAsync(Scenes.MainCamp));
    }
    
}
