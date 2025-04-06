== Act1_Fishing
Ворота: Аналогичные ворота тем, которые сторожат главный вход в Пионерский лагерь. Судя по всему, строители решили не утруждаться проектированием иных ворот.
+ [Открыть ворота]
{
- isSubTaskInProgress("fishing", "meet_oleg_fishing"): 
~tp("Player", "start_forest_area")

- isSubTaskInProgress("fishing", "go_to_bed_night_fishing"):
~tp("Player", "start_main_camp_from_forest")

}
-> END
+ [Закончить осмотр] 
-> END
