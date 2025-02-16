
== Act1_TeamGame
Место для выкапывания: В землю воткнут красный флажок. Очевидно, что это нужное место для выкапывания ямы.
+ {itemIsExist("Shovel")} [Выкопать яму]
{isSubTaskInProgress("team_game", "digging"):
~setDoneSubTask("team_game", "digging")
}
~startMiniGameDigging()
-> Act1_TeamGame_Result
+ [*Уйти*] -> END

== Act1_TeamGame_Result
Место для выкапывания: В участке земли образовалась идеальная яма квадратной формы.
+ [Яма готова] -> END