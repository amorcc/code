import uuid


class LoginUser():
    """ 已经登录的用户 """

    def __init__(self, userName, userSN_R, userSN_S):
        self.username = userName
        self.usersn_r = userSN_R
        self.usersn_s = userSN_S
        self.token = str(uuid.uuid1()).replace("-", "")

    def get_token(self):
        return self.token


class TokenMng():
    __cache = []

    @staticmethod
    def add(login_user):
        """新增一个登录用户到列表"""
        if TokenMng.exist(login_user.token) == False:
            TokenMng.__cache.insert(0, login_user)
            print('插入一个值')
        else:
            print('已经存在')

    @staticmethod
    def exist(token):
        """查看指定的token是否在列表中存在"""
        for item in TokenMng.__cache:
            if(item.token == token):
                return True

        return False

    @staticmethod
    def get(token):
        """根据token获取用户信息"""
        for item in TokenMng.__cache:
            if(item.token == token):
                return item

        return None

    @staticmethod
    def remove(token):
        """删除指定token的用户"""
        u = TokenMng.get(token)
        if u != None:
            TokenMng.__cache.remove(u)

    @staticmethod
    def get_length():
        return len(TokenMng.__cache)
