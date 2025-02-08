INCLUDE globals.ink
EXTERNAL pickupItem(item)
EXTERNAL itemIsExist(item)
EXTERNAL setDoneTask(idTask)
EXTERNAL setDoneSubTask(idTask, idSubTask)
-> NameQuest
INCLUDE Quests\Act1\HelpForFriend\Picklock_Act1_HelpForFriend.ink

== NameQuest
{ 
- CurrentQuest == "help_for_friend":-> Act1_HelpForFriend
}
-> Repeat

== Repeat
Отмычка: Тонкое изогнутое металлическое изделие, которое окажись в неправильных руках может принести беду. Но сегодня ей повезло поучаствовать в специальной операции по спасению заклинившего дверного замка.
+ [Закончить осмотр] -> END