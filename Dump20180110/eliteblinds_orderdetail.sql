CREATE DATABASE  IF NOT EXISTS `eliteblinds` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `eliteblinds`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: eliteblinds
-- ------------------------------------------------------
-- Server version	5.7.20-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `orderdetail`
--

DROP TABLE IF EXISTS `orderdetail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `orderdetail` (
  `OrderDetailID` int(11) NOT NULL AUTO_INCREMENT,
  `OrderID` int(11) DEFAULT NULL,
  `Width` double DEFAULT NULL,
  `Height` double DEFAULT NULL,
  `SplPelmetWidth` double DEFAULT NULL,
  `WidthMadeBy` varchar(1000) DEFAULT NULL,
  `HeightMadeBy` varchar(1000) DEFAULT NULL,
  `QualityCheckedBy` varchar(1000) DEFAULT NULL,
  `SlatStyleID` int(11) DEFAULT NULL,
  `CordStyleID` int(11) DEFAULT NULL,
  `ReturnRequired` tinyint(4) DEFAULT NULL,
  `MountType` tinyint(4) DEFAULT NULL,
  `SquareMeter` double DEFAULT NULL,
  `ControlID` int(11) DEFAULT NULL,
  `ControlStyle` int(11) DEFAULT NULL,
  `OpeningStyle` int(11) DEFAULT NULL,
  `PelmetStyle` int(11) DEFAULT NULL,
  `ColorID` int(11) DEFAULT NULL,
  `MaterialID` int(11) DEFAULT NULL,
  `Roll` tinyint(4) DEFAULT NULL,
  `ReadyMadeSize` double DEFAULT NULL,
  PRIMARY KEY (`OrderDetailID`),
  KEY `fk_OrderDetail_OrderID_idx` (`OrderID`),
  KEY `fk_OrderDetail_SlatStyleID_idx` (`SlatStyleID`),
  KEY `fk_OrderDetail_CordStyleID_idx` (`CordStyleID`),
  KEY `fk_OrderDetail_ControlID_idx` (`ControlID`),
  KEY `fk_OrderDetail_ColorID_idx` (`ColorID`),
  KEY `fk_OrderDetail_MaterialID_idx` (`MaterialID`),
  CONSTRAINT `fk_OrderDetail_ColorID` FOREIGN KEY (`ColorID`) REFERENCES `colors` (`ColorsID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_OrderDetail_ControlID` FOREIGN KEY (`ControlID`) REFERENCES `control` (`ControlID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_OrderDetail_CordStyleID` FOREIGN KEY (`CordStyleID`) REFERENCES `cordstyle` (`CordStyleID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_OrderDetail_MaterialID` FOREIGN KEY (`MaterialID`) REFERENCES `material` (`MaterialID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_OrderDetail_OrderID` FOREIGN KEY (`OrderID`) REFERENCES `order` (`OrderID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_OrderDetail_SlatStyleID` FOREIGN KEY (`SlatStyleID`) REFERENCES `slatstyle` (`SlatStyleID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orderdetail`
--

LOCK TABLES `orderdetail` WRITE;
/*!40000 ALTER TABLE `orderdetail` DISABLE KEYS */;
/*!40000 ALTER TABLE `orderdetail` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-01-10 10:41:41
