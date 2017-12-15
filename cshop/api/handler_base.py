import logging
import json

import tornado.web
logger = logging.getLogger(__name__)

from service.result_message import ResultMessage
from models.logined_user import LoginUser, TokenMng


class BaseHandler(tornado.web.RequestHandler):
    def write_error(self, status_code, **kwargs):
        logger.error("", **kwargs)
        self.set_header('Content-Type', 'application/json; charset=UTF-8')
        self.write(ResultMessage().ShowSysError())
        self.finish()


class IndexHandler(BaseHandler):
    def get(self):
        self.write("this is web api.")


class ApiHandler(BaseHandler):
    def post(self, class_name, methon_name):
        # 动态加载service包
        self.set_header('Content-Type', 'application/json; charset=UTF-8')
        try:
            amod = __import__("service." + class_name.lower(), fromlist=True)
        except:
            self.write(ResultMessage().ShowNoServiceError(class_name))
            self.finish()
            return

        try:
            param = self.request.body.decode('utf-8')
            param = json.loads(param)
        except:
            self.write(ResultMessage().ShowParamError())
            self.finish()

        if hasattr(amod, methon_name):
            ret = getattr(amod, methon_name)

            if hasattr(ret, "NoLogin") == False or ret.NoLogin == False:
                # 需要验证登录,判断是否登录，如果没有就通知client错误信息
                if TokenMng.exist(param.get("token", "")) == False:
                    self.finish(ResultMessage().MustLogin())
                    return

            rt = ret(param)

            self.finish(rt)
            return
        else:
            self.write(ResultMessage().ShowNoServiceError(methon_name))
            self.finish()
            return

    def get(self, class_name, methon_name):
        self.write("访问非法")


class NofoundHandler(BaseHandler):
    def get(self):
        self.write('404')

    def post(self):
        self.write('404')
