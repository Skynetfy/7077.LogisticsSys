<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sql.aspx.cs" Inherits="Sys.WebUI.Sql" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:TextBox TextMode="MultiLine" ID="tb1" runat="server" Rows="20" width="500"></asp:TextBox>
            <br />
            <asp:Button ID="btn1" runat="server" Text="执 行" OnClick="btn1_Click" />
            <br />
            <asp:Label ID="lb1" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
