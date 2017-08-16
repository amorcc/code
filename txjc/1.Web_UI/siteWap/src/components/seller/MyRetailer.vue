<template>
    <div>
        <div>
            <top-nav title="我的零售商"></top-nav>
        </div>
        <div class="mt40 bg-white p10 text-right">
            <a href="/#/addretailer">新增零售商</a>
        </div>
        <div class="mt10 bg-white p10 pb10">
            <div v-for="item of dataIn.table" class="bb1">
                <div class="row p0 lh20 mb10" style="padding-top:10px; padding-bottom:10px;">
                    <div class="col-xs-4 p0">
                        <span class="glyphicon glyphicon-glass fs12" aria-hidden="true"></span>
                        <span class="text-bold">分销商：</span>
                    </div>
                    <div class="col-xs-8 p0">
                        <span>{{item.CompanyName}}</span>
                    </div>
                </div>
                <div class="row p0">
    
                    <div class="col-xs-4 p0">
                        <span class="text-bold">合作时间：</span>
                    </div>
                    <div class="col-xs-8 p0">
                        <span>{{item.DateAdded}}</span>
                    </div>
    
                </div>
                <div class="lh30 text-right">
                    <a v-on:click="onRemoveSupplier_Clcik(item)" href="javascript:;">停止合作</a>
                </div>
    
                <i class="clearfix" />
            </div>
            <div v-if="dataIn.table.length == 0">
                未查询到供货商数据
            </div>
        </div>
    
        <div v-if="PageIndex < dataIn.TotalPages" v-on:click="nextPage_Click()" class="show-more m10 bg-white border text-center">
            点击查看更多
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
            orderStatus: 0,
            PageIndex: 1,
            dataIn: {
                table: [],
                TotalRows: 0,
                TotalPages: 1,
            },
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
            this.getMySupplierInfo(iPageIndex)
        },
        nextPage_Click: function () {
            var me = this;
            me.PageIndex++;
            me.getMySupplierInfo(me.PageIndex);
        },
        getMySupplierInfo: function (iPageIndex) {
            var me = this;
            var para = {};
            para.PageIndex = iPageIndex || 1;
            para.PageSize = me.PAGE_SIZE;

            me.fetchData({
                cmd: '/api/Supplier/GetMyRetailer',
                para: para,
                callback: function (data) {
                    me.dataIn.TotalRows = data.TotalRows;
                    me.dataIn.TotalPages = data.TotalPages;
                    // me.dataIn.table = data.Data;

                    if (data && data.Data) {
                        for (var key in data.Data) {
                            me.dataIn.table.push(data.Data[key]);
                        }
                    }
                }
            });
        },
        onRemoveSupplier_Clcik: function (item) {
            var me = this;

            dialog({
                title: '提示',
                content: '您确认要停止合作吗？停止合作后，您将无法看到该卖家发布的商品',
                ok: function () {
                    var para = {};
                    para.Id = item.Id;

                    me.fetchData({
                        cmd: '/api/Retailer/RemoveSupplier',
                        para: para,
                        callback: function (data) {
                            me.getMySupplierInfo(1);
                        }
                    });
                },
                cancel: function () { },
            }).showModal();
        },
    },
}
</script>
