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
-- Table structure for table `capital_works_qbr`
--

DROP TABLE IF EXISTS `capital_works_qbr`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `capital_works_qbr` (
  `capital_works_id` int(11) NOT NULL,
  `capital_works_year` varchar(5) NOT NULL,
  `capital_works_quarter` int(11) NOT NULL,
  `capital_works_revised_budget` double DEFAULT NULL,
  PRIMARY KEY (`capital_works_id`,`capital_works_quarter`,`capital_works_year`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `capital_works_qbr`
--

LOCK TABLES `capital_works_qbr` WRITE;
/*!40000 ALTER TABLE `capital_works_qbr` DISABLE KEYS */;
INSERT INTO `capital_works_qbr` VALUES (1,'16/17',3,1000000),(2,'16/17',3,15500),(3,'16/17',3,30000),(4,'16/17',3,30000),(5,'16/17',3,122000),(6,'16/17',3,75000),(7,'16/17',3,10000);
/*!40000 ALTER TABLE `capital_works_qbr` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-03-13 15:58:39
