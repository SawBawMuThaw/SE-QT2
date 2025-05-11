<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="Exer1.MainForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Main Form</title>
    <link rel="stylesheet"  href="Content/bootstrap.min.css" />
</head>
<body>
    <div class="container p-3">
        <div class="col-12">
            <form id="form1" runat="server">
                    <div>
                        <div>
                            <div class="m-auto btn-group">
                                <asp:Button ID="ItemBtn" CssClass="btn btn-outline-info" runat="server" Text="Item" OnClick="ItemBtn_Click" />
                                <asp:Button ID="AgentBtn" CssClass="btn btn-outline-info" runat="server" Text="Agent" OnClick="AgentBtn_Click" />
                                <asp:Button ID="OrderBtn" CssClass="btn btn-outline-info" runat="server" Text="Order" OnClick="OrderBtn_Click" />
                                <asp:Button ID="OrdDetBtn" CssClass="btn btn-outline-info" runat="server" Text="Order Details" OnClick="OrdDetBtn_Click" />
                            </div>
                            
                            <hr />
                            <div>
                                <asp:MultiView ID="MultiView1" runat="server" OnActiveViewChanged="MultiView1_ActiveViewChanged">
                                    <asp:View ID="OrderDetailView" runat="server">
                                        <asp:Label CssClass="form-label" ID="Label15" runat="server" Text="Order ID"></asp:Label>
                                        <br />
                                        <asp:TextBox CssClass="form-control" ID="txtOrdDetOrdId" runat="server"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="Label7" runat="server" CssClass="form-label" for="ItemList" Text="Item"></asp:Label>
                                        <asp:DropDownList ID="ItemList" runat="server" CssClass="form-select" DataTextField="Text" DataValueField="Value" OnSelectedIndexChanged="ItemList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="Label8" runat="server" CssClass="form-label" for="txtItemQuality" Text="Quantity"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtItemQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="Label9" runat="server" CssClass="form-label" for="txtUnitAmount" Text="Unit Amount"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtUnitAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                        <br />
                                        <asp:Label CssClass="form-label" ID="Label16" runat="server" Text="Order Detail ID"></asp:Label>
                                        <br />
                                        <asp:TextBox CssClass="form-control" ID="txtOrdDetID" runat="server"></asp:TextBox>
                                        <br />
                                        <asp:Button ID="AddItemBtn" runat="server" CssClass="btn btn-outline-info" OnClick="AddItemBtn_Click" Text="Add" />
                                        <asp:Button ID="RmvItemBtn" runat="server" CssClass="btn btn-outline-info" OnClick="RmvItemBtn_Click" Text="Remove" />
                                        <asp:Button ID="GetOrdDetBtn" runat="server" CssClass="btn btn-outline-info" OnClick="GetOrdDetBtn_Click" Text="Get Order Details" />
                                        <br />
                                        <br />
                                        <asp:Label ID="OrdDetErrLabel" runat="server" Text="Label"></asp:Label>
                                        <br />
                                        <asp:GridView CssClass="table" ID="OrdDetGrid" runat="server" Width="1256px">
                                        </asp:GridView>
                                    </asp:View>
                                    <asp:View ID="OrderView" runat="server">
                                        <div class="m-auto btn-group">
                                            <asp:Button ID="CreateOrdBtn" CssClass="btn btn-outline-info" runat="server" OnClick="CreateOrdBtn_Click" Text="Create New Order" />
                                            <asp:Button ID="GetOrdBtn" CssClass="btn btn-outline-info" runat="server" OnClick="GetOrdBtn_Click" Text="Get Order" />
                                            <asp:Button ID="AllOrdsBtn" CssClass="btn btn-outline-info" runat="server" OnClick="AllOrdsBtn_Click" Text="Show All Orders" />
                                            <asp:Button ID="GetReportBtn" CssClass="btn btn-outline-info" runat="server" OnClick="GetReportBtn_Click" Text="Get Report" />
                                        </div>
                                        <br />
                                        <asp:Label ID="Label12" CssClass="form-label" for="txtAgentId" runat="server" Text="AgentId"></asp:Label>
                                        <!-- &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; -->
                                        <asp:TextBox ID="txtAgentId" CssClass="form-control" runat="server"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="Label10" CssClass="form-label" runat="server" Text="OrderID"></asp:Label>
                                        <!-- &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; -->
                                        &nbsp;&nbsp;<asp:TextBox ID="txtOrdViewOrdId" runat="server"></asp:TextBox>
                                        <br />
                                        <!-- &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; -->
                                        <br />
                                        <!-- &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; -->
                                        <br />
                                        <!-- &nbsp;&nbsp;-->
                                        <br />
                                        <br />
                                        <div class="m-auto btn-group">
                                        </div>
                                        <br />
                                        <br />
                                        <asp:Label ID="ErrorLabel" CssClass="m-auto" runat="server" Text="Label"></asp:Label>
                                        <br />
                                        <asp:GridView CssClass="table" ID="OrderGrid" runat="server">
                                        </asp:GridView>
                                    </asp:View>
                                    <asp:View ID="AgentView" runat="server">
                                        <asp:Label ID="Label5" CssClass="form-label" for="txtName" runat="server" Text="Name"></asp:Label>
                                        <!-- &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; -->
                                        <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="Label6" CssClass="form-label" for="txtAddr" runat="server" Text="Address"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:TextBox ID="txtAddr" CssClass="form-control" runat="server"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="Label13" CssClass="form-label" for="txtAgentViewId" runat="server" Text="Agent ID "></asp:Label>
                                        <asp:TextBox ID="txtAgentViewId" CssClass="form-control" runat="server"></asp:TextBox>
                                        <br />
                                        <div class="m-auto btn-group">
                                            <asp:Button ID="Button1" CssClass="btn btn-outline-info" runat="server" Text="Create" OnClick="Button1_Click" />
                                            <asp:Button ID="UpdateAgentBtn" CssClass="btn btn-outline-info" runat="server" Text="Update" OnClick="UpdateAgentBtn_Click" />
                                            <asp:Button ID="DeleteAgentBtn" CssClass="btn btn-outline-info" runat="server" Text="Delete" OnClick="DeleteAgentBtn_Click" />
                                        </div>
                                        
                                        <br />
                                        <br />
                                        <asp:Label ID="AgentErrorLabel" CssClass="m-auto" runat="server" Text="Label"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:GridView ID="AgentGrid" CssClass="table" runat="server" OnSelectedIndexChanged="AgentGrid_SelectedIndexChanged">
                                        </asp:GridView>
                                        <br />
                                    </asp:View>
                                    <asp:View ID="ItemView" runat="server">
                                        <asp:Label ID="Label1" CssClass="form-label" for="txtItemName" runat="server" Text="Name"></asp:Label>
                                        <!-- &nbsp;&nbsp;-->
                                        <asp:TextBox ID="txtItemName" CssClass="form-control" runat="server"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="Label2" CssClass="form-label" for="txtItemSize" runat="server" Text="Size"></asp:Label>
                                        <!-- &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; -->
                                        <asp:TextBox ID="txtItemSize" CssClass="form-control" runat="server"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="Label3" CssClass="form-label" for="txtItemStock" runat="server" Text="Stock"></asp:Label>
                                        &nbsp;&nbsp;
                                        <asp:TextBox ID="txtItemStock" CssClass="form-control" runat="server"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="Label14" CssClass="form-label" for="txtItemViewId" runat="server" Text="Item ID"></asp:Label>
                                        &nbsp;<asp:TextBox ID="txtItemViewId" CssClass="form-control" runat="server"></asp:TextBox>
                                        <br />
                                        <br />
                                        <div class="m-auto btn-group">
                                            <asp:Button ID="AddBtn" CssClass="btn btn-outline-info" runat="server" OnClick="AddBtn_Click" Text="Add" />
                                            <asp:Button ID="DelBtn" CssClass="btn btn-outline-info" runat="server" OnClick="DelBtn_Click" Text="Delete" />
                                            <asp:Button ID="UpdateBtn" CssClass="btn btn-outline-info" runat="server" OnClick="UpdateBtn_Click" Text="Update" />
                                        </div>
                                        
                                        <br />
                                        <br />
                                        <asp:Label ID="ItemErrorLabel" CssClass="m-auto" runat="server" Text="Label"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label4" CssClass="form-label" for="DropDownList1" runat="server" Text="Sort By"></asp:Label>
                                        &nbsp;<asp:DropDownList CssClass="form-select" ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <br />
                                        <asp:GridView ID="ItemGrid" CssClass="table" runat="server" AutoGenerateSelectButton="True">
                                        </asp:GridView>
                                        <br />
                                    </asp:View>
                                </asp:MultiView>
                            </div>
                        </div>
                    </div>
                </form>
        </div>
    </div>
    
</body>
</html>
