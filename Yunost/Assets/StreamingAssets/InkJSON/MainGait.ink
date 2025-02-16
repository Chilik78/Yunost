INCLUDE globals.ink
EXTERNAL hitHealth(value)
EXTERNAL changeScene(sceneName)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
INCLUDE Quests\Act1\LongRoad\MainGait_Act1_LongRoad.ink


-> NameQuest

== NameQuest
{ 
- isTaskInProgress("long_road", 0): -> Act1_LongRoad
} 
-> END 