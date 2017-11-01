<template>
    <div>
        <div class="mt40">
            <top-nav title="邀请分销商"></top-nav>
        </div>
        <!-- <div class="mt10 mt60 alert alert-success ml20 mr20">
            {{hintMsg}}
        </div> -->
        <div v-if="IsLogin==1" class="bg-white p20 text-center">
            <a href="/#/home">去首页</a>
        </div>
        <div v-if="IsLogin==0">
            <div class="p10 text-right" style="color:#bbbbbb">
                建材行业B2B解决方案
            </div>
            <div class="login-pic">
                <img src="/static/images/login.jpg">
            </div>
            <div class="bg-white pl40 pr40 pt10 pb10">
                <div class="login-row lh40 bb1 pb2">
                    <div class="left w40">
                        <img src="/static/images/username.png" />
                    </div>
                    <div class="ml40" style="margin-left:40px;">
                        <input v-model="username" class="w-fill b0 pl10" type="text" placeholder="请输入您的用户名">
                    </div>
                    <div class="clear"></div>
                </div>
                <div class="login-row lh40 pt10 bb1 pb2">
                    <div class="left w40">
                        <img src="/static/images/pwd.png" />
                    </div>
                    <div class="ml40" style="margin-left:40px;">
                        <input v-model="password" class="w-fill b0 pl10" type="password" placeholder="请输入您的密码">
                    </div>
                    <div class="clear"></div>
                </div>
            </div>
            <div class="login-bottom">
                <div class="text-center">
                    <input v-on:click="onLoginClick()" class="login-btn" type="button" value="登录" />
                </div>
                <div class="login-reg-area text-center">
                    <a v-on:click="gotoRegPage()" href="javascript:;">立即注册</a>
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
        gotoRegPage: function () {
            this.$router.push({ path: '/reg/' + this.UserSN_S });
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
                        me.hintMsg = "尚未登录，登录后将自动跳转供货商店铺";
                    }
                    if (data && data.IsLogin == 1 && data.Success == 1) {
                        me.$router.push({ path: '/shop/' + me.UserSN_S });
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
