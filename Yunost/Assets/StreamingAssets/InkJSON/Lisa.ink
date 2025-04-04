INCLUDE globals.ink
EXTERNAL itemIsExist(item)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)
EXTERNAL applyPlacement(act, name)
-> NameQuest
INCLUDE Quests\Act1\Meeting\Lisa_Act1_Meeting.ink

== NameQuest
{ 
- isTaskInProgress("meeting", 0) or isTaskInProgress("treasure hunt", 0) or isSubTaskInProgress("fishing", 0): -> Act1_Meeting
} 
-> END