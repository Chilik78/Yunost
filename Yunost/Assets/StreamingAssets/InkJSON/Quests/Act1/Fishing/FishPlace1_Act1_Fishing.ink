== Act1_Fishing
Место для ловли рыбы: Край пристани, наверное, самое удачное место для ловли крупных видов рыбы.
+ {useFishPlace1 == false} [Закинуть удочку]
~startMiniGame(0, difficultyFishing)
~useFishPlace1 = true
-> Act1_Fishing_Result
+ [*Уйти*]
-> END

== Act1_Fishing_Result
TODO: Добавить разветвление (Поймал рыбу или нет)
Место для ловли рыбы: Рыба поймана! На крючке, видимо, Обыкновенный Судак!
+ [Поймал!]
~setDoneSubTask("fishing", "fishing_at_beach")
-> END