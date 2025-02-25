namespace MiniGames
{
    public enum TypesMiniGames
    {
        /// <summary> �� ��������� ������-���� ������� � ��������� </summary>
        HoldingObjectInRange,
        /// <summary> �� ����������� �� ������-���� ���� �� ������ ����� </summary>
        AdvancePathEachStage,
        /// <summary> �� ������� ������� �����-���� ������� � ������� ������������� ������� </summary>
        QuickPressKeyCertainTime,
        /// <summary> ���� ����� � ������ �� ������� </summary>
        GameWolfConsole,
        /// <summary> �� ������� ��������� ������� �����-���� ������� � ������������ ������/�������� </summary>
        QuickTempPressKeyCertainRange,
        /// <summary> �� ���������� ��������� � ���������� ������� </summary>
        ConnectElements,
        /// <summary> �� ���������� �������� �����, ������� ����� ��������� ����������� </summary>
        ReachEndPointWithObstacles,
        /// <summary> �� ����� ����� </summary>
        BreakingLock,
    }

    public enum TypeDifficultMiniGames
    {
        Easy,
        Medium,
        Hard,
    }
    public enum TypeResultMiniGames
    {
        Failed,
        �ompleted,
    }
}
