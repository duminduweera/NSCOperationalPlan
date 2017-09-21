-- MySQL dump 10.13  Distrib 5.7.12, for Win64 (x86_64)
--
-- Host: nscutil    Database: nsc_operation_plan
-- ------------------------------------------------------
-- Server version	5.6.17

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
-- Table structure for table `kpi_progress`
--

DROP TABLE IF EXISTS `kpi_progress`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `kpi_progress` (
  `kpi_id` int(3) NOT NULL,
  `kpi_year` varchar(5) NOT NULL,
  `kpi_month` tinyint(2) NOT NULL,
  `kpi_progress` varchar(45) DEFAULT NULL,
  `kpi_remark` mediumtext,
  PRIMARY KEY (`kpi_id`,`kpi_year`,`kpi_month`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `kpi_progress`
--

LOCK TABLES `kpi_progress` WRITE;
/*!40000 ALTER TABLE `kpi_progress` DISABLE KEYS */;
INSERT INTO `kpi_progress` VALUES (1,'16/17',9,'40','Test 1'),(2,'16/17',9,'55','Test 2'),(3,'16/17',9,'53','Test 3'),(4,'16/17',9,'10','tt1'),(5,'16/17',9,'9','tt2'),(6,'16/17',9,'50','tt3'),(7,'16/17',9,'9','tt4'),(8,'16/17',9,'90','A1'),(9,'16/17',9,'13','A2'),(10,'16/17',9,'150','A3'),(11,'16/17',9,'34','A4'),(12,'16/17',9,'3000','A5');
/*!40000 ALTER TABLE `kpi_progress` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-03-13 16:01:49
