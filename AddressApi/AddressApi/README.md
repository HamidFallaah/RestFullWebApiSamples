Create the Query bellow on your sqlserver and set the settings of launchSettings.json for local server and create the database and table
--------------------------------------------------------------------------------------------------------

-- 1) Create a sample database
IF DB_ID('AddressBookDb') IS NULL
BEGIN
  CREATE DATABASE AddressBookDb;
END
GO

USE AddressBookDb;
GO

-- 2) Create the table
IF OBJECT_ID('dbo.Addresses', 'U') IS NOT NULL
  DROP TABLE dbo.Addresses;
GO

CREATE TABLE dbo.Addresses
(
    AdrId           INT IDENTITY(1,1) PRIMARY KEY,
    adrPostalCode   NVARCHAR(10)  NOT NULL UNIQUE,
    adrBuildingName NVARCHAR(100) NULL,
    adrFloor        NVARCHAR(10)  NULL,
    adrHouseNumber  NVARCHAR(10)  NULL,
    adrLocalityCode NVARCHAR(20)  NULL,
    adrLocalityName NVARCHAR(100) NULL,
    adrLocalityType NVARCHAR(50)  NULL,
    adrMainAvenue   NVARCHAR(100) NULL,
    adrProvince     NVARCHAR(100) NULL,   -- treat as "state"
    adrSideFloor    NVARCHAR(10)  NULL,
    adrStreet       NVARCHAR(100) NULL,
    adrSubLocality  NVARCHAR(100) NULL,
    adrTownShip     NVARCHAR(100) NULL,
    adrLatitude     DECIMAL(9,6)  NULL,
    adrLongitude    DECIMAL(9,6)  NULL
);
GO

-- 3) Helpful indexes
CREATE INDEX IX_Addresses_Province ON dbo.Addresses(adrProvince);
CREATE INDEX IX_Addresses_PostalCode ON dbo.Addresses(adrPostalCode);
GO

-- 4) Seed 50 rows (10 provinces x 5 rows each; unique postal codes)
-- CA (California) base lat/lon 34.0500, -118.2400
INSERT dbo.Addresses(adrPostalCode, adrBuildingName, adrFloor, adrHouseNumber, adrLocalityCode, adrLocalityName, adrLocalityType, adrMainAvenue, adrProvince, adrSideFloor, adrStreet, adrSubLocality, adrTownShip, adrLatitude, adrLongitude) VALUES
('90001','Sunset Plaza','3','101','URB','Los Angeles','City','Wilshire Blvd','California','1','S Spring St','Downtown','Central LA',34.0500,-118.2400),
('90002','Sunset Plaza','5','102','URB','Los Angeles','City','Wilshire Blvd','California','2','W 7th St','Downtown','Central LA',34.0510,-118.2410),
('90003','Pacific Tower','7','103','URB','Los Angeles','City','Hollywood Blvd','California','3','N Vine St','Hollywood','Central LA',34.0520,-118.2420),
('90004','Pacific Tower','9','104','URB','Los Angeles','City','Hollywood Blvd','California','4','N Highland Ave','Hollywood','Central LA',34.0530,-118.2430),
('90005','Ocean View','2','105','URB','Santa Monica','City','Ocean Ave','California','1','Colorado Ave','Pier District','Westside',34.0540,-118.2440);

-- TX (Texas) base 30.2700, -97.7400
INSERT dbo.Addresses VALUES
('73301','Longhorn Center','2','201','URB','Austin','City','Congress Ave','Texas','Mezz','E 6th St','Warehouse','Travis Twp',30.2700,-97.7400),
('73302','Longhorn Center','4','202','URB','Austin','City','Congress Ave','Texas','1','E 7th St','Warehouse','Travis Twp',30.2710,-97.7410),
('73303','Capitol Plaza','6','203','URB','Austin','City','Guadalupe St','Texas','2','W 24th St','University','Travis Twp',30.2720,-97.7420),
('73304','Capitol Plaza','8','204','URB','Austin','City','Guadalupe St','Texas','3','W 29th St','North Campus','Travis Twp',30.2730,-97.7430),
('73305','Riverfront One','10','205','URB','Austin','City','Cesar Chavez St','Texas','PH','Red River St','Downtown','Travis Twp',30.2740,-97.7440);

