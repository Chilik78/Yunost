INCLUDE globals.ink
EXTERNAL pickupItem(item)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)
-> NameQuest
INCLUDE Quests\Act1\SweetHome\HomeKey_Act1_SweetHome.ink


== NameQuest
{ 
- isTaskInProgress("sweet_home", 0) or isTaskInProgress("help_for_friend", 0): -> Act1_SweetHome
} 
-> END