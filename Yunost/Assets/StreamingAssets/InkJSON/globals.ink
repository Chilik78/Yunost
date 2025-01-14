// Текущее задание
VAR CurrentQuest = "long_road"
VAR CurrentSubquest = "talk_makar"

/*
// Управляющие переменные для Макара
VAR CurrentQuestMakar = "LongRoad"

// Управляющие переменные для Олега
VAR CurrentQuestOleg = "LongRoad"

// Управляющие переменные для Директора
VAR CurrentQuestDirector = "LongRoad"

// Управляющие переменные для Софии
VAR CurrentQuestSofia = "LongRoad"

// Управляющие переменные для Елизаветы
VAR CurrentQuestLisa = "LongRoad"

// Функция для изменения текущего квеста игровым персонажам
== function changeCurrentQuests(Makar, Oleg, Director, Sofia, Lisa)
~CurrentQuestMakar = Makar
~CurrentQuestOleg = Oleg
~CurrentQuestDirector = Director
~CurrentQuestSofia = Sofia
~CurrentQuestLisa = Lisa

== function changeCurrentQuestMakar(Makar)
~CurrentQuestMakar = Makar

== function changeCurrentQuestOleg(Oleg)
~CurrentQuestOleg = Oleg

== function changeCurrentQuestDirector(Director)
~CurrentQuestDirector = Director

== function changeCurrentQuestSofia(Sofia)
~CurrentQuestSofia = Sofia

== function changeCurrentQuestLisa(Lisa)
~CurrentQuestLisa = Lisa
*/

// Главные Ворота
VAR Удар_По_Воротам = "Нет"

//// Act1 ////

/// LongRoad

// Поговорить с Макаром - talk_makar
// Взять Чемодан и вернуться к Макару - take_case
// Найти Олега у Домиков - search_oleg

// Makar
VAR itemExist = false
VAR longRoadRepeat1 = false
VAR longRoadRepeat2 = false

// MainGait
VAR hitGait = false


// Акт 1. Друг в беде

VAR Повторение_Инструменты_Акт1_ПомощьДругу = "Нет"
VAR Повторение_Ключ_Акт1_ДомМилыйДом = "Нет"
VAR Инструмент_Найден = false

// Акт 1. Дом..Милый дом

VAR Ключ_Подобран = false
VAR Дверь_Открыта = "Нет"
VAR Повторение_Директор_Акт1_ДомМилыйДом = "Нет"


