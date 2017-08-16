<template>
    <div>
        <div class="div-fill">
            欢迎登录系统{{openid}}
        </div>
        <div class="div-fill">
            <input class="input-text-big width-fill mb10" type="text" v-model="username" value="" placeholder="请输入您的用户名" />
            <input class="input-text-big width-fill mb10" type="password" v-model="password" value="" placeholder="请输入您的密码" />
            <input class="btn-fill-big" type="button" v-on:click="onLoginClick()" value="登录" />
            <input class="btn-fill-big" type="button" v-on:click="onLoginClick1()" value="卖家登录" />
            <a href="/#/reg">立即注册</a>
        </div>
        <div class="div-fill">
            以下内容需要美工做称宣传图片
        </div>
        <div class="div-fill">
            系统特色1：私密B2B
        </div>
        <div class="div-fill">
            系统特色2：极简操作 ，关注核心
        </div>
        <div class="div-fill">
            系统特色3：云平台，节约硬件投入和运营成本
        </div>
        <div class="div-fill">
            系统特色4：针对建材销售行业
        </div>
    </div>
</template>
<script>
export default {
    name: 'hello',
    data: function () {
        return {
            "username": "cc",
            "password": "111111",
            'openid': '',
            'access_token': '',
        }
    },
    mounted: function () {
        var me = this;
        var openid = me.$route.params.openid;
        var access_token = me.$route.params.access_token;
        if (openid && openid != 0) {
            me.openid = openid;
        }

        if (access_token && access_token != 0) {
            me.access_token = access_token;
        }

    },
    methods: {
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
                    me.$router.push({ path: '/home' });
                }
            });
        },
        onLoginClick1: function () {
            var me = this;
            var para = {};
            para.username = 'xzbc';
            para.password = me.password;

            this.fetchData({
                cmd: '/api/userauth/login',
                para: para,
                callback: function (data) {
                    localStorage.setItem("token", data.Token);
                    me.$router.push({ path: '/home' });
                }
            });
        },
    },
}
</script>
