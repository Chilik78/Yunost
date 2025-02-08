INCLUDE globals.ink
EXTERNAL itemIsExist(item)
EXTERNAL setDoneTask(idTask)
EXTERNAL setDoneSubTask(idTask, idSubTask)
EXTERNAL tpSofia()
-> NameQuest
INCLUDE Quests\Act1\TeamGame\Lisa_Act1_TeamGame.ink

== NameQuest
{ 
- CurrentQuest == "team_game": -> Act1_TeamGame
} 
-> END