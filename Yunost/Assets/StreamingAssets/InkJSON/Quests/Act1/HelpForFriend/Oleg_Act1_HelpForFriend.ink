
== Act1_HelpForFriend
{CurrentSubquest == "talk_oleg": -> Act1_HelpForFriend_TalkOleg}
{CurrentSubquest == "check_door": -> Act1_HelpForFriend_RepeatCheckDoor}
{CurrentSubquest == "search_tools" && repeatTalkSearchTools == false: -> Act1_HelpForFriend_SearchTools}
{CurrentSubquest == "search_tools" && repeatTalkSearchTools == true: -> Act1_HelpForFriend_RepeatSearchTools}
{CurrentSubquest == "open_door" && screwdriverExist == true && picklockExist == true: -> Act1_HelpForFriend_OpenDoor}
{CurrentSubquest == "talk_result": -> Act1_HelpForFriend_TalkResult}
-> END

== Act1_HelpForFriend_TalkOleg
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
Олег: Не парься, две пары рук лучше, чем одна. Для того, чтобы приступить к вскрытию замка нам необходимо раздобыть Инструменты для взлома. Нам понадобятся Отвёртка и Отмычки. Поищи их у скамеек, тут недавно работал столяр, может оставил что-то. Заодно можешь осмотреть дверь.
+ [Понял, пойду искать.] 
~setDoneSubTask("help_for_friend", "talk_oleg")
~setDoneSubTask("help_for_friend", "check_door")
-> END

== Act1_HelpForFriend_RepeatCheckDoor
Олег: Ты уже осмотрел дверь? Если нет, то самое время. Может тогда что-то придёт в голову?
+ [Сейчас осмотрю дверь.]
-> END

== Act1_HelpForFriend_SearchTools
Олег: Ну как? Есть идеи?
+ [Пока не знаю, где найти инструменты для взлома.] -> Act1_HelpForFriend_SearchToolsTalk

== Act1_HelpForFriend_SearchToolsTalk
Вы: Пока не знаю, где найти инструменты для взлома.
Олег: Тебе нужно найти Отвёртку и Отмычки. Поищи их у скамеек, тут недавно работал столяр, может оставил что-то. Объединив их, получишь свои Инструменты для взлома. Элементарно же!
+ [Понял. Пойду поищу.] 
~repeatTalkSearchTools = true
-> END

== Act1_HelpForFriend_RepeatSearchTools
Олег: Не можешь найти Отвёртку и Отмычки? Поищи внимательнее у Скамеек напротив Домиков. Что-то столяр должен был оставить! Как найдёшь, то объедини их и получишь Инструменты для взлома.
+ [Буду искать внимательнее.] -> END

== Act1_HelpForFriend_OpenDoor
Олег: О, я вижу ты раздобыл Инструменты для взлома. Дерзай к Двери вскрывать замок или поищи другой способ открыть Дверь.
+ [Посмотрю, что можно сделать] -> END

== Act1_HelpForFriend_TalkResult
{
- hitDoor == true: Олег: Спасибо тебе за помощь. Но с дверью ты переборщил...как бы нам не прилетело от Анатолия Степановича. Лучше сходи к Администрации и обговори с ним...эту ситуацию...Заодно и Ключ возьмёшь от своего Дома.
- hitDoor == false: Олег: Ты смог открыть дверь! Молодец, Саня. Огромное спасибо, друг! Да и тебе бы пора уже отдохнуть. Ключ от двери твоего Дома у Директора. Сходи к Администрации и забери его. Увидимся позже!
}
+ {itemIsExist("key")} [Ключ уже забрал.] 
-> Act1_HelpForFriend_TalkResultHaveKey
+ {not itemIsExist("key")} [До встречи.]
~setDoneSubTask("help_for_friend", "talk_result")
-> END

== Act1_HelpForFriend_TalkResultHaveKey
Вы: Ключ уже забрал.
{
 - hitDoor == true: Олег: Оперативно сработано! Но всё же поговори с ним о Двери.
 ~setDoneSubTask("help_for_friend", "talk_result")
 - hitDoor == false: Олег: Оперативно сработано! Тогда дерзай отдыхать!
 ~setDoneSubTask("help_for_friend", "talk_result")
 ~setDoneSubTask("sweet_home", "director")
 ~setDoneSubTask("sweet_home", "search_key")
}
+ [Увидимся]
-> END
