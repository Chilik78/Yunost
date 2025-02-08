INCLUDE globals.ink
EXTERNAL pickupItem(item)
EXTERNAL itemIsExist(item)
EXTERNAL setDoneTask(idTask)
EXTERNAL setDoneSubTask(idTask, idSubTask)
-> NameQuest
INCLUDE Quests\Act1\HelpForFriend\Screwdriver_Act1_HelpForFriend.ink

== NameQuest
{ 
- CurrentQuest == "help_for_friend": -> Act1_HelpForFriend
}
-> Repeat

== Repeat
Отвёртка: Обыкновенная плоская отвёртка с затупленным концом. Незаменимый ручной инструмент для столярных работ. Обычно применяется для завинчивания и отвинчивания крепёжных изделий. Сегодня ей не повезло, ибо использовать её будут не по назначению.
+ [Закончить осмотр] -> END