INCLUDE globals.ink
EXTERNAL pickupItem(item)
EXTERNAL itemIsExist(item)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)
EXTERNAL startMiniGame(idMiniGame, idDifficulty)
-> NameQuest

INCLUDE Quests\Act1\Fishing\Newspaper_Act1_Fishing_TreasureHunt.ink

== NameQuest
{ 
- isSubTaskInProgress("fishing", "reading_newspaper"):-> Act1_Fishing_TreasureHunt
- isSubTaskInProgress("treasure_hunt", "reading_newspaper"):->Act1_Fishing_TreasureHunt
}
-> Default

== Default
Новостная газета: На главной странице политические новости. На второй странице головоломка.
+ [*Уйти*]
-> END