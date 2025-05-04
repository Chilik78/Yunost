
== Act1_Fishing_TreasureHunt
Новостная газета: Головоломка в виде лабиринта занимает целую газетную страницу.
+[Решить головоломку] 
~startMiniGame(8, 0)
{
- isSubTaskInProgress("fishing", "reading_newspaper"):
~setDoneSubTask("fishing", "reading_newspaper")

- else:
~setDoneSubTask("treasure_hunt", "reading_newspaper")
}
-> END
+[*Уйти*] -> END

