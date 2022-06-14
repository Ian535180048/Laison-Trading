<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SkripsiIanKeefe.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
        <meta name="description" content="" />
        <meta name="author" content="" />
        <title>Login - Laison Trading</title>
        <link href="css/styles.css" rel="stylesheet" />
        <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/js/all.min.js" crossorigin="anonymous"></script>
</head>
 <body class="bg-primary">
    <form id="form1" runat="server">
    <div>
    <div id="layoutAuthentication">
            <div id="layoutAuthentication_content">
                <main>
                    <div class="container">
                        <div class="row justify-content-center">
                            <div class="col-lg-5">
                                <div class="card shadow-lg border-0 rounded-lg mt-5">
                                    <div class="card-header"><h3 class="text-center font-weight-light my-4">Login</h3></div>
                                    <div class="card-body">
                                        <form>
                                            <div class="form-floating mb-3">
                                                <asp:textbox CssClass="form-control" ID="txtboxusername" placeholder="name@example.com" runat="server" />
                                                <%--<input class="form-control" id="inputEmail" type="email" placeholder="name@example.com" />--%>
                                                <label for="inputEmail">Username</label>
                                            </div>
                                            <div class="form-floating mb-3">
                                                <asp:textbox CssClass="form-control" ID="txtboxinputpassword" placeholder="Password" TextMode="Password" runat="server" />
                                                <%--<input class="form-control" id="inputPassword" type="password" placeholder="Password" />--%>
                                                <label for="inputPassword">Password</label>
                                            </div>
                                            <div class="d-flex align-items-center justify-content-between mt-4 mb-0">
                                                <asp:LinkButton CssClass="small" Text="Forgot Password" ID="btn_forgot" OnClick="btn_forgot_Click" runat="server" />
                                                <asp:linkbutton text="Login" ID="btn_login" OnClick="btn_login_Click" CssClass="btn btn-primary"  runat="server" />
                                                
                                            </div>
                                            <asp:label CssClass="text-danger" text="Incorrect Username Or Password" runat="server" ID="lblincorrect" Visible="false" />
                                        
                                        </form>
                                    </div>
                                    <div class="card-footer text-center py-3">
                                        <%--<div class="small"><a href="register.html">Need an account? Sign up!</a></div>--%>
                                        <asp:linkbutton CssClass="small" ID="lb_signup" text="Need an account? Sign up!" OnClick="lb_signup_Click" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </main>
            </div>
        </div>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.0/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
        <script src="js/scripts.js"></script>
    </div>
    </form>
</body>
</html>
