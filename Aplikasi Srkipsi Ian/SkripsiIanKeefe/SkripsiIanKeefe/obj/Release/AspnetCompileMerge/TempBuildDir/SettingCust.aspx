<%@ Page Title="" Language="C#" MasterPageFile="~/MainCustomer.Master" AutoEventWireup="true" CodeBehind="SettingCust.aspx.cs" Inherits="SkripsiIanKeefe.SettingCust" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Profile</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Profile Paging</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Nama Depan</li>
                        <asp:TextBox runat="server" ID="txtboxcustfirstname" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Nama Belakang</li>
                        <asp:TextBox runat="server" ID="txtboxcustlastname" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Email</li>
                        <asp:TextBox runat="server" ID="txtboxemail" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Phone Number</li>
                        <asp:TextBox runat="server" ID="txtboxphonenumber" />
                        <asp:RegularExpressionValidator ErrorMessage="Format Nomor Telpon Salah"
                            ValidationExpression="^(\+62|62|0)8[1-9][0-9]{6,9}$"
                            ForeColor="Red" ControlToValidate="txtboxphonenumber" runat="server" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Password</li>
                        <asp:TextBox runat="server" ID="txtboxpassowrd" TextMode="Password" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Confirm Password</li>
                        <asp:TextBox runat="server" ID="txtboxconfirmpassword" TextMode="Password" />
                    </div>
                </div>
            </div>
            <asp:UpdatePanel ID="uppass" Visible="false" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label Text="Password Salah / Tidak sesuai dengan autentikasi" ID="lblpass" ForeColor="Red" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>

            <br />
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <asp:LinkButton Text="Change" CssClass="btn btn-primary" runat="server" ID="ltlchange" OnClick="ltlchange_Click" />
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="form-floating">
                        <asp:LinkButton Text="Change Password" CssClass="btn btn-primary" runat="server" ID="ltlchangepass" OnClick="ltlchangepass_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
