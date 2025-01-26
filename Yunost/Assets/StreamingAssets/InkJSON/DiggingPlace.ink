INCLUDE globals.ink
EXTERNAL pickupItem(item)
EXTERNAL itemIsExist(item)
EXTERNAL setDoneTask(idTask)
EXTERNAL setDoneSubTask(idTask, idSubTask)
EXTERNAL changeTime(h, m)
EXTERNAL startMiniGameDigging()
-> NameQuest
INCLUDE Quests\Act1\TeamGame\DiggingPlace_Act1_TeamGame.ink

== NameQuest
{ 
- CurrentQuest == "team_game": -> Act1_TeamGame
} 
-> END