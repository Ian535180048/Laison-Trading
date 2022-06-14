<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="SkripsiIanKeefe.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Forgot Password</title>
    <link href="css/styles.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/js/all.min.js" crossorigin="anonymous"></script>
</head>
<body class="bg-primary">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="smApplication" />
        <div>
            <div id="layoutAuthentication">
                <div id="layoutAuthentication_content">
                    <main>
                        <div class="container">
                            <div class="justify-content-center">
                                <div class="col-lg-15">
                                    <div class="card shadow-lg border-0 rounded-lg mt-5">
                                        <div class="card-header">
                                            <h3 class="text-center font-weight-light my-4">Password Recovery</h3>
                                        </div>
                                        <div class="card-body">
                                            <div class="small mb-3 text-muted">Masukkan password barumu.</div>
                                            <form>
                                                <div class="row mb-3">
                                                    <div class="col-md-6">
                                                        <div class="form-floating mb-3">
                                                            <%--<input class="form-control" id="inputEmail" type="email" placeholder="name@example.com" />--%>
                                                            <asp:Label ID="lblusername" Text="Username" runat="server" />
                                                            <asp:Label ID="lblaktual" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row mb-3">
                                                    <div class="col-md-6">
                                                        <div class="form-floating">
                                                            <asp:TextBox CssClass="form-control" ID="txtboxpassword" placeholder="Enter your Password" TextMode="Password" runat="server" />
                                                            <label for="inputPassword">Password</label>
                                                            <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="txtboxpassword" ForeColor="Red" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-floating mb-3">
                                                            <asp:TextBox CssClass="form-control" ID="txtboxpasswordconfirm" placeholder="Confirm your Password" TextMode="Password" runat="server" />
                                                            <%--<input class="form-control" id="inputPasswordConfirm" type="password" placeholder="Confirm password" />--%>
                                                            <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="txtboxpasswordconfirm" ForeColor="Red" runat="server" />
                                                            <label for="inputPasswordConfirm">Confirm Password</label>
                                                            <asp:Label Text="Password Tidak Sama" ID="lblpasscon" ForeColor="Red" Visible="false" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="d-flex align-items-center justify-content-between mt-4 mb-0">
                                                    <asp:LinkButton Text="Back" CssClass="small" ID="lbback" OnClick="lbback_Click" runat="server" CausesValidation="false" />
                                                    <asp:Button Text="Reset" ID="btnreset" CssClass="btn btn-primary" OnClick="btnreset_Click" runat="server" />
                                                </div>
                                            </form>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </main>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
