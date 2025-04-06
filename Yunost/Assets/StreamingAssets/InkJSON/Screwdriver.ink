INCLUDE globals.ink
EXTERNAL pickupItem(item)
EXTERNAL itemIsExist(item)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)
-> NameQuest

INCLUDE Quests\Act1\HelpForFriend\Screwdriver_Act1_HelpForFriend.ink

== NameQuest
{ 
- isTaskInProgress("help_for_friend", 0): -> Act1_HelpForFriend
}
-> Repeat

== Repeat
Отвёртка: Обыкновенная плоская отвёртка с затупленным концом. Незаменимый ручной инструмент для столярных работ. Обычно применяется для завинчивания и отвинчивания крепёжных изделий. Сегодня ей не повезло, ибо использовать её будут не по назначению.
+ [Закончить осмотр] -> END