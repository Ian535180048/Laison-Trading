<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SkripsiIanKeefe.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Register Account</title>
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
                            <div class="row justify-content-center">
                                <div class="col-lg-7">
                                    <div class="card shadow-lg border-0 rounded-lg mt-5">
                                        <div class="card-header">
                                            <h3 class="text-center font-weight-light my-4">Create Account</h3>
                                        </div>
                                        <div class="card-body">
                                            <form>
                                                <div class="row mb-3">
                                                    <div class="col-md-6">
                                                        <div class="form-floating mb-3 mb-md-0">
                                                            <asp:TextBox CssClass="form-control" ID="txtboxfirstname" placeholder="Enter your first name" runat="server" />
                                                            <%--<input class="form-control" id="inputFirstName" type="text" placeholder="Enter your first name" />--%>
                                                            <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="txtboxfirstname" ForeColor="Red" runat="server" />
                                                            <label for="txtboxfirstname">First name</label>
                                                            <asp:RegularExpressionValidator ErrorMessage="Tidak boleh ada angka" ControlToValidate="txtboxfirstname" runat="server"
                                                                ValidationExpression="[a-zA-Z ]*$" ForeColor="Red" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-floating">
                                                            <asp:TextBox CssClass="form-control" ID="txtboxlastname" placeholder="Enter your last name" runat="server" />
                                                            <%--<input class="form-control" id="inputLastName" type="text" placeholder="Enter your last name" />--%>
                                                            <label for="txtboxlastname">Last name</label>
                                                            <asp:RegularExpressionValidator ErrorMessage="Tidak boleh ada angka" ControlToValidate="txtboxlastname" runat="server"
                                                                ValidationExpression="[a-zA-Z ]*$" ForeColor="Red" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-floating mb-3">
                                                    <asp:TextBox CssClass="form-control" ID="txtboxemail" placeholder="name@example.com" runat="server" />
                                                    <asp:RegularExpressionValidator ErrorMessage="Format Email Salah" ControlToValidate="txtboxemail"
                                                        ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                                        Display="Dynamic" ForeColor="Red" runat="server" />
                                                    <%--<input class="form-control" id="inputEmail" type="email" placeholder="name@example.com" />--%>
                                                    <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="txtboxemail" ForeColor="Red" runat="server" />
                                                    <label for="txtboxemail">Email address</label>
                                                </div>
                                                <div class="row mb-3">
                                                    <div class="col-md-6">
                                                        <div class="form-floating mb-3 mb-md-0">
                                                            <asp:TextBox CssClass="form-control" ID="txtboxusername" placeholder="Enter your Username" runat="server" />
                                                            <%--<input class="form-control" id="inputFirstName" type="text" placeholder="Enter your first name" />--%>
                                                            <label for="txtboxusername">User Name</label>
                                                            <asp:Label Text="Username sudah ada " Visible="false" ID="lbluserada" ForeColor="Red" runat="server" />
                                                            <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="txtboxusername" ForeColor="Red" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-floating">
                                                            <asp:TextBox CssClass="form-control" ID="txtboxphonenumber" placeholder="Enter your Phone Number" TextMode="Phone" runat="server" />
                                                            <%--<input class="form-control" id="inputLastName" type="text" placeholder="Enter your last name" />--%>
                                                            <label for="txtboxphonenumber">Phone Number</label>
                                                            <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="txtboxphonenumber" ForeColor="Red" runat="server" />
                                                            <asp:RegularExpressionValidator ErrorMessage="Format Nomor Telpon Salah"
                                                                ValidationExpression="^(\+62|62|0)8[1-9][0-9]{6,9}$" 
                                                                ForeColor="Red" ControlToValidate="txtboxphonenumber" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row mb-3">
                                                    <div class="col-md-6">
                                                        <div class="form-floating mb-3 mb-md-0">
                                                            <asp:TextBox CssClass="form-control" ID="txtboxpassword" placeholder="Enter your Password" TextMode="Password" runat="server" />
                                                            <%--<input class="form-control" id="inputPassword" type="password" placeholder="Create a password" />--%>
                                                            <label for="inputPassword">Password</label>
                                                            <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="txtboxpassword" ForeColor="Red" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-floating mb-3 mb-md-0">
                                                            <asp:TextBox CssClass="form-control" ID="txtboxpasswordconfirm" placeholder="Confirm your Password" TextMode="Password" runat="server" />
                                                            <%--<input class="form-control" id="inputPasswordConfirm" type="password" placeholder="Confirm password" />--%>
                                                            <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="txtboxpasswordconfirm" ForeColor="Red" runat="server" />
                                                            <label for="inputPasswordConfirm">Confirm Password</label>
                                                            <asp:Label Text="Password Tidak Sama" ID="lblpasscon" ForeColor="Red" Visible="false" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="mt-4 mb-0">
                                                    <div class="d-grid">
                                                        <%--<a class="btn btn-primary btn-block" href="login.html">Create Account</a>--%>
                                                        <asp:Button CssClass="btn btn-primary btn-block" Text="Create Account" ID="btncreateaccount" OnClick="btncreateaccount_Click" runat="server" />
                                                    </div>
                                                </div>
                                            </form>
                                        </div>
                                        <div class="card-footer text-center py-3">
                                            <asp:LinkButton CssClass="small" Text="Have an account? Go to login" ID="lbback" OnClick="lbback_Click" runat="server" CausesValidation="false" />
                                            <%--<div class="small"><a href="login.html">Have an account? Go to login</a></div>--%>
                                            <asp:UpdatePanel runat="server" ID="uperror" Visible="false" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label Text="Nomor Telpon/ Username/ Email telah terdaftar" ForeColor="Red" ID="lblerror" runat="server" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
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
