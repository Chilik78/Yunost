
== Act1_TeamGame
Место для выкапывания: В землю воткнут красный флажок. Очевидно, что это нужное место для выкапывания ямы.
+ {itemIsExist("Shovel")} [Выкопать яму]
{CurrentSubquest == "digging_2":
~setDoneSubTask("team_game", "digging_2")
}
{CurrentSubquest == "digging_1":
~setDoneSubTask("team_game", "digging_1")
}
{CurrentSubquest == "digging_0": 
~setDoneSubTask("team_game", "digging_0")
}
~startMiniGameDigging()
-> Act1_TeamGame_Result
+ [*Уйти*] -> END

== Act1_TeamGame_Result
Место для выкапывания: В участке земли образовалась идеальная яма квадратной формы.
+ [Яма готова] -> END