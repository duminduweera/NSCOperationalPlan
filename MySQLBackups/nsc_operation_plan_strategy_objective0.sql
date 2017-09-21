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
-- Table structure for table `strategy_objective`
--

DROP TABLE IF EXISTS `strategy_objective`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `strategy_objective` (
  `id` varchar(10) NOT NULL,
  `rank` int(11) DEFAULT NULL,
  `strategy_objective` mediumtext NOT NULL,
  `theme_id` varchar(10) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_strategy_objective_theme1_idx` (`theme_id`),
  CONSTRAINT `fk_strategy_objective_theme1` FOREIGN KEY (`theme_id`) REFERENCES `theme` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `strategy_objective`
--

LOCK TABLES `strategy_objective` WRITE;
/*!40000 ALTER TABLE `strategy_objective` DISABLE KEYS */;
INSERT INTO `strategy_objective` VALUES ('LA1',1,'Established and sustainable investment program in place','Theme 4'),('LA2',2,'Revenue and income growth strategy in place','Theme 4'),('LA3',3,'Ensure Council is compliant with statutory regulations','Theme 4'),('LA4',4,'Proactively engage with the community','Theme 4'),('OC1',1,'Regional standard Narrabri CBD','Theme 1'),('OC2',2,'Regional standard industrial land developments','Theme 1'),('OC3',3,'Adequate health services to meet the needs of a regional centre','Theme 1'),('OC4',4,'Expanded tertiary educational facilities (agriculture, education, business, mining and health)','Theme 1'),('OC5',5,'Adequate accommodation available to meet demand (residential, community, industrial, aged and itinerant)','Theme 1'),('OC6',6,'A safe place to live, work and experience the diversity of cultural activities','Theme 1'),('PT1',1,'Narrabri Shire to be a regional centre','Theme 3'),('PT2',2,'Airport to be of regional quality ','Theme 3'),('PT3',3,'Regional standard infrastructure','Theme 3'),('SE1',1,'Sustainable land use','Theme 2'),('SE2',2,'Ensure a clean, green environment for the future','Theme 2');
/*!40000 ALTER TABLE `strategy_objective` ENABLE KEYS */;
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
