using System;
using UnityEngine;

[Serializable]
public class Route
{
	public string name;
	public IRouterController value;
	public Route[] children;
}
