INCLUDE globals.ink
EXTERNAL pickupItem(item)
EXTERNAL itemIsExist(item)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)
-> NameQuest

INCLUDE Quests\Act1\HelpForFriend\Picklock_Act1_HelpForFriend.ink

== NameQuest
{ 
- isTaskInProgress("help_for_friend", 0):-> Act1_HelpForFriend
}
-> Repeat

== Repeat
Отмычка: Тонкое изогнутое металлическое изделие, которое окажись в неправильных руках может принести беду. Но сегодня ей повезло поучаствовать в специальной операции по спасению заклинившего дверного замка.
+ [Закончить осмотр] -> END