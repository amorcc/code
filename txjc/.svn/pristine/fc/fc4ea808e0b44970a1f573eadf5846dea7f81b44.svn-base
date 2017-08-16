<template>
    <div>
        <div class="div-fill">
            登录并绑定微信
        </div>
        <div class="div-fill">
            <input class="input-text-big width-fill mb10" type="text" v-model="username" value="" placeholder="请输入您的用户名" />
            <input class="input-text-big width-fill mb10" type="password" v-model="password" value="" placeholder="请输入您的密码" />
            <input class="btn-fill-big" type="button" v-on:click="onLoginClick()" value="登录" />
            <input class="btn-fill-big" type="button" v-on:click="onLoginClick1()" value="卖家登录" />
            <a href="/#/reg">立即注册</a>
        </div>
    
    </div>
</template>
<script>


export default {
    name: 'WxLogin',
    data: function () {
        return {
            openid: '',
        }
    },

    mounted: function () {
        this.initPage();
    },
    methods: {
        initPage: function () {
            var me = this;
            me.openid = me.$route.params.openid;
        },
        onLoginClick: function () {

        },
    },

}
</script>
