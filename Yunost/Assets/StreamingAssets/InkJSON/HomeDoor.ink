INCLUDE globals.ink
EXTERNAL itemIsExist(item)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)
EXTERNAL tp(objName, id)
EXTERNAL applyPlacement(act, name)
-> NameQuest

INCLUDE Quests\Act1\HelpForFriend\HomeDoor_Act1_HelpForFriend.ink
INCLUDE Quests\Act1\SweetHome\HomeDoor_Act1_SweetHome.ink
INCLUDE Quests\Act1\Fishing\HomeDoor_Act1_TreasureHunt_Fishing.ink

== NameQuest
{ 
- isTaskInProgress("help_for_friend", 0): -> Act1_HelpForFriend
- isTaskInProgress("sweet_home", 0): -> Act1_SweetHome
- isTaskInProgress("meeting", 0) or isTaskInProgress("treasure_hunt", 0) or isTaskInProgress("fishing", 0): -> Act1_TreasureHunt_Fishing
} 
-> END

/*
-> ДверьГлавногоДома

== ДверьГлавногоДома ==
//~Ключ_Подобран = itemInInventory("key")
Дверь: Обычная деревянная дверь...с замком. Уговаривать её открыться не имеет смысла. Лишь ключ поможет узнать, что скрывается за ней.
    {Дверь_Открыта == "Да":
        + [Зайти в дом]
        //~setDoneSubTask("3", "3")
        -> END
    }
    {Ключ_Подобран == true: 
        + [Открыть дверь ключом] -> ДверьОткрыта
    }
    + [Закончить осмотр] -> END

== ДверьОткрыта == 
Дверь: Пару звонких проворотов ключом и замок больше не является преградой. Интересно, когда наступит момент, когда ключ перестанет быть авторитетом для замка?
    //~setDoneSubTask("3", "2")
    //~Дверь_Открыта = "Да"
    + [Наконец-то я смогу отдохнуть] -> ДверьГлавногоДома
*/