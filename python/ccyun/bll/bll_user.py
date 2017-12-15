"""用户相关的业务"""
from common.base_bll import BaseBLL
from common.action_result import ActionResult


class BLLUser(BaseBLL):
    """ 用户相关的业务类 """

    def __init__(self):
        super(BLLUser, self).__init__()

    def Login(self, username, password):
        rt = ActionResult.ReturnResult('登录成功了')
        return rt
