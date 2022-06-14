<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DetailPembayaranHutang.aspx.cs" Inherits="SkripsiIanKeefe.DetailPembayaranHutang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Pembayaran Hutang</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Detail Pembayaran Hutang</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Trans Pembayaran Hutang No</li>
                        <asp:Label ID="lbltransno" runat="server" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Trans Hutang No</li>
                        <asp:Label ID="lbltranshutangno" runat="server" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Tanggal Transaksi</li>
                        <asp:Label ID="lbltanggal" runat="server" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Ammount</li>
                        <asp:Label ID="lblamt" runat="server" />
                    </div>
                </div>
            </div>

        </div>
    </div>
    <br />

</asp:Content>
