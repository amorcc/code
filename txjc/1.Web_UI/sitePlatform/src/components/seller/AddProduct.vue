<template>
    <div>
        <div class="mb40">
            <top-nav :title="title"></top-nav>
        </div>
        <div class="bg-white mb10">
            <div class="lh40 ml10 mr10 bb1 text-bold">
                <span class="glyphicon glyphicon-th-large" aria-hidden="true"></span>
                商品基础信息
            </div>
            <div class="p10">
                <form class="form-horizontal ">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2 control-label remove-font-weight">商品名称：</label>
                        <div class="col-sm-10">
                            <input v-model="dataIn.ProName" type="email" class="form-control" id="inputEmail3" placeholder="商品名称">
                        </div>
                    </div>
                </form>
                <form class="form-horizontal ">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2 control-label remove-font-weight">商品状态：</label>
                        <div class="col-sm-10">
                            <select v-model="dataIn.Status" class="form-control">
                                <option value="1">上架</option>
                                <option value="0">下架</option>
                            </select>
                        </div>
                    </div>
                </form>
                <div class="checkbox">
                    <label>
                        <input v-model="dataIn.BillNeeded" type="checkbox" value=""> 是否提供发票
                    </label>
                </div>
            </div>
        </div>
        <div class="bg-white mb10">
            <div class="lh40 ml10 mr10 bb1 text-bold">
                <span class="glyphicon glyphicon-th-large" aria-hidden="true"></span>
                库存与价格
            </div>
            <div class="p10">
                <form class="form-horizontal ">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2 control-label remove-font-weight">虚拟库存：</label>
                        <div class="col-sm-10">
                            <input v-model="dataIn.ProAmount" type="email" class="form-control" id="inputEmail3" placeholder="虚拟库存">
                        </div>
                    </div>
                </form>
                <form class="form-inline">
                    <div class="form-group">
                        <label class="sr-only" for="exampleInputAmount">Amount (in dollars)</label>
                        <div class="input-group">
                            <div class="input-group-addon">￥</div>
                            <input v-model="dataIn.ProPrice" type="text" class="form-control" id="exampleInputAmount" placeholder="商品价格">
                        </div>
                    </div>
                </form>
            </div>
        </div>
        <div class="bg-white mb10">
            <div class="lh40 ml10 mr10 bb1">
                <span class="glyphicon glyphicon-th-large" aria-hidden="true"></span>
                添加产品图片
            </div>
            <div class="lh20 remove-font-weight p10 c-ccc fs12">(10M以下，第一张默认为主图片,最低选择一张)</div>
            <div>
                <file-upload title="" name="file" :imgUrl="imgUrl" :post-action="postAction" :put-action="postAction" :multiple="false" :files="files" :serverFiles="serverFiles" mode="html5" ref="upload">
                </file-upload>
            </div>
        </div>
        <div class="bg-white mb10">
            <div class="lh40 ml10 mr10 bb1 text-bold">
                <span class="glyphicon glyphicon-th-large" aria-hidden="true"></span>
                商品介绍
            </div>
            <div class="p10">
                <textarea v-model="dataIn.Desc" class="form-control" rows="3"></textarea>
            </div>
        </div>
        <div class="bg-white p10 text-center mb10 ">
            <input v-on:click="onSubmit_Click()" type="button" value="发布" class="btn-fill-big yj" />
        </div>
    </div>
</template>
<script>
import buyerFooter from './../common/BuyersFooter.vue'
import topNav from './../common/TopNav.vue';
import fileUpload from './../fileupload/FileUpload.vue';

export default {
    name: 'addProduct',
    components: {
        buyerFooter,
        topNav,
        fileUpload,
    },
    props: {
        files: {
            type: Array,
            default: () => [],
        },
    },
    data: function () {
        return {
            upload: {},
            postAction: '',
            title: '发布产品',
            serverFiles: [],
            ProId: 0,
            dataIn: {
                Status: 0,
            },
            imgUrl: "",
        }
    },
    mounted: function () {
        this.imgUrl = this.API_URL;
        this.postAction = this.API_URL + '/api/UploadFile/Upload';
        this.upload = this.$refs.upload.$data;
        this.initPage();
    },
    methods: {
        initPage: function () {
            var me = this;
            me.ProId = me.$route.params.proid;

            if (me.ProId == 0) {
                me.title = '发布产品';
            }
            else {
                me.title = '产品信息修改';
                var para = {};
                para.ProId = me.ProId;

                me.fetchData({
                    cmd: '/api/Product/GetProductInfo',
                    para: para,
                    callback: function (data) {
                        me.dataIn = data;

                        if (data.ProImage && data.ProImage.length > 0) {
                            me.serverFiles.push(data.ProImage);
                        }

                        if (data.ProImages && data.ProImages.length > 0) {
                            //me.serverFiles.push(data.ProImages.split(","));
                            var images = data.ProImages.split(',');

                            images.forEach(function (t) {
                                me.serverFiles.push(t);
                            });
                        }
                    }
                });
            }

        },
        onSubmit_Click: function () {
            var me = this;
            //string iProName, int iBillNeeded, int iStatus, int iAmount, decimal iPrice, List<string> iImages, string iDesc
            if (!(me.dataIn.ProName) || me.dataIn.ProName == '') {
                me.showTips('请填写商品名称', 'error');
                return false;
            }

            if (me.checkInt(me.dataIn.ProAmount, 0) == false) {
                me.showTips('请输入正确的库存', 'error');
                return false;
            }

            if (me.checkMoney(me.dataIn.ProPrice, 0.01) == false) {
                me.showTips('请输入正确的价格,商品价格不能为0', 'error');
                return false;
            }

            if (me.serverFiles.length < 1) {
                me.showTips('最低添加一个商品图片', 'error');
                return false;
            }

            var para = {};
            para.ProId = me.ProId;
            para.Images = me.serverFiles.join(',');
            para.Status = me.dataIn.Status;
            para.ProName = me.dataIn.ProName;
            para.BillNeeded = me.dataIn.BillNeeded ? 1 : 0;
            para.Amount = me.dataIn.ProAmount;
            para.Price = me.dataIn.ProPrice;
            para.Desc = me.dataIn.Desc;

            me.fetchData({
                cmd: '/api/Product/ModifyProduct',
                para: para,
                callback: function (data) {
                    me.$router.push({ path: '/pm/' });
                }
            });
        },
    },
}
</script>
