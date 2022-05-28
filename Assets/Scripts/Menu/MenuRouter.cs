using System;
using UnityEngine;

public class MenuRouter : MonoBehaviour
{
	public static MenuRouter Instance;
	public Route[] routes;
	private Route currentRoute;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		currentRoute = routes[0];
		currentRoute.value.Enter();
	}

	public bool GoTo(string routeName, Route parent = null)
	{
		var splitURI = routeName.Split('/');

		Route route;

		if (parent != null)
		{
			route = Array.Find(parent.children, r => r.name == splitURI[0]);
		}
		else
		{
			route = Array.Find(routes, r => r.name == splitURI[0]);
		}

		if (route == null)
		{
			Debug.LogError("Route not found: " + routeName);
			return false;
		}

		if (splitURI.Length == 1)
		{
			if (currentRoute.value != null)
			{
				currentRoute.value.Exit();
			}

			currentRoute = route;
			currentRoute.value.Enter();
			return true;
		}
		else
		{
			return GoTo(string.Join("/", splitURI, 1, splitURI.Length - 1), route);
		}
	}
}
