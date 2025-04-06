INCLUDE globals.ink
EXTERNAL hitHealth(value)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL tp(objName, id)
INCLUDE Quests\Act1\Fishing\MainGaitForest_Act1_Fishing.ink


-> NameQuest

== NameQuest
{
- isTaskInProgress("fishing", 0): -> Act1_Fishing
}
-> END
