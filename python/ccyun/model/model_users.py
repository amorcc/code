""" users，company"""
from sqlalchemy import Column, String, Integer

from common.base_model import BaseModel


class UsersInfo(BaseModel):
    """ 用户登录表 """
    __tablename__ = "users"

    user_name = Column(String(50), index=True, nullable=False)
    password = Column(String(50))
    user_sn = Column(String(50), default='', server_default='', nullable=False)
    status = Column(Integer, default=1, server_default='1',
                    doc='用户状态：1-正常，0-禁用')

    def __repr__(self):
        return "<Users(Username='%s'" % (self.UserName)


class CompanyInfo(BaseModel):
    """ 公司表 """
    __tablename__ = "company"

    company_name = Column(String(200), default='', server_default='')
    shop_name = Column(String(20), default='', server_default='')
    user_sn = Column(String(50), nullable=False)
    contact = Column(String(50), default='', server_default='', doc='联系人')
    contact_phone = Column(String(200), default='',
                           server_default='', doc='联系电话')
    address = Column(String(200), default='', server_default='', doc='详细地址')
    area_code = Column(Integer, default=0, server_default='0', doc='区域编码')
