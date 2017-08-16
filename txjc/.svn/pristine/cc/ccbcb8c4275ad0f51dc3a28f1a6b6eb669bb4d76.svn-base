<template>
    <div>
        <div class="mt40">
            <top-nav title="添加供货商"></top-nav>
        </div>
        <div class="mt10 mt60 alert alert-success ml20 mr20">
            {{hintMsg}}
        </div>
        <div v-if="IsLogin==1" class="bg-white p20 text-center">
            <a href="/#/home">去首页</a>
        </div>
        <div v-if="IsLogin==0">
            <div class="bg-white">
                <div class="username-pwd">
                    <img src="/static/images/username.png">
                    <input v-model="username" type="text" placeholder="请输入您的用户名">
                </div>
                <div class="username-pwd" style="border:none;">
                    <img src="/static/images/pwd.png">
                    <input v-model="password" type="password" placeholder="请输入您的密码">
                </div>
            </div>
            <div class="login-bottom">
                <div class="text-center">
                    <input v-on:click="onLoginClick()" class="login-btn" type="button" value="登录" />
                </div>
                <div class="login-reg-area text-center">
                    <a href="/#/reg">立即注册</a>
                    <span>|</span>
                    <a href="#">找回密码</a>
                    <span>|</span>
                    <a href="javascript:;" v-on:click="onLoginClick1()">立即体验</a>
                </div>
            </div>
        </div>
        <buyer-footer></buyer-footer>
    </div>
</template>
<script>
import buyerFooter from './../common/BuyersFooter.vue'
import topNav from './../common/TopNav.vue';

export default {
    name: 'preview',
    components: {
        buyerFooter,
        topNav
    },
    data: function () {
        return {
            hintMsg: '正在验证信息，请稍候……',
            IsLogin: 1,
            username: "",
            password: "",
            UserSN_S: "",
        }
    },
    mounted: function () {
        this.initPage();
    },
    methods: {
        initPage: function () {
            var me = this;
            me.UserSN_S = me.$route.params.usersn;
            me.joinMe();
        },
        joinMe: function () {
            var me = this;
            var para = {};
            para.UserSN_S = me.UserSN_S;

            me.fetchData({
                cmd: '/api/Company/joinme',
                para: para,
                callback: function (data) {
                    if (data && data.IsLogin == 0) {
                        me.IsLogin = 0;
                        me.hintMsg = "尚未登录，登录后将自动加入该供货商";
                    }
                    if (data && data.IsLogin == 1 && data.Success == 1) {
                        me.$router.push({ path: '/mysupplier' });
                    }
                }
            });
        },
        onLoginClick: function () {
            var me = this;
            var para = {};
            para.username = me.username;
            para.password = me.password;
            para.openid = me.openid;
            para.access_token = me.access_token;

            this.fetchData({
                cmd: '/api/userauth/login',
                para: para,
                callback: function (data) {
                    localStorage.setItem("token", data.Token);
                    me.IsLogin = 1;
                    me.hintMsg = '正在验证信息，请稍候……';
                    me.joinMe();
                }
            });
        },
    },
}
</script>
