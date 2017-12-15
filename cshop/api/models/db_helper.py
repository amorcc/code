"""
数据库帮助类
"""
from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker, scoped_session

from config import db_config


class DBHelper():
    __Engine = None
    __Session = None

    @staticmethod
    def GetEngine():
        """获取一个Engine"""
        if DBHelper.__Engine == None:
            dbConStr = 'mysql+pymysql://{}:{}@{}:{}/{}?charset=utf8'.format(
                db_config["username"], db_config["password"], db_config["ip"], db_config["port"], db_config["db_name"])
            print("\n******" + dbConStr + "******\n")
            DBHelper.__Engine = create_engine(dbConStr, echo=True)
            # print(DBHelper.__Engine)
        return DBHelper.__Engine

    @staticmethod
    def GetSession():
        """获取数据库的DBSession"""
        if DBHelper.__Session == None:
            session_factory = sessionmaker(bind=DBHelper.GetEngine())
            DBHelper.__Session = scoped_session(session_factory)

        return DBHelper.__Session
