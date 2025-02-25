namespace MiniGames
{
    public enum TypesMiniGames
    {
        /// <summary> На удержания какого-либо объекта в диапазоне </summary>
        HoldingObjectInRange,
        /// <summary> На продвижение по какому-либо пути на каждом этапе </summary>
        AdvancePathEachStage,
        /// <summary> На быстрое нажатие какой-либо клавиши в течение определенного времени </summary>
        QuickPressKeyCertainTime,
        /// <summary> Игра волка с яицами на консоле </summary>
        GameWolfConsole,
        /// <summary> На быстрое временное нажатие какой-либо клавиши в определенный момент/диапазон </summary>
        QuickTempPressKeyCertainRange,
        /// <summary> На соединение элементов в правильном порядке </summary>
        ConnectElements,
        /// <summary> На достижение конечной точки, проходя через различные препятствия </summary>
        ReachEndPointWithObstacles,
        /// <summary> На взлом замка </summary>
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
        Сompleted,
    }
}
