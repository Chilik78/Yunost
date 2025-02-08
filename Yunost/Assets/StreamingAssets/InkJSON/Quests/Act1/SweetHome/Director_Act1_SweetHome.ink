
== Act1_SweetHome
Анатолий Степанович: *Стоит и напевает мелодию из очень уж знакомой советской композиции*
{
- hitDoorTalk == false && hitDoor == true: -> Act1_SweetHomeHitDoor
}
-> Act1_SweetHomeDialog

== Act1_SweetHomeDialog
+ [Анатолий Степанович, когда мы приступим к работе?] -> Act1_SweetHomeWorkTalk
+ {not itemIsExist("key")} [Где я могу взять Ключ от Дома №1?] -> Act1_SweetHomeSearchKey
+ [Закончить разговор] -> END

== Act1_SweetHomeHitDoor
Анатолий Степанович: Здравствуй, Александр. Я наслышен, что вы с Олегом выбили дверь в Доме №2. Я возмущён вашим поступком и тем, что вы не обратились за помощью ко мне!
~hitDoorTalk = true
+ [Если честно, я сам такого от себя не ожидал.] -> Act1_SweetHomeIBlame
+ [Это был Олег. Я лишь наблюдал со стороны. (-Лояльность Олег)] -> Act1_SweetHomeBlameOleg


== Act1_SweetHomeIBlame
Вы: Если честно, я сам такого от себя не ожидал.
Анатолий Степанович: Александр, если вы не отремонтируете Замок, то вас настигнут последствия. Я терпелив, но в меру.
+ [Всё будет сделано, Анатолий Степанович!] 
~setDoneSubTask("sweet_home", "director")
{itemIsExist("key"): 
~setDoneSubTask("sweet_home", "search_key")
}
-> Act1_SweetHomeDialog


== Act1_SweetHomeBlameOleg
Вы: Это был Олег. Я лишь наблюдал со стороны.
Анатолий Степанович: Я надеюсь, Александр, что вы меня не обманываете. Я поговорю с Олегом. В любом случае, требую от вас отремонтировать дверной Замок, иначе вас настигнут последствия.
+ [Я вас понял, Анатолий Степанович]
~setDoneSubTask("sweet_home", "director")
{itemIsExist("key"): 
~setDoneSubTask("sweet_home", "search_key")
}
-> Act1_SweetHomeDialog

== Act1_SweetHomeWorkTalk
Вы: Анатолий Степанович, когда мы приступим к работе?
Анатолий Степанович: Александр, не спешите, сначала отнесите вещи в Дом №1 и отдохните после долгой дороги. К обеду жду вас на собрании вожатых у Администрации.
+ [Так точно! Направляюсь отдыхать.] -> Act1_SweetHomeDialog

== Act1_SweetHomeSearchKey
{hitDoor == false:
~setDoneSubTask("sweet_home", "director")
}
Вы: Где я могу взять Ключ от Дома №1?
Анатолий Степанович: Точно! Я позабыл о Ключе от вашего Дома. Я оставил его у входа в Администрацию. Заберите и ступайте.
+ [Спасибо.] -> Act1_SweetHomeDialog

