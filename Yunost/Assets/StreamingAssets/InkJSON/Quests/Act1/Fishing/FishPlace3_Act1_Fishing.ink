== Act1_Fishing
Место для ловли рыбы: Песчаный берег чуть дальше пристани выглядит многообещающим местом.
+ {useFishPlace3 == false} [Закинуть удочку]
~startMiniGame(0, difficultyFishing)
~useFishPlace3 = true
-> Act1_Fishing_Result
+ [*Уйти*]
-> END

== Act1_Fishing_Result
TODO: Добавить разветвление (Поймал рыбу или нет)
Место для ловли рыбы: Рыба поймана! На крючке, видимо, Обыкновенный Язь!
+ [Поймал!]
~setDoneSubTask("fishing", "fishing_at_beach")
-> END