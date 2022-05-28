using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : IRouterController
{
	public void OnPlay()
	{
		SceneManager.LoadScene(1);
	}

	public void OnSettings()
	{
		MenuRouter.Instance.GoTo("main/settings");
	}

	public void OnExit()
	{
		Application.Quit();
	}

	public override void OnRouteExit() {}

	public override void OnRouteEnter() {}
}
