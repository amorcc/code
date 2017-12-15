"""其他表，主要是配置类"""
from sqlalchemy import Column, String, Integer, DECIMAL

from common.base_model import BaseModel


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
