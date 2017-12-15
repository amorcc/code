import json


class ActionResult(object):
    """ 接口统一返回的数据结构 """

    def __init__(self):
        self.ResponseID = 0
        self.Message = ""
        self.Data = ""

    @staticmethod
    def ReturnResult(data, error_msg=''):
        a = ActionResult()
        if error_msg == "":
            a.ResponseID = 1
        else:
            a.ResponseID = 0

        a.Message = error_msg

        a.Data = data
        print(data)
        print('xinde')
        print(a.Data)
        return json.dumps(a, default=lambda obj: obj.__dict__, indent=4, ensure_ascii=False)
