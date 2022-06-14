<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="SkripsiIanKeefe.AddProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Product</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Add Product</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Nama Product</li>
                        <asp:TextBox runat="server" ID="txtboxnamaproduct" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Required" ControlToValidate="txtboxnamaproduct" runat="server" ForeColor="Red" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Stock Total</li>
                        <asp:TextBox ID="txtboxstokproduct" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Required" ControlToValidate="txtboxstokproduct" runat="server" ForeColor="Red" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Total Harga</li>
                        <asp:TextBox ID="txtboxharga" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Required" ControlToValidate="txtboxharga" runat="server" ForeColor="Red" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ErrorMessage="Number Only" ControlToValidate="txtboxharga" ValidationExpression="^\d+$" runat="server" ForeColor="Red" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Gambar</li>
                        <asp:FileUpload ID="flpdgambar" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Required" ControlToValidate="flpdgambar" runat="server" ForeColor="Red" />

                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Berat Produk</li>
                        <asp:TextBox ID="txtboxberat" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Required" ControlToValidate="txtboxberat" runat="server" ForeColor="Red" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ErrorMessage="Number Only" ControlToValidate="txtboxberat" ValidationExpression="^\d+$" runat="server" ForeColor="Red" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <asp:LinkButton Text="Submit" CssClass="btn btn-primary" runat="server" ID="lbadd" OnClick="lbadd_Click" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <asp:LinkButton Text="Cancel" CssClass="btn btn-primary" runat="server" ID="lbcancel" OnClick="lbcancel_Click" CausesValidation="false" />
                    </div>
                </div>

            </div>
        </div>
        <br />
</asp:Content>
