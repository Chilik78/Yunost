
== Act1_HelpForFriend
~toolExist = itemIsExist("tool")
Дверь: Та самая дверь, за которой вряд-ли можно будет обнаружить что-то интересное. Сейчас замок этой Двери лишь испытание, рассчитанное на проверку дружбы и смекалки.
+ {tryToOpenDoor == false && doorOpened == false} [Открыть дверь] -> Act1_HelpForFriend_TryOpenDoor
+ {toolExist == true && doorOpened == false} [Взломать дверь] -> Act1_HelpForFriend_BreakingDoor
+ [Осмотреть дверь] -> Act1_HelpForFriend_CheckDoor
+ {firstCheckDoor == true && doorOpened == false} [Попробовать выбить дверь] -> Act1_HelpForFriend_HitDoor
+ [Отойти от двери] -> END


== Act1_HelpForFriend_TryOpenDoor
Дверь: Проворот дверной ручки не дал ожидаемого результата. Дверь и вправду оказалась закрыта. Но а как же приступать к решительным мерам, предварительно не смирившись с реальностью, где замок применяют по назначению, а не просто используют для создания ложного впечатления защищённости.
+ [Оставить попытки открыть дверь] 
~tryToOpenDoor = true
-> Act1_HelpForFriend

== Act1_HelpForFriend_CheckDoor
Дверь: Дверь изготовлена из натуральной древесины. Обыкновенность двери подчёркивает стандартный цилиндровый замок, рассчитанный на несколько проворотов ключа. Совершив резкий удар плечём в дверную область вблизи замка есть вероятность открыть дверь без лишней подготовки. С другой стороны, если раздобыть Инструменты для взлома, то можно взломать замок и избежать лишних "жертв". 
+ [Закончить осмотр] 
{firstCheckDoor == false && isSubTaskInProgress("help_for_friend", "check_door"): 
~setDoneSubTask("help_for_friend", "check_door")
}
~firstCheckDoor = true
-> Act1_HelpForFriend

== Act1_HelpForFriend_HitDoor
Дверь: Сильный удар плечом в деревянную Дверь застал Замок врасплох. Громкий треск дерева в сочетании со звонким механическим звуком Замка привлекли внимание Олега. Судя по удивлённому лицу он определённо не ожидал такого развития событий. Дверь открыта! Но теперь...придётся столкнуться с последствиями такого выбора.
+ [*лёгкий стон* Ух...Моё плечо...]
~hitHealth(30)
{isSubTaskInProgress("help_for_friend", "search_tools"): 
~setDoneSubTask("help_for_friend", "search_tools")
~setDoneSubTask("help_for_friend", "open_door")
}
{isSubTaskInProgress("help_for_friend", "open_door"):
~setDoneSubTask("help_for_friend", "open_door")
}
~doorOpened = true
~hitDoor = true
-> Act1_HelpForFriend_HitDoorOpened

== Act1_HelpForFriend_HitDoorOpened
Вы: *лёгкий стон* Ух...Моё плечо...
Олег: Сань, вот это ты влетел в дверь! Это было нечто! Как ты? Цел? Рекомендую тебе потом сходить в медпункт, чтобы тебя осмотрели. Ха-ха! Никто мне не поверит, как сам Александр именно так решил проблему с дверью. Интересно, что скажет Анатолий Степанович на этот счёт...
+ [Разберёмся. Я попробую ему всё объяснить.]
-> END

== Act1_HelpForFriend_BreakingDoor
~startMiniGame()
~doorOpened = true
Дверь: Через какое-то время сосредоточенной попытки овладения Инструментами для взлома прозвучал долгожданный металлический щелчок. Дверь открылась! Стоит отметить, при этом никто не пострадал. Но судя по реакции Замка...в следующий раз лучше использовать Ключ.
~setDoneSubTask("help_for_friend", "open_door")
+ [Ура! Я сделал это! Дверь открыта!] -> END

 