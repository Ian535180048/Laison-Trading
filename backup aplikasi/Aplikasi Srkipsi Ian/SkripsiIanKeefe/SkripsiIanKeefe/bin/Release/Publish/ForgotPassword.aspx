<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="SkripsiIanKeefe.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Password Reset</title>
    <link href="css/styles.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/js/all.min.js" crossorigin="anonymous"></script>
</head>
<body class="bg-primary">

    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="smApplication" />
        <div>
            <div id="layoutAuthentication_content">
                <main>
                    <div class="container">
                        <div class="row justify-content-center">
                            <div class="col-lg-5">
                                <div class="card shadow-lg border-0 rounded-lg mt-5">
                                    <div class="card-header">
                                        <h3 class="text-center font-weight-light my-4">Password Recovery</h3>
                                    </div>
                                    <div class="card-body">
                                        <div class="small mb-3 text-muted">Masukkan email yang akan di recovery.</div>
                                        <form>
                                            <div class="row mb-3">
                                                <div class="col-md-6">
                                                    <div class="form-floating mb-3">
                                                        <%--<input class="form-control" id="inputEmail" type="email" placeholder="name@example.com" />--%>
                                                        <asp:TextBox CssClass="form-control" ID="txtboxemail" placeholder="name@example.com" runat="server" />
                                                        <label for="inputEmail">Email address</label>
                                                    </div>

                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-floating">
                                                        <asp:TextBox CssClass="form-control" ID="txtboxusername" placeholder="Enter your username" runat="server" />
                                                        <%--<input class="form-control" id="inputLastName" type="text" placeholder="Enter your last name" />--%>
                                                        <label for="txtboxlastname">Username</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row mb-3">
                                                <div class="col-md-6">
                                                    <div class="form-floating mb-3">
                                                        <%--<input class="form-control" id="inputEmail" type="email" placeholder="name@example.com" />--%>
                                                        <asp:TextBox CssClass="form-control" ID="txtboxphonenumber" placeholder="Enter your Phone Number" TextMode="Phone" runat="server" />
                                                        <label for="txtboxphonenumber">Phone Number</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="d-flex align-items-center justify-content-between mt-4 mb-0">
                                                <asp:LinkButton CssClass="small" Text="Back" ID="lb_back" runat="server" OnClick="lb_back_Click" />
                                                <asp:Button CssClass="btn btn-primary" Text="Reset Password" ID="btnreset" runat="server" OnClick="btnreset_Click" />
                                                <%--<a class="btn btn-primary" href="login.html">Reset Password</a>--%>
                                            </div>
                                            <asp:UpdatePanel runat="server" ID="uperror" Visible="false" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label Text="Nomor Telpon/ Username/ Email tidak terdaftar" ForeColor="Red" ID="lblerror" runat="server" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </main>
            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.0/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
    <script src="js/scripts.js"></script>
</body>
</html>
