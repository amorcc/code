"""
初始化创建数据库表和创建测试数据
"""
import random

from models.models import *
from models.db_helper import DBHelper

from config import db_config


def CreateTable():
    """ 创建表 """
    userinput = input("是否清除所有表结构？如果清除，则所有数据丢失。(y/n):")
    if(userinput.lower() == 'y'):
        print("开始清除所有表结构")
        BaseModel.metadata.drop_all(DBHelper.GetEngine())
    else:
        print("未清理数据表接口，我们将只同步新增的表")

    # 开始同步表
    BaseModel.metadata.create_all(DBHelper.GetEngine())

    print("\n***********************************")
    print("同步数据库结构完成")


def AddTestData():
    """ 添加测试数据 """
    session = DBHelper.GetSession()
    usersn = str(random.randint(10000000, 99999999))

    # 新增一个公司
    company1 = CompanyInfo()
    company1.address = '公司1的地址'
    company1.area_code = '410000'
    company1.company_name = '公司1'
    company1.contact = '联系人'
    company1.shop_name = '公司1的店铺'
    company1.user_sn = usersn

    # 新增商品
    goods1 = GoodsInfo()
    goods1.image = 'http://i1.zol-img.com.cn/g5/M00/0A/03/ChMkJlnxQn-II8YMAACA-3otnksAAhjwgJ7GroAAIET536.jpg'
    goods1.images = ''
    goods1.is_recommend = 1
    goods1.name = '华为mate9 128G'
    goods1.price = 2920.22
    goods1.status = 1
    goods1.user_sn = usersn

    goods2 = GoodsInfo()
    goods2.image = 'http://2a.zol-img.com.cn/product/137_400x300/984/ceyZyYvx7mdLc.jpg'
    goods2.images = ''
    goods2.is_recommend = 1
    goods2.name = '小米4 全网通 128G'
    goods2.price = 2920.22
    goods2.status = 1
    goods2.user_sn = usersn

    for item in session.query(CompanyInfo).all():
        print(item.company_name)
        session.delete(item)
    session.commit()

    # session.add(company1)
    # session.add(goods1)
    # session.add(goods2)
    # session.commit()
    session.close()


if __name__ == "__main__":
    CreateTable()
    # AddTestData()
