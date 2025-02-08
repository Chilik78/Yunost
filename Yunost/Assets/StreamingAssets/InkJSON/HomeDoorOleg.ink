INCLUDE globals.ink
EXTERNAL hitHealth(value)
EXTERNAL startMiniGame()
EXTERNAL itemIsExist(item)
EXTERNAL setDoneSubTask(idTask, idSubTask)
-> NameQuest
INCLUDE Quests\Act1\HelpForFriend\HomeDoorOleg_Act1_HelpForFriend.ink

== NameQuest
{ 
- CurrentQuest == "help_for_friend": -> Act1_HelpForFriend
}
-> Repeat

== Repeat
Дверь: Есть ощущение, что с этой дверью больше делать нечего.
+ [Закончить осмотр] -> END