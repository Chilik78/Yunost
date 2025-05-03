INCLUDE globals.ink
EXTERNAL itemIsExist(item)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, supTaskId)
EXTERNAL setStateTask(idTask, status)
EXTERNAL setDoneSubTask(idTask, idSubTask)
EXTERNAL getTaskState(idTask)
EXTERNAL applyPlacement(act, name)
EXTERNAL startMiniGame(idMiniGame, idDifficulty)
EXTERNAL checkStateMiniGame()
-> NameQuest
INCLUDE Quests\Act1\LongRoad\Makar_Act1_LongRoad.ink
INCLUDE Quests\Act1\LongRoad\Makar_Act1_LongRoad_Test.ink
INCLUDE Quests\Act1\Meeting\Makar_Act1_Meeting.ink

== NameQuest
{ 
- isTaskInProgress("long_road", 0): -> Act1_LongRoad_Test
- isTaskInProgress("meeting", 0) or isTaskInProgress("treasure_hunt", 0): -> Act1_Meeting
- else: 
Видимо, с Макаром сейчас не о чем поговорить.
+ [*Уйти*] -> END
} 
-> END
