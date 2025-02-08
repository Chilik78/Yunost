namespace ProgressModul
{
    /// <summary>
    /// Interface for an object that needs to be saved.
    /// </summary>
    public interface ISaveLoadObject
    {
        /// <summary>
        /// �� ��� ����������� �������
        /// </summary>
        public string ComponentSaveId { get; }

        /// <summary>
        /// ���������� ������ ��� ����������
        /// </summary>
        /// <returns>������ ��� ����������</returns>
        public SaveLoadData GetSaveLoadData();

        /// <summary>
        /// ��������������� ������
        /// </summary>
        /// <param name="loadData">������ ��� ��������������</param>
        public void RestoreValues(SaveLoadData loadData);

        /// <summary>
        /// ������������� ��������� ��������
        /// </summary>
        public void SetDefault();
    }
}