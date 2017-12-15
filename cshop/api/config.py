# 数据库配置
db_config = dict(
    ip='127.0.0.1',
    port=3306,
    db_name='test',
    username='root',
    password='ejiang',
    echo=True,
)

# 站点相关配置以及tornado的相关参数
config = dict(
    debug=True,
    log_level="DEBUG",
    log_console=True,
    log_file=True,
    log_file_path="logs/log",  # 末尾自动添加 @端口号.txt_日期
    compress_response=True,
    xsrf_cookies=True,
    cookie_secret="kjsdhfweiofjhewnfiwehfneiwuhniu",
    login_url="/auth/login",
    port=8888,
    max_threads_num=500,
    # database=database_config,
    # redis_session=redis_session_config,
    # session_keys=session_keys,
    # 是否为主从节点中的master节点, 整个集群有且仅有一个,(要提高可用性的话可以用zookeeper来选主,该项目就暂时不做了)
    master=True,
    navbar_styles={"inverse": "魅力黑", "default": "优雅白"},  # 导航栏样式
    default_avatar_url="identicon",
    application=None,  # 项目启动后会在这里注册整个server，以便在需要的地方调用，勿修改
)
