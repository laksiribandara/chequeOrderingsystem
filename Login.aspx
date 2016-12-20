<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtusn" runat="server" TabIndex="1" Text="6915"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="login" onclick="Button1_Click" 
            TabIndex="2" />
            <asp:Label ID="txt" runat="server"></asp:Label><asp:Label ID="Label1" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
