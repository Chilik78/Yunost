INCLUDE globals.ink
EXTERNAL pickupItem(item)
EXTERNAL itemIsExist(item)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)

-> NameQuest
INCLUDE Quests\Act1\TeamGame\Shovel_Act1_TeamGame.ink

== NameQuest
{ 
- isTaskInProgress("team_game", 0): -> Act1_TeamGame
} 
-> END