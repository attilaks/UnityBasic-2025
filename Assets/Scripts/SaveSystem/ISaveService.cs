namespace SaveSystem
{
	public interface ISaveService
	{
		void Save(SaveData saveData);
		SaveData Load();
	}
}