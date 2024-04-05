# SpellScraper
***SpellScraper*** - пиксельная Top-Down 2D игра в жанре экшн с магией и моральным выбором. 

Игроку предлагается добраться до финального босса на самом верху небоскрёба. В процессе продвижения наверх ему придется сразиться с врагами и минибоссами, используя магию.

Особенностью игры является моральный выбор игрока – возможность использовать смертельные и несмертельные атаки. Ваш выбор будет влиять на финальную схватку с главным боссом. 

***Целью*** проекта является создание игры с вышеперечисленными особенностями, а также получить опыт в разработке и научиться работать в команде. Для этого необходимо выполнить следующие ***задачи***:

• Распределение ролей в команде

• Разработка основных механик

• Написание саундтреков

• Отрисовка спрайтов

• Дизайн уровней

### Ответы на вопросы по механике
| Действия | Кнопики |
| --- | --- |
| Движение  | `W`, `A`, `S`, `D` |
| Смертельная атака | `ЛКМ` |
| Несмертельная атака | `ПКМ` |
| Выбор магии | `1` - `4`, `СКМ` |
| Направление игрока| `Курсор` |
| Отражение снарядов| `Пробел`|

Если столкнутся снаряды огонь-вода или земля-воздух, то они взаимоуничтожатся.

Если к вам достаточно близко подлетел вражеский снаряд, то по нажатию пробела вы сможете отразить его обратно.

В левом верхнем углу расположена маска - индикатор выбранной магии. 

Маска также является индикатором здровья игрока. Если вы увидели, что на ней появилась трещина - значит следующий снаряд, попавший по вам, будет смертельным.

Цвет глаз отображает выбранную стихию и 50% сопротивление к этой магии. 

У врагов также, как и у игрока, есть 50% сопротивление к магии, зависящее от их цвета. Например, огненный(красный) враг будет умирать от двух попаданий огненной магией.

На ЛКМ ГГ стреляет смертельной магией, а на ПКМ - несмертельной. От типа атаки, которой вы расправляетесь с врагом, будет зависеть концовка.
