INCLUDE globals.ink
EXTERNAL itemIsExist(item)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)
EXTERNAL tpSofia()
INCLUDE Quests\Act1\HelpForFriend\Oleg_Act1_HelpForFriend.ink
INCLUDE Quests\Act1\SweetHome\Oleg_Act1_SweetHome.ink
INCLUDE Quests\Act1\TeamGame\Oleg_Act1_TeamGame.ink

-> NameQuest

== NameQuest
{ 
- isTaskInProgress("help_for_friend", 0): -> Act1_HelpForFriend
- isTaskInProgress("sweet_home", 0): -> Act1_SweetHome
- isTaskInProgress("team_game", 0) && isSubTaskInProgress("team_game","talk_counselors"): -> Act1_TeamGame
}
-> END