-- NY (New York) base 40.7100, -74.0100
INSERT dbo.Addresses VALUES
('10001','Empire House','3','301','URB','New York','City','5th Ave','New York','1','W 34th St','Midtown','Manhattan Twp',40.7100,-74.0100),
('10002','Empire House','5','302','URB','New York','City','5th Ave','New York','2','W 33rd St','Midtown','Manhattan Twp',40.7110,-74.0110),
('10003','Liberty Plaza','7','303','URB','New York','City','Broadway','New York','3','E 14th St','Union Sq','Manhattan Twp',40.7120,-74.0120),
('10004','Liberty Plaza','9','304','URB','New York','City','Broadway','New York','4','Wall St','FiDi','Manhattan Twp',40.7130,-74.0130),
('10005','Hudson View','11','305','URB','New York','City','West St','New York','5','Chambers St','Tribeca','Manhattan Twp',40.7140,-74.0140);

-- FL (Florida) base 25.7600, -80.1900
INSERT dbo.Addresses VALUES
('33101','Bayfront Tower','2','401','URB','Miami','City','Biscayne Blvd','Florida','1','NE 1st St','Downtown','Miami Twp',25.7600,-80.1900),
('33102','Bayfront Tower','4','402','URB','Miami','City','Biscayne Blvd','Florida','2','NE 2nd St','Downtown','Miami Twp',25.7610,-80.1910),
('33103','Coral House','6','403','URB','Miami','City','Miracle Mile','Florida','3','Ponce de Leon','Coral Gables','Miami Twp',25.7620,-80.1920),
('33104','Coral House','8','404','URB','Miami','City','Miracle Mile','Florida','4','Andalusia Ave','Coral Gables','Miami Twp',25.7630,-80.1930),
('33105','Ocean Breeze','10','405','URB','Miami Beach','City','Collins Ave','Florida','PH','Lincoln Rd','South Beach','Miami Beach Twp',25.7640,-80.1940);

-- IL (Illinois) base 41.8800, -87.6300
INSERT dbo.Addresses VALUES
('60601','Lakeshore Place','3','501','URB','Chicago','City','Michigan Ave','Illinois','1','E Randolph St','Loop','Cook Twp',41.8800,-87.6300),
('60602','Lakeshore Place','5','502','URB','Chicago','City','Michigan Ave','Illinois','2','E Lake St','Loop','Cook Twp',41.8810,-87.6310),
('60603','River North Hub','7','503','URB','Chicago','City','Wacker Dr','Illinois','3','N State St','River North','Cook Twp',41.8820,-87.6320),
('60604','River North Hub','9','504','URB','Chicago','City','Wacker Dr','Illinois','4','N Clark St','River North','Cook Twp',41.8830,-87.6330),
('60605','Skyline Court','11','505','URB','Chicago','City','Roosevelt Rd','Illinois','5','S Wabash Ave','South Loop','Cook Twp',41.8840,-87.6340);

-- WA (Washington) base 47.6100, -122.3300
INSERT dbo.Addresses VALUES
('98101','Pine Crest','2','601','URB','Seattle','City','Pine St','Washington','1','4th Ave','Downtown','King Twp',47.6100,-122.3300),
('98102','Pine Crest','4','602','URB','Seattle','City','Pine St','Washington','2','5th Ave','Downtown','King Twp',47.6110,-122.3310),
('98103','Lake Union View','6','603','URB','Seattle','City','Westlake Ave','Washington','3','Mercer St','SLU','King Twp',47.6120,-122.3320),
('98104','Lake Union View','8','604','URB','Seattle','City','Westlake Ave','Washington','4','Valley St','SLU','King Twp',47.6130,-122.3330),
('98105','Cascade Court','10','605','URB','Seattle','City','University Way','Washington','PH','NE 45th St','U-District','King Twp',47.6140,-122.3340);

