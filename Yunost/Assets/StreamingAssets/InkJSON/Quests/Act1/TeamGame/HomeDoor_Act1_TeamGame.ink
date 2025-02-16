
== Act1_TeamGame
+ {isSubTaskInProgress("team_game", "leave_house")} [Выйти из Дома №1] 
~setDoneSubTask("team_game", "leave_house")
~changeSceneWithTp("MainCamp", "NearHubHome")
~tpNPC()
-> END
+ {isSubTaskInProgress("team_game", "go_to_bed_night")} [Войти в Дом №1] 
~changeScene("HubHome")
-> END
+ {isSubTaskInProgress("team_game", "leave_house")} [Осмотреть дверь] -> Act1_TeamGame_CheckDoorIn
+ {not isSubTaskInProgress("team_game", "leave_house")} [Осмотреть дверь] -> Act1_TeamGame_CheckDoorOutside
+ [*Уйти*] -> END

== Act1_TeamGame_CheckDoorIn
Дверь: Входная деревянная дверь с цилиндрическим замков Дома №1. Если открыть дверь, то снаружи ждут летние солнечные лучи и обязательства перед администрацией лагеря.
+ [Закончить осмотр] -> Act1_TeamGame

== Act1_TeamGame_CheckDoorOutside
Дверь: В центре Двери красуется табличка с цифрой "1". Видимо, это единственный опознавательный знак, чтобы отличить один жилой Дом от другого. У дверной ручки расположился простой цилиндрический замок, рассчитанный на несколько проворотов ключа. Есть предчувствие, что с этой дверью придётся взимодействовать не раз.
+ [Закончить осмотр] -> Act1_TeamGame
