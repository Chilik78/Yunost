
== Act1_SweetHome
{
- isSubTaskInProgress("sweet_home","open_door") or isSubTaskInProgress("sweet_home","go_to_home"): -> Act1_SweetHomeRepeatTalk
- isSubTaskInProgress("sweet_home","director") or meetDirector == false: -> Act1_SweetHomeHaventKey
- isSubTaskInProgress("sweet_home","director") && itemIsExist("key"): -> Act1_SweetHomeHaveKeyTalkDirector
- repeatTalkOleg == true: -> Act1_SweetHomeRepeatTalk
}

== Act1_SweetHomeHaventKey
Олег: Рекомендую тебе поспешить, пока Анатолий Степанович у себя. Если ты вдруг подзабыл, то он у Администрации.
+ [Помню помню. Уже иду.] -> END

== Act1_SweetHomeHaveKeyTalkDirector
Олег: Поспеши к Анатолию Степановичу. Лучше ему от нас узнать о ситуации с Дверью.
~repeatTalkOleg = true
+ [Уже иду.] -> END

== Act1_SweetHomeRepeatTalk
Олег: *Олег задумчиво стоял и наблюдал за тем, как ветер колышет листву*
+ [*Уйти*] -> END
