== Act1_Fishing
Место для ловли рыбы: Место у пристане не кажется многообещающим, но попробовать стоит.
+ {useFishPlace2 == false} [Закинуть удочку]
~startMiniGame(0, difficultyFishing)
~useFishPlace2 = true
-> Act1_Fishing_Result
+ [*Уйти*]
-> END

== Act1_Fishing_Result
TODO: Добавить разветвление (Поймал рыбу или нет)
Место для ловли рыбы: Рыба поймана! На крючке, видимо, Золотой Карась!
+ [Поймал!]
~setDoneSubTask("fishing", "fishing_at_beach")
-> END