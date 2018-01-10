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
-- Table structure for table `order`
--

DROP TABLE IF EXISTS `order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `order` (
  `OrderID` int(11) NOT NULL AUTO_INCREMENT,
  `OrderNumber` varchar(1000) DEFAULT NULL,
  `CustomerID` int(11) DEFAULT NULL,
  `IsNew` tinyint(4) DEFAULT NULL,
  `Fault` int(11) DEFAULT NULL,
  `Evidence` tinyint(4) DEFAULT NULL,
  `Company` varchar(1000) DEFAULT NULL,
  `Reference` varchar(1000) DEFAULT NULL,
  `OrderType` int(11) DEFAULT NULL,
  `OrderStatus` varchar(1000) DEFAULT NULL,
  `OrderDate` datetime DEFAULT NULL,
  `NumbOfBlinds` int(11) DEFAULT NULL,
  `ConsignNoteNum` varchar(1000) DEFAULT NULL,
  `CompleteDate` datetime DEFAULT NULL,
  `DeliveryDate` datetime DEFAULT NULL,
  `DepartureDate` datetime DEFAULT NULL,
  `ArrivalDate` datetime DEFAULT NULL,
  `OrderM2` double DEFAULT NULL,
  PRIMARY KEY (`OrderID`),
  KEY `fk_Order_CustomerID_idx` (`CustomerID`),
  CONSTRAINT `fk_Order_CustomerID` FOREIGN KEY (`CustomerID`) REFERENCES `customer` (`customerID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `order`
--

LOCK TABLES `order` WRITE;
/*!40000 ALTER TABLE `order` DISABLE KEYS */;
/*!40000 ALTER TABLE `order` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-01-10 10:41:43
