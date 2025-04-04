INCLUDE globals.ink
EXTERNAL pickupItem(item)
EXTERNAL itemIsExist(item)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)
-> NameQuest

INCLUDE Quests\Act1\Fishing\Bait_Act1_Fishing.ink

== NameQuest
{ 
- isTaskInProgress("fishing", 0):-> Act1_Fishing
}
-> END