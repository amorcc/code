""" 商品相关的表 """
from sqlalchemy import Column, String, Integer, DECIMAL

from common.base_model import BaseModel


class GoodsInfo(BaseModel):
    """ 商品表 """
    __tablename__ = "goods"

    name = Column(String(50), default='', server_default='', nullable=False)
    price = Column(DECIMAL(10, 2), default=0, server_default='0')
    image = Column(String(500), default='', server_default='',
                   nullable=False, doc='商品主图')
    images = Column(String(5000), default='', server_default='',
                    nullable=False, doc='商品详情的图片，多个逗号隔开')
    user_sn = Column(String(50), default='', server_default='',
                     nullable=False, doc='所属公司')
    is_recommend = Column(Integer, default=0, server_default='0', doc='是否推荐')
    status = Column(Integer, default=1, server_default='1',
                    doc='商品状态：1-正常，0-下架')
