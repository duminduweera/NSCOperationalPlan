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
-- Table structure for table `kpi`
--

DROP TABLE IF EXISTS `kpi`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `kpi` (
  `id` int(3) unsigned NOT NULL,
  `kpm_id` varchar(3) DEFAULT NULL,
  `manager_id` varchar(3) DEFAULT NULL,
  `efficiency_description` mediumtext,
  `prefix` varchar(2) DEFAULT NULL COMMENT 'Less than (<)\nGreater than (>)\nEqual (=)\netc',
  `unit` varchar(2) DEFAULT NULL COMMENT '%, $, Minitue, Hrs, Days etc',
  `service_plan_id` varchar(3) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `kpi`
--

LOCK TABLES `kpi` WRITE;
/*!40000 ALTER TABLE `kpi` DISABLE KEYS */;
INSERT INTO `kpi` VALUES (1,'001','201','Increase staff satisfaction about Council’s IT services - IT','02','01','009'),(2,'001','201','Staying up-to-date with current Technology(Software/Hardware) Developments','02','01','009'),(3,'001','201','Maintain compliance with State Records Act – Records','02','01','009'),(4,'002','201','Average maintenance time per IT equipment ','01','10','009'),(5,'002','201','Average time spend per resolving staff enquiries regarding IT ','01','10','009'),(6,'002','201','Increase the space required for records storage ','00','01','009'),(7,'002','201','Average time spend per Record Correspondence ','01','10','009'),(8,'003','201','Number of InfoXpert (ERMS) Users','00','03','009'),(9,'003','201','Number of external sites','00','03','009'),(10,'003','201','Number of Council’s Desktop Users','00','03','009'),(11,'003','201','Number of Servers','00','03','009'),(12,'003','201','Number of Records Department correspondence ','00','03','009');
/*!40000 ALTER TABLE `kpi` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-03-13 16:01:48
