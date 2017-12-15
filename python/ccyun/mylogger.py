import logging
import logging.config
"""logging的使用示例
"""

if __name__ == '__main__':

    # 比较复杂的用法
    LOGGING = 
        # 版本，总是1
        'version': 1,
        'disable_existing_loggers': True,
        'formatters': {
            'verbose': {'format': '%(levelname)s %(asctime)s %(module)s %(process)d %(thread)d %(message)s'},
            'simple': {'format': '%(levelname)s %(message)s\n'},
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
    logger = logging.getLogger('mylogger')
    logger.info('Hello')
