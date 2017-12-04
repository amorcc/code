''' 设置内容存储 '''


class Settings():
    """存储《外星人入侵》的所有设置的类"""

    def __init__(self):
        """初始化游戏的设置"""
        # 屏幕设置
        self.screen_width = 1200
        self.screen_height = 800
        # self.bg_color = (0, 0, 205)
        self.bg_color = (230, 230, 230)

        # 飞船的设置
        self.ship_speed_factor = 1.5
        self.ship_limit = 1

        # 子弹设置
        self.bullet_speed_factor = 10
        self.bullet_width = 500
        self.bullet_height = 15
        self.bullet_color = 192, 14, 235
        # 允许最大的子弹数
        self.bullets_allowed = 30

        # 外星人设置
        self.alien_speed_factor = 1
        # 外星人撞到屏幕边缘时，外星人群向下移动的速度
        self.fleet_drop_speed = 10
        # fleet_direction为1表示向右移动，为-1表示向左移动
        self.fleet_direction = 1

        # 以什么样的速度加快游戏节奏
        self.speedup_scale = 1.1

        self.initialize_dynamic_settings()

    def initialize_dynamic_settings(self):
        """ 初始化随游戏进行而变化的设置 """
        self.ship_speed_factor = 1.5
        self.bullet_speed_factor = 3
        self.alien_speed_factor = 1

        # fleet_direction 为1 表示向右，为-1表示向左
        self.fleet_direction = 1

        # 记分
        self.alien_points = 50

    def increase_speed(self):
        """ 提高速度设置 """
        self.ship_speed_factor *= self.speedup_scale
        self.bullet_speed_factor *= self.speedup_scale
        self.alien_speed_factor *= self.speedup_scale
