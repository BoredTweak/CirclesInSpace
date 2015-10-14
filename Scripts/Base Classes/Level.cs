using UnityEngine;
using System.Collections;

public class Level 
{
	public int AsteroidCount{ get; set; }
	public int CurrentNumber{ get; set; }
	public int RequiredAsteroidCount;
	public int Destroyed{get;set;}
	public float MaxTimer{get;set;}
	public float RemainingTime{get;set;}
}
