using UnityEngine;
using System.Collections;

public static class Utility
{
	/// <summary>
	/// Vyberie z pola nahodnu hodnotu
	/// </summary>
	public static T GetRandomItem<T>(this T[] array)
	{
		return array[Random.Range(0, array.Length)];
	}

	public static bool GetRandomBool()
	{
		return Random.value < 0.5f;
	}
}
