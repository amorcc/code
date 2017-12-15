import logging

import tornado.httpserver
import tornado.ioloop
import tornado.options
import tornado.web

import settings
from tornado.options import define, options
define("port", default=settings.PORT, help="run on the given port", type=int)

import settings


class BaseHandler(tornado.web.RequestHandler):
    def write_error(self, status_code, **kwargs):
        print("**************")
        print(**kwargs)
        settings.logger.write_error(**kwargs)
        print("**************")
        settings.logger.error(str(kwargs))


class IndexHandler(BaseHandler):
    """ 默认的get接口 """

    def get(self):
        self.write("this is web api.")


class ApiHandler(BaseHandler):
    """ 所有api的统一处理handler """

    def post(self, class_name, methon_name):
        """ api的所有请求，反射处理 """
        # 动态加载handler的包
        # try:
        amod = __import__("handler." + class_name.lower(), fromlist=True)
        # except:
        #     self.write("访问出错，请检查您的api接口名称是否正确")
        #     return

        if hasattr(amod, methon_name):
            ret = getattr(amod, methon_name)
            rt = ret("sdf")
            self.finish(rt)
            return
        else:
            self.write("访问出错，请检查您的api接口名称是否正确")
            return

        self.write("调用了接口，class: %s, methon: %s" % (class_name, methon_name))

    def get(self, class_name, methon_name):
        self.write("访问非法")


class NofoundHandler(BaseHandler):
    def get(self):
        self.write("sdfsf")


def start_service():
    tornado.options.parse_command_line()
    app = tornado.web.Application(
        handlers=[
            (r"/", IndexHandler),
            (r"/api/(\w+)/(\w+)", ApiHandler),
            (".*", NofoundHandler),
        ],
        debug=settings.DEBUG
    )

    http_server = tornado.httpserver.HTTPServer(app)
    http_server.listen(options.port)
    tornado.ioloop.IOLoop.instance().start()
