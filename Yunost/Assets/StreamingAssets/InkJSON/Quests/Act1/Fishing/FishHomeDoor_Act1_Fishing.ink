== Act1_Fishing 
Дверь Рыболовной Хижины: Внимание привлекает классический цилиндрический дверной замок с поднятым "белым флагом". *К этой двери применять силу не стоит, ей и так уже досталось*
+ {isSubTaskInProgress("fishing", "meet_oleg_fishing") or isSubTaskInProgress("fishing", "search_fishing_rod")} [Войти в Рыболовную Хижину] 
~tp("Player", "start_fish_home")
-> END
+ {isSubTaskInProgress("fishing", "go_to_fishing")} [Выйти в Лесные Окрестности]
~tp("Player", "near_fish_home")
-> END
+ [*Уйти*] -> END
