using UnityEngine;
using System.Collections;

public class SaveData 
{
	private readonly string name;
	private readonly int indexValue;

	private SaveData(string name, int indexValue)
	{
		this.name = name;
		this.indexValue = indexValue;
	}

	public override string ToString ()
	{
		return name;
	}

	//List here
	public static readonly SaveData BestLevel = new SaveData("Best Level", 0);
	public static readonly SaveData AsteroidCredits = new SaveData ("Asteroid Credits", 1);
	public static readonly SaveData SoundMuted = new SaveData ("Sound Muted", 2);
	public static readonly SaveData MusicMuted = new SaveData ("Music Muted", 3);
	/*public static readonly SaveData JetpackForce = new SaveData (Upgrade.JetpackForce.ToString (), 4);
	public static readonly SaveData JetpackFuel = new SaveData (Upgrade.JetpackFuel.ToString (), 5);
	public static readonly SaveData MaxSpin = new SaveData (Upgrade.MaxSpin.ToString (), 6);
	public static readonly SaveData SpinCorrection = new SaveData (Upgrade.SpinCorrection.ToString (), 7);*/

}