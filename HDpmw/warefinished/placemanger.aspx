<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="placemanger.aspx.cs" Inherits="HDpmw.warefinished.placemanger" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>成品库库位管理</title>
    <script src="../Scripts/vue.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="example">
            <input type="text" v-model="didi" />
            <input type="text" v-model="family" />
            <br />
            didi={{didi}},family={{family}},didifamily={{didifamily}}
        </div>
        <script>
            var vm = new Vue({
                el: '#example',
                data: {
                    didi: 'didi',
                    family: 'family'
                },
                computed: {
                    didifamily: {
                        get: function () {
                            return this.didi + ' ' + this.family
                        },
                        set: function (newVal) {
                            var names = newVal.split(' ')
                            this.didi = names[0]
                            this.family = names[1]
                        }
                    }
                }
            })
        </script>

        <div id="app">
            <p>{{ message }}</p>
        </div>

        <script>
            new Vue({
                el: '#app',
                data: {
                    message: 'Hello Vue.js!'
                }
            })

            vm.didi = "set"
            vm.family = "测试"
        </script>



        <div id="items">
            <my-item v-repeat="items" inline-template>
                <button>{{fulltext}}</button>
            </my-item>
        </div>

        <script>
            var items = [
                { number: 1, text='one' },
                { number: 2, text='two' }
            ]

            var vue = new Vue({
                el: '#items',
                data: { items: items },
                components: {
                    'my-item': {
                        replace: true,
                        computed: {
                            fulltext: function () {
                                return 'item ' + this.text
                            }
                        }
                    }
                }
            })

        </script>



        <div id="txt_1">
            <%--<span>欢迎加入 {{name}} 家族</span><br />
        <input type="text" v-model="name" placeholder="请输入家族名" />--%>

            <%--<input type="checkbox" id="checkbox" v-model="checked" />
            <label for="checkbox">{{checked}}</label>--%>

            <%--<input type="checkbox" id="flash" value="flash" v-model="bizLines" />
            <label for="flash">快车</label>
            <input type="checkbox" id="premium" value="premium" v-model="bizLines" />
            <label for="premium">专车</label>
            <input type="checkbox" id="bus" value="bus" v-model="bizLines" />
            <label for="bus">巴士</label><br />--%>

            <select v-model="bizLines">
                <option v-for="option in options" :value="option.value">
                    {{option.text}}
                </option>
            </select>
            <span>Checked Lines:{{bizLines | json}}</span>
        </div>
        <script>
            var vm_1 = new Vue({
                el: '#txt_1',
                data: {
                    //name: 'Vue'
                    //checked:true
                    bizLines: 'premium',
                    options: [
                        { text: '快车', value: 'flash' },
                        { text: '专车', value: 'premium' },
                        { text: '巴士', value: 'bus' }
                    ]
                }
            })
        </script>


        <div id="dynamicfilterby">
            <input v-model="name" type="text" />
            <ul>
                <li v-for="user in users | orderBy 'Bruce' ">
                    {{user.name}}
                </li>
            </ul>
        </div>

        <script>
            new Vue({
                el: '#dynamicfilterby',
                data: {
                    name: '',
                    users: [
                        { name: 'Bruce' },
                        { name: 'Chuck' },
                        { name: 'Jackie' }
                    ]
                }
            })
        </script>

    </form>
</body>
</html>
