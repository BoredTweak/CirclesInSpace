using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UpgradeManager : MonoBehaviour {

	public List<Upgrade> upgrades;

	void Start () 
	{
		upgrades = new List<Upgrade> ();
		InitUpgrades ();
		SetUpgradeLevels ();
	}

	public void SetUpgradeLevels()
	{
		for (int i = 0; i < upgrades.Count; i++) 
		{
			CheckPlayerPrefsForUpgrade (upgrades [i]);	
		}
	}

	void CheckPlayerPrefsForUpgrade(Upgrade upgrade)
	{
		upgrade.Level = PlayerPrefs.GetInt (upgrade.ToString (), 0);
	}

	void InitUpgrades()
	{
		upgrades.Add (Upgrade.JetpackForce);
		upgrades.Add(Upgrade.JetpackFuel);
		upgrades.Add (Upgrade.MaxSpin);
		upgrades.Add (Upgrade.SpinCorrection);
	}
}

