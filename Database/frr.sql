-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.7.19-log - MySQL Community Server (GPL)
-- Server OS:                    Win32
-- HeidiSQL Version:             9.4.0.5125
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for db_h_clinic
CREATE DATABASE IF NOT EXISTS `db_h_clinic` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `db_h_clinic`;

-- Dumping structure for table db_h_clinic.tbl_clients
CREATE TABLE IF NOT EXISTS `tbl_clients` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `phone` varchar(15) NOT NULL,
  `job` varchar(255) DEFAULT NULL,
  `address` text,
  `birthday` datetime NOT NULL,
  `diabetesType` int(11) NOT NULL DEFAULT '0',
  `is_active` tinyint(1) NOT NULL DEFAULT '1',
  `is_male` tinyint(1) NOT NULL DEFAULT '1',
  `creation` datetime DEFAULT CURRENT_TIMESTAMP,
  `user_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name` (`name`),
  UNIQUE KEY `phone` (`phone`),
  KEY `FK_tbl_clients_tbl_users` (`user_id`),
  CONSTRAINT `FK_tbl_clients_tbl_users` FOREIGN KEY (`user_id`) REFERENCES `tbl_users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Dumping data for table db_h_clinic.tbl_clients: ~2 rows (approximately)
/*!40000 ALTER TABLE `tbl_clients` DISABLE KEYS */;
INSERT IGNORE INTO `tbl_clients` (`id`, `name`, `phone`, `job`, `address`, `birthday`, `diabetesType`, `is_active`, `is_male`, `creation`, `user_id`) VALUES
	(1, 'haitham', '07703867142', 'Programmer', 'Qazi Mohammed', '1994-06-04 00:00:00', 0, 1, 1, '2017-12-12 12:41:56', 1),
	(3, 'Obyda', '07703335333', 'Designer', '123', '1995-07-20 00:00:00', 0, 1, 1, '2017-12-12 12:42:28', 1);
/*!40000 ALTER TABLE `tbl_clients` ENABLE KEYS */;

-- Dumping structure for table db_h_clinic.tbl_dates
CREATE TABLE IF NOT EXISTS `tbl_dates` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `datetime` datetime NOT NULL,
  `is_reported` tinyint(1) NOT NULL DEFAULT '0',
  `client_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `creation` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `FK_tbl_dates_tbl_clients` (`client_id`),
  KEY `FK_tbl_dates_tbl_users` (`user_id`),
  CONSTRAINT `FK_tbl_dates_tbl_clients` FOREIGN KEY (`client_id`) REFERENCES `tbl_clients` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_tbl_dates_tbl_users` FOREIGN KEY (`user_id`) REFERENCES `tbl_users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table db_h_clinic.tbl_dates: ~0 rows (approximately)
/*!40000 ALTER TABLE `tbl_dates` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_dates` ENABLE KEYS */;

-- Dumping structure for table db_h_clinic.tbl_sessions
CREATE TABLE IF NOT EXISTS `tbl_sessions` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `client_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `card_number` int(11) NOT NULL DEFAULT '0' COMMENT 'number for card that print on small paper like ticket',
  `creation` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `weight` int(11) NOT NULL DEFAULT '0',
  `HbAlC` double NOT NULL DEFAULT '0',
  `RBS` double NOT NULL DEFAULT '0',
  `PR` double NOT NULL DEFAULT '0',
  `BP` varchar(50) NOT NULL DEFAULT '0',
  `note` text,
  PRIMARY KEY (`id`),
  KEY `FK_tbl_sessions_tbl_clients` (`client_id`),
  KEY `FK_tbl_sessions_tbl_users` (`user_id`),
  CONSTRAINT `FK_tbl_sessions_tbl_clients` FOREIGN KEY (`client_id`) REFERENCES `tbl_clients` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_tbl_sessions_tbl_users` FOREIGN KEY (`user_id`) REFERENCES `tbl_users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8;

-- Dumping data for table db_h_clinic.tbl_sessions: ~2 rows (approximately)
/*!40000 ALTER TABLE `tbl_sessions` DISABLE KEYS */;
INSERT IGNORE INTO `tbl_sessions` (`id`, `client_id`, `user_id`, `card_number`, `creation`, `weight`, `HbAlC`, `RBS`, `PR`, `BP`, `note`) VALUES
	(32, 1, 1, 1, '2017-12-12 12:51:58', 0, 0, 0, 0, '0', NULL),
	(33, 3, 1, 2, '2017-12-12 12:52:08', 1, 1, 1, 1, '1', '1'),
	(34, 1, 1, 3, '2017-12-12 13:12:06', 22, 22, 22, 22, '22', '22');
/*!40000 ALTER TABLE `tbl_sessions` ENABLE KEYS */;

-- Dumping structure for table db_h_clinic.tbl_sessions_documents
CREATE TABLE IF NOT EXISTS `tbl_sessions_documents` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `session_id` int(11) NOT NULL,
  `description` text,
  `document` mediumblob NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_tbl_sessions_documents_tbl_sessions` (`session_id`),
  CONSTRAINT `FK_tbl_sessions_documents_tbl_sessions` FOREIGN KEY (`session_id`) REFERENCES `tbl_sessions` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table db_h_clinic.tbl_sessions_documents: ~1 rows (approximately)
/*!40000 ALTER TABLE `tbl_sessions_documents` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_sessions_documents` ENABLE KEYS */;

-- Dumping structure for table db_h_clinic.tbl_users
CREATE TABLE IF NOT EXISTS `tbl_users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `password` varchar(32) NOT NULL,
  `email` varchar(50) DEFAULT NULL,
  `phone` varchar(32) DEFAULT NULL,
  `type` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- Dumping data for table db_h_clinic.tbl_users: ~4 rows (approximately)
/*!40000 ALTER TABLE `tbl_users` DISABLE KEYS */;
INSERT IGNORE INTO `tbl_users` (`id`, `name`, `password`, `email`, `phone`, `type`) VALUES
	(1, 'admin', '21232f297a57a5a743894a0e4a801fc3', 'hiethem.hiethem@yahoo.com', '009647703867142', 1),
	(3, 'reception', '21232f297a57a5a743894a0e4a801fc3', 'hiethem.hiethem@yahoo.com', '009647703867142', 4),
	(4, 'assistant', '21232f297a57a5a743894a0e4a801fc3', 'hiethem.hiethem@yahoo.com', '009647703867142', 2),
	(5, 'doctor', '21232f297a57a5a743894a0e4a801fc3', 'hiethem.hiethem@yahoo.com', '009647703867142', 3);
/*!40000 ALTER TABLE `tbl_users` ENABLE KEYS */;

-- Dumping structure for function db_h_clinic.getLastSessionDatetime
DELIMITER //
CREATE DEFINER=`root`@`%` FUNCTION `getLastSessionDatetime`(
	`clientId` INT,
	`sessionId` INT


) RETURNS datetime
BEGIN
	return ifnull((
		select
			tbl_sessions.creation
		from
			tbl_sessions
		where
			tbl_sessions.client_id = clientId and
			tbl_sessions.id < sessionId
		order by
			tbl_sessions.creation desc
		limit 0,1
	),'2017-01-01 00:00:00');
END//
DELIMITER ;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
