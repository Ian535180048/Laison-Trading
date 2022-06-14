<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AddStokBarang.aspx.cs" Inherits="SkripsiIanKeefe.AddStokBarang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Pembelian Stok Barang</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Penambahan Transaksi Pembelian Stok Barang</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Nama Pemasok</li>
                        <asp:DropDownList runat="server" ID="ddlsupplier">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Required" ControlToValidate="ddlsupplier" runat="server" ForeColor="Red" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Nama Produk</li>
                        <asp:DropDownList runat="server" ID="ddlproduk">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Required" ControlToValidate="ddlproduk" runat="server" ForeColor="Red" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Tanggal Transaksi</li>
                        <asp:TextBox ID="txtboxtanggal" TextMode="Date" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Required" ControlToValidate="txtboxtanggal" runat="server" ForeColor="Red" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Total Barang</li>
                        <asp:TextBox runat="server" ID="txtboxammount" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Required" ControlToValidate="txtboxammount" runat="server" ForeColor="Red" />
                        <asp:RegularExpressionValidator ErrorMessage="Number Only" ControlToValidate="txtboxammount" ValidationExpression="^\d+$" runat="server" ForeColor="Red" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Total Harga</li>
                        <asp:TextBox ID="txtboxharga" runat="server" AutoPostBack="true" OnTextChanged="txtboxharga_TextChanged"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Required" ControlToValidate="txtboxharga" runat="server" ForeColor="Red" />
                        <asp:RegularExpressionValidator ErrorMessage="Number Only" ControlToValidate="txtboxharga" ValidationExpression="[0-9]+(,[0-9]+)*" runat="server" ForeColor="Red" />
                    </div>
                </div>
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
    <br />
</asp:Content>
