namespace SaveSystem.Interfaces
{
	public interface ISaveService
	{
		void Save(SaveData saveData);
		void Load();
	}
}