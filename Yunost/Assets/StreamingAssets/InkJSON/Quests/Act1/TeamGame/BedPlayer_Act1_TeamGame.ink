
== Act1_TeamGame
+ {isSubTaskInProgress("team_game", "go_to_bed_night")} [Лечь спать] 
~setDoneSubTask("team_game", "go_to_bed_night")
-> END
+ [Осмотреть кровать] -> Act1_TeamGame_CheckBed
+ [*Уйти*] -> END

== Act1_TeamGame_CheckBed
Кровать: После непродолжительного отдыха тяга к горизонтальному положению тела угасла.
-> Act1_TeamGame