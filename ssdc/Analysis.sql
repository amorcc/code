DELIMITER $$

USE `ssdc`$$

DROP PROCEDURE IF EXISTS `Analysis`$$

CREATE DEFINER=`root`@`%` PROCEDURE `Analysis`()
BEGIN
	DECLARE batch VARCHAR(50) DEFAULT '';
	
	DECLARE idnum VARCHAR(20);
	DECLARE idcount INT;	
	DECLARE done INT DEFAULT -1;  
	
	DECLARE cur2 CURSOR FOR	SELECT IdNumber,COUNT(1) FROM ssdc.table2 GROUP BY IdNumber;
	DECLARE cur3 CURSOR FOR	SELECT IdNumber,COUNT(1) FROM ssdc.table3 GROUP BY IdNumber;
	DECLARE cur4 CURSOR FOR	SELECT IdNumber,COUNT(1) FROM ssdc.table4 GROUP BY IdNumber;
	DECLARE cur5 CURSOR FOR	SELECT IdNumber,COUNT(1) FROM ssdc.table5 GROUP BY IdNumber;
	DECLARE cur6 CURSOR FOR	SELECT IdNumber,COUNT(1) FROM ssdc.table6 GROUP BY IdNumber;
	DECLARE cur7 CURSOR FOR	SELECT IdNumber,COUNT(1) FROM ssdc.table7 GROUP BY IdNumber;
	DECLARE cur8 CURSOR FOR	SELECT IdNumber,COUNT(1) FROM ssdc.table8 GROUP BY IdNumber;
	
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done=1;  
	
	SET batch= UUID();
	
	# -----------------------------------------------------------
	# 0、 删除现有数据
	DELETE FROM ssdc.contrastresult;
	
	# -----------------------------------------------------------
	# 1、 将table1信息统计，并插入结果表中
	INSERT INTO ssdc.contrastresult(IdNumber,table1count)
	SELECT IdNumber,COUNT(1)
	FROM table1
	GROUP BY IdNumber;
	
	# -----------------------------------------------------------
	# 2、 开始汇总table2表
	OPEN cur2; 
	
	myLoop: LOOP  
		FETCH NEXT FROM cur2 INTO idnum, idcount;  
		
		IF done = 1 THEN   
			LEAVE myLoop;  
		END IF;  
		
		IF EXISTS (SELECT 1 FROM ssdc.contrastresult WHERE IdNumber=idnum) THEN
			UPDATE ssdc.contrastresult SET table2count=idcount WHERE IdNumber = idnum;
		ELSE 
			INSERT INTO ssdc.contrastresult(IdNumber,table2count) VALUES(idnum,idcount);
		END IF;
	END LOOP myLoop;  
	
	CLOSE cur2;
	
	# -----------------------------------------------------------
	# 3、 开始汇总table3表
	SET done = -1;
	OPEN cur3; 
	
	myLoop: LOOP  
		FETCH NEXT FROM cur3 INTO idnum, idcount;  
		
		IF done = 1 THEN   
			LEAVE myLoop;  
		END IF;  
		
		IF EXISTS (SELECT 1 FROM ssdc.contrastresult WHERE IdNumber=idnum) THEN
			UPDATE ssdc.contrastresult SET table3count=idcount WHERE IdNumber = idnum;
		ELSE 
			INSERT INTO ssdc.contrastresult(IdNumber,table3count) VALUES(idnum,idcount);
		END IF;
	END LOOP myLoop;  
	
	CLOSE cur3;
	
	# -----------------------------------------------------------
	# 4、 开始汇总table4表
	SET done = -1;
	OPEN cur4; 
	
	myLoop: LOOP  
		FETCH NEXT FROM cur4 INTO idnum, idcount;  
		
		IF done = 1 THEN   
			LEAVE myLoop;  
		END IF;  
		
		IF EXISTS (SELECT 1 FROM ssdc.contrastresult WHERE IdNumber=idnum) THEN
			UPDATE ssdc.contrastresult SET table4count=idcount WHERE IdNumber = idnum;
		ELSE 
			INSERT INTO ssdc.contrastresult(IdNumber,table4count) VALUES(idnum,idcount);
		END IF;
	END LOOP myLoop;  
	
	CLOSE cur4;
	
	# -----------------------------------------------------------
	# 5、 开始汇总table5表
	SET done = -1;
	OPEN cur5; 
	
	myLoop: LOOP  
		FETCH NEXT FROM cur5 INTO idnum, idcount;  
		
		IF done = 1 THEN   
			LEAVE myLoop;  
		END IF;  
		
		IF EXISTS (SELECT 1 FROM ssdc.contrastresult WHERE IdNumber=idnum) THEN
			UPDATE ssdc.contrastresult SET table5count=idcount WHERE IdNumber = idnum;
		ELSE 
			INSERT INTO ssdc.contrastresult(IdNumber,table5count) VALUES(idnum,idcount);
		END IF;
	END LOOP myLoop;  
	
	CLOSE cur5;
	
	# -----------------------------------------------------------
	# 6、 开始汇总table6表
	SET done = -1;
	OPEN cur6; 
	
	myLoop: LOOP  
		FETCH NEXT FROM cur6 INTO idnum, idcount;  
		
		IF done = 1 THEN   
			LEAVE myLoop;  
		END IF;  
		
		IF EXISTS (SELECT 1 FROM ssdc.contrastresult WHERE IdNumber=idnum) THEN
			UPDATE ssdc.contrastresult SET table6count=idcount WHERE IdNumber = idnum;
		ELSE 
			INSERT INTO ssdc.contrastresult(IdNumber,table6count) VALUES(idnum,idcount);
		END IF;
	END LOOP myLoop;  
	
	CLOSE cur6;
	
	# -----------------------------------------------------------
	# 7、 开始汇总table7表
	SET done = -1;
	OPEN cur7; 
	
	myLoop: LOOP  
		FETCH NEXT FROM cur7 INTO idnum, idcount;  
		
		IF done = 1 THEN   
			LEAVE myLoop;  
		END IF;  
		
		IF EXISTS (SELECT 1 FROM ssdc.contrastresult WHERE IdNumber=idnum) THEN
			UPDATE ssdc.contrastresult SET table7count=idcount WHERE IdNumber = idnum;
		ELSE 
			INSERT INTO ssdc.contrastresult(IdNumber,table7count) VALUES(idnum,idcount);
		END IF;
	END LOOP myLoop;  
	
	CLOSE cur7;
	
	# -----------------------------------------------------------
	# 8、 开始汇总table8表
	SET done = -1;
	OPEN cur8; 
	
	myLoop: LOOP  
		FETCH NEXT FROM cur8 INTO idnum, idcount;  
		
		IF done = 1 THEN   
			LEAVE myLoop;  
		END IF;  
		
		IF EXISTS (SELECT 1 FROM ssdc.contrastresult WHERE IdNumber=idnum) THEN
			UPDATE ssdc.contrastresult SET table8count=idcount WHERE IdNumber = idnum;
		ELSE 
			INSERT INTO ssdc.contrastresult(IdNumber,table8count) VALUES(idnum,idcount);
		END IF;
	END LOOP myLoop;  
	
	CLOSE cur8;
	
	# -----------------------------------------------------------
	# 9、 汇总总数据条数
	UPDATE ssdc.contrastresult SET sumcount = table1count+table2count+table3count+table4count+table5count+table6count+table7count+table8count;
	
    END$$

DELIMITER ;