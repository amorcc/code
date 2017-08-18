<template>
    <div>
        <div>
            <top-nav title="店铺页"></top-nav>
        </div>
        <div class="mt40 pro">
            <div style="position:relative">
                <img src="/static/images/shop_ad.jpg" class="w100" />
                <div class="shop-info-area">
                    <div class="shop-info-img">
                        <img src="/static/images/shop-logo.png" />
                    </div>
                    <div class="shop-info-other">
                        <div class="fs16 lh30 div-ellipsis" style="height:30px;">
                            这是公司名称
                        </div>
                        <div class="fs12 lh20">
                            <span class="glyphicon glyphicon-phone" aria-hidden="true"></span>
                            <span>13203856178</span>
                            <span>(微信：</span>
                            <span>13203856178)</span>
                        </div>
                        <div class="fs12 lh20 div-ellipsis" style="height:20px;div-ellipsis">
                            主营：板材等一系列的东西板材等一系列的东西板材等一系列的东西
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="lh30 p10 bg-white fs16 text-bold">
            全部商品
        </div>
        <buyer-footer></buyer-footer>
    </div>
</template>
<script>
import buyerFooter from './../common/BuyersFooter.vue'
import topNav from './../common/TopNav.vue';
export default {
    name: 'Shop',
    components: {
        buyerFooter,
        topNav
    },
    data: function () {
        return {
            UserSN_S: '',
            companyInfo: {},
        }
    },
    mounted: function () {
        this.initPage();
    },
    methods: {
        initPage: function () {
            var me = this;
            me.UserSN_S = me.$route.params.usersn;
            var para = {};
            para.UserSN_S = me.UserSN_S;

            me.fetchData({
                cmd: '/api/Company/RetailerGetCompanyInfo',
                para: para,
                callback: function (data) {
                    me.companyInfo = data;
                }
            });
        },
    },

}
</script>
