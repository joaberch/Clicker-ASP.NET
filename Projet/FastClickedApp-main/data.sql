GRANT ALL PRIVILEGES ON *.* TO 'root'@'192.168.0.2' IDENTIFIED BY 'root';

CREATE DATABASE IF NOT EXISTS db_clickGame;

USE db_clickGame;

CREATE TABLE `t_joueur` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`username` VARCHAR(255) CHARACTER SET utf8 COLLATE utf8_general_ci,
	`nbrRestart` INT,
	PRIMARY KEY (`id`)
);