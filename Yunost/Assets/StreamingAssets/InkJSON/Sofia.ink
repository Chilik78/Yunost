INCLUDE globals.ink
EXTERNAL itemIsExist(item)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)
EXTERNAL applyPlacement(act, name)
-> NameQuest
INCLUDE Quests\Act1\Meeting\Sofia_Act1_Meeting.ink
INCLUDE Quests\\Act1\TreasureHunt\Sofia_Act1_TreasureHunt.ink

== NameQuest
{ 
- isTaskInProgress("meeting", 0) or isTaskInProgress("fishing", 0): -> Act1_Meeting
- isTaskInProgress("treasure_hunt", 0): -> Act1_TreasureHunt
} 
-> END