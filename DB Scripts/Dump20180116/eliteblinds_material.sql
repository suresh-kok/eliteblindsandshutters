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
-- Table structure for table `material`
--

DROP TABLE IF EXISTS `material`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `material` (
  `MaterialID` int(11) NOT NULL AUTO_INCREMENT,
  `MaterialDesc` varchar(1000) DEFAULT NULL,
  `For` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`MaterialID`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `material`
--

LOCK TABLES `material` WRITE;
/*!40000 ALTER TABLE `material` DISABLE KEYS */;
INSERT INTO `material` VALUES (1,'Blockout(colorfull world)','Roller'),(2,'Sunscreen(deluxview)','Roller'),(3,'Blockout(opera)','Roller'),(4,'Translucent(opera)','Roller'),(5,'Blockout 89mm','OVertical'),(6,'Blockout 127mm','OVertical'),(7,'PVC 89mm','OVertical'),(8,'Aluminium','OVenetian'),(9,'Fauxwood','OVenetian'),(10,'Basswood','OVenetian'),(11,'Dim Out','OHoneycomb'),(12,'Blockout','OHoneycomb'),(13,'lockout','ORoman'),(14,'Blockout','ORoller'),(15,'Sunscreen','ORoller'),(16,'Prestige','ORoller'),(17,'Dim Out Opera','OZebra'),(18,'Dim Out Danube','OZebra'),(19,'Blackout Louvre','OZebra'),(20,'Basswood 63mm','Valance'),(21,'Raycowood 82.5mm','Valance'),(22,'Cedar 63mm','Valance'),(23,'Basswood 50mm','BottomRail'),(24,'Raycowood 45mm','BottomRail'),(25,'Cedar 45mm','BottomRail'),(26,'Foxwood 50cm H Shaped','BottomRail'),(27,'Foxwood 195cm H Shaped','BottomRail');
/*!40000 ALTER TABLE `material` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-01-16 11:39:52
