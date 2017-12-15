'''
数据库实体类
'''

from datetime import datetime

from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy import Integer, DateTime, Column, Sequence, String, DECIMAL

Base = declarative_base()


class BaseModel(Base):
    '''
    model类的基类

    每个实体默认定义id和date_added列
    '''
    __abstract__ = True
    id = Column(Integer, Sequence("id"), primary_key=True)
    date_aded = Column(DateTime, default=datetime.utcnow)


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
    user_sn_s = Column(String(50), nullable=False)
    contact = Column(String(50), default='', server_default='', doc='联系人')
    contact_phone = Column(String(200), default='',
                           server_default='', doc='联系电话')
    address = Column(String(200), default='', server_default='', doc='详细地址')
    area_code = Column(Integer, default=0, server_default='0', doc='区域编码')


class Buyer(BaseModel):
    """买家表"""
    __tablename__ = "buyer"

    name = Column(String(20))
    user_sn_r = Column(String(50), nullable=False)
    status = Column(Integer, default=1, server_default='1',
                    doc='状态：1-正常，0-下架')


class BuyerAddress(BaseModel):
    """买家地址表"""
    __tablename__ = "buyer_addresss"

    area_code = Column(Integer, default=0, server_default=0)
    receiver = Column(String(20), nullable=False)
    phone = Column(String(20), nullable=False)
    address = Column(String(200), nullable)


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


class OrderInfo(BaseModel):
    """ 订单表 """
    __tablename__ = "order"

    order_code = Column(String(50), default='',
                        server_default='', nullable=False)
    order_money = Column(DECIMAL(10, 2), default=0,
                         server_default='0', doc='订单金额，下单时的金额')
    final_money = Column(DECIMAL(10, 2), default=0,
                         server_default='0', doc='实际金额，改价后的金额，实收金额')
    trans_fee = Column(DECIMAL(10, 2), default=0,
                       server_default='0', doc='运费')
    status = Column(Integer, default=1, server_default='1',
                    doc='订单状态:-1=已取消,1-待付款,2-已付款,3-已出库,4-已发货,5-已完成')
    message = Column(String(500), default='', server_default='',
                     nullable=False, doc='买家留言')
    address = Column(String(500), default='', server_default='',
                     nullable=False, doc='买家详细收货地址：省市县街道')
    receiver = Column(String(20), default='', server_default='', doc="收货人姓名")
    receiver_phone = Column(String(20), default='',
                            server_default='', doc='收货人手机号')
    pay_type = Column(Integer, default=0, server_default='0', doc='主要支付方式')
    other_pay_type = Column(String(20), default='',
                            server_default='', doc='其他支付方式，多个逗号隔开')
    seller_name = Column(String(50), default='', server_default='', doc='卖家名称')
    buyer_name = Column(String(50), default='', server_default='', doc='买家名称')
    user_sn_r = Column(String(20), default='', server_default='')
    user_sn_s = Column(String(20), default='', server_default='')
    receipt_type = Column(Integer, default=0,
                          server_default='0', doc='0-不要票，1-普票,2-专票')
    receipt_title = Column(String(50), default='',
                           server_default='', doc='发票抬头')
    receipt_tax_id = Column(String(50), default='',
                            server_default='', doc='纳税人识别号')
    receipt_bank_name = Column(
        String(50), default='', server_default='', doc='开户行')
    receipt_bank_account = Column(
        String(50), default='', server_default='', doc='银行账号')
    receipt_address = Column(String(50), default='',
                             server_default='', doc='发票地址')
    receipt_phone = Column(String(50), default='',
                           server_default='', doc='发票手机号')
    cs_status = Column(Integer, default=0,
                       server_default='0', doc='售后状态: 0-无售后')
    proof_status = Column(Integer, default=0,
                          server_default='0', doc='线下支付支付凭证状态：0-无')
    pay_success_amount = Column(
        DECIMAL(10, 2), default=0, server_default='0', doc='已经成功支付的金额')
    discount = Column(DECIMAL(10, 2), default=0,
                      server_default='0', doc='订单优惠金额')
    is_activity = Column(Integer, default=0,
                         server_default='0', doc='是否包含活动购买')


class OrderDetailsInfo(BaseModel):
    __tablename__ = 'order_detals'

    order_code = Column(String(50), nullable=False)
    sub_order_code = Column(String(50), nullable=False)
    pro_id = Column(Integer, nullable=False)
    pro_name = Column(String(50), default='',
                      server_default='', nullable=False)
    pro_price = Column(DECIMAL(10, 2), default=0,
                       server_default='0', doc='商品价格，改价后的价格')
    pro_image = Column(String(500), default='', server_default='',
                       nullable=False, doc='商品主图')
    pro_count = Column(Integer, default=1, server_default='1')
    pro_price_create = Column(
        DECIMAL(10, 2), default=0, server_default='0', doc='下单时的价格')
    sub_total = Column(DECIMAL(10, 2), default=0, server_default='0')
    discount = Column(DECIMAL(10, 2), default=0,
                      server_default='0', doc='优惠金额')
    a_id = Column(Integer, default=0, server_default='0', doc='活动id')
    a_type = Column(Integer, default=0, server_default='0', doc='活动类型')
    ext_id = Column(Integer, default=0, server_default='0', doc='捆绑套餐id')
    a_name = Column(String(50), default='', server_default='', doc='活动名称')
    a_rule = Column(String(100), default='', server_default='', doc='活动规则描述')
    a_start = Column(String(50), default='', server_default='', doc='活动开始时间')
    a_end = Column(String(50), default='', server_default='', doc='活动结束时间')


class SysPayTypeInfo(BaseModel):
    """系统支持的支付类型表"""
    __tablename__ = "sys_pay_type"

    pay_type_name = Column(String(20), default='', server_default='')
    pay_type_desc = Column(String(50), default='', server_default='')
    show = Column(Integer, default=1, server_default='1', doc='是否显示')
    status = Column(Integer, default=1, server_default='1',
                    doc='状态：0-未启用，1-已启用')


class PaySettingInfo(BaseModel):
    """公司支付配置表"""
    __tablename__ = 'pay_setting'

    user_sn = Column(String(20), nullable=False)
    wechatpay_appid = Column(String(200), default='', server_default='')
    wechatpay_mchid = Column(String(200), default='', server_default='')
    wechatpay_key = Column(String(200), default='', server_default='')
    wechatpay_appsecret = Column(String(200), default='', server_default='')
    alipay_app_id = Column(String(200), default='', server_default='')
    alipay_private_key = Column(String(200), default='', server_default='')
    alipay_alipay_public_key = Column(
        String(200), default='', server_default='')
    alipay_sign_type = Column(String(200), default='', server_default='')