-- CO (Colorado) base 39.7400, -104.9900
INSERT dbo.Addresses VALUES
('80201','Mile High One','3','701','URB','Denver','City','Colfax Ave','Colorado','1','N Broadway','Capitol Hill','Denver Twp',39.7400,-104.9900),
('80202','Mile High One','5','702','URB','Denver','City','Colfax Ave','Colorado','2','Lincoln St','Capitol Hill','Denver Twp',39.7410,-104.9910),
('80203','Union Square','7','703','URB','Denver','City','Wynkoop St','Colorado','3','Blake St','LoDo','Denver Twp',39.7420,-104.9920),
('80204','Union Square','9','704','URB','Denver','City','Wynkoop St','Colorado','4','Market St','LoDo','Denver Twp',39.7430,-104.9930),
('80205','Cherry Creek Gate','11','705','URB','Denver','City','1st Ave','Colorado','5','Steele St','Cherry Creek','Denver Twp',39.7440,-104.9940);

-- GA (Georgia) base 33.7500, -84.3900
INSERT dbo.Addresses VALUES
('30301','Peachtree Center','2','801','URB','Atlanta','City','Peachtree St','Georgia','1','Andrew Young Intl Blvd','Downtown','Fulton Twp',33.7500,-84.3900),
('30302','Peachtree Center','4','802','URB','Atlanta','City','Peachtree St','Georgia','2','Baker St','Downtown','Fulton Twp',33.7510,-84.3910),
('30303','Midtown Loft','6','803','URB','Atlanta','City','10th St','Georgia','3','West Peachtree St','Midtown','Fulton Twp',33.7520,-84.3920),
('30304','Midtown Loft','8','804','URB','Atlanta','City','10th St','Georgia','4','Spring St','Midtown','Fulton Twp',33.7530,-84.3930),
('30305','Buckhead Point','10','805','URB','Atlanta','City','Piedmont Rd','Georgia','PH','Lenox Rd','Buckhead','Fulton Twp',33.7540,-84.3940);

-- AZ (Arizona) base 33.4500, -112.0700
INSERT dbo.Addresses VALUES
('85001','Desert Ridge','2','901','URB','Phoenix','City','Camelback Rd','Arizona','1','N Central Ave','Midtown','Maricopa Twp',33.4500,-112.0700),
('85002','Desert Ridge','4','902','URB','Phoenix','City','Camelback Rd','Arizona','2','N 7th St','Midtown','Maricopa Twp',33.4510,-112.0710),
('85003','Papago View','6','903','URB','Phoenix','City','Washington St','Arizona','3','N 1st St','Downtown','Maricopa Twp',33.4520,-112.0720),
('85004','Papago View','8','904','URB','Phoenix','City','Washington St','Arizona','4','N 3rd St','Downtown','Maricopa Twp',33.4530,-112.0730),
('85005','South Mountain Gate','10','905','URB','Phoenix','City','Baseline Rd','Arizona','PH','S 24th St','South Mountain','Maricopa Twp',33.4540,-112.0740);

-- MA (Massachusetts) base 42.3600, -71.0600
INSERT dbo.Addresses VALUES
('02101','Harbor Point','3','1001','URB','Boston','City','Atlantic Ave','Massachusetts','1','State St','Downtown','Suffolk Twp',42.3600,-71.0600),
('02102','Harbor Point','5','1002','URB','Boston','City','Atlantic Ave','Massachusetts','2','Milk St','Downtown','Suffolk Twp',42.3610,-71.0610),
('02103','Back Bay Row','7','1003','URB','Boston','City','Boylston St','Massachusetts','3','Newbury St','Back Bay','Suffolk Twp',42.3620,-71.0620),
('02104','Back Bay Row','9','1004','URB','Boston','City','Boylston St','Massachusetts','4','Exeter St','Back Bay','Suffolk Twp',42.3630,-71.0630),
('02105','Cambridge Crossing','11','1005','URB','Cambridge','City','Mass Ave','Massachusetts','5','Prospect St','Central Sq','Middlesex Twp',42.3640,-71.0640);
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------
