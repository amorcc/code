from bll.bll_user import BLLUser


def Login(para):
    print('---------------------')
    rt = BLLUser().Login('a', '111111')
    print('---------------------')
    return rt
