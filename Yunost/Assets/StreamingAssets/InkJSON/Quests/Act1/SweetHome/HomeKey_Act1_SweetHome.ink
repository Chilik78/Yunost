
== Act1_SweetHome
Ключ: У входа в Администрацию лежит Ключ. Видимо, его сюда положили, чтобы не забыть забрать. Вопрос в том, кого ожидает этот ключ?
+ {meetDirector == true or isSubTaskInProgress("sweet_home","search_key")} [Взять ключ] 
~pickupItem("key")
{isSubTaskInProgress("sweet_home","search_key"): 
~setDoneSubTask("sweet_home", "search_key")
}
-> END
+ [Осмотреть ключ] -> Act1_SweetHome_CheckKey
+ [Уйти] -> END

== Act1_SweetHome_CheckKey
Ключ: Внешне ключ начал покрываться ржавчиноЙ из-за неаккуратного использования. На брелке Ключа изображена цифра "1". Скорее всего в будущем, появятся и остальные, но уже с другой цифрой и другим назначением.
+ [Закончить осмотр] -> Act1_SweetHome