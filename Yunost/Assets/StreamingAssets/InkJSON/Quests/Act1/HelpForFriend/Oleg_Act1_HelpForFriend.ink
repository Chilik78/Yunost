INCLUDE globals.ink

//EXTERNAL startMiniGame()
//EXTERNAL itemInInventory(item)
EXTERNAL setDoneTask(idTask)
EXTERNAL setDoneSubTask(idTask, idSubTask)

== Act1_HelpForFriend
Олег: О, ты всё-таки смог меня найти! Дело в том, что я не могу попасть в свой дом. Дверь заклинило. Поможешь её открыть?
+ [Да, конечно. Я же "профессиональный взломщик".] -> Act1_HelpForFriend_Professional
+ [Не уверен, что смогу взломать дверь в одиночку.] -> Act1_HelpForFriend_Beginner


== Act1_HelpForFriend_Professional
Вы: Да, конечно. Я же "профессиональный взломщик".
Олег: Тогда дерзай, буду тебе благодарен. Если понадобится моя помощь, то подходи - обсудим.
+ [Пойду осмотрю дверь.] 
~setDoneSubTask("help_for_friend", "talk_oleg")
-> END

== Act1_HelpForFriend_Beginner
Вы: Не уверен, что смогу взломать дверь в одиночку.
Олег: Не парься, две пары рук лучше, чем одна. Для того, чтобы приступить к вскрытию замка нам необходимо раздобыть Инструменты для взлома. Нам понадобятся Отвёртка и Отмычки. Поищи их у скамеек, тут недавно работал столяр, может оставил что-то.
+ [Понял, пойду искать] 
~setDoneSubTask("help_for_friend", "talk_oleg")
~setDoneSubTask("help_for_friend", "talk_oleg")
-> END