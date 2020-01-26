/* CREATING THE TABLE ADMIN */
CREATE TABLE TABLE_ADMIN(
	ADMIN_ID INTEGER NOT NULL IDENTITY(1,1),
	USERNAME VARCHAR(20) NOT NULL,
	USER_PASSWORD VARCHAR(50) NOT NULL,
	PRIMARY KEY(ADMIN_ID)
);
/* VALUES IN TABLE ADMIN */
INSERT INTO TABLE_ADMIN VALUES ('PMSADMIN', '1234')
INSERT INTO TABLE_ADMIN VALUES ('PMSADMIN2', '1234')

/*SYSTEM TABLES*/
/* CUSTOMER TABLE */
CREATE TABLE CUSTOMER(
	CUSTOMER_ID INTEGER NOT NULL IDENTITY(1,1) ,
	FIRST_NAME VARCHAR(50) NOT NULL,
	LAST_NAME VARCHAR(50) NOT NULL,
	PHONE_NUMBER INTEGER NOT NULL,
	ADDRESS VARCHAR(200) NOT NULL,
	EMAIL VARCHAR(100),
	PRIMARY KEY (CUSTOMER_ID),
);
/*CAR TABLE*/
CREATE TABLE CAR(
	PLATE_NUMBER VARCHAR(20) NOT NULL,
	CAR_MODEL VARCHAR(50) NOT NULL,
	CUSTOMER_ID INTEGER NOT NULL,
	PRIMARY KEY(PLATE_NUMBER),
	FOREIGN KEY(CUSTOMER_ID) REFERENCES CUSTOMER(CUSTOMER_ID)


/* CHECKINOUT TABLE */
CREATE TABLE CHECK_IN_OUT(
	CHECK_ID INTEGER NOT NULL IDENTITY(1,1),
	PLATE_NUMBER VARCHAR(20) NOT NULL,
	TIME_IN TIME NOT NULL,
	TIME_OUT TIME NOT NULL DEFAULT '00:00:00',
	CHECK_DATE DATE NOT NULL,
	TOTAL_TIME TIME NOT NULL,
	TOTAL_AMOUNT INTEGER NOT NULL,
	PRIMARY KEY (CHECK_ID),
	FOREIGN KEY (PLATE_NUMBER) REFERENCES CAR(PLATE_NUMBER)
);

/* PARKING TABLE */
CREATE TABLE PARKING(
	SPOT_ID INTEGER NOT NULL,
	CHECK_ID INTEGER NOT NULL,
	SPOT_STATUS BIT NOT NULL DEFAULT 0,
	PRIMARY KEY (SPOT_ID),
	FOREIGN KEY (CHECK_ID) REFERENCES CHECK_IN_OUT (CHECK_ID),
);










/*cursor for time spent in slot*/
DECLARE timespent_cursor CURSOR
FOR SELECT CHECK_ID,CHECK_DATE,TIME_IN,TIME_OUT FROM CHECK_IN_OUT

DECLARE @CHECK_ID INTEGER
DECLARE @CHECK_DATE DATE
DECLARE @TIME_IN TIME
DECLARE @TIME_OUT TIME


OPEN timespent_cursor
FETCH NEXT FROM timespent_cursor INTO @CHECK_ID,@CHECK_DATE,@TIME_IN,@TIME_OUT

WHILE(@@FETCH_STATUS=0)
BEGIN
	EXEC sp_timeinslot @CHECK_ID=@CHECK_ID,@CHECK_DATE=@CHECK_DATE,@TIME_IN=@TIME_IN,@TIME_OUT=@TIME_OUT
	FETCH NEXT FROM timespent_cursor INTO @CHECK_ID,@CHECK_DATE,@TIME_IN,@TIME_OUT
END
CLOSE timespent_cursor

/*stored procedure for time in slot*/
CREATE PROC sp_timeinslot @CHECK_ID INTEGER, @CHECK_DATE DATE, @TIME_IN TIME, @TIME_OUT TIME
AS
DECLARE @hoursdiff int

BEGIN
	SET @hoursdiff=DATEDIFF(HOUR,@timein,@timeout)
END

	UPDATE CHECK_IN_OUT SET TOTAL_TIME=@hoursdiff WHERE @CHECK_ID=@CHECK_ID
GO


/*trigger for cost calculation and update slot to empty*/
/* TRIGGER TO UPDATE PARKING_SPOT_STATUS TABLE */
CREATE TRIGGER UPDATE_PARKING_SPOT_STATUS on check_in_out
AFTER UPDATE 
AS
	DECLARE @SPOT_STATUS AS BIT = (SELECT SPOT_STATUS FROM PARKING WHERE SPOT_ID = SPOT_ID)
	IF (@SPOT_STATUS = 1)
		UPDATE PARKING SET @SPOT_STATUS = 0 WHERE SPOT_ID =SPOT_ID
	INSERT INTO PARKING VALUES ((SELECT CHECK_ID FROM CHECK_IN_OUT WHERE PLATE_NUMBER = PLATE_NUMBER), (SELECT SPOT_ID FROM PARKING WHERE SPOT_ID = SPOT_ID))
	GO


/*TRIGGER TO CALCULATE COST*/
CREATE TRIGGER CALCULATE_COST on check_in_out
AFTER UPDATE 
AS 
	DECLARE @TOTAL_AMOUNT integer =0 (SELECT TOTAL_AMOUNT FROM CHECK_IN_OUT WHERE CHECK_ID = CHECK_ID)
	DECLARE @TOTAL_TIME TIME = (SELECT TOTAL_TIME FROM CHECK_IN_OUT WHERE CHECK_ID = CHECK_ID)
	IF (@TOTAL_TIME = 1)
		SET TOTAL_AMOUNT=TOTAL_TIME *100
	ELSE IF (@TOTAL_TIME>1)
		SET TOTAL_AMOUNT=100+30
		UPDATE CHECK_IN_OUT SET @TOTAL_AMOUNT = 0 WHERE CHECK_ID =CHECK_ID
	GO


	
