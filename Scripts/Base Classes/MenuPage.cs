using UnityEngine;
using System.Collections;

public class MenuPage 
{
	private string name;
	public int pageIndex{get;set;}

	public string Name
	{
		get{ return name;}
		set { name = value;}
	}
}

public enum PageName
{
	Main = 0,
	Controls = 1,
	Credits = 2,
	Store = 3
}