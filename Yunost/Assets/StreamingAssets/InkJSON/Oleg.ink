INCLUDE globals.ink
EXTERNAL itemIsExist(item)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)
EXTERNAL applyPlacement(act, name)
INCLUDE Quests\Act1\HelpForFriend\Oleg_Act1_HelpForFriend.ink
INCLUDE Quests\Act1\SweetHome\Oleg_Act1_SweetHome.ink
INCLUDE Quests\Act1\Meeting\Oleg_Act1_Meeting.ink
INCLUDE Quests\Act1\Fishing\Oleg_Act1_Fishing.ink

-> NameQuest

== NameQuest
{ 
- isTaskInProgress("help_for_friend", 0): -> Act1_HelpForFriend
- isTaskInProgress("sweet_home", 0): -> Act1_SweetHome
- isTaskInProgress("meeting", 0) or isTaskInProgress("treasure_hunt", 0): -> Act1_Meeting
- isTaskInProgress("fishing", 0): -> Act1_Fishing
}
-> END


