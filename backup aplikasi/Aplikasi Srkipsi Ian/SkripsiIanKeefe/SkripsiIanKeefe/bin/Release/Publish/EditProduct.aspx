<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="SkripsiIanKeefe.EditProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Produk</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Edit Produk</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Nomor Produk</li>
                        <asp:Label id="lblprodno" runat="server" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Nama Produk</li>
                        <asp:TextBox runat="server" ID="txtboxproductname" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Required" ControlToValidate="txtboxproductname" runat="server" ForeColor="Red" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Berat Produk Dalam Hitungan Gram</li>
                        <asp:TextBox ID="txtboxberat" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Required" ControlToValidate="txtboxberat" runat="server" ForeColor="Red" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ErrorMessage="Number Only" ControlToValidate="txtboxberat" ValidationExpression="^\d+$" runat="server" ForeColor="Red" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Total Produk</li>
                        <asp:TextBox runat="server" ID="txtboxammount" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Required" ControlToValidate="txtboxammount" runat="server" ForeColor="Red" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ErrorMessage="Number Only" ControlToValidate="txtboxammount" ValidationExpression="^\d+$" runat="server" ForeColor="Red" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Harga Transaksi</li>
                        <asp:TextBox ID="txtboxharga" runat="server" OnTextChanged="txtboxharga_TextChanged" AutoPostBack="true" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Required" ControlToValidate="txtboxharga" runat="server" ForeColor="Red" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ErrorMessage="Number Only" ControlToValidate="txtboxharga" ValidationExpression="[0-9]+(,[0-9]+)*" runat="server" ForeColor="Red" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Gambar </li>
                        <asp:FileUpload runat="server" ID="flpdgambar" />
                    </div>
                </div>


            <br />
                        <li class="breadcrumb-item active">Deskripsi Produk </li>
                        <asp:TextBox ID="txtboxdeskripsi" TextMode="MultiLine" Rows="8" runat="server" />
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <asp:LinkButton Text="Submit" CssClass="btn btn-success" runat="server" ID="lbadd" OnClick="lbadd_Click" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <asp:LinkButton Text="Cancel" CssClass="btn btn-primary" runat="server" ID="lbcancel" OnClick="lbcancel_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
            </div>
    </div>
    <br />
</asp:Content>
