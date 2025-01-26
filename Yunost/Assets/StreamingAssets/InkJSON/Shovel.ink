INCLUDE globals.ink
EXTERNAL pickupItem(item)
EXTERNAL itemIsExist(item)
EXTERNAL setDoneTask(idTask)
EXTERNAL setDoneSubTask(idTask, idSubTask)
-> NameQuest
INCLUDE Quests\Act1\TeamGame\Shovel_Act1_TeamGame.ink

== NameQuest
{ 
- CurrentQuest == "team_game": -> Act1_TeamGame
} 
-> END