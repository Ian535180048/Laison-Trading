<%@ Page Title="" Language="C#" MasterPageFile="~/MainCustomer.Master" AutoEventWireup="true" CodeBehind="SuccessPembayaran.aspx.cs" Inherits="SkripsiIanKeefe.SuccessPembayaran" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Request Pembelian Sukses</h1>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <p class="mb-0">
                    Request atas pembelian berhasil. 

                </p>
                <br />
                <br />
                <p class="mb-0">
                    Mohon melakukan pembayaran melalui transfer ke nomor rekening 1271380238. 
                </p>
                <br />
                <br />
                <p class="mb-0">
                    Mohon melampirkan bukti pembayaran setelah melakukan pembayaran terhadap produk Toko Laison.
                </p>
                <br />
                <br />
                <p class="mb-0">
                    Batas waktu pembayaran 24 jam. 
                </p>
                <br />
                <br />
                <p class="mb-0">
                    Terima Kasih telah berbelanja di Toko Laison.
                </p>
            </div>
        </div>
    </div>

</asp:Content>
