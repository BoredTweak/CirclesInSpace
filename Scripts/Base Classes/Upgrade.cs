using UnityEngine;
using System.Collections;

public class Upgrade 
{
	public int Level {get;set;}
	private readonly int maxLevel;
	private readonly string Name;
	private readonly int indexValue;
	private readonly float multiplier;

	private Upgrade(string name, int indexValue, int maxLevel, float multiplier)
	{
		this.Name = name;
		this.indexValue = indexValue;
		this.maxLevel = maxLevel;
		this.multiplier = multiplier;
	}

	public override string ToString ()
	{
		return Name;
	}

	public int MaxLevel()
	{
		return maxLevel;
	}

	public int AsteroidUpgradeCost()
	{
		return (int)(99 + (10 * Level) + Mathf.Pow(2, Level));
	}

	public float MultiplyingValue()
	{
		return (this.Level * this.multiplier);
	}

	//List here
	public static readonly Upgrade JetpackForce = new Upgrade("Jetpack Force", 0, 10, 0.05f);
	public static readonly Upgrade JetpackFuel = new Upgrade ("Jetpack Fuel", 1, 20, 0.1f);
	public static readonly Upgrade MaxSpin = new Upgrade ("Max Spin", 2, 90, 0.01f);
	public static readonly Upgrade SpinCorrection = new Upgrade ("Spin Correction", 3, 20, 0.1f);


}
