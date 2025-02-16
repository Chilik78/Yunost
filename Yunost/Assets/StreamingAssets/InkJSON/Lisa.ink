INCLUDE globals.ink
EXTERNAL itemIsExist(item)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)
EXTERNAL tpSofia()
-> NameQuest
INCLUDE Quests\Act1\TeamGame\Lisa_Act1_TeamGame.ink

== NameQuest
{ 
- isTaskInProgress("team_game", 0): -> Act1_TeamGame
} 
-> END