import json


class ResultMessage():

    def __init__(self, data={}, message='', id=0):
        self.Data = data
        self.Message = message
        self.ResponseID = id

    def GetResult(self):
        rt = {
            "ResponseID": self.ResponseID,
            "Message": self.Message,
            "Data": self.Data,
        }

        return json.dumps(rt)

    def ShowSysError(self):
        rt = {
            "ResponseID": 1,
            "Message": u'系统异常，请稍候重试',
            "Data": {},
        }

        return json.dumps(rt)

    def ShowNoServiceError(self, class_name):
        rt = {
            "ResponseID": 1,
            "Message": '接口{}不存在，请核对后重试'.format(class_name),
            "Data": {},
        }

        return json.dumps(rt)

    def ShowParamError(self):
        rt = {
            "ResponseID": 1,
            "Message": "请正确传递参数信息",
            "Data": {},
        }

        return json.dumps(rt)

    def MustLogin(self):
        rt = {
            "ResponseID": -1,
            "Message": "请登录后使用",
            "Data": {},
        }

        return json.dumps(rt)
