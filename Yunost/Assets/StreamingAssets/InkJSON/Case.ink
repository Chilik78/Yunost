INCLUDE globals.ink
EXTERNAL pickupItem(item)
-> NameQuest
INCLUDE Quests\Act1\LongRoad\Case_Act1_LongRoad.ink

== NameQuest
{ 
- CurrentQuest == "long_road": -> Act1_LongRoad
} 
-> END