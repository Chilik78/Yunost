
== Act1_TreasureHunt_Fishing
+ {isSubTaskInProgress("meeting", "leave_house")} [Выйти из Дома №1] 
~setDoneSubTask("meeting", "leave_house")
~tp("Player", "near_hub_home")
TODO: Перемещение NPC
~applyPlacement("Акт 1", "meeting_admin")
-> END
+ {isSubTaskInProgress("treasure_hunt", "go_to_bed_night") or isSubTaskInProgress("fishing", "go_to_bed_night")} [Войти в Дом №1] 
~tp("Player", "start_hub_home")
-> END
+ {isSubTaskInProgress("meeting", "leave_house")} [Осмотреть дверь] -> Act1_TreasureHunt_Fishing_CheckDoorIn
+ {not isSubTaskInProgress("meeting", "leave_house")} [Осмотреть дверь] -> Act1_TreasureHunt_Fishing_CheckDoorOutside
+ [*Уйти*] -> END

== Act1_TreasureHunt_Fishing_CheckDoorIn
Дверь: Входная деревянная дверь с цилиндрическим замков Дома №1. Если открыть дверь, то снаружи ждут летние солнечные лучи и обязательства перед администрацией лагеря.
+ [Закончить осмотр] -> Act1_TreasureHunt_Fishing

== Act1_TreasureHunt_Fishing_CheckDoorOutside
Дверь: В центре Двери красуется табличка с цифрой "1". Видимо, это единственный опознавательный знак, чтобы отличить один жилой Дом от другого. У дверной ручки расположился простой цилиндрический замок, рассчитанный на несколько проворотов ключа. Есть предчувствие, что с этой дверью придётся взимодействовать не раз.
+ [Закончить осмотр] -> Act1_TreasureHunt_Fishing
