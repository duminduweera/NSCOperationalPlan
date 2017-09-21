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
-- Table structure for table `service_plan`
--

DROP TABLE IF EXISTS `service_plan`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `service_plan` (
  `id` varchar(3) CHARACTER SET utf8 NOT NULL,
  `service_plan_manager_id` varchar(3) NOT NULL,
  `service_plan` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `service_plan`
--

LOCK TABLES `service_plan` WRITE;
/*!40000 ALTER TABLE `service_plan` DISABLE KEYS */;
INSERT INTO `service_plan` VALUES ('000','','-NONE-'),('001','400','Cemeteries'),('002','302','Depot'),('003','301','Design and Investigation'),('004','302','Development Services'),('005','401','Economic Development'),('006','402','Enviro Health Compliance Services'),('007','200','Financial Services'),('008','302','Fleet Management'),('009','201','Information Services'),('010','102','Infrastructure Delivery Business Support'),('011','300','Narrabri Airport'),('012','202','Narrabri Shire Libraries'),('013','400','Parks and Open Spaces'),('014','304','Project Management Services'),('015','203','Property and Assets'),('016','402','Saleyards'),('017','402','Solid Waste Management'),('018','400','Swimming Pools'),('019','204','The Crossing Theatre'),('020','403','Tourism Services'),('021','302','Transport'),('022','305','Water Services'),('023','100','Workforce Management'),('024','400','Caravan Parks');
/*!40000 ALTER TABLE `service_plan` ENABLE KEYS */;
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
