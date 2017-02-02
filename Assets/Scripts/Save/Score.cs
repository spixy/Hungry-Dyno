using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ScoreStorage
{
	[Serializable]
	private class SerializedScore
	{
		public List<string> Players;
		public List<int> Scores;

		public SerializedScore Init()
		{
			Players = new List<string>(1);
			Scores = new  List<int>(1);
			return this;
		}
	}

	private readonly string saveFile;

	private readonly Dictionary<string, int> scoreTable;

	private SerializedScore save;

	public ScoreStorage()
	{
		this.scoreTable = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
		this.saveFile = Path.Combine(Application.persistentDataPath, "score.save");

		this.LoadFromDisk();
	}

	public int GetScore(string name)
	{
		int value = 0;

		if (!string.IsNullOrEmpty(name))
		{
			scoreTable.TryGetValue(name, out value);
		}

		return value;
	}

	public void SetScore(string name, int value, bool saveInstantly = false)
	{
		scoreTable[name] = value;

		if (saveInstantly)
		{
			SaveToDisk();
		}
	}

	/// <summary>
	/// Vrati skore zoradene od najvyssieho
	/// </summary>
	public List<KeyValuePair<string, int>> GetTopScoreTable()
	{
		var kvpList = scoreTable.ToList();
		kvpList.Sort((a, b) => b.Value.CompareTo(a.Value));
		return kvpList;
	}

	/// <summary>
	/// Nacita tabulku z disku
	/// </summary>
	public void LoadFromDisk()
	{
		if (!File.Exists(saveFile))
			return;

		string fileContent = File.ReadAllText(saveFile);
		save = JsonUtility.FromJson<SerializedScore>(fileContent);
		
		if (this.save.Players.Count != this.save.Scores.Count)
		{
			Debug.LogError("Player count not equal to score count");
			return;
		}

		this.scoreTable.Clear();

		for (int i = 0; i < save.Players.Count; i++)
		{
			this.scoreTable.Add(save.Players[i], save.Scores[i]);
		}
	}

	/// <summary>
	/// Ulozi tabulku na disk
	/// </summary>
	public void SaveToDisk()
	{
		if (save == null)
		{
			save = new SerializedScore().Init();
		}

		save.Scores.Clear();
		save.Players.Clear();

		foreach (var kvp in this.scoreTable)
		{
			save.Players.Add(kvp.Key);
			save.Scores.Add(kvp.Value);
		}

		File.WriteAllText(saveFile, JsonUtility.ToJson(this.save));
	}
}
