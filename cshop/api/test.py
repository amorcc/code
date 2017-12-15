import json
import uuid

from service.usermng import reg
from models.logined_user import TokenMng

u = uuid.uuid1()
print(u)

class_name = "usermng"
methon_name = "reg"
amod = __import__("service." + class_name.lower(), fromlist=True)
param = {"token":"aaaa"}
ret = getattr(amod, methon_name)

if hasattr(ret, "NoLogin") == False or ret.NoLogin == False:
    # 需要验证登录
    if TokenMng.exist(param.get("token", "")) == False:
        print('必须登录')


# para = json.loads('{"token":"aaaadddd"}')
# rt = reg(para)
# print(rt)
# if hasattr(reg,'NoLogin'):
#     print(reg.NoLogin)
# else:
#     print("reg不存在函数属性NoLogin")

# print("*****")

# def reg2():
#     print('reg2')

# if hasattr(reg2,'NoLogin'):
#     print(reg2.NoLogin)
# else:
#     print("reg2不存在函数属性NoLogin")
# class_name = "usermng"
# methon_name = "Reg"
# para = json.loads('{"token":"aaaadddd"}')
# amod = __import__("service." + class_name.lower(), fromlist=True)


# if hasattr(amod, methon_name):
#     ret = getattr(amod, methon_name)
#     # print(ret)
#     rt = ret(para)
#     print(rt)
# from extends.utils import Dict
# import uuid
# import random

# from models.logined_user import LoginUser, TokenMng

# for i in range(10):
#     print(random.randint(10000000, 99999999))


# user1 = LoginUser("aaa", "r", "s", "aa11")

# TokenMng.Add(user1)

# u = TokenMng.Get("aa11")

# if u != None:
#     print(u.UserName)

# u2 = TokenMng.Get("aa22")

# if u2 != None:
#     print(u2.UserName)
# else:
#     print("is not exist")

# TokenMng.Remove("aa11")

# print(TokenMng.GetLength())

# print("***************")
# print(uuid.uuid1())
# # print(uuid.uuid5())
