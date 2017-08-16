<template>
    <div>
        <div class="mt40">
            <top-nav title="公司资料"></top-nav>
        </div>
        <div class="div-text mt10 mt60">
            <span class="red-line ml10">&nbsp;</span>
            邀请分销商二维码
        </div>
        <div class="mt20 bg-white lh40 fs16 p20" v-if="dataIn.IsOpenSupplier == 0">
            没有开通卖家，请到我的个人中心开通后重试。
        </div>
        <div class="mt20 bg-white lh40 fs16 p20" v-if="dataIn.IsOpenSupplier == 1 && dataIn.QRCode == ''">
            尚未生成邀请二维码。
            <a v-on:click="CreateQRCode()" herf="javascript:;">立即生成</a>
        </div>
        <div class="mt20 bg-white lh40 fs16 p20 join-me" v-if="dataIn.IsOpenSupplier == 1 && dataIn.QRCode != ''">
            <div>
                <img :src="dataIn.QRCode" />
            </div>
            <div class="pl20 pr20 lh40 text-center">
                <a v-on:click="CreateQRCode()" herf="javascript:;">重新生成</a>
            </div>
            <div class="pl20 pr20 lh20 text-left">
                1.长按二维码图片，选择保存图片
            </div>
            <div class="pl20 pr20 lh20 text-left">
                2.保存后可以将图片发送给分销商，邀请注册自动建立供销关系
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
            dataIn: {
            },
        }
    },
    mounted: function () {
        this.initPage();
    },
    methods: {
        initPage: function () {
            this.getCompanyInfo()
        },
        getCompanyInfo: function () {
            var me = this;
            var para = {};

            me.fetchData({
                cmd: '/api/Company/GetCompanyInfo',
                para: para,
                callback: function (data) {
                    me.dataIn = data;
                }
            });
        },
        CreateQRCode: function () {
            var me = this;
            var para = {};

            me.fetchData({
                cmd: '/api/Company/CreateInviteQRCode',
                para: para,
                callback: function (data) {
                    location.reload();
                }
            });
        },
    },
}
</script>
