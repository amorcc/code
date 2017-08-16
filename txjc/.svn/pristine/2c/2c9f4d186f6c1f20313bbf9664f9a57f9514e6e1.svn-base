<template>
    <div>
        <div class="mt40">
            <top-nav title="个人中心"></top-nav>
        </div>
        <div class="user-area">
            <div class="container ">
                <div class="row">
                    <div class="user-pic">
                        <img src="/static/images/newDefaul.png" />
                    </div>
                    <div class="user-right">
                        <div class="user-name">
                            <span>{{dataIn.user.UserName}}</span>
                            <a class="ml10" href="javascipt">
                                <span class="glyphicon glyphicon glyphicon-envelope" aria-hidden="true"></span>3</a>
                        </div>
                        <div class="user-company">
                            {{dataIn.user.CompanyName}}
                        </div>
                        <div class="user-btn">
                            <a class="mr10" href="javascript:;">修改密码</a>
                            <a v-on:click="onLogOut_Click()" href="javascript:;">退出登录</a>
                        </div>
                        <div class="fs12 text-right lh30">
                            <span>(邀请码：{{dataIn.user.UserSN}})</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="div-text mt10">
            <span class="red-line ml10">&nbsp;</span>
            我是买家
        </div>
        <div class="quick-menu-home pt10 pb10">
            <div class="container div-full">
                <div class="row">
                    <template v-for="menu in dataIn.menu">
                        <div v-if="menu.Role == 22" class="col-xs-3 p0 mt10 btn-menu">
                            <a :href="menu.Url">
                                <img :src="menu.Icon" />
                                <div class="div-text">{{menu.MenuName}}</div>
                            </a>
                        </div>
                    </template>
                </div>
            </div>
        </div>
        <div class="div-text mt10">
            <span class="red-line ml10">&nbsp;</span>
            我是卖家
        </div>
        <div v-if="dataIn.user.IsOpenSupplier == 0" class="bg-white p10 mt10">
            尚未开通卖家服务。
            <a v-on:click="onApplyOpenSupplier()" href="javascirpt:;">立即申请开通</a>
        </div>
    
        <div v-if="dataIn.user.IsOpenSupplier == 1" class="quick-menu-home pt10 pb10">
            <div class="container div-full">
                <div class="row">
                    <template v-for="menu in dataIn.menu">
                        <div v-if="menu.Role == 21" class="col-xs-3 p0 mt10 btn-menu">
                            <a :href="menu.Url">
                                <img :src="menu.Icon" />
                                <div class="div-text">{{menu.MenuName}}</div>
                            </a>
                        </div>
                    </template>
                </div>
            </div>
        </div>
        <div class="div-text mt10">
            <span class="red-line ml10">&nbsp;</span>
            系统设置
        </div>
    
        <div class="quick-menu-home pt10 pb10">
            <div class="container div-full">
                <div class="row">
                    <template v-for="menu in dataIn.menu">
                        <div v-if="menu.Role == 301" class="col-xs-3 p0 mt10 btn-menu">
                            <a :href="menu.Url">
                                <img :src="menu.Icon" />
                                <div class="div-text">{{menu.MenuName}}</div>
                            </a>
                        </div>
                    </template>
                </div>
            </div>
        </div>
    
        <div class="mb80"></div>
        <buyer-footer></buyer-footer>
    </div>
</template>
<script>

import buyerFooter from './common/BuyersFooter.vue'
import topNav from './common/TopNav.vue';

export default {
    name: 'MyCenter',
    components: {
        buyerFooter,
        topNav
    },
    data: function () {
        return {
            dataIn: {
                user: {

                },
                menu: [],
            },
        }
    },

    mounted: function () {
        this.initPage();
    },
    methods: {
        initPage: function () {
            var me = this;
            var iPara = {};

            me.fetchData({
                cmd: '/api/UserAuth/GetUserInfo',
                para: iPara,
                callback: function (data) {
                    me.dataIn.user = data;

                    me.fetchData({
                        cmd: '/api/UserAuth/GetUserMenu',
                        para: iPara,
                        callback: function (data) {
                            me.dataIn.menu = data;
                        }
                    });
                }
            });
        },
        onLogOut_Click: function () {
            var me = this;
            var iPara = {};

            this.fetchData({
                cmd: '/api/UserAuth/LogOut',
                para: iPara,
                callback: function (data) {
                    me.$router.push({ path: '/login' });
                }
            });
        },
        onApplyOpenSupplier: function () {
            var me = this;

            dialog({
                title: '提示',
                content: '您确认要申请开通卖家服务吗？',
                ok: function () {
                    var para = {};

                    me.fetchData({
                        cmd: "/api/UserAuth/ApplyOpenSupplier",
                        para: para,
                        callback: function (data) {
                            me.showTips('申请已经受理，下次登录时，将为您开通卖家服务!');
                        }
                    });
                },
                cancel: function () { },
            }).showModal();

        },
    },

}
</script>
