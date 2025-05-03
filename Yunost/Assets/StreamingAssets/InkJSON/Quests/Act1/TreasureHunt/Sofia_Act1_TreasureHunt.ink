

== Act1_TreasureHunt
София: *София задумчиво разглядывает территорию поляны*
+ {isSubTaskInProgress("treasure_hunt", "meet_sofia")} [Каков наш план действий?] -> Act1_TreasureHunt_Plan
+ [Как у тебя дела?] -> Act1_TreasureHunt_Discussion
+ {isSubTaskInProgress("treasure_hunt", "search_shovel")}[Я готов приступить к выкапыванию ям] -> Act1_TreasureHunt_Digging
+ {isSubTaskInProgress("treasure_hunt", "result_talk_sofia")} [Я выкопал все ямы.] -> Act1_TreasureHunt_ResultTalk
+ [*Уйти*] -> END

== Act1_TreasureHunt_Plan
Вы: Каков наш план действий?
{itemIsExist("Shovel"):
София: Нам нужна лопата...А, ты уже с ней! Тогда нам нужно выкопать три небольших ямки, чтобы разместить в них наши "сокровища". Как ты понимаешь, выкопать их я попрошу тебя. А уж я займусь черчением карты.
+ [Я готов приступать к выкапыванию ям]
~setDoneSubTask("treasure_hunt", "meet_sofia")
~setDoneSubTask("treasure_hunt","search_shovel")
-> Act1_TreasureHunt_Digging
- else:
София: Для начала возьми лопату. Дальше нам нужно выкопать три небольших ямки, чтобы разместить в них наши "сокровища". Как ты понимаешь, выкопать их я попрошу тебя. А уж я займусь черчением карты.
+ [Может быть поменяемся ролями? (- Лояльность)] -> Act1_TreasureHunt_ChangeRoles
+ [Понял, приступим] -> END 
}


== Act1_TreasureHunt_ChangeRoles
Вы: Может быть поменяемся ролями?
София: Остроумно, Александр. Но, пожалуй, я откажусь. А у тебя как раз будет возможность потренировать свои мышцы рук.
+ [Воспользуюсь этой возможностью.] -> END

== Act1_TreasureHunt_Discussion
Вы: Как у тебя дела?
София: Норм.
+ [Ок.]
-> END

== Act1_TreasureHunt_Digging
Вы: Я готов приступать к выкапыванию ям 
София: Я отметила тебе на поляне точки раскопок Красными Флажочками. Выкопай три ямки. Этого нам должно хватить.
+ [Элементарно, следи за работой профессионала] -> END

== Act1_TreasureHunt_ResultTalk
Вы: Я выкопал все ямы.
София: Хорошо, в таком случае, я сейчас помещу в них "сокровища". Спасибо тебе за помощь, дальше я справлюсь сама. 
+ [Принял, в таком случае до завтра.] 
~setDoneSubTask("treasure_hunt", "result_talk_sofia")
-> END