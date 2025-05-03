VAR minigame_result = -1

== Act1_TreasureHunt
Место для выкапывания: В землю воткнут красный флажок. Очевидно, что это нужное место для выкапывания ямы.
+ {itemIsExist("Shovel")} [Выкопать яму] 
~startMiniGame(4, 1)
-> Act1_TreasureHunt_CheckStateMiniGame
+ [*Уйти*] -> END

== Act1_TreasureHunt_CheckStateMiniGame
~minigame_result = checkStateMiniGame()
** {minigame_result == -1} [*Осмотреть яму*] -> Act1_TreasureHunt_CheckStateMiniGame

** {minigame_result == 1} [*Оценить полученную яму*] -> Act1_TreasureHunt_MiniGameWin

** {minigame_result == 0} [*Оценить полученную яму*] -> Act1_TreasureHunt_MiniGameFailed
-> END

== Act1_TreasureHunt_MiniGameFailed
~hitStamina(10)
Вы: *Оценить полученную яму*
Место для выкапывания: Яма недостаточно глубокая. Нужно приложить ещё усилий.
+ [Эх, нужно постараться получше]
-> END

== Act1_TreasureHunt_MiniGameWin
{isSubTaskInProgress("treasure_hunt", "digging"):
~setDoneSubTask("treasure_hunt", "digging")
}
Место для выкапывания: В участке земли образовалась идеальная яма.
+ [Яма готова] -> END

