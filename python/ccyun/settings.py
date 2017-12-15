import os
import logging
import logging.config

from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker, scoped_session

PACKAGE_NAME = 'ccyun'


HOST = '127.0.0.1'
PORT = 8888
# 是否调试模式
DEBUG = True
# 代码修改时, 是否自动重启
AUTO_RELOAD = True if DEBUG else False

###########################################
# 数据库相关
DB_CON_STR = "mysql+pymysql://root:ejiang@127.0.0.1:3306/test"
ENGINE = create_engine(DB_CON_STR, echo=True)
SESSION_FACTORY = sessionmaker(bind=ENGINE)
DB_SESSION = scoped_session(SESSION_FACTORY)

###########################################
# 日志相关
# 当前目录所在路径
BASE_PATH = os.path.abspath(os.path.dirname(__file__))

# 日志所在目录
LOG_PATH = os.path.join(BASE_PATH, 'logs')
if not os.path.exists(LOG_PATH):
    # 创建日志文件夹
    os.makedirs(LOG_PATH)

# 创建一个logger
logger = logging.getLogger('mylogger')
# logger.setLevel(logging.DEBUG)

# 比较复杂的用法
LOGGING = {
    # 版本，总是1
    'version': 1,
    'disable_existing_loggers': True,
    'formatters': {
        'verbose': {'format': '%(levelname)s %(asctime)s %(module)s %(process)d %(thread)d %(message)s'},
        'simple': {'format': '%(levelname)s %(message)s'},
        'default': {
            'format': '%(asctime)s %(message)s',
            'datefmt': '%Y-%m-%d %H:%M:%S'
        }
    },
    'handlers': {
        'null': {
            'level': 'DEBUG',
            'class': 'logging.NullHandler',
        },
        'console': {
            'level': 'DEBUG',
            'class': 'logging.StreamHandler',
            'formatter': 'default'
        },
        'file': {
            'level': 'INFO',
            # TimedRotatingFileHandler会将日志按一定时间间隔写入文件中，并
            # 将文件重命名为'原文件名+时间戮'这样的形式
            # Python提供了其它的handler，参考logging.handlers
            'class': 'logging.handlers.TimedRotatingFileHandler',
            'formatter': 'default',
            # 后面这些会以参数的形式传递给TimedRotatingFileHandler的
            # 构造器

            # filename所在的目录要确保存在
            'filename': os.path.join(LOG_PATH, 'server.log'),
            # 每5分钟刷新一下
            'when': 'M',
            'interval': 1,
            'encoding': 'utf8',
        }
    },
    'loggers': {
        # 定义了一个logger
        'mylogger': {
            'level': 'DEBUG',
            'handlers': ['console', 'file'],
            'propagate': True
        }
    }
}
logging.config.dictConfig(LOGGING)
