<template>
    <div>
        <div>
            <top-nav title="我的供货商"></top-nav>
        </div>
        <div class="mt40 bg-white p10 pb10">
            <div v-for="item of dataIn.table" class="bb1">
                <div class="row p0 lh20 mb10" style="padding-top:10px; padding-bottom:10px;">
                    <div class="col-xs-4 p0">
                        <span class="glyphicon glyphicon-glass fs12" aria-hidden="true"></span>
                        <span class="text-bold">供货商：</span>
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
                    <a href="javascript:;">停止合作</a>
                </div>
    
                <i class="clearfix" />
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
            orderStatus: 0,
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
        getMySupplierInfo: function (iPageIndex) {
            var me = this;
            var para = {};
            para.PageIndex = iPageIndex || 1;
            para.PageSize = me.PAGE_SIZE;

            me.fetchData({
                cmd: '/api/Retailer/GetMySupplier',
                para: para,
                callback: function (data) {
                    me.dataIn.TotalRows = data.TotalRows;
                    me.dataIn.TotalPages = data.TotalPages;
                    me.dataIn.table = data.Data;
                }
            });
        },
    },
}
</script>
