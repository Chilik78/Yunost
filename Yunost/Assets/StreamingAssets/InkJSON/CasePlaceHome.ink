INCLUDE globals.ink
EXTERNAL pickupItem(item)
EXTERNAL itemIsExist(item)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)
EXTERNAL putItem()
-> NameQuest
INCLUDE Quests\Act1\SweetHome\CasePlaceHome_Act1_SweetHome.ink
INCLUDE Quests\Act1\TeamGame\CasePlaceHome_Act1_TeamGame.ink

== NameQuest
{ 
- isTaskInProgress("sweet_home", 0): -> Act1_SweetHome
- isTaskInProgress("team_game", 0): -> Act1_TeamGame
} 
-> END