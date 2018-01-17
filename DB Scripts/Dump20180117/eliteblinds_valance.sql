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
-- Table structure for table `valance`
--

DROP TABLE IF EXISTS `valance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `valance` (
  `ValanceID` int(11) NOT NULL AUTO_INCREMENT,
  `UtilityOrderID` int(11) DEFAULT NULL,
  `MaterialID` int(11) DEFAULT NULL,
  `ColorID` int(11) DEFAULT NULL,
  `Size` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`ValanceID`),
  KEY `fk_Valance_UtilityOrderID_idx` (`UtilityOrderID`),
  KEY `fk_Valance_MaterialID_idx` (`MaterialID`),
  KEY `fk_Valance_ColorID_idx` (`ColorID`),
  CONSTRAINT `fk_Valance_ColorID` FOREIGN KEY (`ColorID`) REFERENCES `colors` (`ColorsID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Valance_MaterialID` FOREIGN KEY (`MaterialID`) REFERENCES `material` (`MaterialID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Valance_UtilityOrderID` FOREIGN KEY (`UtilityOrderID`) REFERENCES `utilityorder` (`UtilityOrderID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `valance`
--

LOCK TABLES `valance` WRITE;
/*!40000 ALTER TABLE `valance` DISABLE KEYS */;
/*!40000 ALTER TABLE `valance` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-01-17 17:07:18
