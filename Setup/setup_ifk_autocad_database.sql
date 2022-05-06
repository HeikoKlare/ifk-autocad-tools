SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;


CREATE TABLE `AnnotationCategories` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(50) COLLATE latin1_german1_ci NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;

CREATE TABLE `Annotations` (
  `id` int(11) NOT NULL,
  `categoryId` int(10) UNSIGNED DEFAULT NULL,
  `name` varchar(50) COLLATE latin1_german1_ci NOT NULL,
  `content` text COLLATE latin1_german1_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;

CREATE TABLE `DetailCategories` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(50) COLLATE latin1_german1_ci NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;

CREATE TABLE `Details` (
  `id` int(10) NOT NULL,
  `name` varchar(100) COLLATE latin1_german1_ci NOT NULL,
  `templateFile` mediumblob NOT NULL,
  `presentationFile` mediumblob NOT NULL,
  `categoryId` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;

CREATE TABLE `Employer` (
  `id` varchar(5) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `name` varchar(50) COLLATE latin1_german1_ci NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci ROW_FORMAT=COMPACT;

CREATE TABLE `Projects` (
  `number` varchar(15) CHARACTER SET latin1 NOT NULL,
  `employer` varchar(100) CHARACTER SET latin1 NOT NULL,
  `description1` varchar(100) CHARACTER SET latin1 NOT NULL,
  `description2` varchar(100) CHARACTER SET latin1 NOT NULL,
  `description3` varchar(100) CHARACTER SET latin1 NOT NULL,
  `description4` varchar(100) CHARACTER SET latin1 NOT NULL,
  `descriptionShort` varchar(100) CHARACTER SET latin1 NOT NULL,
  `createdAt` varchar(21) CHARACTER SET latin1 NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;


ALTER TABLE `AnnotationCategories`
  ADD PRIMARY KEY (`id`);

ALTER TABLE `Annotations`
  ADD PRIMARY KEY (`id`),
  ADD KEY `categoryId_idx` (`categoryId`);

ALTER TABLE `DetailCategories`
  ADD PRIMARY KEY (`id`);

ALTER TABLE `Details`
  ADD PRIMARY KEY (`id`),
  ADD KEY `CategoryId_idx` (`categoryId`);

ALTER TABLE `Employer`
  ADD PRIMARY KEY (`id`);

ALTER TABLE `Projects`
  ADD PRIMARY KEY (`number`);


ALTER TABLE `AnnotationCategories`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

ALTER TABLE `Annotations`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `DetailCategories`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

ALTER TABLE `Details`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT;


ALTER TABLE `Annotations`
  ADD CONSTRAINT `AnnotationCategoryId` FOREIGN KEY (`categoryId`) REFERENCES `AnnotationCategories` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE `Details`
  ADD CONSTRAINT `DetailCategoryId` FOREIGN KEY (`categoryId`) REFERENCES `DetailCategories` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
