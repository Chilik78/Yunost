INCLUDE globals.ink
EXTERNAL itemIsExist(item)
EXTERNAL setStateTask(taskId, state)
EXTERNAL setDoneSubTask(taskId, subTaskId)
EXTERNAL isTaskInProgress(taskId, type)
EXTERNAL isSubTaskInProgress(taskId, subTaskId)
EXTERNAL changeScene(nameScene)
EXTERNAL changeSceneWithTp(nameScene, id)
EXTERNAL tpNPC()
-> NameQuest

INCLUDE Quests\Act1\HelpForFriend\HomeDoor_Act1_HelpForFriend.ink
INCLUDE Quests\Act1\SweetHome\HomeDoor_Act1_SweetHome.ink
INCLUDE Quests\Act1\TeamGame\HomeDoor_Act1_TeamGame.ink

== NameQuest
{ 
- isTaskInProgress("help_for_friend", 0): -> Act1_HelpForFriend
- isTaskInProgress("sweet_home", 0): -> Act1_SweetHome
- isTaskInProgress("team_game", 0): -> Act1_TeamGame
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
        //~changeScene("MyHome")
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