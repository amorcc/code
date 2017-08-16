<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="log4net_init.aspx.cs" Inherits="UC_WebAPI.LogMng.log4net_init" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        .btn {
            line-height: 30px;
            font-size: 14px;
            padding: 6px;
        }

        .model_block {
            margin-top: 30px;
        }

            .model_block .row {
                line-height: 28px;
                font-size: 14px;
                padding: 5px;
            }

        .versionStr {
            color: red;
            margin-left: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="model_block">
            <div class="row" style="font-size: 20px; font-weight: 700;">
                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            </div>
        </div>
        <div class="model_block">
            <div class="row" style="font-size: 20px; font-weight: 700;">基础功能</div>
            <div style="margin-left: 30px;">
                <asp:Button runat="server" Text="初始化log4net" OnClick="Unnamed1_Click" />
                <asp:Button ID="Button1" runat="server" Text="测试错误日志" OnClick="Button1_Click" />
                <asp:Button ID="Button4" runat="server" Text="Debug日志" OnClick="Button4_Click" />
            </div>
            <div class="row">
                <asp:Label ID="Label1" runat="server" Text="" CssClass="versionStr"></asp:Label>
            </div>
        </div>
        <div class="model_block">
            <div class="row" style="font-size: 20px; font-weight: 700;">版本号查询</div>
            <div style="margin-left: 30px;">
                <span>录入BIN目录的完整dll文件名称</span>
                <asp:TextBox ID="TextBox1" runat="server" Text="my.system.services.order.dll" Width="300px">my.system.services.order.dll</asp:TextBox>
                <asp:Button ID="Button2" runat="server" Text="查询" OnClick="Button2_Click" />
            </div>
            <div class="row">
                <asp:Label ID="Label2" runat="server" Text="" CssClass="versionStr"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
