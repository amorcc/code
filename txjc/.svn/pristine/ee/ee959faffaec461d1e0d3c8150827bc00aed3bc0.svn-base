<template>
    <div>
        <div class="bg-white mb10">
            <div class="lh40 ml10 mr10 bb1 text-bold">
                <span class="glyphicon glyphicon-th-large" aria-hidden="true"></span>
                立即注册
            </div>
            <div class="p10">
                <form class="form-horizontal ">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2 control-label remove-font-weight">手机号：</label>
                        <div class="col-sm-10">
                            <input v-model="dataIn.UserName" type="text" class="form-control" id="inputEmail3" placeholder="请填写您的手机号">
                        </div>
                    </div>
                </form>
                <form class="form-horizontal ">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2 control-label remove-font-weight">公司名称：</label>
                        <div class="col-sm-10">
                            <input v-model="dataIn.CompanyName" type="text" class="form-control" id="inputEmail3" placeholder="请填写您的公司或者店铺名称">
                        </div>
                    </div>
                </form>
                <form class="form-horizontal ">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2 control-label remove-font-weight">密码：</label>
                        <div class="col-sm-10">
                            <input v-model="dataIn.Password" type="password" class="form-control" id="inputEmail3" placeholder="请填写您的密码，最低6位">
                        </div>
                    </div>
                </form>
                <form class="form-horizontal ">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2 control-label remove-font-weight">确认密码：</label>
                        <div class="col-sm-10">
                            <input v-model="dataIn.Password1" type="password" class="form-control" id="inputEmail3" placeholder="请再次填写您的密码">
                        </div>
                    </div>
                </form>
            </div>
        </div>
        <div class="bg-white p10 text-center mb10 ">
            <div>
            </div>
            <div>
                <input v-on:click="onSubmit_Click()" type="button" value="立即注册" class="btn-fill-big yj" />
            </div>
        </div>
    </div>
</template>
<script>

export default {
    name: 'Reg',
    data: function () {
        return {
            dataIn: {
                UserName: '13203856178',
                CompanyName: '111',
                Password: '111111',
                Password1: '111111',
                RegSource: 0,
            },
        }
    },
    mounted: function () {
    },
    methods: {
        onSubmit_Click: function () {
            var me = this;
            if (me.dataIn.UserName.trim() == '') {
                me.showTips('请正确填写您的手机号', 'error');
                return false;
            }
            if (me.dataIn.UserName.trim().length != 11) {
                me.showTips('请正确填写您的手机号', 'error');
                return false;
            }

            if (me.dataIn.Password != me.dataIn.Password1) {
                me.showTips('两次密码输入不一致', 'error');
                return false;
            }

            if (me.dataIn.Password.length < 6) {
                me.showTips('密码最低6位，数字、字母组合', 'error');
                return false;
            }

            if (me.dataIn.CompanyName.trim() == '') {
                me.showTips('请正确填写您的公司名称或店铺名称', 'error');
                return false;
            }

            var para = {};
            para = me.dataIn;


            me.fetchData({
                cmd: '/api/UserAuth/Reg',
                para: para,
                callback: function (data) {
                    me.showTips('注册成功');
                    me.$router.push({ path: '/login/' });
                }
            });

        },
    },
}
</script>
