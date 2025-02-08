namespace ProgressModul
{
    /// <summary>
    /// Interface for an object that needs to be saved.
    /// </summary>
    public interface ISaveLoadObject
    {
        /// <summary>
        /// ИД для определения объекта
        /// </summary>
        public string ComponentSaveId { get; }

        /// <summary>
        /// Возвращает данные для сохранения
        /// </summary>
        /// <returns>Данные для сохранения</returns>
        public SaveLoadData GetSaveLoadData();

        /// <summary>
        /// Восстанавливает данные
        /// </summary>
        /// <param name="loadData">Данные для восстановления</param>
        public void RestoreValues(SaveLoadData loadData);

        /// <summary>
        /// Устанавливает начальные значения
        /// </summary>
        public void SetDefault();
    }
}