== Act1_Fishing
Удочка: Рыболовное удилище, которое используют для различных видов ловли рыбы.
+ [Подобрать Удочку]
~pickupItem("fishing_rod")
{itemIsExist("Bait") == true: 
~setDoneSubTask("fishing", "search_fishing_rod")
}
-> END
+ [Закончить осмотр] -> END
-> END