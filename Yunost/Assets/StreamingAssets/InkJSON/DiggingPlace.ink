INCLUDE globals.ink
EXTERNAL pickupItem(item)
EXTERNAL itemIsExist(item)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)
EXTERNAL changeTime(h, m)
EXTERNAL startMiniGameDigging()
-> NameQuest
INCLUDE Quests\Act1\TreasureHunt\DiggingPlace_Act1_TreasureHunt.ink

== NameQuest
{ 
- isTaskInProgress("treasure_hunt", 0): -> Act1_TreasureHunt
} 
-> END