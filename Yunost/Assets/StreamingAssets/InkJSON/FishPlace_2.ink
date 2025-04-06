INCLUDE globals.ink
EXTERNAL pickupItem(item)
EXTERNAL itemIsExist(item)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)
EXTERNAL changeTime(h, m)
EXTERNAL startMiniGame(idMiniGame, idDifficulty)
-> NameQuest
INCLUDE Quests\Act1\Fishing\FishPlace2_Act1_Fishing.ink

VAR useFishPlace2 = false

== NameQuest
{ 
- isSubTaskInProgress("fishing", "fishing_at_beach"): -> Act1_Fishing
}
-> Default

== Default
Место для ловли рыбы: Перспективное место, с которого можно попробовать закинуть удочку для ловли рыбы.
+ [*Уйти*]
-> END

