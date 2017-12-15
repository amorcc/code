import json

from .result_message import ResultMessage
from models.db_helper import DBHelper
from models.models import UsersInfo
from models.logined_user import LoginUser, TokenMng


def reg(param):
    """ 用户注册 """
    username = param.get("UserName", "aaabbb")
    rt = {
        "token2": param["token"],
        "Username": username,
    }
    return ResultMessage(rt).GetResult()


reg.NoLogin = True


def login(param):
    """ 登录 """
    username = param.get("username", "")
    password = param.get("password", "")

    u = LoginUser("username", "usersn_r", "usersn_s")
    TokenMng.add(u)
    rt = {
        "token": u.token
    }

    return ResultMessage(rt).GetResult()


login.NoLogin = True


def get_user_info(param):
    rt = {
        "username": "u111"
    }

    return ResultMessage(rt).GetResult()
