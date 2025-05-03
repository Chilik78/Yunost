VAR minigame_result = -1

== Act1_Fishing
Место для ловли рыбы: Край пристани, наверное, самое удачное место для ловли крупных видов рыбы.
+ {useFishPlace1 == false} [Закинуть удочку] 
~startMiniGame(0, difficultyFishing)
~useFishPlace1 = true
-> Act1_Fishing_CheckStateMiniGame
+ [*Уйти*]
-> END

== Act1_Fishing_CheckStateMiniGame
~minigame_result = checkStateMiniGame()
** {minigame_result == -1} [*Вытащить удочку*] -> Act1_Fishing_CheckStateMiniGame

** {minigame_result == 1} [*Снять рыбу с удочки*] -> Act1_Fishing_MiniGameWin

** {minigame_result == 0} [*Осмотреть крючок*] -> Act1_Fishing_MiniGameFailed

== Act1_Fishing_MiniGameWin
Вы: *Снять рыбу с удочки*
Место для ловли рыбы: Рыба поймана! На крючке, видимо, Обыкновенный Судак!
+ [Поймал!]
~setDoneSubTask("fishing", "fishing_at_beach")
~fishPlace1Win = true
-> END

== Act1_Fishing_MiniGameFailed
~hitStamina(10)
Вы: *Осмотреть крючок*
Место для ловли рыбы: Рыба сорвалась с крючка.
+ [Сорвалась!]
~setDoneSubTask("fishing", "fishing_at_beach")
-> END
