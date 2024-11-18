-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1
-- Время создания: Окт 30 2024 г., 08:45
-- Версия сервера: 10.4.32-MariaDB
-- Версия PHP: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `dpc`
--

-- --------------------------------------------------------

--
-- Структура таблицы `bilet`
--

CREATE TABLE `bilet` (
  `bilet_id` int(11) NOT NULL,
  `nume` text NOT NULL,
  `prenume` text NOT NULL,
  `data_procurarii` date NOT NULL,
  `eveniment_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_romanian_ci;

--
-- Дамп данных таблицы `bilet`
--

INSERT INTO `bilet` (`bilet_id`, `nume`, `prenume`, `data_procurarii`, `eveniment_id`) VALUES
(6, 'Ion', 'Popescu', '2024-10-20', 1),
(7, 'Maria', 'Ionescu', '2024-10-21', 1),
(8, 'Andrei', 'Georgescu', '2024-10-22', 1),
(9, 'Elena', 'Matei', '2024-10-23', 1),
(10, 'Cristian', 'Radu', '2024-10-24', 1);

-- --------------------------------------------------------

--
-- Структура таблицы `eveniment`
--

CREATE TABLE `eveniment` (
  `eveniment_id` int(11) NOT NULL,
  `denumire` text NOT NULL,
  `disciplina` text NOT NULL,
  `data_inceput` date NOT NULL,
  `locatia` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_romanian_ci;

--
-- Дамп данных таблицы `eveniment`
--

INSERT INTO `eveniment` (`eveniment_id`, `denumire`, `disciplina`, `data_inceput`, `locatia`) VALUES
(1, 'PGL Wallachia', '', '2005-10-07', 'București'),
(3, 'morojna', 'ebati', '2024-10-30', 'sss');

-- --------------------------------------------------------

--
-- Структура таблицы `organizatie`
--

CREATE TABLE `organizatie` (
  `organizatie_id` int(255) NOT NULL,
  `denumire` text NOT NULL,
  `data_crearii` date NOT NULL,
  `originea` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_romanian_ci;

--
-- Дамп данных таблицы `organizatie`
--

INSERT INTO `organizatie` (`organizatie_id`, `denumire`, `data_crearii`, `originea`) VALUES
(1, 'Team Secret', '2014-08-21', 'Europa'),
(2, 'Evil Geniuses', '1999-11-24', 'America de Nord'),
(3, 'PSG.LGD', '2009-05-20', 'China'),
(4, 'OG', '2015-10-31', 'Europa'),
(5, 'Tundra Esports', '2019-09-03', 'Europa'),
(7, 'Stas Esport', '2024-10-30', 'Briceni');

-- --------------------------------------------------------

--
-- Структура таблицы `roster`
--

CREATE TABLE `roster` (
  `roster_id` int(11) NOT NULL,
  `organizatie_id` int(11) NOT NULL,
  `disciplina` text NOT NULL,
  `data_formare` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_romanian_ci;

--
-- Дамп данных таблицы `roster`
--

INSERT INTO `roster` (`roster_id`, `organizatie_id`, `disciplina`, `data_formare`) VALUES
(1, 1, 'Dota 2', '2014-08-21'),
(2, 2, 'Dota 2', '2023-01-01'),
(3, 3, 'Dota 2', '2009-05-20'),
(4, 4, 'Dota 2', '2018-08-25'),
(5, 5, 'Dota 2', '2021-09-03'),
(9, 7, 'Ghensin', '2024-10-29');

-- --------------------------------------------------------

--
-- Структура таблицы `roster_eveniment`
--

CREATE TABLE `roster_eveniment` (
  `roster_eveniment_id` int(11) NOT NULL,
  `eveniment_id` int(11) NOT NULL,
  `roster_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_romanian_ci;

--
-- Дамп данных таблицы `roster_eveniment`
--

INSERT INTO `roster_eveniment` (`roster_eveniment_id`, `eveniment_id`, `roster_id`) VALUES
(1, 1, 1),
(2, 1, 2),
(3, 1, 3),
(4, 1, 4),
(5, 1, 5),
(7, 3, 9);

-- --------------------------------------------------------

--
-- Структура таблицы `roster_sportiv`
--

CREATE TABLE `roster_sportiv` (
  `roster_sportiv_id` int(11) NOT NULL,
  `roster_id` int(11) NOT NULL,
  `sportiv_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_romanian_ci;

--
-- Дамп данных таблицы `roster_sportiv`
--

INSERT INTO `roster_sportiv` (`roster_sportiv_id`, `roster_id`, `sportiv_id`) VALUES
(1, 1, 1),
(2, 1, 2),
(3, 1, 3),
(4, 1, 4),
(5, 1, 5),
(6, 2, 6),
(7, 2, 7),
(8, 2, 8),
(9, 2, 9),
(10, 2, 10),
(11, 3, 11),
(12, 3, 12),
(13, 3, 13),
(14, 3, 14),
(15, 3, 15),
(16, 4, 16),
(17, 4, 17),
(18, 4, 18),
(19, 4, 19),
(20, 4, 20),
(21, 5, 21),
(22, 5, 22),
(23, 5, 23),
(24, 5, 24),
(25, 5, 25),
(28, 9, 28);

-- --------------------------------------------------------

--
-- Структура таблицы `sportiv`
--

CREATE TABLE `sportiv` (
  `sportiv_id` int(11) NOT NULL,
  `nume` text NOT NULL,
  `prenume` text NOT NULL,
  `data_nasterii` date NOT NULL,
  `organizatie_id` int(11) NOT NULL,
  `porecla` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_romanian_ci;

--
-- Дамп данных таблицы `sportiv`
--

INSERT INTO `sportiv` (`sportiv_id`, `nume`, `prenume`, `data_nasterii`, `organizatie_id`, `porecla`) VALUES
(1, 'Clement', 'Ivanov', '1990-03-06', 1, 'Puppey'),
(2, 'Michal', 'Jankowski', '1995-06-04', 1, 'Nisha'),
(3, 'William', 'Morales', '1997-01-09', 1, 'LeBron'),
(4, 'Kuro', 'Salehi Takhasomi', '1996-01-28', 1, 'KuroKy'),
(5, 'Aydin', 'Sahin', '1998-09-15', 1, 'iXmike'),
(6, 'Artour', 'Babaev', '1996-07-01', 2, 'Arteezy'),
(7, 'Andreas', 'Nielsen', '1991-07-06', 2, 'Cr1t-'),
(8, 'Zachary', 'Pauley', '1999-10-20', 2, 'S4'),
(9, 'Sumail', 'Hassan', '1999-09-13', 2, 'SumaiL'),
(10, 'Jason', 'Kwan', '1997-12-01', 2, 'Kpii'),
(11, 'Wang', 'Chunyu', '1997-04-07', 3, 'Ame'),
(12, 'Xu', 'Linsen', '1994-05-14', 3, 'fy'),
(13, 'Yang', 'Zheng', '1995-09-01', 3, 'Chalice'),
(14, 'Zhang', 'Yongzheng', '1999-06-30', 3, 'NothingToSay'),
(15, 'Li', 'Jingyu', '1999-05-01', 3, 'Faith_bian'),
(16, 'Johan', 'Sundstein', '1993-10-08', 4, 'N0tail'),
(17, 'Tal', 'Aizik', '1995-04-02', 4, 'Fly'),
(18, 'Anathan', 'Phatthana', '1996-04-02', 4, 'ana'),
(19, 'Sebastian', 'Debs', '1996-07-14', 4, 'Ceb'),
(20, 'Mikael', 'Kousa', '1998-02-25', 4, 'Mika'),
(21, 'Leon', 'Kirilin', '1996-12-13', 5, 'Nine'),
(22, 'Nikita', 'Sukharev', '2002-04-29', 5, '33'),
(23, 'Tobias', 'Søren', '1997-10-04', 5, 'SyndereN'),
(24, 'Johan', 'Lindqvist', '1999-08-12', 5, 'fng'),
(25, 'Filip', 'Wigert', '1999-05-12', 5, 'Moo'),
(28, 'Stas', 'Caragheur', '2005-12-23', 7, 'stasupernatural');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `bilet`
--
ALTER TABLE `bilet`
  ADD PRIMARY KEY (`bilet_id`),
  ADD KEY `eveniment_id` (`eveniment_id`);

--
-- Индексы таблицы `eveniment`
--
ALTER TABLE `eveniment`
  ADD PRIMARY KEY (`eveniment_id`);

--
-- Индексы таблицы `organizatie`
--
ALTER TABLE `organizatie`
  ADD PRIMARY KEY (`organizatie_id`);

--
-- Индексы таблицы `roster`
--
ALTER TABLE `roster`
  ADD PRIMARY KEY (`roster_id`),
  ADD KEY `organizatie_id` (`organizatie_id`);

--
-- Индексы таблицы `roster_eveniment`
--
ALTER TABLE `roster_eveniment`
  ADD PRIMARY KEY (`roster_eveniment_id`),
  ADD KEY `eveniment_id` (`eveniment_id`),
  ADD KEY `roster_id` (`roster_id`);

--
-- Индексы таблицы `roster_sportiv`
--
ALTER TABLE `roster_sportiv`
  ADD PRIMARY KEY (`roster_sportiv_id`),
  ADD KEY `roster_id` (`roster_id`),
  ADD KEY `sportiv_id` (`sportiv_id`);

--
-- Индексы таблицы `sportiv`
--
ALTER TABLE `sportiv`
  ADD PRIMARY KEY (`sportiv_id`),
  ADD KEY `organizatie_id` (`organizatie_id`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `bilet`
--
ALTER TABLE `bilet`
  MODIFY `bilet_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT для таблицы `eveniment`
--
ALTER TABLE `eveniment`
  MODIFY `eveniment_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT для таблицы `organizatie`
--
ALTER TABLE `organizatie`
  MODIFY `organizatie_id` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT для таблицы `roster`
--
ALTER TABLE `roster`
  MODIFY `roster_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT для таблицы `roster_eveniment`
--
ALTER TABLE `roster_eveniment`
  MODIFY `roster_eveniment_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT для таблицы `roster_sportiv`
--
ALTER TABLE `roster_sportiv`
  MODIFY `roster_sportiv_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- AUTO_INCREMENT для таблицы `sportiv`
--
ALTER TABLE `sportiv`
  MODIFY `sportiv_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `bilet`
--
ALTER TABLE `bilet`
  ADD CONSTRAINT `bilet_ibfk_1` FOREIGN KEY (`eveniment_id`) REFERENCES `eveniment` (`eveniment_id`);

--
-- Ограничения внешнего ключа таблицы `roster`
--
ALTER TABLE `roster`
  ADD CONSTRAINT `roster_ibfk_1` FOREIGN KEY (`organizatie_id`) REFERENCES `organizatie` (`organizatie_id`);

--
-- Ограничения внешнего ключа таблицы `roster_eveniment`
--
ALTER TABLE `roster_eveniment`
  ADD CONSTRAINT `roster_eveniment_ibfk_1` FOREIGN KEY (`eveniment_id`) REFERENCES `eveniment` (`eveniment_id`),
  ADD CONSTRAINT `roster_eveniment_ibfk_2` FOREIGN KEY (`roster_id`) REFERENCES `roster` (`roster_id`);

--
-- Ограничения внешнего ключа таблицы `roster_sportiv`
--
ALTER TABLE `roster_sportiv`
  ADD CONSTRAINT `roster_sportiv_ibfk_1` FOREIGN KEY (`roster_id`) REFERENCES `roster` (`roster_id`),
  ADD CONSTRAINT `roster_sportiv_ibfk_2` FOREIGN KEY (`sportiv_id`) REFERENCES `sportiv` (`sportiv_id`);

--
-- Ограничения внешнего ключа таблицы `sportiv`
--
ALTER TABLE `sportiv`
  ADD CONSTRAINT `sportiv_ibfk_1` FOREIGN KEY (`organizatie_id`) REFERENCES `organizatie` (`organizatie_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
