<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportForm.aspx.cs" Inherits="Exer1.ReportForm" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Report</title>
    <link rel="stylesheet"  href="Content/bootstrap.min.css" />
    <style type="text/css">
        .h-75 {}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="col-12 p-3">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" OnClick="Button1_Click" Text="Go back to Main" />
            <br />
            <rsweb:ReportViewer CssClass="w-100 h-75" ID="ReportViewer1" runat="server" Width="1077px">
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
