-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema campingdata
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema campingdata
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `campingdata` DEFAULT CHARACTER SET latin1 ;
USE `campingdata` ;

-- -----------------------------------------------------
-- Table `campingdata`.`place`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `campingdata`.`place` (
  `placeID` INT(11) NOT NULL,
  `surface` INT(11) NOT NULL,
  `price` INT(11) NOT NULL,
  `water` BIT(1) NOT NULL DEFAULT b'1',
  `electric` BIT(1) NOT NULL DEFAULT b'1',
  `isBlocked` BIT(1) NULL DEFAULT b'0',
  PRIMARY KEY (`placeID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `campingdata`.`reservations`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `campingdata`.`reservations` (
  `reservationID` INT(11) NOT NULL AUTO_INCREMENT,
  `startDate` DATE NOT NULL,
  `endDate` DATE NOT NULL,
  `personAmount` INT(11) NOT NULL,
  `userID` INT(11) NULL DEFAULT NULL,
  `placeID` INT(11) NULL DEFAULT NULL,
  `isBlock` BIT(1) NULL DEFAULT NULL,
  PRIMARY KEY (`reservationID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1
COMMENT = '	';


-- -----------------------------------------------------
-- Table `campingdata`.`user`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `campingdata`.`user` (
  `userID` INT(11) NOT NULL,
  `email` VARCHAR(255) NOT NULL,
  `Phonenumber` VARCHAR(15) NOT NULL,
  `wachtwoord` BLOB NOT NULL,
  `isAdmin` BIT(1) NOT NULL,
  PRIMARY KEY (`userID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
