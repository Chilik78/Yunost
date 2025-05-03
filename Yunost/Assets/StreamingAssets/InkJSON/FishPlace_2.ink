INCLUDE globals.ink
EXTERNAL pickupItem(item)
EXTERNAL itemIsExist(item)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)
EXTERNAL changeTime(h, m)
EXTERNAL startMiniGame(idMiniGame, idDifficulty)
EXTERNAL checkStateMiniGame()
EXTERNAL hitStamina(value)
-> NameQuest
INCLUDE Quests\Act1\Fishing\FishPlace2_Act1_Fishing.ink

== NameQuest
{ 
- isSubTaskInProgress("fishing", "fishing_at_beach") && (useFishPlace2 == false): -> Act1_Fishing
}
-> Default

== Default
Место для ловли рыбы: Перспективное место, с которого можно попробовать закинуть удочку для ловли рыбы.
+ [*Уйти*]
-> END

