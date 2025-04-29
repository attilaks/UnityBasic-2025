using System.IO;
using UnityEngine;

namespace SaveSystem
{
	public sealed class JsonSaveService : ISaveService
	{
		private readonly string _savePath = Path.Combine(Application.persistentDataPath, "quickSave.json");

		public void Save(SaveData saveData)
		{
			var json = JsonUtility.ToJson(saveData);
			File.WriteAllText(_savePath, json);
		}

		public SaveData Load()
		{
			if (!SaveExists())
			{
				return null;
			}
			
			var json = File.ReadAllText(_savePath);
			var saveData = JsonUtility.FromJson<SaveData>(json);
			return saveData;
		}

		private bool SaveExists() => File.Exists(_savePath);
	}
}