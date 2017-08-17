<template>
    <div id="login">
        <div class="p10 text-right" style="color:#bbbbbb">
            建材行业B2B解决方案
        </div>
        <div class="login-pic">
            <img src="/static/images/login.jpg">
        </div>
        <div class="bg-white pl40 pr40 pt10 pb10">
            <div class="login-row lh40 bb1 pb2">
                <div class="left w40">
                    <img src="/static/images/username.png">
                </div>
                <div class="ml40" style="margin-left:40px;">
                    <input v-model="username" class="w-fill b0 pl10" type="text" placeholder="请输入您的用户名">
                </div>
                <div class="clear"></div>
            </div>
            <div class="login-row lh40 pt10 bb1 pb2">
                <div class="left w40">
                    <img src="/static/images/pwd.png">
                </div>
                <div class="ml40" style="margin-left:40px;">
                    <input v-model="password" class="w-fill b0 pl10" type="password" placeholder="请输入您的用户名">
                </div>
                <div class="clear"></div>
            </div>
        </div>
        <div class="login-bottom">
            <div class="text-center">
                <input v-on:click="onLoginClick()" class="login-btn" type="button" value="登录" />
            </div>
            <div class="login-reg-area text-center">
                <a href="/#/reg/none">立即注册</a>
                <span>|</span>
                <a href="#">找回密码</a>
                <span>|</span>
                <a href="javascript:;" v-on:click="onLoginClick1()">立即体验</a>
            </div>
        </div>
    
    </div>
</template>
<script>
export default {
    name: 'login',
    data: function () {
        return {
            username: "",
            password: "",
            openid: '',
            access_token: '',
            type: '1',
        }
    },
    mounted: function () {
        // debugger;
        var me = this;
        var openid = me.$route.params.openid;
        var access_token = me.$route.params.access_token;
        var type = me.$route.params.type;
        if (openid && openid != 0) {
            me.openid = openid;
        }

        if (access_token && access_token != 0) {
            me.access_token = access_token;
        }

        if (type && type != 0) {
            me.type = type;
        }

    },
    methods: {
        onLoginClick: function () {
            var me = this;


            if (me.openid != '' && me.openid != 0 && me.access_token != '' && me.access_token != 0) {
                dialog({
                    title: '提示',
                    content: '是否绑定该微信自动登录系统？',
                    ok: function () {
                        var para = {};
                        para.username = me.username;
                        para.password = me.password;
                        para.openid = me.openid;
                        para.access_token = me.access_token;
                        me.login(para);
                    },
                    cancel: function () {
                        var para = {};
                        para.username = me.username;
                        para.password = me.password;
                        para.openid = '';
                        para.access_token = '';
                        me.login(para);
                    },
                }).showModal();
            } else {
                var para = {};
                para.username = me.username;
                para.password = me.password;
                para.openid = '';
                para.access_token = '';
                me.login(para);
            }
        },
        login: function (iPara) {
            var me = this;
            me.fetchData({
                cmd: '/api/userauth/login',
                para: iPara,
                callback: function (data) {
                    localStorage.setItem("token", data.Token);

                    switch (me.type) {
                        case '2':
                            me.$router.push({
                                path: '/sol/2'
                            });
                            break;
                        case '3':
                            me.$router.push({
                                path: '/pm'
                            });
                            break;
                        case '4':
                            me.$router.push({
                                path: '/amountprice'
                            });
                            break;
                        case '5':
                            me.$router.push({
                                path: '/company'
                            });
                            break;
                        default:
                            me.$router.push({
                                path: '/home'
                            });
                            break;
                    }

                }
            });
        },
        onLoginClick1: function () {
            var me = this;
            var para = {};
            para.username = '13203856178';
            para.password = '111111';

            me.fetchData({
                cmd: '/api/userauth/login',
                para: para,
                callback: function (data) {
                    localStorage.setItem("token", data.Token);
                    me.$router.push({
                        path: '/home'
                    });
                }
            });
        },
    },
}
</script>