import logging

import tornado.httpserver
import tornado.ioloop
import tornado.options
import tornado.web

import log_config
from config import config
from handler_base import BaseHandler, IndexHandler, ApiHandler, NofoundHandler

from tornado.options import define, options
define("port", default=config['port'], help='run on the given port', type=int)

logger = logging.getLogger(__name__)


def start_web_service():
    tornado.options.parse_command_line()
    app = tornado.web.Application(
        handlers=[
            (r"/", IndexHandler),
            (r"/api/(\w+)/(\w+)", ApiHandler),
            (".*", NofoundHandler),
        ],
        debug=True
    )

    http_server = tornado.httpserver.HTTPServer(app)
    http_server.listen(options.port)
    tornado.ioloop.IOLoop.instance().start()


def Init():
    # 加载命令行配置
    log_config.init(config['port'], config['log_console'],
                    config['log_file'], config['log_file_path'], config['log_level'])

    # 启动web服务器
    start_web_service()


if __name__ == '__main__':
    Init()
    # print(config["debug"])
