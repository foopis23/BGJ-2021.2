using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuController : IRouterController
{
    public Slider audioSlider;
    public Slider sensitivitySlider;

	public void OnBack()
	{
		MenuRouter.Instance.GoTo("main");
	}

	public void OnAudioChange(float value)
	{
        PlayerPrefs.SetFloat("volume", value);
		AudioListener.volume = value;
	}

	public void OnSensitivityChange(float value)
	{
		PlayerPrefs.SetFloat("sensitivity", value);
	}

	public override void OnRouteExit()
	{

	}

	public override void OnRouteEnter()
	{
        audioSlider.value = PlayerPrefs.GetFloat("volume", 1f);
        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity", 50f);
	}
}
