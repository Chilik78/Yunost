# Юность

Компьютерная игра в жанре квест, разработанная студентами 4-го курса ВолгГТУ.

## О проекте

**Юность** — приключенческая игра от третьего лица, в которой игрок управляет Саньком, только что прибывшим в летний лагерь. Разворачивающийся сюжет погружает в жизнь лагеря через квесты, диалоги с персонажами и мини-игры.

### Технологии

| | |
|---|---|
| Движок | Unity 6 (6000.0.26f1) |
| Язык | C# |
| Нарратив | Ink (скрипты компилируются в JSON) |
| Рендеринг | HDRP 17.0.3 |
| UI | TextMeshPro + Unity UI |
| Сериализация | Newtonsoft.Json 13.0.3 |

## Геймплей

Игрок от третьего лица перемещается по лагерю, общается с персонажами и выполняет задания. Диалоговая система позволяет выбирать реплики, которые влияют на прохождение квестов.

### Квесты (Акт 1)

| Квест | Описание |
|---|---|
| **Длинная дорога** | Первое знакомство с лагерем — забрать чемодан, найти свой домик |
| **Сладкий дом** | Обустроить место в домике, получить ключ |
| **Помощь другу** | Помочь Олегу разобраться с проблемой (взлом замка) |
| **Рыбалка** | Поймать рыбу вместе с Олегом |
| **Охота за сокровищами** | Найти клад по подсказкам из газеты |

### Персонажи

- **Санёк** — главный герой
- **Макар** — старожил лагеря, проводник по территории
- **Олег** — сосед по домику, нуждается в помощи
- **Лиса (Lisa)** — персонаж из квеста знакомства
- **София** — связана с квестом охоты за сокровищами
- **Директор** — начальник лагеря

### Мини-игры

- **Взлом замка** — подобрать отмычку в нужный момент
- **Рыбалка** — удерживать поплавок в заданном диапазоне
- **Лабиринт** — пройти лабиринт из газетной мини-игры
- **Реакция** — вовремя нажать клавишу в нужный момент
- **Препятствия** — добраться до точки финиша, обходя препятствия

## Структура проекта

```
Yunost/Assets/_Project/
├── Scenes/               — Unity-сцены (Main, Menu, MainCamp, CampStation, HubHome, ...)
├── Develop/
│   ├── Core/             — Ядро (ServiceLocator, SaveLoad, DialogSystem, MiniGames, Player)
│   └── Scripts/          — Gameplay-скрипты (Dialog, Inventory, HUD, World, MiniGames, ...)
├── Resources/            — Конфигурации (InitTasks, InitLevelPlacements, ...)
└── Prefabs/              — Префабы игровых объектов

Yunost/Assets/StreamingAssets/InkJSON/
├── Quests/Act1/          — Диалоговые скрипты квестов (Fishing, HelpForFriend, LongRoad, ...)
├── *.ink                 — Общие диалоги NPC и предметов
└── globals.ink           — Глобальные переменные Ink
```

Сборки и тесты запускаются через Unity Editor — внешних build-скриптов нет.

## Архитектура

### Initialization Flow

1. `GlobalInitScript.cs` — `[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]` создаёт `SaveLoadSystem` до загрузки сцены.
2. `InitSystem.cs` — основной MonoBehaviour-бутстрап: регистрирует все сервисы в `ServiceLocator`, либо загружает сохранение, либо инициализирует из `InitConfig` (конфигурация в Editor).

### Service Locator

`Core/Global/ServiceLocator.cs` — центральный DI-контейнер. Все основные системы регистрируются при старте и извлекаются глобально. Добавление новой системы: зарегистрировать в `InitSystem.cs` и получить через `ServiceLocator.Get<T>()`.

### Система сохранения и загрузки игрового процесса

`Core/ProgressModul/SaveLoadSystem/SaveLoadSystem.cs` — оркестрирует сохранение через **Strategy pattern** (`ISaveLoadStrategy`). Каждый компонент (PlayerStats, TaskObserver, NPC, TimeControl и др.) имеет свою стратегию в `SaveLoadObjects/`.

### Диалоговая система (Ink)

- Нарративные скрипты — JSON-файлы в `Assets/StreamingAssets/InkJSON/` (100+ файлов).
- `Core/DialogSystem/DialogVariables.cs` — хранит и синхронизирует глобальные переменные Ink.
- `Scripts/Dialog/DialogManager.cs` — управляет UI-представлением и обработкой выборов игрока.
- Структура диалогов: `InkJSON/Quests/Act1/` (квесты), `InkJSON/NPC/` (персонажи), `InkJSON/Items/` (предметы).

### Мини-игры

Базовый класс: `Core/MiniGames/MiniGame.cs` с lifecycle-колбэками.  
Менеджер запуска: `Scripts/MiniGames/MiniGamesManager.cs`.  
Реализации в `Core/MiniGames/`:
- `BreakingLock` — взлом замка
- `HoldingObjectInRange` — рыбалка
- `Maze` — лабиринт (газетная мини-игра)
- `QuickTempPressKeyInCertainRange` — реакция
- `ReachEndPointWithObstacles` — преодоление препятствий

Каждая мини-игра вызывает событие с результатом, который подхватывает `TaskObserver`.

### Система квестов

`Core/ProgressModul/TaskObserver.cs` — управление квестами и задачами; определения задач хранятся в JSON (Resources). Результаты мини-игр прокидываются через события в `TaskObserver`.

### Краткий обзор ключевых систем

| Система | Путь |
|---|---|
| Bootstrap | `Scripts/InitSystem/InitSystem.cs` |
| Service Locator | `Core/Global/ServiceLocator.cs` |
| Save/Load | `Core/ProgressModul/SaveLoadSystem/SaveLoadSystem.cs` |
| Квесты | `Core/ProgressModul/TaskObserver.cs` |
| Диалоги (Ink) | `Core/DialogSystem/DialogVariables.cs` |
| Диалоги (UI) | `Scripts/Dialog/DialogManager.cs` |
| Мини-игры (запуск) | `Scripts/MiniGames/MiniGamesManager.cs` |
| Инвентарь | `Scripts/Inventory/InventoryManager.cs` |
| Мир/Сцены | `Scripts/World/LevelManager.cs` |

## Запуск

- Основная точка входа: `Assets/_Project/Scenes/Main.unity`
- Меню: `Assets/_Project/Scenes/Menu.unity`
- Прочие сцены: `DemonstrationScene`, `MainCamp`, `CampStation`, `HubHome`

1. Открыть проект в **Unity Editor** (версия 6000.0.26f1)
2. Открыть папку `Yunost/` как проект в Unity Hub
3. Загрузить сцену `Assets/_Project/Scenes/Main.unity`
4. Нажать **Play** в Unity Editor

## Авторы

Студенты 4-го курса Волгоградского государственного технического университета (ВолгГТУ).
