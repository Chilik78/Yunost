
== Act1_TreasureHunt_Fishing
{
- isSubTaskInProgress("fishing", "go_to_bed_night"): -> Act1_Fishing
- isSubTaskInProgress("treasure_hunt", "go_to_bed_night"): -> Act1_TreasureHunt
}

== Act1_TreasureHunt
+ [Лечь cпать] 
~setDoneSubTask("treasure_hunt", "go_to_bed_night")
-> END
+ [Осмотреть кровать] -> Act1_TreasureHunt_Fishing_CheckBed
+ [*Уйти*] -> END

== Act1_Fishing
+ [Лечь cпать] 
~setDoneSubTask("fishing", "go_to_bed_night")
-> END
+ [Осмотреть кровать] -> Act1_TreasureHunt_Fishing_CheckBed
+ [*Уйти*] -> END

== Act1_TreasureHunt_Fishing_CheckBed
Кровать: После непродолжительного отдыха тяга к горизонтальному положению тела угасла.
-> Act1_TreasureHunt_Fishing