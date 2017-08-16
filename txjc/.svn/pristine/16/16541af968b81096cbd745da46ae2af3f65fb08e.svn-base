<template>
    <nav class="topNav navbar navbar-default navbar-fixed-top">
        <div class="container">
            <div class="row" style="height:40px;">
                <a v-on:click="onBackOff_Click()" href="javascript:;" class="returnBtn">
                    <i class="back"></i>
                </a>
                <div class="title">
                    {{title}}
                </div>
                <a href="#" class="menuBtn">
                    <i class="menu"></i>
                </a>
            </div>
        </div>
    </nav>
</template>

<script>
export default {
    name: 'TopNav',
    props: ['title'],
    data: function () {
        return {
        }
    },
    mounted: function () {
    },
    methods: {
        onBackOff_Click: function () {
            history.go(-1);
        },
    },
}
</script>
