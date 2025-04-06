== Act1_Fishing
Банка с приманкой: Консервная банка от тушёнки. Тушёнки там больше нет. Банка на половину наполнена землёй, а в ней временно проживают черви.
+ [Подобрать Банку с приманкой]
~pickupItem("bait")
{itemIsExist("FishingRod") == true: 
~setDoneSubTask("fishing", "search_fishing_rod")
}
-> END
+ [Закончить осмотр] -> END
-> END