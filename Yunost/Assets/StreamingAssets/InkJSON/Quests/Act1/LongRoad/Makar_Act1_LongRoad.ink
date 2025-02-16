
== Act1_LongRoad
~itemExist = itemIsExist("bag")
{itemExist == true && longRoadRepeat2 == false: -> Act1_LongRoad_hasCase}
{itemExist == false && isSubTaskInProgress("long_road", "talk_makar") && longRoadRepeat1 == false: -> Act1_LongRoad_withoutCase}
-> Act1_LongRoad_repeat

// ГГ подобрал Чемодан
= Act1_LongRoad_hasCase
Макар: Санёк, ты еле на ногах стоишь. Иди к своему домику, оставь там Чемодан и отдохни, а то прям тут свалишься. И, да, у соседнего домика тебя ждёт Олег, ему требуется твоя помощь.
+ [Правда, стоило бы передохнуть.]
    // Смена текущего подзадания
    //{isSubTaskInProgress("long_road", "talk_makar"): 
    //~setDoneSubTask("long_road", "talk_makar")
    //}
    ~setDoneSubTask("long_road", "take_case")
    -> Act1_LongRoad_hasCaseTired
+ [Да я полон сил! Пошли к Директору.]
    // Cмена текущего подзадания
    //{isSubTaskInProgress("long_road", "talk_makar"): 
    //~setDoneSubTask("long_road", "talk_makar")
    //}
    ~setDoneSubTask("long_road", "take_case")
    ->Act1_LongRoad_hasCaseEnergetic
-> DONE

// ГГ ещё не подобрал Чемодан
= Act1_LongRoad_withoutCase
Макар: Санёк, ты еле ковыляешь. А чемодан то твой где? У автобуса оставил? Давай забирай свой чемодан - жду тебя тут.
+ [Чемодан...точно! Я мигом!]
    // Cмена текущего подзадания
    ~setDoneSubTask("long_road", "talk_makar")
    -> Act1_LongRoad_pickUpCase1
+ [Что ещё за чемодан? Где я?]
    // Cмена текущего подзадания
    ~setDoneSubTask("long_road", "talk_makar")
    -> Act1_LongRoad_pickUpCase2
-> DONE

// ГГ вспомнил о Чемодане
= Act1_LongRoad_pickUpCase1
Вы: Чемодан...точно! Я мигом!
Макар: Давай, пошевеливайся! Дел полно!
+ [Иду, иду...не шелести особо...]
    // Cмена текущего подзадания
    //~setDoneSubTask("long_road", "talk_makar")
    ~longRoadRepeat1 = true
-> END

// ГГ забыл о Чемодане
= Act1_LongRoad_pickUpCase2
Вы: Что ещё за чемодан? Где я?
Макар: Да...тяжёлый случай. Сань, время моё не трать, чуть позже директор приведёт тебя в чувство. Бери тот чемодан у автобуса в руки и сюда. Я же понятно изъясняюсь?
+ [Чемодан. Автобус. Руки.]
    // Cмена текущего подзадания
    //~setDoneSubTask("long_road", "talk_makar")
    ~longRoadRepeat1 = true
-> END

// ГГ с Чемоданом, но устал
= Act1_LongRoad_hasCaseTired
Вы: Правда, стоило бы передохнуть.
Макар: Давай, дорогу я надеюсь ты помнишь. Иди до театральной сцены прямо, потом налево и выйдешь к домикам. А и да...Тебя там Олег ждёт у своего дома, у него какая-то проблема. Встретимся позже. 
+ [Принял. До встречи.]
~longRoadRepeat2 = true
-> END

// ГГ с Чемоданом, но полон сил
= Act1_LongRoad_hasCaseEnergetic
Вы: Да я полон сил! Пошли к директору.
Макар: Тебе видней. В любом случае отнеси чемодан в свой дом. И да...заодно Олегу помоги, он тебя ждёт у своего дома. Иди до театральной сцены прямо, потом налево и выйдешь к домикам.
+ [До встречи.]
    ~longRoadRepeat2 = true
-> END

// Повторный диалог
= Act1_LongRoad_repeat
{longRoadRepeat1 == true && longRoadRepeat2 == false: 
Макар: Хватит уже маячить перед глазами. Подбери уже свой чемодан.
+ [Задумался, сейчас заберу.]
-> DONE
- else:
Макар: Напоминаю, отнеси свои вещи в свой дом. Иди до театральной сцены прямо, потом налево и выйдешь к домикам. Не забудь про Олега!
+ [Выдвигаюсь.]
-> DONE
}


