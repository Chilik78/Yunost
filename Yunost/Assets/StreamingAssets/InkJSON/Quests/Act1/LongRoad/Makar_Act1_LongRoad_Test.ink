
== Act1_LongRoad_Test
+ [Запустить мини-игру]
-> start_mini_game

== start_mini_game
VAR minigame_result = -1
Игра должна запуститься
~startMiniGame(0, 0)
-> wait_for_result

== wait_for_result
~minigame_result = checkStateMiniGame()
** {minigame_result == -1} [Мини-игра идёт] -> wait_for_result
** {minigame_result == 1} [Победа] -> next_dialog_win
** {minigame_result == 0} [Провал] -> next_dialog_failed
-> END

== next_dialog_win
Ты победил в мини-игре
+ [Ок] -> END

== next_dialog_failed
Ты проиграл в мини-игре
+ [Ок] -> END