
CREATE TABLE `Organization` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) NOT NULL,
    `Description` varchar(500) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext NOT NULL,
    `UpdatedBy` longtext NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateUpdated` datetime(6) NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Contact` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Firstname` varchar(100) NOT NULL,
    `Lastname` varchar(100) NULL,
    `BirthDate` datetime NULL,
    `Email` varchar(100) NULL,
    `Phone` varchar(50) NULL,
    `OrganizationId` int NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    `CreatedBy` varchar(450) NOT NULL,
    `UpdatedBy` varchar(450) NULL,
    `DateCreated` datetime NOT NULL,
    `DateUpdated` datetime NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Contact_Organization_OrganizationId` FOREIGN KEY (`OrganizationId`) REFERENCES `Organization` (`Id`) ON DELETE SET NULL
);