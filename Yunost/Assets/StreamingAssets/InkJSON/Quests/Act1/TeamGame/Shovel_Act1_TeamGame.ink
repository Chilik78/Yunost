
== Act1_TeamGame
Лопата: Деревянный черенок с металлическим полотном приостановил свою деятельность на обеденный перерыв.
+ [Взять лопату]
~pickupItem("Shovel")
-> END
+ [Осмотреть лопату] -> Act1_TeamGame_CheckShovel
+ [*Уйти*] -> END

== Act1_TeamGame_CheckShovel
TODO Добавить описание
-> END