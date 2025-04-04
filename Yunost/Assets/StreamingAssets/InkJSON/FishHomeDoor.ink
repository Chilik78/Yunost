INCLUDE globals.ink
EXTERNAL itemIsExist(item)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)
EXTERNAL tp(objName, id)
EXTERNAL applyPlacement(act, name)
-> NameQuest

INCLUDE Quests\Act1\Fishing\FishHomeDoor_Act1_Fishing.ink


== NameQuest
{ 
- isTaskInProgress("fishing", 0): -> Act1_Fishing
} 
-> END