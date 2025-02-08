
== Act1_HelpForFriend
{
- meetDirector == false: -> Act1_HelpForFriend_FirstTalk
- meetDirector == true: -> Act1_HelpForFriend_Repeat
}
-> END

== Act1_HelpForFriend_FirstTalk
Анатолий Степанович: Здравствуй, Александр. Хорошо, что ты зашёл ко мне! Я забыл передать твой ключ от твоего Домика. Я оставил его у входа в Администрацию.
+ [Спасибо, заберу Ключ и пойду отдыхать]
~meetDirector = true
-> END

== Act1_HelpForFriend_Repeat
Директор: *Стоит и напевает мелодию из очень уж знакомой советской композиции*
+ [*Уйти*] -> END