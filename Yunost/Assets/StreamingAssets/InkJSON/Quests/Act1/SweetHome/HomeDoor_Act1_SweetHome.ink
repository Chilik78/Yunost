

== Act1_SweetHome
Дверь: Входная деревянная Дверь в Дом. Добавить больше нечего.

// Сцена MainCamp
+ {homeDoorOpened == true && isSubTaskInProgress("sweet_home", "go_to_home")} [Войти в Дом №1] 
~setDoneSubTask("sweet_home","go_to_home")
~changeScene("HubHome")
-> END 
+ {isSubTaskInProgress("sweet_home", "open_door")} [Открыть дверь] -> Act1_SweetHome_OpenDoor
+ {not isSubTaskInProgress("sweet_home", "drop_case") && not isSubTaskInProgress("sweet_home", "go_to_bed")} [Осмотреть дверь] -> Act1_SweetHome_CheckDoorOutside
// Сцена HubHome
+ {isSubTaskInProgress("sweet_home", "drop_case") or isSubTaskInProgress("sweet_home", "go_to_bed")} [Осмотреть дверь] -> Act1_SweetHome_CheckDoorIn
+ [Уйти] -> END

== Act1_SweetHome_CheckDoorIn
Дверь: Входная деревянная дверь с цилиндрическим замков Дома №1. Если открыть дверь, то снаружи ждут летние солнечные лучи и обязательства перед администрацией лагеря.
+ [Закончить осмотр] -> Act1_SweetHome

-> Act1_SweetHome

== Act1_SweetHome_CheckDoorOutside
Дверь: В центре Двери красуется табличка с цифрой "1". Видимо, это единственный опознавательный знак, чтобы отличить один жилой Дом от другого. У дверной ручки расположился простой цилиндрический замок, рассчитанный на несколько проворотов ключа. Есть предчувствие, что с этой дверью придётся взимодействовать не раз.
+ [Закончить осмотр] -> Act1_SweetHome

== Act1_SweetHome_OpenDoor
Дверь: Пару звонких проворотов ключом и замок больше не является преградой. Интересно, наступит ли момент, когда дверь перестанет подчиняться прихоти замка.
~homeDoorOpened = true
~setDoneSubTask("sweet_home","open_door")
-> Act1_SweetHome