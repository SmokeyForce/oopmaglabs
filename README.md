Концепція ТЗ для усіх лабораторних робіт: «Інтелектуальна система управління паркуванням»

Опис проекту:
Розробити систему для управління паркувальними майданчиками в місті,торговому центрі, внутрішнього типу(наприклад підземний паркінг) та завнішнього типу(парковка біля ТЦ), що включає моніторинг вільних місць, резервування, динамічне встановлення тарифів, управління доступом і аналітику. Система повинна бути модульною, масштабованою та гнучкою для інтеграції з різними апаратними та програмними рішеннями (сенсори, мобільний додаток, веб-інтерфейс тощо). Використання паттернів дозволить розбити завдання на логічні блоки, забезпечити розширюваність і зрозумілість архітектури.

В першій лабораторній роботі було використано такі паттерни - Factory Method: Забезпечує створення конкретних об’єктів (наприклад, сенсорів зайнятості, бар’єрів, інформаційних панелей) через загальний інтерфейс, що дозволяє легко додавати нові типи пристроїв.
Abstract Factory: Дозволяє створювати сімейства сумісних пристроїв для різних типів паркувальних зон (наприклад, внутрішні парковки, вуличні майданчики, VIP-зони), що гарантує їх узгодженість.
Фабрика для створення внутрішньої парковки та фабрика для створення зовнішньої парковки.
Взаємодія – Сенсор фіксує появу машини -> Бар'єр підіймається -> Паркова зона стає активною -> Бар’єр закривається 
Абстрактно доступні пристрої поділяють на зовнішні та внутрішні, так само як і паркові зони, при створенні паркової зони тільки пристрої притаманні цій зоні можуть створюватись.

В другій лабораторній роботі були такі паттерни - Prototype: Дозволяє клонувати вже налаштований пристрій або конфігурацію, що зручно для створення численних подібних пристроїв із збереженням базових налаштувань.
Builder: Для поступового створення складних об’єктів, таких як налаштування сценаріїв роботи пристроїв.
За допомогою Builder створюється складні об’єкти, які уточнують деталі парковки, далі Prototype клонує цю парковку та дозволяє змінити в нії певні деталі конфігурації.

В третій лабораторній роботі були такі паттерни - Strategy: Дозволяє створювати ціни на парковки, фіксовані ціни та динамічні (наприклад ціна в залежності від часу доби)
Observer: Надсилання підписникам інформації щодо зміни ціни тарифу.
Command: Дозволяє виконувати операції над тарифами – зміна тарифу, знижки, скасування, тощо.
Є користувачі системи, люди які підписуються в Observer, це люди які паркують свій автомобіль на парковочне місце та за допомогою Observer отримують сповіщення про тарифи та про те, скільки потрібно заплатити за час того, скільки автомобіль стояв на парко-місці (фіксовані та динамічні, отримують сповіщення про тарифи), за допомогою Strategy ці тарифи можуть змінюватись, Command дозволяє виконувати операції над змінами тарифу – скасовує попередні зміни, змінює тарифи, тощо.

Нам необхідно розробити можливість автоматизувати такі дії як підйом та спуск шлагбауму, включити та виключити освітлення. Необхідно розробити можливість, щоб вони могли працювати одночасно, відмінятись одночасно.
Окрім цього необхідно розробити різні види оплат, реалізація цього має бути шаблонною, з можливістью змінити конкретні її елементи.
Macro Command: Використовується для створення складних дій з декількох команд, які можуть виконуватися та відмінятися разом. Приклад - використовується для автоматизації відкриття шлагбаума і увімкнення світлофора під час заїзду на парковку.
Template Method - Використовується для визначення загального алгоритму з можливістю зміни окремих його частин у підкласах. Приклад - Використовується для створення стандартного процесу оплати з можливістю зміни деталей залежно від способу оплати.
