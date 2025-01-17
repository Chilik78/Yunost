
== Act1_SweetHome
Дверь: Входная деревянная Дверь в Дом. Добавить больше нечего.
+ {homeDoorOpened == true} [Зайти в Дом №1] 
~setDoneSubTask("sweet_home","go_to_home")
~changeScene("HubHome")
-> END 
+ {CurrentSubquest == "open_door"} [Открыть дверь] -> Act1_SweetHome_OpenDoor
+ [Осмотреть дверь] -> Act1_SweetHome_CheckDoor
+ [Уйти] -> END

== Act1_SweetHome_CheckDoor
Дверь: В центре Двери красуется табличка с цифрой "1". Видимо, это единственный опознавательный знак, чтобы отличить один жилой Дом от другого. У дверной ручки расположился простой цилиндрический замок, рассчитанный на несколько проворотов ключа. Есть предчувствие, что с этой дверью придётся взимодействовать не раз.
+ [Закончить осмотр] -> Act1_SweetHome

== Act1_SweetHome_OpenDoor
Дверь: Пару звонких проворотов ключом и замок больше не является преградой. Интересно, наступит ли момент, когда дверь перестанет подчиняться прихоти замка.
~homeDoorOpened = true
~setDoneSubTask("sweet_home","open_door")
-> Act1_SweetHome