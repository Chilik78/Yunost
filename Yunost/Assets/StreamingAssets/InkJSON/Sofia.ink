INCLUDE globals.ink
EXTERNAL itemIsExist(item)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)
EXTERNAL tpSofia()
-> NameQuest
INCLUDE Quests\Act1\TeamGame\Sofia_Act1_TeamGame.ink

== NameQuest
{ 
- isTaskInProgress("team_game", 0) && isSubTaskInProgress("team_game", "talk_counselors"): -> Act1_TeamGame
- isTaskInProgress("team_game", 0) && not isSubTaskInProgress("team_game", "talk_counselors"): -> Act1_TeamGameTalk
} 
-> END