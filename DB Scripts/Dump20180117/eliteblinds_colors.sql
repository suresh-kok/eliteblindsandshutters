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
-- Table structure for table `colors`
--

DROP TABLE IF EXISTS `colors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `colors` (
  `ColorsID` int(11) NOT NULL AUTO_INCREMENT,
  `ColorsDesc` varchar(1000) DEFAULT NULL,
  `For` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`ColorsID`)
) ENGINE=InnoDB AUTO_INCREMENT=54 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `colors`
--

LOCK TABLES `colors` WRITE;
/*!40000 ALTER TABLE `colors` DISABLE KEYS */;
INSERT INTO `colors` VALUES (1,'Alaskan Snow(Ice)','Roller'),(2,'California Cream(Syllabub)','Roller'),(3,'Santa Monica Seashell(Moon)','Roller'),(4,'Caribbean Cream(Loft)','Roller'),(5,'Barbados Beige(Whisper)','Roller'),(6,'Hawaii Sand(Cloud)','Roller'),(7,'Dubai Dune(Dune)','Roller'),(8,'Tuscan Taupe(Stone)','Roller'),(9,'Alaskan Snow','OVertical'),(10,'Hawaii Sand','OVertical'),(11,'Sydney Sand','OVertical'),(12,'California Cream','OVertical'),(13,'Carribean Cream','OVertical'),(14,'Dubai Dune','OVertical'),(15,'Tuscan Taupe','OVertical'),(16,'Milan Night','OVertical'),(17,'White','OVenetian'),(18,'NutMeg','OVenetian'),(19,'Mahogany','OVenetian'),(20,'Walnut','OVenetian'),(21,'White','OHoneycomb'),(22,'Rope Swing','OHoneycomb'),(23,'Ancient Artifact','OHoneycomb'),(24,'Grey','OHoneycomb'),(25,'Dark Coffee','OHoneycomb'),(26,'White','ORoman'),(27,'Lemon Chanbers','ORoman'),(28,'Nw England Stones','ORoman'),(29,'Golden Bronze','ORoman'),(30,'Naval Gery','ORoman'),(31,'Natural','ORoller'),(32,'Chalk','ORoller'),(33,'Perfect Praline','OZebra'),(34,'Praline White','OZebra'),(35,'HWAII SAND (CLOUD,IVORY)','Fabric'),(36,'CARRIBEAN CREAM (LOFT,BISCUIT)','Fabric'),(37,'ICE (WHITE,ALASKAN SNOW)','Fabric'),(38,'BARBADOSE BEIGE ( WHISPER,WHEAT)','Fabric'),(39,'CORNSILK','Fabric'),(40,'DUBAI DUNE (DUNE,LATTE)','Fabric'),(41,'CALIFORNIA CREAM (SYLLABUB,BARLEY)','Fabric'),(42,'CHALK','Fabric'),(43,'ORIGINAL (NATURAL)','Fabric'),(44,'White','Valance'),(45,'Nutmeg','Valance'),(46,'Arctic Snow','Valance'),(47,'Ivory','Valance'),(48,'Medium','Valance'),(49,'White','BottomRail'),(50,'Nutmeg','BottomRail'),(51,'Arctic Snow','BottomRail'),(52,'Ivory','BottomRail'),(53,'Medium','BottomRail');
/*!40000 ALTER TABLE `colors` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-01-17 17:07:17
