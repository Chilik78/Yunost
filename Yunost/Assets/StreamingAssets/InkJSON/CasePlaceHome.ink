INCLUDE globals.ink
EXTERNAL pickupItem(item)
EXTERNAL itemIsExist(item)
EXTERNAL setDoneTask(idTask)
EXTERNAL setDoneSubTask(idTask, idSubTask)
EXTERNAL putItem()
-> NameQuest
INCLUDE Quests\Act1\SweetHome\CasePlaceHome_Act1_SweetHome.ink
INCLUDE Quests\Act1\TeamGame\CasePlaceHome_Act1_TeamGame.ink

== NameQuest
{ 
- CurrentQuest == "sweet_home": -> Act1_SweetHome
- CurrentQuest == "team_game": -> Act1_TeamGame
} 
-> END