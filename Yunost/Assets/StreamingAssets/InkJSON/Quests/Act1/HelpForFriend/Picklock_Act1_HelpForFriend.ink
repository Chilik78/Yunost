
== Act1_HelpForFriend
Отмычка: Тонкое изогнутое металлическое изделие, которое окажись в неправильных руках может принести беду. Но сегодня ей повезло поучаствовать в специальной операции по спасению заклинившего дверного замка.
+ [Подобрать Набор отмычек] 
~pickupItem("picklock")
~picklockExist = true
{screwdriverExist == true:
~setDoneSubTask("help_for_friend", "search_tools")
}
-> END
+ [Закончить осмотр] -> END
-> END