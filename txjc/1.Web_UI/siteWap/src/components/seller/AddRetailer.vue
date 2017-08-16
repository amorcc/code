<template>
    <div>
        <div>
            <top-nav title="新增分销商"></top-nav>
        </div>
        <div class="mt40 bg-white p10 text-right">
    
            <div class="form-group">
                <input v-model="key" type="text" class="form-control" placeholder="请输入分销商邀请码">
            </div>
            <button v-on:click="onSearchSupplier_Click()" type="button" class="btn btn-default">查询</button>
        </div>
        <div class="bg-white mt10 p10">
            <div class="lh40 fs15 text-bold bb1">查询结果：</div>
            <div v-if="dataIn" class="container">
                <div class="row pt10">
                    <div class="col-xs-4 p0 text-right text-bold">
                        邀请码：
                    </div>
                    <div class="col-xs-8 p0">
                        {{dataIn.UserSN}}
                    </div>
                </div>
                <div class="row pt10">
                    <div class="col-xs-4 p0 text-right text-bold">
                        公司名称：
                    </div>
                    <div class="col-xs-8 p0">
                        {{dataIn.CompanyName}}
                    </div>
                </div>
                <div class="row pt10">
                    <div class="col-xs-4 p0 text-right text-bold">
                        联系电话：
                    </div>
                    <div class="col-xs-8 p0">
                        {{dataIn.CompanyPhone}}
                    </div>
                </div>
                <div class="row pt10">
                    <div class="col-xs-4 p0 text-right text-bold">
                        主营业务：
                    </div>
                    <div class="col-xs-8 p0">
                        {{dataIn.BusinessScope}}
                    </div>
                </div>
                <div class="row pt10 text-center">
                    <button v-on:click="onAddSupplier_Click()" type="button" class="btn btn-default">立即添加</button>
                </div>
            </div>
            <div v-if="!dataIn" class="fs14 lh40">
                没有该邀请码的供应商信息
            </div>
        </div>
    
        <buyer-footer></buyer-footer>
    </div>
</template>
<script>
import buyerFooter from './../common/BuyersFooter.vue'
import topNav from './../common/TopNav.vue';

export default {
    name: 'AddSupplier',
    components: {
        buyerFooter,
        topNav
    },
    data: function () {
        return {
            orderStatus: 0,
            key: '',
            dataIn: null,
            selectedClass: {
                'border-bottom': "2px solid red",
            }
        }
    },
    mounted: function () {
        this.initPage(1);
    },
    methods: {
        initPage: function (iPageIndex) {
        },
        onSearchSupplier_Click: function () {
            var me = this;
            var para = {};
            para.UserSN = me.key;

            me.fetchData({
                cmd: '/api/Retailer/SearchSupplier',
                para: para,
                callback: function (data) {
                    me.dataIn = data;
                }
            });
        },
        onAddSupplier_Click: function () {
            var me = this;
            var para = {};
            para.UserSN_R = me.dataIn.UserSN;

            me.fetchData({
                cmd: '/api/Supplier/AddRetailer',
                para: para,
                callback: function (data) {
                    me.$router.push({ path: '/myretailer' });
                }
            });
        },
    },
}
</script>
