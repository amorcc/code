""" 根据model类创建 """
from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker

import settings
from common.base_model import BaseModel
from model.model_users import *
from model.model_goods import *
from model.model_order import *
from model.model_other import *


def init_db():
    """初始化数据库"""
    session = settings.DB_SESSION()

    user2 = UsersInfo(user_name='a', password='111111',
                      user_sn='100001', status=1)
    session.add(user2)
    session.commit()
    session.close()

if __name__ == "__main__":
    # engine = create_engine(settings.DB_CON_STR, echo=True)
    print(settings.ENGINE)

    userinput = input('是否清除所有表结构？如果清除，则所有数据丢失。 (y/n):')
    if(userinput == 'y'):
        print('开始清除所有表结构')
        BaseModel.metadata.drop_all(settings.ENGINE)
    else:
        print('未清楚数据表结构，我们将只同步新增表')
    BaseModel.metadata.create_all(settings.ENGINE)
    print("\n**********************")
    print("已经完成")

    # init_db()