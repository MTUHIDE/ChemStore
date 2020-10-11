  IF EXISTS(SELECT 1 FROM information_schema.tables 
  WHERE table_name = '
'__EFMigrationsHistory'' AND table_schema = DATABASE()) 
BEGIN
CREATE TABLE `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

END;

CREATE TABLE `building` (
    `building_id` int(11) NOT NULL AUTO_INCREMENT,
    `building_name` varchar(45) NULL,
    PRIMARY KEY (`building_id`)
);

CREATE TABLE `chem_in_container` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    PRIMARY KEY (`id`)
);

CREATE TABLE `chemical` (
    `cas_number` int(11) NOT NULL AUTO_INCREMENT,
    `chem_name` varchar(45) NULL,
    `hazard_id` int(11) NULL,
    PRIMARY KEY (`cas_number`)
);

CREATE TABLE `has_location` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    PRIMARY KEY (`id`)
);

CREATE TABLE `hazard` (
    `hazard_id` int(11) NOT NULL AUTO_INCREMENT,
    `hazard_details` mediumtext NULL,
    PRIMARY KEY (`hazard_id`)
);

CREATE TABLE `container` (
    `container_id` int(11) NOT NULL,
    `unit` varchar(45) NULL,
    `size` double NULL,
    `chem_id` int(11) NULL,
    `location_id` int(11) NULL,
    PRIMARY KEY (`container_id`),
    CONSTRAINT `container_id` FOREIGN KEY (`container_id`) REFERENCES `has_location` (`id`) ON DELETE RESTRICT
);

CREATE TABLE `location` (
    `location_id` int(11) NOT NULL AUTO_INCREMENT,
    `department` int(11) NULL,
    `room` int(11) NULL,
    `building` int(11) NULL,
    `location_fid` int(11) NOT NULL,
    PRIMARY KEY (`location_id`),
    CONSTRAINT `location_fid` FOREIGN KEY (`location_fid`) REFERENCES `has_location` (`id`) ON DELETE RESTRICT
);

CREATE TABLE `person_in_charge` (
    `pic_id` int(11) NOT NULL,
    `email` varchar(40) NULL,
    `pic_name` varchar(45) NULL,
    PRIMARY KEY (`pic_id`),
    CONSTRAINT `pic_id` FOREIGN KEY (`pic_id`) REFERENCES `has_location` (`id`) ON DELETE RESTRICT
);

CREATE TABLE `has_hazard` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `hazard_id` int(11) NULL,
    `chemical_id` int(11) NULL,
    PRIMARY KEY (`id`),
    CONSTRAINT `chemical_id` FOREIGN KEY (`chemical_id`) REFERENCES `chemical` (`cas_number`) ON DELETE RESTRICT,
    CONSTRAINT `hazard_id` FOREIGN KEY (`hazard_id`) REFERENCES `hazard` (`hazard_id`) ON DELETE RESTRICT
);

CREATE INDEX `chemical_id_idx` ON `has_hazard` (`chemical_id`);

CREATE INDEX `hazard_id_idx` ON `has_hazard` (`hazard_id`);

CREATE INDEX `location_fid_idx` ON `location` (`location_fid`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20200926234434_InitialCreate', '3.1.8');

