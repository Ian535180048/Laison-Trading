<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DetailPembelianStok.aspx.cs" Inherits="SkripsiIanKeefe.DetailPembelianStok" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Pembelian Stok Barang</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Detail Pembelian Stok Barang</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Nomor Transaksi</li>
                        <asp:Label runat="server" ID="lbltransno" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Nama Pemasok</li>
                        <asp:Label runat="server" ID="lblsuppliernama" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Tanggal</li>
                        <asp:Label runat="server" ID="lbltanggal" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Nama Produk</li>
                        <asp:Label runat="server" ID="lblprodnama" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Total</li>
                        <asp:Label ID="lbltotal" runat="server" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Total Stok</li>
                        <asp:Label runat="server" ID="lblstokamt" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
</asp:Content>
