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

    private void Start()
    {
        _sceneControl = ServiceLocator.Get<SceneControl>();
        _preloader = GameObject.Find("Preloader");

        _slider = _preloader.GetComponentInChildren<Slider>();
        _slider.enabled = false;
        _preloader.SetActive(false);

        _sceneControl.StartLoading += () => {
            _preloader.SetActive(true);
        };
        _sceneControl.StoptLoading += () => {
            _preloader.SetActive(false);

            //fader.ClearFading();
        };
        _sceneControl.ProgressLoading += (progress) => _slider.value = progress;

    }
    
}
