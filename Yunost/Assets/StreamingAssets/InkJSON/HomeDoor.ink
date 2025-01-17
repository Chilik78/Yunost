INCLUDE globals.ink
EXTERNAL itemIsExist(item)
EXTERNAL setDoneTask(idTask)
EXTERNAL setDoneSubTask(idTask, idSubTask)
EXTERNAL changeScene(nameScene)
-> NameQuest
INCLUDE Quests\Act1\HelpForFriend\HomeDoor_Act1_HelpForFriend.ink
INCLUDE Quests\Act1\SweetHome\HomeDoor_Act1_SweetHome.ink

== NameQuest
{ 
- CurrentQuest == "help_for_friend": -> Act1_HelpForFriend
- CurrentQuest == "sweet_home": -> Act1_SweetHome
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