DELIMITER $$

USE `ssdc`$$

DROP PROCEDURE IF EXISTS `Result123`$$

CREATE DEFINER=`root`@`%` PROCEDURE `Result123`(iPageIndex INT, iPageSize INT)
BEGIN
	SET @startRow = (iPageIndex-1)* iPageSize;  
	SET @pageSize = iPageSize;  
	SET @strsql = CONCAT(  
		'SELECT  a.IdNumber AS table1IdNumber,a.col1 AS table1col1,a.col2 AS table1col2,a.col3 AS table1col3,a.col4 AS table1col4,a.col5 AS table1col5,a.col6 AS table1col6,a.col7 AS table1col7,a.col8 AS table1col8,',
		'        b.IdNumber AS table2IdNumber,b.col1 AS table2col1,b.col2 AS table2col2,b.col3 AS table2col3,b.col4 AS table2col4,b.col5 AS table2col5,b.col6 AS table2col6,b.col7 AS table2col7,b.col8 AS table2col8,',
		'        c.IdNumber AS table3IdNumber,c.col1 AS table3col1,c.col2 AS table3col2,c.col3 AS table3col3,c.col4 AS table3col4,c.col5 AS table3col5,c.col6 AS table3col6,c.col7 AS table3col7,c.col8 AS table3col8 ',
		'FROM user_data a,user_data b,user_data c,contrastresult d ',
		'WHERE a.IdNumber = b.IdNumber AND a.IdNumber = c.IdNumber    AND b.IdNumber = c.IdNumber    AND a.IdNumber = d.IdNumber ',
		'    AND a.TableNum = 1    AND b.TableNum = 2    AND c.TableNum = 3 ',	
		'    AND d.table1count > 0    AND d.table2count > 0 ',	
		'    AND d.table3count > 0 ',	
		'ORDER BY a.Id ',	
		' limit ',
		@startRow,
		',',
		@pageSize	
	);  
	
	SELECT @strsql;
    
	PREPARE strsql FROM @strsql;#定义预处理语句   
	EXECUTE strsql;             #执行预处理语句   
	DEALLOCATE PREPARE strsql;  #删除定义
	
    
	SELECT COUNT(0) FROM contrastresult WHERE d.table1count > 0 AND d.table2count > 0 AND d.table3count > 0;
	
	END$$
DELIMITER ;