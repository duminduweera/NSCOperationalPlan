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
-- Table structure for table `theme`
--

DROP TABLE IF EXISTS `theme`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `theme` (
  `id` varchar(10) NOT NULL,
  `theme_short` varchar(100) NOT NULL,
  `theme_description` varchar(500) NOT NULL,
  `theme_code_for_strategy` varchar(5) DEFAULT NULL,
  `theme_color` varchar(7) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `theme`
--

LOCK TABLES `theme` WRITE;
/*!40000 ALTER TABLE `theme` DISABLE KEYS */;
INSERT INTO `theme` VALUES ('Theme 1','One community - Valuing the contributions that everyone can make','Our community believes strongly in the Council’s vision statement of an “entire Shire community”. Everyone has a contribution to make and everyone deserves to share in investments made by Council. Continuing eff orts are needed to engage our young people and to involve the indigenous members of our community.','OC','#FF80FF'),('Theme 2','A sustainable environment - Respect for our whole living environment','The community is mindful that the whole of the environment needs to be considered to ensure that “a quality living environment for the entire Shire community” is achievable. Everyone can contribute whether living and working in towns or villages or in rural areas.','SE','#00B050'),('Theme 3','A place to thrive - A strong, diverse economy that attracts and retains businesses, services and tou','The people of Narrabri Shire are very proud of the region in which they work and live. The community has a strong desire to see its Shire prosper. They want the Shire to be attractive as a destination for people to live, run businesses and visit.','PT','#0000CC'),('Theme 4','Proactive leadership and advocacy - Managing for all and standing up for our Shire','Our community recognises that Council does not have complete control over every aspect of the Shire. Sometimes we need contributions from state and federal governments and agencies in order to make changes. However, the community does look to Council for strong leadership, clear communication, effi cient support of development and a preparedness to meet commitments.','LA','#990000');
/*!40000 ALTER TABLE `theme` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-03-13 15:58:38
