INCLUDE globals.ink
EXTERNAL hitHealth(value)
EXTERNAL startMiniGame()
EXTERNAL itemIsExist(item)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)

-> NameQuest

INCLUDE Quests\Act1\HelpForFriend\HomeDoorOleg_Act1_HelpForFriend.ink

== NameQuest
{ 
- isTaskInProgress("help_for_friend", 0): -> Act1_HelpForFriend
}
-> Repeat

== Repeat
Дверь: Есть ощущение, что с этой дверью больше делать нечего.
+ [Закончить осмотр] -> END