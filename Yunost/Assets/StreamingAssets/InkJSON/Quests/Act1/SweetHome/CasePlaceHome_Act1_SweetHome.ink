
== Act1_SweetHome
+ {itemIsExist("bag")} [Поставить Чемодан] 
~setDoneSubTask("sweet_home", "drop_case")
~putItem()
-> END
+ [Осмотреть угол] -> Act1_SweetHome_CasePlaceCheck
+ [*Уйти*]
-> END

== Act1_SweetHome_CasePlaceCheck
Угол у стола: Идеальное место, чтобы разместить Чемодан и что-либо ещё.
+ [Закончить осмотр] -> Act1_SweetHome