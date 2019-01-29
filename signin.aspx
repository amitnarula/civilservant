<%@ Page Title="" Language="C#" AutoEventWireup="true" Inherits="_Signin" CodeFile="~/signin.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Login</title>
    <!-- CSS -->
    <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Roboto:400,100,300,500">
    <link rel="stylesheet" href="assets/bootstrap/css/bootstrap.min.css">
    <link rel="stylesheet" href="assets/font-awesome/css/font-awesome.min.css">
    <link rel="stylesheet" href="assets/css/form-elements.css">
    <link rel="stylesheet" href="assets/css/style.css">
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
            <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
            <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
        <![endif]-->
    <!-- Favicon and touch icons -->
    <link rel="shortcut icon" href="assets/ico/fav-icon.png">
</head>
<body>
    <form runat="server">
    <div class="top-content">
        <div class="inner-bg">
            <div class="container">
                <div class="row">
                    <div class="col-sm-5 form-box m-auto">
                        <div class="form-top">
                            <div class="form-top-left">
                                <h3>
                                    Login</h3>
                                <p>
                                    <asp:Label ID="lblmessage" runat="server" Visible="false"></asp:Label></p>
                            </div>
                            <div class="form-top-right">
                                <i class="fa fa-pencil"></i>
                            </div>
                        </div>
                        <div class="form-bottom">
                            <form role="form" action="" method="post" class="registration-form">
                            <div class="form-group">
                                <label class="sr-only" for="form-user-name">
                                    Username</label>
                                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-first-name form-control" placeholder="Username"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label class="sr-only" for="form-password">
                                    Password</label>
                                <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-email form-control" placeholder="Password" runat="server"></asp:TextBox>
                            </div>
                            <asp:Button ID="btnLogin" CssClass="btn" Text="Login" runat="server" OnClick="btnlogin_Click" />
                            <a href="~/recoveraccount.aspx" id="A1" runat="server">Forgot password</a>
                            </form>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <!-- Javascript -->
    <script src="assets/js/jquery-1.11.1.js"></script>
    <script src="assets/bootstrap/js/bootstrap.js"></script>
    <script src="assets/js/jquery.backstretch.js"></script>
    <script src="assets/js/scripts.js"></script>
    <!--[if lt IE 10]>
            <script src="assets/js/placeholder.js"></script>
        <![endif]-->
    </form>
</body>
</html>
