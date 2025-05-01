using System.IO;
using SaveSystem.Interfaces;
using UnityEngine;

namespace SaveSystem
{
	// ReSharper disable once ClassNeverInstantiated.Global
	public sealed class JsonSaveService : ISaveService, ISaveDataApplier
	{
		private readonly string _savePath = Path.Combine(Application.persistentDataPath, "quickSave.json");

		private static SaveData? _saveDataToBeApplied;

		public void Save(SaveData saveData)
		{
			var json = JsonUtility.ToJson(saveData);
			File.WriteAllText(_savePath, json);
		}

		public void Load()
		{
			if (!SaveExists())
			{
				return;
			}
			
			var json = File.ReadAllText(_savePath);
			var saveData = JsonUtility.FromJson<SaveData>(json);
			_saveDataToBeApplied = saveData;
		}
		
		public SaveData? GetSaveDataTobeApplied() => _saveDataToBeApplied;

		private bool SaveExists() => File.Exists(_savePath);
	}
}