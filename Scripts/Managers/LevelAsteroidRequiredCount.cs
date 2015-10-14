using UnityEngine;
using System.Collections;

public static class LevelAsteroidRequiredCount 
{
	public static void CalculateRequiredAsteroidCount(this Level level)
	{
		//flat (5) + 0.5% of total spawned * level.currentNumber?

		float percentage = 0.005f; 
			//((float)level.CurrentNumber) / ((float)level.CurrentNumber + ((float)level.AsteroidCount/10));

			//y = (50+x)*(x/(x+5))
		level.RequiredAsteroidCount = (int)((float)(5f + (level.AsteroidCount * level.CurrentNumber) * percentage));

	}
}
