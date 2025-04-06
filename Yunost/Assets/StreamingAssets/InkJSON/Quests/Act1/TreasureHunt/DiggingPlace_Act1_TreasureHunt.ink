
== Act1_TreasureHunt
Место для выкапывания: В землю воткнут красный флажок. Очевидно, что это нужное место для выкапывания ямы.
+ {itemIsExist("Shovel")} [Выкопать яму]
{isSubTaskInProgress("treasure_hunt", "digging"):
~setDoneSubTask("treasure_hunt", "digging")
}
~startMiniGame(4, 1)
-> Act1_TreasureHunt_Result
+ [*Уйти*] -> END

== Act1_TreasureHunt_Result
Место для выкапывания: В участке земли образовалась идеальная яма квадратной формы.
+ [Яма готова] -> END