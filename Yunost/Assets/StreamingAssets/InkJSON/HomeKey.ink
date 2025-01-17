INCLUDE globals.ink
EXTERNAL pickupItem(item)
EXTERNAL setDoneSubTask(idTask, idSubTask)
EXTERNAL setDoneTask(idTask)
-> NameQuest
INCLUDE Quests\Act1\SweetHome\HomeKey_Act1_SweetHome.ink


== NameQuest
{ 
- CurrentQuest == "sweet_home" or CurrentQuest == "help_for_friend": -> Act1_SweetHome
} 
-> END