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
-- Table structure for table `rollerblindtype`
--

DROP TABLE IF EXISTS `rollerblindtype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `rollerblindtype` (
  `RollerBlindTypeID` int(11) NOT NULL AUTO_INCREMENT,
  `Description` varchar(5000) DEFAULT NULL,
  `Profile` varchar(5000) DEFAULT NULL,
  `RollerColor` varchar(1000) DEFAULT NULL,
  `DLXCODE` varchar(1000) DEFAULT NULL,
  `PCSCTN` varchar(1000) DEFAULT NULL,
  `MOQ` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`RollerBlindTypeID`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rollerblindtype`
--

LOCK TABLES `rollerblindtype` WRITE;
/*!40000 ALTER TABLE `rollerblindtype` DISABLE KEYS */;
INSERT INTO `rollerblindtype` VALUES (1,'38mm Alum.Rod T 1.2mm  0.447kg/m Mill Finished 5.8m/pcs',NULL,'MILL FINISEHD','7000254','20PCS/CTN','500KG'),(2,'Alum Bottom Rail 0.222Kg/m 5.8m/pcs',NULL,'SLIVERY MATT','7000257','20PCS/CTN','500KG'),(3,'38mm Long Clutch (2 pics spring)',NULL,'White/Ivory',NULL,'80SET/CTN','10000set'),(4,'38mm Long Clutch (3 pics spring)',NULL,'White/Ivory',NULL,'80SET/CTN','10000set'),(5,'38mm Short Clutch',NULL,'White/Ivory',NULL,'100SET/CTN','10000set'),(6,'Double bracket for Long /Short Clutch ((both control :left control   /right control))',NULL,'White/Ivory',NULL,'25SET/CTN',NULL),(7,'NO1 Clutch (deceleration ratio)',NULL,'White/Black',NULL,'80SET/CTN',NULL),(8,'NO2 Clutch (no deceleration ratio)',NULL,'White/Black',NULL,'80SET/CTN',NULL),(9,'Center Bracket with both end Clutch',NULL,'White/Black',NULL,'25SET/CTN',NULL),(10,'Double Bracket (Without clutch ,only steel bracket) (both control :left control   /right control)',NULL,'White/Black',NULL,'25SET/CTN',NULL),(11,'43mm Adjust Tube For 38mm Clutch',NULL,NULL,NULL,'100PCS/CTN',NULL),(12,'End Cap for Alum Bottom Rail for Code 175',NULL,'GREY','7000260','5000PAR/CTN','20000set'),(13,'Chain (Steel plated nickel)',NULL,'SILVERY','7000263','500M/CTN','10000M'),(14,'Chain (Stainless Steel)',NULL,'SILVERY','7000263','500M/CTN','10000M'),(15,'Chain connector Steel plated nickel',NULL,'SILVERY','P1-CC01-SN','20000M/CTN','20000PCS'),(16,'Chain Stopper',NULL,'CLEAR','P1-CS01-C','20000M/CTN','10000PCS'),(17,'Chain&Cord tie down clip',NULL,'CLEAR','P1-CSD01-C','2500M/CTN','10000PCS'),(18,'HOLD DOWN STRIP 15MM',NULL,'White','P1-HDS01','500M/CTN','10000M'),(19,'warning stickers',NULL,NULL,NULL,NULL,NULL),(20,'warning tag',NULL,NULL,NULL,NULL,NULL),(21,'child safty kit',NULL,NULL,NULL,NULL,NULL),(22,'double side tape 12mm',NULL,NULL,NULL,NULL,NULL),(23,'double side tape 20mm',NULL,NULL,NULL,NULL,NULL),(24,'hold down strip with Tape',NULL,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `rollerblindtype` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-01-17 17:07:16
