using UnityEngine;

public abstract class IRouterController : MonoBehaviour
{
	public void Exit() {
		OnRouteExit();
		gameObject.SetActive(false);
	}

	public void Enter() {
		gameObject.SetActive(true);
		OnRouteEnter();
	}

	public abstract void OnRouteExit();
	public abstract void OnRouteEnter();
}