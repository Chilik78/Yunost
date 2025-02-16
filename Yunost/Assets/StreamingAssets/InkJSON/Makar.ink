INCLUDE globals.ink
EXTERNAL itemIsExist(item)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, supTaskId)
EXTERNAL setStateTask(idTask, status)
EXTERNAL setDoneSubTask(idTask, idSubTask)
EXTERNAL tpSofia()
EXTERNAL getTaskState(idTask)
-> NameQuest
INCLUDE Quests\Act1\LongRoad\Makar_Act1_LongRoad.ink
INCLUDE Quests\Act1\TeamGame\Makar_Act1_TeamGame.ink


== NameQuest
{ 
- isTaskInProgress("long_road", 0): -> Act1_LongRoad
- isTaskInProgress("team_game", 0): -> Act1_TeamGame
} 
-> END
