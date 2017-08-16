<template>
    <div>
        新增收货地址
        <div>
            <select v-on:change="level0Change()" v-model="provinceCode">
                <option value="-1">请选择</option>
                <option v-for="item0 of areaLevel0" :value="item0.Code">{{item0.Name}}</option>
            </select>
            <select v-on:change="level1Change()" v-model="cityCode">
                <option value="-1">请选择</option>
                <option v-for="item0 of areaLevel1" :value="item0.Code" >{{item0.Name}}</option>
            </select>
            <select  v-model="countyCode">
                <option value="-1">请选择</option>
                <option v-for="item0 of areaLevel2" :value="item0.Code" >{{item0.Name}}</option>
            </select>
        </div>
        <div>
            <input v-model="AddressDetail" type="text" placeholder="请填写详细地址信息" />
        </div>
        <div>
            <input v-model="Receiver" type="text" placeholder="请填写收货人姓名" />
        </div>
        <div>
            <input v-model="Phone" type="text" placeholder="请填写收货人手机号" />
        </div>
        <div>
            <input type="button" v-on:click="submit_Click()" value="新增"/>
        </div>
    </div>
</template>
<script>
export default {
    name: 'AddAddress',
    data:function(){
        return {
            areaLevel0 : [],
            areaLevel1 : [],
            areaLevel2 : [],
            provinceCode : -1,
            cityCode : -1,
            countyCode : -1,
            Phone :'',
            Receiver: '',
            AddressDetail : '',
        }
    },
    mounted: function() {
        this.initPage();
    },
    methods : {
        initPage:function(){
            var me = this;

            var para = {};
            para.pcode = 0;
            this.fetchData({
                cmd: '/api/AreaMng/GetAreaInfo',
                para: para,
                callback: function(data) {
                    me.areaLevel0 = data;
                }
            });
        },
        level0Change:function(){
            var me = this;

            var para = {};
            para.pcode = me.provinceCode;
            this.fetchData({
                cmd: '/api/AreaMng/GetAreaInfo',
                para: para,
                callback: function(data) {
                    me.areaLevel1 = data;
                    me.cityCode = -1;
                    me.countyCode = -1;
                }
            });
        },
        level1Change:function(){
            var me = this;

            var para = {};
            para.pcode = me.cityCode;
            this.fetchData({
                cmd: '/api/AreaMng/GetAreaInfo',
                para: para,
                callback: function(data) {
                    me.areaLevel2 = data;
                    me.countyCode = -1;
                }
            });
        },
        submit_Click:function(){
            var me = this;
            if(me.countyCode == -1){
                me.showTips('请选择地址的区域信息!','error');
                return false;
            }

            if(me.AddressDetail == ''){
                me.showTips('请填写详细地址','error');
                return false;
            }

            if(me.Receiver == ''){
                me.showTips('请填写收货人姓名','error');
                return false;
            }

            if(me.Phone== ''){
                me.showTips('请填写收货人手机号','error');
                return false;
            }

            var para = {};
            para.AddressDetail = me.AddressDetail;
            para.Receiver = me.Receiver;
            para.Phone = me.Phone;
            para.Zone = me.countyCode;
            para.Province = me.provinceCode;
            para.City = me.cityCode;


            this.fetchData({
                cmd: '/api/Address/UpdateAddress',
                para: para,
                callback: function(data) {
                    me.areaLevel2 = data;
                    me.countyCode = -1;
                }
            });


        },
    },

}
</script>
