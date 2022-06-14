<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DetailProduct.aspx.cs" Inherits="SkripsiIanKeefe.DetailProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Produk</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Rincian Produk</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">No Produk</li>
                        <asp:Label ID="lblprodno" runat="server" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Nama Produk</li>
                        <asp:Label ID="lblprodname" runat="server" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Berat Produk Dalam Hitungan Gram</li>
                        <asp:Label ID="lblberat" runat="server" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Kapasitas Produk</li>
                        <asp:Label ID="lblamt" runat="server" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Harga Transaksi</li>
                        <asp:Label ID="lblharga" runat="server" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Gambar</li>
                        <asp:Image ID="img" runat="server" Style="width: 380px; height: 380px;" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Deskripsi</li>
                        <asp:Label ID="lbldescr" runat="server" />
                    </div>
                </div>
            </div>

        </div>
    </div>
    <br />
</asp:Content>
