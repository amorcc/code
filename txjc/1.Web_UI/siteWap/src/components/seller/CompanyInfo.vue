<template>
    <div>
        <div class="mt40">
            <top-nav title="公司资料"></top-nav>
        </div>
    
        <!-- 公司资料 -->
        <div class="div-text mt10 mt60">
            <span class="red-line ml10">&nbsp;</span>
            公司资料
        </div>
        <div class="bg-white mt20">
            <div class="p10">
                <form class="form-horizontal ">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2 control-label remove-font-weight">公司名称：</label>
                        <div class="col-sm-10">
                            <input v-model="dataIn.CompanyName" type="email" class="form-control" id="inputEmail3" placeholder="公司名称">
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2 control-label remove-font-weight">店铺名称：</label>
                        <div class="col-sm-10">
                            <input v-model="dataIn.ShopName" type="email" class="form-control" id="inputEmail3" placeholder="店铺名称">
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2 control-label remove-font-weight">联系方式：</label>
                        <div class="col-sm-10">
                            <input v-model="dataIn.CompanyPhone" type="email" class="form-control" id="inputEmail3" placeholder="手机号或者固话号">
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2 control-label remove-font-weight">微信号：</label>
                        <div class="col-sm-10">
                            <input v-model="dataIn.WechatNumber" type="email" class="form-control" id="inputEmail3" placeholder="微信号">
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2 control-label remove-font-weight">主营品类：</label>
                        <div class="col-sm-10">
                            <input v-model="dataIn.BusinessScope" type="email" class="form-control" id="inputEmail3" placeholder="主营品类信息">
                        </div>
                    </div>
                </form>
                <div class="lh40 ml10 mr10 bb1">
                    <span class="glyphicon glyphicon-th-large" aria-hidden="true"></span>
                    添加公司Logo
                </div>
                <div class="lh20 remove-font-weight p10 c-ccc fs12">(10M以下,默认只选择最前一张图片)</div>
                <div>
                    <file-upload title="" name="file" :imgUrl="imgUrl" :post-action="postAction" :put-action="postAction" :multiple="false" :files="files" :serverFiles="serverFiles" mode="html5" ref="upload">
                    </file-upload>
                </div>
                <div class="bg-white p10 text-center mb10 ">
                    <input v-on:click="onSubmit_Click()" type="button" value="提交" class="btn-fill-big yj " />
                </div>
            </div>
        </div>
        <div class="div-text mt10 mt20">
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
        <div class="mb60">
        </div>
        <buyer-footer></buyer-footer>
    </div>
</template>
<script>
import buyerFooter from './../common/BuyersFooter.vue'
import topNav from './../common/TopNav.vue';
import fileUpload from './../fileupload/FileUpload.vue';

export default {
    name: 'preview',
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
            imgUrl: "",
            serverFiles: [],
            dataIn: {
            },
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

                    if (me.dataIn.LogoImgUrl && me.dataIn.LogoImgUrl != '') {
                        me.serverFiles.push(me.dataIn.LogoImgUrl);
                    }
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
        onSubmit_Click: function () {
            var me = this;
            // if (me.serverFiles.length == 0) {
            //     me.showTips('请添加公司LOGO', 'error');
            // }
            console.log(me.serverFiles);
            if (me.serverFiles.length > 0) {
                me.dataIn.LogoImgUrl = me.serverFiles[0];
            }

            var para = me.dataIn;
            me.fetchData({
                cmd: '/api/Company/UpdateCompanyInfo',
                para: para,
                callback: function (data) {
                }
            });
        },
    },
}
</script>
