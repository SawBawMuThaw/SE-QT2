<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="Exer1.LoginForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet"  href="Content/bootstrap.min.css" />
</head>
    <div class="container">
        <div class="col-4 m-auto">
            <form id="form1" runat="server">
                    <div>
                        <div class="mb-3">
                            <!-- Username
                            or Email&nbsp; -->
                            <label for="inputtxtBox" class="form-label">Username or Email</label>
                            <asp:TextBox ID="inputtxtBox" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        
                        <!-- <br /> 
                        Password&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-->
                        <div class="mb-3">
                            <label for="txtPwd" class="form-label">Password</label>
                            <asp:TextBox CssClass="form-control" ID="txtPwd" type="password" runat="server"></asp:TextBox>
                        </div>
                        
                        <br />
                        <asp:Button ID="LoginBtn" CssClass="btn btn-primary m-auto" runat="server" Text="Login" OnClick="LoginBtn_Click" />
                        <br />
                        <div class="text-center">
                            <asp:Label ID="Label1"  runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>
                </form>
        </div>
    </div>
<body>
    

    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
