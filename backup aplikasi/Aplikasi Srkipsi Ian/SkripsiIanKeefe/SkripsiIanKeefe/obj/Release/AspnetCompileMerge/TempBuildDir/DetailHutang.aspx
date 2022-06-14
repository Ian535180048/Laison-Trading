<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DetailHutang.aspx.cs" Inherits="SkripsiIanKeefe.DetailHutang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Hutang</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Detail Hutang</li>
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
                        <li class="breadcrumb-item active">Nama Klien</li>
                        <asp:Label runat="server" ID="lblnamaclient" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <li class="breadcrumb-item active">Tanggal Transaksi</li>
                    <asp:Label runat="server" ID="lbltanggaltransaksi" />
                </div>
            </div>


            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Status</li>
                        <asp:Label ID="lblstatus" runat="server" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Total Hutang</li>
                        <asp:Label ID="lbltotalhutang" runat="server" />
                    </div>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Sisa Hutang</li>
                        <asp:Label ID="lblsisa" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
