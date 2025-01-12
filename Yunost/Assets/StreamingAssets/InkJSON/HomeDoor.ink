INCLUDE globals.ink
EXTERNAL itemInInventory(item)
EXTERNAL changeScene(sceneName)
EXTERNAL setDoneTask(idTask)
EXTERNAL setDoneSubTask(idTask, idSubTask)


-> ДверьГлавногоДома

== ДверьГлавногоДома ==
~Ключ_Подобран = itemInInventory("key")
Дверь: Обычная деревянная дверь...с замком. Уговаривать её открыться не имеет смысла. Лишь ключ поможет узнать, что скрывается за ней.
    {Дверь_Открыта == "Да":
        + [Зайти в дом]
        ~setDoneSubTask("sweet_home", "sweet_home_3")
        ~changeScene("MyHome")
        -> END
    }
    {Ключ_Подобран == true: 
        + [Открыть дверь ключом] -> ДверьОткрыта
    }
    + [Закончить осмотр] -> END

== ДверьОткрыта == 
Дверь: Пару звонких проворотов ключом и замок больше не является преградой. Интересно, когда наступит момент, когда ключ перестанет быть авторитетом для замка?
    ~setDoneSubTask("sweet_home", "sweet_home_2")
    ~Дверь_Открыта = "Да"
    + [Наконец-то я смогу отдохнуть] -> ДверьГлавногоДома