INCLUDE globals.ink
EXTERNAL pickupItem(item)
EXTERNAL itemIsExist(item)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL changeTime(h, m)
-> NameQuest
INCLUDE Quests\Act1\SweetHome\BedPlayer_Act1_SweetHome.ink
INCLUDE Quests\Act1\Fishing\BedPlayer_Act1_TreasureHunt_Fishing.ink

== NameQuest
{ 
- isTaskInProgress("sweet_home", 0): -> Act1_SweetHome
- isTaskInProgress("meeting", 0) or isTaskInProgress("fishing", 0) or isTaskInProgress("treasure_hunt", 0): -> Act1_TreasureHunt_Fishing
} 
-> END