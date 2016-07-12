CREATE TABLE Kerbals (
KerbalID int PRIMARY KEY IDENTITY,
UserName varchar(50) UNIQUE,
Password varchar(50)
);

CREATE TABLE Rockets (
RocketID int PRIMARY KEY IDENTITY,
KerbalID int REFERENCES Kerbals(KerbalID) ON DELETE CASCADE,
RocketName varchar(50),
Payload float NULL,
ParentBody varchar(50),
StageNumber int,
);

CREATE TABLE Stages (
StageID int PRIMARY KEY IDENTITY,
RocketID int REFERENCES Rockets(RocketID) ON DELETE CASCADE,
StageNumber int,
WetMass float,
DryMass float,
Isp int,
DeltaV int,
Thrust float,
MinTWR float,
MaxTWR float
);

CREATE PROC spCountUserByName(@value AS varchar(50))
AS
BEGIN
SELECT COUNT(*) FROM Kerbals WHERE UserName = @value;
END;

CREATE PROC spGetPasswordFromUserName(@value AS varchar(50))
AS
BEGIN
SELECT Password FROM Kerbals WHERE UserName = @value;
END;

CREATE PROC spCreateNewUser(@name AS varchar(50), @password AS varchar(50))
AS
BEGIN
INSERT INTO Kerbals (UserName, Password) values(@name, @password);
END;

CREATE PROC spDeleteUser(@value AS varchar(50))
AS
BEGIN
DELETE FROM Kerbals WHERE UserName = @value;
END;

CREATE PROC spAddRocket(
	@kerbalID AS int, 
	@rocketName AS varchar(50), 
	@payload AS float, 
	@parentBody AS varchar(50), 
	@stageNumber AS int)
AS
BEGIN
INSERT INTO Rockets VALUES (
	@kerbalID, 
	@rocketName, 
	@payload, 
	@parentBody, 
	@stageNumber);
END;

CREATE PROC spAddStage(
	@rocketID AS int, 
	@stageID as int, 
	@wetMass AS float, 
	@dryMass AS float, 
	@isp As int, 
	@deltaV AS int, 
	@thrust AS float, 
	@minTWR AS float, 
	@maxTWR AS float)
AS
BEGIN
INSERT INTO Stages VALUES(
	@rocketID,
	@stageID,
	@wetMass,
	@dryMass,
	@isp,
	@deltaV,
	@thrust,
	@minTWR,
	@maxTWR);
END;

DROP TABLE Stages
DROP TABLE Rockets
DROP TABLE Kerbals

DROP PROC spCountUserByName;
DROP PROC spGetPasswordFromUserName;
DROP PROC spCreateNewUser;
DROP PROC spDeleteUser;
DROP PROC spAddRocket;
DROP PROC spAddStage;