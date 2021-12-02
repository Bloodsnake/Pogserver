-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 25. Nov 2021 um 15:32
-- Server-Version: 10.4.21-MariaDB
-- PHP-Version: 8.0.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `messwerte`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `messwerte`
--

CREATE TABLE `messwerte` (
  `Zahl` int(11) NOT NULL,
  `Zeitpunk` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `SensorID` int(11) NOT NULL,
  `MessungsID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_german2_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `physikalische größe`
--

CREATE TABLE `physikalische größe` (
  `Name` text COLLATE utf32_german2_ci NOT NULL,
  `Einheit` text COLLATE utf32_german2_ci NOT NULL,
  `Formelzeichen` varchar(1) COLLATE utf32_german2_ci NOT NULL,
  `MessungsID` int(12) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_german2_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `sensoren`
--

CREATE TABLE `sensoren` (
  `Bezeichnung` text COLLATE utf32_german2_ci NOT NULL,
  `Seriennummer` int(11) NOT NULL,
  `Hersteller` text COLLATE utf32_german2_ci NOT NULL,
  `Hersterllernummer` int(11) NOT NULL,
  `StandortID` int(11) NOT NULL,
  `SensorID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_german2_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `standort`
--

CREATE TABLE `standort` (
  `Bezeichnung` text COLLATE utf32_german2_ci NOT NULL,
  `Standort` point NOT NULL,
  `StandortID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_german2_ci;

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `messwerte`
--
ALTER TABLE `messwerte`
  ADD PRIMARY KEY (`Zeitpunk`),
  ADD KEY `Zeitpunk` (`Zeitpunk`),
  ADD KEY `SensorID` (`SensorID`),
  ADD KEY `MessungsID` (`MessungsID`);

--
-- Indizes für die Tabelle `physikalische größe`
--
ALTER TABLE `physikalische größe`
  ADD PRIMARY KEY (`MessungsID`),
  ADD KEY `UnitID` (`MessungsID`);

--
-- Indizes für die Tabelle `sensoren`
--
ALTER TABLE `sensoren`
  ADD PRIMARY KEY (`StandortID`),
  ADD KEY `StandortID` (`StandortID`),
  ADD KEY `SensorID` (`SensorID`);

--
-- Indizes für die Tabelle `standort`
--
ALTER TABLE `standort`
  ADD PRIMARY KEY (`StandortID`),
  ADD KEY `StandortID` (`StandortID`);

--
-- Constraints der exportierten Tabellen
--

--
-- Constraints der Tabelle `physikalische größe`
--
ALTER TABLE `physikalische größe`
  ADD CONSTRAINT `physikalische größe_ibfk_1` FOREIGN KEY (`MessungsID`) REFERENCES `messwerte` (`MessungsID`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints der Tabelle `sensoren`
--
ALTER TABLE `sensoren`
  ADD CONSTRAINT `sensoren_ibfk_1` FOREIGN KEY (`SensorID`) REFERENCES `messwerte` (`SensorID`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints der Tabelle `standort`
--
ALTER TABLE `standort`
  ADD CONSTRAINT `standort_ibfk_1` FOREIGN KEY (`StandortID`) REFERENCES `sensoren` (`StandortID`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
