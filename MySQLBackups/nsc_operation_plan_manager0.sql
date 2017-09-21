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
-- Table structure for table `manager`
--

DROP TABLE IF EXISTS `manager`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `manager` (
  `id` varchar(3) NOT NULL,
  `manager_description` varchar(45) DEFAULT NULL,
  `manager_name` varchar(45) DEFAULT NULL,
  `manager_id` varchar(3) DEFAULT NULL,
  `manager_dept` varchar(45) DEFAULT NULL,
  `manager_subdept` varchar(45) DEFAULT NULL,
  `manager_login_name` varchar(20) DEFAULT NULL,
  `manager_permission` varchar(3) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_manager_manager1_idx` (`manager_id`),
  CONSTRAINT `fk_manager_manager1` FOREIGN KEY (`manager_id`) REFERENCES `manager` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `manager`
--

LOCK TABLES `manager` WRITE;
/*!40000 ALTER TABLE `manager` DISABLE KEYS */;
INSERT INTO `manager` VALUES ('001','General Manager','Stewart Todd','001','General Managers','General Managers','toddste','010'),('002','Director Corporate Services','Lindsay Mason','002','Corporate Services','Corporate Services','masonli','010'),('003','Director Infrastructure Delivery','Darren Raeck','003','Infrastructure Delivery','Infrastructure Delivery','raeckda','010'),('004','Director Development and Economic Growth','Tony Meppem','004','Development and Economic Growth','Development and Economic Growth','meppeto','010'),('100','Executive Manager Human Resources','Jacqui Carolan','001','General Managers','Human Resources','caroljr','010'),('102','Manager Corporate Strategy','Grace Farrer','001','General Managers','General Managers','farrego','010'),('200','Manager Financial Services','Tim McClellan','002','Corporate Services','Finance Services','mccleti','020'),('201','Manager Information Services','Sudintha Perera','002','Corporate Services','Information Services','perersu','099'),('202','Manager Library Services','Library Manager','002','Corporate Services','Library Services','librarymanager','020'),('203','Manager Property and Assets','Peter Murphy','002','Corporate Services','Property and Assets','murphyp','020'),('204','Manager The Crossing Theatre Venue','Trent Bruinsma','002','Corporate Services','The Crossing Theatre Venue','bruintr','020'),('300','Manager Airport','Scott McFarland','003','Infrastructure Delivery','Airport','mcfarsj','020'),('301','Manager Design Services','Anthony Smetanin','003','Infrastructure Delivery','Design Services','smetaaj','020'),('302','Manager Plant and Depot','Brett Dickinson','003','Infrastructure Delivery','Plant and Depot','dickibl','020'),('303','Manager Road Services','Alan Lawrance','003','Infrastructure Delivery','Road Services','lawraal','020'),('304','Manager Projects','Nader Zoljalali','003','Infrastructure Delivery','Senior Project Services','zoljana','020'),('305','Manager Water Services','Richard Madden','003','Infrastructure Delivery','Water Services','madderi','020'),('400','Manager Community Facilities','Helen Carroll','004','Development and Economic Growth','Community Facilities','carrohm','020'),('401','Manager Economic Development','Bill Birch','004','Development and Economic Growth','Economic Development','birchbr','020'),('402','Manager Environmental Services','Marcela Lopez','004','Development and Economic Growth','Strategy and Land Use','lopezma','020'),('403','Manager Tourism','Penelope Jobling','004','Development and Economic Growth','Tourism','phelppa','020'),('404','Manager Planning and Development','Daniel Boyce','004','Development and Economic Growth','Planning and Development','boyceda','020');
/*!40000 ALTER TABLE `manager` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-03-13 16:01:47
