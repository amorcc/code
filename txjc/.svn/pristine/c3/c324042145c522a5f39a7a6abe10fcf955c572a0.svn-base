<template>
    <div>
        <div>
            <top-nav title="支付方式配置"></top-nav>
        </div>
        <div class="mt60 bg-white p20">
            <div class="lh30 text-bold fs14">
                请选择支持的支付方式
            </div>
            <div v-for="item in table" class="bb1-dashed pb10">
                <div class="checkbox">
                    <label>
                        <input v-on:change="openChange(item)" v-model="item.IsOpen" type="checkbox"> {{item.PayTypeDesc}}
                    </label>
                </div>
                <div class="ml20 fs12 c-ccc">
                    {{item.Memo}}
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
    name: 'SetPayType',
    components: {
        buyerFooter,
        topNav
    },
    data: function () {
        return {
            table: [],
        }
    },
    mounted: function () {
        this.initPage();
    },
    methods: {
        initPage: function () {
            var me = this;
            me.getSupplierPayInfo();
        },
        getSupplierPayInfo: function () {
            var me = this;
            var para = {};

            this.fetchData({
                cmd: '/api/Pay/GetSupplierPayType',
                para: para,
                callback: function (data) {
                    me.table = data;
                }
            });
        },
        openChange: function (item) {
            var me = this;
            var para = {};
            para.IsOpen = item.IsOpen ? 1 : 0;
            para.PayTypeId = item.SysPayTypeId;

            this.fetchData({
                cmd: '/api/Pay/SetSupplierPayType',
                para: para,
                callback: function (data) {

                }
            });
            //alert(item.Memo+','+item.SysPayTypeId+item.IsOpen);
        },
    },
}
</script>
