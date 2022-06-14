<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AddPembayaranHutang.aspx.cs" Inherits="SkripsiIanKeefe.AddPembayaranHutang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Pembayaran Hutang</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Add Transaction Pembayaran Hutang</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Client Name :
                            <asp:Label ID="lblclientname" runat="server" /></li>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Required" ControlToValidate="ddlclient" runat="server" ForeColor="Red" />--%>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">No Transaksi :
                            <asp:Label ID="lbltransno" runat="server" /></li>
                            <asp:Label id="lbltanggal" Visible="false" runat="server" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Hutang Ammount :
                            <asp:Label ID="lblhutangammount" runat="server" /></li>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Sisa Hutang :
                            <asp:Label ID="lblsisaammount" runat="server" /></li>
                    </div>
                </div>
            </div>

            <br />
            <asp:Label Text="Data Pembayaran Hutang" runat="server" Font-Bold="true" />

            <br />
            <br />
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Ammount Hutang Yang Terbayar</li>
                        <asp:TextBox runat="server" ID="txtboxammount" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Required" ControlToValidate="txtboxammount" runat="server" ForeColor="Red" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ErrorMessage="Number Only" ControlToValidate="txtboxammount" ValidationExpression="^\d+$" runat="server" ForeColor="Red" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Tanggal Transaksi</li>
                        <asp:TextBox ID="txtboxtanggal" TextMode="Date" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Required" ControlToValidate="txtboxtanggal" runat="server" ForeColor="Red" />
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
            <asp:Label Text="Tanggal Transaksi Lebih Kecil Dibandingkan Transaksi Pengajuan Hutang" ID="lblerrortanggal" ForeColor="Red" Visible="false" runat="server" />
            <asp:Label Text="Ammount Lebih Besar Dibandingkan Sisa Hutang Ammount" ID="lblerrorammount" ForeColor="Red" Visible="false" runat="server" />
        </div>
    </div>
    <br />
</asp:Content>
