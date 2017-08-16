DELIMITER $$

USE `ssdc`$$

DROP PROCEDURE IF EXISTS `ResultAll`$$

CREATE DEFINER=`root`@`%` PROCEDURE `ResultAll`(iPageIndex INT, iPageSize INT)
BEGIN
	SET @startRow = (iPageIndex-1)* iPageSize;  
	SET @pageSize = iPageSize;  
	SET @strsql = CONCAT(  
		'SELECT ID,IdNumber AS ''身份证号'',table1count,table2count,table3count,table4count,table5count,table6count,table7count,table8count FROM contrastresult',
		' ORDER BY ID',	
		' limit ',
		@startRow,
		',',
		@pageSize	
	);  
    
	PREPARE strsql FROM @strsql;#定义预处理语句   
	EXECUTE strsql;             #执行预处理语句   
	DEALLOCATE PREPARE strsql;  #删除定义
	
    
	SELECT COUNT(0) FROM contrastresult;
    END$$

DELIMITER ;