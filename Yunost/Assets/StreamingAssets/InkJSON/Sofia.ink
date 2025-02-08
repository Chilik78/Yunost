INCLUDE globals.ink
EXTERNAL itemIsExist(item)
EXTERNAL setDoneTask(idTask)
EXTERNAL setDoneSubTask(idTask, idSubTask)
EXTERNAL tpSofia()
-> NameQuest
INCLUDE Quests\Act1\TeamGame\Sofia_Act1_TeamGame.ink

== NameQuest
{ 
- CurrentQuest == "team_game" && CurrentSubquest == "talk_counselors": -> Act1_TeamGame
- CurrentQuest == "team_game" && CurrentSubquest != "talk_counselors": -> Act1_TeamGameTalk
} 
-> END